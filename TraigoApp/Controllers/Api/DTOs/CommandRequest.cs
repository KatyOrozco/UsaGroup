using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class CommandRequest
    {
        public string Email { get; set; }
        public int IdComanda { get; set; }
        public int Estado { get; set; }
        public Double ValorComanda { get; set; }
        public Double ValorTraigo { get; set; }
        public Double ValorViajero { get; set; }
    }
}