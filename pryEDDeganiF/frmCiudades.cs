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
    public partial class frmCiudades : Form
    {
        public frmCiudades()
        {
            InitializeComponent();
        }

        private void frmCiudades_Load(object sender, EventArgs e)
        {
            frmCiudades x = new frmCiudades();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Ciudad.csv";
            x.Grabar(txtCiudad.Text);

            MessageBox.Show("Ciudad grabada correctamente");
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Ciudad.csv";

            lstCiudades.Items.Clear();
            x.Recorrer(lstCiudades);
        }
    }
}
