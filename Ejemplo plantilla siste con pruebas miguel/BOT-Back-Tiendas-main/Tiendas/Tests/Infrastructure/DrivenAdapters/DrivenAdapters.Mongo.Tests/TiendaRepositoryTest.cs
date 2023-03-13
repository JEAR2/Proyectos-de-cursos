using Domain.Model.Entities;
using Domain.Model.Tests;
using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Tests.Entities;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class TiendaRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<TiendaEntity>> _mockCollectionTienda;
        private readonly Mock<IAsyncCursor<TiendaEntity>> _tipoCursor;

        public TiendaRepositoryTest()
        {
            _mockContext = new();
            _mockCollectionTienda = new();
            _tipoCursor = new();

            _mockCollectionTienda.Object.InsertMany(ObtenerTiendasTest());

            _tipoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _tipoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        [Fact]
        public async Task ObtenerTipoPorId_Exitoso()
        {
            _tipoCursor.Setup(item => item.Current).Returns(ObtenerTiendasTest);

            _mockCollectionTienda.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<TiendaEntity>>(),
                It.IsAny<FindOptions<TiendaEntity, TiendaEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tipoCursor.Object);

            _mockContext.Setup(c => c.Tiendas).Returns(_mockCollectionTienda.Object);

            var tiendaRepository = new TiendaRepository(_mockContext.Object);

            var result = await tiendaRepository.ObtenerTiendasAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ObtenerTiendas_Con_Coleccion_Vacia_Retorna_Lista_Vacia()
        {
            _mockCollectionTienda.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<TiendaEntity>>(),
                It.IsAny<FindOptions<TiendaEntity, TiendaEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tipoCursor.Object);

            _mockContext.Setup(c => c.Tiendas).Returns(_mockCollectionTienda.Object);

            var tiendaRepository = new TiendaRepository(_mockContext.Object);

            var result = await tiendaRepository.ObtenerTiendasAsync();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("Tienda 1234", 1, "Físico")]
        [InlineData("Tienda 1234", 2, "Virtual")]
        public async Task CrearTienda_Exitoso(string nombreTienda, int idTipo, string nombreTipo)
        {
            _mockCollectionTienda.Setup(op => op.InsertOneAsync(
                 It.IsAny<TiendaEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.Tiendas).Returns(_mockCollectionTienda.Object);

            var tiendaRepository = new TiendaRepository(_mockContext.Object);

            Tienda result = await tiendaRepository.InsertarTiendaAsync(ObtenerTiendaTest(nombreTienda, idTipo, nombreTipo));

            Assert.NotNull(result);
            Assert.Equal(nombreTienda, result.Nombre);
            Assert.IsType<Tienda>(result);
        }

        [Fact]
        public async Task CrearTienda_SinTipo()
        {
            string nombreTienda = "Tienda 11";

            _mockCollectionTienda.Setup(op => op.InsertOneAsync(
                    It.IsAny<TiendaEntity>(),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.Tiendas).Returns(_mockCollectionTienda.Object);

            var tiendaRepository = new TiendaRepository(_mockContext.Object);

            Tienda result = await tiendaRepository.InsertarTiendaAsync(new TiendaBuilderTest()
                .ConNombre(nombreTienda)
                .ConUbicacion(new UbicacionBuilderTest().Build())
                .Build());

            Assert.NotNull(result);
            Assert.Equal(nombreTienda, result.Nombre);
            Assert.IsType<Tienda>(result);
        }

        #region Private Methods

        private Tienda ObtenerTiendaTest(string nombreTienda, int idTipo, string nombreTipo) => new TiendaBuilderTest()
            .ConNombre(nombreTienda)
            .ConTipo(new TipoBuilderTest().ConId(idTipo).ConNombre(nombreTipo).Build())
            .ConUbicacion(new UbicacionBuilderTest().Build())
        .Build();

        private List<TiendaEntity> ObtenerTiendasTest() => new()
            {
                new TiendaEntityBuilderTest()
                    .ConId("1234")
                    .ConNombre("Tienda 1234")
                    .ConTipo(new TipoEntityBuilderTest().ConId(1).ConNombre("Físico").Build())
                    .ConUbicacion(new UbicacionEntity(0,0))
                .Build(),
                new TiendaEntityBuilderTest()
                    .ConId(id: "1111")
                    .ConNombre(nombre: "Tienda 1111")
                    .ConTipo(tipo: new TipoEntityBuilderTest().ConId(id: 2).ConNombre(nombre: "Virtual").Build())
                    .ConUbicacion(ubicacion: new UbicacionEntity(0,0))
                .Build()
            };

        #endregion Private Methods
    }
}