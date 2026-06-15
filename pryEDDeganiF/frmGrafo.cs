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
    public partial class frmGrafo : Form
    {
        private clsGrafo grafo = new clsGrafo();

        public frmGrafo()
        {
            InitializeComponent();

            // Cargar comboboxes con las ciudades y evitar edición por parte del usuario
            var ciudades = new[] { "Cordoba", "Salta", "Santa Fe", "Mendoza", "Buenos Aires" };

            comboBox1.Items.AddRange(ciudades);
            comboBox2.Items.AddRange(ciudades);
            comboBox3.Items.AddRange(ciudades);
            comboBox4.Items.AddRange(ciudades);
            cmbboxListar_Viajes_Desde.Items.AddRange(ciudades);
            cmbbox_Listar_Viajes_Hasta.Items.AddRange(ciudades);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbboxListar_Viajes_Desde.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbbox_Listar_Viajes_Hasta.DropDownStyle = ComboBoxStyle.DropDownList;

            // Seleccionar primer elemento por defecto cuando existan items
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            if (comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;
            if (cmbboxListar_Viajes_Desde.Items.Count > 0) cmbboxListar_Viajes_Desde.SelectedIndex = 0;
            if (cmbbox_Listar_Viajes_Hasta.Items.Count > 0) cmbbox_Listar_Viajes_Hasta.SelectedIndex = 0;

            // Eventos de botones
            btnAgregar.Click += BtnAgregar_Click;
            btnBorrarCarga.Click += BtnBorrarCarga_Click;
            btnConsultar.Click += BtnConsultar_Click;
            btnBorrarConsulta.Click += BtnBorrarConsulta_Click;
            btnListar_Destinos.Click += BtnListar_Destinos_Click;
            btnListar_Origenes.Click += BtnListar_Origenes_Click;
            btnVerTodo.Click += BtnVerTodo_Click;
        }

        private void BtnAgregar_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Seleccione origen y destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var origen = comboBox1.SelectedItem.ToString();
            var destino = comboBox2.SelectedItem.ToString();

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("Precio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            grafo.AgregarArista(origen, destino, precio);
            MessageBox.Show("Viaje agregado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarMatriz();
        }

        private void BtnBorrarCarga_Click(object sender, System.EventArgs e)
        {
            // Limpiar campos de carga
            txtPrecio.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }

        private void BtnConsultar_Click(object sender, System.EventArgs e)
        {
            if (comboBox4.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Seleccione origen y destino para la consulta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var origen = comboBox4.SelectedItem.ToString();
            var destino = comboBox3.SelectedItem.ToString();
            var precio = grafo.ObtenerPrecio(origen, destino);
            lblPrecio.Text = precio.HasValue ? precio.Value.ToString("0.##") : "No existe viaje";
        }

        private void BtnBorrarConsulta_Click(object sender, System.EventArgs e)
        {
            lblPrecio.Text = string.Empty;
            if (comboBox4.Items.Count > 0) comboBox4.SelectedIndex = 0;
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;

            // También limpiar la grilla para que no queden datos visibles
            try
            {
                dgvCola.DataSource = null;
                dgvCola.Rows.Clear();
                dgvCola.Columns.Clear();
            }
            catch
            {
                // Ignorar errores si la grilla ya está limpia
            }
        }

        private void BtnListar_Destinos_Click(object sender, System.EventArgs e)
        {
            if (cmbboxListar_Viajes_Desde.SelectedItem == null)
            {
                MessageBox.Show("Seleccione origen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var origen = cmbboxListar_Viajes_Desde.SelectedItem.ToString();
            var lista = grafo.ListarDestinosDesde(origen);
            var dt = new DataTable();
            dt.Columns.Add("Destino");
            dt.Columns.Add("Precio");
            foreach (var kv in lista)
            {
                var r = dt.NewRow();
                r[0] = kv.Key;
                r[1] = kv.Value.ToString("0.##");
                dt.Rows.Add(r);
            }
            dgvCola.DataSource = dt;
        }

        private void BtnListar_Origenes_Click(object sender, System.EventArgs e)
        {
            if (cmbbox_Listar_Viajes_Hasta.SelectedItem == null)
            {
                MessageBox.Show("Seleccione destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var destino = cmbbox_Listar_Viajes_Hasta.SelectedItem.ToString();
            var lista = grafo.ListarOrigenesHacia(destino);
            var dt = new DataTable();
            dt.Columns.Add("Origen");
            dt.Columns.Add("Precio");
            foreach (var kv in lista)
            {
                var r = dt.NewRow();
                r[0] = kv.Key;
                r[1] = kv.Value.ToString("0.##");
                dt.Rows.Add(r);
            }
            dgvCola.DataSource = dt;
        }

        private void BtnVerTodo_Click(object sender, System.EventArgs e)
        {
            MostrarMatriz();
        }

        private void MostrarMatriz()
        {
            var dt = grafo.ToDataTable();
            dgvCola.DataSource = dt;
        }
    }
}
