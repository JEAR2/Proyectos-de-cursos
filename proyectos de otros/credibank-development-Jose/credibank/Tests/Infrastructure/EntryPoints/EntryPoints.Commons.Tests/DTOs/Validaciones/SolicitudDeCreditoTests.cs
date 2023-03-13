using EntryPoints.Commons.DTOs.Comandos;

using FluentAssertions;

using FluentValidation;

using Helpers.DtoBuilders.Comandos;

using Xunit;

namespace EntryPoints.Commons.DTOs.Validaciones.Tests;

public class SolicitudDeCreditoTests
{
    private readonly SolicitudDeCredito _validaciones;

    public SolicitudDeCreditoTests()
    {
        _validaciones = new();
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando la Fecha De Solicitud no es válida")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoLaFechaDeSolicitudEsInvalida()
    {
        // Arrange
        SolicitarCredito dto = new SolicitarCreditoDTOTestBuilder()
            .ConMonto(10000)
            .ConPlazoEnMeses(1)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Monto no es válido")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElMontoNoEsValido()
    {
        // Arrange
        SolicitarCredito dto = new SolicitarCreditoDTOTestBuilder()
            .ConPlazoEnMeses(1)
            .ConFechaDeSolicitud(DateTime.Now)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Plazo En Meses no es válido.")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElPlazoEnMesesNoEsValido()
    {
        // Arrange
        SolicitarCredito dto = new SolicitarCreditoDTOTestBuilder()
            .ConFechaDeSolicitud(DateTime.Now)
            .ConMonto(1)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow no debería lanzar ninguna excepción cuando el DTO es válido.")]
    public void ValidateAndThrow_NoDeberiaLanzarNingunaExcepcion_CuandoLaFechaDeSolicitudEsValida()
    {
        // Arrange
        SolicitarCredito dto = new SolicitarCreditoDTOTestBuilder()
            .ConFechaDeSolicitud(DateTime.Now)
            .ConMonto(10000)
            .ConPlazoEnMeses(1)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .NotThrow<ValidationException>();
    }
}