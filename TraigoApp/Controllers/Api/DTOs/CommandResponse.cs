using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class CommandResponse
    {
        public bool Respuesta { get; set; }
        public int IdCommand { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public double? ValorTraigo { get; set; }
        public double? ValorViajero { get; set; }
        public double? ValorComanda { get; set; }
    }
}