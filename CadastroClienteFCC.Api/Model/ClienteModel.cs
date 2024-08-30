namespace CadastroClienteFCC.Api.Model
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public string? CPF { get; set; }
        public string? Nome { get; set; }
        public string? RG { get; set; }
        public DateTime DataExpedicaoRG { get; set; }
        public string? OrgaoExpedidor { get; set; }
        public string? UF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? EstadoCivil  { get; set; }
    }
}
