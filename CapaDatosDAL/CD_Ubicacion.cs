using CapaEntidadDL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CapaDatosDAL
{
    public class CD_Ubicacion
    {
        public List<Departamento> ObtenerDepartamento()
        {
            List<Departamento> lista = new List<Departamento>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM DEPARTAMENTO";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Departamento()
                                {
                                    IdDepartamento = dr["IdDepartamento"].ToString(),
                                    Descripcion = (dr["Descripcion"]).ToString(),
                                }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Departamento>();
            }

            return lista;
        }

        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM PROVINCIA WHERE IdDepartamento = @iddepartamento";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    IdProvincia = dr["IdProvincia"].ToString(),
                                    Descripcion = (dr["Descripcion"]).ToString(),
                                }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Provincia>();
            }

            return lista;
        }

        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<Distrito> lista = new List<Distrito>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM DISTRITO WHERE IdProvincia = @idprovincia AND IdDepartamento = @iddepartamento";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Distrito()
                                {
                                    IdDistrito = dr["IdDistrito"].ToString(),
                                    Descripcion = (dr["Descripcion"]).ToString(),
                                }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Distrito>();
            }

            return lista;
        }
    }
}
