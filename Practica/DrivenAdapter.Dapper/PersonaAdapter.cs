using Dapper;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using System.Data;

namespace DrivenAdapter.Dapper
{
    public class PersonaAdapter : IPersonaRepository
    {
        private readonly IDbConnection _conecction;

        public PersonaAdapter(IConnection connection)
        {
            _conecction = connection.CreateConnection();
        }

        public List<PersonaEntity> GetAll()
        {
            using (_conecction)
            {
                var personas = _conecction.Query<PersonaEntity>("ViewDataPersons", commandType: CommandType.StoredProcedure);

                return (List<PersonaEntity>)personas;
            }
        }
    }
}