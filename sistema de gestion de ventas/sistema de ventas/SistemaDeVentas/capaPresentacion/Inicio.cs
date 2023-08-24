using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

//reconoce los iconos de la libreria
using FontAwesome.Sharp;

namespace capaPresentacion
{
    public partial class Inicio : Form
    {
        //indica el usuario que ingreso al sistema
        private static Usuario usuarioactual;
        //indica el menu actual seleccionado
        private static IconMenuItem menuactivo=null;
        //indica el formulario activo actualmente.
        private static Form formularioactivo=null;




        public Inicio(Usuario ousuario)
        {
            usuarioactual = ousuario;
            
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

            //aca vamos a restringir, segun que usuario ingresa para que sea visible el menu o no.
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioactual.IdUsuario);

            //va recorrer el todos los iconmenu del menu del diseño
            foreach (IconMenuItem iconmenu in menu.Items ) {
                //         any determina si la lista contiene elementos
                //   m es cada elemento de la lista, y iconmenu del menu
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);
                if (encontrado == false) {
                    iconmenu.Visible = false;
                
                }

            }

            lblusuario.Text = usuarioactual.NombreCompleto;
        }

        private void AbrirFormulario(IconMenuItem menu,Form formulario ) {

            if (menuactivo != null) {

                menuactivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            menuactivo = menu;

            //si el formulacio activo ya se mostro algo, necesitamos que se cierre. para visualizar el nuevo
            if (formularioactivo!=null) {
                formularioactivo.Close();
            }

            formularioactivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;
            contenedor.Controls.Add(formulario);

            formulario.Show();

        }

        
        private void menuusuario_Click(object sender, EventArgs e)
        {
            //el metodo necesita un menu, que esta en sender, lo casteamos, y el menu necesita el nuevo form, que va abrir
            AbrirFormulario((IconMenuItem)sender,new frmUsuarios());

        }

        private void submenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmCategoria());
        }

        private void submenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmProducto());
        }

        private void submenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentas ());
        }

        private void submenuVerdetalleVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmDetalleVenta());
        }

        private void submenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompras());
        }

        private void submenuVerDetallecompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmDetalleCompra());
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuclientes, new frmClientes());
        }

        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuproveedores, new frmProveedores());
        }

        private void menureportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReportes());
        }

        
    }
}
