using Helpers.ObjectsUtils.Setting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapter.Dapper
{
    public class Conecction : IConnection
    {
        private readonly Secrets secrets;
        private readonly string _conection;

        public Conecction()
        {
            secrets = new Secrets();
            _conection = secrets.DapperConnection;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_conection);
    }
}