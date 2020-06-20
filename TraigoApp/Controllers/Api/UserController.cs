using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraigoApp.Controllers.Api.DTOs;
using TraigoApp.Controllers.Api.Utils;
using TraigoApp.Models;

namespace TraigoApp.Controllers.Api
{
    public class UserController : ApiController
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        /// </summary>
        /// <param name="datosUser"></param>
        /// <returns></returns>
        [HttpPost]
        public UserResponse Login (UserRequest datosUser)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            IList<User> userDb = (from i in _context.User
                                      where i.Email == datosUser.Email && i.Password == datosUser.Clave
                                      select i).ToList<User>();
            UserResponse respuesta = new UserResponse();
            if (userDb.Count == 1)
            {
                respuesta.Id = userDb[0].Id;
                respuesta.Nombre = userDb[0].Nombre;
                respuesta.Email = userDb[0].Email;
                respuesta.Ingresa = true;
            }
            else
            {
                respuesta.Ingresa = false;
            }
           
            return respuesta;
        }

        /// <summary>
        /// Create User
        /// /api/user/createuser
        /// </summary>
        /// <param name="usuarioRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserResponse> CreateUser (UserRequest datosUser)
        {
            //comprobar si el usuario ya existe
            //var userExiste = _context.User.SingleOrDefault(c => c.Email == datosUser.Email);
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            IList<User> userExiste = (from i in _context.User
                                                where i.Email == datosUser.Email
                                                select i).ToList<User>();

            UserResponse respuesta = new UserResponse();

            //if usuario.count 
            if (userExiste.Count == 0)
            {
                User usuarioNuevo = new User();
                usuarioNuevo.Nombre = datosUser.Nombre;
                usuarioNuevo.Email = datosUser.Email.ToLower();
                usuarioNuevo.CelularEc = datosUser.CelularEc;
                usuarioNuevo.Cedula = datosUser.Cedula;
                usuarioNuevo.CityId = datosUser.IdCiudad;
                if (datosUser.Clave != "" && datosUser.Clave != null)
                {
                    usuarioNuevo.Password = datosUser.Clave;
                }
                _context.User.Add(usuarioNuevo);
                _context.SaveChanges();
                UtilsMail utils = new UtilsMail();
                await utils.enviarBienvenida(usuarioNuevo.Email, usuarioNuevo.Nombre);
                respuesta.Id = usuarioNuevo.Id;
                respuesta.Email = usuarioNuevo.Email;
                respuesta.Nombre = usuarioNuevo.Nombre;
                respuesta.Cedula = usuarioNuevo.Cedula;
                respuesta.CelularEc = usuarioNuevo.CelularEc;
                respuesta.CiudadId = usuarioNuevo.CityId;
                respuesta.NewUser = true;
                //respuesta.Ciudad = usuarioNuevo.City.Nombre;
            }
            else
            {
                respuesta.NewUser = false;
            }
            return respuesta;
        }

        /// <summary>
        /// /api/user/EditUser
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public UserResponse EditUser(UserRequest datosUsuario)
        {
            var user = _context.User.SingleOrDefault(c => c.Cedula == datosUsuario.Cedula);
            user.Email = datosUsuario.Email;
          
            user.CityId = datosUsuario.IdCiudad;
            user.CelularEc = datosUsuario.CelularEc;
            _context.SaveChanges();
            UserResponse respuesta = new UserResponse();
            respuesta.Id = user.Id;
            respuesta.Nombre = user.Nombre;
            respuesta.Email = user.Email;
            respuesta.CelularEc = user.CelularEc;
            respuesta.Ciudad = user.City.Nombre;
            respuesta.CiudadId = user.CityId;
            return respuesta;
        }


        /// <summary>
        /// /api/user/getuserbyemail
        /// </summary>
        /// <param name="datosUser"></param>
        /// <returns></returns>
        [HttpPost]
        public UserResponse GetUserEmail (UserRequest datosUser)
        {
            UserResponse respuesta = new UserResponse();
            var user = _context.User.SingleOrDefault(c => c.Email == datosUser.Email);

            if(user != null)
            {
                respuesta.Id = user.Id;
                respuesta.Nombre = user.Nombre;
                respuesta.Email = user.Email;
                respuesta.Cedula = user.Cedula;
                respuesta.CelularEc = user.CelularEc;
                respuesta.CiudadId = user.CityId;
                respuesta.Ciudad = user.City.Nombre;
            }
            else
            {
                respuesta.NewUser = true;
            }
            return respuesta;
        }

        [HttpPost]
        public UserResponse AuthAdmin (UserRequest datosLogin)
        {
            var usuario = datosLogin.Email;
            var clave = datosLogin.Clave;
            UserResponse respuesta = new UserResponse();
            if (clave == "Katy1996*.*" && usuario == "info@usa.group")
            {
                respuesta.NewUser = true;
            }
            else
            {
                respuesta.NewUser = false;
            }
            return respuesta;
        }

        /// <summary>
        /// reset password send email 
        /// </summary>
        /// <param name="datosLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UserResponse> resetPassword(UserRequest datosReset)
        {
            IList<User> userExiste = (from i in _context.User
                                      where i.Email == datosReset.Email
                                      select i).ToList<User>();
            UserResponse respuesta = new UserResponse();
            if (userExiste.Count == 1)
            {
                UtilsMail utils = new UtilsMail();
                await utils.resetPassword(userExiste[0].Email, userExiste[0].Nombre);
                respuesta.Ingresa = true;
            }
            else
            {
                respuesta.Ingresa = false;
            }
            return respuesta;
        }

        /// <summary>
        /// save new password in database
        /// </summary>
        /// <param name="datosLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public bool changePassword(UserRequest datosLogin)
        {
            var usuario = _context.User.SingleOrDefault(c => c.Email == datosLogin.Email);
            usuario.Password = datosLogin.Clave;
            _context.SaveChanges();
            return true;
        }
    }
}
