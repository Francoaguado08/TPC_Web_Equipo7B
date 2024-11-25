using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Web
{
    public partial class PersonalesDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {






        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DatosPersonales datos = new DatosPersonales
            {
                IDUsuario = int.Parse(Request.QueryString["IDUsuario"]),
                DNI = txtDNI.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Domicilio = txtDomicilio.Text,
                Pais = "Argentina", // o seleccionable
                Provincia = "Buenos Aires", // o seleccionable
                Telefono = txtTelefono.Text
            };

            DatoPersonalNegocio negocio = new DatoPersonalNegocio();    
            negocio.Agregar(datos);

            Response.Redirect("Compras.aspx");



        }
    }
}