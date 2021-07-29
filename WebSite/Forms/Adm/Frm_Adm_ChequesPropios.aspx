﻿<%@ Page Title="AAEMM. Cheques Propios" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_ChequesPropios.aspx.vb" Inherits="Forms_Administracion_ChequesPropios" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_ChequesPropios.js?version=20210721")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>';
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
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloGrillaDimensional"></span>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="row mt-1">
                                    <div class="col-lg-9">
                                        <input id="TxtBuscador" class="InputDatoFormulario" type="text" placeholder="Nro. Cheque / Importe / Fecha (dd/MM/aaaa)" autocomplete="off">
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Estado :</span></div>
                                    <div class="col-lg-7" id="CboBusca"></div>
                                    <div class="col-lg-3">
                                        <div class="Boton BtnBuscar">
                                            <a id="LinkBtnBuscar" href="#"><span id="SpanBuscar"></span></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1 text-center mh-100" style="height: 250px;">
                            <div class="col-12 mh-100" id="GrillaRegistrados"></div>
                        </div>
                    </div>
                    <%--Entidad--%>
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="">Cheque</span>
                                </div>
                            </div>
                        </div>
                        <div id="DatosEntidad" style="height: 365px;">
                            <div class="row mt-1">
                                <div class="col-1"></div>
                                <div class="col-4">
                                   <%-- <div class="Boton BtnNuevo">
                                        <a id="LinkBtnNuevo" href="#"><span id="SpanBtnNuevo"></span></a>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Número</span>
                                </div>
                                <div class="col-3">
                                    <input id="TxtNombre" class="DatoFormulario InputDatoFormulario" type="text" readonly="readonly" placeholder="Número" autocomplete="off">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Importe</span>
                                </div>
                                <div class="col-4">
                                    <input id="TxtImporte" class="DatoFormulario InputDatoFormulario" type="text" readonly="readonly" placeholder="Importe" autocomplete="off">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Fecha Emisión</span>
                                </div>
                                <div class="col-4">
                                    <input id="TxtFechaEmision" class="DatoFormulario InputDatoFormulario" type="text" readonly="readonly" placeholder="Emisión" autocomplete="off">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Fecha Débito</span>
                                </div>
                                <div class="col-4">
                                    <input id="TxtFechaDebito" class="DatoFormulario InputDatoFormulario" type="text" readonly="readonly" placeholder="Débito" autocomplete="off">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Observaciones</span>
                                </div>
                                <div class="col-8">
                                    <textarea id="TxtObservaciones" class="DatoFormulario TextareaDatoFormulario" placeholder="Observaciones"></textarea>
                                </div>
                            </div>
                             <div class="row mt-1">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <div class="Boton BtnEliminar">
                                    <a id="LinkAnularCheque" href="#"><span id="SpanAnularCheque"></span></a>
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
