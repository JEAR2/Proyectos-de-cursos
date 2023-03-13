using System.Collections.Immutable;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;

using DrivenAdapters.Mongo.DbModels;

using MongoDB.Driver;

using Moq;

using Xunit;
using FluentAssertions;
using Helpers.Domain.Clientes;
using Helpers.Domain.Creditos;
using static MongoDB.Driver.UpdateResult;
using Helpers.Domain.Pagos;
using AutoMapper;
using credibank.AppServices.Automapper;

namespace DrivenAdapters.Mongo.Adapters.Tests;

public class RepositorioDeClientesAdapterTests
{
    private readonly Mock<IContext> _dbContextMock;
    private readonly Mock<IMongoCollection<DocumentoCliente>> _coleccionDeClientesMock;
    private readonly Mock<ITestAsyncCursor<DocumentoCliente>> _cursorDeClientesMock;
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;
    private readonly IMapper _mapper;

    public RepositorioDeClientesAdapterTests()
    {
        MapperConfiguration mapperConfig = new(
            options => options.AddProfile<ConfigurationProfile>()
        );
        _mapper = mapperConfig.CreateMapper();
        _dbContextMock = new Mock<IContext>();
        _coleccionDeClientesMock = new Mock<IMongoCollection<DocumentoCliente>>();
        _cursorDeClientesMock = new Mock<ITestAsyncCursor<DocumentoCliente>>();

        // El setup de este mock se debe colocar aquí para que al momento de instanciar
        // el repositorio de documentos la colección de Clientes dentro de IContext se inicialice de forma correcta.
        _dbContextMock
            .Setup(x => x.Clientes)
            .Returns(_coleccionDeClientesMock.Object);

        _repositorioDeClientes = new RepositorioDeClientesAdapter(_dbContextMock.Object, _mapper);
    }

