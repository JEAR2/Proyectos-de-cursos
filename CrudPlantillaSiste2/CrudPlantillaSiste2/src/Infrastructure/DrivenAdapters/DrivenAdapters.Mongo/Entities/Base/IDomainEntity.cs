using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Entities.Base
{
    public interface IDomainEntity<out T> where T : class
    {
        /// <summary>
        /// Convierte una entidad de infraestrutura o DTO a una entodad de Dominio
        /// </summary>
        /// <returns></returns>
        T AsEntity();
    }
}