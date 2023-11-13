using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidadDL;
using System.Data;

namespace CapaDatosDAL
{
    public class CD_Marca
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IdMarca,Descripcion,Activo FROM MARCA";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = (dr["Descripcion"]).ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Marca>();
            }

            return lista;
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            int idAutoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAutoGenerado = 0;
                Mensaje = ex.Message;
            }
            return idAutoGenerado;
        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", oConexion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.IdMarca);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", oConexion);
                    cmd.Parameters.AddWithValue("IdMarca", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public List<Marca> ListarMarcaPorCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT DISTINCT m.IdMarca,m.Descripcion FROM PRODUCTO p");
                    sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria");
                    sb.AppendLine("INNER JOIN MARCA M ON m.IdMarca = p.IdMarca AND m.Activo = 1");
                    sb.AppendLine("WHERE c.IdCategoria = IIF(@idcategoria = 0, c.IdCategoria,@idcategoria)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = (dr["Descripcion"]).ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Marca>();
            }

            return lista;
        }

    }
}
