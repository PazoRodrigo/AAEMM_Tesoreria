<%@ Page Title="" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Rep_IngresosMensualesMenu.aspx.vb" Inherits="Forms_Reportes_Frm_Rep_IngresosMensualesMenu" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Rep_IngresosMensualesMenu.js?version=20210811")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_IngresosMensuales.aspx")%>';
                window.location = redirect;
            }
        };
    </script>
    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario">Reporte Ingresos Mensuales</span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>' class="LinkBtn"
                    title="Volver a Reportes"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1">
                    <div class="col-lg-4">
                        <nav>
                            <ul>
                                <li>
                                    <div class="row">
                                        <div class="col-lg-3">Año :</div>
                                        <div class="col-lg-9">
                                            <div class="form-group">
                                                <div id="CboAño"></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div class="row">
                                        <div class="col-lg-3">Mes :</div>
                                        <div class="col-lg-9">
                                            <div class="form-group">
                                                <div id="CboMes"></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div class="Boton BtnImprimir">
                                        <a id="LinkBtnImprimir" href="#">Imprimir</a>
                                    </div>
                                </li>
                            </ul>
                        </nav>
                    </div>

                    <%-- <div class="col-lg-4">
                        <nav>
                            <ul class="Menu">
                                <li class="BtnDistribuidor">
                                    <ul class="SubMenu">
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202007")%>'>Julio / 2020</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202008")%>'>Agosto / 2020</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202009")%>'>Septiembre / 2020</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202010")%>'>Octubre / 2020</a></li>
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
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202011")%>'>Noviembre / 2020</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202012")%>'>Diciembre / 2020</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202101")%>'>Enero / 2021</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202102")%>'>Febrero / 2021</a></li>
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
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202103")%>'>Marzo / 2021</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202104")%>'>Abril / 2021</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202105")%>'>Mayo / 2021</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=202106")%>'>Junio / 2021</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>--%>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

