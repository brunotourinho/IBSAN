using Dapper;
using IMPORTADOR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper.Contrib.Extensions;

namespace IMPORTADOR.Repositories
{
    class DapperRepository
    {
        private readonly string _connectionString;
        internal IDbConnection Connection => new MySql.Data.MySqlClient.MySqlConnection(_connectionString);
        public int foo;

        private List<IBSANBR_IND_AG> retList;

        public DapperRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Default"].ToString();
        }

        public async Task INSERT_IBSANBR_IND_AG(List<IBSANBR_IND_AG> lista)
        {            
            try
            {
                retList = lista;

                //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2,0,0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for(var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_IND_AG>(lista[i]);
                        retList.Remove(lista[i]);
                    }
                    //scope.Complete();
                }
            }
            catch (Exception ex)
            {
                await INSERT_IBSANBR_IND_AG(retList);
                //throw ex;
            }
        }
    }
}
