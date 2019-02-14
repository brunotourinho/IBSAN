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
    public class InformacoesRepository
    {
        private readonly string _connectionString;

        public InformacoesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        internal IDbConnection Connection => new MySqlConnection(_connectionString);


        public async Task<List<Municipio>> Listar()
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<Municipio>(@"SELECT DISTINCT Municipios.CodigoMunicipio, Municipios.Nome, Municipios.UF FROM Municipios ORDER BY Municipios.Nome");
                return result.ToList();
            }
        }

        public async Task<List<PopulacaoCobertura>> PopulacaoCobertura(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PopulacaoCobertura>(@"SET SQL_BIG_SELECTS=1; SELECT Prestadores.Competencia, Prestadores.Prestador, Municipios.GE12A, Municipios.GE06A ,Informacoes.AG001, Informacoes.ES026 FROM Prestadores 
                INNER JOIN Municipios on Prestadores.CodigoMunicipio = Municipios.CodigoMunicipio AND Prestadores.Competencia = Municipios.Competencia
                INNER JOIN Informacoes on Prestadores.CodigoMunicipio = Informacoes.CodigoMunicipio AND Prestadores.Competencia = Informacoes.Competencia
                WHERE Prestadores.CodigoMunicipio = ?CodigoMunicipio AND Prestadores.Competencia in (2010,2011,2012,2013,2014,2015,2016) ORDER BY Prestadores.Competencia", new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }            
        }
    }
}
