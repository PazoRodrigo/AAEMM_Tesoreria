<%@ Page Title="AAEMM. Rpt. Comprobantes" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Rep_Comprobante.aspx.vb" Inherits="Forms_Reportes_Frm_Rep_Comprobante" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Rep_Comprobante.js?version=20210729")%>'></script>
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
            if (myHFBusqueda.value.length > 0) {
                $("#Reporte").css('display', 'none');
                $("#divCantRegistrosImprimir").css('display', 'none');
                $("#Grilla").css('display', 'none');
                $('#<%= BtnOcultoImprimirComprobantes.ClientID%>').click();
                $("#Reporte").css('display', 'block');
            }
        })
    </script>
    <asp:HiddenField ID="hfBusqueda" runat="server" />
    <asp:Button ID="BtnOcultoImprimirComprobantes" runat="server" Style="visibility: hidden; display: none;" />

    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a id="LinkBtnInidicadores" href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn" title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario">Reporte Comprobantes</span>
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
                        <div class="container border border-primary" style="height: 408px;">
                            <div id="divBuscadorComprobante" style="height: 385px;">
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
                                                name="CheckEstadoComprobante" id="BuscaComprobanteEstadoPa" value="1">
                                            <label class="custom-control-label"
                                                for="BuscaComprobanteEstadoPa">Pagado</label>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="custom-control custom-switch">
                                            <input type="checkbox" class="custom-control-input"
                                                name="CheckEstadoComprobante" id="BuscaComprobanteEstadoPe" value="0">
                                            <label class="custom-control-label"
                                                for="BuscaComprobanteEstadoPe">Pendiente</label>
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
                        </div>
                        <div class="row mt-2">
                            <div class="col-1"></div>
                            <div class="col-10">
                                <a href="#" id="BtnBuscar" class="btn btn-md btn-block btn-primary">Buscar
                                </a>
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
                                <div class="row mt-2 justify-content-between">
                                    <div class="col-4 text-right text-light text-bold">
                                        <a href="#" id="BtnImprimir" class="btn btn-md btn-block btn-light">Consultar
                                            para Imprimir
                                        </a>
                                    </div>

                                    <div class="col-6 text-right text-bold">
                                        <h4 id="LblValorSeleccion" class="text-light pr-3"></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Reporte" class="container-fluid" style="height: 450px; overflow-y: scroll;">
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