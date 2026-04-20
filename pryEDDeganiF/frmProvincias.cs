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
    public partial class frmProvincias : Form
    {
        public frmProvincias()
        {
            InitializeComponent();
        }

        private void frmProvincias_Load(object sender, EventArgs e)
        {
            frmProvincias x = new frmProvincias();
            x.ShowDialog();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Provincia.csv";
            x.Grabar(txtProvincia.Text);

            MessageBox.Show("Provincia grabada correctamente");
        }
    }
}
