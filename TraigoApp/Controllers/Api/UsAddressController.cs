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
    public class UsAddressController : ApiController
    {
        private ApplicationDbContext _context;
        public UsAddressController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// /api/usaddress/NewUsAddress
        /// </summary>
        /// <param name="datosUsAddress"></param>
        /// <returns></returns>
        [HttpPost]
        public UsAddressResponse NewUsAddress (UsAddressRequest datosUsAddress)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datosUsAddress.Email);
            UsAddress direccion = new UsAddress();
            direccion.Direccion = datosUsAddress.Direccion;
            direccion.Ciudad = datosUsAddress.Ciudad;
            direccion.Zipcode = datosUsAddress.Zipcode;
            direccion.Nombre = datosUsAddress.Nombre;
            direccion.Telefono = datosUsAddress.Telefono;
            direccion.StateId = datosUsAddress.Estado;
            direccion.UserId = user.Id;
            _context.UsAddress.Add(direccion);
            _context.SaveChanges();
            UsAddressResponse respuesta = new UsAddressResponse();
            return respuesta;
        }

        /// <summary>
        /// /api/usaddress/GetAddressByEmail
        /// </summary>
        /// <param name="datosUser"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<UsAddressResponse> GetAddressByEmail (UsAddressRequest datosUser)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datosUser.Email);
            List<UsAddressResponse> respuesta = new List<UsAddressResponse>();
            IList<UsAddress> UsAddresses = (from i in _context.UsAddress
                                                where i.UserId == user.Id
                                                select i).ToList<UsAddress>();

            foreach (UsAddress dir in UsAddresses)
            {
                UsAddressResponse item = new UsAddressResponse();
                item.Direccion = dir.Direccion;
                item.Ciudad = dir.Ciudad;
                item.Zipcode = dir.Zipcode;
                item.Nombre = dir.Nombre;
                item.Telefono = dir.Telefono;
                item.EstadoNombre = dir.State.Nombre;
                item.DireccionId = dir.Id;
                respuesta.Add(item);
            }

            return respuesta;
        }
    }
}
