<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* ENCABEZADO */
        h1.display-4 {
            font-family: 'Poppins', sans-serif;
            font-weight: bold;
            background: linear-gradient(90deg, #007BFF, #6610f2);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
        }
        h2.display-5 {
            font-family: 'Roboto', sans-serif;
            color: #6c757d;
        }

        /* CONTENEDOR DE FILTROS */
        .bg-dark {
            background: linear-gradient(135deg, #343a40, #495057);
            border-radius: 8px;
        }
        .btn-primary {
            background-color: #007BFF;
            border-color: #007BFF;
        }
        .btn-secondary {
            background-color: #6c757d;
            border-color: #6c757d;
        }
        .form-select {
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        /* TARJETAS */
        .card {
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        }
        .card:hover {
            transform: scale(1.03);
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
        }
        .card-img-top {
            border-bottom: 2px solid #007BFF;
            border-radius: 8px 8px 0 0;
            height: 200px;
            object-fit: contain;
            background-color: #f8f9fa;
        }
        .card-body {
            background-color: #f8f9fa;
            padding: 20px;
            border-top: 1px solid #dee2e6;
        }

        /* BARRA DE CREDITOS EN EL PIE */
        .footer-bar {
            background: linear-gradient(90deg, #007BFF, #6610f2);
            color: white;
            text-align: center;
            padding: 20px 0;
            margin-top: 50px;
            position: relative;
            overflow: hidden;
        }
        .footer-bar::before {
            content: '';
            position: absolute;
            top: -20px;
            left: 0;
            width: 100%;
            height: 40px;
            background: white;
            border-radius: 0 0 50% 50%;
        }
        .footer-bar p {
            font-family: 'Roboto', sans-serif;
            font-size: 1rem;
            margin: 0;
            z-index: 1;
            position: relative;
        }
    </style>

    <div class="container mt-5">
        <div class="text-center mb-4">
            <h1 class="display-4">BIENVENIDOS A DIGITAL PLANET</h1>
            <h2 class="display-5">STOCK DISPONIBLE</h2>
        </div>

        <!-- FILTROS -->
        <div class="bg-dark text-white p-3 mb-4 d-flex justify-content-center align-items-center">
            <asp:Label ID="lblFiltrarPor" runat="server" Text="Filtrar por:" CssClass="me-3" />
            <asp:DropDownList ID="ddlFiltrarPor" runat="server" CssClass="form-select me-3"></asp:DropDownList>

            <asp:Label ID="lblCriterio" runat="server" Text="Criterio:" CssClass="me-3" />
            <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-select me-3" />

            <asp:Button ID="btnAplicarFiltro" runat="server" Text="Aplicar filtro" CssClass="btn btn-primary me-3" />
            <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar filtros" CssClass="btn btn-secondary me-3" />

            <div class="input-group ms-3">
                <asp:TextBox ID="tbxBuscar" CssClass="form-control" runat="server" />
                <asp:Button Text="Buscar" ID="btnBuscar" CssClass="btn btn-secondary" runat="server" />
            </div>
        </div>

        <!-- TARJETAS -->
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <% foreach (Dominio.Articulo arti in listaArticulo) { %>
            <div class="col">
                <div class="card h-100">
                    <%
                        string defaultUrl = "https://nayemdevs.com/wp-content/uploads/2020/03/default-product-image.png";
                        string imagenIndex = defaultUrl;

                        if (arti.ImagenURL != null && arti.ImagenURL.Any())
                        {
                            imagenIndex = arti.ImagenURL[0];
                        }
                    %>
                    <img src="<%= imagenIndex %>" class="card-img-top" alt="Imagen del Articulo <%= arti.Nombre %>" onerror="this.src='<%= defaultUrl %>'">
                    <div class="card-body">
                        <h5 class="card-title"><%= arti.Nombre %></h5>
                        <p class="card-text"><%= arti.Descripcion %></p>
                        <p class="card-text"><strong>ARS <%= arti.Precio %></strong></p>
                        <a href="DetalleArticulo.aspx?id=<%= arti.ID %>" class="btn btn-primary">Ver más</a>
                    </div> 
                </div>
            </div>
            <% } %>
        </div>
    </div>

    <!-- BARRA DE CREDITOS -->
    <div class="footer-bar">
        <p>© 2024 Digital Planet. Todos los derechos reservados. Desarrollado por "Equipo 7 B SRL" para "UTN FRGP".</p>
    </div>
</asp:Content>
