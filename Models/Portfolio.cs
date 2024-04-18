using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Portfolio
    {
        public int Id {get; set;}
        public int StockId { get; set; }
        public int AppUserId { get; set; }

        public Stock? Stock { get; set; }

        public AppUser? User {get; set;}
    }
}