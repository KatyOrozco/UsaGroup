using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public virtual Quotation Quotation { get; set; }
        public int QuotationId { get; set; }
        public virtual Command Command { get; set; }
        public int CommandId { get; set; }
    }
}