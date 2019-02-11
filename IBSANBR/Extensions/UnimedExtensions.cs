using System.Collections.Generic;

namespace IBSANBR.Extensions
{
    public static class UnimedExtensions
    {
        public static List<KeyValuePair<string, string>> ListarUnimeds(bool federacao = false, bool nomeReduzido = false)
        {
            var unimeds = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(nomeReduzido ? "Araguaia" : "Unimed Araguaia", "235"),
                new KeyValuePair<string, string>(nomeReduzido ? "Cáceres" : "Unimed Cáceres", "318"),
                new KeyValuePair<string, string>(nomeReduzido ? "Cuiabá" : "Unimed Cuiabá", "056"),
                new KeyValuePair<string, string>(nomeReduzido ? "Norte do MT" : "Unimed Norte de Mato Grosso", "279"),
                new KeyValuePair<string, string>(nomeReduzido ? "Rondonópolis" : "Unimed Rondonópolis", "139"),
                new KeyValuePair<string, string>(nomeReduzido ? "Vale do Jauru" : "Unimed Vale do Jauru", "271"),
                new KeyValuePair<string, string>(nomeReduzido ? "Vale do Sepotuba" : "Unimed Vale do Sepotuba", "531")
            };
            if (federacao)
                unimeds.Add(new KeyValuePair<string, string>(nomeReduzido ? "Federação MT" : "Unimed Mato Grosso", "511"));
            return unimeds;
        }

        public static string UnimedPorCodigo(string codigo, bool nomeReduzido = false)
        {
            switch (codigo)
            {
                case "056":
                    return nomeReduzido ? "Cuiabá" : "Unimed Cuiabá";

                case "139":
                    return nomeReduzido ? "Rondonópolis" : "Unimed Rondonópolis";

                case "235":
                    return nomeReduzido ? "Araguaia" : "Unimed Araguaia";

                case "271":
                    return nomeReduzido ? "Vale do Jauru" : "Unimed Vale do Jauru";

                case "279":
                    return nomeReduzido ? "Norte do Mato Grosso" : "Unimed Norte do Mato Grosso";

                case "318":
                    return nomeReduzido ? "Cáceres" : "Unimed Cáceres";

                case "531":
                    return nomeReduzido ? "Vale do Sepotuba" : "Unimed Vale do Sepotuba";

                case "511":
                    return nomeReduzido ? "Federação MT" : "Unimed Mato Grosso";

                default:
                    return "Não identificado";
            }
        }
    }
}