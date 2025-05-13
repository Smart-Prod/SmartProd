namespace SmartProd.Models
{
    public class Departamento
    {
        public Guid Id { get; set; }
        public string? Nome { get; set;}
        public string? Descricao { get; set; }
        public string? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
