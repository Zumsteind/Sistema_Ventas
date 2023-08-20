using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio

    //lo unico que hacemos devolvemos el metodo de la capa datos, CD_ el que sea
{
        public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario();



        public List<Usuario> Listar(){

            return objcd_usuario.Listar();
        }


    }
}
