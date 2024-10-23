using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuarios
    {

        public int IDUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public int IDTipoUsuario { get; set; }
        public string Nombre { get; set; }


        public Usuarios()
        {
        }


        public Usuarios(int idusuario, string contraseña, string email, int idtipousuario, string nombre)
        {
            this.IDUsuario = idusuario;
            this.Contraseña = contraseña;
            this.Email = email;
            this.IDTipoUsuario = idtipousuario;
            this.Nombre = nombre;
        }
    }
}
