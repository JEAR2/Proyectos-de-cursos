using EntryPoints.Commons.DTOs.Comandos;

using FluentValidation;

namespace EntryPoints.Commons.DTOs.Validaciones;

public class CreacionDeCliente : AbstractValidator<CrearCliente>
{
    public CreacionDeCliente()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty()
            .WithMessage("Debes ingresar un nombre para el cliente que se desea crear.")
            .MinimumLength(2)
            .WithMessage("No puedes ingresar un nombre con menos de 2 letras o caracteres.")
            .MaximumLength(50)
            .WithMessage("No puedes ingresar un nombre con más de 50 letras o caracteres.");
        RuleFor(c => c.Apellido)
            .NotEmpty()
            .WithMessage("Debes ingresar un apellido para el cliente que se desea crear.")
            .MinimumLength(2)
            .WithMessage("No puedes ingresar un apellido con menos de 2 letras o caracteres.")
            .MaximumLength(50)
            .WithMessage("No puedes ingresar un apellido con más de 50 letras o caracteres.");
        RuleFor(c => c.CorreoElectronico)
            .EmailAddress()
            .WithMessage("Debes ingresar un correo electrónico para poder crear la cuenta del cliente.");
        RuleFor(c => c.NumeroDeCelular)
            .Must(x => x.All(char.IsDigit))
            .WithMessage("Debes ingresar un número de contacto que contenga sólo números.")
            .NotEmpty()
            .WithMessage("No puedes crear un cliente sin número de contacto telefónico.")
            .MinimumLength(10)
            .WithMessage("Los número de contacto deben tener mínimo 10 dígitos.")
            .MaximumLength(10)
            .WithMessage("Los número de contacto deben tener máximo 10 dígitos.");
        RuleFor(c => c.DocumentoDeIdentidad)
            .NotEmpty()
            .WithMessage("Debes ingresar un número de documento de identidad para poder crear la cuenta.");
    }
}