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
        private readonly string _databaseName = "dbTest";

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
        public void Obtener_Coleccion_Clientes_Exitosamente()
        {
            var clientesCollection = _context.Clientes;
            Assert.NotNull(clientesCollection);
        }

        [Fact]
        public void Obtener_Coleccion_Creditos_Exitosamente()
        {
            var creditosCollection = _context.Creditos;
            Assert.NotNull(creditosCollection);
        }
    }
}