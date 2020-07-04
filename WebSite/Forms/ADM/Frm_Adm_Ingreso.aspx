﻿<%@ Page Title="AAEMM. Ingresos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_ADM_Ingreso.aspx.vb" Inherits="Forms_ADM_Frm_Adm_Ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_Ingreso.js?version20200702_01")%>'></script>
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
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc90_L">
                <span id="SpanNombreFormulario"></span>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary" style="height: 408px;">
                            <div class="col-12 text-center">Fecha Acreditación</div>
                            <div class="container">
                                <div class="row mt-1 justify-content-center">
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
                                <div class="col-4">Estado</div>
                                <div class="col-8">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox"
                                            id="IdEstadoA" value="A">
                                        <label class="form-check-label" for="IdEstadoA">A</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox"
                                            id="IdEstadoL" value="L">
                                        <label class="form-check-label" for="IdEstadoL">L</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox"
                                            id="IdEstadoP" value="P">
                                        <label class="form-check-label" for="IdEstadoP">P</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox"
                                            id="IdEstadoR" value="R">
                                        <label class="form-check-label" for="IdEstadoR">R</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckEstado" type="checkbox"
                                            id="IdEstadoT" value="T">
                                        <label class="form-check-label" for="IdEstadoT">T</label>
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
                                <div class="col-4">Tipo</div>
                                <div class="col-8">
                                    <div id="ChecksTipo"></div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoBN"
                                            value="1">
                                        <label class="form-check-label" for="TipoBN">BN</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoPF"
                                            value="2">
                                        <label class="form-check-label" for="TipoPF">PF</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" name="CheckTipo" type="checkbox" id="TipoMC"
                                            value="3">
                                        <label class="form-check-label" for="TipoMC">MC</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-4">Importe</div>
                                <div class="col-5">
                                    <input type="text" id="BuscaImporte" class="form-control"
                                        onkeypress="return jsSoloNumeros(event)" placeholder="Importe">
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
                                    Ingresos
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
                        <div id="ContenedorSeleccionado" class="row" style="display: none;">
                            <div class="container border border-primary pb-3" style="height: 296px;">
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
                                                class="form-control text-center" placeholder="Código" maxlength="6"
                                                style="width: 120px;">
                                        </div>
                                        <div class="col-7 d-flex justify-content-center">
                                            <input type="text" id="EntidadRazonSocial" class="form-control"
                                                placeholder="Razon Social">
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-1">Fecha</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadAcred"
                                                class="form-control datepicker text-center" placeholder="Acreditación">
                                        </div>
                                        <div class="col-1">Período</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadPeriodo" class="form-control text-center"
                                                placeholder="Período">
                                        </div>
                                        <div class="col-1">Importe</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadImporte" class="form-control text-right"
                                                placeholder="Importe" onkeypress="return jsSoloNumeros(event)" />
                                        </div>
                                    </div>
                                    
                                    <div class="row mt-1">
                                        <div class="col-1">Origen</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadOrigen" class="form-control text-center"
                                                placeholder="Origen" disabled>
                                        </div>
                                        <div class="col-1">Cheque</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadNroCheque" class="form-control text-right"
                                                placeholder="Nro. Cheque">
                                        </div>
                                        <div class="col-1">Estado</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadEstado" class="form-control text-center"
                                                placeholder="Estado" disabled>
                                        </div>
                                    </div>
                                    <div class="row mt-4">
                                        <div class="col-1"></div>
                                        <div class="col-6">
                                            <a id="BtnModificar" href="#"
                                                class="btn btn-md btn-block btn-success">Actualizar
                                                Ingreso</a>
                                        </div>
                                        <div class="col-1"></div>
                                        <div class="col-3">
                                            <a id="BtnExplotar" href="#"
                                                class="btn btn-md btn-block btn-warning">Separar
                                                Ingreso</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-7">
                                        <div id="GrillaIngresoSeparado"></div>
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