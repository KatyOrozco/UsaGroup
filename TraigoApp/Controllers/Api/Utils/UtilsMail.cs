﻿using SendGrid;
using System;
using System.Net.Mail;
using System.Web.Configuration;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace TraigoApp.Controllers.Api.Utils
{
    public class UtilsMail
    {
        
        public async Task<bool> enviarBienvenida(string mail, string nombre)
        {
            string apiKey = WebConfigurationManager.AppSettings["ApiSendgrid"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@usa.group", "USA Group"),
                Subject = "Bienvenido a USA Group",
                PlainTextContent = "Bienvenido a la primera comunidad colaborativa que conecta usuarios con viajeros, para que de este modo puedan obtener sus compras directamente en Ecuador de forma rápida y segura.",
                HtmlContent = "<!DOCTYPE html><html><body><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse;height:100%;margin:0;padding:0;width:100%'> <tbody><tr> <td align='center' valign='top' id='cell' style='height:100%;margin:0;padding:0;width:100%'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='top' style='background:#f7f7f7 none no-repeat center/cover;background-color:#f7f7f7;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:54px;padding-bottom:54px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding:9px' > <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:9px;padding-left:9px;padding-top:0;padding-bottom:0;text-align:center'> <a href='http://www.usa.group/' title='' target='_blank'> <img align='center' alt='' src='http://usa.group/Content/Img/Index/LogoA.png' width='276' style='max-width:276px;padding-bottom:0;display:inline!important;vertical-align:bottom;border:0;height:auto;outline:none;text-decoration:none'> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> <tr> <td align='center' valign='top' style='background:#ffffff none no-repeat center/cover;background-color:#ffffff;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:27px;padding-bottom:63px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding-top:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%;min-width:100%;border-collapse:collapse' width='100%'> <tbody><tr> <td valign='top' style='padding-top:0;padding-right:18px;padding-bottom:9px;padding-left:18px;word-break:break-word;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'> <h2 style='text-align:center;display:block;margin:0;padding:0;color:#222222;font-family:Helvetica;font-size:28px;font-style:normal;font-weight:bold;line-height:150%;letter-spacing:normal'><strong><span style='font-size:19px'>¡Hola "+nombre+"!</span></strong></h2><div style='text-align:center'><strong><span style='color:#000000'><span style='font-size:20px'>Te estábamos esperando</span></span></strong></div><p style='font-size:18px!important;margin:10px 0;padding:0;color:#808080;font-family:Helvetica;line-height:150%;text-align:left'>Bienvenido a la primera comunidad colaborativa que conecta usuarios de la tienda AMAZON con viajeros, para que de este modo puedan obtener sus compras directamente en Ecuador de forma rápida y segura.</p><p style='font-size:18px!important;margin:10px 0;padding:0;color:#808080;font-family:Helvetica;line-height:150%;text-align:left'>No esperes más!!!</p> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px 18px 0px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td style='padding-top:0;padding-right:18px;padding-bottom:18px;padding-left:18px' valign='top' align='center'> <table border='0' cellpadding='0' cellspacing='0' style='border-collapse:separate!important;border-radius:3px;background-color:#ffa901'> <tbody> <tr> <td align='center' valign='middle' style='font-family:Helvetica;font-size:18px;padding:18px'> <a title='Cotiza Gratis' href='http://www.usa.group/dashboard' style='font-weight:bold;letter-spacing:-0.5px;line-height:100%;text-align:center;text-decoration:none;color:#ffffff;display:block' target='_blank'><img align='center' alt='' src='http://usa.group/Content/Img/mail/calculator.png' width='16' style='max-width:16px;padding-bottom:0;display:inline!important;vertical-align:bottom;border:0;height:auto;outline:none;text-decoration:none'> Cotiza Gratis</a> </td> </tr> </tbody> </table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding:9px'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:9px;padding-left:9px;padding-top:0;padding-bottom:0;text-align:center'> <a href='http://www.usa.group' title='' target='_blank'> <img align='center' alt='USA Group' src='http://usa.group/Content/Img/Index/3.jpg' width='532' style='max-width:532px;padding-bottom:0;display:inline!important;vertical-align:bottom;border:0;height:auto;outline:none;text-decoration:none'> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td style='padding-top:9px;padding-left:18px;padding-bottom:9px;padding-right:18px'> <table border='0' cellspacing='0' width='100%' style='min-width:100%!important;background-color:#f7f7f7;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding:18px;text-align:center;word-break:break-word;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%'> <div style='text-align:left'>USA Group protege tu privacidad. Lee nuestras&nbsp;<a href='http://usa.group/faq' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Condiciones de uso</a>.</div><p align='left' style='margin:10px 0;padding:0;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'>No leemos las respuestas enviadas a este correo electrónico. Si tienes consultas, con gusto te ayudaremos a través de nuestra Fanpage de <a href='https://www.facebook.com' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Facebook</a> o nuestro <a href='https://api.whatsapp.com/send?phone=593961127346' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Whatsapp</a>.</p><p align='left' style='margin:10px 0;padding:0;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'>Copyright ©2017 USA Group LLC. Todos los derechos reservados. Las marcas comerciales y las marcas mencionadas son propiedad de sus respectivos titulares. USA Group&nbsp;y el logotipo de USA Group son marcas comerciales de USA Group LLC, 2035 Sunset Lake Rd, Newark, DE 19702.</p> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> <tr> <td align='center' valign='top' style='background:#333333 none no-repeat center/cover;background-color:#333333;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:45px;padding-bottom:63px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td align='center' valign='top' style='padding:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td align='center' style='padding-left:9px;padding-right:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'> <table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='top'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='https://www.facebook.com/ target='_blank'><img src='http://usa.group/Content/Img/mail/facebook.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.twitter.com/' target='_blank'><img src='http://usa.group/Content/Img/mail/twitter.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.instagram.com/' target='_blank'><img src='http://usa.group/Content/Img/mail/instagram.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.usa.group' target='_blank'><img src='http://usa.group/Content/Img/mail/link.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:0;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='https://api.whatsapp.com/send?phone=593961127346' target='_blank'><img src='http://usa.group/Content/Img/mail/whatsapp.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr></tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-top:2px solid #505050;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table></body></html> ",
            };
            msg.AddTo(new EmailAddress(mail));
            var response = await client.SendEmailAsync(msg);
            return true;
        }

        public async Task<bool> resetPassword(string mail, string nombre)
        {
            string apiKey = WebConfigurationManager.AppSettings["ApiSendgrid"];
            var client = new SendGridClient(apiKey);
            var url = "http://www.usa.group/user/changepassword?user="+mail;
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@usa.group", "USA Group"),
                Subject = "Recupera tu cuenta de USA Group",
                PlainTextContent = "Ingresa al siguiente enlace para recuperar tu cuenta: "+url,
                HtmlContent = "<!DOCTYPE html><html><body><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' style='border-collapse:collapse;height:100%;margin:0;padding:0;width:100%'> <tbody><tr> <td align='center' valign='top' style='height:100%;margin:0;padding:0;width:100%'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='top' style='background:#f7f7f7 none no-repeat center/cover;background-color:#f7f7f7;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:54px;padding-bottom:54px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding:9px'> <table align='left' width='100%' border='0' cellpadding='0' cellspacing='0' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:9px;padding-left:9px;padding-top:0;padding-bottom:0;text-align:center'> <a href='http://www.usa.group/' title='' target='_blank'> <img align='center' alt='' src='http://usa.group/Content/Img/Index/LogoA.png' width='276' style='max-width:276px;padding-bottom:0;display:inline!important;vertical-align:bottom;border:0;height:auto;outline:none;text-decoration:none'> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> <tr> <td align='center' valign='top' style='background:#ffffff none no-repeat center/cover;background-color:#ffffff;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:27px;padding-bottom:63px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top' style='padding-top:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='max-width:100%;min-width:100%;border-collapse:collapse' width='100%'> <tbody><tr> <td valign='top' style='padding-top:0;padding-right:18px;padding-bottom:9px;padding-left:18px;word-break:break-word;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'> <h2 style='display:block;margin:0;padding:0;color:#222222;font-family:Helvetica;font-size:28px;font-style:normal;font-weight:bold;line-height:150%;letter-spacing:normal;text-align:left'><span style='font-size:19px'>Queremos ayudarte a restablecer tu&nbsp;contraseña.</span></h2><p style='font-size:18px!important;margin:10px 0;padding:0;color:#808080;font-family:Helvetica;line-height:150%;text-align:left'>Hola "+nombre+", :<br><br>Simplemente haz clic en el botón o copia y pega este enlace en tu navegador para restablecer tu contraseña. (Es válido por 24 horas solamente).</p><p style='font-size:18px!important;margin:10px 0;padding:0;color:#808080;font-family:Helvetica;line-height:150%;text-align:left'><a href='" + url + "' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>https://www.usa.group/<wbr>password_resets/IT6gCPyIsMiME-xK7TeedQ/edit<wbr>ad</a></p> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px 18px 0px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td style='padding-top:0;padding-right:18px;padding-bottom:18px;padding-left:18px' valign='top' align='center'> <table border='0' cellpadding='0' cellspacing='0' style='border-collapse:separate!important;border-radius:3px;background-color:#00add8'> <tbody> <tr> <td align='center' valign='middle' style='font-family:Helvetica;font-size:18px;padding:18px'> <a title='Restablece tu contraseña' href='" + url + "' style='font-weight:bold;letter-spacing:-0.5px;line-height:100%;text-align:center;text-decoration:none;color:#ffffff;display:block' target='_blank'>Restablece tu contraseña</a> </td> </tr> </tbody> </table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td valign='top'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td style='padding-top:9px;padding-left:18px;padding-bottom:9px;padding-right:18px'> <table border='0' cellspacing='0' width='100%' style='min-width:100%!important;background-color:#f7f7f7;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding:18px;text-align:center;word-break:break-word;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%'> <div style='text-align:left'>USA Group protege tu privacidad. Lee nuestras&nbsp;<a href='http://usa.group/faq' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Condiciones de uso</a>.</div><p align='left' style='margin:10px 0;padding:0;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'>No leemos las respuestas enviadas a este correo electrónico. Si tienes consultas, con gusto te ayudaremos a través de nuestra Fanpage de <a href='https://www.facebook.com/' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Facebook</a> o nuestro <a href='https://api.whatsapp.com/send?phone=593961127346' style='color:#00add8;font-weight:normal;text-decoration:underline' target='_blank'>Whatsapp</a>.</p><p align='left' style='margin:10px 0;padding:0;color:#808080;font-family:Helvetica;font-size:16px;line-height:150%;text-align:left'>Copyright ©2017 USA Group LLC. Todos los derechos reservados. Las marcas comerciales y las marcas mencionadas son propiedad de sus respectivos titulares. USA Group&nbsp;y el logotipo de USA Group son marcas comerciales de USA Group LLC, 2035 Sunset Lake Rd, Newark, DE 19702.</p> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> <tr> <td align='center' valign='top' style='background:#333333 none no-repeat center/cover;background-color:#333333;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:45px;padding-bottom:63px'> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse;max-width:600px!important'> <tbody><tr> <td valign='top' style='background:transparent none no-repeat center/cover;background-color:transparent;background-image:none;background-repeat:no-repeat;background-position:center;background-size:cover;border-top:0;border-bottom:0;padding-top:0;padding-bottom:0'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody> <tr> <td align='center' valign='top' style='padding:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td align='center' style='padding-left:9px;padding-right:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse'> <tbody><tr> <td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'> <table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='top'> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='https://www.facebook.com/' target='_blank'><img src='http://usa.group/Content/Img/mail/facebook.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.twitter.com/' target='_blank'><img src='http://usa.group/Content/Img/mail/twitter.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.instagram.com/' target='_blank'><img src='http://usa.group/Content/Img/mail/instagram.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:10px;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='http://www.usa.group' target='_blank'><img src='http://usa.group/Content/Img/mail/link.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'> <tbody><tr> <td valign='top' style='padding-right:0;padding-bottom:9px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'> <tbody><tr> <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'> <table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'> <tbody><tr> <td align='center' valign='middle' width='24'> <a href='https://api.whatsapp.com/send?phone=593961127346' target='_blank'><img src='http://usa.group/Content/Img/mail/whatsapp.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24'></a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr></tbody></table> </td> </tr> </tbody></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse;table-layout:fixed!important'> <tbody> <tr> <td style='min-width:100%;padding:18px'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-top:2px solid #505050;border-collapse:collapse'> <tbody><tr> <td> <span></span> </td> </tr> </tbody></table> </td> </tr> </tbody></table></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table></body></html> ",
            };
            msg.AddTo(new EmailAddress(mail));
            var response = await client.SendEmailAsync(msg);
            return true;
        }

        public async Task<bool> emailGenerico(string email, string asunto, string texto, string html)
        {
            string apiKey = WebConfigurationManager.AppSettings["ApiSendgrid"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@usa.group", "USA Group"),
                Subject = asunto,
                PlainTextContent = texto,
                HtmlContent = html,
            };
            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg);
            return true;
        }

    }
}