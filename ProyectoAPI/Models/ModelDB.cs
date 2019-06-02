namespace ProyectoAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Publicacion> Publicacion { get; set; }

        // ACA SE VAN AGREGANDO LAS CLASES (TABLAS) QUE VAMOS NECESITANDO. por ejemplo como publicacion.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
