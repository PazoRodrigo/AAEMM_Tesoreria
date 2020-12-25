<%@ Page Title="AAEMM. Gastos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Gasto.aspx.vb" Inherits="Forms_ADM_Frm_Adm_Gasto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_Gasto.js?version20201028")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
                window.location = redirect;
            }
        };
    </script>
    <style>
        a {
            text-decoration: none;
        }

            a:hover {
                text-decoration: none;
                color: #fff;
            }

        .btnComprobanteOn {
            display: block;
            width: 100%;
            padding-top: 10px;
            padding-bottom: 10px;
            float: left;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#263238), to(#007aff));
            background-image: linear-gradient(180deg, #263238, #007aff);
            background-position: 0px 0px, 0% 50%, 100% 50%;
            background-size: auto, 1px, 1px;
            background-repeat: repeat, no-repeat, no-repeat;
            font-family: 'PT Sans', sans-serif;
            color: #fff;
            font-size: 16px;
            text-align: center;
            text-decoration: none;
        }

        .btnComprobanteOff {
            display: none;
            width: 100%;
            padding-top: 10px;
            padding-bottom: 10px;
            float: left;
            background-position: 0% 50%, 100% 50%;
            background-size: 1px, 1px;
            background-repeat: no-repeat, no-repeat;
            font-family: 'PT Sans', sans-serif;
            color: hsla(0, 0%, 100%, 0.25);
            font-size: 16px;
            text-align: center;
            text-decoration: none;
        }


        .btnGastoOn {
            display: block;
            width: 100%;
            padding-top: 10px;
            padding-bottom: 10px;
            float: left;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#263238), to(#007aff));
            background-image: linear-gradient(180deg, #263238, #007aff);
            background-position: 0px 0px, 0% 50%, 100% 50%;
            background-size: auto, 1px, 1px;
            background-repeat: repeat, no-repeat, no-repeat;
            font-family: 'PT Sans', sans-serif;
            color: #fff;
            font-size: 16px;
            text-align: center;
            text-decoration: none;
        }

        .btnGastoOff {
            display: none;
            width: 100%;
            padding-top: 10px;
            padding-bottom: 10px;
            float: left;
            background-position: 0% 50%, 100% 50%;
            background-size: 1px, 1px;
            background-repeat: no-repeat, no-repeat;
            font-family: 'PT Sans', sans-serif;
            color: hsla(0, 0%, 100%, 0.25);
            font-size: 16px;
            text-align: center;
            text-decoration: none;
        }

        .SpanGastoCabecera {
            font-size: 2em;
            color: #fff;
        }
    </style>
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
                <div class="row mt-1">
                    <div class="col-lg-12">
                        <div class="row mt-1" style="height: 90px;">
                            <div class="col-1" title="Nro. Gasto" style="text-align: center;">
                                Gasto
                                <br />
                                <div class="SpanGastoCabecera">
                                    <span id="SpanNroGasto"></span>
                                </div>
                            </div>
                            <div class="col-2" title="Importe" style="text-align: center;">
                                Importe
                                <br />
                                <div class="SpanGastoCabecera">
                                    <span id="SpanGastoImporte"></span>
                                </div>
                            </div>
                            <div class="col-2" title="Comprobantes" style="text-align: center;">
                                Comprobantes
                                <br />
                                <div class="SpanGastoCabecera">
                                    <span id="SpanGastoComprobantes"></span>
                                </div>
                            </div>
                            <div class="col-2" title="Estado Gasto">
                                Estado Gasto
                                <br />
                                <div class="SpanGastoCabecera">
                                    <span id="SpanGastoEstado"></span>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="Boton BtnNuevo">
                                    <a id="LinkBtnNuevoGasto" href="#"><span id="SpanBtnGasto"></span></a>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="Boton BtnNuevo">
                                    <a id="LinkBtnNuevoComprobante" href="#"><span id="SpanBtnComprobante"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="row">
                            <div class="col-lg-6">
                                <a href="#" class="btnGastoOn" data-ix="tabComprobante">Gasto</a>
                                <a href="#" class="btnGastoOff" data-ix="tabComprobante">Gasto</a>
                            </div>
                            <div class="col-lg-6">
                                <a href="#" class="btnComprobanteOn" data-ix="tabComprobante"
                                    style="display: none;">Comprobantes</a>
                                <a href="#" class="btnComprobanteOff" data-ix="tabComprobante">Comprobantes</a>
                            </div>
                        </div>
                        <div class="row mt-1 text-center" style="height: 250px; overflow-y: scroll">
                            <div class="col-lg-12">
                                <div id="GrillaGastosRegistrados"></div>
                                <div id="GrillaComprobantesRegistrados"></div>
                            </div>
                        </div>
                        <div class="row mt-1 justify-content-around">
                            <div class="col-lg-8">
                                <div class="Boton BtnBuscar">
                                    <a id="LinkBtnCerrarGasto" href="#"><span id="SpanBtnCerrarGasto"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-8">
                        <div id="GastoDetalle" style="display: none;">
                            <div style="width: 100%;">
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Cuenta</div>
                                    <div class="col-lg-8">
                                        <div id="CboCuenta"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Fecha Gasto</div>
                                    <div class="col-lg-3">
                                        <input id="TxtFechaGasto"
                                            class="DatoFormularioComprobante InputDatoFormulario datepicker" type="text"
                                            placeholder="Fecha Gasto" autocomplete="off"
                                            onkeypress="return jsNoEscribir(event)">
                                    </div>
                                    <div class="col-lg-2 ml-3">Fecha Pago</div>
                                    <div class="col-lg-3">
                                        <input id="TxtFechaPago"
                                            class="DatoFormularioComprobante InputDatoFormulario datepicker" type="text"
                                            placeholder="Fecha Pago" autocomplete="off"
                                            onkeypress="return jsNoEscribir(event)">
                                    </div>

                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Observaciones</div>
                                    <div class="col-lg-8">
                                        <textarea id="TxtObservaciones"
                                            class="DatoFormularioComprobante TextareaDatoFormulario"
                                            placeholder="Observaciones"></textarea>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Importe</div>
                                    <div class="col-lg-5">
                                        <input id="TxtImporte" class="DatoFormularioComprobante InputDatoFormulario"
                                            type="text" placeholder="Importe" autocomplete="off"
                                            onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Originario</div>
                                    <div class="col-lg-8">
                                        <div id="CboOriginarioGasto"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">C. de C.</div>
                                    <div class="col-lg-8">
                                        <div id="CboCentroCosto"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Proveedor</div>
                                    <div class="col-lg-8">
                                        <div id="CboProveedor"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2 ml-3">Nro. Compr.</div>
                                    <div class="col-lg-3">
                                        <input id="TxtNroComprobante"
                                            class="DatoFormularioComprobante InputDatoFormulario" type="text"
                                            placeholder="Nro. Compr." autocomplete="off"
                                            onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                    <div class="col-lg-2 ml-3">Tipo Pago</div>
                                    <div class="col-lg-3">
                                        <div id="CboTipoPago"></div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-8"></div>
                                    <div class="col-lg-4">
                                        <div class="Boton BtnGuardar">
                                            <a id="LinkBtnGuardarComprobante" href="#"><span
                                                id="SpanBtnGuardarComprobante"></span></a>
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
