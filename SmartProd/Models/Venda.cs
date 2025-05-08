namespace SmartProd.Models
{
    public class Venda
    {
        public Guid Id { get; set; }
        public int ClienteId { get; set; }
        
        public int FuncionarioId { get; set; }
        
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
