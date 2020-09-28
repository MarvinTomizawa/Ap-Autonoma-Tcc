namespace Assets.Scripts.Exception
{
    public static class ExceptionsMessages
    {
        public const string TicketEBrinquedoJaExistente = "Já existe um comando para o brinquedo e ticket removido informados";
        public const string FilaVazia = "A fila está vazia";
        public const string TamanhoMaximoPreenchido = "Tamanho máximo de tickets preenchido.";
        public const string NaoFoiPossivelProcessar = "Não foi encontrado um ticket para o brinquedo a ser adicionado.";

        public static string SemComandoParaBrinquedoETicket(char letter, char ticket)
            => $"Não possui comando para a letra {letter} e ticket {ticket}.";
    }
}
