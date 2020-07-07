using OdeToFood.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{

    //DbContext generalmente representa una conexión de base de datos y un conjunto de tablas. 
    public partial class RestoDbContext : DbContext
    {
        public RestoDbContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer<RestoDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        /// DbSet Artist se utiliza para representar una tabla.
        public virtual DbSet<Artist> Artist { get; set; }

        /// DbSet Product se utiliza para representar una tabla.
        public virtual DbSet<Product> Product { get; set; }


        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }

        /// DbSet Product se utiliza para representar una tabla.
        public virtual DbSet<Error> Error { get; set; }

    }
}
