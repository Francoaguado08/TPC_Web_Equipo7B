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
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

        
        //Es una lista de URLS COMO ATRIBUTO.
        public List<string> ImagenURL { get; set; } = new List<string>();
    }

}
