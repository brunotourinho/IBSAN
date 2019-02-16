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
        public List<PopulacaoAtendimento> PopulacaoAtendimento { get; set; }

        [BindProperty]
        public List<ReceitaDespesaDesempenho> ReceitaDespesaDesempenho { get; set; }

        [BindProperty]
        public List<PerdasAgua> PerdasAgua { get; set; }

        [BindProperty]
        public ParticipacaoDespesas ParticipacaoDespesas { get; set; }

        [BindProperty]
        public List<ProducaoConsumo> ProducaoConsumo { get; set; }

        [BindProperty]
        public Estatisticas Estatisticas { get; set; }


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
            PopulacaoAtendimento = await _infoRepository.PopulacaoAtendimento(CodigoMunicipio);
            ProducaoConsumo = await _infoRepository.ConsumoProducao(CodigoMunicipio);
            PerdasAgua = await _infoRepository.PerdasAgua(CodigoMunicipio);
            ReceitaDespesaDesempenho = await _infoRepository.ReceitaDespesaDesempenho(CodigoMunicipio);
            ParticipacaoDespesas = await _infoRepository.ParticipacaoDespesas(CodigoMunicipio);
            Estatisticas = await _infoRepository.Estatisticas(CodigoMunicipio);
            return Page();
        }
    }
}