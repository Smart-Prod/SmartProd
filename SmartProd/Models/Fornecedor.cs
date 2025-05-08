namespace SmartProd.Models
{
    public class Fornecedor
    {
        public Guid Id { get; set; }
        public string? NomeEmpresa { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }
        public string? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
