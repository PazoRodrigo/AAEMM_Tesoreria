<%@ Page Title="" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Rep_Gastos2.aspx.vb" Inherits="Forms_ADM_Frm_Rep_Gastos2" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Rep_Gastos2.js?version20201028")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>';
                window.location = redirect;
            }
        };
        var strBusqueda = '';
        var strTipoBusqueda = '';
        $('body').on('click', '#BtnImprimir', async function (e) {
            var myHFBusqueda = document.getElementById('<%= hfBusqueda.ClientID%>');
            myHFBusqueda.value = strBusqueda;
            console.log(strBusqueda);
            var myHFTipoBusqueda = document.getElementById('<%= hfTipoBusqueda.ClientID%>');
            myHFTipoBusqueda.value = strTipoBusqueda;
            if (myHFBusqueda.value.length > 0) {
                $("#Reporte").css('display', 'none');
                $("#divCantRegistrosImprimir").css('display', 'none');
                $("#Grilla").css('display', 'none');
                if (myHFTipoBusqueda.value == 'C') {
                    $('#<%= BtnOcultoImprimirComprobantes.ClientID%>').click();
                } else {
                    $('#<%= BtnOcultoImprimirGastos.ClientID%>').click();
                }
                $("#Reporte").css('display', 'block');
            }
        })
    </script>
    <asp:HiddenField ID="hfTipoBusqueda" runat="server" />
    <asp:HiddenField ID="hdntxtbxTaksit" runat="server" Value="" Visible="false" />
    <asp:HiddenField ID="hfBusqueda" runat="server" />
    <asp:Button ID="BtnOcultoImprimirGastos" runat="server" Style="visibility: hidden; display: none;" />
    <asp:Button ID="BtnOcultoImprimirComprobantes" runat="server" Style="visibility: hidden; display: none;" />

    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario">Reporte Gastos y Comprobantes</span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>' class="LinkBtn"
                    title="Volver a Reportes"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary">
                            <div class="row pt-2 pb-3 bg-info">
                                <div class="col-6">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" name="BuscaGastoComprobante"
                                            id="RBuscaGasto" value="RGasto">
                                        <label class="form-check-label" for="RBuscaGasto">Gastos</label>
                                    </div>
                                </div>
                                <div class="col-6 ">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" name="BuscaGastoComprobante"
                                            id="RBuscaComprobante" value="RComprobante">
                                        <label class="form-check-label" for="RBuscaComprobante">Comprobantes</label>
                                    </div>
                                </div>
                            </div>
                            <div id="divBuscadorGasto" style="display: none; height: 385px;">
                                <div class="row mt-1">
                                    <div class="container">
                                        <div class="row mt-2 justify-content-center">
                                            <div class="col-5">
                                                <input type="text" id="BuscaGastoDesde"
                                                    class="form-control datepicker text-center"
                                                    onkeypress="return jsNoEscribir(event)" placeholder="Desde">
                                            </div>
                                            <div class="col-1"></div>
                                            <div class="col-5">
                                                <input type="text" id="BuscaGastoHasta"
                                                    class="form-control datepicker text-center"
                                                    onkeypress="return jsNoEscribir(event)" placeholder="Hasta">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-4">Estado</div>
                                    <div class="col-8">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" name="CheckEstadoGasto"
                                                value="1" id="BuscaGastoEstadoA">
                                            <label class="custom-control-label" for="BuscaGastoEstadoA">Abierto</label>
                                        </div>
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input" name="CheckEstadoGasto"
                                                value="2" id="BuscaGastoEstadoC">
                                            <label class="custom-control-label" for="BuscaGastoEstadoC">Cerrado</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divBuscadorComprobante" style="display: none; height: 385px;">
                                <div class="row mt-1">
                                    <div class="container">
                                        <div class="row mt-2 justify-content-center">
                                            <div class="col-5">
                                                <input type="text" id="BuscaComprobanteDesde"
                                                    class="form-control datepicker text-center"
                                                    onkeypress="return jsNoEscribir(event)" placeholder="Desde">
                                            </div>
                                            <div class="col-1"></div>
                                            <div class="col-5">
                                                <input type="text" id="BuscaComprobanteHasta"
                                                    class="form-control datepicker text-center"
                                                    onkeypress="return jsNoEscribir(event)" placeholder="Hasta">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Estado</div>
                                    <div class="col-3">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input"
                                                name="CheckEstadoComprobante" id="BuscaComprobanteEstadoP" value="1">
                                            <label class="custom-control-label"
                                                for="BuscaComprobanteEstadoP">Pagado</label>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input"
                                                name="CheckEstadoComprobante" id="BuscaComprobanteEstadoP" value="0">
                                            <label class="custom-control-label"
                                                for="BuscaComprobanteEstadoP">Pendiente</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Gasto</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteGasto"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Originario</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteOriginarioGasto"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Proveedor</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteProveedor"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">C. de C.</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteCentroCosto"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Pago</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteTipoPago"></div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-3">Cuenta</div>
                                    <div class="col-9">
                                        <div id="CboBuscaComprobanteCuenta"></div>
                                    </div>
                                </div>
                                <div class="row mt-1 justify-content-center">
                                    <div class="col-5">
                                        <input type="text" id="BuscaComprobanteImporte" class="form-control text-center"
                                            onkeypress="return jsSoloNumeros(event)" placeholder="Importe" />
                                    </div>
                                    <div class="col-1"></div>
                                    <div class="col-5">
                                        <input type="text" id="BuscaComprobanteNroComprobante" class="form-control"
                                            onkeypress="return jsSoloNumerosSinPuntos(event)"
                                            placeholder="Nro. Compr." />
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-1 justify-content-end">
                                <div class="col-8 mr-2">
                                    <a href="#" id="BtnBuscar" class="btn btn-md btn-block btn-primary">Buscar
                                    </a>
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
                        <div id="divCantRegistrosImprimir" style="display: none;">
                            <div class="container">
                                <div class="row mt-1 justify-content-center">
                                    <div class="col-8 mr-2">
                                        <a href="#" id="BtnImprimir" class="btn btn-md btn-block btn-light">Imprimir
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Reporte" class="container-fluid" style="height: 480px; overflow-y: scroll;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>

</asp:Content>