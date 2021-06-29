<%@ Page Title="AAEMM. Administración" Language="VB"
    MasterPageFile="~/Forms/MP.master" AutoEventWireup="false"
    CodeFile="Frm_Dist_Administracion.aspx.vb"
    Inherits="Forms_Frm_Dist_Administracion" %>

<asp:Content
    ID="Contenido"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <script
        src='<%= ResolveClientUrl("Frm_Dist_Administracion.js?version=20210628_1")%>'></script>

    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
        } else if (e.ctrlKey && e.altKey && (e.which == 69 || e.which == 101)) {
            // Ctrol + Alt + E
            redirect =  '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Empresa.aspx")%>';
        } else if (e.ctrlKey && e.altKey && (e.which == 72 || e.which == 104)) {
            // Ctrol + Alt + H
            redirect = '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Chequera.aspx")%>';
        } else if (e.ctrlKey && e.altKey && (e.which == 73 || e.which == 105)) {
            // Ctrol + Alt + I
            redirect =  '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Ingreso.aspx")%>';
        } else if (e.ctrlKey && e.altKey && (e.which == 77 || e.which == 109)) {
            // Ctrol + Alt + M
            redirect =  '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Empleado.aspx")%>';
       <%-- } else if (e.ctrlKey && e.altKey && (e.which == 80 || e.which == 112)) {
            // Ctrol + Alt + P
            redirect =  '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesPropios.aspx")%>';
        } else if (e.ctrlKey && e.altKey && (e.which == 84 || e.which == 116)) {
            // Ctrol + Alt + T
            redirect =  '<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesTerceros.aspx")%>';--%>
            } else if (e.ctrlKey && e.altKey && (e.which == 67 || e.which == 99)) {
                // Ctrol + Alt + C
                redirect = '/Adm/Frm_Adm_ChequesPropios.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 79 || e.which == 111)) {
                // Ctrol + Alt + O
                redirect = '/DIM/Frm_ABM_OriginarioGasto.aspx';
            } else if (e.ctrlKey && e.altKey && (e.which == 68 || e.which == 100)) {
                // Ctrol + Alt + D
                redirect = '/DIM/Frm_ABM_TipoDomicilio.aspx';
            }
            window.location = redirect;
        };
    </script>
    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a
                    id="LinkBtnInidicadores"
                    href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn"
                    title="Indicadores"><span class="icon-stats-dots"></span></a>
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
                                        <li>
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Empresa.aspx")%>'>(Alt + E) - Empresas</a>
                                        </li>
                                        <li>
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Empleado.aspx")%>'>(Alt + M) - Empleados</a>
                                        </li>
                                        <%--
                    <li>
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Chequera.aspx")%>'
                        >(Alt + H) - Chequera</a
                      >
                    </li>
                    <li>
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesPropios.aspx")%>'
                        >(Alt + P) - Cheques Propios</a
                      >
                    </li>

                                        --%>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="col-lg-4">
                        <nav>
                            <ul class="Menu">
                                <li class="BtnDistribuidor">
                                    <ul class="SubMenu">
                                        <li>
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_ChequesTerceros.aspx")%>'>(Alt + T) - Cheques de Terceros</a>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="col-lg-4">
                        <nav>
                            <ul class="Menu">
                                <li class="BtnDistribuidor">
                                    <ul class="SubMenu">
                                        <li>
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_CuentaCorriente.aspx")%>'>(Alt + A) - Cuentas Corrientes</a>
                                        </li>
                                         <li>
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Adm/Frm_Adm_Sueldos.aspx")%>'>Sueldos</a>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>
