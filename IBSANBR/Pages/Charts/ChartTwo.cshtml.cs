using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBSANBR.Models;
using IBSANBR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IBSANBR.Pages.Charts
{
    public class ChartTwoModel : PageModel
    {
        private readonly InformacoesRepository _infoRepository;

        public ChartTwoModel(InformacoesRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        [FromForm]
        public string CodigoMunicipio { get; set; }

        public List<Municipio> Municipios { get; set; }

        public List<ReceitaCustoOperacao> ReceitaCustoOperacao { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _infoRepository.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Municipios = await _infoRepository.Listar();
            ReceitaCustoOperacao = await _infoRepository.ReceitaCustoOperacao(CodigoMunicipio);
            return Page();
        }
    }
}