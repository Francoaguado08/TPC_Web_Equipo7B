<%@ Page Title="Detalles Artículo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPC_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Estilo general */
        .detalle-articulo-container {
            display: flex;
            align-items: flex-start;
            gap: 20px; /* Espaciado entre imagen y detalles */
        }

        /* Ajuste de la imagen */
        .img-small {
            max-width: 300px; /* Ancho máximo de la imagen */
            max-height: 300px; /* Altura máxima de la imagen */
            object-fit: cover; /* Mantiene proporciones */
            border-radius: 8px; /* Bordes redondeados */
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Sombra */
        }

        /* Tarjeta de detalles */
        .detalle-card {
            flex: 1; /* Ocupa el espacio restante */
        }

        .detalle-card-header {
            background-color: #007bff; /* Color de fondo (azul Bootstrap) */
            color: white; /* Color del texto */
            padding: 10px;
            border-radius: 8px 8px 0 0;
            text-align: center;
            font-weight: bold;
        }

        .detalle-card-body {
            border: 1px solid #ddd;
            border-top: none;
            padding: 20px;
            border-radius: 0 0 8px 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1); /* Sombra */
        }

        /* Botones */
        .detalle-buttons {
            margin-top: 20px;
            text-align: center;
        }
    </style>

    <div class="container mt-5">
        <!-- Título de la página -->
        <h1 class="text-center mb-4">Detalle del Artículo</h1>

        <!-- Contenedor principal -->
        <div class="detalle-articulo-container">
            <!-- Imagen del artículo -->
            <div>
                <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <%  
                            string defaultUrl = "https://nayemdevs.com/wp-content/uploads/2020/03/default-product-image.png";
                            Negocio.ImagenesNegocio negocioObj = new Negocio.ImagenesNegocio();

                            int idArticulo = int.Parse(Request.QueryString["id"]);
                            List<Dominio.Imagen> listaImagenes = negocioObj.listarImagenesArticuloSeleccionado(idArticulo);

                            if (listaImagenes.Count == 0)
                            { %>
                        <div class="carousel-item active">
                            <img src="<%= defaultUrl %>" class="d-block img-small" alt="Imagen predeterminada">
                        </div>
                        <%  }
                            else
                            {
                                for (int i = 0; i < listaImagenes.Count; i++)
                                { %>
                        <div class="carousel-item <%=(i == 0) ? "active" : "" %>">
                            <img src="<%= listaImagenes[i].ImagenURl %>" class="d-block img-small" alt="Imagen del producto"
                                 onerror="this.src='<%= defaultUrl %>'">
                        </div>
                        <%      }
                            } %>
                    </div>

                    <!-- Controles del carrusel -->
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Anterior</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Siguiente</span>
                    </button>
                </div>
            </div>

            <!-- Detalles del artículo -->
            <div class="detalle-card">
                <div class="detalle-card-header">Información del Artículo</div>
                <div class="detalle-card-body">
                    <p><strong>Nombre:</strong> <asp:Label ID="txtNombre" runat="server" /></p>
                    <p><strong>Código:</strong> <asp:Label ID="txtCodigo" runat="server" /></p>
                    <p><strong>Precio:</strong> <asp:Label ID="txtPrecio" runat="server" /></p>
                    <p><strong>Descripción:</strong> <asp:Label ID="txtDescripcion" runat="server" /></p>
                    <p><strong>Marca:</strong> <asp:Label ID="txtMarca" runat="server" /></p>
                    <p><strong>Categoría:</strong> <asp:Label ID="txtCategoria" runat="server" /></p>
                </div>
            </div>
        </div>

        <!-- Botones de acción -->
        <div class="detalle-buttons">
            <a href="Default.aspx" class="btn btn-secondary me-2">Volver al Inicio</a>
            <% 
                string agregarAlCarrito = string.Format("Compras.aspx?id={0}", idArticulo);
            %>
            <a href="<%= agregarAlCarrito %>" class="btn btn-primary">Agregar al Carrito</a>
        </div>
    </div>
</asp:Content>
