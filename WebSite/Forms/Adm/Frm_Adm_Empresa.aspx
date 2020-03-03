<%@ Page Title="AAEMM. Empresas" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Empresa.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empresa" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Adm_Empresa.js")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>';
                window.location = redirect;
            }
        };
    </script>
    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>' class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario"></span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>' class="LinkBtn" title="Volver a Configuración"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1">
                    <%--Buscador--%>
                    <div class="col-lg-7">
                        <div class="row mt-1">
                            <div class="col-1"></div>
                            <div class="col-4">
                                <div class="Boton BtnNuevo">
                                    <a id="LinkBtnNuevo" href="#"><span id="SpanBtnNuevo"></span></a>
                                </div>
                            </div>
                            <div class="col-1"></div>
                            <div class="col-4">
                                <div class="Boton BtnBuscar">
                                    <a id="LinkBtnBuscar" href="#"><span id="SpanBtnBuscar"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Razón Social</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtRazonSocial" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Razón Social" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">CUIT</span>
                            </div>
                            <div class="col-5">
                                <input id="TxtCUIT" class="DatoFormulario InputDatoFormulario" type="text" placeholder="CUIT" maxlength="11" style="width: 160px" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Convenio</span>
                            </div>
                            <div class="col-8">
                                <div id="CboConvenio"></div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">C. de C.</span>
                            </div>
                            <div class="col-8">
                                <div id="CboCentroCosto"></div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Email</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtEmail" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Email" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <div class="Boton BtnInfo">
                                    <a id="LinkBtnDomicilios" href="#"><span id="SpanBtnDomicilios"></span></a>
                                </div>
                            </div>
                            <div class="col-1"></div>
                            <div class="col-3">
                                <div class="Boton BtnInfo">
                                    <a id="LinkBtnContactos" href="#"><span id="SpanBtnContactos"></span></a>
                                </div>
                            </div>
                            <div class="col-1"></div>
                            <div class="col-3">
                                <div class="Boton BtnInfo">
                                    <a id="LinkBtntelefonos" href="#"><span id="SpanBtnTelefonos"></span></a>
                                </div>
                            </div>

                        </div>

                         <div class="row mt-3">
                            <div class="col-1 text-center"></div>
                            <div class="col-10 text-center">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnReactivar" href="#"><span id="SpanBtnReactivar"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--Entidad--%>
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloDimensional"></span>
                                </div>
                            </div>
                        </div>
                        <div id="DatosEntidad" style="height: 365px;">
                            <div class="row mt-1">
                                <div class="col-5">
                                    <span class="SpanDatoFormulario">Saldo Cuenta Corriente</span>
                                </div>
                                <div class="col-7">
                                    <input id="TxtSaldoCuentaCorriente" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Saldo" autocomplete="off" readonly="readonly">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-5">
                                    <span class="SpanDatoFormulario">Empleados</span>
                                </div>
                                <div class="col-2">
                                    <input id="TxtEmpleados" class="DatoFormulario InputDatoFormulario" type="text" placeholder="0" autocomplete="off" readonly="readonly">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-5">
                                    <span class="SpanDatoFormulario">Afiliados</span>
                                </div>
                                <div class="col-2">
                                    <input id="TxtEAfiliados" class="DatoFormulario InputDatoFormulario" type="text" placeholder="0" autocomplete="off" readonly="readonly">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-5">
                                    <span class="SpanDatoFormulario">No Afiliados</span>
                                </div>
                                <div class="col-2">
                                    <input id="TxtNoAfiliados" class="DatoFormulario InputDatoFormulario" type="text" placeholder="0" autocomplete="off" readonly="readonly">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-12 text-center">
                                    <div class="Boton BtnImprimir">
                                        <a id="LinkBtnImprimirNomina" href="#"><span id="SpanBtnImprimirNomina"></span></a>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-5">
                                <div class="col-1"></div>
                                <div class="col-3 text-center">
                                    <div class="Boton BtnEliminar">
                                        <a id="LinkBtnEliminar" href="#"><span id="SpanBtnEliminar"></span></a>
                                    </div>
                                </div>
                                <div class="col-1"></div>
                                <div class="col-5">
                                    <div class="Boton BtnGuardar">
                                        <a id="LinkBtnGuardar" href="#"><span id="SpanBtnGuardar"></span></a>
                                    </div>
                                </div>
                            </div>
                             <div class="row mt-2">
                                <div class="col-12 text-center">
                                    <div class="Boton BtnImprimir">
                                        <a id="LinkBtnImprimir" href="#"><span id="SpanBtnImprimir"></span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>



