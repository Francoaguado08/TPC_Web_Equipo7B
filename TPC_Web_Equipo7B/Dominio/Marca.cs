using System;

namespace Dominio
{
    public class Marca
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public Marca()
        {
            ID = 0;
            Descripcion = string.Empty;
        }

        public Marca(int id, string nombre)
        {
            ID = id;
            Descripcion = nombre;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
