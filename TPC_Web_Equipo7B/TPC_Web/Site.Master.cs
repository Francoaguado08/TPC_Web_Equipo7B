using System;
using System.Web.UI;

namespace TPC_Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int? idUsuario = Session["IDUsuario"] as int?;
                int? tipoUsuario = Session["tipoUsuario"] as int?;

                liLogin.Visible = idUsuario == null;
                liPerfil.Visible = idUsuario != null;
                btnLogout.Visible = idUsuario != null;

                if (idUsuario != null && tipoUsuario == 1)
                {
                    // Agregar dinámicamente el enlace de Administrar
                    var liAdministrar = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                    liAdministrar.Attributes["class"] = "nav-item";

                    var aAdministrar = new System.Web.UI.HtmlControls.HtmlAnchor
                    {
                        HRef = "Administrar.aspx",
                        InnerText = "Administrar"
                    };
                    aAdministrar.Attributes["class"] = "nav-link";

                    liAdministrar.Controls.Add(aAdministrar);
                    Navbar.Controls.Add(liAdministrar); // Navbar es el contenedor del menú.
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}