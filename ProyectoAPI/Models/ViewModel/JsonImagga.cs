using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI.Models.ViewModel
{
    public class JsonImagga
    {
        public Status status { get; set; }
        public Result result { get; set; }
        //
        public class Result
        {
            public List<Tags> tags { get; set; }
        }
        public class Status
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public class Tags
        {
            public string confidence { get; set; }
            public Tag tag { get; set; }
        }

        public class Tag {
            public string es { get; set; }
        }

        //public Array Tag { get; set; }
        //public string En { get; set; }
        //tags : [{
        //    confidence : "valor",
        //    tag : {
        //    en : "valor"
        //    }
        //    }]
    }

   
   

}