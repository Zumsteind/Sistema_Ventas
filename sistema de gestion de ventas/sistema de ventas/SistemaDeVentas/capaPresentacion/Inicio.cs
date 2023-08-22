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

namespace capaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioactual;

        public Inicio(Usuario ousuario)
        {
            usuarioactual = ousuario;
            
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            lblusuario.Text = usuarioactual.NombreCompleto;
        }

        
    }
}
