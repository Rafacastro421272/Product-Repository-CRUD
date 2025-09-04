using Actividad_1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Interface
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetALL();

        Invoice GetByID(int id);

        bool Save(Invoice invoice);

        bool DeleteById(int id);

    }
}
