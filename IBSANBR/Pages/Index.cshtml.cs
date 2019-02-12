using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBSANBR.Models;
using IBSANBR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IBSANBR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MunicipioRepository _municipioRepository;

        public IndexModel(MunicipioRepository municipioRepository)
        {
            _municipioRepository = municipioRepository;
        }

        [FromForm]
        public string[] MunicipiosSelecionados { get; set; }
        public List<Municipio> Municipios { get; set; }

        public List<Municipio> MunicipiosInfo { get; set; } = new List<Municipio>();

        public List<PopulacaoCobertura> PopulacaoCoberturaInfo { get; set; } = new List<PopulacaoCobertura>();

        public List<PopulacaoCobertura> PopulacaoCobertura { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _municipioRepository.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Municipios = await _municipioRepository.Listar();
            PopulacaoCobertura = await _municipioRepository.PopulacaoCobertura();


            if(MunicipiosSelecionados.Length > 0)
            {
                foreach(var m in MunicipiosSelecionados)
                {
                    PopulacaoCoberturaInfo.Add(PopulacaoCobertura.Where(x => x.Codigo == m).SingleOrDefault());
                    MunicipiosInfo.Add(Municipios.Where(x=>x.Codigo == m).SingleOrDefault());
                }                
            }
            return Page();
        }
    }
}
