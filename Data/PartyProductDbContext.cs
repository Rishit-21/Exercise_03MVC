using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise_03.Data
{
    public class PartyProductDbContext:DbContext
    {
        public PartyProductDbContext(DbContextOptions<PartyProductDbContext> options)
            :base(options)
        {

        }
        public DbSet<Party> Party { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<AssignParty> AssignParty { get; set; }
         
        public DbSet<ProductRate> ProductRate { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Party>().HasIndex(x => new { x.partyName }).IsUnique(true);
            mb.Entity<Product>().HasIndex(x => new { x.productName }).IsUnique(true);
            mb.Entity<AssignParty>().HasIndex(x => new { x.PartyId }).IsUnique(false);
            mb.Entity<AssignParty>().HasIndex(x => new { x.ProductId }).IsUnique(false);
            mb.Entity<ProductRate>().HasIndex(x => new { x.ProductId }).IsUnique(true);
        }
    }
}
