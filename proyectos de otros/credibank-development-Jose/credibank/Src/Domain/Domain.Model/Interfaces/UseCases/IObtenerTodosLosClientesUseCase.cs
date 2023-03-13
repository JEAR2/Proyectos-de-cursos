using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.UseCases;

public interface IObtenerTodosLosClientesUseCase
{
    public Task<IEnumerable<Cliente>> ObtenerTodos();
}