//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Publicacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publicacion()
        {
            this.Compartir = new HashSet<Compartir>();
            this.Enlace = new HashSet<Enlace>();
            this.Paso = new HashSet<Paso>();
            this.Publicacion_Usuario = new HashSet<Publicacion_Usuario>();
            this.Publicacion_Categoria = new HashSet<Publicacion_Categoria>();
            this.Publicacion_Material = new HashSet<Publicacion_Material>();
        }
    
        public int id { get; set; }
        public string titulo { get; set; }
        public string subtitulo { get; set; }
        public string descripcion { get; set; }
        public string fechaSubida { get; set; }
        public string imagenPortada { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compartir> Compartir { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enlace> Enlace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paso> Paso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publicacion_Usuario> Publicacion_Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publicacion_Categoria> Publicacion_Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publicacion_Material> Publicacion_Material { get; set; }
    }
}
