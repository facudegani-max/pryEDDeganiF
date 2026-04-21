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

        private void Cola_Load(object sender, EventArgs e)
        {

        }

        ClsCola fila = new ClsCola();
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ClsNodo n = new ClsNodo();
            n.Cod = Convert.ToInt32(txtCodigo_Agregar.Text);
            n.Nom = txtNombre_Agregar.Text;
            n.Tra = txtTramite_Agregar.Text;

            fila.Agregar(n);
            fila.Mostrar(lstCola);
            fila.Mostrar(dgvCola);

            //Cambiar a label
            //lblCodigo.Text = txtCodigo_Agregar.Text;
            //lblNombre.Text = txtNombre_Agregar.Text;
            //lblTramite.Text = txtTramite_Agregar.Text;

            txtCodigo_Agregar.Text = "";
            txtNombre_Agregar.Text = "";
            txtTramite_Agregar.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (fila.Ini != null)
            {
                fila.Eliminar();
                fila.Mostrar(lstCola);
                fila.Mostrar(dgvCola);
                //fila.Mostrar();
            }
            else 
            { 
                txtCodigo_Eliminar.Text = "";
                txtNombre_Eliminar.Text = "";
                txtTramite_Eliminar.Text = "";
            }
        }
    }
}
