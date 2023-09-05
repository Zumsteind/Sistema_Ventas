﻿using CapaEntidad;
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
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            // se carga comboBox de "estado" utilizando la nueva clase "OpcionCombo"
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cbxEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cbxEstado.DisplayMember = "Texto";
            cbxEstado.ValueMember = "Valor";
            cbxEstado.SelectedIndex = 0;

            
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
            List<Proveedor> lista= new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvData.Rows.Add(new object[] { "", item.IdProveedor, item.Documento, item.RazonSocial, item.Correo, item.Telefono,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtID.Text),
                Documento = txtDocumento.Text,
                RazonSocial = txtRazonSocial.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cbxEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProveedor == 0)
            {
                int IdGenerado = new CN_Proveedor().Registrar(obj, out Mensaje);

                if (IdGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] { "", IdGenerado, txtDocumento.Text, txtRazonSocial.Text, txtCorreo.Text, txtTelefono.Text,
                    ((OpcionCombo)cbxEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cbxEstado.SelectedItem).Texto.ToString()});
                    LimpiarTXT();
                }
                else
                    MessageBox.Show(Mensaje);
            }
            else
            {
                bool Resultado = new CN_Proveedor().Editar(obj, out Mensaje);

                if (Resultado == true)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtID.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
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
            txtRazonSocial.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cbxEstado.SelectedIndex = 0;
            txtDocumento.Select();
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
                int indice = e.RowIndex; //se obtiene el numero del indice de la fila en donde se hace click

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtID.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtRazonSocial.Text = dgvData.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                   
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
                if (MessageBox.Show("¿Desea eliminar el Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;
                    Proveedor obj = new Proveedor() { IdProveedor = Convert.ToInt32(txtID.Text) };

                    bool Respuesta = new CN_Proveedor().Eliminar(obj, out Mensaje);

                    if (Respuesta)
                    dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    
                    else
                    MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                    
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
