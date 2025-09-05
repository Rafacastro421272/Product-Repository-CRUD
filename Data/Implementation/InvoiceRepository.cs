using Actividad_1.Data.Interface;
using Actividad_1.Data.Utils;
using Actividad_1.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Implementation
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public bool DeleteById(int id)
        {
            var param = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    Name = "@NroFactura",
                    Value = id
                }
            };

            int rows = DataHelper.GetInstance().ExecuteSPNonQuery("DeleteFactura", param);
            return rows > 0;
        }

        public List<Invoice> GetALL()
        {
            var invoices = new List<Invoice>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetAllFactura");

            foreach (DataRow row in dt.Rows)
            {
                Invoice i = new Invoice();
                i.Id = (int)row["nroFactura"];
                i.Date = (DateTime)row["fecha"];
                i.Customer = (string)row["cliente"];    
                i.PaymentMethod = new PaymentMethod();
                i.PaymentMethod.Id = (int)row["id_formaPago"];
                i.PaymentMethod.Name = (string)row["nombre"];
                invoices.Add(i);
            }

            return invoices;

        }

        public Invoice GetByID(int id)
        {
            List<ParameterSP> param = new List<ParameterSP>() { new ParameterSP() { Name = "@NroFactura", Value = id } };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetFacturaById", param);

            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                Invoice i = new Invoice();
                i.Id = (int)dt.Rows[0]["nroFactura"];
                i.Date = (DateTime)dt.Rows[0]["fecha"];
                i.Customer = (string)dt.Rows[0]["cliente"];
                i.PaymentMethod = new PaymentMethod();
                i.PaymentMethod.Id = (int)dt.Rows[0]["id_formaPago"];
                i.PaymentMethod.Name = (string)dt.Rows[0]["nombre"];

                i.Details = new List<InvoiceDetail>();

                var dtDetails = DataHelper.GetInstance().ExecuteSPQuery("GetDetalleFacturaByFactura", param);
                foreach (DataRow dr in dtDetails.Rows)
                {
                    InvoiceDetail detail = new InvoiceDetail();
                    detail.Id = (int)dr["id_detalleFactura"];
                    detail.Product = new Product();
                    detail.Product.Id = (int)dr["id_articulo"];
                    detail.Product.Name = (string)dr["Articulo"];
                    detail.Quantity = (int)dr["cantidad"];
                    detail.Product.UnitPrice = (decimal)dr["pre_unitario"];
                    detail.InvoiceId = (int)dr["nroFactura"];

                    i.Details.Add(detail);
                }
                return i;
            }
            return null;
        }


        public bool Save(Invoice invoice)
        {
            bool result = true;

            using (var uow = new UnitOfWork())
            {
                try
                {
                    SqlCommand cmd;
                    int invoiceId = invoice.Id;

                    if (invoice.Id == 0)
                    {
                        cmd = new SqlCommand("InsertFactura", uow.Connection, uow.Transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdFormaPago", invoice.PaymentMethod.Id);
                        cmd.Parameters.AddWithValue("@Cliente", invoice.Customer);
                        cmd.Parameters.AddWithValue("@Activo", invoice.Status);

                        // Si el SP requiere la fecha, descomentar la siguiente línea:
                        // cmd.Parameters.AddWithValue("@Fecha", invoice.Date);

                        SqlParameter param = new SqlParameter("@NroFactura", SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();

                        invoiceId = (int)param.Value;
                    }
                    else
                    {
                        cmd = new SqlCommand("UpdateFactura", uow.Connection, uow.Transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NroFactura", invoice.Id);
                        cmd.Parameters.AddWithValue("@Fecha", invoice.Date);
                        cmd.Parameters.AddWithValue("@IdFormaPago", invoice.PaymentMethod.Id);
                        cmd.Parameters.AddWithValue("@Cliente", invoice.Customer);
                        cmd.Parameters.AddWithValue("@Activo", invoice.Status);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var detail in invoice.Details)
                    {
                        SqlCommand cmdDetail;
                        if (detail.Id == 0)
                        {
                            cmdDetail = new SqlCommand("InsertDetalleFactura", uow.Connection, uow.Transaction);
                            cmdDetail.CommandType = CommandType.StoredProcedure;
                            cmdDetail.Parameters.AddWithValue("@NroFactura", invoiceId);
                            cmdDetail.Parameters.AddWithValue("@IdArticulo", detail.Product.Id);
                            cmdDetail.Parameters.AddWithValue("@Cantidad", detail.Quantity);
                            cmdDetail.ExecuteNonQuery();
                        }
                        else
                        {
                            cmdDetail = new SqlCommand("UpdateDetalleFactura", uow.Connection, uow.Transaction);
                            cmdDetail.CommandType = CommandType.StoredProcedure;
                            cmdDetail.Parameters.AddWithValue("@IdDetalle", detail.Id);
                            cmdDetail.Parameters.AddWithValue("@NroFactura", invoiceId);
                            cmdDetail.Parameters.AddWithValue("@IdArticulo", detail.Product.Id);
                            cmdDetail.Parameters.AddWithValue("@Cantidad", detail.Quantity);
                            cmdDetail.ExecuteNonQuery();
                        }
                    }

                    uow.Commit();
                }
                catch (SqlException ex)
                {
                    uow.Rollback();
                    Console.WriteLine("Error al guardar la factura: " + ex.Message);
                    result = false;
                }
            }
            return result;
        }
    }
}
