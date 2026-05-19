using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryEDDeganiF.Clases
{
    internal class clsArbolBinario
    {

        private clsNodo PrimerNodo;

        public clsNodo Raiz
        {
            get { return PrimerNodo; }
            set { PrimerNodo = value; }
        }

        public void Agregar(clsNodo nuevo)
        {
            if (Raiz == null)
            {
                Raiz = nuevo;
            }
            else
            {
                clsNodo Ant = Raiz; //ant
                clsNodo aux = Raiz;

                while (aux != null)
                {
                    Ant = aux;

                    if (nuevo.Codigo < aux.Codigo)
                    {
                        aux = aux.Izquierdo;
                    }
                    else
                    {
                        aux = aux.Derecho;
                    }
                }

                if (nuevo.Codigo < Ant.Codigo)
                {
                    Ant.Izquierdo = nuevo;
                }
                else
                {
                    Ant.Derecho = nuevo;
                }
            }
        }

    }
}


        


  

