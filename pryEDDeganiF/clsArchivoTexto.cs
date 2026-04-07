using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pryEDDeganiF
{
    internal class clsArchivoTexto
    {

        public string NombreArchivo = "ArchivoTexto.txt";


        public void Grabar() 
        {
            StreamWriter AD = new StreamWriter(NombreArchivo, true);
            AD.WriteLine("Hola Mundo");
            AD.Close();
        }

        public void Grabar(string Dato)
        {
            StreamWriter AD = new StreamWriter(NombreArchivo, true);
            AD.WriteLine(Dato);
            AD.Close();
        }
        public void Grabar(string Nombre, string Codigo)
        {
            StreamWriter AD = new StreamWriter(NombreArchivo, true);
            AD.WriteLine(Nombre);
            AD.Write(";");
            AD.Write(Codigo);
            AD.Close();
        }
        public void Recorrer()
        {

        }

    }
}
