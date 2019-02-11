namespace IBSANBR.Extensions
{
    public class SystemMessage
    {
        public static class MessageType
        {
            public const string Success = "Success";
            public const string Error = "Error";
        }

        public static class Messages
        {
            public const string SaveSuccess = "Registro gravado com sucesso!";
            public const string EditSuccess = "Registro alterado com sucesso!";
            public const string DeleteSuccess = "Registro excluído com sucesso!";
            public const string SaveError = "Não foi possível gravar o registro! ";
            public const string EditError = "Não foi possível alterar o registro! ";
            public const string DeleteError = "Não foi possível excluir o registro! ";
        }

        public string Type;
        public string Title { get; set; }
        public string Text { get; set; }
        public object ReturnObject { get; set; }

        public SystemMessage(string type, string text = "", object returnObject = null, string title = null)
        {
            string defaultTitle = type == "Success" ? "Sucesso!" : "Erro!";
            Type = type;
            Text = text;
            Title = title ?? defaultTitle;
            ReturnObject = returnObject;
        }
    }
}