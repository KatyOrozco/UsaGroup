using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class QuotationRequest
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public Double Precio { get; set; }
        public Double Peso { get; set; }
        public Double Largo { get; set; }
        public Double Ancho { get; set; }
        public Double Alto { get; set; }
        public int EstadoCotizacion { get; set; }
        public int? ViajeId { get; set; }
        public string Email { get; set; }
        public int CategoryId { get; set; }
        public string NombreItem { get; set; }
        public string TrackingNumber { get; set; }
    }
}