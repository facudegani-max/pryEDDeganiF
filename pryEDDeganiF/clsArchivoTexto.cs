using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
        public void Recorrer(ListBox lst)
        {
            String DatoLeido;
            lst.Items.Clear();
            StreamReader AD = new StreamReader(NombreArchivo);
            DatoLeido = AD.ReadLine();

            while (DatoLeido != null)
            {
                lst.Items.Add(DatoLeido);
                DatoLeido = AD.ReadLine();
            }
            AD.Close();
        }
        public void Grabar(string Dato1, string Dato2, String Dato3)
        {
            StreamWriter AD = new StreamWriter(NombreArchivo, true);
            AD.WriteLine(Dato1);
            AD.Write(";");
            AD.WriteLine(Dato2);
            AD.Write(";");
            AD.WriteLine(Dato3);
            AD.Write(";");
            AD.Close();
        }

        public void Recorrer(ComboBox lst)
        {
            String DatoLeido;
            lst.Items.Clear();
            StreamReader AD = new StreamReader(NombreArchivo);
            DatoLeido = AD.ReadLine();

            while (DatoLeido != null)
            {
                lst.Items.Add(DatoLeido);
                DatoLeido = AD.ReadLine();
            }
            AD.Close();
            lst.SelectedIndex = 0;

        }

        public void Recorrer(DataGridView Grilla)
        {
            String DatoLeido;
            Grilla.Rows.Clear();
            StreamReader AD = new StreamReader(NombreArchivo);
            DatoLeido = AD.ReadLine();

            while (DatoLeido != null)
            {
                Grilla.Rows.Add(DatoLeido.Split(';'));
                DatoLeido = AD.ReadLine();
            }
            AD.Close();

        }
    }
}
