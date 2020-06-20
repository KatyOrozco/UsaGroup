using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime Vuelta { get; set; }
        public DateTime Ida { get; set; }
        public DateTime FechaHasta { get; set; }
        public DateTime FechaDesde { get; set; }
        public string NotasImportantes { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual UsAddress UsAddress { get; set; }
        public int UsAddressId { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public string CodigoReserva { get; set; }
        public virtual Airline Airline { get; set; }
        public int AirlineId { get; set; }
    }
}