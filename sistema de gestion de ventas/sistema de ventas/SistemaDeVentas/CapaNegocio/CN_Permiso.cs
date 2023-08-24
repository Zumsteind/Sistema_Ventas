using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
     public class CN_Permiso
    {
        private CD_Permisos objcd_permisos = new CD_Permisos();



        public List<Permiso> Listar(int idusuario)
        {

            return objcd_permisos.Listar(idusuario);
        }


    }
}
