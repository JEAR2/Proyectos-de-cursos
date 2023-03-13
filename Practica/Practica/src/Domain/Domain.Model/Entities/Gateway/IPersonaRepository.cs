using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface IPersonaRepository
    {
        List<PersonaEntity> GetAll();
    }
}