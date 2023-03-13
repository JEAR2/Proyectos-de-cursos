using System;
using System.Threading.Tasks;

using credinet.exception.middleware.models;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;

using Helpers.Commons.Exceptions;

namespace Domain.UseCases.Clientes;

public class ObtenerClientePorDocumentoDeIdentidadUseCase : IObtenerClientePorDocumentoDeIdentidadUseCase
{
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;

    public ObtenerClientePorDocumentoDeIdentidadUseCase(IRepositorioDeClientesGateway repositorioDeClientes)
    {
        _repositorioDeClientes = repositorioDeClientes;
    }

    public async Task<Cliente> Obtener(string documentoDeIdentidad)
    {
        Cliente cliente = await _repositorioDeClientes.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad);
        if (cliente == null)
            throw new BusinessException(
                "El Documento de Identidad ingresado no pertenece a ningún Cliente registrado.",
                Convert.ToInt32(TipoExcepcionNegocio.ClienteNoExiste)
            );

        return cliente;
    }
}