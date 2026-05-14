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
    public partial class ListaSimple : Form
    {
        public ListaSimple()
        {
            InitializeComponent();
        }

        clsListaSimple lista = new clsListaSimple();
        private void ListaSimple_Load(object sender, EventArgs e)
        {
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsNodo nuevo = new clsNodo();

            nuevo.Nombre = txtNombre_Agregar.Text;
            nuevo.Codigo = Convert.ToInt32(txtCodigo_Agregar.Text);
            cmbCodigoEliminar.Items.Add(nuevo.Codigo);
            nuevo.Tramite = txtTramite_Agregar.Text;

            lista.Agregar(nuevo);
            lista.Recorrer(dgvListaSimple);
            lista.Recorrer(lstListaSimple);

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
                lista.Recorrer(dgvListaSimple);
                lista.Recorrer(lstListaSimple);
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
    }
}
