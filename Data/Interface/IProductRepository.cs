using Actividad_1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product GetById(int id);

        bool Save(Product product);

        bool DeleteById(int id);
    }
}
