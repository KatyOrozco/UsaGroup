using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string CelularEc { get; set; }
        public string CelularUs { get; set; }
        public bool NewUser { get; set; }
        public bool Ingresa { get; set; }
        public string Ciudad { get; set; }
        public int CiudadId { get; set; }
    }
}