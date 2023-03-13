using System.Threading.Tasks;

using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;
using Domain.Model.Entities;
using credinet.exception.middleware.models;
using Helpers.Commons.Exceptions;
using System;
using System.Collections.Generic;

namespace Domain.UseCases.Clientes;

public class CrearClienteUseCase : ICrearClienteUseCase
{
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;

    public CrearClienteUseCase(IRepositorioDeClientesGateway repositorioDeClientes)
    {
        _repositorioDeClientes = repositorioDeClientes;
    }

    public async Task<Cliente> Crear(Cliente cliente)
    {
        bool clienteConDocumentoDeIdentidadYaExiste = await _repositorioDeClientes
            .ClienteConDocumentoDeIdentidadExisteAsync(cliente.DocumentoDeIdentidad);
        if (clienteConDocumentoDeIdentidadYaExiste)
            throw new BusinessException("El Documento de Identidad dado ya está registrado a nombre de otro Cliente.", Convert.ToInt32(TipoExcepcionNegocio.DocumentoDeIdentidadYaRegistrado));

        return await CrearSafe(cliente);
    }

    private Task<Cliente> CrearSafe(Cliente cliente)
    {
        cliente.Creditos = new List<Credito>();
        return _repositorioDeClientes.GuardarAsync(cliente);
    }
}