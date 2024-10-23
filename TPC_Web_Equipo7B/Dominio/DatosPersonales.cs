using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DatosPersonales
    {

        public int ID { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Domicilio { get; set; }
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Provincia{ get; set; }
        public string Telefono{ get; set; }
        
        public DatosPersonales()
        {
        }


        public DatosPersonales(int id, string apellido, string dni, string domicilio, int idusuario, string nombre, string pais, string provincia, string telefono)
        {
            this.ID = id;
            this.Apellido = apellido;
            this.DNI = dni;
            this.Domicilio = domicilio;
            this.IDUsuario= idusuario;
            this.Nombre = nombre;
            this.Pais = pais;
            this.Provincia = provincia;
            this.Telefono = telefono;
        }
    }
}
