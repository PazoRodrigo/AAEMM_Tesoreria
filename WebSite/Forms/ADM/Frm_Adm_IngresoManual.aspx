<%@ Page Title="AAEMM. Recibos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_IngresoManual.aspx.vb" Inherits="Forms_ADM_Frm_Adm_IngresoManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_IngresoManual.js?version=20210721")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>';
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
                <span id="SpanNombreFormulario"></span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>' class="LinkBtn"
                    title="Volver a Administración"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary" style="height: 408px;">
                            <div class="container">
                                <div class="row mt-1 justify-content-center">
                                    <div class="col-10">
                                        <a href="#" id="BtnNuevoRecibo" class="btn btn-md btn-block btn-success">Nuevo Recibo</a>
                                    </div>
                                </div>
                            </div>
                            <div class="container mt-4">
                                <div class="col-12 text-center">Fecha Acreditación</div>
                                <div class="row mt-2 justify-content-center">
                                    <div class="col-5">
                                        <input type="text" id="BuscaDesdeAcred" class="form-control datepicker"
                                            onkeypress="return jsNoEscribir(event)" placeholder="Desde">
                                    </div>
                                    <div class="col-2"></div>
                                    <div class="col-5">
                                        <input type="text" id="BuscaHastaAcred" class="form-control datepicker"
                                            onkeypress="return jsNoEscribir(event)" placeholder="Hasta">
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Empresa</div>
                                <div class="col-8">
                                    <input type="text" id="BuscaRazonSocial" class="form-control"
                                        placeholder="Razon Social">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">CUIT</div>
                                <div class="col-8">
                                    <input type="text" id="BuscaCUIT" class="form-control text-center"
                                        placeholder="CUIT" onkeypress="return jsSoloNumerosSinPuntos(event)"
                                        maxlength="11" style="width: 120px;">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Importe</div>
                                <div class="col-5">
                                    <input type="text" id="BuscaImporte" class="form-control" onkeypress="return jsSoloNumeros(event)" placeholder="Importe">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Nro. Recibo</div>
                                <div class="col-7">
                                    <input type="text" id="BuscaNroRecibo" class="form-control"
                                        onkeypress="return jsSoloNumerosSinPuntos(event)" placeholder="Nro. Recibo">
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Nro. Cheque</div>
                                <div class="col-7">
                                    <input type="text" id="BuscaNroCheque" class="form-control"
                                        onkeypress="return jsSoloNumerosSinPuntos(event)" placeholder="Nro. Cheque">
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-10">
                                <a href="#" id="BtnBuscador" class="btn btn-md btn-block btn-primary">Buscar
                                    Recibos
                                </a>
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
                        <div id="divCantRegistrosBusqueda" style="display: none;">
                            <div class="container">
                                <div class="row mt-2 justify-content-between">
                                    <div class="col-4 text-right text-light text-bold">
                                        Registros : <span id="LblCantidadRegistrosGrilla" class="text-bold"></span>
                                    </div>
                                    <div class="col-6 text-right text-bold">
                                        <h4 id="LblValorSeleccion" class="text-light pr-3"></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="ContenedorSeleccionadoManual" class="row" style="display: none;">
                            <div class="container border border-primary pb-3" style="height: 480px;">
                                <div id="ContenidoSeleccionado">
                                    <div class="row mt-4">
                                        <div class="col-3 text-center">CUIT</div>
                                        <div class="col-2 text-center">Código</div>
                                        <div class="col-7 text-center">Razon Social</div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-3 d-flex justify-content-center">
                                            <input type="text" id="EntidadCUIT"
                                                onkeypress="return jsSoloNumerosSinPuntos(event)"
                                                class="form-control text-center" placeholder="CUIT" maxlength="11"
                                                style="width: 140px;" />
                                        </div>
                                        <div class="col-2 d-flex justify-content-center">
                                            <input type="text" id="EntidadCodigoEntidad"
                                                readonly="readonly"
                                                class="form-control text-center" placeholder="Código" maxlength="6"
                                                style="width: 120px;">
                                        </div>
                                        <div class="col-7 d-flex justify-content-center">
                                            <input type="text" id="EntidadRazonSocial"
                                                class="form-control"
                                                readonly="readonly"
                                                placeholder="Razon Social" readonly="readonly">
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-1">Fecha</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadFecha"
                                                class="form-control datepicker text-center" placeholder="Fecha Recibo">
                                        </div>
                                        <div class="col-2">Nro. Recibo</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadNroRecibo" class="form-control text-right"
                                                placeholder="Nro. Recibo" onkeypress="return jsSoloNumeros(event)" />

                                        </div>
                                        <%--<div style="visibility: hidden" class="col-1">Estado</div>--%>
                                        <div class="col-3">
                                            <a id="BtnAnularRecibo" style="display:none" href="#" class="btn btn-md btn-block btn-danger">Anular  </a>
                                            <%--<input style="visibility: hidden" type="text" id="EntidadEstado" class="form-control text-center"
                                                placeholder="Estado" disabled>--%>
                                            
                                        </div>
                                    </div>
                                    <div class="row mt-1 justify-content-end">
                                        <div class="col-5">
                                            <div class="row mt-1 mb-1">
                                                <div class="col-5 text-right pr-2">Importe Total</div>
                                                <div class="col-6">
                                                    <input type="text" id="EntidadImporteTotal" class="form-control text-right"
                                                        placeholder="Importe Total" onkeypress="return jsSoloNumeros(event)" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-5 text-right pr-2">
                                                    Efectivo: 
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" id="EntidadImporteEfectivo" class="form-control text-right"
                                                        placeholder="Efectivo" onkeypress="return jsSoloNumeros(event)" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-7">
                                            <textarea type="text" id="EntidadObservaciones" class="form-control"
                                                placeholder="Observaciones"></textarea>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-5">
                                            <div class="row justify-content-center">
                                                <div class="col-10">
                                                    <a href="#" id="BtnChequesTransferencias" class="btn btn-block btn-warning">Cheques y Transferencias</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-7">
                                            <div class="row">
                                                <div class="col-3">
                                                    <input type="text" id="PeriodoPeriodo" class="form-control text-right"
                                                        placeholder="MMaaaa" onkeypress="return jsSoloNumeros(event)" />
                                                </div>
                                                <div class="col-4">
                                                    <input type="text" id="PeriodoImporte" class="form-control text-right"
                                                        placeholder="Importe" onkeypress="return jsSoloNumeros(event)" />
                                                </div>
                                                <div class="col-5">
                                                    <a id="BtnAgregarPeriodo" href="#" class="btn btn-md btn-block btn-warning">Agregar Período</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivDetalleRecibo" style="height: 150px;">
                                        <div class="row mt-1">
                                            <div class="col-7">
                                                <div id="GrillaPagos"></div>
                                            </div>
                                            <div class="col-5">
                                                <div id="GrillaPeriodos"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-1 justify-content-center">
                                        <div class="col-10">
                                            <a id="BtnGuardarRecibo" href="#"
                                                class="btn btn-md btn-block btn-success">Guardar
                                                Recibo</a>
                                        </div>
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

