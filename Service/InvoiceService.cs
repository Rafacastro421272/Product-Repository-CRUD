using Actividad_1.Data.Implementation;
using Actividad_1.Data.Interface;
using Actividad_1.Domain;
using System.Collections.Generic;

namespace Actividad_1.Service
{
    public class InvoiceService
    {
        private IInvoiceRepository _repository;
        public InvoiceService()
        {
            _repository = new InvoiceRepository();
        }

        public List<Invoice> GetInvoices()
        {
            return _repository.GetALL();
        }

        public Invoice GetInvoice(int id)
        {
            return _repository.GetByID(id);
        }

        public bool SaveInvoice(Invoice invoice)
        {
            return _repository.Save(invoice);
        }

        public bool DeleteInvoice(int id)
        {
            return _repository.DeleteById(id);
        }
    }
}
