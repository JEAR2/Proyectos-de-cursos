using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Gateways;

public interface IRepositorioDeClientesGateway
{
    public Task<IEnumerable<Cliente>> ObtenerTodosAsync();

    public Task<Cliente> ObtenerPorDocumentoDeIdentidadAsync(string id);

    public Task<bool> ClienteConDocumentoDeIdentidadExisteAsync(string numeroDeDocumentoDeIdentidad);

    public Task<Cliente> GuardarAsync(Cliente cliente);

    public Task<Credito> AnexarCreditoAClienteAsync(Cliente cliente, Credito credito);

    public Task<Credito> AnexarCuotaACreditoAsync(Cliente cliente, Credito credito, Pago pago);
}