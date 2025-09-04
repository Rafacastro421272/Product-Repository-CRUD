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
            var param = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    Name = "IdArticulo",
                    Value = id
                }
            };

            int rows = DataHelper.GetInstance().ExecuteSPNonQuery("DeleteArticulo", param);
            return rows > 0;
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
                p.UnitPrice = (string)row["pre_unitario"];
                list.Add(p);
            }
            return list;
        }

        public Product GetById(int id)
        {
            // Preparar parámetros
            List<ParameterSP> param = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    Name = "@idarticulo",
                    Value = id
                }
            };

            
            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetArticuloById", param);

            // Si vino un registro, lo mapeamos a Product y lo retornamos
            if (dt != null && dt.Rows.Count > 0)
            {
                Product p = new Product()
                {
                   Id = (int)dt.Rows[0]["id_articulo"],
                   Name = (string)dt.Rows[0]["nombre"],
                   UnitPrice = (string)dt.Rows[0]["pre_unitario"]
                };
                return p;
            }

            return null;
        }

        public bool Save(Product product)
        {
            int rowsAffected = 0;
            var param = new List<ParameterSP>
            {
                new ParameterSP { Name = "@Nombre", Value = product.Name },
                new ParameterSP { Name = "@PrecioUnitario", Value = product.UnitPrice },
                new ParameterSP { Name = "@Activo", Value = product.Status ? 1 : 0 }
            };

            if (product.Id == 0)
            {
                rowsAffected = DataHelper.GetInstance().ExecuteSPNonQuery("InsertArticulo",param);
            }
            else
            {
                param.Add(new ParameterSP { Name = "@IdArticulo", Value = product.Id });
                rowsAffected = DataHelper.GetInstance().ExecuteSPNonQuery("UpdateArticulo",param);
            }
            return rowsAffected > 0;
        }
    }
}
