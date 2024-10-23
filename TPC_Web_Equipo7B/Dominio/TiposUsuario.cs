using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoUsuarios
    {

        public int IDTipoUsuario { get; set; }
        public string Nombre { get; set; }
       


        public TipoUsuarios()
        {
        }


        public TipoUsuarios(int idtipousuario, string nombre)
        {
            this.IDTipoUsuario= idtipousuario;
            this.Nombre= nombre;
            
        }
    }
}
