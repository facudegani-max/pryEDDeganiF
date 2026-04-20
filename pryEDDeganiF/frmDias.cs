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
    public partial class frmDias : Form
    {
        public frmDias()
        {
            InitializeComponent();
        }

        private void frmDias_Load(object sender, EventArgs e)
        {
            frmDias x = new frmDias();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Dias.csv";
            x.Grabar(txtDia.Text);

            MessageBox.Show("Dia grabado correctamente");
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Dias.csv";
            lstDias.Items.Clear();
            x.Recorrer(lstDias);
        }
    }
}
