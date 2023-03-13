using EntryPoints.Commons.DTOs.Comandos;

using FluentValidation;

namespace EntryPoints.Commons.DTOs.Validaciones;

public class PagoDeCuota : AbstractValidator<PagarCuota>
{
    public PagoDeCuota()
    {
        RuleFor(c => c.CuotasACancelar)
            .NotEmpty()
            .WithMessage("Debes especificar la cantidad de cuotas que deseas cancelar");
        RuleFor(c => c.FechaDeCancelacion)
            .NotEmpty()
            .WithMessage("Debes especificar un valor para la fecha de cancelación.");
    }
}