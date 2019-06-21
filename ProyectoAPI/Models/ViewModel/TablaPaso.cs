using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
namespace ProyectoAPI.Models.ViewModel
{
    public class TablaPaso
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public FileUpload upload { get; set; }
        public int idPublicacion { get; set; }
    }
}