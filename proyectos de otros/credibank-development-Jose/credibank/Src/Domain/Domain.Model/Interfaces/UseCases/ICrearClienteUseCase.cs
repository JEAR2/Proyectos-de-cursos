using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.UseCases;

public interface ICrearClienteUseCase
{
    public Task<Cliente> Crear(Cliente cliente);
}