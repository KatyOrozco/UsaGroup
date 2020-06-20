using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class UsAddress
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Zipcode { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual State State { get; set; }
        public int StateId { get; set; }
    }
}