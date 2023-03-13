using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests;
using Domain.UseCase.Tiendas;
using Helpers.Commons.Exceptions;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class TiendaUseCaseTest
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ITiendaRepository> _mockTiendaRepository;
        private readonly Mock<ITipoRepository> _mockTipoRepository;

        private readonly TiendaUseCase _tiendaUseCase;

        public TiendaUseCaseTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            //Primera forma
            _mockTiendaRepository = _mockRepository.Create<ITiendaRepository>();
            //Segunda forma
            _mockTipoRepository = new();

            _tiendaUseCase = new(_mockTiendaRepository.Object, _mockTipoRepository.Object);
        }

        [Fact]
        public async Task ObtenerTiendasExitoso()
        {
            _mockTiendaRepository
                .Setup(repository => repository.ObtenerTiendasAsync())
                .ReturnsAsync(ObtenerTiendasTest);

            List<Tienda> tiendas = await _tiendaUseCase.ObtenerTiendas();

            _mockRepository.VerifyAll();
            Assert.NotEmpty(tiendas);
            Assert.NotNull(tiendas);
        }

        [Fact]
        public async Task ObtenerTiendasSinDatos()
        {
            _mockTiendaRepository
                .Setup(repository => repository.ObtenerTiendasAsync())
                .ReturnsAsync(new List<Tienda>());

            List<Tienda> tiendas = await _tiendaUseCase.ObtenerTiendas();

            _mockRepository.VerifyAll();
            Assert.Empty(tiendas);
            Assert.NotNull(tiendas);
        }

        [Theory]
        [InlineData("Tienda 1234", 1, "Físico")]
        [InlineData("Tienda 1234", 2, "Virtual")]
        public async Task CrearTiendasExitoso(string nombreTienda, int idTipo, string nombreTipo)
        {
            Tienda nuevaTienda = new TiendaBuilderTest()
                    .ConNombre(nombreTienda)
                    .ConTipo(new TipoBuilderTest().ConId(idTipo).Build())
                    .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build();

            _mockTiendaRepository
                .Setup(repository => repository.InsertarTiendaAsync(It.IsAny<Tienda>()))
                .ReturnsAsync(ObtenerTiendaTest(nombreTienda, idTipo, nombreTipo));

            _mockTipoRepository
                .Setup(repository => repository.ObtenerTipoPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new TipoBuilderTest().ConId(idTipo).ConNombre(nombreTipo).Build());

            Tienda tiendaCreada = await _tiendaUseCase.CrearTienda(nuevaTienda);

            _mockRepository.VerifyAll();

            _mockTipoRepository.Verify(repository => repository.ObtenerTipoPorIdAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(tiendaCreada);
            Assert.NotNull(tiendaCreada.Id);
            Assert.Equal("1234", tiendaCreada.Id);
            Assert.Equal(nombreTipo, tiendaCreada.Tipo.Nombre);
        }

        [Theory]
        [InlineData("Tienda 1234", 0, "Físico")]
        [InlineData("Tienda 1234", -1, "Virtual")]
        public async Task CrearTiendas_Con_IdTipo_Menor_A_Uno_Retorna_Excepcion(string nombreTienda, int idTipo, string nombreTipo)
        {
            Tienda nuevaTienda = new TiendaBuilderTest()
                    .ConNombre(nombreTienda)
                    .ConTipo(new TipoBuilderTest().ConId(idTipo).Build())
                    .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build();

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _tiendaUseCase.CrearTienda(nuevaTienda));

            _mockRepository.VerifyAll();

            _mockTipoRepository.Verify(repository => repository.ObtenerTipoPorIdAsync(It.IsAny<int>()), Times.Never);

            Assert.Equal((int)TipoExcepcionNegocio.TipoInvalido, exception.code);
        }

        [Fact]
        public async Task CrearTiendas_Con_Tienda_Nula_Retorna_Excepcion()
        {
            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _tiendaUseCase.CrearTienda(null));

            _mockRepository.VerifyAll();

            _mockTipoRepository.Verify(repository => repository.ObtenerTipoPorIdAsync(It.IsAny<int>()), Times.Never);

            Assert.Equal((int)TipoExcepcionNegocio.TiendaInvalida, exception.code);
        }

        #region Private Methods

        private List<Tienda> ObtenerTiendasTest() => new()
            {
                new TiendaBuilderTest()
                    .ConId("1234")
                    .ConNombre("Tienda 1234")
                    .ConTipo(new TipoBuilderTest().ConId(1).ConNombre("Físico").Build())
                    .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build(),
                new TiendaBuilderTest()
                    .ConId(id: "1111")
                    .ConNombre(nombre: "Tienda 1111")
                    .ConTipo(tipo: new TipoBuilderTest().ConId(id: 2).ConNombre(nombre: "Virtual").Build())
                    .ConUbicacion(ubicacion: new UbicacionBuilderTest().Build())
                .Build()
            };

        private Tienda ObtenerTiendaTest(string nombreTienda, int idTipo, string nombreTipo) => new TiendaBuilderTest()
                    .ConId("1234")
                    .ConNombre(nombreTienda)
                    .ConTipo(new TipoBuilderTest().ConId(idTipo).ConNombre(nombreTipo).Build())
                    .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build();

        #endregion Private Methods
    }
}