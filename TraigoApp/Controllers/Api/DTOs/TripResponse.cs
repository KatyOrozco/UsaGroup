using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class TripResponse
    {
        public string Ida { get; set; }
        public string Vuelta { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string NotasImportantes { get; set; }
        public string Estado { get; set; }
        public string Reserva { get; set; }
        public string Aerolinea { get; set; }
        public string NombreViajero { get; set; }
        public int ViajeId { get; set; }
    }
}