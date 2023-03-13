using EntryPoints.Commons.DTOs.Comandos;

namespace Helpers.DtoBuilders.Comandos;

public class CrearClienteDTOTestBuilder
{
    private readonly CrearCliente _cliente = new();

    public CrearClienteDTOTestBuilder ConNombre(string nombre)
    {
        _cliente.Nombre = nombre;
        return this;
    }

    public CrearClienteDTOTestBuilder ConApellido(string apellido)
    {
        _cliente.Apellido = apellido;
        return this;
    }

    public CrearClienteDTOTestBuilder ConCorreoElectronico(string correoElectronico)
    {
        _cliente.CorreoElectronico = correoElectronico;
        return this;
    }

    public CrearClienteDTOTestBuilder ConDocumentoDeIdentidad(string documentoDeIdentidad)
    {
        _cliente.DocumentoDeIdentidad = documentoDeIdentidad;
        return this;
    }

    public CrearClienteDTOTestBuilder ConNumeroDeCelular(string numeroDeCelular)
    {
        _cliente.NumeroDeCelular = numeroDeCelular;
        return this;
    }

    public CrearCliente Build() => _cliente;
}