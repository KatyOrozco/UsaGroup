using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
    }
}