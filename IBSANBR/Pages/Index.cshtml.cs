using IBSANBR.Models;
using IBSANBR.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBSANBR
{
    public class IndexModel : PageModel
    {
        private readonly InformacoesRepository _infoRepository;

        [FromForm]
        public string CodigoMunicipio { get; set; }

        public List<Municipio> Municipios { get; set; }

        [BindProperty]
        public List<PopulacaoCobertura> PopulacaoCobertura { get; set; }

        [BindProperty]
        public List<ReceitaCustoOperacao> ReceitaCustoOperacao { get; set; }

        [BindProperty]
        public Custos Custos { get; set; }


        public IndexModel(InformacoesRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Municipios = await _infoRepository.Listar();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Municipios = await _infoRepository.Listar();
            PopulacaoCobertura = await _infoRepository.PopulacaoCobertura(CodigoMunicipio);
            ReceitaCustoOperacao = await _infoRepository.ReceitaCustoOperacao(CodigoMunicipio);
            Custos = await _infoRepository.Custos(CodigoMunicipio);
            return Page();
        }
    }
}