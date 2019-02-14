using IBSANBR.Models;
using IBSANBR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBSANBR.Pages
{
    public class ChartOneModel : PageModel
    {
        private readonly InformacoesRepository _infoRepository;

        public ChartOneModel(InformacoesRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        [FromForm]
        public string CodigoMunicipio { get; set; }

        public List<Municipio> Municipios { get; set; }

        public List<PopulacaoCobertura> PopulacaoCobertura { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _infoRepository.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Municipios = await _infoRepository.Listar();
            PopulacaoCobertura = await _infoRepository.PopulacaoCobertura(CodigoMunicipio);
            return Page();
        }
    }
}