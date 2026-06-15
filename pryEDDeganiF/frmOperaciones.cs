using System;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    public partial class frmOperaciones : Form
    {
        private clsBaseDatos bd = new clsBaseDatos();

        public frmOperaciones()
        {
            InitializeComponent();

            // Proyección simple
            btnProyeccionSimple.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT Titulo FROM Libro");

            // Proyección multiatributo
            btnProyeccionMultiatributo.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT Titulo, Precio, Cantidad FROM Libro");

            // Agrupar (cantidad de libros por idioma)
            btnAgrupar.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT IdIdioma, COUNT(*) AS CantidadLibros FROM Libro GROUP BY IdIdioma");

            // Selección simple
            btnSeleccionSimple.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT * FROM Libro WHERE Precio > 100");

            // Selección multiatributo
            btnSeleccionMultiatributo.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT * FROM Libro WHERE Precio > 50 AND Cantidad > 0");

            // Selección por convolución (subconsulta)
            btnselect_Convolucion.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT * FROM Libro WHERE IdAutor IN (SELECT IdAutor FROM Autor WHERE Nombre LIKE 'J%')");

            // Unión
            btnUnion.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT Titulo FROM Libro WHERE Precio < 50 UNION SELECT Titulo FROM Libro WHERE Precio > 200");

            // Intersección (simulada con JOIN, ya que Access no soporta INTERSECT)
            btnInterseccion.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT Libro.Titulo FROM Libro INNER JOIN Autor ON Libro.IdAutor = Autor.IdAutor WHERE Autor.Nombre LIKE 'G%'");

            // Diferencia
            btnDiferencia.Click += (s, e) =>
                dataGridView1.DataSource = bd.EjecutarConsulta(
                    "SELECT Titulo FROM Libro WHERE IdIdioma = 1 AND IdLibro NOT IN (SELECT IdLibro FROM Libro WHERE IdIdioma = 2)");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnProyeccionSimple_Click(object sender, EventArgs e)
        {
        }

        private void btnProyeccionMultiatributo_Click(object sender, EventArgs e)
        {
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
        }

        private void btnSeleccionSimple_Click(object sender, EventArgs e)
        {
        }

        private void btnSeleccionMultiatributo_Click(object sender, EventArgs e)
        {
        }

        private void btnselect_Convolucion_Click(object sender, EventArgs e)
        {
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
        }

        private void btnInterseccion_Click(object sender, EventArgs e)
        {
        }

        private void btnDiferencia_Click(object sender, EventArgs e)
        {
        }
    }
}