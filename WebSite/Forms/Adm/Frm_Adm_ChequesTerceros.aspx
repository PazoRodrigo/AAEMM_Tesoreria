<%@ Page Title="AAEMM. Cheques Terceros" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_ChequesTerceros.aspx.vb" Inherits="Forms_Administracion_Frm_Adm_ChequesTerceros" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_ChequesTerceros.js?version20210417")%>'></script>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = "data:application/vnd.ms-excel;base64,",
                template =
                    '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>',
                base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)));
                },
                format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) {
                        return c[p];
                    });
                };
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table);
                var ctx = { worksheet: name || "Worksheet", table: table.innerHTML };
                window.location.href = uri + base64(format(template, ctx));
            };
        })();
    </script>
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
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>' class="LinkBtn" title="Volver a Configuración"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <div class="container border border-primary" style="height: 408px;">
                            <div class="col-12 text-center">Fecha Vencimiento</div>
                            <div class="container">
                                <div class="row mt-1 justify-content-center">
                                    <div class="col-5">
                                        <input type="text" id="BuscaDesdeVenc" class="form-control datepicker"
                                            onkeypress="return jsNoEscribir(event)" placeholder="Desde">
                                    </div>
                                    <div class="col-2"></div>
                                    <div class="col-5">
                                        <input type="text" id="BuscaHastaVenc" class="form-control datepicker"
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
                                    Cheques
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-8">
                        <div id="BotoneraSuperior" class="row border border-primary">
                            <div class="col-12">
                                <div class="row justify-content-between mt-1">
                                    <div class="col-3">
                                        <a id="BtnChequesRecibidos" class="btn btn-md btn-block btn-primary" href="#">Recibidos <span id="LblChequesRecibidos"></span></a>
                                    </div>
                                    <div class="col-3">
                                        <a id="BtnChequesDepositados" class="btn btn-md btn-block btn-warning" href="#">Depositados <span id="LblChequesDepositados"></span></a>
                                    </div>
                                    <div class="col-3">
                                        <a id="BtnChequesAcreditados" class="btn btn-md btn-block btn-success" href="#">Acreditados <span id="LblChequesAcreditados"></span></a>
                                    </div>
                                    <div class="col-3">
                                        <a id="BtnChequesRechazados" class="btn btn-md btn-block btn-danger" href="#">Rechazados <span id="LblChequesRechazados"></span></a>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                        <div id="divCantRegistrosBusqueda" style="display: none;">
                            <div class="container">
                                <div class="row mt-2 justify-content-between">
                                    <div class="col-4 text-right text-light text-bold">
                                        Registros : <span id="LblCantidadRegistrosGrilla" class="text-bold"></span>
                                    </div>
                                    <div class="col-4 text-right text-bold">
                                        <h4 id="LblValorSeleccion" class="text-light pr-3"></h4>
                                    </div>
                                    <div class="col-4">
                                        <a
                                    id="BtnExcelInscripciones"
                                    href="#"
                                    class="btn btn-block btn-info"
                                    onclick="tableToExcel('GrillaDetalle', 'Cheques')">Exportar a EXCEL</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="ContenedorSeleccionado" class="row" style="display: none;">
                            <div class="container border border-primary pb-3" style="height: 296px;">
                                <div id="ContenidoSeleccionado">
                                    <div class="row mt-3">
                                        <div id="IdEntidad" style="display: none;" class="datoCheque"></div>
                                        <div class="col-1">Cheque</div>
                                        <div class="col-3">
                                            <input type="text"
                                                id="EntidadNroCheque"
                                                onkeypress="return jsSoloNumeros(event)"
                                                class="form-control text-center datoCheque"
                                                placeholder="Nro. Cheque">
                                        </div>
                                        <div class="col-1">Banco</div>
                                        <div class="col-3">
                                            <div id="CboBanco"></div>
                                        </div>
                                        <div class="col-1">Importe</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadImporte" class="form-control text-right datoCheque"
                                                placeholder="Importe"
                                                readonly="readonly"
                                                onkeypress="return jsSoloNumeros(event)" />
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-1">Fecha</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadVenc"
                                                class="form-control datepicker text-center datoCheque" placeholder="Vencimiento">
                                        </div>
                                        <div class="col-1">Recibo</div>
                                        <div class="col-3">
                                            <input type="text" id="EntidadNroRecibo"
                                                readonly="readonly"
                                                class="form-control text-center datoCheque" placeholder="Nro. Recibo">
                                        </div>
                                        <div class="col-1">Estado</div>
                                        <div class="col-3">
                                            <input type="text"
                                                readonly="readonly"
                                                id="EntidadEstado" class="form-control text-center datoCheque"
                                                placeholder="Estado">
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-3 text-center">CUIT</div>
                                        <div class="col-2 text-center">Código</div>
                                        <div class="col-7 text-center">Razon Social</div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-3 d-flex justify-content-center">
                                            <input type="text" id="EntidadCUIT"
                                                onkeypress="return jsSoloNumerosSinPuntos(event)"
                                                class="form-control text-center datoCheque"
                                                readonly="readonly"
                                                placeholder="CUIT" maxlength="11"
                                                style="width: 140px;" />
                                        </div>
                                        <div class="col-2 d-flex justify-content-center">
                                            <input type="text" id="EntidadCodigoEntidad"
                                                class="form-control text-center datoCheque"
                                                readonly="readonly"
                                                placeholder="Código" maxlength="6"
                                                style="width: 120px;">
                                        </div>
                                        <div class="col-7 d-flex justify-content-center">
                                            <input type="text" id="EntidadRazonSocial"
                                                readonly="readonly"
                                                class="form-control datoCheque"
                                                placeholder="Razon Social">
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-2 text-light">Nuevo Estado</div>
                                        <div class="col-10">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input DivEstadoRecibido" style="display: none;" type="radio" name="RadioEstado" id="inlineRadioRecibido" value="0">
                                                <label class="form-check-label DivEstadoRecibido" style="display: none;" for="inlineRadioRecibido">Recibido</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input DivEstadoDepositado" style="display: none;" type="radio" name="RadioEstado" id="inlineRadioDepositado" value="1">
                                                <label class="form-check-label DivEstadoDepositado" style="display: none;" for="inlineRadioDepositado">Depositado</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input DivEstadoAcreditado" style="display: none;" type="radio" name="RadioEstado" id="inlineRadioAcreditado" value="2">
                                                <label class="form-check-label DivEstadoAcreditado" style="display: none;" for="inlineRadioAcreditado">Acreditado </label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input DivEstadoRechazado" style="display: none;" type="radio" name="RadioEstado" id="inlineRadioRechazado" value="10">
                                                <label class="form-check-label DivEstadoRechazado" style="display: none;" for="inlineRadioRechazado">Rechazado</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input DivEstadoVencido" style="display: none;" type="radio" name="RadioEstado" id="inlineRadioVencido" value="11">
                                                <label class="form-check-label DivEstadoVencido" style="display: none;" for="inlineRadioVencido">Vencido</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 justify-content-center">
                                        <div class="col-10">
                                            <a id="BtnGuardarCheque" href="#"
                                                class="btn btn-md btn-block btn-success">Guardar
                                                Nuevo Estado Cheque</a>
                                        </div>
                                    </div>
                                </div>
                                
                                <!-- <div id="DivSalvarCheque" style="display: block;"></div> -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>


