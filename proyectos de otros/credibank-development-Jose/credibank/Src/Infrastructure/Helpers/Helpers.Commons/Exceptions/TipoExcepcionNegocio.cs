using System.ComponentModel;

namespace Helpers.Commons.Exceptions;

public enum TipoExcepcionNegocio
{
    [Description("Excepción de negocio no controlada")]
    ExceptionNoControlada = 555,

    [Description("Excepción de negocio: el Documento de Identidad ingresado ya está registrado a nombre de otro Cliente.")]
    DocumentoDeIdentidadYaRegistrado = 556,

    [Description("Excepción de negocio: el Documento de Identidad ingresado no pertenece a ningún Cliente registrado.")]
    ClienteNoExiste = 557,

    [Description("Excepción de negocio: el Monto ingresado para solicitar el crédito no es válido.")]
    MontoDeCreditoNoValido = 558,

    [Description("Excepción de negocio: el plazo ingresado para cancelar el crédito no es válido.")]
    PlazoDeCancelacionNoValido = 559,

    [Description("Excepción de negocio: el cliente no cuenta con el crédito ingresado.")]
    CreditoNoExiste = 560,

    [Description("Excepción de negocio: el cliente intentó pagar más cuotas de las que debe.")]
    SeIntentoPagarMasCuotasDeLasDebidas = 561,
}