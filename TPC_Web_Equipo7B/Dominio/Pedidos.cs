using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedidos
    {

        public int ID { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IDUsuario { get; set; }


        public Pedidos()
        {
        }


        public Pedidos(int id, string estado, DateTime fechapedido, int idusuario)
        {
            this.ID = id;
            this.Estado = estado;
            this.FechaPedido = fechapedido;
            this.IDUsuario = idusuario;
    
        }
    }
}
