using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using credinet.exception.middleware.models;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;

using Helpers.Commons.Exceptions;

namespace Domain.UseCases.Clientes;

public class SolicitarCreditoUseCase : ISolicitarCreditoUseCase
{
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;

    private const int _plazoMinimoParaCancelar = 1;
    private const decimal _montoMinimo = 100000.0M;

    public SolicitarCreditoUseCase(IRepositorioDeClientesGateway repositorioDeClientes)
    {
        _repositorioDeClientes = repositorioDeClientes;
    }

    public async Task<Credito> Solicitar(string documentoDeIdentidad, Credito credito, decimal tasaDeInteresEfectivoAnual)
    {
        Cliente cliente = await _repositorioDeClientes.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad);
        if (cliente == null)
            throw new BusinessException("El cliente con el id ingresado no está registrado en el sistema.", Convert.ToInt32(TipoExcepcionNegocio.ClienteNoExiste));
        if (credito.Monto < _montoMinimo)
            throw new BusinessException("El monto ingresado para solicitar el crédito no es válido.", Convert.ToInt32(TipoExcepcionNegocio.MontoDeCreditoNoValido));
        if (credito.PlazoEnMeses < _plazoMinimoParaCancelar)
            throw new BusinessException("El plazo ingresado para cancelar el crédito no es válido.", Convert.ToInt32(TipoExcepcionNegocio.PlazoDeCancelacionNoValido));

        return await SolicitarSafe(cliente, credito, tasaDeInteresEfectivoAnual);
    }

    private Task<Credito> SolicitarSafe(Cliente cliente, Credito credito, decimal tasaDeInteresEfectivoAnual)
    {
        cliente.SolicitarCredito(credito, tasaDeInteresEfectivoAnual);
        credito.CambiarEstado(EstadosDeCredito.EN_VIGENCIA);

        return _repositorioDeClientes.AnexarCreditoAClienteAsync(cliente, credito);
    }
}