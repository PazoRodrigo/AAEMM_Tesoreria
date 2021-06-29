<%@ Page Title="AAEMM. Reportes" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Reportes.aspx.vb" Inherits="Forms_Frm_Dist_Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Dist_Reportes.js?version=20210628_1")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
            } else if (e.ctrlKey && e.altKey && (e.which == 73 || e.which == 105)) {
                // Ctrol + Alt + I
                redirect = '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Ingresos.aspx")%>';
            } else if (e.ctrlKey && e.altKey && (e.which == 71 || e.which == 103)) {
                // Ctrol + Alt + G
                redirect = '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Gastos.aspx")%>';
            } else if (e.ctrlKey && e.altKey && (e.which == 67 || e.which == 99)) {
                // Ctrol + Alt + C
                redirect = '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Comprobantes.aspx")%>';
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
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Ingresos.aspx")%>'>(Alt + I) - Ingresos</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensualesMenu.aspx")%>'>(Alt + M) - Ingresos Mensuales</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Gasto.aspx")%>'>(Alt + G) - Gastos</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Comprobante.aspx")%>'>(Alt + C) - Comprobantes</a></li>
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

