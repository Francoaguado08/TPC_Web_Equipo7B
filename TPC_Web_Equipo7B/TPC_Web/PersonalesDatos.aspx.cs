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
            if (!IsPostBack)
            {
                if (Request.QueryString["IDUsuario"] == null || !int.TryParse(Request.QueryString["IDUsuario"], out _))
                {
                    // Manejar el error, redirigir o mostrar mensaje
                    Response.Redirect("CrearUsuario");
                }
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["IDUsuario"], out int idUsuario))
            {
                // Manejar el error, mostrar mensaje al usuario o redirigir
                Response.Redirect("CrearUsuario");
                return;
            }

            DatosPersonales datos = new DatosPersonales
            {
                IDUsuario = idUsuario,
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

            Response.Redirect("Default.aspx");
        }

    }
}