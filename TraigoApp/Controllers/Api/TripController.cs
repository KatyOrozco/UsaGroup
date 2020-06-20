using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TraigoApp.Controllers.Api.DTOs;
using TraigoApp.Models;

namespace TraigoApp.Controllers.Api
{
    public class TripController : ApiController
    {
        private ApplicationDbContext _context;
        public TripController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// /api/trip/newtrip
        /// </summary>
        /// <param name="datosTrip"></param>
        /// <returns></returns>
        [HttpPost]
        public TripResponse NewTrip(TripRequest datosTrip)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datosTrip.Email);
            Trip viaje = new Trip();
            viaje.Vuelta = datosTrip.Vuelta.Date;
            viaje.Ida = datosTrip.Ida.Date;
            viaje.FechaDesde = datosTrip.ItemDesde.Date;
            viaje.FechaHasta = datosTrip.ItemHasta.Date;
            viaje.NotasImportantes = datosTrip.NotasImportantes;
            viaje.UserId = user.Id;
            viaje.UsAddressId = datosTrip.UsAddressId;
            viaje.StatusId = datosTrip.StatusId;
            viaje.CodigoReserva = datosTrip.CodigoReserva;
            viaje.AirlineId = datosTrip.AirlineId;
            _context.Trip.Add(viaje);
            _context.SaveChanges();
            TripResponse respuesta = new TripResponse();
            return respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datosTrip"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<TripResponse> GetActiveTripsByEmail(TripRequest datosTrip)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datosTrip.Email);
            List<TripResponse> respuesta = new List<TripResponse>();
            DateTime fechaHoy = DateTime.Now;

            IList<Trip> Trips = (from i in _context.Trip
                                 where i.UserId == user.Id && i.FechaHasta > fechaHoy
                                 select i).ToList<Trip>();

            foreach (Trip viaje in Trips)
            {
                TripResponse item = new TripResponse();
                item.Ida = viaje.Ida.ToString("dd/MM/yyyy");
                item.Vuelta = viaje.Vuelta.ToString("dd/MM/yyyy");
                item.FechaDesde = viaje.FechaDesde.ToString("dd/MM/yyyy");
                item.FechaHasta = viaje.FechaHasta.ToString("dd/MM/yyyy");
                item.NotasImportantes = viaje.NotasImportantes;
                item.Estado = viaje.Status.Nombre;
                item.Reserva = viaje.CodigoReserva;
                item.Aerolinea = viaje.Airline.Nombre;
                item.ViajeId = viaje.Id;
                item.NombreViajero = viaje.User.Nombre;
                respuesta.Add(item);
            }
            return respuesta;
        }

        [HttpPost]
        public IEnumerable<TripResponse> GetActiveTrips()
        {
            List<TripResponse> respuesta = new List<TripResponse>();
            DateTime fechaHoy = DateTime.Now;

            IList<Trip> Trips = (from i in _context.Trip
                                 where i.FechaHasta > fechaHoy
                                 select i).ToList<Trip>();

            foreach (Trip viaje in Trips)
            {
                TripResponse item = new TripResponse();
                item.Ida = viaje.Ida.ToString("dd/MM/yyyy");
                item.Vuelta = viaje.Vuelta.ToString("dd/MM/yyyy");
                item.FechaDesde = viaje.FechaDesde.ToString("dd/MM/yyyy");
                item.FechaHasta = viaje.FechaHasta.ToString("dd/MM/yyyy");
                item.NotasImportantes = viaje.NotasImportantes;
                item.Estado = viaje.Status.Nombre;
                item.Reserva = viaje.CodigoReserva;
                item.Aerolinea = viaje.Airline.Nombre;
                item.NombreViajero = viaje.User.Nombre;
                item.ViajeId = viaje.Id;
                respuesta.Add(item);
            }

            return respuesta;
        }

    }
}
