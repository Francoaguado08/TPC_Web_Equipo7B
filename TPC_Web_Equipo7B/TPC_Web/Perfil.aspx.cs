using System;
using Dominio;
using Negocio;

namespace TPC_Web
{
    public partial class EditarPerfil : System.Web.UI.Page
    {
        // Instancias de las clases de negocio
        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        DatoPersonalNegocio datoPersonalNegocio = new DatoPersonalNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los datos del usuario logueado
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            // Verificar si hay sesión activa
            if (Session["IDUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Obtener el ID del usuario logueado
            int idUsuario = int.Parse(Session["IDUsuario"].ToString());

            // Obtener los datos personales y de usuario
            Usuario usuario = usuarioNegocio.listar().Find(u => u.IDUsuario == idUsuario);
            DatosPersonales datos = datoPersonalNegocio.Listar().Find(d => d.IDUsuario == idUsuario);

            if (usuario == null || datos == null)
            {
                Response.Write("<script>alert('Error: No se encontraron datos para el usuario.');</script>");
                return;
            }

            // Asignar los valores obtenidos a los campos del formulario
            txtNombre.Text = datos.Nombre;
            txtApellido.Text = datos.Apellido;
            txtEmail.Text = usuario.Email;
            txtDomicilio.Text = datos.Domicilio;
            txtTelefono.Text = datos.Telefono;
            txtPais.Text = datos.Pais;
            txtProvincia.Text = datos.Provincia;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            // Lógica para habilitar edición
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtEmail.Enabled = true;
            txtDomicilio.Enabled = true;
            txtTelefono.Enabled = true;
            txtPais.Enabled = true;
            txtProvincia.Enabled = true;

            btnEditar.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(Session["IDUsuario"].ToString());

                DatosPersonales datosActualizados = new DatosPersonales
                {
                    IDUsuario = idUsuario,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Domicilio = txtDomicilio.Text,
                    Telefono = txtTelefono.Text,
                    Pais = txtPais.Text,
                    Provincia = txtProvincia.Text
                };

                Usuario usuarioActualizado = new Usuario
                {
                    IDUsuario = idUsuario,
                    Email = txtEmail.Text
                };

                datoPersonalNegocio.Modificar(datosActualizados);
                usuarioNegocio.modificar(usuarioActualizado);

                Response.Redirect("Perfil.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al guardar los cambios: " + ex.Message + "');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Redirigir al perfil sin realizar cambios
            Response.Redirect("Perfil.aspx");
        }


    }
}
