using System;

namespace Dominio
{
    public class Imagen
    {
        public int ID { get; set; }
        public int IDArticulo { get; set; }
        public string ImagenUrl { get; set; }

        public Imagen()
        {
            IDArticulo = 0;
            ImagenUrl = string.Empty;
        }
    }
}
