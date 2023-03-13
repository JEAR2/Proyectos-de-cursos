using DrivenAdapters.Mongo;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class ContextTest
    {
        private readonly Mock<IMongoDatabase> _mockDB;
        private readonly Mock<IMongoClient> _mockClient;

        private readonly string _connectionString = "mongodb://Conexion_mongo_test";
        private readonly string _databaseName = "TestDB";

        private readonly Context _context;

        public ContextTest()
        {
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();

            _mockClient.Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(_mockDB.Object);

            _context = new Context(_connectionString, _databaseName);
        }

        [Fact]
        public void Obtener_Coleccion_Tiendas_Exitosamente()
        {
            var tiendasCollection = _context.Tiendas;

            Assert.NotNull(tiendasCollection);
        }

        [Fact]
        public void Obtener_Coleccion_Tipos_Exitosamente()
        {
            var tiposCollection = _context.Tipos;

            Assert.NotNull(tiposCollection);
        }
    }
}