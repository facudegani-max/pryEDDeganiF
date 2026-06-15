using System;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    public partial class frmConsultasConOperaciones : Form
    {
        clsBaseDatos bd = new clsBaseDatos();

        public frmConsultasConOperaciones()
        {
            InitializeComponent();
            // Evitar que el usuario pueda escribir en el combo
            cmbOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            // Asegurar que la grilla no sea editable por el usuario
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void frmConsultasConOperaciones_Load(
            object sender, EventArgs e)
        {
            cmbOperaciones.Items.Add("Todos los libros");
            cmbOperaciones.Items.Add("Libros con su autor");
            cmbOperaciones.Items.Add("Libros con país");
            cmbOperaciones.Items.Add("Libros en español");
            cmbOperaciones.Items.Add("Libros con precio mayor a 1000");
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            string sql = "";

            switch (cmbOperaciones.Text)
            {
                case "Todos los libros":
                    sql = "SELECT * FROM Libro";
                    break;

                case "Libros con su autor":
                    sql = @"SELECT Libro.Titulo,
                            Autor.Nombre AS Autor
                            FROM Autor INNER JOIN Libro
                            ON Autor.IdAutor = Libro.IdAutor";
                    break;

                case "Libros con país":
                    sql = @"SELECT Libro.Titulo,
                            Pais.Nombre AS Pais
                            FROM Pais INNER JOIN Libro
                            ON Pais.IdPais = Libro.IdPais";
                    break;

                case "Libros en español":
                    sql = @"SELECT Libro.Titulo
                            FROM Libro INNER JOIN Idioma
                            ON Libro.IdIdioma = Idioma.IdIdioma
                            WHERE Idioma.Nombre='Español'";
                    break;

                case "Libros con precio mayor a 1000":
                    sql = "SELECT Titulo, Precio FROM Libro WHERE Precio > 1000";
                    break;
            }

            dataGridView1.DataSource =
                bd.EjecutarConsulta(sql);

            // Mostrar la consulta ejecutada en la etiqueta (si existe)
            try
            {
                // lblConsultasBaseDatos puede ser un Label en el diseñador
                lblConsultasBaseDatos.Text = sql;
            }
            catch
            {
                // Si no existe el label, ignorar
            }
        }
    }
}