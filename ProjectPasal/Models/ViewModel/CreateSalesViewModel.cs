using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPasal.Models.ViewModel
{
    public class CreateSalesViewModel
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Remark { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}