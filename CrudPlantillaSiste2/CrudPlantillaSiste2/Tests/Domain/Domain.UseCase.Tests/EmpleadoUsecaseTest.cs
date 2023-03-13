using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests;
using Domain.UseCase.Empleados;
using Helpers.Commons.Exceptions;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class EmpleadoUsecaseTest
    {
        private readonly Mock<IEmpleadoRepository> _mockEmpleadoRepository;
        private readonly Mock<IDepartamentoRepository> _mockDepartamentoRepository;
        private readonly EmpleadoUseCase _empleadoUseCase;

        public EmpleadoUsecaseTest()
        {
            _mockEmpleadoRepository = new();
            _mockDepartamentoRepository = new();
            _empleadoUseCase = new(_mockEmpleadoRepository.Object, _mockDepartamentoRepository.Object);
        }

        [Fact]
        public async Task ObtenerEmpleadosExitoso()
        {
            _mockEmpleadoRepository.Setup(repository => repository.ObtenerEmpleadosAsync())
                .ReturnsAsync(ObtenerEmpleadosTest);

            List<Empleado> empleados = await _empleadoUseCase.ObtenerEmpleadosAsync();

            Assert.NotEmpty(empleados);
            Assert.NotNull(empleados);
        }

        [Theory]
        [InlineData("John", 1, "depa 1")]
        [InlineData("Edward", 2, "depa 2")]
        public async Task CreaEmpleado_retorna_EmpleadoCreado(string nombreEmpleado, int idDepartamento, string nombreDepartamento)
        {
            Empleado empleado = new EmpleadoBuilderTest()
                .ConId("1")
                .ConNombre(nombreEmpleado)
                .ConDepartamento(new DepartamentoBuilderTest().ConId(idDepartamento).Build())
                .Build();
            _mockEmpleadoRepository.Setup(repository => repository.InsertarEmpleadoAsync(It.IsAny<Empleado>()))
                .ReturnsAsync(ObtenreEmpleadoTest(nombreEmpleado, idDepartamento, nombreDepartamento));
            _mockDepartamentoRepository.Setup(repository => repository
                                        .ObtenerDepartamentoPorIdAsync(idDepartamento))
                                        .ReturnsAsync(new DepartamentoBuilderTest().ConId(idDepartamento).ConNombre(nombreDepartamento).Build());
            Empleado empleadoCreado = await _empleadoUseCase.InsertarEmpleadoAsync(empleado);

            _mockDepartamentoRepository.Verify(repository => repository.ObtenerDepartamentoPorIdAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(empleadoCreado);
            Assert.NotNull(empleadoCreado.Id);
            Assert.Equal("1", empleadoCreado.Id);
            Assert.Equal(nombreEmpleado, empleadoCreado.Nombre);
            Assert.Equal(nombreDepartamento, empleadoCreado.Departamento.Nombre);
        }

        [Fact]
        public async Task CreaEmpleado_ConInformacionNula_retorna_Excepcion()
        {
            BusinessException excepcion = await Assert.ThrowsAsync<BusinessException>(async () => await _empleadoUseCase.InsertarEmpleadoAsync(null));

            _mockDepartamentoRepository.Verify(repository => repository.ObtenerDepartamentoPorIdAsync(It.IsAny<int>()), Times.Never);

            Assert.Equal((int)TipoExcepcionNegocio.EmpleadoNoValido, excepcion.code);
        }

        [Theory]
        [InlineData("John", -1, "depa 1")]
        [InlineData("Edward", -2, "depa 2")]
        public async Task CreaEmpleado_ConIdDepartamentoMenorAUno_retorna_Excepcion(string nombreEmpleado, int idDepartamento, string nombreDepartamento)
        {
            Empleado empleado = new EmpleadoBuilderTest()
                .ConId("1")
                .ConNombre(nombreEmpleado)
                .ConDepartamento(new DepartamentoBuilderTest().ConId(idDepartamento).ConNombre(nombreDepartamento).Build())
                .Build();
            BusinessException excepcion = await Assert.ThrowsAsync<BusinessException>(async () => await _empleadoUseCase.InsertarEmpleadoAsync(empleado));

            _mockDepartamentoRepository.Verify(repository => repository.ObtenerDepartamentoPorIdAsync(It.IsAny<int>()), Times.Never);

            Assert.Equal((int)TipoExcepcionNegocio.DepartamentoNoValido, excepcion.code);
        }

        [Fact]
        public async Task EliminarEmpleado_return_True()
        {
            string idEmpleado = "1";
            Empleado empleado = new EmpleadoBuilderTest()
               .ConId("1")
               .ConNombre("john")
               .ConDepartamento(new DepartamentoBuilderTest().ConId(1).ConNombre("depa 1").Build())
               .Build();
            _mockEmpleadoRepository.Setup(repository => repository.EliminarEmpleado(It.IsAny<string>())).ReturnsAsync(true);

            bool empleadoEliminado = await _empleadoUseCase.EliminarEmpleado(It.IsAny<string>());

            Assert.True(empleadoEliminado);
        }

        [Fact]
        public async Task ActualizarEmpleado_return_EmpleadoActualizado()
        {
            string idEmpleado = "1";
            Empleado empleado = new EmpleadoBuilderTest()
               .ConId("1")
               .ConNombre("john")
               .Build();
            Departamento departamento = new DepartamentoBuilderTest().ConId(1).ConNombre("depa 1").Build();
            empleado.EstablecerDepartamento(new DepartamentoBuilderTest().ConId(1).ConNombre("depa 1").Build());
            _mockEmpleadoRepository.Setup(repository => repository.ActualizarEmpleado(It.IsAny<string>(), It.IsAny<Empleado>())).ReturnsAsync((Empleado)empleado);
            _mockDepartamentoRepository
                .Setup(repository => repository.ObtenerDepartamentoPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(departamento);

            Empleado empleadoActualizado = await _empleadoUseCase.ActualizarEmpleado(idEmpleado, empleado);

            _mockDepartamentoRepository.Verify(repository => repository.ObtenerDepartamentoPorIdAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(empleadoActualizado);
            Assert.Equal(idEmpleado, empleadoActualizado.Id);
            Assert.Equal(empleado.Departamento.Id, empleadoActualizado.Departamento.Id);
        }

        private List<Empleado> ObtenerEmpleadosTest() => new()
        {
            new EmpleadoBuilderTest()
            .ConId("1")
            .ConNombre("John")
            .ConApellido("Acevedo")
            .ConEdad(30)
            .ConCorreo("Email")
            .ConSexo("M")
            .ConDepartamento(departamento:
                new DepartamentoBuilderTest()
                .ConId(1).ConNombre("depa 1")
                .ConDescripcion("Descripcion")
                .Build()
                ).Build(),
            new EmpleadoBuilderTest()
            .ConId("2")
            .ConNombre("Edward")
            .ConApellido("Rojas")
            .ConEdad(30)
            .ConCorreo("Email")
            .ConSexo("M")
            .ConDepartamento(departamento:
                new DepartamentoBuilderTest()
                .ConId(2).ConNombre("depa 2")
                .ConDescripcion("Descripcion 2")
                .Build()
                ).Build()
        };

        private Empleado ObtenreEmpleadoTest(string nombreEmpleado, int idDepartamento, string nombreDepartamento)
        {
            return new EmpleadoBuilderTest()
                .ConId("1")
                .ConNombre(nombreEmpleado)
                .ConDepartamento(new DepartamentoBuilderTest().ConId(idDepartamento).ConNombre(nombreDepartamento).Build())
                .Build();
        }
    }
}