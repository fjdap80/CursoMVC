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
    public class CD_Reporte
    {
        public DashBoard VerDashBoard() //método Listar todos los usuario que tenemos en la tabla
        {
            DashBoard objeto = new DashBoard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))                {
                    
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);//comando que permite ejecutar la query
                    cmd.CommandType = CommandType.StoredProcedure;//comando de tipo texto
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())//lee todos los resultados del select
                    {
                        while (dr.Read())
                        {

                            objeto = new DashBoard()
                            {
                                TotalCliente= Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta= Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto= Convert.ToInt32(dr["TotalProducto"]),

                            };
                          
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashBoard();
            }

            return objeto;
        }
    }
}
