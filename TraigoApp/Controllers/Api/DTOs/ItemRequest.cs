using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Controllers.Api.DTOs
{
    public class ItemRequest
    {
        public int IdQuotation { get; set; }
        public int IdCommand { get; set; }
        public int IdItem { get; set; }
    }
}