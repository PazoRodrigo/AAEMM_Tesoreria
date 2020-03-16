<%@ Page Title="AAEMM. Gastos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Gasto.aspx.vb" Inherits="Forms_Frm_Gasto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Gasto.js")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
            }
            window.location = redirect;
        };
    </script>
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
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-1"></div>
                            <div class="col-7">
                                <input id="TxtBuscarNombre" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nombre" autocomplete="off">
                            </div>
                            <div class="col-1"></div>
                            <div class="col-3">
                                <div class="Boton BtnBuscar">
                                    <a id="LinkBuscar" href="#"><span id="SpanBtnBuscar"></span></a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1 text-center" style="height: 200px;">
                            <div class="col-12 mh-100" id="GrillaGastosRegistrados"></div>
                        </div>
                         <div class="row mt-1 text-center" style="height: 200px;">
                            <div class="col-12 mh-100" id="GrillaComprobantesRegistrados"></div>
                        </div>
                    </div>
                    <div class="col-lg-7">
                        <div class="row mt-1">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">Originario</div>
                            <div class="col-lg-3"><div id="CboOriginarioGasto"></div></div>
                            <div class="col-lg-3">Proveedor</div>
                            <div class="col-lg-3"><div id="CboProveedor"></div></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">Nro. Compr.</div>
                            <div class="col-lg-3">
                                <input id="TxtNroComprobante" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nro. Compr." autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                            <div class="col-lg-3">C. de C.</div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">Fecha Gasto</div>
                            <div class="col-lg-3">
                                <input id="TxtFechaGasto" class="DatoFormulario InputDatoFormulario datepicker" type="text" placeholder="Fecha Gasto" autocomplete="off">
                            </div>
                            <div class="col-lg-2">Cuenta</div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">Fecha Pago</div>
                            <div class="col-lg-3">
                                <input id="TxtFechaPgo" class="DatoFormulario InputDatoFormulario datepicker" type="text" placeholder="Fecha Pago   " autocomplete="off">
                            </div>
                            <div class="col-lg-2">Tipo</div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-2">Importe</div>
                            <div class="col-lg-3">
                                <input id="TxtImporte" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Importe" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

