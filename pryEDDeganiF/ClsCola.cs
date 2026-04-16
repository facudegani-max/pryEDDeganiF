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
        

        public void Mostrar(DataGridView Grilla)
        {
            ClsNodo aux = Ini;
            while (aux != null)
            {
                Console.WriteLine("Codigo: " + aux.Cod);
                Console.WriteLine("Nombre: " + aux.Nom);
                Console.WriteLine("Trabajo: " + aux.Tra);
                Console.WriteLine();
                aux = aux.Sig;
            }
        }
    }
}