namespace ProyectoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Publicacion")]
    public partial class Publicacion
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(100)]
        public string descripcion { get; set; }

        [StringLength(100)]
        public string imagen { get; set; }
    }
}
