using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string NombreItem { get; set; }
        public Double Precio { get; set; }
        public Double Peso { get; set; }
        public Double ComisionTraigo { get; set; }
        public Double ComisionTraigoIva { get; set; }
        public Double ComisionViajero { get; set; }
        public Double ISD { get; set; }
        public Boolean VentaExitosa { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FechaCotizacion { get; set; }
        public DateTime FechaVentaExitosa{ get; set; }
        public Double Largo { get; set; }
        public Double Ancho { get; set; }
        public Double Alto { get; set; }
        public Double PesoVolumetrico { get; set; }
        public Double Volumen { get; set; }
        public string TackingNumber { get; set; }
        public string Courrier { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public virtual QuotationStatus QuotationStatus { get; set; }
        public int QuotationStatusId { get; set; }
        public DateTime? FechaVuelta { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}