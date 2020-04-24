<%@ Page Title="AAEMM. Empleados" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Empleado.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empleado" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Adm_Empleado.js?version20200416_1")%>'></script>
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
                    <div class="col-lg-12">
                        <div class="row mt-1">
                            <div class="col-1"></div>
                            <div class="col-2">
                                <div class="Boton BtnNuevo">
                                    <a id="LinkBtnNuevo" href="#"><span id="SpanBtnNuevo"></span></a>
                                </div>
                            </div>
                            <div class="col-1"></div>
                            <div class="col-2">
                                <div class="Boton BtnBuscar">
                                    <a id="LinkBtnArmarUC" href="#"><span id="SpanBtnBuscar"></span></a>
                                </div>
                            </div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <div class="Boton BtnEliminar">
                                    <a id="LinkBtnEliminar" href="#"><span id="SpanBtnEliminar"></span></a>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnGuardar" href="#"><span id="SpanBtnGuardar"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-7">
                        <div class="row mt-4">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Nombre y Apellido</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtNombre" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nombre y Apellido" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Sexo</span>
                            </div>
                            <div class="col-1">
                                <input id="TxtSexo" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Sexo" maxlength="11" style="width: 140px" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">CUIL</span>
                            </div>
                            <div class="col-5">
                                <input id="TxtCUIL" class="DatoFormulario InputDatoFormulario" type="text" placeholder="CUIL" maxlength="11" style="width: 140px" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">DNI</span>
                            </div>
                            <div class="col-5">
                                <input id="TxtDNI" class="DatoFormulario InputDatoFormulario" type="text" placeholder="DNI" maxlength="8" style="width: 140px" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Entidad</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtEntidad" readonly="readonly" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Entidad" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Nacimiento</span>
                            </div>
                            <div class="col-2">
                                <input id="TxtFechaNacimiento" class="DatoFormulario InputDatoFormulario datepicker" type="text" placeholder="Fecha" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Afiliación</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtFechaAfiliacion" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Afiliación" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Fecha Ing. Entidad</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtFechaINgresoEntidad" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Ing. Entidad" autocomplete="off">
                            </div>
                        </div>

                    </div>
                    <div class="col-lg-5">
                        <div class="row mt-4">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloDimensional"></span>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Email</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtEmail" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Email" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Dirección</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtDireccion" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Dirección" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-3">
                                <span class="SpanDatoFormulario">CP :</span>
                            </div>
                            <div class="col-2">
                                <input id="TxtCP" class="DatoFormulario InputDatoFormulario" type="text" placeholder="CP" maxlength="4" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                            <div class="col-6">
                                <input id="TxtLocalidad" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Localidad" autocomplete="off" readonly="readonly">
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                            </div>
                            <div class="col-6">
                                <div class="Boton BtnInfo">
                                    <a id="LinkBtnFamiliares" href="#"><span id="SpanBtnFamiliares"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-10 text-center">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimir" href="#"><span id="SpanBtnImprimir"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Fecha Baja</span>
                            </div>
                            <div class="col-3">
                                <input id="TxtFechaBaja" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Fecha Baja" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Motivo Baja</span>
                            </div>
                            <div class="col-8">
                                <div id="CboMotivoBaja"></div>
                            </div>
                        </div>

                        <%--         <div class="row mt-2">
                            <div class="col-12 text-center">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimirNomina" href="#"><span id="SpanBtnImprimirNomina"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-5">
                            <div class="col-1"></div>
                            <div class="col-3 text-center">
                            </div>
                            <div class="col-1"></div>
                            <div class="col-5">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-12 text-center">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimir" href="#"><span id="SpanBtnImprimir"></span></a>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>



