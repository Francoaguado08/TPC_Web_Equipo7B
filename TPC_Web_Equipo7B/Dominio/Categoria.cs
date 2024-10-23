using System;

namespace Dominio
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre{ get; set; }

        public Categoria()
        {
            ID = 0;
            Nombre = string.Empty;
        }

        public Categoria(int id, string descripcion)
        {
            ID = id;
            Nombre = descripcion;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
