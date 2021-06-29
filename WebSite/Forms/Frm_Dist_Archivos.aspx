<%@ Page Title="AAEMM. Archivos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Archivos.aspx.vb" Inherits="Forms_Frm_Dist_Archivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Dist_Archivos.js?version=20210628_1")%>'></script>

    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
            } else if (e.ctrlKey && e.altKey && (e.which == 73 || e.which == 105)) {
                // Ctrol + Alt + I
                redirect =  '<%= ResolveClientUrl("~/Forms/Arc/Frm_Arc_Ingresos.aspx")%>';
            } 
            window.location = redirect;
        };
    </script>

    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>' class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc90_L">
                <span id="SpanNombreFormulario"></span>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1">
                    <div class="col-lg-4">
                        <nav>
                            <ul class="Menu">
                                <li class="BtnDistribuidor">
                                    <ul class="SubMenu">
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Arc/Frm_Arc_Ingresos.aspx")%>'>(Alt + I)  - Ingresos</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </li>
    </ul>

</asp:Content>

