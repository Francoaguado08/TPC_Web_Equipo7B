using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

//namespace TPC_Web
//{
////    public partial class AdministrarUsuario : System.Web.UI.Page
////    {
////        protected void Page_Load(object sender, EventArgs e)
////        {
////            if (!IsPostBack)
////            {
////                CargarUsuarios();
////            }
////        }

////        protected void CargarUsuarios()
////        {
////            try
////            {
////                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
////                List<Usuarios> listaUsuarios = usuarioNegocio.listar();

////                // Verifica si la lista contiene elementos
////                if (listaUsuarios.Count == 0)
////                {
////                    Response.Write("<script>alert('No se encontraron usuarios');</script>");
////                }

////                gvUsuarios.DataSource = listaUsuarios;
////                gvUsuarios.DataBind();
////            }
////            catch (Exception ex)
////            {
////                // Manejo de errores
////                Response.Write($"<script>alert('Error al cargar los usuarios: {ex.Message}');</script>");
////            }
////        }


////        protected void btnVolver_Click(object sender, EventArgs e)
////        {
////            Response.Redirect("Administrar.aspx");
////        }
////    }
////}
