using System;

namespace Dominio
{
    public class Marca
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public Marca()
        {
            ID = 0;
            Nombre = string.Empty;
        }

        public Marca(int id, string nombre)
        {
            ID = id;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
