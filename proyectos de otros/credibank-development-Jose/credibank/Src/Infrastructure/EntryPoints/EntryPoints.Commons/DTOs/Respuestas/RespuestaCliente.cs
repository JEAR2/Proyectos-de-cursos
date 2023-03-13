using System.Collections.Generic;

namespace EntryPoints.Commons.DTOs.Respuestas;

public class RespuestaCliente
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string? CorreoElectronico { get; set; }
    public string DocumentoDeIdentidad { get; set; }
    public string NumeroDeCelular { get; set; }
    public IList<RespuestaCredito> Creditos { get; set; }
}