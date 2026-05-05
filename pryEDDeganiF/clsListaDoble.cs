using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryEDDeganiF
{

    private Int32 codigo;
    private string nombre;
    private string trabajo;
    private clsNodo sig;
    private clsNodo ant;
    internal class clsListaDoble
    {

        private clsNodo pri;
        private clsNodo ult;

        public clsNodo Anterior { get { return ant; } set { ant = value; } }
        public clsNodo Primero { get { return pri; } set { pri = value; } }
        public clsNodo Ultimo { get { return ult; } set { ult = value; } }
        
        
        public void Agregar(clsNodo Nuevo)
        {
            if (Primero == null)
            {
                Primero = Nuevo;
                Ultimo = Nuevo;
            }
            else
            {
                Ultimo.Sig = Nuevo;
                Nuevo.Ant = Ultimo;
                Ultimo = Nuevo;
            }
        }
        public void Eliminar()
        {
            if (Primero.Codigo == codigo && Ultimo == Primero)
            {
                Primero = null;
                Ultimo = null;
            }
            else
            {
                if (Primero.Codigo == codigo)
                {
                    Primero = Primero.Sig;
                    Primero.Ant = null;
                }
                else if (Ultimo.Codigo == codigo)
                {
                    Ultimo = Ultimo.Ant;
                    Ultimo.Sig = null;
                }
                else
                {
                    clsNodo aux = Primero;
                    clsNodo ant = Primero;

                    while (aux.Codigo< codigo)
                    {
                        ant = aux;
                        aux = aux.Sig;
                    }
                    aux = aux.Sig;
                    aux.Ant = ant;
                    ant.Sig = aux;

                 
                }
            }
        }
        public void Mostrar(DataGridView DgvListaDoble)
        {
            clsNodo aux = Primero;
            DgvListaDoble.Rows.Clear();
            while (aux != null)
            {
                DgvListaDoble.Rows.Add(aux.Cod, aux.Nom, aux.Tra);
                aux = aux.Sig;
            }
        }
    }
}
