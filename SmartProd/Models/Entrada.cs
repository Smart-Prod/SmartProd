namespace SmartProd.Models
{
    public class Entrada
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        
        public int FornecedorId { get; set; }
        
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }
    }
}
