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
                var result = await db.QueryAsync<PopulacaoCobertura>(
                    @"SET SQL_BIG_SELECTS=1; SELECT Municipios.Competencia, Municipios.GE12A, SUM(Informacoes.AG001) AS AG001, SUM(Informacoes.ES001) AS ES001 FROM Municipios INNER JOIN Informacoes on Municipios.CodigoMunicipio = Informacoes.CodigoMunicipio AND Municipios.Competencia = Informacoes.Competencia WHERE Municipios.CodigoMunicipio = ?CodigoMunicipio AND Municipios.Competencia in (2010,2011,2012,2013,2014,2015,2016) GROUP BY Municipios.Competencia, Municipios.GE12A ORDER BY Municipios.Competencia",
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<ReceitaCustoOperacao>> ReceitaCustoOperacao(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<ReceitaCustoOperacao>(@"SET SQL_BIG_SELECTS=1; SELECT Municipios.Competencia, Informacoes.FN001, Informacoes.FN017  FROM Municipios INNER JOIN Informacoes ON Municipios.CodigoMunicipio = Informacoes.CodigoMunicipio AND Municipios.Competencia = Informacoes.Competencia WHERE Municipios.CodigoMunicipio = ?CodigoMunicipio AND Municipios.Competencia in (2010,2011,2012,2013,2014,2015,2016) GROUP BY Municipios.Competencia ORDER BY Municipios.Competencia", new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<Custos> Custos(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                return await db.QuerySingleOrDefaultAsync<Custos>(@"SET SQL_BIG_SELECTS = 1; SELECT SUM(Informacoes.FN010) AS FN010, SUM(Informacoes.FN011) AS FN011, SUM(Informacoes.FN013) AS FN013, SUM(Informacoes.FN014) AS FN014, SUM(Informacoes.FN021) AS FN021, SUM(Informacoes.FN021) AS FN021, SUM(Informacoes.FN020 + Informacoes.FN039 + Informacoes.FN027) AS OUTRAS FROM Municipios INNER JOIN Informacoes on Municipios.CodigoMunicipio = Informacoes.CodigoMunicipio AND Municipios.Competencia = Informacoes.Competencia WHERE Municipios.CodigoMunicipio = ?CodigoMunicipio AND Municipios.Competencia = (SELECT MAX(Municipios.Competencia) FROM Municipios)", new { CodigoMunicipio = codigoMunicipio });
            }
        }
    }
}
