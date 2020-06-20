using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraigoApp.Models
{
    public class Command
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public Double? TotalComanda { get; set; }
        public Double? TotalComTraigo { get; set; }
        public Double? TotalComViajero { get; set; }
        public virtual CommandStatus CommandStatus { get; set; }
        public int CommandStatusId { get; set; }
    }
}