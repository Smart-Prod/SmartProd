namespace SmartProd.Models
{
    public class Saida
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        
        public int Quantidade { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
