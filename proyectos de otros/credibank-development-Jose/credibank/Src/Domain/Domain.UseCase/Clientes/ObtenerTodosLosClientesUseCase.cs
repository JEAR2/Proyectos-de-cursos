using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;

namespace Domain.UseCases.Clientes;

public class ObtenerTodosLosClientesUseCase : IObtenerTodosLosClientesUseCase
{
    private readonly IRepositorioDeClientesGateway _repositorioDeClientes;

    public ObtenerTodosLosClientesUseCase(IRepositorioDeClientesGateway repositorioDeClientes)
    {
        _repositorioDeClientes = repositorioDeClientes;
    }

    public Task<IEnumerable<Cliente>> ObtenerTodos()
    {
        return _repositorioDeClientes.ObtenerTodosAsync();
    }
}