using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Domain
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool Status { get; set; }
        public List<InvoiceDetail> Details { get; set; }


        public override string ToString()
        {
            return  $@"Invoice Number: {Id} - Invoice Date: {Date} - Invoice Payment Method: {PaymentMethod.Name}";
        }
    }
}
