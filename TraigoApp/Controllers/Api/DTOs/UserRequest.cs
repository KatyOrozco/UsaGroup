using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class UserRequest
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string CelularEc { get; set; }
        public string CelularUs { get; set; }
        public string Clave { get; set; }
        public int IdCiudad { get; set; }
    }
}