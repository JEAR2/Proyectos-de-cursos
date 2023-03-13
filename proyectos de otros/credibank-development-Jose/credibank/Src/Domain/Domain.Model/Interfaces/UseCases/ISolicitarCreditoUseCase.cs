using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.UseCases;

public interface ISolicitarCreditoUseCase
{
    public Task<Credito> Solicitar(string documentoDeIdentidad, Credito credito, decimal tasaDeInteresEfectivoAnual);
}