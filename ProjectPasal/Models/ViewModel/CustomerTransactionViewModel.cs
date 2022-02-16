using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPasal.Models.ViewModel
{
    public class CustomerTransactionViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<double> Balance { get; set; }
        public string Description { get; set; }
    }
}