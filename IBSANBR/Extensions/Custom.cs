using System.Collections.Generic;

namespace IBSANBR.Extensions
{
    public static class Custom
    {
        public static string NumeroProcesso(string numero)
        {
            return numero.Substring(0, 3) + "." + numero.Substring(3, 3) + "." + numero.Substring(6, 5) + "/" + numero.Substring(11, 4);
        }

        public static List<string> GetUfs()
        {
            return new List<string>() { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO" };
        }

        public static List<string> PerfilRecomendacao(int idPerfil)
        {
            /*
                1	Administrador
                2	Coordenador
                3	Administrativo Federação
                4	Auditor Federação
                5	Mediador Federação
                6	Atendimento Unimed
                7	Jurídico
                8   Consultor
            */

            List<string> acoes = new List<string>();
            switch (idPerfil)
            {
                //-> Administrador
                case 1:
                    acoes = new List<string>()
                    {
                        "Questionamento Técnico",
                        "Divergência em OPME",
                        "Divergência Técnica Mantida (Junta Médica)",
                        //"Favorável Parcial",
                        //"Recomenda-se Autorizar (Mediação)",
                        "Recomenda-se Autorizar (Finalizar)",
                        //"Recomenda-se Regra (Mediação)",
                        "Recomenda-se Regra (Finalizar)",
                        "Favorável Integral",
                        "Encaminhar para Jurídico",
                        //"Desfavorável",
                        "Resultado Junta Médica",
                        "Avaliação Coordenador",
                        //"Avaliação Auditor",
                        "Manter Mediação",
                        "Avaliação Consultoria",
                        "Finalizar Conforme Junta Médica"
                    };
                    break;
                //-> Coordenador
                case 2:
                    acoes = new List<string>()
                    {
                        "Questionamento Técnico",
                        "Divergência em OPME",
                        "Divergência Técnica Mantida (Junta Médica)",
                        "Recomenda-se SAG por Regra (Finalizar)",
                        //"Favorável Parcial",
                        //"Recomenda-se Autorizar (Mediação)",
                        "Recomenda-se Autorizar (Finalizar)",
                        //"Recomenda-se Regra (Mediação)",
                        "Recomenda-se Regra (Finalizar)",
                        "Favorável Integral",
                        "Encaminhar para Jurídico",
                        //"Desfavorável",
                        "Resultado Junta Médica",
                        "Avalia Coordenador",
                        //"Avaliação Auditor",
                        "Manter Mediação",
                        "Avaliação Consultoria",
                        "Finalizar Conforme Junta Médica"
                    };
                    break;
                //-> Administrativo Federação
                case 3:
                    acoes = new List<string>()
                    {
                        "Resultado Junta Médica"
                    };
                    break;

                //-> Auditor Federação
                case 4:
                    acoes = new List<string>()
                    {
                        "Questionamento Técnico",
                        "Divergência em OPME",
                        "Divergência Técnica Mantida (Junta Médica)",
                        //"Favorável Parcial",
                        //"Recomenda-se Autorizar (Mediação)",
                        "Recomenda-se Autorizar (Finalizar)",
                        //"Recomenda-se Regra (Mediação)",
                        "Recomenda-se Regra (Finalizar)",
                        "Favorável Integral",
                        "Avaliação Auditor",
                        //"Desfavorável",
                        "Resultado Junta Médica",
                        "Avaliação Coordenador",
                        //"Avaliação Auditor",
                        "Avaliação Consultoria"
                    };
                    break;
                //-> Mediador Federação
                case 5:
                    acoes = new List<string>()
                    {
                        "Avaliação Coordenador",
                        "Avaliação Auditor",
                        "Manter Mediação",
                    };
                    break;
                //-> Administrativo Unimed não tem ação
                case 6:
                    acoes = new List<string>();
                    break;
                //-> Jurídico
                case 7:
                    acoes = new List<string>()
                    {
                        "Avaliação Coordenador"
                    };
                    break;
                //-> Consultor
                case 8:
                    acoes = new List<string>()
                    {
                        "Avaliação Auditor"
                    };
                    break;

                default:
                    break;
            }
            return acoes;
        }

        public static StatusColor GetStatusColor(string recomendacao)
        {
            switch (recomendacao)
            {
                case "Junta Médica": return new StatusColor { Cor = "purple", Status = "Junta Médica" };
                case "Mediador": return new StatusColor { Cor = "yellow", Status = "Mediação" };
                case "Concluído": return new StatusColor { Cor = "blue", Status = "Concluído" };
                case "Jurídico": return new StatusColor { Cor = "grey", Status = "Jurídico" };
                case "Auditoria": return new StatusColor { Cor = "green", Status = "Auditoria" };
                case "Coordenador": return new StatusColor { Cor = "teal", Status = "Coordenador" };
                case "Consultor": return new StatusColor { Cor = "orange", Status = "Consultor" };
                default: return new StatusColor { Cor = "black", Status = "Não Encontrado" };
            }
        }

        public static string RecomendacaoStatus(string recomendacao)
        {
            switch (recomendacao)
            {
                case "Questionamento Técnico": return "Mediador";
                case "Divergência em OPME": return "Mediador";
                case "Divergência Técnica Mantida (Junta Médica)": return "Junta Médica";
                case "Recomenda-se SAG por Regra (Finalizar)": return "Concluído";
                //case "Favorável Parcial": return "Mediador";
                //case "Recomenda-se Autorizar (Mediação)": return "Mediador";
                case "Recomenda-se Autorizar (Finalizar)": return "Concluído";
                //case "Recomenda-se Regra (Mediação)": return "Mediador";
                case "Recomenda-se Regra (Finalizar)": return "Concluído";
                case "Favorável Integral": return "Concluído";
                case "Encaminhar para Jurídico": return "Jurídico";
                case "Avaliação Auditor": return "Auditoria";
                case "Desfavorável": return "Concluído";
                case "Resultado Junta Médica": return "Coordenador";
                case "Avaliação Coordenador": return "Coordenador";
                case "Manter Mediação": return "Mediador";
                case "Avaliação Consultoria": return "Consultor";
                case "Finalizar Conforme Junta Médica": return "Concluído";
                default: return "";
            }
        }

        public static bool AcessaItem(int idPerfil, string itemStatus)
        {
            /*
                1	Administrador
                2	Coordenador
                3	Administrativo Federação
                4	Auditor Federação
                5	Mediador Federação
                6	Atendimento Unimed
                7	Jurídico
            */

            //-> Se o item estiver concluído, ninguém pode acessar
            if (itemStatus != "Concluído")
            {
                //-> Acessa Item em Coordenação Administrador, Coordenador podem acessar
                if (itemStatus == "Coordenador")
                {
                    List<int> acessoCoordenador = new List<int> { 1, 2 };
                    if (acessoCoordenador.Contains(idPerfil))
                    {
                        return true;
                    }

                    return false;
                }

                //-> Acessa Item em Mediação Administrador, Coordenador e Mediador podem acessar
                if (itemStatus == "Mediador")
                {
                    List<int> acessoMediador = new List<int> { 1, 2, 5 };
                    if (acessoMediador.Contains(idPerfil))
                    {
                        return true;
                    }

                    return false;
                }

                //-> Acessa Item em Auditoria Administrador, Coordenador e Auditor podem acessar
                if (itemStatus == "Auditoria")
                {
                    List<int> acessoAuditoria = new List<int> { 1, 2, 4 };
                    if (acessoAuditoria.Contains(idPerfil))
                    {
                        return true;
                    }
                }

                //-> Acessa Item em Junta Médica apenas Administrador, Coordenador e Administrativo
                if (itemStatus == "Junta Médica")
                {
                    List<int> acessoJuntaMedica = new List<int> { 1, 2, 3 };
                    if (acessoJuntaMedica.Contains(idPerfil))
                    {
                        return true;
                    }
                }

                //-> Acessa Item em Consultoria apenas Administrador, Coordenador e Administrativo
                if (itemStatus == "Consultor")
                {
                    List<int> acessoConsultor = new List<int> { 1, 2, 3 };
                    if (acessoConsultor.Contains(idPerfil))
                    {
                        return true;
                    }
                }

                //-> Acessa Item em Jurídico apenas Administrador, Coordenador e Jurídico podem acessar
                if (itemStatus == "Jurídico")
                {
                    List<int> acessoJuridico = new List<int> { 1, 2, 7 };
                    if (acessoJuridico.Contains(idPerfil))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public class StatusColor
        {
            public string Cor { get; set; }
            public string Status { get; set; }
        }
    }
}