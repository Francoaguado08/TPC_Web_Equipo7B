<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gracias.aspx.cs" Inherits="TPC_Web.Gracias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="container text-center py-5">
        <div class="card shadow-lg border-0">
            <div class="card-body p-5">
                <h1 class="display-4 text-success mb-4">¡Gracias por tu compra!
                </h1>
                <p class="lead text-muted mb-4">
                    Tu pedido ha sido registrado exitosamente. Nos pondremos en contacto contigo pronto.
               
                </p>
                <p class="text-dark mb-4">
                    Si tienes alguna pregunta o deseas saber el estado de tu pedido, contáctanos por WhatsApp:
               
                </p>
   <a id="whatsappLink" runat="server" class="btn btn-success btn-lg shadow-sm" target="_blank">
    <i class="fab fa-whatsapp"></i> Contactar por WhatsApp
</a>
            </div>
        </div>


    </div>

</asp:Content>
