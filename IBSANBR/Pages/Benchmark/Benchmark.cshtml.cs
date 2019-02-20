using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBSANBR.Models;
using IBSANBR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IBSANBR.Pages.Benchmark
{
    public class BenchmarkModel : PageModel
    {
        private readonly InformacoesRepository _infoRepository;

        public BenchmarkModel(InformacoesRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }
        public List<Municipio> Municipios { get; set; }

        public List<Elemento> Elementos { get; set; }


        [BindProperty(SupportsGet = true)]
        public string CodigoMunicipio { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] ElementoSelecionados { get; set; } = new string[] { };
        [BindProperty(SupportsGet = true)]
        public string[] CompetenciaSelecionados { get; set; } = new string[] { };
        [BindProperty(SupportsGet = true)]
        public List<Elemento> ElementosTela { get; set; }

        [BindProperty(SupportsGet = true)]
        public BenchmarkItem Benchmark { get; set; }

        public static decimal GetPropValue(object src, string propName)
        {
            return ((decimal)src.GetType().GetProperty(propName).GetValue(src, null));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _infoRepository.ListarMunicipios();
            Elementos = await _infoRepository.ListarElementosIN();

            if (!string.IsNullOrEmpty(CodigoMunicipio) && CompetenciaSelecionados.Length > 0 && ElementoSelecionados.Length > 0)
            {

                Municipio m = await _infoRepository.ListarMunicipios(CodigoMunicipio);
                Benchmark = new BenchmarkItem()
                {
                    Elementos = Elementos.Where(x => ElementoSelecionados.Contains(x.CodigoElemento)).ToList(),
                    Categorias = CompetenciaSelecionados,
                };

                foreach (var en in ElementoSelecionados)
                {
                    foreach (var c in CompetenciaSelecionados)
                    {
                        ElementoNacional elementoNacional = await _infoRepository.ListarElementosNacionais(c);
                        ElementoEstadual elementoEstadual = await _infoRepository.ListarElementosEstaduais(m.UF, c);
                        decimal valorElementoNacional = GetPropValue(elementoNacional, en);
                        decimal valorElementoEstadual = GetPropValue(elementoEstadual, en);
                        //decimal valorElementoMunicipal = GetPropValue(elementoMunicipal, en);

                        Benchmark.Series.Add(new BenchmarkSerie()
                        {
                            name = en,
                            data = new decimal[] { valorElementoEstadual, valorElementoNacional }
                        });

                    }
                }
            }

            return Page();
        }
    }
}