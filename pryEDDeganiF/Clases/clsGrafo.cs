using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace pryEDDeganiF
{
    internal class clsGrafo
    {
        private readonly List<string> _nodos = new List<string>();
        private readonly Dictionary<string, Dictionary<string, decimal>> _adyacencia
            = new Dictionary<string, Dictionary<string, decimal>>();

        public IEnumerable<string> Nodos => _nodos.AsReadOnly();

        public void AgregarNodo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del nodo no puede estar vacío.", nameof(nombre));

            if (!_nodos.Contains(nombre))
            {
                _nodos.Add(nombre);
                _adyacencia[nombre] = new Dictionary<string, decimal>();
            }
        }

        public void AgregarArista(string origen, string destino, decimal precio)
        {
            AgregarNodo(origen);
            AgregarNodo(destino);
            _adyacencia[origen][destino] = precio;
        }

        public bool EliminarArista(string origen, string destino)
        {
            if (_adyacencia.ContainsKey(origen) && _adyacencia[origen].ContainsKey(destino))
            {
                return _adyacencia[origen].Remove(destino);
            }
            return false;
        }

        public decimal? ObtenerPrecio(string origen, string destino)
        {
            if (_adyacencia.ContainsKey(origen) && _adyacencia[origen].ContainsKey(destino))
                return _adyacencia[origen][destino];
            return null;
        }

        public List<KeyValuePair<string, decimal>> ListarDestinosDesde(string origen)
        {
            if (!_adyacencia.ContainsKey(origen))
                return new List<KeyValuePair<string, decimal>>();

            return _adyacencia[origen].Select(kv => new KeyValuePair<string, decimal>(kv.Key, kv.Value)).ToList();
        }

        public List<KeyValuePair<string, decimal>> ListarOrigenesHacia(string destino)
        {
            var resultado = new List<KeyValuePair<string, decimal>>();
            foreach (var nodo in _nodos)
            {
                if (_adyacencia.ContainsKey(nodo) && _adyacencia[nodo].ContainsKey(destino))
                    resultado.Add(new KeyValuePair<string, decimal>(nodo, _adyacencia[nodo][destino]));
            }
            return resultado;
        }

        // Convierte el grafo a un DataTable con columnas: Origen + cada nodo.
        // Cada celda contiene el precio si existe la arista, vacío en caso contrario.
        public DataTable ToDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("Origen", typeof(string));

            foreach (var nodo in _nodos)
                dt.Columns.Add(nodo, typeof(string));

            foreach (var origen in _nodos)
            {
                var row = dt.NewRow();
                row[0] = origen;
                for (int c = 0; c < _nodos.Count; c++)
                {
                    var destino = _nodos[c];
                    var precio = ObtenerPrecio(origen, destino);
                    row[c + 1] = precio.HasValue ? precio.Value.ToString("0.##") : string.Empty;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        // Convierte el grafo a un DataTable con columnas: Origen, Destino y Precio.
        public DataTable ToDataTableViajes()
        {
            var dt = new DataTable();
            dt.Columns.Add("Origen", typeof(string));
            dt.Columns.Add("Destino", typeof(string));
            dt.Columns.Add("Precio", typeof(string));

            foreach (var origen in _nodos)
            {
                if (_adyacencia.ContainsKey(origen))
                {
                    foreach (var kv in _adyacencia[origen])
                    {
                        var row = dt.NewRow();
                        row[0] = origen;
                        row[1] = kv.Key;
                        row[2] = kv.Value.ToString("0.##");
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        public void Limpiar()
        {
            _nodos.Clear();
            _adyacencia.Clear();
        }
    }
}
