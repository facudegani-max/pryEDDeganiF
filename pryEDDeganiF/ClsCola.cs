using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryEDDeganiF
{
    internal class ClsCola
    {
        private ClsNodo ini;
        private ClsNodo fin;

        public ClsNodo Ini { get { return ini; } set { ini = value; } }
        public ClsNodo Fin { get { return fin; } set { fin = value; } }



        public void Agregar(ClsNodo Nuevo)
        {
            if (Ini == null)
            {
                Ini = Nuevo;
                Fin = Nuevo;
            }
            else
            {
                Fin.Sig = Nuevo;
                Fin = Nuevo;
            }
        }

        public void Eliminar()
        {
            if (Ini == Fin)
            {
                Ini = null;
                Fin = null;
            }
            else
            {
                Ini = Ini.Sig;
            } 
        }
        

        public void Mostrar(DataGridView DgvCola)
        {
            ClsNodo aux = Ini;
            DgvCola.Rows.Clear();
            while (aux != null)
            {
                DgvCola.Rows.Add(aux.Cod, aux.Nom, aux.Tra);
                aux = aux.Sig;

            }
        }
        public void Mostrar(ComboBox cb)
        {
            ClsNodo aux = Ini;
            cb.Items.Clear();
            while (aux != null)
            {
                cb.Items.Add(aux.Cod + " - " + aux.Nom + " - " + aux.Tra);
                aux = aux.Sig;

            }
        }


        public void Mostrar(ListBox lstCola)
        {
            ClsNodo aux = Ini;
            lstCola.Items.Clear();
            while (aux != null)
            {
                lstCola.Items.Add(aux.Cod + " - " + aux.Nom + " - " + aux.Tra);
                aux = aux.Sig;

            }
        }
    }
}