using System;
using System;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    public partial class frmConsultaSQL : Form
    {
        clsBaseDatos bd = new clsBaseDatos();

        public frmConsultaSQL()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = txtConsulta.Text.Trim();
                if (string.IsNullOrEmpty(consulta))
                {
                    MessageBox.Show("Escriba una consulta SQL en el cuadro de texto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Deshabilitar edición en la grilla mientras se ejecuta la consulta
                this.Cursor = Cursors.WaitCursor;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;

                var tabla = bd.EjecutarConsulta(consulta);
                dataGridView1.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                // asegurar que la grilla permanezca no editable
                dataGridView1.ReadOnly = true;
            }
        }
    }
}
