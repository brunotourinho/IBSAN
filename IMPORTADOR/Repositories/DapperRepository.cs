using Dapper.Contrib.Extensions;
using IMPORTADOR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace IMPORTADOR.Repositories
{
    internal class DapperRepository
    {
        private readonly string _connectionString;
        internal IDbConnection Connection => new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        public DapperRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Default"].ToString();
        }

        public async Task INSERT_IBSANBR_IND_AG(List<IBSANBR_IND_AG> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_IND_AG>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_IND_ES(List<IBSANBR_IND_ES> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_IND_ES>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_IND_FIN(List<IBSANBR_IND_FIN> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_IND_FIN>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_IND_QD(List<IBSANBR_IND_QD> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_IND_QD>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_INF_AG(List<IBSANBR_INF_AG> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_INF_AG>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_INF_ES(List<IBSANBR_INF_ES> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_INF_ES>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_INF_FN(List<IBSANBR_INF_FN> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_INF_FN>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_INF_GE(List<IBSANBR_INF_GE> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_INF_GE>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task INSERT_IBSANBR_INF_QD(List<IBSANBR_INF_QD> lista)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { Timeout = new TimeSpan(2, 0, 0) }, TransactionScopeAsyncFlowOption.Enabled))
                using (IDbConnection db = Connection)
                {
                    for (var i = 0; i < lista.Count; i++)
                    {
                        await db.InsertAsync<IBSANBR_INF_QD>(lista[i]);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}