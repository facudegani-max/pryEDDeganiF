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

        // Recorridos publicos
        public List<clsNodo> InOrden()
        {
            var lista = new List<clsNodo>();
            InOrdenRec(Raiz, lista);
            return lista;
        }

        public List<clsNodo> PreOrden()
        {
            var lista = new List<clsNodo>();
            PreOrdenRec(Raiz, lista);
            return lista;
        }

        public List<clsNodo> PostOrden()
        {
            var lista = new List<clsNodo>();
            PostOrdenRec(Raiz, lista);
            return lista;
        }

        private void InOrdenRec(clsNodo nodo, List<clsNodo> lista)
        {
            if (nodo == null) return;
            InOrdenRec(nodo.Izquierdo, lista);
            lista.Add(nodo);
            InOrdenRec(nodo.Derecho, lista);
        }

        private void PreOrdenRec(clsNodo nodo, List<clsNodo> lista)
        {
            if (nodo == null) return;
            lista.Add(nodo);
            PreOrdenRec(nodo.Izquierdo, lista);
            PreOrdenRec(nodo.Derecho, lista);
        }

        private void PostOrdenRec(clsNodo nodo, List<clsNodo> lista)
        {
            if (nodo == null) return;
            PostOrdenRec(nodo.Izquierdo, lista);
            PostOrdenRec(nodo.Derecho, lista);
            lista.Add(nodo);
        }

        // Obtiene todos los nodos en InOrden (util para reconstruir)
        public List<clsNodo> ObtenerTodos()
        {
            return InOrden();
        }

        // Elimina un nodo reconstruyendo el arbol sin el codigo indicado
        public void Eliminar(int codigo)
        {
            var todos = ObtenerTodos()
                .Where(n => n.Codigo != codigo)
                .Select(n => new clsNodo { Codigo = n.Codigo, Nombre = n.Nombre, Tramite = n.Tramite })
                .ToList();

            Raiz = null;
            foreach (var n in todos)
                Agregar(n);
        }

        // Equilibra el arbol construyendo uno balanceado desde la lista ordenada
        public void Equilibrar()
        {
            var ordenados = ObtenerTodos()
                .Select(n => new clsNodo { Codigo = n.Codigo, Nombre = n.Nombre, Tramite = n.Tramite })
                .OrderBy(n => n.Codigo)
                .ToList();

            Raiz = BuildBalanced(ordenados, 0, ordenados.Count - 1);
        }

        private clsNodo BuildBalanced(List<clsNodo> list, int inicio, int fin)
        {
            if (inicio > fin) return null;
            int mid = (inicio + fin) / 2;
            var nodo = list[mid];
            nodo.Izquierdo = BuildBalanced(list, inicio, mid - 1);
            nodo.Derecho = BuildBalanced(list, mid + 1, fin);
            return nodo;
        }
    }
}


        


  

