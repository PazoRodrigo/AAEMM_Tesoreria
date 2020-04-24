<%@ Page Title="AAEMM. Reportes" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Reportes.aspx.vb" Inherits="Forms_Frm_Dist_Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Dist_Reportes.js?version20200416_1")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
            } else if (e.ctrlKey && e.altKey && (e.which == 67 || e.which == 99)) {
                // Ctrol + Alt + C
                redirect = 'Frm_ABM_CentroCostos.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 86 || e.which == 118)) {
                // Ctrol + Alt + V
                redirect = 'Frm_ABM_Convenios.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 85 || e.which == 117)) {
                // Ctrol + Alt + U
                redirect = 'Frm_ABM_CuentaContable.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 79 || e.which == 111)) {
                // Ctrol + Alt + O
                redirect = 'Frm_ABM_OriginarioGasto.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 84 || e.which == 116)) {
                // Ctrol + Alt + T
                redirect = 'Frm_ABM_TipoContacto.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 68 || e.which == 100)) {
                // Ctrol + Alt + D
                redirect = 'Frm_ABM_TipoDomicilio.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 80 || e.which == 112)) {
                // Ctrol + Alt + P
                redirect = 'Frm_ABM_TipoPago.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 82 || e.which == 114)) {
                // Ctrol + Alt + R
                redirect = 'Frm_ABM_Proveedores.aspx';
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
        <li>
            <li class="linea">
                <div class="container-fluid">
                    <div class="row mt-1">
                        <div class="col-lg-4">
                            <nav>
                                <ul class="Menu">
                                    <li class="BtnDistribuidor">
                                        <ul class="SubMenu">
                                            <%--<li><a href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesPropios.aspx")%>'>(Alt + P)  - Cheques Propios</a></li>
                                             <li><a href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesTerceros.aspx")%>'>(Alt + T)  - Cheques Terceros</a></li>--%>
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
        </li>
    </ul>
</asp:Content>

