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
    public partial class frmAlumnos : Form
    {
        public frmAlumnos()
        {
            InitializeComponent();
        }

        private void Alumnos_Load(object sender, EventArgs e)
        {
            clsArchivoTexto x = new clsArchivoTexto();
            x.NombreArchivo = "Alumnos.csv";
            if (File.Exists(x.NombreArchivo)) x.Recorrer(dgvAlumnos);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            clsArchivoTexto objCliente = new clsArchivoTexto();
            objCliente.NombreArchivo = "Alumnos.csv";
            objCliente.Grabar(txtCodigo.Text, txtNombre.Text, txtCarrera.Text);
            objCliente.Recorrer(dgvAlumnos);

            MessageBox.Show("Alumno grabado correctamente");
        }
    }
}
