using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests;
using Domain.UseCase.Creditos;
using Helpers.Commons.Exceptions;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class CreditoUseCaseTest
    {
        private readonly Mock<ICreditoRepository> _mockCreditoRepository;
        private readonly CreditoUseCase _creditoUseCase;
        // private readonly Mock<IOptions<ConfiguradorAppSettings>> _mockConfiguration;
        // private readonly decimal _interes;

        public CreditoUseCaseTest()
        {
            _mockCreditoRepository = new();
            _creditoUseCase = new(_mockCreditoRepository.Object);
            // _mockConfiguration = new();
            // _interes = ((decimal)_mockConfiguration.Object.Value.Interes);
        }

        [Theory]
        [InlineData("compra celular", 350000, 8, 1.5)]
        [InlineData("compra computador", 2350000, 6, 1.5)]
        public async Task Credito_Use_Case_Crear_Credito_Retorna_Credito_Creado(string concepto, decimal monto, int cuotas, decimal interes)
        {
            Credito credito = new CreditoBuilderTest()
                .ConConcepto(concepto)
                .ConMonto(monto)
                .ConCuotas(cuotas)
                .ConInteres(interes)
                .Build();
            _mockCreditoRepository.Setup(repository => repository.CrearCredito(It.IsAny<Credito>()))
                .ReturnsAsync(ObtenerCreditoTest(concepto, monto, cuotas, interes));

            Credito creditoCreado = await _creditoUseCase.CrearCredito(credito);

            _mockCreditoRepository.Verify(repository => repository.CrearCredito(It.IsAny<Credito>()), Times.Once());
            Assert.NotNull(creditoCreado);
            Assert.NotNull(creditoCreado.Id);
            Assert.Equal(concepto, creditoCreado.Concepto);
        }

        [Fact]
        public async Task Credito_Use_Case_Crear_Credito_Retorna_Excepcion()
        {
            BusinessException excepcion = await Assert.ThrowsAsync<BusinessException>(async () => await _creditoUseCase.CrearCredito(null));

            _mockCreditoRepository.Verify(repository => repository.CrearCredito(It.IsAny<Credito>()), Times.Never());

            Assert.Equal((int)TipoExcepcionNegocio.CreditoNoValido, excepcion.code);
        }

        [Fact]
        public async Task Credito_Use_Case_Obtener_Credito_Por_Id_Retorna_Credito_Encontrado()
        {
            string concepto = "compra celular";
            decimal monto = 350000;
            int cuotas = 8;
            decimal interes = (decimal)1.5;
            _mockCreditoRepository.Setup(repository => repository.ObtenerCreditoPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCreditoTest(concepto, monto, cuotas, interes));
            Credito creditoEncontrado = await _creditoUseCase.ObtenerCreditoPorId(It.IsAny<string>());

            _mockCreditoRepository.Verify(repository => repository.ObtenerCreditoPorId(It.IsAny<string>()), Times.Once());

            Assert.NotNull(creditoEncontrado);
            Assert.Equal(concepto, creditoEncontrado.Concepto);
        }

        [Fact]
        public async Task Credito_Use_Case_Actualizar_Credito_Retorna_Credito_Actualizado()
        {
            string idCredito = "1";
            Credito credito = new CreditoBuilderTest()
                .ConId(idCredito)
                .ConConcepto("concepto")
                .ConMonto(25000)
                .ConCuotas(4)
                .ConInteres((decimal)1.5)
                .Build();
            _mockCreditoRepository.Setup(repository => repository.ActualizarCredito(It.IsAny<string>(), It.IsAny<Credito>()))
                .ReturnsAsync(credito);

            Credito creditoActualizado = await _creditoUseCase.ActualizarCredito(idCredito, credito);

            _mockCreditoRepository.Verify(repository => repository.ActualizarCredito(It.IsAny<string>(), It.IsAny<Credito>()), Times.Once);
            Assert.NotNull(creditoActualizado);
            Assert.Equal(idCredito, creditoActualizado.Id);
        }

        private Credito ObtenerCreditoTest(string concepto, decimal monto, int cuotas, decimal interes)
        {
            return new CreditoBuilderTest()
                .ConId("1")
                .ConConcepto(concepto)
                .ConMonto(monto)
                .ConCuotas(cuotas)
                .ConInteres(interes)
                .Build();
        }
    }
}