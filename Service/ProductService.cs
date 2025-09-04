using Actividad_1.Data.Implementation;
using Actividad_1.Data.Interface;
using Actividad_1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Service
{
    public class ProductService
    {
        private IProductRepository _repository;
        public ProductService() 
        {
            _repository = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
            return _repository.GetAll();
        }

        public Product GetProduct(int id)
        {
            return _repository.GetById(id);
        }
    }
}
