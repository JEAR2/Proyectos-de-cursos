using EntryPoints.Commons.DTOs.Comandos;

using FluentValidation;

namespace EntryPoints.Commons.DTOs.Validaciones;

public class SolicitudDeCredito : AbstractValidator<SolicitarCredito>
{
    public SolicitudDeCredito()
    {
        RuleFor(c => c.Monto)
            .NotEmpty()
            .WithMessage("Debes especificar un valor para el Monto que deseas solicitar.");
        RuleFor(c => c.PlazoEnMeses)
            .NotEmpty()
            .WithMessage("Debes especificar un valor para el plazo del préstamo.");
        RuleFor(c => c.FechaDeSolicitud)
            .NotEmpty()
            .WithMessage("Debes especificar un valor para la fecha de solicitud.");
    }
}