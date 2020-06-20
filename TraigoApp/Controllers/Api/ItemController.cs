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
    public class ItemController : ApiController
    {
        private ApplicationDbContext _context;
        public ItemController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// ingresa items a una comanda creada
        /// /api/item/newItemInCommand
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public ItemResponse newItemInCommand(ItemRequest datos)
        {
            Item item = new Item();
            item.CommandId = datos.IdCommand;
            item.QuotationId = datos.IdQuotation;
            _context.Item.Add(item);
            _context.SaveChanges();
            ItemResponse respuesta = new ItemResponse();
            respuesta.entra = true;
            return respuesta;
        }

        /// <summary>
        /// delete an item
        /// /api/item/deleteItem
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public bool deteleItem(ItemRequest datos)
        {
            var item = _context.Item.SingleOrDefault(c => c.Id == datos.IdItem);
            _context.Item.Remove(item);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// /api/item/GetItemsByCommand
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ItemResponse> GetItemsByCommand(ItemRequest datos)
        {
            var command = _context.Command.SingleOrDefault(c => c.Id == datos.IdCommand);

            List<ItemResponse> respuesta = new List<ItemResponse>();
            IList<Item> itemsByCommand = (from i in _context.Item
                                                where i.CommandId == datos.IdCommand
                                                orderby i.Id descending
                                                select i).ToList<Item>();

            foreach (Item item in itemsByCommand)
            {
                ItemResponse itemDto = new ItemResponse();
                itemDto.URL = item.Quotation.Url;
                itemDto.ComisionTraigoIva = Math.Round(item.Quotation.ComisionTraigoIva,2);
                itemDto.ComisionViajero = Math.Round(item.Quotation.ComisionViajero, 2);
                itemDto.Precio = Math.Round(item.Quotation.Precio, 2);
                itemDto.NombreItem = item.Quotation.NombreItem;
                itemDto.Peso = item.Quotation.Peso;
                itemDto.Id = item.Id;
                itemDto.IdCotizacion = item.QuotationId;
                itemDto.ComisionTotal = Math.Round(item.Quotation.ComisionViajero + item.Quotation.ComisionTraigoIva, 2);
                respuesta.Add(itemDto);
            }
            return respuesta;
        }
    }
}