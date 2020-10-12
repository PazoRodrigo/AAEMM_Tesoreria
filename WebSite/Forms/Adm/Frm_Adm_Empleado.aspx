<%@ Page Title="AAEMM. Empleados" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Empleado.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empleado" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_Empleado.js?version20201012")%>'></script>
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
                                <div class="col-9 mb-2 font-weight-bold text-light">Situación de Empleados </div>
                            </div>
                            <div class="row">
                                <div class="col-1"></div>
                                <div class="col-11">
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="switchIncluirAfiliados">
                                        <label class="custom-control-label" for="switchIncluirAfiliados">Afiliados</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-1"></div>
                                <div class="col-11">
                                    <div class="custom-control custom-switch">
                                        <input type="checkbox" class="custom-control-input" id="switchIncluirNoAfiliados">
                                        <label class="custom-control-label" for="switchIncluirNoAfiliados">No Afiliados</label>
                                    </div>
                                </div>
                            </div>
                           <div class="row mt-2 mb-2 justify-content-center">
                                <div class="col-9 font-weight-bold text-light">Parámetros</div>
                            </div>
                             <div class="row mt-1">
                                <div class="col-3">Nombre</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaNombre" class="form-control" placeholder="Nombre">
                                </div>
                            </div>
                             <div class="row mt-1">
                                <div class="col-3">DNI</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaDNI" class="form-control text-center"
                                        placeholder="DNI" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="8" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-3">CUIL</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaCUIL" class="form-control text-center"
                                        placeholder="CUIL" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="11" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-3">Empresa</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaRazonSocial" class="form-control" placeholder="Razon Social">
                                </div>
                            </div>
                             <div class="row mt-1">
                                <div class="col-3">CUIT</div>
                                <div class="col-9">
                                    <input type="text" id="BuscaCUIT" class="form-control text-center"
                                        placeholder="CUIT" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="11" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-2 mb-2">
                                <div class="col-1"></div>
                                <div class="col-10">
                                    <a href="#" id="BtnBuscador" class="btn btn-md btn-block btn-primary">Buscar Empleados
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
                                    <div class="col-2">CUIT</div>
                                    <div class="col-2">
                                        <input type="text" id="EntidadCUIT" class="form-control text-center"
                                            placeholder="CUIT" maxlength="11" style="width: 120px;">
                                    </div>
                                    <div class="col-2">Razon Social</div>
                                    <div class="col-6">
                                        <input type="text" id="EntidadRazonSocial" class="form-control"
                                            placeholder="Razon Social" disabled>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-2">Convenio</div>
                                    <div class="col-4">
                                    </div>
                                  <div class="col-2">C. de C.</div>
                                    <div class="col-4">
                                    </div>
                                </div>
                                  <div class="row mt-2 justify-content-end">
                                    <div class="col-3">
                                        <a href="#" class="btn btn-block btn-info ">Institución</a>
                                    </div>
                                    <div class="col-3">
                                        <a href="#" class="btn btn-block btn-info ">Personas</a>
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
                        </div>
                    </div>
                </div>
            </div>
        </li>--%>
    </ul>
</asp:Content>



