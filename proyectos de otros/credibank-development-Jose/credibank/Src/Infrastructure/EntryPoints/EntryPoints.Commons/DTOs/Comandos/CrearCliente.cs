namespace EntryPoints.Commons.DTOs.Comandos;

public class CrearCliente
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string? CorreoElectronico { get; set; }
    public string DocumentoDeIdentidad { get; set; }
    public string NumeroDeCelular { get; set; }
}