using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoUsuario
    {
        Admin = 1,
        Cliente =2
    }
    public class Usuarios
    {

        public int IDUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Nombre { get; set; }


        public Usuarios()
        {
        }


        public Usuarios(int idusuario, string contraseña, string email, bool admin, string nombre)
        {
            this.IDUsuario = idusuario;
            this.Contraseña = contraseña;
            this.Email = email;
            TipoUsuario = admin ? TipoUsuario.Admin : TipoUsuario.Cliente;
            this.Nombre = nombre;
        }
    }
}
