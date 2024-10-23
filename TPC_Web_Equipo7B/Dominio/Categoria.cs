using System;

namespace Dominio
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public Categoria()
        {
            ID = 0;
            Descripcion = string.Empty;
        }

        public Categoria(int id, string descripcion)
        {
            ID = id;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
