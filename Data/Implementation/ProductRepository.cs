using Actividad_1.Data.Interface;
using Actividad_1.Data.Utils;
using Actividad_1.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            List<Product> list = new List<Product>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetAllArticulo");

            foreach(DataRow row in dt.Rows)
            {
                Product p = new Product();
                p.Id = Convert.ToInt32(row["id_articulo"]);
                p.Name = (string)row["nombre"];
                p.UnitPrice = (decimal)row["pre_unitario"];
                list.Add(p);
            }
            return list;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
