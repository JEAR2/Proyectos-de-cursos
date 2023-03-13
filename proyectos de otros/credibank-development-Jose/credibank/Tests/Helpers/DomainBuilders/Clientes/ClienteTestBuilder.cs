using Domain.Model.Entities;

namespace Helpers.Domain.Clientes;

public class ClienteTestBuilder
{
    private string Id { get; set; }
    private string Nombre { get; set; }
    private string Apellido { get; set; }
    private string? CorreoElectronico { get; set; }
    private string CedulaDeCiudadania { get; set; }
    private string NumeroDeCelular { get; set; }
    private IList<Credito> Creditos { get; set; }

    public static ClienteTestBuilder Builder() => new();

    public ClienteTestBuilder ConId(string id)
    {
        Id = id;
        return this;
    }

    public ClienteTestBuilder ConNombre(string nombre)
    {
        Nombre = nombre;
        return this;
    }

    public ClienteTestBuilder ConApellido(string apellido)
    {
        Apellido = apellido;
        return this;
    }

    public ClienteTestBuilder ConCorreoElectronico(string correoElectronico)
    {
        CorreoElectronico = correoElectronico;
        return this;
    }

    public ClienteTestBuilder ConCedulaDeCiudadania(string cedulaDeCiudadania)
    {
        CedulaDeCiudadania = cedulaDeCiudadania;
        return this;
    }

    public ClienteTestBuilder ConNumeroDeCelular(string numeroDeCelular)
    {
        NumeroDeCelular = numeroDeCelular;
        return this;
    }

    public ClienteTestBuilder ConCreditos(IList<Credito> creditos)
    {
        Creditos = creditos;
        return this;
    }

    public Cliente Build() => new()
    {
        Id = Id,
        Nombre = Nombre,
        Apellido = Apellido,
        DocumentoDeIdentidad = CedulaDeCiudadania,
        CorreoElectronico = CorreoElectronico,
        NumeroDeCelular = NumeroDeCelular,
        Creditos = Creditos
    };
}