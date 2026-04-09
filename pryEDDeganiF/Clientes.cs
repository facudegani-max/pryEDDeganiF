using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryEDDeganiF
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto objCliente = new clsArchivoTexto();
            objCliente.NombreArchivo = "Clientes.csv";
            objCliente.Grabar(txtCodigo.Text, txtNombre.Text, txtDeuda.Text);
            objCliente.Recorrer(dgvClientes);

            MessageBox.Show("Cliente grabado correctamente");

        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Clientes.csv";
            if (File.Exists(x.NombreArchivo)) x.Recorrer(dgvClientes);

        }
    }
}
