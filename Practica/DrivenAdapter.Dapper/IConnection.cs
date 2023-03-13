using Domain.Model.Entities;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrivenAdapter.Dapper
{
    public interface IConnection
    {
        public IDbConnection CreateConnection();
    }
}