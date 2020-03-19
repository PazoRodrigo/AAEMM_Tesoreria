<%@ Page Title="AAEMM. Gastos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Gasto.aspx.vb" Inherits="Forms_Frm_Gasto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Gasto.js")%>'></script>
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
            background-image: -webkit-gradient(linear, left top, left bottom, from(#263238), to(#007aff)), url('../images/Line2.png'), url('../images/Line2.png');
            background-image: linear-gradient(180deg, #263238, #007aff), url('../images/Line2.png'), url('../images/Line2.png');
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
            background-image: url('../images/Line2.png'), url('../images/Line2.png');
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
            background-image: -webkit-gradient(linear, left top, left bottom, from(#263238), to(#007aff)), url('../images/Line2.png'), url('../images/Line2.png');
            background-image: linear-gradient(180deg, #263238, #007aff), url('../images/Line2.png'), url('../images/Line2.png');
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
            background-image: url('../images/Line2.png'), url('../images/Line2.png');
            background-position: 0% 50%, 100% 50%;
            background-size: 1px, 1px;
            background-repeat: no-repeat, no-repeat;
            font-family: 'PT Sans', sans-serif;
            color: hsla(0, 0%, 100%, 0.25);
            font-size: 16px;
            text-align: center;
            text-decoration: none;
        }
    </style>
    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>' class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc90_L">
                <span id="SpanNombreFormulario"></span>
            </div>
        </li>
        <li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1">
                    <div class="col-lg-12">
                        <div class="row mt-1">
                            <div class="col-1" title="Nro. Gasto">
                                <div class="SpanGastoCabecera" style="font-size: 3em; color: #fff; font-weight: bold;">
                                    <span id="SpanNroGasto">131</span>
                                </div>
                            </div>
                            <div class="col-1"></div>
                            <div class="col-4" title="Importe">
                                <div class="SpanGastoCabecera" style="font-size: 3em; color: #fff; font-weight: bold;">
                                    <span id="SpanGastoImporte">10600.00</span>
                                </div>
                            </div>
                            <div class="col-1" title="Estado Gasto">
                                <div class="SpanGastoCabecera" style="font-size: 2em; color: #fff;">
                                    <span id="SpanGastoEstado">Abierto</span>
                                </div>
                            </div>
                            <div class="col-1"></div>
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
                                <a href="#" class="btnComprobanteOn" data-ix="tabComprobante">Comprobantes</a>
                                <a href="#" class="btnComprobanteOff" data-ix="tabComprobante">Comprobantes</a>
                            </div>
                        </div>
                        <div class="row mt-1 text-center">
                            <div class="col-lg-12">
                                <div id="GrillaGastosRegistrados"></div>
                                <div id="GrillaComprobantesRegistrados"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-8">
                        <div class="row mt-1">
                            <div class="col-lg-2">Originario</div>
                            <div class="col-lg-4">
                                <div id="CboOriginarioGasto"></div>
                            </div>
                            <div class="col-lg-2">Proveedor</div>
                            <div class="col-lg-4">
                                <div id="CboProveedor"></div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-2">Nro. Compr.</div>
                            <div class="col-lg-4">
                                <input id="TxtNroComprobante" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nro. Compr." autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                            <div class="col-lg-2">C. de C.</div>
                            <div class="col-lg-4">
                                <div id="CboCentroCosto"></div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-2">Fecha Gasto</div>
                            <div class="col-lg-4">
                                <input id="TxtFechaGasto" class="DatoFormulario InputDatoFormulario datepicker" type="text" placeholder="Fecha Gasto" autocomplete="off">
                            </div>
                            <div class="col-lg-2">Cuenta</div>
                            <div class="col-lg-4">
                                <div id="CboCuenta"></div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-2">Fecha Pago</div>
                            <div class="col-lg-4">
                                <input id="TxtFechaPgo" class="DatoFormulario InputDatoFormulario datepicker" type="text" placeholder="Fecha Pago   " autocomplete="off">
                            </div>
                            <div class="col-lg-2">Tipo</div>
                            <div class="col-lg-4">
                                <div id="CboTipoGasto"></div>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-lg-2">Importe</div>
                            <div class="col-lg-4">
                                <input id="TxtImporte" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Importe" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnGuardarComprobante" href="#"><span id="SpanBtnGuardarComprobante"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-4"></div>
                        <div class="row mt-4">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnCerrarGasto" href="#"><span id="SpanBtnCerrarGasto"></span></a>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimirGasto" href="#"><span id="SpanBtnImprimirGasto"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