    [Fact(DisplayName = "#AnexarCreditoAClienteAsync debería guardar un crédito dentro del cliente.")]
    public async Task AnexarCreditoAClienteAsync_DeberiaGuardarElCreditoDentroDelCliente()
    {
        Credito credito = CreditoTestBuilder.Builder()
            .ConId(Guid.NewGuid().ToString())
            .ConPagosRealizados(new List<Pago>())
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConId(Guid.NewGuid().ToString())
            .ConNombre("Juan Gonzales")
            .ConCedulaDeCiudadania("13284098324")
            .ConCreditos(new List<Credito>())
            .Build();
        _coleccionDeClientesMock
            .Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()
            ))
            .Returns(
                Task.FromResult<UpdateResult>(new Acknowledged(1, 1, cliente.Id))
            );

        // Act
        Credito creditoAsociado = await _repositorioDeClientes.AnexarCreditoAClienteAsync(cliente, credito);

        // Assert
        creditoAsociado.Should().NotBeNull();
        _coleccionDeClientesMock.Verify(
            x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
        );
    }

    [Fact(DisplayName = "#AnexarCuotaACreditoAsync debería guardar un pago dentro de un crédito del cliente.")]
    public async Task AnexarCuotaACreditoAsync_DeberiaGuardarUnPagoDentroDelCreditoDeUnCliente()
    {
        Pago pago = PagoTestBuilder.Builder()
            .Build();
        Credito credito = CreditoTestBuilder.Builder()
            .ConId(Guid.NewGuid().ToString())
            .ConPagosRealizados(new List<Pago>())
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConId(Guid.NewGuid().ToString())
            .ConNombre("Juan Gonzales")
            .ConCedulaDeCiudadania("13284098324")
            .ConCreditos(new List<Credito>() { credito })
            .Build();
        _coleccionDeClientesMock
            .Setup(x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()
            ))
            .Returns(
                Task.FromResult<UpdateResult>(new Acknowledged(1, 1, ""))
            );

        // Act
        Credito creditoModificado = await _repositorioDeClientes.AnexarCuotaACreditoAsync(cliente, credito, pago);

        // Assert
        creditoModificado.Should().NotBeNull();
        creditoModificado.PagosRealizados.Count.Should().Be(1);
        _coleccionDeClientesMock.Verify(
            x => x.UpdateOneAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateDefinition<DocumentoCliente>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
        );
    }

    [Fact(DisplayName = "#ClienteConDocumentoDeIdentidadExisteAsync debería retornar false cuando el cliente no está registrado.")]
    public async Task ClienteConDocumentoDeIdentidadExisteAsync_DeberiaRetornarFalse_CuandoElClienteNoEstaRegistrado()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Juan Gonzales")
            .ConCedulaDeCiudadania("13284098324")
            .ConCreditos(new List<Credito>())
            .Build();
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(new List<DocumentoCliente>());
        _cursorDeClientesMock
            .Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => false);
        _cursorDeClientesMock
            .Setup(x => x.AnyAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => false);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        bool clienteExiste = await _repositorioDeClientes.ClienteConDocumentoDeIdentidadExisteAsync(cliente.DocumentoDeIdentidad);

        // Assert
        clienteExiste.Should().BeFalse();
    }

    [Fact(DisplayName = "#ClienteConDocumentoDeIdentidadExisteAsync debería retornar true cuando el cliente está registrado.")]
    public async Task ClienteConDocumentoDeIdentidadExisteAsync_DeberiaRetornarTrue_CuandoElClienteEstaRegistrado()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Juan Gonzales")
            .ConCedulaDeCiudadania("13284098324")
            .ConCreditos(new List<Credito>())
            .Build();
        DocumentoCliente documento = new DocumentoCliente()
        {
            DocumentoDeIdentidad = cliente.DocumentoDeIdentidad
        };
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(new List<DocumentoCliente>() { documento });
        _cursorDeClientesMock
            .Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => true);
        _cursorDeClientesMock
            .Setup(x => x.AnyAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => true);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        bool clienteExiste = await _repositorioDeClientes.ClienteConDocumentoDeIdentidadExisteAsync(cliente.DocumentoDeIdentidad);

        // Assert
        clienteExiste.Should().BeTrue();
    }

    [Fact(DisplayName = "#GuardarAsync debería guardar el registro del usuario.")]
    public async Task GuardarAsync_DeberiaGuardarElRegistroDelUsuarioYRetornarUnaInstanciaConElIdCreado()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Pepito")
            .ConCedulaDeCiudadania("29432904832")
            .Build();
        _coleccionDeClientesMock
            .Setup(x => x.InsertOneAsync(
                It.IsAny<DocumentoCliente>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
            ))
            .Returns(Task.CompletedTask);

        // Act
        Cliente clienteGuardado = await _repositorioDeClientes.GuardarAsync(cliente);

        // Assert
        clienteGuardado.DocumentoDeIdentidad.Should().Be(cliente.DocumentoDeIdentidad);
    }

    [Fact(DisplayName = "#ObtenerPorDocumentoDeIdentidadAsync debería retornar null si el cliente no existe.")]
    public async Task ObtenerPorDocumentoDeIdentidadAsync_DeberiaRetornarNull_CuandoElClienteNoExiste()
    {
        // Arrange
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(new List<DocumentoCliente>());
        _cursorDeClientesMock
            .Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => false);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        Cliente clienteObtenido = await _repositorioDeClientes.ObtenerPorDocumentoDeIdentidadAsync("");

        // Assert
        clienteObtenido.Should().BeNull();
    }

    [Fact(DisplayName = "#ObtenerPorDocumentoDeIdentidadAsync debería retornar la información del cliente cuando este existe.")]
    public async Task ObtenerPorDocumentoDeIdentidadAsync_DeberiaRetornarLaInformacionDelCliente_CuandoEsteExiste()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Juan Gonzales")
            .ConCedulaDeCiudadania("13284098324")
            .ConCreditos(new List<Credito>())
            .Build();
        DocumentoCliente modelo = new DocumentoCliente()
        {
            Nombre = cliente.Nombre,
            DocumentoDeIdentidad = cliente.DocumentoDeIdentidad,
            Creditos = new List<DocumentoCredito>()
        };
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(new List<DocumentoCliente>() { modelo });
        _cursorDeClientesMock
            .Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => true);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        Cliente clienteObtenido = await _repositorioDeClientes.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad);

        // Assert
        clienteObtenido.Should().NotBeNull();
        clienteObtenido.DocumentoDeIdentidad.Should().Be(cliente.DocumentoDeIdentidad);
    }

    [Fact(DisplayName = "#ObtenerTodosAsync debería retornar los documentos de todos los Clientes registrados.")]
    public async Task ObtenerTodosAsync_DeberiaRetornarTodosLosRegistrosDeLosClientesRegistrados()
    {
        // Arrange
        List<DocumentoCliente> documentos = new()
        {
            new DocumentoCliente(),
            new DocumentoCliente(),
        };
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(documentos);
        _cursorDeClientesMock
            .Setup(x => x.ToList(It.IsAny<CancellationToken>()))
            .Returns(documentos);
        _cursorDeClientesMock
            .SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        IEnumerable<Cliente> results = await _repositorioDeClientes.ObtenerTodosAsync();
        ImmutableList<Cliente> clientesObtenidos = results.ToImmutableList();

        // Assert
        clientesObtenidos.Should().NotBeNullOrEmpty();
        _coleccionDeClientesMock.Verify(
            x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
        );
    }

    [Fact(DisplayName = "#ObtenerTodosAsync debería retornar una lista vacía cuando no hay Clientes registrados.")]
    public async Task ObtenerTodosAsync_DeberiaRetornarUnaListaVacia_CuandoNoHayClientesRegistrados()
    {
        // Arrange
        _cursorDeClientesMock
            .Setup(x => x.Current)
            .Returns(new List<DocumentoCliente>());
        _cursorDeClientesMock
            .Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => false);
        _coleccionDeClientesMock
            .Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<DocumentoCliente>>(),
                It.IsAny<FindOptions<DocumentoCliente, DocumentoCliente>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(_cursorDeClientesMock.Object);

        // Act
        IList<Cliente> clientesObtenidos = (await _repositorioDeClientes.ObtenerTodosAsync())
            .ToImmutableList();
        // Assert
        clientesObtenidos.Should().NotBeNull();
        clientesObtenidos.Should().BeEmpty();
    }
}

public interface ITestAsyncCursor<TDocument> : IAsyncCursor<TDocument>
{
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    List<TDocument> ToList(CancellationToken cancellationToken = default);
}