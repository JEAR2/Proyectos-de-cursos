using System;
using System.Linq;
using System.Threading.Tasks;

using credinet.exception.middleware.models;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;

using Helpers.Commons.Exceptions;

namespace Domain.UseCases.Clientes;

public class PagarCuotaUseCase : IPagarCuotasUseCase
{
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;

    public PagarCuotaUseCase(IRepositorioDeClientesGateway repositorioDeClientes)
    {
        _repositorioDeClientes = repositorioDeClientes;
    }

    public async Task<Credito> Pagar(string documentoDeIdentidad, string idDelCredito, Pago pago)
    {
        Cliente cliente = await _repositorioDeClientes.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad);
        if (cliente == null)
            throw new BusinessException("El cliente con el id ingresado no está registrado en el sistema.", Convert.ToInt32(TipoExcepcionNegocio.ClienteNoExiste));
        Credito credito = cliente.Creditos.First(c => c.Id == idDelCredito);
        if (credito == null)
            throw new BusinessException("El crédito ingresado no existe dentro del Cliente..", Convert.ToInt32(TipoExcepcionNegocio.CreditoNoExiste));
        if (pago.CuotasACancelar > credito.CuotasRestantes)
            throw new BusinessException("El cliente intentó pagar más cuotas de las que debe..", Convert.ToInt32(TipoExcepcionNegocio.SeIntentoPagarMasCuotasDeLasDebidas));

        return await PagarSafe(cliente, credito, pago);
    }

    private Task<Credito> PagarSafe(Cliente cliente, Credito credito, Pago pago)
    {
        pago.Monto = credito.MontoPorCuota * pago.CuotasACancelar; // solo para desarrollo

        cliente.PagarCuotaDeCredito(credito.Id, pago);
        return _repositorioDeClientes.AnexarCuotaACreditoAsync(cliente, credito, pago);
    }
}