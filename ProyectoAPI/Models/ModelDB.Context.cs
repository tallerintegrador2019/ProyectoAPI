﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class todaviasirveDBEntities : DbContext
    {
        public todaviasirveDBEntities()
            : base("name=todaviasirveDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Enlace> Enlace { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Publicacion> Publicacion { get; set; }
        public virtual DbSet<Publicacion_Categoria> Publicacion_Categoria { get; set; }
        public virtual DbSet<Publicacion_Material> Publicacion_Material { get; set; }
        public virtual DbSet<Rango> Rango { get; set; }
        public virtual DbSet<Redes> Redes { get; set; }
        public virtual DbSet<Suscriptor> Suscriptor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
