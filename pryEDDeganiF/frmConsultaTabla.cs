using System;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    public partial class frmConsultaTabla : Form
    {
        private string currentDbPath = null;
        private bool comboContainsTables = false;
        // Buscar archivos .mdb y .accdb en la carpeta de la aplicación y en "BaseDatos"
        private string[] BuscarArchivosAccess()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var searchDirs = new[] { baseDir, Path.Combine(baseDir, "BaseDatos") };
            var files = searchDirs.Where(Directory.Exists)
                .SelectMany(d => Directory.GetFiles(d, "*.mdb").Concat(Directory.GetFiles(d, "*.accdb")))
                .ToArray();
            return files;
        }

        public frmConsultaTabla()
        {
            InitializeComponent();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            btnListar.Click += BtnListar_Click;
            Load += FrmConsultaTabla_Load;
        }

        private void FrmConsultaTabla_Load(object sender, EventArgs e)
        {
            // Si existe Libreria.mdb/accdb en BaseDatos, cargar sus tablas en el combo.
            var files = BuscarArchivosAccess();
            var found = files.FirstOrDefault(p => p.EndsWith("Libreria.mdb", StringComparison.OrdinalIgnoreCase)
                || p.EndsWith("Libreria.accdb", StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                PopulateTablesFromAccess(found);
            }
            else
            {
                CargarBasesDeDatosAccess();
            }

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void PopulateTablesFromAccess(string path)
        {
            try
            {
                var connStr = BuildOleDbConnectionString(path);
                using (var conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    var schema = conn.GetSchema("Tables");
                    comboBox1.Items.Clear();
                    foreach (DataRow row in schema.Rows)
                    {
                        var tableType = row["TABLE_TYPE"].ToString();
                        if (!string.Equals(tableType, "TABLE", StringComparison.OrdinalIgnoreCase))
                            continue;
                        var tableName = row["TABLE_NAME"].ToString();
                        comboBox1.Items.Add(tableName);
                    }

                    if (comboBox1.Items.Count > 0)
                    {
                        comboContainsTables = true;
                        currentDbPath = path;
                        comboBox1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener tablas de Libreria: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarBasesDeDatosAccess();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboContainsTables && currentDbPath != null && comboBox1.SelectedItem != null)
            {
                MostrarContenidoTabla(comboBox1.SelectedItem.ToString());
            }
        }

        private void MostrarContenidoTabla(string tableName)
        {
            try
            {
                var connStr = BuildOleDbConnectionString(currentDbPath);
                using (var conn = new OleDbConnection(connStr))
                using (var cmd = new OleDbCommand($"SELECT * FROM [{tableName}]", conn))
                {
                    var dt = new DataTable();
                    var da = new OleDbDataAdapter(cmd);
                    conn.Open();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarBasesDeDatosAccess()
        {
            try
            {
                var files = BuscarArchivosAccess();
                comboBox1.Items.Clear();
                foreach (var f in files)
                {
                    comboBox1.Items.Add(f);
                }

                if (comboBox1.Items.Count == 0)
                {
                    MessageBox.Show("No se encontraron archivos de Access (.mdb/.accdb) en la carpeta de la aplicación ni en la carpeta 'Databases'.\n" +
                        "Coloque sus archivos .mdb/.accdb en esa carpeta o en la carpeta del ejecutable.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar archivos de Access: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildOleDbConnectionString(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            if (ext == ".accdb")
            {
                return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Persist Security Info=False;";
            }
            else
            {
                // mdb
                return $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};Persist Security Info=False;";
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un archivo de base de datos Access o una tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el combo actualmente contiene nombres de tablas (comboContainsTables == true),
            // al presionar Listar mostramos el contenido de la tabla seleccionada.
            if (comboContainsTables && currentDbPath != null)
            {
                var tableName = comboBox1.SelectedItem.ToString();
                // Mostrar contenido de la tabla seleccionada
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    dataGridView1.ReadOnly = true;
                    MostrarContenidoTabla(tableName);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    // mantener ReadOnly para evitar edición
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                }
                return;
            }

            // Si el combo contiene rutas de archivos (no tablas), entonces tratamos la selección como archivo
            var path = comboBox1.SelectedItem.ToString();
            if (!File.Exists(path))
            {
                MessageBox.Show("El archivo seleccionado no existe: " + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Cargar las tablas del archivo seleccionado en el combo y luego mostrar la primera tabla
                var connStr = BuildOleDbConnectionString(path);
                using (var conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    var schema = conn.GetSchema("Tables");
                    var tablas = new System.Collections.Generic.List<string>();
                    foreach (DataRow row in schema.Rows)
                    {
                        var tableType = row["TABLE_TYPE"].ToString();
                        if (!string.Equals(tableType, "TABLE", StringComparison.OrdinalIgnoreCase))
                            continue;
                        var tableName = row["TABLE_NAME"].ToString();
                        tablas.Add(tableName);
                    }

                    if (tablas.Count == 0)
                    {
                        MessageBox.Show("No se encontraron tablas en el archivo seleccionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    comboBox1.Items.Clear();
                    foreach (var t in tablas)
                        comboBox1.Items.Add(t);
                    comboContainsTables = true;
                    currentDbPath = path;
                    comboBox1.SelectedIndex = 0;

                    // Mostrar la primera tabla automáticamente
                    this.Cursor = Cursors.WaitCursor;
                    dataGridView1.ReadOnly = true;
                    MostrarContenidoTabla(tablas[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar las tablas del archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
