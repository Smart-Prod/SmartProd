namespace SmartProd.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }
        public string? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
