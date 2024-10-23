using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int ID { get; set; }
        public string Codigo { get; set; } 
        public string Descripcion { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public string Nombre { get; set; } 
        public decimal Precio { get; set; }
        public string ImagenURL { get; set; }

        public Articulo()
        {
            ID = 0;
            Codigo = string.Empty;
            Descripcion = string.Empty;
            Categoria = new Categoria();
            Marca = new Marca();
            Nombre = string.Empty;
            Precio = 0;
            ImagenURL = string.Empty;
        }

        public Articulo(int id, string codigo, string descripcion, Categoria categoria, Marca marca, string nombre, decimal precio, string ImagenURL)
        {
            this.ID = id;
            this.Codigo = codigo;
            this.Descripcion = descripcion;
            Categoria = categoria;
            Marca = marca;
            this.Nombre = nombre;
            this.Precio = precio;
            ImagenURL = ImagenURL;
        }
    }

}
