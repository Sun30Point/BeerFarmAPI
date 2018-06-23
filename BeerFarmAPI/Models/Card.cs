using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFarmAPI.Models
{
    public class Card
    {
        public int ID { get; set; }

        public string Guid { get; set; }

        public string Total { get; set; }

        public int IsActive { get; set; }
    }
}