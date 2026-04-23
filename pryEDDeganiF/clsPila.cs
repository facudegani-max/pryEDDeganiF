using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    internal class clsPila
    {
        private clsNodo pri;
        private clsNodo fin;

        public clsNodo Primero { get { return pri; } set { pri = value; } }
        public clsNodo Final { get { return fin; } set { fin = value; } }


        public void Agregar(clsNodo Nuevo)
        {
            if (Primero == null)
            {
                pri = Nuevo;
            }
            else
            {
                Nuevo.Sig = Primero;
                Primero = Nuevo;
            }
        }

        public void Eliminar()
        {
            if (Primero != null)
            {
                Primero = Primero.Sig;
            }
        }

        /*
        public void Mostrar(DataGridView DgvCola)
        {
            clsNodo aux = Ini;
            DgvCola.Rows.Clear();
            while (aux != null)
            {
                DgvCola.Rows.Add(aux.Cod, aux.Nom, aux.Tra);
                aux = aux.Sig;

            }
        }
        public void Mostrar(ComboBox cb)
        {
            clsNodo aux = Ini;
            cb.Items.Clear();
            while (aux != null)
            {
                cb.Items.Add(aux.Cod + " - " + aux.Nom + " - " + aux.Tra);
                aux = aux.Sig;

            }
        }


        public void Mostrar(ListBox lstCola)
        {
        
        /00clsNodo aux = Ini;
            lstCola.Items.Clear();
            while (aux != null)
            {
                lstCola.Items.Add(aux.Cod + " - " + aux.Nom + " - " + aux.Tra);
                aux = aux.Sig;

            }
        }
        */
    }
}
