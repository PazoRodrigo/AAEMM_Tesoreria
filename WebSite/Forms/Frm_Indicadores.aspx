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
    <script src='<%= ResolveClientUrl("Frm_Indicadores.js?version=20210811")%>'></script>
    <div id="PopUp">
    </div>
    <asp:Button
        ID="btnSubirOculto"
        runat="server"
        Style="visibility: hidden; display: none;" />
    <div class="container-fluid">
        <div class="row mb-2">
            <div class="d-none d-md-block col-12">
                <div id="DivNombreFormulario100">
                    <span id="NombreFormulario"></span>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="d-none d-lg-block col-10"></div>
            <div class="d-none d-lg-block col-2">
                <a href="#" id="BtnMensajero" class="btn btn-block btn-warning text-dark">Mensajero<span id="LblCantMensajes"></span></a>
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
                                    <a href="#" id="FORM0_INDICADORES_EMPRESAS" style="display: none;">
                                        <div class="LblIndicador">Empresas</div>
                                        <div class="LblValorIndicador" id="LblEmpresas"></div>
                                    </a>
                                    <ul class="SubMenuInidicador">
                                        <li class="BtnIndicador">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_SINDEUDA" style="display: none;">
                                                <div class="LblIndicador">Sin Deuda</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasSinDeuda">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_DEUDA1" style="display: none;">
                                                <div class="LblIndicador">Deuda 1 Mes</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda1Mes">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_DEUDA3" style="display: none;">
                                                <div class="LblIndicador">Deuda 3 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda3Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_DEUDA6" style="display: none;">
                                                <div class="LblIndicador">Deuda 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeuda6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicador">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_DEUDAMAYOR6" style="display: none;">
                                                <div class="LblIndicador">Deuda > 6 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasDeudaMayor6Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorWarning">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_SINPAGOSULTIMOS12" style="display: none;">
                                                <div class="LblIndicador">Sin Pagos Últimos 12 Meses</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasSinPagosUltimos12Meses">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorWarning">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_PAGOSINTERCALADOS" style="display: none;">
                                                <div class="LblIndicador">Pagos Intercalados</div>
                                                <div
                                                    class="LblValorIndicador"
                                                    id="LblEmpresasPagosIntercalados">
                                                </div>
                                            </a>
                                        </li>
                                        <li class="BtnIndicadorDanger">
                                            <a href="#" id="FORM0_INDICADORES_EMPRESAS_INACTIVAS" style="display: none;">
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
                                    <a href="#" id="FORM0_INDICADORES_EMPLEADOS" style="display: none;">
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
                                            <a id="FORM0_BTNCONFIGURACION"
                                                style="display: none;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>'>Configuración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="FORM0_BTNADMINISTRACION"
                                                style="display: none;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>'>Administración</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="FORM0_BTNREPORTES"
                                                style="display: none"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>'>Reportes</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="FORM0_BTNINGRESOS"
                                                style="display: none;"
                                                href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>'>Ingresos</a>
                                        </li>
                                        <li class="BtnDistribuidorIndicadores">
                                            <a id="FORM0_BTNGASTOS"
                                                style="display: none;"
                                                href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Gasto.aspx")%>'>Gastos</a>
                                        </li>
                                        <li id="FORM0_BTNARCHIVOS"
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
                <div class="row justify-content-right" id="FORM0_DIVINGRESOARCHIVOS" style="display: none;">
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
</asp:Content>
