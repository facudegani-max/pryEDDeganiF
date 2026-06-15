using System;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace pryEDDeganiF
{
    internal class clsBaseDatos
    {
        private string cadenaConexion;

        public clsBaseDatos()
        {
            // Intentar ubicar Libreria.mdb en la carpeta BaseDatos dentro del directorio de la aplicación
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var posible = Path.Combine(baseDir, "BaseDatos", "Libreria.mdb");
                if (!File.Exists(posible))
                {
                    // intentar sin subcarpeta
                    posible = Path.Combine(baseDir, "Libreria.mdb");
                }

                if (File.Exists(posible))
                {
                    cadenaConexion = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={posible};Persist Security Info=False;";
                }
                else
                {
                    // fallback: usar nombre de archivo (puede funcionar si la app se ejecuta desde carpeta correcta)
                    cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Libreria.mdb;Persist Security Info=False;";
                }
            }
            catch
            {
                cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Libreria.mdb;Persist Security Info=False;";
            }
        }

        public DataTable EjecutarConsulta(string sql)
        {
            DataTable tabla = new DataTable();

            using (OleDbConnection conexion =
                new OleDbConnection(cadenaConexion))
            {
                OleDbDataAdapter adaptador =
                    new OleDbDataAdapter(sql, conexion);

                adaptador.Fill(tabla);
            }

            return tabla;
        }
    }
}