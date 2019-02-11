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

        public string[] MunicipiosSelecionados { get; set; }
        public List<Municipio> Municipios { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _municipioRepository.Listar();
            return Page();
        }
    }
}
