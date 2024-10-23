using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetallesPedidos
    {
   
        public int IDDetallePedido { get; set; }
        public int Cantidad { get; set; }
        public int IDArticulo { get; set; }
        public int IDPedido { get; set; }
        public decimal PrecioUnitario { get; set; }

       
        public DetallesPedidos()
        {
        }

    
        public DetallesPedidos(int idDetallePedido, int cantidad, int idArticulo, int idPedido, decimal precioUnitario)
        {
            this.IDDetallePedido = idDetallePedido;
            this.Cantidad = cantidad;
            this.IDArticulo = idArticulo;
            this.IDPedido = idPedido;
            this.PrecioUnitario = precioUnitario;
        }
    }
}
