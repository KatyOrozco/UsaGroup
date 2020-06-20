using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using TraigoApp.Controllers.Api.DTOs;
using TraigoApp.Models;

namespace TraigoApp.Controllers.Api
{
    public class QuotationController : ApiController
    {
        private ApplicationDbContext _context;
        public QuotationController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IEnumerable<QuotationResponse> GetQuotationByUser(QuotationRequest datosUser)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datosUser.Email);
            List<QuotationResponse> respuesta = new List<QuotationResponse>();
            IList<Quotation> QuotationByUser = (from i in _context.Quotation
                                                where i.UserId == user.Id
                                                orderby i.FechaCotizacion ascending
                                                select i).ToList<Quotation>();

            foreach (Quotation cotizacion in QuotationByUser)
            {
                QuotationResponse item = new QuotationResponse();
                item.URL = cotizacion.Url;
                item.ComisionTraigo = Math.Round(cotizacion.ComisionTraigoIva, 2);
                item.ComisionViajero = Math.Round(cotizacion.ComisionViajero, 2);
                item.Precio = Math.Round(cotizacion.Precio, 2);
                item.NombreItem = cotizacion.NombreItem;
                item.Peso = cotizacion.Peso;
                item.Id = cotizacion.Id;
                item.Estado = cotizacion.QuotationStatus.Nombre;
                item.ComisionTotal = Math.Round(cotizacion.ComisionViajero + cotizacion.ComisionTraigoIva, 2);
                //Double costoTotal = Math.Round(cotizacion.ComisionTraigo + cotizacion.ComisionViajero + cotizacion.Precio + cotizacion.ComisionTraigoIva, 2);
                if(cotizacion.FechaVuelta.HasValue)
                {
                    item.FechaVuelta = cotizacion.Trip.User.Nombre + " / " + cotizacion.Trip.Vuelta.ToString("dd/MM/yyyy");
                }
                else
                {
                    item.FechaVuelta = "No definido";
                }
                item.TextoResultado = "Estimado viajero, tenemos esta oferta por el item " + cotizacion.NombreItem + " de peso " + item.Peso + " lbs. Oferta de comisión: " + item.ComisionViajero + " USD.";
                respuesta.Add(item);
            }
            return respuesta;
        }

        /// <summary>
        /// Funcion trae paquetes asignados a un viaje. 
        /// Trae paquetes por viaje.
        /// URL: /api/quotaiton/PackagesByTrip
        /// </summary>
        /// <param name="datosCotizacion">TripId: int</param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<QuotationResponse> PackagesByTrip(QuotationRequest datosCotizacion)
        {
            List<QuotationResponse> response = new List<QuotationResponse>();
            IList<Quotation> PackagesWithTrip = (from i in _context.Quotation
                                                     where i.TripId == datosCotizacion.ViajeId && i.QuotationStatusId == 3
                                                     orderby i.Trip.Id descending
                                                     select i).ToList<Quotation>();

            foreach (Quotation asignados in PackagesWithTrip)
            {
                QuotationResponse item = new QuotationResponse();
                item.NombreCliente = asignados.User.Nombre;
                item.URL = asignados.Url;
                item.ComisionTraigo = Math.Round(asignados.ComisionTraigoIva, 2);
                item.ComisionViajero = Math.Round(asignados.ComisionViajero, 2);
                item.Precio = Math.Round(asignados.Precio, 2);
                item.NombreItem = asignados.NombreItem;
                item.Peso = asignados.Peso;
                item.Id = asignados.Id;
                item.TrackingNumber = asignados.TackingNumber;
                item.Estado = asignados.QuotationStatus.Nombre;
                item.ComisionTotal = Math.Round(asignados.ComisionViajero + asignados.ComisionTraigo + asignados.ComisionTraigoIva, 2);
                Double costoTotal = Math.Round(asignados.ComisionTraigo + asignados.ComisionViajero + asignados.Precio + asignados.ComisionTraigoIva, 2);
                response.Add(item);
            }

            return response;
        }

        [HttpPost]
        public IEnumerable<QuotationResponse> GetQuotationByTrip(QuotationRequest datosCotizacion)
        {
            var trip = _context.Trip.SingleOrDefault(c => c.Id == datosCotizacion.ViajeId);

            List<QuotationResponse> respuesta = new List<QuotationResponse>();
            IList<Quotation> QuotationByTrip = (from i in _context.Quotation
                                                where i.TripId == trip.Id
                                                orderby i.FechaCotizacion descending
                                                select i).ToList<Quotation>();

            foreach (Quotation cotizacion in QuotationByTrip)
            {
                QuotationResponse item = new QuotationResponse();
                item.URL = cotizacion.Url;
                item.ComisionTraigo = Math.Round(cotizacion.ComisionTraigoIva, 2);
                item.ComisionViajero = Math.Round(cotizacion.ComisionViajero, 2);
                item.Precio = Math.Round(cotizacion.Precio, 2);
                item.NombreItem = cotizacion.NombreItem;
                item.Peso = cotizacion.Peso;
                item.Id = cotizacion.Id;
                item.ComisionTotal = Math.Round(cotizacion.ComisionViajero + cotizacion.ComisionTraigo + cotizacion.ComisionTraigoIva, 2);
                Double costoTotal = Math.Round(cotizacion.ComisionTraigo + cotizacion.ComisionViajero + cotizacion.Precio + cotizacion.ComisionTraigoIva, 2);
                respuesta.Add(item);
            }
            return respuesta;
        }

        [HttpPost]
        public QuotationResponse NewQuotation (QuotationRequest datosCotizacion)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            if(datosCotizacion.Peso == 0 || datosCotizacion.Precio == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var user = _context.User.SingleOrDefault(c => c.Email == datosCotizacion.Email);
            Quotation cotizacion = new Quotation();
            cotizacion.Url = datosCotizacion.URL;
            cotizacion.NombreItem = textInfo.ToTitleCase(datosCotizacion.NombreItem);
            cotizacion.UserId = user.Id;
            cotizacion.FechaCotizacion = DateTime.Now.Date;
            cotizacion.FechaVentaExitosa = DateTime.Now.Date;
            cotizacion.Precio = datosCotizacion.Precio;
            cotizacion.Largo = datosCotizacion.Largo;
            cotizacion.Ancho = datosCotizacion.Ancho;
            cotizacion.Alto = datosCotizacion.Alto;
            cotizacion.CategoryId = datosCotizacion.CategoryId;
            cotizacion.Peso = Math.Round(datosCotizacion.Peso, 2);
            cotizacion.QuotationStatusId = 1;
            cotizacion.PaymentId = 1;
            Double precioIsd = datosCotizacion.Precio * 0.05;
            cotizacion.ISD = Math.Round(precioIsd, 2);
            Double volumen = datosCotizacion.Largo * datosCotizacion.Ancho * datosCotizacion.Alto;
            cotizacion.Volumen = volumen;
            Double volumetrico = volumen / 5000;
            cotizacion.PesoVolumetrico = volumetrico;
            cotizacion.VentaExitosa = false;
        
            switch (datosCotizacion.CategoryId)
            {
                case 1:

                    cotizacion.ComisionViajero = 100;
                    cotizacion.ComisionTraigo = 50;
                    Double precioPesoLaptop = datosCotizacion.Peso * 6;
                    if (precioPesoLaptop > 100)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoLaptop, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;


                case 2:
                    cotizacion.ComisionViajero = 60;
                    cotizacion.ComisionTraigo = 30;
                    Double precioPesoConsola = datosCotizacion.Peso * 6;
                    if (precioPesoConsola > 60)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoConsola, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                case 3:
                    cotizacion.ComisionViajero = 10;
                    cotizacion.ComisionTraigo = 5;
                    Double precioPesoPerfumes = datosCotizacion.Peso * 6;
                    if (precioPesoPerfumes > 10)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoPerfumes, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                
                case 4:
                    cotizacion.ComisionViajero = 50;
                    cotizacion.ComisionTraigo = 20;
                    Double precioPesoMonitorComputadora = datosCotizacion.Peso * 6;
                    if (precioPesoMonitorComputadora > 50)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoMonitorComputadora, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;
            
                case 5:
                    cotizacion.ComisionViajero = 50;
                    cotizacion.ComisionTraigo = 25;
                    Double precioPesoCelular = datosCotizacion.Peso * 6;
                    if (precioPesoCelular > 50)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoCelular, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                default:
                    {

                     cotizacion.ComisionViajero = Math.Round(datosCotizacion.Peso * 6, 2);
                    cotizacion.ComisionTraigo = Math.Round(datosCotizacion.Precio * 0.10, 2);
                    }
                    break;
            }

            _context.Quotation.Add(cotizacion);
            _context.SaveChanges();
            QuotationResponse respuesta = new QuotationResponse();
            respuesta.ComisionTraigo = cotizacion.ComisionTraigo;
            respuesta.ComisionViajero = cotizacion.ComisionViajero;
            respuesta.Peso = cotizacion.Peso;
            respuesta.Precio = cotizacion.Precio;
            respuesta.URL = cotizacion.Url;
            respuesta.Id = cotizacion.Id;
            respuesta.NombreItem = cotizacion.NombreItem;
            respuesta.FechaVuelta = "Fecha aún no asignada";
            Double costoTotal = Math.Round(cotizacion.ComisionViajero + cotizacion.Precio + cotizacion.ComisionTraigo, 2);
            respuesta.ComisionTotal = costoTotal;
            return respuesta;
        }


        /// </summary>
        /// <param name="datosCotizacion"></param>
        /// <returns></returns>
        [HttpPost]
        public QuotationResponse EditQuotationAll(QuotationRequest datosCotizacion)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            QuotationResponse respuesta = new QuotationResponse();
            var cotizacion = _context.Quotation.SingleOrDefault(c => c.Id == datosCotizacion.Id);
            respuesta.FechaVuelta = "No definido";
            if (datosCotizacion.ViajeId != null)
            {
                var viaje = _context.Trip.SingleOrDefault(c => c.Id == datosCotizacion.ViajeId);
                var transaccion = _context.TransactionControl.SingleOrDefault(c => c.Id == datosCotizacion.Id);
                if (transaccion == null)
                {
                    TransactionControl nuevaTransaccion = new TransactionControl();
                    nuevaTransaccion.QuotationId = datosCotizacion.Id;
                    nuevaTransaccion.TripId = datosCotizacion.ViajeId;
                    nuevaTransaccion.UserId = cotizacion.User.Id;
                    nuevaTransaccion.TravelerUserId = viaje.UserId;
                    _context.TransactionControl.Add(nuevaTransaccion);
                }
                else
                {
                    transaccion.TripId = datosCotizacion.ViajeId;
                    transaccion.TravelerUserId = viaje.UserId;
                }

                cotizacion.TripId = viaje.Id;
                cotizacion.FechaVuelta = viaje.Vuelta.Date;
                           
            }

            if(datosCotizacion.EstadoCotizacion == 3)
            {
                cotizacion.FechaVentaExitosa = DateTime.Now;
            }
            cotizacion.QuotationStatusId = datosCotizacion.EstadoCotizacion;
            cotizacion.TackingNumber = datosCotizacion.TrackingNumber;
            cotizacion.Url = datosCotizacion.URL;
            cotizacion.NombreItem = textInfo.ToTitleCase(datosCotizacion.NombreItem);
            cotizacion.Precio = datosCotizacion.Precio;
            cotizacion.Largo = datosCotizacion.Largo;
            cotizacion.Ancho = datosCotizacion.Ancho;
            cotizacion.Alto = datosCotizacion.Alto;
            cotizacion.CategoryId = datosCotizacion.CategoryId;
            cotizacion.Peso = datosCotizacion.Peso;
            Double precioIsd = datosCotizacion.Precio * 0.05;
            cotizacion.ISD = Math.Round(precioIsd, 2);
            Double volumen = datosCotizacion.Largo * datosCotizacion.Ancho * datosCotizacion.Alto;
            cotizacion.Volumen = volumen;
            Double volumetrico = volumen / 5000;
            cotizacion.PesoVolumetrico = volumetrico;
            cotizacion.VentaExitosa = false;
            switch (datosCotizacion.CategoryId)
            {
                case 1:

                    cotizacion.ComisionViajero = 100;
                    cotizacion.ComisionTraigo = 50;
                    Double precioPesoLaptop = datosCotizacion.Peso * 6;
                    if (precioPesoLaptop > 100)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoLaptop, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;


                case 2:
                    cotizacion.ComisionViajero = 60;
                    cotizacion.ComisionTraigo = 30;
                    Double precioPesoConsola = datosCotizacion.Peso * 6;
                    if (precioPesoConsola > 60)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoConsola, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                case 3:
                    cotizacion.ComisionViajero = 10;
                    cotizacion.ComisionTraigo = 5;
                    Double precioPesoPerfumes = datosCotizacion.Peso * 6;
                    if (precioPesoPerfumes > 10)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoPerfumes, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;


                case 4:
                    cotizacion.ComisionViajero = 50;
                    cotizacion.ComisionTraigo = 20;
                    Double precioPesoMonitorComputadora = datosCotizacion.Peso * 6;
                    if (precioPesoMonitorComputadora > 50)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoMonitorComputadora, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                case 5:
                    cotizacion.ComisionViajero = 50;
                    cotizacion.ComisionTraigo = 25;
                    Double precioPesoCelular = datosCotizacion.Peso * 6;
                    if (precioPesoCelular > 50)
                    {
                        cotizacion.ComisionViajero = Math.Round(precioPesoCelular, 2);
                        cotizacion.ComisionTraigo = Math.Round(cotizacion.ComisionViajero * 0.50, 2);
                    }
                    break;

                default:
                    {

                        cotizacion.ComisionViajero = Math.Round(datosCotizacion.Peso * 6, 2);
                        cotizacion.ComisionTraigo = Math.Round(datosCotizacion.Precio * 0.10, 2);
                    }
                    break;
            }

            cotizacion.ComisionTraigoIva = Math.Round(cotizacion.ComisionTraigo * 1.12, 2);
            _context.SaveChanges();
            if(cotizacion.TripId != null)
            {
                respuesta.FechaVuelta = cotizacion.Trip.User.Nombre + " / " + cotizacion.Trip.Vuelta.ToString("dd/MM/yyyy");
            }
            else
            {
                respuesta.FechaVuelta = "No definido";
            }
            respuesta.ComisionTraigo = Math.Round(cotizacion.ComisionTraigoIva, 2);
            respuesta.ComisionViajero = Math.Round(cotizacion.ComisionViajero, 2);
            respuesta.Peso = cotizacion.Peso;
            respuesta.Precio = cotizacion.Precio;
            respuesta.URL = cotizacion.Url;
            respuesta.Id = cotizacion.Id;
            respuesta.Estado = cotizacion.QuotationStatus.Nombre;
            respuesta.TrackingNumber = cotizacion.TackingNumber;
            respuesta.NombreItem = cotizacion.NombreItem;
            Double costoTraer = Math.Round(cotizacion.ComisionViajero + cotizacion.ComisionTraigoIva, 2);
            Double costoTotal = Math.Round(cotizacion.ComisionViajero + cotizacion.Precio + cotizacion.ComisionTraigoIva, 2);
            respuesta.ComisionTotal = costoTotal;
            respuesta.TextoResultado = "Estimado usuario, su cotización de " + cotizacion.NombreItem + " de peso " + cotizacion.Peso + " lbs. El valor a pagar es " + costoTraer + " USD.";
            return respuesta;
        }

        /// <summary>
        /// Función trae la cotización por id cotizacion
        /// URL: /api/quotation/GetQuotationById
        /// </summary>
        /// <param name="datosCotizacion"></param>
        /// <returns></returns>
        [HttpPost]
        public QuotationResponse GetQuotationById (QuotationRequest datosCotizacion)
        {
            var cotizacion = _context.Quotation.SingleOrDefault(c => c.Id == datosCotizacion.Id);
            QuotationResponse respuesta = new QuotationResponse();
            respuesta.URL = cotizacion.Url;
            respuesta.NombreItem = cotizacion.NombreItem;
            respuesta.Precio = cotizacion.Precio;
            respuesta.IdCategoria = cotizacion.CategoryId;
            respuesta.Peso = cotizacion.Peso;
            respuesta.Largo = cotizacion.Largo;
            respuesta.Ancho = cotizacion.Ancho;
            respuesta.Alto = cotizacion.Alto;
            respuesta.TrackingNumber = cotizacion.TackingNumber;
            respuesta.IdEstado = cotizacion.QuotationStatusId;
            respuesta.IdViaje = cotizacion.TripId;
            return respuesta;
        }

        /// <summary>
        /// Funcion para actualizar el tracking number
        /// URL: /api/quotation/updatetrackingnumber
        /// </summary>
        /// <param name="datosCotizacion">
        /// INT: IdQuotation
        /// STRING: TrackingNumber
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdateTrackingNumber (QuotationRequest datosCotizacion)
        {
            var item = _context.Quotation.SingleOrDefault(c => c.Id == datosCotizacion.Id);
            item.TackingNumber = datosCotizacion.TrackingNumber;
            _context.SaveChanges();
            return true;
        }

        
    }
}
