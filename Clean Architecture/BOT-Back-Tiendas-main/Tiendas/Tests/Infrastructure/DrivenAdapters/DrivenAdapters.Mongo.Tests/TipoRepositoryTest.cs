using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Tests.Entities;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class TipoRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<TipoEntity>> _mockCollectionTipos;
        private readonly Mock<IAsyncCursor<TipoEntity>> _tipoCursor;

        public TipoRepositoryTest()
        {
            _mockContext = new();
            _mockCollectionTipos = new();
            _tipoCursor = new();

            _mockCollectionTipos.Object.InsertOne(ObtenerTipo());

            _tipoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _tipoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        [Fact]
        public async Task ObtenerTipoPorId_Exitoso()
        {
            int idTipo = 1;

            List<TipoEntity> listTipos = new() { ObtenerTipo() };

            _tipoCursor.Setup(item => item.Current).Returns(listTipos);

            _mockCollectionTipos.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<TipoEntity>>(),
                It.IsAny<FindOptions<TipoEntity, TipoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tipoCursor.Object);

            _mockContext.Setup(c => c.Tipos).Returns(_mockCollectionTipos.Object);

            var tipoRepository = new TipoRepository(_mockContext.Object);

            var result = await tipoRepository.ObtenerTipoPorIdAsync(idTipo);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObtenerTipoPorId_Con_Id_Imvalido_Retorna_Nulo()
        {
            int idTipo = 0;

            _mockCollectionTipos.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<TipoEntity>>(),
                It.IsAny<FindOptions<TipoEntity, TipoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tipoCursor.Object);

            _mockContext.Setup(c => c.Tipos).Returns(_mockCollectionTipos.Object);

            var tipoRepository = new TipoRepository(_mockContext.Object);

            var result = await tipoRepository.ObtenerTipoPorIdAsync(idTipo);

            Assert.Null(result);
        }

        #region Private Methods

        private static TipoEntity ObtenerTipo() => new TipoEntityBuilderTest()
            .ConId(id: 1)
            .ConNombre(nombre: "Tipo 1")
            .Build();

        #endregion Private Methods
    }
}