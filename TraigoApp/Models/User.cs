using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string CelularEc { get; set; }
        public string CelularUs { get; set; }
        public virtual City City { get; set; }
        public int CityId { get; set; }
        public string Password { get; set; }
    }
}