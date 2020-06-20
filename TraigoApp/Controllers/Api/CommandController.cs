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
    public class CommandController : ApiController
    {
        private ApplicationDbContext _context;
        public CommandController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// create new commanda if does not exist.
        /// /api/command/newCommand
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public CommandResponse newCommand(CommandRequest datos)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datos.Email);
            IList<Command> comandas = (from i in _context.Command
                                       where i.UserId == user.Id && i.CommandStatusId == 3
                                       select i).ToList<Command>();
            CommandResponse respuesta = new CommandResponse();
            respuesta.Respuesta = false;
            if (comandas.Count() == 0)
            {
                Command comanda = new Command();
                comanda.UserId = user.Id;
                comanda.CommandStatusId = 3;
                _context.Command.Add(comanda);
                _context.SaveChanges();
                respuesta.Respuesta = true;
                respuesta.IdCommand = comanda.Id;
            }
            return respuesta;
        }


        /// <summary>
        /// /api/command/updatecommand
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public CommandResponse updateCommand (CommandRequest datos)
        {
            var idComanda = _context.Command.SingleOrDefault(c => c.Id == datos.IdComanda);
            idComanda.CommandStatusId = datos.Estado;
            idComanda.TotalComanda = datos.ValorComanda;
            idComanda.TotalComTraigo = datos.ValorTraigo;
            idComanda.TotalComViajero = datos.ValorViajero;
            _context.SaveChanges();
            CommandResponse respuesta = new CommandResponse();
            respuesta.Respuesta = true;
            return respuesta;
        }

        /// <summary>
        /// compruebo si hay commands
        /// /api/command/getcommand
        /// Estado 1 = en cotizacion
        /// Estado 2 = en revisión
        /// Estado 3 = Esperando pago
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public CommandResponse getCommand (CommandRequest datos)
        {
            var user = _context.User.SingleOrDefault(c => c.Email == datos.Email);
            IList<Command> comandas = (from i in _context.Command
                                       where i.UserId == user.Id && i.CommandStatusId == 1
                                       select i).ToList<Command>();
            CommandResponse respuesta = new CommandResponse();
            respuesta.Respuesta = true;
            if (comandas.Count() > 0)
            {
                respuesta.Respuesta = false;
                respuesta.IdCommand = comandas[0].Id;
            }
            return respuesta;
        }

        /// <summary>
        /// get command panel for Kate
        /// /api/command/getcommandpanel
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<CommandResponse> getCommandPanel(CommandRequest datos)
        {
            List<CommandResponse> respuesta = new List<CommandResponse>();

            IList<Command> comandas = (from i in _context.Command
                                       where i.CommandStatusId == 2
                                       select i).ToList<Command>();
            foreach (var item in comandas)
            {
                CommandResponse comanda = new CommandResponse();
                comanda.IdCommand = item.Id;
                comanda.ValorComanda = item.TotalComanda;
                comanda.Email = item.User.Email;
                comanda.Nombre = item.User.Nombre;
                comanda.Celular = item.User.CelularEc;
                respuesta.Add(comanda);
            }
            return respuesta;
        }

        /// <summary>
        /// get command panel for Kate
        /// /api/command/getCommandsVerify
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<CommandResponse> getCommandsVerify(CommandRequest datos)
        {
            List<CommandResponse> respuesta = new List<CommandResponse>();

            IList<Command> comandas = (from i in _context.Command
                                       where i.CommandStatusId == 3
                                       select i).ToList<Command>();
            foreach (var item in comandas)
            {
                CommandResponse comanda = new CommandResponse();
                comanda.IdCommand = item.Id;
                comanda.ValorComanda = item.TotalComanda;
                comanda.Email = item.User.Email;
                comanda.Nombre = item.User.Nombre;
                comanda.Celular = item.User.CelularEc;
                respuesta.Add(comanda);
            }
            return respuesta;
        }

        /// <summary>
        /// get command panel for Kate
        /// /api/command/getCommandsPaid
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<CommandResponse> getCommandsPaid(CommandRequest datos)
        {
            List<CommandResponse> respuesta = new List<CommandResponse>();

            IList<Command> comandas = (from i in _context.Command
                                       where i.CommandStatusId == 4
                                       select i).ToList<Command>();
            foreach (var item in comandas)
            {
                CommandResponse comanda = new CommandResponse();
                comanda.IdCommand = item.Id;
                comanda.ValorComanda = item.TotalComanda;
                comanda.Email = item.User.Email;
                comanda.Nombre = item.User.Nombre;
                comanda.Celular = item.User.CelularEc;
                respuesta.Add(comanda);
            }
            return respuesta;
        }

        [HttpPost]
        public CommandResponse getCommandById(CommandRequest datos) {
            CommandResponse respuesta = new CommandResponse();
            var comanda = _context.Command.SingleOrDefault(c => c.Id == datos.IdComanda);
            respuesta.Nombre = comanda.User.Nombre;
            respuesta.Email = comanda.User.Email;
            respuesta.Celular = comanda.User.CelularEc;
            respuesta.ValorComanda = comanda.TotalComanda;
            respuesta.ValorTraigo = comanda.TotalComTraigo;
            respuesta.ValorViajero = comanda.TotalComViajero;
            return respuesta;
        }


        [HttpPost]
        public async Task<bool> solicitarPago(CommandRequest datos)
        {

            IList<Item> items = (from i in _context.Item
                                 where i.CommandId == datos.IdComanda
                                 select i).ToList<Item>();
            var comanda = _context.Command.SingleOrDefault(c => c.Id == datos.IdComanda);
            var nombre = items[0].Quotation.User.Nombre;
            var email = items[0].Quotation.User.Email;
            var asunto = "Tu cotización en USA Group ha sido aprobada";
            var html = "<!DOCTYPE html><html><body><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse;height:100%;margin:0;padding:0;width:100%'> <tbody><tr> <td align='center' valign='top' id='cell' style='height:100%;margin:0;padding:0;width:100%'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='top' style='background:#f7f7f7 none no-repeat center/cover;background-color:#f7f7f7;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:54px;padding-bottom:54px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding:9px' > <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:9px;padding-left:9px;padding-top:0;padding-bottom:0;text-align:center'> <a href='http://www.usa.group/' title='' target='_blank'> <img align='center' alt='' src='http://usa.group/Content/Img/Index/LogoA.png' width='276' style='max-width:276px;padding-bottom:0;display:inline!important;vertical-align:bottom;border:0;height:auto;outline:none;text-decoration:none'> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> <tr> <td align='center' valign='top' style='background:#ffffff none no-repeat center/cover;background-color:#ffffff;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:27px;padding-bottom:63px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding-top:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%;min-width:100%;border-collapse:collapse' width='100%'> <tbody><tr> <td valign='top' style='padding-top:0;padding-right:18px;padding-bottom:9px;padding-left:18px;word-break:break-word;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'> <h2 style='text-align:center;display:block;margin:0;padding:0;color:#222222;font-family:Helvetica;font-size:28px;font-style:normal;font-weight:bold;line-height:150%;letter-spacing:normal'><strong><span style='font-size:19px'>Estimado, " + nombre + "</span></strong></h2><div style='text-align:center'><strong><span style='color:#000000'><span style='font-size:20px'>Hemos generado una solicitud de pago:</span></span></strong></div><p style='font-size:18px!important;margin:10px 0;padding:0;color:#808080;font-family:Helvetica;line-height:150%;text-align:left'> Ahora solo debes pagar el valor de comisión de USA Group para que los datos del viajero en USA, a donde debes realizar el envío, sean liberados.<br><br> <b>Comisión USA Group:</b> " + comanda.TotalComTraigo + " (a pagar este momento)<br> <b>Comisión viajero:</b> " + comanda.TotalComViajero + " (a pagar contra entrega)<br> <b>TOTAL:</b> " + comanda.TotalComanda + "<br><br> <b>DEPÓSITO o TRANSFERENCIA:</b><br> <b>A nombre de:</b> USA Group<br> <b>RUC:</b> 0405533255<br> <b>Banco de Pichincha Cta. Corriente :</b> 555555 <br><br> Una vez realizada la transacción responde cuanto antes a este correo con el comprobante.<br><br> <b>IMPORTANTE:</b> Todos los viajeros son certificados por USA Group LLC, el pago de la comisión de viajero debe realizarse directamente al mismo y posterior entrega de tus productos.</p> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> <tr> <td valign='top' style='padding:9px'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:9px;padding-left:9px;padding-top:0;padding-bottom:0;text-align:center'> <div style='text-align:center'><strong><span style='color:#000000'><span style='font-size:20px'>TU COTIZACIÓN:</span></span></strong></div>";

            html += "<table border='1'> <thead> <tr> <th>Producto</th> <th>Comisión USA Group</th> <th>Comisión Viajero</th> </tr> </thead>";
            html += "<tbody><tr>";
            foreach (var item in items)
            {
                html += "<tr><td>" + item.Quotation.NombreItem + "</td><td>" + item.Quotation.ComisionTraigoIva + "</td><td>" + item.Quotation.ComisionViajero + "</td></tr>";
            }
            html += "</tr></tbody>";
            html += "<tbody > <tr> <td> <strong>Suma de comisiones</strong> </td> <td>" + comanda.TotalComTraigo + " USD</td> <td>" + comanda.TotalComViajero + " USD</td> </tr><tr><td>Total</td><td></td><td><strong>" + comanda.TotalComanda + " USD</strong></td></tr> </tbody>";
            html += "<table border='1'> <thead> <tr> <th>Producto</th> <th>Comisión USA Group</th> <th>Comisión Viajero</th> </tr> </thead>";

            var texto = " Debes realizar el pago  de " + comanda.TotalComTraigo + " USD. EL número de cuenta es: 270000042, tipo corriente. A nombre de USA Group";
             UtilsMail utils = new UtilsMail();
            await utils.emailGenerico(email, asunto, texto, html);
            return true;
        }

    }
}
