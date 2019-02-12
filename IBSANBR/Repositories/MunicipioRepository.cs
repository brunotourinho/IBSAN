using Dapper;
using IBSANBR.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Repositories
{
    public class MunicipioRepository
    {
        private readonly string _connectionString;

        public MunicipioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        internal IDbConnection Connection => new MySqlConnection(_connectionString);


        public async Task<List<Municipio>> Listar()
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<Municipio>(@"SELECT * FROM Municipios");
                return result.ToList();
            }
        }

        public async Task<List<PopulacaoCobertura>> PopulacaoCobertura()
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PopulacaoCobertura>(@"SET SQL_BIG_SELECTS=1; SELECT DISTINCT Municipios.Codigo, Municipios.Nome, Municipios.Populacao, IOAgua.IN001 FROM Municipios LEFT JOIN IOAgua on Municipios.Codigo = IOAgua.CodigoMunicipio");
                return result.ToList();
            }            
        }
    }
}
