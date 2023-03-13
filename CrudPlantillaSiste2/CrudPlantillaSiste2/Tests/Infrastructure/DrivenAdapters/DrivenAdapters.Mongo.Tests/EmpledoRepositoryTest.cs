using Domain.Model.Entities;
using Domain.Model.Tests;
using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Repositories;
using DrivenAdapters.Mongo.Tests.Entities;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class EmpledoRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<EmpleadoEntity>> _mockCollectionEmpleados;
        private readonly Mock<IAsyncCursor<EmpleadoEntity>> _empleadoCursor;

        public EmpledoRepositoryTest()
        {
            _mockContext = new();
            _mockCollectionEmpleados = new();
            _empleadoCursor = new();

            _mockCollectionEmpleados.Object.InsertMany(ObtenerEmpleadosTest());

            _empleadoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _empleadoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        [Fact]
        public async Task ObtenerEmpleadosConColeccionVacia_retorna_ListaVacia()
        {
            _mockCollectionEmpleados.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<EmpleadoEntity>>(),
                It.IsAny<FindOptions<EmpleadoEntity, EmpleadoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_empleadoCursor.Object);

            _mockContext.Setup(c => c.Empleados).Returns(_mockCollectionEmpleados.Object);

            var empleadoRepository = new EmpleadoRepository(_mockContext.Object);

            var result = await empleadoRepository.ObtenerEmpleadosAsync();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("john", 1, "depa1")]
        [InlineData("Edward", 2, "depa2")]
        public async Task CrearEmpleado_Exitoso(string nombreEmpleado, int idDepartamento, string nombreDepartamento)
        {
            _mockCollectionEmpleados.Setup(op => op.InsertOneAsync(It.IsAny<EmpleadoEntity>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Empleados).Returns(_mockCollectionEmpleados.Object);

            var empleadoRepository = new EmpleadoRepository(_mockContext.Object);

            Empleado result = await empleadoRepository.InsertarEmpleadoAsync(ObtenerEmpleadoTest(nombreEmpleado, idDepartamento, nombreDepartamento));

            Assert.NotNull(result);
            Assert.Equal(nombreEmpleado, result.Nombre);
            Assert.IsType<Empleado>(result);
        }

        [Fact]
        public async Task CrearEmpleadoSinDepartamento()
        {
            string nombreEmpleado = "John Edward";

            _mockCollectionEmpleados.Setup(op => op.InsertOneAsync(
                    It.IsAny<EmpleadoEntity>(),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.Empleados).Returns(_mockCollectionEmpleados.Object);

            var empleadoRepository = new EmpleadoRepository(_mockContext.Object);

            Empleado result = await empleadoRepository.InsertarEmpleadoAsync(new EmpleadoBuilderTest()
                .ConNombre(nombreEmpleado)
                .Build());

            Assert.NotNull(result);
            Assert.Equal(nombreEmpleado, result.Nombre);
            Assert.IsType<Empleado>(result);
        }

        [Fact]
        public async Task EliminarEmpleadoConExito()
        {
            string idEmpleado = "1";
            //_mockCollectionEmpleados.Setup(op => op.FindOneAndDeleteAsync(It.IsAny<FilterDefinition<EmpleadoEntity>>(), It.IsAny<FindOneAndDeleteOptions<EmpleadoEntity, EmpleadoEntity>>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.Empleados).Returns(_mockCollectionEmpleados.Object);
            EmpleadoRepository empleadoRepository = new EmpleadoRepository(_mockContext.Object);

            var result = await empleadoRepository.EliminarEmpleado(idEmpleado);
            Assert.True(result);
        }

        private List<EmpleadoEntity> ObtenerEmpleadosTest() => new()
        {
            new EmpleadoEntityBuilderTest()
            .ConId(id:"1")
            .ConNombre(nombre:"John")
            .ConDepartamento(new DepartamentoEntityBuilderTest().ConId(id:1).ConNombre(nombre:"depa1").Build())
            .Build(),
            new EmpleadoEntityBuilderTest()
            .ConId(id:"2")
            .ConNombre(nombre:"Edward")
            .ConDepartamento(new DepartamentoEntityBuilderTest().ConId(id:1).ConNombre(nombre:"depa1").Build())
            .Build()
        };

        private Empleado ObtenerEmpleadoTest(string nombreEmpleado, int idDepartamento, string nombreDepartamento) => new EmpleadoBuilderTest()
            .ConId(id: "1")
            .ConNombre(nombre: nombreEmpleado)
            .ConDepartamento(new DepartamentoBuilderTest().ConId(id: idDepartamento).ConNombre(nombre: nombreDepartamento).Build())
            .Build();
    }
}