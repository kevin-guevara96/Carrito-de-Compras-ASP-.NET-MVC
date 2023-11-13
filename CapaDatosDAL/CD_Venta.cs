using CapaEntidadDL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatosDAL
{
    public class CD_Venta
    {
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oConexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("Contacto", obj.Contacto);
                    cmd.Parameters.AddWithValue("IdDistrito", obj.IdDistrito);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public List<DetalleVenta> ListarCompras(int idclente)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM fn_ListarCompra(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@idcliente", idclente);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                oProducto = new Producto()
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-MX")),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),

                                },
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-MX")),
                                IdTransaccion = dr["IdTransaccion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<DetalleVenta>();
            }

            return lista;
        }

    }
}
