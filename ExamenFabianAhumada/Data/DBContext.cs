using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ExamenFabianAhumada.Models;

namespace ExamenFabianAhumada.Data
{
    public class EjemploDbContext : DbContext
    {
        public EjemploDbContext(DbContextOptions<EjemploDbContext> options) : base(options)
        {
        }

        /* DbSet indica el modelo que se va a mapear (reflejar) a la base de datos */
        public DbSet<Ubicacion> Ubicacion { get; set; }

        public DbSet<Proveedor> Proveedor { get; set; }

        public DbSet<TipoProducto> TipoProducto { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Acá se pueden cargar los datos iniciales de la base de datos

            /*
             modelBuilder.Entity<Rol>().HasData(new Rol
            {
                Id = 1,
                Nombre = "Administrador"
            });
            */

        }

    }

}
