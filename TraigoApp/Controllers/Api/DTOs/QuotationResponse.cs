using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class QuotationResponse
    {
        public int Id { get; set; }
        public Double ComisionTraigo { get; set; }
        public Double ComisionViajero { get; set; }
        public Double Precio { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public string URL { get; set; }
        public string NombreItem { get; set; }
        public string TextoResultado { get; set; }
        public Double ComisionTotal { get; set; }
        public Double Peso { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
        public string FechaVuelta { get; set; }
        public int IdCategoria { get; set; }
        public Double Largo { get; set; }
        public Double Ancho { get; set; }
        public Double Alto { get; set; }
        public string TrackingNumber { get; set; }
        public string NombreCliente { get; set; }
        public int? IdViaje { get; set; }
    }
}