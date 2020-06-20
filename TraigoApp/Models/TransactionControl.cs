using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class TransactionControl
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public int TravelerUserId { get; set; }
        public virtual Quotation Quotation { get; set; }
        public int QuotationId { get; set; }
        public virtual Trip Trip { get; set; }
        public int? TripId { get; set; }
    }
}