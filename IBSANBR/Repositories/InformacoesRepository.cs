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

        public async Task<List<PopulacaoAtendimento>> PopulacaoAtendimento(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PopulacaoAtendimento>(
                    @"SET SQL_BIG_SELECTS=1; SELECT Populacao.Competencia, Populacao.GE12A, Informacoes.IN055, Informacoes.IN056 FROM Populacao INNER JOIN Informacoes on Populacao.CodigoMunicipio = Informacoes.CodigoMunicipio AND Populacao.Competencia = Informacoes.Competencia WHERE Informacoes.CodigoMunicipio = ?CodigoMunicipio AND Populacao.Competencia in (2010,2011,2012,2013,2014,2015,2016) GROUP BY Populacao.Competencia ORDER BY Populacao.Competencia",
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }       

        public async Task<List<ProducaoConsumo>> ConsumoProducao(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<ProducaoConsumo>(@"SET SQL_BIG_SELECTS=1; SELECT Informacoes.Competencia, Informacoes.AG006, Informacoes.AG010, Informacoes.AG011, Informacoes.AG018 FROM Informacoes WHERE Informacoes.CodigoMunicipio = CodigoMunicipio AND Informacoes.Competencia in (2010,2011,2012,2013,2014,2015,2016) GROUP BY Informacoes.Competencia ORDER BY Informacoes.Competencia", new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<PerdasAgua>> PerdasAgua(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PerdasAgua>(@"SET SQL_BIG_SELECTS = 1; SELECT Informacoes.Competencia, Informacoes.IN049, Informacoes.IN050 FROM Informacoes WHERE Informacoes.CodigoMunicipio = ?CodigoMunicipio AND Informacoes.Competencia in (2010, 2011, 2012, 2013, 2014, 2015, 2016) GROUP BY Informacoes.Competencia ORDER BY Informacoes.Competencia", new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<ReceitaDespesaDesempenho>> ReceitaDespesaDesempenho(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<ReceitaDespesaDesempenho>(@"SET SQL_BIG_SELECTS=1; SELECT Informacoes.Competencia, Informacoes.IN003, Informacoes.IN004, Informacoes.IN012 FROM Informacoes WHERE Informacoes.CodigoMunicipio = ?CodigoMunicipio AND Informacoes.Competencia in (2010,2011,2012,2013,2014,2015,2016) GROUP BY Informacoes.Competencia ORDER BY Informacoes.Competencia", new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<ParticipacaoDespesas> ParticipacaoDespesas(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                return await db.QuerySingleOrDefaultAsync<ParticipacaoDespesas>(@"SET SQL_BIG_SELECTS=1; SELECT Informacoes.Competencia, Informacoes.IN036, Informacoes.IN037, Informacoes.IN038, Informacoes.IN039 FROM Informacoes WHERE Informacoes.CodigoMunicipio = '120001' AND Informacoes.Competencia = (SELECT MAX(Informacoes.Competencia) FROM Informacoes) GROUP BY Informacoes.Competencia ORDER BY Informacoes.Competencia", new { CodigoMunicipio = codigoMunicipio });
            }
        }

        public async Task<Estatisticas> Estatisticas(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var resultE = await db.QuerySingleOrDefaultAsync<Estatisticas>(@"SET SQL_BIG_SELECTS=1; SELECT Informacoes.AG003, Informacoes.ES003, Informacoes.AG005, Informacoes.ES005, Informacoes.IN055, Informacoes.IN056, Informacoes.IN049, Informacoes.IN012, Informacoes.IN043 FROM Informacoes WHERE Informacoes.CodigoMunicipio = ?CodigoMunicipio AND Informacoes.Competencia = (SELECT MAX(Informacoes.Competencia) FROM Informacoes) GROUP BY Informacoes.Competencia ORDER BY Informacoes.Competencia", new { CodigoMunicipio = codigoMunicipio });
                var resultP = await db.QueryAsync<string>(@"SET SQL_BIG_SELECTS=1; SELECT CONCAT(Prestadores.Prestador, ' - ' , Prestadores.Sigla) AS Prestador FROM Prestadores WHERE Prestadores.CodigoMunicipio = ?CodigoMunicipio AND Prestadores.Competencia = (SELECT MAX(Informacoes.Competencia) FROM Informacoes)", new { CodigoMunicipio = codigoMunicipio });

                var e = new Estatisticas()
                {
                    Prestadores = resultP.ToList(),
                    AG003 = resultE.AG003,
                    ES003 = resultE.ES003,
                    AG005 = resultE.AG005,
                    ES004 = resultE.ES004,
                    IN055 = resultE.IN055,
                    IN056 = resultE.IN056,
                    IN049 = resultE.IN049,
                    IN012 = resultE.IN012,
                    IN043 = resultE.IN043
                };

                return e;
            }
        }
    }
}
