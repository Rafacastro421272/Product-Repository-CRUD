using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Domain
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int InvoiceId  { get; set; }
        public Product Product { get; set; }


        public override string ToString()
        {
            return $@"Detail Id: {Id} - Product: {Product.Name} - Unit Price: ${Product.UnitPrice} - Quantity: {Quantity}";
        }

    }
}
