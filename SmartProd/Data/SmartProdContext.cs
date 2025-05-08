using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartProd.Models;

namespace SmartProd.Data
{
    public class SmartProdContext : DbContext
    {
        public SmartProdContext (DbContextOptions<SmartProdContext> options)
            : base(options)
        {
        }

        public DbSet<SmartProd.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<SmartProd.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<SmartProd.Models.Fornecedor> Fornecedor { get; set; } = default!;
        public DbSet<SmartProd.Models.Funcionario> Funcionario { get; set; } = default!;
        public DbSet<SmartProd.Models.Produto> Produto { get; set; } = default!;
        public DbSet<SmartProd.Models.Saida> Saida { get; set; } = default!;
        public DbSet<SmartProd.Models.Entrada> Entrada { get; set; } = default!;
    }
}
