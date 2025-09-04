using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }

        public override string ToString()
        {
            return $@"Product Id: {Id} - Product Name: {Name}   - Unit Price: $ {UnitPrice}";
        }
    }
}
