using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar() //método Listar
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdUsuario,Nombres,Apellidos,Correo,Clave,Reestablecer,Activo from USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oconexion);//comando que permite ejecutar la query
                    cmd.CommandType = CommandType.Text;//comando de tipo texto
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())//lee todos los resultados del select
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                               new Usuario()
                               {
                                   IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                   Nombres = dr["Nombres"].ToString(),
                                   Apellidos = dr["Apellidos"].ToString(),
                                   Correo = dr["Correo"].ToString(),
                                   Clave = dr["Clave"].ToString(),
                                   Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                   Activo = Convert.ToBoolean(dr["Activo"])
                               }
                                );
                        }
                    }



                }




            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }
    }
}

