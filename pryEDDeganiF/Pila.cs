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
    public partial class Pila : Form
    {
        public Pila()
        {
            InitializeComponent();
        }


        clsPila pila = new clsPila();

        private void Pila_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCodigo_Agregar.Text) &&
                !String.IsNullOrWhiteSpace(txtNombre_Agregar.Text) &&
                !String.IsNullOrWhiteSpace(txtTramite_Agregar.Text))
            {
                clsNodo nuevo = new clsNodo();

                nuevo.Nombre = txtNombre_Agregar.Text;
                nuevo.Codigo = Convert.ToInt32(txtCodigo_Agregar.Text);
                nuevo.Tramite = txtTramite_Agregar.Text;

                pila.Agregar(nuevo);
                pila.Recorrer(dgvPila);
                pila.Recorrer(lstPila);

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

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (pila.Primero != null)
            {
                MessageBox.Show("Se ha sacado a: " + pila.Primero.Nombre);
                lblEliminarCodigo.Text = pila.Primero.Codigo.ToString();
                lblEliminarNombre.Text = pila.Primero.Nombre;
                lblEliminarTramite.Text = pila.Primero.Tramite;
                pila.Eliminar();
                pila.Recorrer(dgvPila);
                pila.Recorrer(lstPila);
                pila.Recorrer();
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
