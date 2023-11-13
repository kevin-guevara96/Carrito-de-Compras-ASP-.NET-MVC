using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidadDL;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CapaDatosDAL
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechaincio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oConexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fechainicio",fechaincio);
                    cmd.Parameters.AddWithValue("fechafin",fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Poducto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-MX")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-MX")),
                                    IdTransaccion = dr["IdTransaccion"].ToString(),                                  
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Reporte>();
            }

            return lista;
        }


        public DashBoard VerDashBoard()
        {
            DashBoard obj = new DashBoard();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                obj = new DashBoard();
            }

            return obj;
        }


    }
}
