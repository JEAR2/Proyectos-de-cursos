using EntryPoints.Commons.DTOs.Comandos;

using FluentAssertions;

using FluentValidation;

using Helpers.DtoBuilders.Comandos;

using Xunit;

namespace EntryPoints.Commons.DTOs.Validaciones.Tests;

public class PagoDeCuotaTests
{
    private readonly PagoDeCuota _validaciones;

    public PagoDeCuotaTests()
    {
        _validaciones = new();
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando la Fecha De Cancelación es inválida.")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoLaFechaDeCancelacionEsInvalida()
    {
        // Arrange
        PagarCuota dto = new PagarCuotaDTOTestBuilder()
            .ConCuotasACancelar(1)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando la cantidad de Cuotas A Cancelar es inválida.")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoLaCantidadDeCuotasACancelarEsInvalida()
    {
        // Arrange
        PagarCuota dto = new PagarCuotaDTOTestBuilder()
            .ConFechaDeCancelacion(DateTime.Now)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow no debería lanzar ninguna excepción cuando el DTO es válido.")]
    public void ValidateAndThrow_NoDeberiaLanzarNingunaExcepcion_CuandoLaCantidadDeCuotasACancelarEsValida()
    {
        // Arrange
        PagarCuota dto = new PagarCuotaDTOTestBuilder()
            .ConFechaDeCancelacion(DateTime.Now)
            .ConCuotasACancelar(1)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .NotThrow<ValidationException>();
    }
}