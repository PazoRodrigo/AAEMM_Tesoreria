<%@ Page Title="AAEMM. Empresas" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_ADM_Empresa.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empresa" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_Empresa.js?version20200601_1")%>'></script>
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
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>' class="LinkBtn" title="Volver a Administración"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary">
                            <div class="row mt-1 justify-content-center">
                                <div class="col-9 mb-2 font-weight-bold text-light">Situación de Empresas </div>
                            </div>
                            <div class="row">
                                <div class="col-1"></div>
                                <div class="col-11">
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="switchIncluirAlta">
                                        <label class="custom-control-label" for="switchIncluirAlta">Empresas de Alta</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-1"></div>
                                <div class="col-11">
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="switchIncluirBaja">
                                        <label class="custom-control-label" for="switchIncluirBaja">Empresas de Baja</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-1"></div>
                                <div class="col-11">
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="switchIncluirCUIT0">
                                        <label class="custom-control-label" for="switchIncluirCUIT0">Incluir CUIT 00-0000000-0</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2 mb-2 justify-content-center">
                                <div class="col-9 font-weight-bold text-light">Parámetros </div>
                            </div>
                            <div class="row">
                                <div class="col-3">CUIT</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaCUIT" class="form-control text-center"
                                        placeholder="CUIT" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="11" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-3">Empresa</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaRazonSocial" class="form-control" placeholder="Razon Social">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-3">C. de C.</div>
                                <div class="col-9">
                                    <div id="BuscaCboCentroCosto"></div>
                                </div>
                            </div>
                            <div class="row mt-5 mb-3">
                                <div class="col-1"></div>
                                <div class="col-10">
                                    <a href="#" id="BtnBuscador" class="btn btn-md btn-block btn-primary">Buscar Empresas
                                    </a>
                                </div>
                            </div>
                        </div>
                         <div id="divCantRegistrosImprimir" style="display: none;">
                            <div class="container">
                                <div class="row mt-2 justify-content-center">
                                    <div class="col-9 text-center">
                                        <a href="#" id="BtnImprimirRegistrosGrilla" class="btn btn-block btn-light"><span id="LblCantidadRegistrosGrilla" class="text-bold"></span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-8">
                        <div id="Grilla" class="row" style="display: none;">
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
                            <div class="container border border-primary pb-1">
                               <div class="row mt-4">
                                    <div class="col-3 text-center">CUIT</div>
                                    <div class="col-2 text-center">Código</div>
                                    <div class="col-7 text-center">Razon Social</div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3 d-flex justify-content-center">
                                        <input type="text" id="EntidadCUIT" class="form-control text-center"
                                            placeholder="CUIT" maxlength="11" style="width: 140px;">
                                    </div>
                                    <div class="col-2 d-flex justify-content-center">
                                        <input type="text" id="EntidadCodigoEntidad" class="form-control text-center"
                                            placeholder="Código" maxlength="6" style="width: 120px;">
                                    </div>
                                    <div class="col-7 d-flex justify-content-center">
                                        <input type="text" id="EntidadRazonSocial" class="form-control"
                                            placeholder="Razon Social" disabled>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-2">Convenio</div>
                                    <div class="col-4">
                                        <div id="CboConvenio"></div>
                                    </div>
                                  <div class="col-2">C. de C.</div>
                                    <div class="col-4">
                                        <div id="CboCentroCosto"></div>
                                    </div>
                                </div>
                                  <div class="row mt-2 justify-content-end">
                                    <div class="col-3">
                                        <a href="#" id="BtnContactoEmpresa" class="btn btn-block btn-info ">Empresa</a>
                                    </div>
                                    <div class="col-3">
                                        <a href="#" id="BtnContactoPersonas" class="btn btn-block btn-info ">Personas</a>
                                    </div>
                                </div>
                              
                                <div class="row mt-2 justify-content-between">
                                    <div class="col-7">
                                        <div class="row justify-content-center">
                                            <div class="col-10">
                                                <a href="#" class="btn btn-block btn-primary">Cuenta Corriente
                                                <br />
                                                    $ 150.000,00
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-5">
                                        <div class="row">
                                            <div class="col-8 text-right">
                                                Empleados:
                                            </div>
                                            <div class="col-2 text-right text-light">
                                                <span id="LblCantidadEmpleados">35</span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-8 text-right">
                                                Afiliados:
                                            </div>
                                            <div class="col-2 text-right text-light">
                                                <span id="LblCantidadAfiliados">23</span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-8 text-right">
                                                No Afiliados:
                                            </div>
                                            <div class="col-2 text-right text-light">
                                                <span id="LblCantidadNoAfiliados">12</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-4 justify-content-center">
                                    <div class="col-10 mb-2">
                                        <a href="#" class="btn btn-block btn-success btn-lg">Guardar Cambios</a>
                                    </div>
                                </div>
                                <div class="row mt-4 justify-content-center">
                                    <div class="col-3 mb-2">
                                        <a href="#" class="btn btn-block btn-light">Imprimir</a>
                                    </div>

                                    <div class="col-3 mb-2">
                                        <a href="#" class="btn btn-block btn-success">Reactivar</a>
                                    </div>
                                    <div class="col-3 mb-2">
                                        <a href="#" class="btn btn-block btn-danger">Dar de Baja</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>

<%--        <li class="linea">
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
                                <input id="TxtCUIT" class="DatoFormulario InputDatoFormulario" type="text" placeholder="CUIT" maxlength="11" style="width: 140px" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
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
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-3">
                                <span class="SpanDatoFormulario">Dirección</span>
                            </div>
                            <div class="col-8">
                                <input id="TxtDireccion" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Dirección" autocomplete="off">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
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
                            <div class="col-1"></div>
                            <div class="col-3">
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
                    </div>
                    <div class="col-lg-5">
                        <div class="row mt-4">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloDimensional"></span>
                                </div>
                            </div>
                        </div>
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
                            <div class="col-1"></div>
                            <div class="col-10 text-center">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimirNomina" href="#"><span id="SpanBtnImprimirNomina"></span></a>
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
                            <div class="col-12 text-center">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnReactivar" href="#"><span id="SpanBtnReactivar"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>--%>
    </ul>
</asp:Content>



