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
    public  class CD_Permisos
    {
        public List<Permiso> Listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    //esta query permite hacer salto de lineas
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.idrol,p.nombremenu from permiso p");
                    query.AppendLine("inner join rol r on r.idrol = p.idrol");
                    query.AppendLine("inner join usuario u on u.idrol = r.idrol");
                    query.AppendLine("where u.idusuario = @idusuario");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    //le enviamos a cmd el parametro con el que va a comparar, es decir remplara el @idusuario por la variable idusuario q le enviamos
                    cmd.Parameters.AddWithValue("@idusuario",idusuario);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oConexion.Open();

                    //sqldatareader se encarga de leer el resultado del comando
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() {IdRol= Convert.ToInt32(dr["idrol"]) },
                                NombreMenu = dr["nombremenu"].ToString(),
                                

                            });

                        }

                    }

                }
                catch (Exception ex)
                {
                    //devuelve una lista vacia
                    lista = new List<Permiso>();
                }


            }

            return lista;
        }


    }
}
