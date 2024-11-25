using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public enum TipoUsuario
        {
            Admin = 1,
            Cliente = 2
        }

        public int IDUsuario { get; set; }
        public string Email { get; set; }
        public string password { get; set; }

        // Cambiar a tipo int
        public int tipousuario { get; set; }

        public Usuario() { }

        public Usuario(string password, string username, string email)
        {
            this.password = password;
            this.Email = email;
        }
    }

}
