using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pryEDDeganiF.Clases;

namespace pryEDDeganiF
{
    public partial class frmArbol : Form
    {
        private clsArbolBinario arbol = new clsArbolBinario();

        public frmArbol()
        {
            InitializeComponent();

            // Eventos
            btnAgregar.Click += BtnAgregar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnEquilibrar.Click += BtnEquilibrar_Click;
            opPre_Orden.CheckedChanged += RecorridoCambiado;
            optIn_Orden.CheckedChanged += RecorridoCambiado;
            opPost_Orden.CheckedChanged += RecorridoCambiado;

            // Evitar edición directa en el combobox de eliminación
            cmbCodigoEliminar.DropDownStyle = ComboBoxStyle.DropDownList;

            ActualizarVista();
        }

        private void BtnAgregar_Click(object sender, System.EventArgs e)
        {
            if (!int.TryParse(txtCodigo_Agregar.Text.Trim(), out int codigo))
            {
                MessageBox.Show("Código inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = new clsNodo
            {
                Codigo = codigo,
                Nombre = txtNombre_Agregar.Text.Trim(),
                Tramite = txtTramite_Agregar.Text.Trim()
            };

            arbol.Agregar(nodo);
            LimpiarEntradas();
            ActualizarVista();
        }

        private void BtnEliminar_Click(object sender, System.EventArgs e)
        {
            if (cmbCodigoEliminar.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un código para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cmbCodigoEliminar.SelectedItem.ToString(), out int codigo))
                return;

            arbol.Eliminar(codigo);
            ActualizarVista();
        }

        private void BtnEquilibrar_Click(object sender, System.EventArgs e)
        {
            arbol.Equilibrar();
            ActualizarVista();
        }

        private void RecorridoCambiado(object sender, System.EventArgs e)
        {
            ActualizarListado();
        }

        private void LimpiarEntradas()
        {
            txtCodigo_Agregar.Clear();
            txtNombre_Agregar.Clear();
            txtTramite_Agregar.Clear();
        }

        private void ActualizarVista()
        {
            ActualizarListado();
            ActualizarComboEliminar();
            ActualizarTreeView();
        }

        private void ActualizarListado()
        {
            List<clsNodo> lista;
            if (opPre_Orden.Checked)
                lista = arbol.PreOrden();
            else if (opPost_Orden.Checked)
                lista = arbol.PostOrden();
            else
                lista = arbol.InOrden();

            dgvListaDoble.Rows.Clear();
            foreach (var n in lista)
            {
                dgvListaDoble.Rows.Add(n.Codigo, n.Nombre, n.Tramite);
            }
        }

        private void ActualizarComboEliminar()
        {
            var todos = arbol.ObtenerTodos().Select(n => n.Codigo.ToString()).ToArray();
            cmbCodigoEliminar.Items.Clear();
            cmbCodigoEliminar.Items.AddRange(todos);
            if (cmbCodigoEliminar.Items.Count > 0)
                cmbCodigoEliminar.SelectedIndex = 0;
        }

        private void ActualizarTreeView()
        {
            treeView1.Nodes.Clear();
            if (arbol.Raiz == null) return;

            var root = ConstruirNodoTree(arbol.Raiz);
            treeView1.Nodes.Add(root);
            treeView1.ExpandAll();
        }

        private TreeNode ConstruirNodoTree(clsNodo nodo)
        {
            if (nodo == null) return null;
            var tn = new TreeNode($"{nodo.Codigo} - {nodo.Nombre}");
            var iz = ConstruirNodoTree(nodo.Izquierdo);
            var de = ConstruirNodoTree(nodo.Derecho);
            if (iz != null) tn.Nodes.Add(iz);
            if (de != null) tn.Nodes.Add(de);
            return tn;
        }
    }
}
