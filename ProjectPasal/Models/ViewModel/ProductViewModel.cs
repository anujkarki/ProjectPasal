using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPasal.Models.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Nullable<double> Price { get; set; }
    }
}