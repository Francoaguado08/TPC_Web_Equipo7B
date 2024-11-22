using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class Pedido
    {
        public int ID { get; set; }
        public int IDUsuario { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; } // Ej: "Pendiente", "Confirmado", "Cancelado"
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();



    }
}
