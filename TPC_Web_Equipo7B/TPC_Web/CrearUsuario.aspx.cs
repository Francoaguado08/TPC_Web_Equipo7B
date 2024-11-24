using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class CrearUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Página sin lógica en Page_Load por ahora
        }

        protected void btnValidarEmail_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                lblEmailError.Text = "Por favor, ingrese un correo válido.";
                lblEmailError.Visible = true;
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            if (negocio.EmailExiste(email)) // Método en la capa de negocio
             {
        lblEmailError.Text = "El correo ya está registrado.";
        lblEmailError.Visible = true;

        // Hacer visible el botón "Aceptar"
        btnAceptar.Visible = true;
             }
            else
            {
                lblEmailError.Visible = false;
                userDetailsSection.Visible = true; // Muestra el formulario completo
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e) 
        {
            
                Response.Redirect("Login.aspx");
            
        }

    }
}