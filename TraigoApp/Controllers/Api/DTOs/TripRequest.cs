using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class TripRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Vuelta { get; set; }
        public DateTime Ida { get; set; }
        public DateTime ItemDesde { get; set; }
        public DateTime ItemHasta { get; set; }
        public string NotasImportantes { get; set; }
        public int StatusId { get; set; }
        public int UsAddressId { get; set; }
        public string CodigoReserva { get; set; }
        public int AirlineId { get; set; }
    }
}