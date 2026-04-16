using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryEDDeganiF
{
    internal class ClsNodo
    {
        //Campos del Nodo

        private Int32 cod;
        private String nom;
        private String tra;
        private ClsNodo sig;

        //Propiedades del Nodo
        public Int32 Cod { get { return cod; } set { cod = value; } }
        public String Nom { get { return Nom; } set { Nom = value; } }
        public String Tra { get { return Tra; } set { Tra = value; } }

        public ClsNodo Sig { get { return sig; } set { sig = value; } }

    }
}