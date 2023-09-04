using CapaEntidad;
using CapaNegocio;
using capaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capaPresentacion
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            // se carga comboBox de "estado" utilizando la nueva clase "OpcionCombo"
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cbxEstado.DisplayMember = "Texto";
            cbxEstado.ValueMember = "Valor";
            cbxEstado.SelectedIndex = 0;

            // se carga comboBox de Roles
            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
                cbxCategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });

            cbxCategoria.DisplayMember = "Texto";
            cbxCategoria.ValueMember = "Valor";
            cbxCategoria.SelectedIndex = 0;


            //se carga el comboBox del buscador con el nombre de cada columba del GridView
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                    cbxBuscador.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cbxBuscador.DisplayMember = "Texto";
            cbxBuscador.ValueMember = "Valor";
            cbxBuscador.SelectedIndex = 0;

            //MOSTRAR TODOS LOS USUARIOS
            List<Producto> lista= new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Producto obj = new Producto()
            {
                IdProducto = Convert.ToInt32(txtID.Text),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cbxCategoria.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cbxEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProducto == 0)
            {
                int IdGenerado = new CN_Producto().Registrar(obj, out Mensaje);

                if (IdGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { 
                    "",
                    IdGenerado,
                    txtCodigo.Text,
                    txtNombre.Text,
                    txtDescripcion.Text,
                    ((OpcionCombo)cbxCategoria.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cbxCategoria.SelectedItem).Texto.ToString(),
                    "0",
                    "0.00",
                    "0.00",
                    ((OpcionCombo)cbxEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cbxEstado.SelectedItem).Texto.ToString()});

                    LimpiarTXT();
                }
                else
                    MessageBox.Show(Mensaje);
            }
            else
            {
                bool Resultado = new CN_Producto().Editar(obj, out Mensaje);

                if (Resultado == true)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtID.Text;
                    row.Cells["Codigo"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Descripcion"].Value = txtDescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cbxCategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cbxCategoria.SelectedItem).Texto.ToString();
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
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cbxEstado.SelectedIndex = 0;
            cbxCategoria.SelectedIndex = 0;
            txtCodigo.Select();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
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
                    txtCodigo.Text = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();
                    

                    // Rellena las comboBox con el contenido del usuario seleccionado
                    foreach (OpcionCombo oc in cbxCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indice_combo = cbxCategoria.Items.IndexOf(oc);
                            cbxCategoria.SelectedIndex = indice_combo;
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
                if (MessageBox.Show("¿Desea eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Producto obj = new Producto() { IdProducto = Convert.ToInt32(txtID.Text) };

                    bool Respuesta = new CN_Producto().Eliminar(obj, out Mensaje);

                    if (Respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        LimpiarTXT();
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
                foreach (DataGridViewRow row in dgvData.Rows)
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
