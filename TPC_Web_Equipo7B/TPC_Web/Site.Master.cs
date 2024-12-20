﻿using System;
using System.Web.UI;
using Negocio;

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
                liHistorialPedidos.Visible = idUsuario != null;  // Visibilidad de Historial de Pedidos

                if (idUsuario != null)
                {
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    string nombreUsuario = usuarioNegocio.ObtenerNombreUsuario((int)idUsuario);

                    if (!string.IsNullOrEmpty(nombreUsuario))
                    {
                        // Cambia el texto y el enlace de "Perfil"
                        aPerfil.InnerText = $"Hola, {nombreUsuario}";
                        aPerfil.HRef = "Perfil.aspx"; // Enlace a la página de perfil
                    }

                    if (tipoUsuario == 1)
                    {
                        // Agregar opciones adicionales solo para tipoUsuario == 1 (por ejemplo, Administrar y Estadísticas)
                        var liAdministrar = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                        liAdministrar.Attributes["class"] = "nav-item";

                        var aAdministrar = new System.Web.UI.HtmlControls.HtmlAnchor
                        {
                            HRef = "Administrar.aspx",
                            InnerText = "Administrar"
                        };
                        aAdministrar.Attributes["class"] = "nav-link";

                        liAdministrar.Controls.Add(aAdministrar);
                        Navbar.Controls.Add(liAdministrar);

                        // Opción "Estadísticas"
                        var liEstadisticas = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                        liEstadisticas.Attributes["class"] = "nav-item";

                        var aEstadisticas = new System.Web.UI.HtmlControls.HtmlAnchor
                        {
                            HRef = "Estadisticas.aspx",
                            InnerText = "Estadísticas"
                        };
                        aEstadisticas.Attributes["class"] = "nav-link";

                        liEstadisticas.Controls.Add(aEstadisticas);
                        Navbar.Controls.Add(liEstadisticas);
                    }
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
