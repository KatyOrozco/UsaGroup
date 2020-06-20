using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PesoVolumetrico { get; set; }
    }
}