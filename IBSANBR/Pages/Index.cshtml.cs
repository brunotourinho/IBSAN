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

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _municipioRepository.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Municipios = await _municipioRepository.Listar();
            if(MunicipiosSelecionados.Length > 0)
            {
                foreach(var m in MunicipiosSelecionados)
                {
                    MunicipiosInfo.Add(Municipios.Where(x=>x.Codigo == m).SingleOrDefault());
                }                
            }
            return Page();
        }
    }
}
