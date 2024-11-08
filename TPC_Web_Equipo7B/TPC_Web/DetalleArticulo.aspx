<%@ Page Title="Detalles Articulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPC_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



  
    <!-- Carrusel de Imágenes -->
    <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-inner">
            <%  
                // URL predeterminada si no hay imágenes para el artículo.
                string defaultUrl = "https://nayemdevs.com/wp-content/uploads/2020/03/default-product-image.png";
                Negocio.ImagenesNegocio negocioObj = new Negocio.ImagenesNegocio();

                // Obtén el ID del artículo de la cadena de consulta y las imágenes asociadas.
                int idArticulo = int.Parse(Request.QueryString["id"]);
                List<Dominio.Imagen> listaImagenes = negocioObj.listarImagenesArticuloSeleccionado(idArticulo);

                if (listaImagenes.Count == 0)
                { %>
                <!-- Imagen predeterminada si no hay imágenes en la lista -->
                <div class="carousel-item active">
                    <img src="<%= defaultUrl %>" class="d-block w-100" alt="Imagen predeterminada" style="height: auto; width: 100%;">
                </div>
            <%  }
                else
                {
                    // Itera sobre cada imagen y añade un nuevo item al carrusel.
                    for (int i = 0; i < listaImagenes.Count; i++)
                    { %>
                <div class="carousel-item <%= (i == 0) ? "active" : "" %>">
                    <img src="<%= listaImagenes[i].ImagenURl %>" class="d-block w-100" alt="Imagen del producto"
                         onerror="this.src='<%= defaultUrl %>'" style="height: auto; width: 100%;">
                </div>
            <%      }
                } %>
        </div>

        <!-- Controles del carrusel -->
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <!-- Bootstrap JS (para el carrusel) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>










    <%--------------------DESCRIPCION DEL ARTICULO ( LABELS ) --%>
    <div class="card" style="width: 18rem;">
        <div class="card-header">
            Detalle del aticulo
        </div>
        <asp:Label ID="txtNombre" runat="server" />
    </div>
    <div class="card" style="width: 18rem;">
        <asp:Label ID="txtCodigo" runat="server" />
    </div>
    <div class="card" style="width: 18rem;">
        <asp:Label ID="txtPrecio" runat="server" />
    </div>
    <div class="card" style="width: 18rem;">
        <asp:Label ID="txtDescripcion" runat="server" />
    </div>
    <div class="card" style="width: 18rem;">
        <asp:Label ID="txtMarca" runat="server" />
    </div>
    <div class="card" style="width: 18rem;">
        <asp:Label ID="txtCategoria" runat="server" />
    </div>
    <%-----------------DESCRIPCION DEL ARTICULO ( LABELS ) --%>



    <div>
        <a href="Default.aspx" class="btn btn-primary">Volver al Inicio</a>

    </div>
    <br />
    <br />
    <br />


     <% 
        // Obtener el ID del artículo de la cadena de consulta
        int id = int.Parse(Request.QueryString["id"]);

        // Usar el ID para crear el enlace "Ver más"
        string agregarAlCarrito = string.Format("Compras.aspx?id={0}", idArticulo);
    %>
    <a href="<%= agregarAlCarrito %>" class="btn btn-primary">Agregar al Carrito</a>








</asp:Content>
