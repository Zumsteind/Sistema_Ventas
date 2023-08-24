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
    public class CD_Usuario
    {

        public List<Usuario> Listar() {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena)) {

                try
                {
                    string query = "select idusuario,documento,nombrecompleto,clave,estado from usuario";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oConexion.Open();

                    //sqldatareader se encarga de leer el resultado del comando
                    using (SqlDataReader dr = cmd.ExecuteReader()) {

                        while (dr.Read()) {

                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["idusuario"]),
                                Documento = dr["documento"].ToString(),
                                NombreCompleto = dr["nombrecompleto"].ToString(),
                                Clave = dr["clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["estado"])

                            }) ; 
                        
                        }
                    
                    }

                }
                catch (Exception ex)
                {
                    //devuelve una lista vacia
                    lista = new List<Usuario>();
                }
            
            
            }

            return lista;
        }


    }
}
