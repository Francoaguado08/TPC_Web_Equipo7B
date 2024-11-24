using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using static System.Collections.Specialized.BitVector32;

namespace TPC_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener tipoUsuario de la sesión
                string tipoUsuario = Session["tipoUsuario"] as string;
                string idUsuario = Session["IDUsuario"] as string; // Asegúrate de guardar esto al loguear al usuario

                // Encontrar elementos del menú
                var loginLink = FindControl("liLogin") as HtmlGenericControl;
                var adminLink = FindControl("liAdmin") as HtmlGenericControl;
                var userLink = FindControl("liUser") as HtmlGenericControl;
                var logoutLink = FindControl("btnLogout") as HtmlGenericControl;

                // Lógica de visibilidad
                if (!string.IsNullOrEmpty(tipoUsuario))
                {
                    if (loginLink != null) loginLink.Visible = false; // Ocultar botón Login
                    if (logoutLink != null) logoutLink.Visible = true; // Mostrar botón Cerrar Sesión

                    if (tipoUsuario == "1") // Admin
                    {
                        if (adminLink != null) adminLink.Visible = true;
                    }
                    else if (tipoUsuario == "2") // Cliente
                    {
                        if (adminLink != null) adminLink.Visible = false;
                    }

                    // Mostrar nombre del usuario
                    if (userLink != null)
                    {
                        userLink.Visible = true;
                        var linkUserName = FindControl("linkUserName") as HtmlAnchor;
                        if (linkUserName != null && !string.IsNullOrEmpty(idUsuario))
                        {
                            linkUserName.InnerText = ObtenerNombreUsuario(idUsuario);
                        }
                    }
                }
                else
                {
                    if (loginLink != null) loginLink.Visible = true; // Mostrar botón Login
                    if (logoutLink != null) logoutLink.Visible = false; // Ocultar botón Cerrar Sesión
                    if (adminLink != null) adminLink.Visible = false; // Ocultar Administración
                    if (userLink != null) userLink.Visible = false; // Ocultar Usuario
                }
            }
        }

        private string ObtenerNombreUsuario(string idUsuario)
        {
            string nombre = "Usuario";
            string connectionString = "tu_cadena_de_conexion"; // Asegúrate de reemplazar esto
            string query = "SELECT Nombre FROM DatosPersonales WHERE IDUsuario = @IDUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDUsuario", idUsuario);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        nombre = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores
                }
            }

            return nombre;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Redirigir al login
            Response.Redirect("Login.aspx");
        }
    }
}
