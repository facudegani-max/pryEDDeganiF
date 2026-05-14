using pryEDDeganiF;
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
    public partial class Cola : Form
    {
        public Cola()
        {
            InitializeComponent();
        }

        clsCola fila = new clsCola();

        private void Cola_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCodigo_Agregar.Text) &&
                !String.IsNullOrWhiteSpace(txtNombre_Agregar.Text) &&
                !String.IsNullOrWhiteSpace(txtTramite_Agregar.Text))
            {
                clsNodo nuevo = new clsNodo();

                nuevo.Nombre = txtNombre_Agregar.Text;
                nuevo.Codigo = Convert.ToInt32(txtCodigo_Agregar.Text);
                nuevo.Tramite = txtTramite_Agregar.Text;

                fila.Agregar(nuevo);
                fila.Recorrer(dgvCola);
                fila.Recorrer(lstCola);

                txtCodigo_Agregar.Clear();
                txtNombre_Agregar.Clear();
                txtTramite_Agregar.Clear();
                txtCodigo_Agregar.Focus();
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos");
                txtCodigo_Agregar.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (fila.Primero != null)
            {
                MessageBox.Show("Se ha atendido a: " + fila.Primero.Nombre);
                lblEliminarCodigo.Text = fila.Primero.Codigo.ToString();
                lblEliminarNombre.Text = fila.Primero.Nombre;
                lblEliminarTramite.Text = fila.Primero.Tramite;
                fila.Eliminar();
                fila.Recorrer(dgvCola);
                fila.Recorrer(lstCola);
                fila.Recorrer();
            }
            else
            {
                lblEliminarCodigo.Text = "";
                lblEliminarNombre.Text = "";
                lblEliminarTramite.Text = "";
            }
        }
    }
}
