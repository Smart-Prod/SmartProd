namespace SmartProd.Models
{
    public class ProdutoVendido
    {
        public Guid ProdutoId { get; set; }

        // Para navegação (relacionamento com Produto, se necessário)
        public Produto? Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        // Calculado: total de cada item vendido
        public decimal Total => Quantidade * ValorUnitario;
    }
}
