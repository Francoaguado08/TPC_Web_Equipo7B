using System;
using System.Collections.Generic;
using Negocio;
using Dominio;
using System.Web.UI.WebControls;

namespace TPC_Web
{
    public partial class AdministrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? idUsuario = Session["IDUsuario"] as int?;
            int? tipoUsuario = Session["tipoUsuario"] as int?;

            if (idUsuario == null || tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        protected void CargarUsuarios()
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                List<Usuario> listaUsuarios = usuarioNegocio.listar();

                if (listaUsuarios.Count == 0)
                {
                    Response.Write("<script>alert('No se encontraron usuarios');</script>");
                }

                gvUsuarios.DataSource = listaUsuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al cargar los usuarios: {ex.Message}');</script>");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrar.aspx");
        }

        protected void gvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsuarios.EditIndex = e.NewEditIndex;
            CargarUsuarios();
        }

        protected void gvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsuarios.EditIndex = -1;
            CargarUsuarios();
        }

        protected void gvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idUsuario = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvUsuarios.Rows[e.RowIndex];
            DropDownList ddlRol = (DropDownList)row.FindControl("ddlRol");
            int nuevoRol = Convert.ToInt32(ddlRol.SelectedValue);

            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.modificarRol(idUsuario, nuevoRol);

                gvUsuarios.EditIndex = -1;
                CargarUsuarios();
                Response.Write("<script>alert('Rol actualizado correctamente');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al actualizar el rol: {ex.Message}');</script>");
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarUsuario")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idUsuario = Convert.ToInt32(gvUsuarios.DataKeys[index].Value); // Obtener IDUsuario

                try
                {
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    usuarioNegocio.eliminar(idUsuario); // Método para eliminar el usuario
                    CargarUsuarios();
                    Response.Write("<script>alert('Usuario eliminado correctamente');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error al eliminar el usuario: {ex.Message}');</script>");
                }
            }


        }

    }   
}