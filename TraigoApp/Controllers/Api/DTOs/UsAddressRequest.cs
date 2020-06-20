using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class UsAddressRequest
    {
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Zipcode { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
    }
}