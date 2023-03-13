using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Repositories;
using DrivenAdapters.Mongo.Tests.Entities;
using MongoDB.Driver;
using Moq;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class DepartamentoRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<DepartamentoEntity>> _mockCollectionDepartamentos;
        private readonly Mock<IAsyncCursor<DepartamentoEntity>> _departamentoCursor;

        public DepartamentoRepositoryTest()
        {
            _mockContext = new();
            _mockCollectionDepartamentos = new();
            _departamentoCursor = new();

            _mockCollectionDepartamentos.Object.InsertOne(ObtenerDepartamento());

            _departamentoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _departamentoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        [Fact]
        public async Task ObtenerDepartamnetoPorId_Exitoso()
        {
            int idDepartamento = 1;

            List<DepartamentoEntity> listaDepartamentos = new() { ObtenerDepartamento() };

            _departamentoCursor.Setup(item => item.Current).Returns(listaDepartamentos);

            _mockCollectionDepartamentos.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<DepartamentoEntity>>(),
                It.IsAny<FindOptions<DepartamentoEntity, DepartamentoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_departamentoCursor.Object);

            _mockContext.Setup(c => c.Departamentos).Returns(_mockCollectionDepartamentos.Object);

            var departamentoRepository = new DepartamentoRepository(_mockContext.Object);

            var result = await departamentoRepository.ObtenerDepartamentoPorIdAsync(idDepartamento);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ObtenerDepartamentoPorIdNoValido_Retorna_Nulo()
        {
            int idDepartamento = 0;

            _mockCollectionDepartamentos.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<DepartamentoEntity>>(),
                It.IsAny<FindOptions<DepartamentoEntity, DepartamentoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_departamentoCursor.Object);

            _mockContext.Setup(c => c.Departamentos).Returns(_mockCollectionDepartamentos.Object);

            var departamentoRepository = new DepartamentoRepository(_mockContext.Object);

            var result = await departamentoRepository.ObtenerDepartamentoPorIdAsync(idDepartamento);

            Assert.Null(result);
        }

        private static DepartamentoEntity ObtenerDepartamento() => new DepartamentoEntityBuilderTest()
            .ConId(id: 1)
            .ConNombre(nombre: "Depa 1")
            .ConDescripcion(descripcion: "decri")
            .Build();
    }
}