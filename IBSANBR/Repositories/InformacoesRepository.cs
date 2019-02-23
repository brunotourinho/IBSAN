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

        public async Task<List<Elemento>> ListarElementosIN()
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<Elemento>(@"SELECT * FROM Elementos WHERE substr(CodigoElemento, 1, 2) = 'IN' ORDER BY Elementos.CodigoElemento");
                return result.ToList();
            }
        }

        public async Task<List<Municipio>> ListarMunicipios()
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<Municipio>(@"SELECT municipios.Id, municipios.CodigoMunicipio, municipios.Nome, municipios.UF FROM municipios ORDER BY municipios.Nome");
                return result.ToList();
            }
        }

        public async Task<Municipio> ListarMunicipios(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                return await db.QuerySingleOrDefaultAsync<Municipio>(@"SELECT Municipios.CodigoMunicipio, Municipios.Nome, Municipios.UF FROM Municipios WHERE CodigoMunicipio = ?CodigoMunicipio", new { CodigoMunicipio = codigoMunicipio });
            }
        }

        public async Task<List<PopulacaoAtendimento>> PopulacaoAtendimento(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PopulacaoAtendimento>(
                    @"SET SQL_BIG_SELECTS=1;
                    SELECT ibsanbr_inf_ge.Referencia AS Referencia, ibsanbr_ind_ag.IN055 AS IN055, ibsanbr_ind_es.IN056 AS IN056, ibsanbr_inf_ge.POP_TOT AS POP_TOT
                    FROM  ibsanbr_inf_ge
                    INNER JOIN ibsanbr_ind_ag ON ibsanbr_inf_ge.CodigoMunicipio = ibsanbr_ind_ag.CodigoMunicipio
                    INNER JOIN ibsanbr_ind_es ON ibsanbr_inf_ge.CodigoMunicipio = ibsanbr_ind_es.CodigoMunicipio
                    WHERE ibsanbr_inf_ge.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_inf_ge.Referencia in (2012,2013,2014,2015,2016)
                    GROUP BY ibsanbr_inf_ge.Referencia
                    ORDER BY ibsanbr_inf_ge.Referencia",
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<ProducaoConsumo>> ConsumoProducao(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<ProducaoConsumo>(@"SET SQL_BIG_SELECTS=1; SELECT ibsanbr_inf_ag.Referencia, SUM(ibsanbr_inf_ag.AG006) AS AG006, SUM(ibsanbr_inf_ag.AG010) AS AG010, SUM(ibsanbr_inf_ag.AG011) AS AG011, SUM(ibsanbr_inf_ag.AG018) AS AG018 FROM ibsanbr_inf_ag WHERE ibsanbr_inf_ag.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_inf_ag.Referencia in (2012,2013,2014,2015,2016) GROUP BY ibsanbr_inf_ag.Referencia ORDER BY ibsanbr_inf_ag.Referencia", 
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<PerdasAgua>> PerdasAgua(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<PerdasAgua>(@"SET SQL_BIG_SELECTS = 1; SELECT ibsanbr_ind_ag.Referencia, ibsanbr_ind_ag.IN049, ibsanbr_ind_ag.IN050 FROM ibsanbr_ind_ag WHERE ibsanbr_ind_ag.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_ind_ag.Referencia in (2012, 2013, 2014, 2015, 2016) GROUP BY ibsanbr_ind_ag.Referencia ORDER BY ibsanbr_ind_ag.Referencia", 
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<List<ReceitaDespesaDesempenho>> ReceitaDespesaDesempenho(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<ReceitaDespesaDesempenho>(@"SET SQL_BIG_SELECTS=1; SELECT ibsanbr_ind_fin.Referencia, ibsanbr_ind_fin.IN003, ibsanbr_ind_fin.IN004, ibsanbr_ind_fin.IN012 FROM ibsanbr_ind_fin WHERE ibsanbr_ind_fin.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_ind_fin.Referencia in (2012,2013,2014,2015,2016) ORDER BY ibsanbr_ind_fin.Referencia", 
                    new { CodigoMunicipio = codigoMunicipio });
                return result.ToList();
            }
        }

        public async Task<ParticipacaoDespesas> ParticipacaoDespesas(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                return await db.QuerySingleOrDefaultAsync<ParticipacaoDespesas>(@"SET SQL_BIG_SELECTS=1; SELECT ibsanbr_ind_fin.Referencia, ibsanbr_ind_fin.IN036, ibsanbr_ind_fin.IN037, ibsanbr_ind_fin.IN038, ibsanbr_ind_fin.IN039 FROM ibsanbr_ind_fin WHERE ibsanbr_ind_fin.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_ind_fin.Referencia = (SELECT MAX(ibsanbr_ind_fin.Referencia) FROM ibsanbr_ind_fin) GROUP BY ibsanbr_ind_fin.Referencia ORDER BY ibsanbr_ind_fin.Referencia", 
                    new { CodigoMunicipio = codigoMunicipio });
            }
        }

        public async Task<Estatisticas> Estatisticas(string codigoMunicipio)
        {
            using (IDbConnection db = Connection)
            {
                var resultE = await db.QuerySingleOrDefaultAsync<Estatisticas>(@"SET SQL_BIG_SELECTS=1; SELECT ibsanbr_inf_ag.AG003, ibsanbr_inf_es.ES003, ibsanbr_inf_ag.AG005, ibsanbr_inf_es.ES004, ibsanbr_ind_ag.IN055, ibsanbr_ind_es.IN056, ibsanbr_ind_ag.IN049, ibsanbr_ind_fin.IN012, ibsanbr_ind_ag.IN043 FROM ibsanbr_ind_fin INNER JOIN ibsanbr_ind_ag ON ibsanbr_ind_ag.CodigoMunicipio = ibsanbr_ind_fin.CodigoMunicipio INNER JOIN ibsanbr_ind_es ON ibsanbr_ind_es.CodigoMunicipio = ibsanbr_ind_fin.CodigoMunicipio INNER JOIN ibsanbr_inf_ag ON ibsanbr_inf_ag.CodigoMunicipio = ibsanbr_ind_fin.CodigoMunicipio INNER JOIN ibsanbr_inf_es ON ibsanbr_inf_es.CodigoMunicipio = ibsanbr_ind_fin.CodigoMunicipio WHERE ibsanbr_ind_fin.CodigoMunicipio = ?CodigoMunicipio AND ibsanbr_ind_fin.Referencia = (SELECT MAX(ibsanbr_ind_fin.Referencia) FROM ibsanbr_ind_fin) GROUP BY ibsanbr_ind_fin.Referencia ORDER BY ibsanbr_ind_fin.Referencia", 
                    new { CodigoMunicipio = codigoMunicipio });

                var resultP = await db.QueryAsync<string>(@"SELECT prestadores.Prestador FROM ibsanbrasil01.prestadores WHERE prestadores.CodigoMunicipio = ?CodigoMunicipio AND prestadores.Competencia = (SELECT MAX(prestadores.Competencia) FROM prestadores);", 
                    new { CodigoMunicipio = codigoMunicipio });

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