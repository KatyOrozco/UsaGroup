using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public bool entra { get; set; }
        public Double ComisionTraigo { get; set; }
        public Double ComisionViajero { get; set; }
        public Double ComisionTraigoIva { get; set; }
        public Double ComisionTotal { get; set; }
        public Double Precio { get; set; }
        public string URL { get; set; }
        public string NombreItem { get; set; }
        public Double Peso { get; set; }
        public string FechaVuelta { get; set; }
        public Double Largo { get; set; }
        public Double Ancho { get; set; }
        public Double Alto { get; set; }
        public string TrackingNumber { get; set; }
        public int IdCotizacion { get; set; }
    }
}