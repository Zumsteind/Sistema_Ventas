using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using capaPresentacion.Utilidades;
using CapaNegocio;
using CapaEntidad;

namespace capaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            // se carga comboBox de "estado" utilizando la nueva clase "OpcionCombo"
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cbxEstado.DisplayMember = "Texto";
            cbxEstado.ValueMember = "Valor";
            cbxEstado.SelectedIndex = 0;

            // se carga comboBox de Roles
            List<Rol> listaRol = new CN_Rol().Listar();

            foreach(Rol item in listaRol)
            cbxRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            
            cbxRol.DisplayMember = "Texto";
            cbxRol.ValueMember = "Valor";
            cbxRol.SelectedIndex = 0;


            //se carga el comboBox del buscador con el nombre de cada columba del GridView
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if(columna.Visible == true && columna.Name!= "btnSeleccionar")
                cbxBuscador.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
                cbxBuscador.DisplayMember = "Texto";
                cbxBuscador.ValueMember = "Valor";
                cbxBuscador.SelectedIndex = 0;

            //MOSTRAR TODOS LOS USUARIOS
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { "", item.IdUsuario, item.Documento, item.NombreCompleto, item.Correo, item.Clave,
                    item.oRol.IdRol, 
                    item.oRol.Descripcion, 
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo" 
                });
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtID.Text),
                Documento = txtDocumento.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtContrasenia.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cbxRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cbxEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if(objUsuario.IdUsuario == 0)
            {
              int IdUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out Mensaje);

                if (IdUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { "", IdUsuarioGenerado, txtDocumento.Text, txtNombreCompleto.Text, txtCorreo.Text, txtContrasenia.Text,
                    ((OpcionCombo)cbxRol.SelectedItem).Valor.ToString(), 
                    ((OpcionCombo)cbxRol.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)cbxEstado.SelectedItem).Valor.ToString(), 
                    ((OpcionCombo)cbxEstado.SelectedItem).Texto.ToString()});
           
                   LimpiarTXT(); 
                }
                 else
                 MessageBox.Show(Mensaje);                 
            }
            else
            {
                bool Resultado = new CN_Usuario().Editar(objUsuario, out Mensaje);

                if (Resultado == true)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32( txtIndice.Text)];
                    row.Cells["Id"].Value = txtID.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Clave"].Value = txtContrasenia.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cbxRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cbxRol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cbxEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cbxEstado.SelectedItem).Texto.ToString();

                    LimpiarTXT();
                }
                else
                MessageBox.Show(Mensaje);                
            }     
        }


        private void LimpiarTXT()
        {
            txtIndice.Text = "-1";
            txtID.Text = "0";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtContrasenia.Text = "";
            txtConfirmarContrasenia.Text = "";
            cbxEstado.SelectedIndex = 0;
            cbxRol.SelectedIndex = 0;
            txtDocumento.Select();
        }

        //Este evento sirve para agregarle un icono al boton del gridView
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex ==0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        //Evento para rellenar los TextBox con el contenido de la fila seleccionada del Gridview
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                //se obtiene el numero del indice de la fila en donde se hace click
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtContrasenia.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarContrasenia.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();


                    // Rellena las comboBox con el contenido del usuario seleccionado
                    foreach (OpcionCombo oc in cbxRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor)== Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cbxRol.Items.IndexOf(oc);
                            cbxRol.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cbxEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cbxEstado.Items.IndexOf(oc);
                            cbxEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if(MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Usuario objUsuario = new Usuario() { IdUsuario = Convert.ToInt32(txtID.Text) };

                    bool Respuesta = new CN_Usuario().Eliminar(objUsuario, out Mensaje);

                    if (Respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string ColumnaFiltro = ((OpcionCombo)cbxBuscador.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[ColumnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBuscador.Text.Trim().ToUpper()))
                    row.Visible = true;
                    
                    else
                    row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBuscador.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarTXT();
        }
    }
}
