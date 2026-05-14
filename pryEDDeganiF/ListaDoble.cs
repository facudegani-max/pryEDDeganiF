using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    public partial class ListaDoble : Form
    {
        public ListaDoble()
        {
            InitializeComponent();
        }

        clsListaDoble lista = new clsListaDoble();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsNodo nuevo = new clsNodo();

            nuevo.Nombre = txtNombre_Agregar.Text;
            nuevo.Codigo = Convert.ToInt32(txtCodigo_Agregar.Text);
            cmbCodigoEliminar.Items.Add(nuevo.Codigo);
            nuevo.Tramite = txtTramite_Agregar.Text;

            lista.Agregar(nuevo);
            lista.Recorrer(dgvListaDoble);
            lista.Recorrer(lstListaDoble);

            txtCodigo_Agregar.Clear();
            txtNombre_Agregar.Clear();
            txtTramite_Agregar.Clear();
            txtCodigo_Agregar.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lista.Primero != null)
            {
                Int32 codigo = Convert.ToInt32(cmbCodigoEliminar.Text);
                lista.Eliminar(codigo);
                lista.Recorrer(dgvListaDoble);
                lista.Recorrer(lstListaDoble);
                lista.Recorrer();

                cmbCodigoEliminar.Items.Remove(codigo);
                cmbCodigoEliminar.Focus();
                cmbCodigoEliminar.Enabled = false;
            }
            else
            {
                cmbCodigoEliminar.Items.Clear();
                txtCodigo_Agregar.Focus();
            }
        }

        private void optDescendente_CheckedChanged(object sender, EventArgs e)
        {
            if (optDescendente.Checked)
            {
                lista.RecorrerDes(dgvListaDoble);
                lista.RecorrerDes(lstListaDoble);
            }
            else
            {
                lista.Recorrer(dgvListaDoble);
                lista.Recorrer(lstListaDoble);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ListaDoble_Load(object sender, EventArgs e)
        {
            optAscender.Checked = true;
        }

        private void optAscender_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
