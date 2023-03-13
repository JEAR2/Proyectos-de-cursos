using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.UseCases;

public interface IPagarCuotasUseCase
{
    public Task<Credito> Pagar(string idDelCliente, string idDelCredito, Pago pago);
}