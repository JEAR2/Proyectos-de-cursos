using System.Threading.Tasks;

using Domain.Model.Entities;

namespace Domain.Model.Interfaces.UseCases;

public interface IObtenerClientePorDocumentoDeIdentidadUseCase
{
    public Task<Cliente> Obtener(string documentoDeIdentidad);
}
