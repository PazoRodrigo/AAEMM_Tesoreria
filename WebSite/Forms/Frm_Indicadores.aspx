<%@ Page Title="AAEMM. Indicadores" Language="VB"
    MasterPageFile="~/Forms/MP.master" AutoEventWireup="false"
    CodeFile="Frm_Indicadores.aspx.vb" Inherits="Forms_Frm_Indicadores" %>

<%@ Register
    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="cc1" %>
<asp:Content
    ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <script type="text/javascript">

</script>
    <script src='<%= ResolveClientUrl("Eventos_Formularios.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("Frm_Indicadores.js?version=20210717")%>'></script>
    <div id="PopUp">
    </div>

    <asp:Button
        ID="btnSubirOculto"
        runat="server"
        Style="visibility: hidden; display: none;" />

    <div class="container-fluid">

        <div class="row">
            <div class="d-none d-md-block col-12">
                <div id="DivNombreFormulario100">
                    <span id="NombreFormulario"></span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-6">
                        <nav>
                            <ul>
                                <li class="MenuIndicador">
                                    <a href="#" id="FORM0_RECAUDACIONNETA" style="display: none;">
                                        <div class="LblIndicador">Recaudación Neta</div>
                                        <div class="LblValorIndicador" id="LblRecaudacionNeta"></div>
                                    </a>
                                    <ul class="SubMenuInidicador">
                                    </ul>
                                </li>
                                <li class="MenuIndicador" style="margin-top: 2px">
                                    <a href="#" id="FORM0_RECAUDACIONBRUTA" style="display: none;">
                                        <div class="LblIndicador">Recaudación Bruta</div>
                                        <div
                                            class="LblValorIndicador"
                                            id="LblRecaudacionBruta">
                                        </div>
                                    </a>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="col-md-6">
                        <nav>
                            <ul>
                                <li class="MenuIndicador">
                                    <a href="#" id="Indicadores_Empresas" style="display: block;">
                                        <div class="LblIndicador">Empresas</div>
                                        <div class="LblValorIndicador" id="LblEmpresas"></div>
                                    </a>
                                    <ul class="SubMenuInidicador">
                                        <li class="BtnIndicador">
                                            <a href="#" id="Indicadores_Empresas_SinDeuda" style="display: block;">
                                                <div class="LblIndicador">Sin Deuda</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasSinDeuda">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="Indicadores_Empresas_Deuda1" style="display: block;">
                                                <div class="LblIndicador">Deuda 1 Mes</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda1Mes">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="Indicadores_Empresas_Deuda3" style="display: block;">
                                                <div class="LblIndicador">Deuda 3 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda3Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="Indicadores_Empresas_Deuda6" style="display: block;">
                                                <div class="LblIndicador">Deuda 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="Indicadores_Empresas_DeudaMayor6" style="display: block;">
                                                <div class="LblIndicador">Deuda > 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeudaMayor6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorWarning">
                                            <a href="#" id="Indicadores_Empresas_SinPagosUltimos12" style="display: block;">
                                                <div class="LblIndicador">Sin Pagos Últimos 12 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasSinPagosUltimos12Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorWarning">
                                            <a href="#" id="Indicadores_Empresas_PagosIntercalados" style="display: block;">
                                                <div class="LblIndicador">Pagos Intercalados</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasPagosIntercalados">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorDanger">
                                            <a href="#" id="Indicadores_Empresas_Inactivas" style="display: block;">
                                                <div class="LblIndicador">Inactivas</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasInactivas">
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                </div>
                <div class="row">
                    <div class="d-none d-lg-block">
                        <div id="ContainerPop"></div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-6">
                        <nav>
                            <ul>
                                <li class="MenuIndicador">
                                    <a href="#" id="Indicadores_Empleados" style="display: block;">
                                        <div class="LblIndicador">Empleados</div>
                                        <div class="LblValorIndicador" id="LblEmpleados"></div>
                                    </a>
                                    <%--                <ul class="SubMenuInidicador">
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosSinDeudaSinBoleta">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosSinDeudaConBoleta">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Deuda 1 Mes</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosDeuda1Mes">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Deuda 3 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosDeuda3Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Deuda 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosDeuda6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Deuda > 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosDeudaMayor6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorDanger">
                                            <a href="#">
                                                <div class="LblIndicador">Inactivos</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpleadosInactivos">
                                                </div>
                                            </a>
                                        </li>
                                    </ul>--%>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="d-none d-md-block col-md-6" style="height: 300px;">
                        <div class="row">
                            <div class="col-12">
                                <nav>
                                    <ul id="UlDistribuidorIndicadores">
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="BtnConfiguracion"
                                                style="display: block;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>'>Configuración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="BtnAdministracion"
                                                style="display: block;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>'>Administración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="BtnReportes"
                                                style="display: block;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>'>Reportes</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="BtnIngresos"
                                                style="display: block;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>'>Ingresos</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="BtnGastos"
                                                style="display: block;"
                                                href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Gasto.aspx")%>'>Gastos</a>
                                        </li>
                                        <li id="BtnArchivos"
                                            class="BtnDistribuidorIndicadores"
                                            style="display: none">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Archivos.aspx")%>'>Archivos</a>
                                        </li>
                                        <%--  <li class="BtnDistribuidorIndicadoresDanger">
                                    <a href="#">
                                        <div>
                                            Ch. Rechazados
                          <span
                              id="LblChequesRechazados"
                              style="float: right; padding-right: 10px"></span>
                                        </div>
                                    </a>
                                </li>--%>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-right" id="DivIngresoArchivos" style="display: block;">
                    <div class="d-none d-lg-block col-lg-10 text-right align-items-end">
                        <div id="PnlResultado" style="display: none"></div>
                        <div id="PnlArchivos">
                            <div>
                                <span
                                    id="SpanIngresoArchivos"
                                    class="AAEMM"
                                    style="font-size: 18px; color: #fff">Ingreso de Archivos</span>
                                <div
                                    style="width: 95%; margin-left: 120px">
                                    <asp:FileUpload
                                        ID="FileUpload1"
                                        CssClass="form-control"
                                        runat="server" />
                                    <br />
                                    <asp:Button
                                        ID="Upload"
                                        CssClass="btn btn-primary"
                                        runat="server"
                                        Text="Subir"
                                        UseSubmitBehavior="False" />
                                    <br />
                                    <asp:Label
                                        ID="LblOK"
                                        runat="server"
                                        CssClass="bg-light text-success "></asp:Label>
                                    <asp:Label
                                        ID="LblError"
                                        runat="server"
                                        CssClass="bg-light text-danger "></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--<ul>
        <li>
            <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1" style="height: 290px">
                    <div class="col-lg-10">
                        <div class="row">
                            <div class="col-lg-4 col-md-12">
                                <nav>
                                    <ul>
                                        <li class="MenuIndicador">
                                            <a href="#" id="Indicadores_Empresas">
                                                <div class="LblIndicador">Empresas</div>
                                                <div class="LblValorIndicador" id="LblEmpresas"></div>
                                            </a>
                                            <ul class="SubMenuInidicador">
                                                <li class="BtnIndicador">
                                                    <a href="#" id="Indicadores_Empresas_SinDeuda">
                                                        <div class="LblIndicador">Sin Deuda</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasSinDeuda">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#" id="Indicadores_Empresas_Deuda1">
                                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasDeuda1Mes">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#" id="Indicadores_Empresas_Deuda3">
                                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasDeuda3Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#" id="Indicadores_Empresas_Deuda6">
                                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasDeuda6Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#" id="Indicadores_Empresas_DeudaMayor6">
                                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasDeudaMayor6Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorWarning">
                                                    <a href="#" id="Indicadores_Empresas_SinPagosUltimos12">
                                                        <div class="LblIndicador">Sin Pagos Últimos 12 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasSinPagosUltimos12Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorWarning">
                                                    <a href="#">
                                                        <div class="LblIndicador">Pagos Intercalados</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasPagosIntercalados">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorDanger">
                                                    <a href="#">
                                                        <div class="LblIndicador">Inactivas</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpresasInactivas">
                                                        </div>
                                                    </a>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                            <div class="col-lg-4 col-md-12">
                                <nav>
                                    <ul>
                                        <li class="MenuIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Empleados</div>
                                                <div class="LblValorIndicador" id="LblEmpleados"></div>
                                            </a>
                                            <ul class="SubMenuInidicador">
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosSinDeudaSinBoleta">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosSinDeudaConBoleta">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosDeuda1Mes">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosDeuda3Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosDeuda6Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosDeudaMayor6Meses">
                                                        </div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorDanger">
                                                    <a href="#">
                                                        <div class="LblIndicador">Inactivos</div>
                                                        <div
                                                            class="LblValorIndicador"
                                                            id="LblEmpleadosInactivos">
                                                        </div>
                                                    </a>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                        <div class="col-lg-4 col-md-12">
                                <nav>
                                    <ul>
                                        <li class="MenuIndicador">
                                            <a href="#" id="BtnRecadudacionNeta">
                                                <div class="LblIndicador">Recaudación Neta</div>
                                                <div class="LblValorIndicador" id="LblRecaudacionNeta"></div>
                                            </a>
                                            <ul class="SubMenuInidicador">
                                            </ul>
                                        </li>
                                        <li class="MenuIndicador" style="margin-top: 2px">
                                            <a href="#" id="BtnRecadudacionBruta">
                                                <div class="LblIndicador">Recaudación Bruta</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblRecaudacionBruta">
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                        <div class="col-lg-10 d-none d-lg-block" style="max-height: 300px;">
                            <div id="ContainerPop"></div>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="row">
                            <div class="col-lg-12">
                                <nav>
                                    <ul id="UlDistribuidorIndicadores">
                                        <li class="BtnDistribuidorIndicadores">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>'>Configuración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>'>Administración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>'>Reportes</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>'>Ingresos</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Gasto.aspx")%>'>Gastos</a>
                                        </li>
                                        <li
                                            class="BtnDistribuidorIndicadores"
                                            style="display: none">
                                            <a
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Archivos.aspx")%>'>Archivos</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadoresDanger">
                                            <a href="#">
                                                <div>
                                                    Ch. Rechazados
                          <span
                              id="LblChequesRechazados"
                              style="float: right; padding-right: 10px"></span>
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8"></div>
                    <div class="col-lg-4">
                        <div class="row">
                            <div class="col-lg-12">
                                <div id="PnlResultado" style="display: none"></div>
                                <div id="PnlArchivos">
                                    <div class="d-none d-lg-block">
                                        <span
                                            id="SpanIngresoArchivos"
                                            class="AAEMM"
                                            style="font-size: 18px; color: #fff">Ingreso de Archivos.</span>
                                        <div
                                            style="width: 95%; margin-right: auto; margin-left: auto">
                                            <asp:FileUpload
                                                ID="FileUpload1"
                                                CssClass="form-control"
                                                runat="server" />
                                            <br />
                                            <asp:Button
                                                ID="Upload"
                                                CssClass="btn btn-primary"
                                                runat="server"
                                                Text="Subir"
                                                UseSubmitBehavior="False" />
                                            <br />
                                            <asp:Label
                                                ID="LblOK"
                                                runat="server"
                                                CssClass="bg-light text-success "></asp:Label>
                                            <asp:Label
                                                ID="LblError"
                                                runat="server"
                                                CssClass="bg-light text-danger "></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>--%>
</asp:Content>
