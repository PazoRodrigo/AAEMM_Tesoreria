<%@ Page Title="AAEMM. Empleados" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Empleado.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empleado" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_Empleado.js?version20200428_1")%>'></script>
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
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary">
                            <div class="col-12 text-center">Fecha Acreditación</div>
                            <div class="container">
                                <div class="row mt-1 justify-content-center">
                                    <div class="col-5">
                                        <input type="text" id="BuscaDesdeAcred"
                                            class="form-control datepicker" onkeypress="return jsNoEscribir(event)" placeholder="Desde">
                                    </div>
                                    <div class="col-2"></div>
                                    <div class="col-5">
                                        <input type="text" id="BuscaHastaAcred"
                                            class="form-control datepicker" onkeypress="return jsNoEscribir(event)" placeholder="Hasta">
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Estado</div>
                                <div class="col-8">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox" id="IdEstadoA" value="A">
                                        <label class="form-check-label" for="IdEstadoA">A</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox" id="IdEstadoL" value="L">
                                        <label class="form-check-label" for="IdEstadoL">L</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox" id="IdEstadoP" value="P">
                                        <label class="form-check-label" for="IdEstadoP">P</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox" id="IdEstadoR" value="R">
                                        <label class="form-check-label" for="IdEstadoR">R</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox" id="IdEstadoT" value="T">
                                        <label class="form-check-label" for="IdEstadoT">T</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Empresa</div>
                                <div class="col-8">
                                    <input type="text" id="BuscaRazonSocial" class="form-control" placeholder="Razon Social">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">CUIT</div>
                                <div class="col-8">
                                    <input type="text" id="BuscaCUIT" class="form-control text-center"
                                        placeholder="CUIT" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="11" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Tipo</div>
                                <div class="col-8">
                                    <div id="ChecksTipo"></div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoBN" value="1">
                                        <label class="form-check-label" for="TipoBN">BN</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoPF" value="2">
                                        <label class="form-check-label" for="TipoPF">PF</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoMC" value="3">
                                        <label class="form-check-label" for="TipoMC">MC</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Importe</div>
                                <div class="col-5">
                                    <input type="text" id="BuscaImporte" class="form-control" onkeypress="return jsSoloNumeros(event)"
                                        placeholder="Importe">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Nro. Recibo</div>
                                <div class="col-7">
                                    <input type="text" id="BuscaNroRecibo" class="form-control" onkeypress="return jsSoloNumerosSinPuntos(event)"
                                        placeholder="Nro. Recibo">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Nro. Cheque</div>
                                <div class="col-7">
                                    <input type="text" id="BuscaNroCheque" class="form-control" onkeypress="return jsSoloNumerosSinPuntos(event)"
                                        placeholder="Nro. Cheque">
                                </div>
                            </div>
                            <div class="row mt-3 mb-3">
                                <div class="col-1"></div>
                                <div class="col-10">
                                    <a href="#" id="BtnBuscador" class="btn btn-md btn-block btn-primary">Buscar Ingresos
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="row">
                                <div class="col-12 text-center">
                                    <span id="LblCantidadRegistrosGrilla" class="text-light text-bold"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-8">
                        <div id="Grilla" class="row" style="display: block;">
                            <div class="container border border-primary">
                                <div class="row mt-1">
                                    <div class="col-12">
                                        <div id="GrillaCabecera"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div id="GrillaDetalle"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Seleccionado" class="row" style="display: none;">
                            <div class="container border border-primary pb-3">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        <%--<li class="linea">
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
        </li>--%>
    </ul>
</asp:Content>



