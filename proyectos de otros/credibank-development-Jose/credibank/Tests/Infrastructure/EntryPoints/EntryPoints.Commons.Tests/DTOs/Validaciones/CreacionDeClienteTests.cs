using FluentValidation;
using FluentAssertions;

using Helpers.DtoBuilders.Comandos;

using Xunit;
using EntryPoints.Commons.DTOs.Comandos;

namespace EntryPoints.Commons.DTOs.Validaciones.Tests;

public class CreacionDeClienteTests
{
    private readonly CreacionDeCliente _validaciones;

    public CreacionDeClienteTests()
    {
        _validaciones = new()
        {
            ClassLevelCascadeMode = CascadeMode.Continue
        };
    }

    [Theory(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el campo Nombre es invalido.")]
    [InlineData("", 2)]
    [InlineData("J", 1)]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElNombreEsInvalido(string nombre, int expectedErrorsCount)
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre(nombre)
            .ConApellido("Hernandez")
            .ConCorreoElectronico("user@gmail.com")
            .ConDocumentoDeIdentidad("2492348230")
            .ConNumeroDeCelular("2034820495")
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(expectedErrorsCount);
    }

    [Theory(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Apellido es invalido.")]
    [InlineData("", 2)]
    [InlineData("J", 1)]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElApellidoEsInvalido(string apellido, int expectedErrorsCount)
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre("Pepito")
            .ConApellido(apellido)
            .ConCorreoElectronico("user@gmail.com")
            .ConDocumentoDeIdentidad("2492348230")
            .ConNumeroDeCelular("2034820495")
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(expectedErrorsCount);
    }

    [Theory(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Correo Electrónico es invalido.")]
    [InlineData("safsafsadf", 1)]
    [InlineData("fsafsdaf", 1)]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElCorreoElectronicoEsInvalido(string correoElectronico, int expectedErrorsCount)
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre("Pepito")
            .ConApellido("Hernandez")
            .ConCorreoElectronico(correoElectronico)
            .ConDocumentoDeIdentidad("2492348230")
            .ConNumeroDeCelular("2034820495")
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(expectedErrorsCount);
    }

    [Theory(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Número De Celular es invalido.")]
    [InlineData("123456789", 1)]
    [InlineData("12345678910", 1)]
    [InlineData("123456d891", 1)]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElNumeroDeCelularEsInvalido(string numeroDeCelular, int expectedErrorsCount)
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre("Pepito")
            .ConApellido("Hernandez")
            .ConCorreoElectronico("pepe@gmail.com")
            .ConDocumentoDeIdentidad("2492348230")
            .ConNumeroDeCelular(numeroDeCelular)
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(expectedErrorsCount);
    }

    [Fact(DisplayName = "#ValidateAndThrow debería lanzar una excepción cuando el Documento De Identidad es invalido.")]
    public void ValidateAndThrow_DeberiaLanzarUnaExcepcion_CuandoElDocumentoDeIdentidadEsInvalido()
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre("Pepito")
            .ConApellido("Hernandez")
            .ConCorreoElectronico("pepe@gmail.com")
            .ConDocumentoDeIdentidad("")
            .ConNumeroDeCelular("2342434334")
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .Throw<ValidationException>()
            .And.Errors.Should().HaveCount(1);
    }

    [Fact(DisplayName = "#ValidateAndThrow no debería lanzar ninguna excepción cuando el DTO es válido.")]
    public void ValidateAndThrow_NoDeberiaLanzarNingunaExcepcion_CuandoElDocumentoDeIdentidadEsValido()
    {
        // Arrange
        CrearCliente dto = new CrearClienteDTOTestBuilder()
            .ConNombre("Pepito")
            .ConApellido("Hernandez")
            .ConCorreoElectronico("pepe@hotmail.com")
            .ConDocumentoDeIdentidad("2492348230")
            .ConNumeroDeCelular("2342342344")
            .Build();

        // Act & Assert
        _validaciones
            .Invoking(x => x.ValidateAndThrow(dto))
            .Should()
            .NotThrow<ValidationException>();
    }
}