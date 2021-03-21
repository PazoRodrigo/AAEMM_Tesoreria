<%@ Page Title="AAEMM. Cuenta Corriente" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_CuentaCorriente.aspx.vb" Inherits="Forms_ADM_Frm_Adm_CuentaCorriente" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Adm_CuentaCorriente.js?version20210321")%>'></script>
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
                <div class="row mt-1">
                    <div class="col-9">
                        <div class="row">
                            <div class="col-3">
                                <a href="#" id="LinkRECAUDADORA">
                                    <div class="col-10 bg-primary text-center font-italic text-light">RECAUDADORA</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h5 id="spanSaldoRECAUDADORA"></h5>
                                    </div>
                                </a>
                            </div>
                            <div class="col-3">
                                <a href="#" id="LinkPAGADORA">
                                    <div class="col-10 bg-primary text-center font-italic text-light">PAGADORA</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h5 id="spanSaldoPAGADORA"></h5>
                                    </div>
                                </a>
                            </div>
                            <div class="col-3">
                                <a href="#" id="LinkCAJA">
                                    <div class="col-10 bg-primary text-center font-italic text-light">CAJA</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h5 id="spanSaldoCAJA"></h5>
                                    </div>
                                </a>
                            </div>
                            <div class="col-3">
                                <a href="#" id="LinkFONDOFIJO">
                                    <div class="col-10 bg-primary text-center font-italic text-light">FONDO FIJO</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h5 id="spanSaldoFONDOFIJO"></h5>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="row">
                            <div class="col-6">
                                <a href="#" id="LinkANTICIPO">
                                    <div class="col-10 bg-primary text-center font-italic text-light">ANTICIPOS</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h6 id="spanSaldoANTICIPOS"></h6>
                                    </div>
                                </a>
                            </div>
                            <div class="col-6">
                                <a href="#" id="LinkPRESTAMO">
                                    <div class="col-10 bg-primary text-center font-italic text-light">PRESTAMOS</div>
                                    <div class="col-10 bg-dark text-right text-light pt-3 pb-1">
                                        <h6 id="spanSaldoPRESTAMOS"></h6>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-5">
                        <div class="TituloDimensional">
                            <span id="SpanTituloGrillaDimensional"></span>
                        </div>
                    </div>
                    <div class="col-5">
                        <div class="TituloDimensional">
                            <span id="SpanTituloAsientos"></span>
                        </div>
                    </div>
                    <div class="col-2">
                        <input type="text" id="TxtFecha" class="form-control datepicker text-center" placeholder="Fecha">
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-5">
                        <div style="height: 300px; overflow-y: scroll;">
                            <div id="DivGrillaAsientos"></div>
                        </div>
                    </div>
                    <div class="col-7">
                        <div class="row">
                            <div class="col-2 text-center bg-info pt-1">
                                <label class="radio-inline">
                                    <input type="radio" name="radioDH" value="0">D</label>
                                <label class="radio-inline">
                                    <input type="radio" name="radioDH" value="1">H</label>
                            </div>
                            <div class="col-10 text-center bg-info pt-2">
                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <div id="CboCuentaContable"></div>

                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <input type="text" id="TxtImporte" class="form-control"
                                            onkeypress="return jsSoloNumeros(event)" placeholder="Importe">
                                    </div>
                                    <div class="col-3">
                                        <a href="#" id="BtnAgregarLinea" class="btn btn-md btn-block btn-primary">AGREGAR</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-12">
                                <div id="DivGrillaLineasAsiento" style="height: 160px; overflow-y: scroll; padding-right: 35px;"></div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-2 text-right text-light">
                            </div>
                            <div class="col-1 text-right text-light">
                                <h6>Debe:</h6>
                            </div>
                            <div class="col-4 text-right text-light">
                                <h5 id="LblImporteTotalAsientoD"></h5>
                            </div>
                            <div class="col-1 text-right text-light">
                                <h6>Haber:</h6>
                            </div>
                            <div class="col-4 text-right text-light">
                                <h5 id="LblImporteTotalAsientoH"></h5>
                            </div>
                        </div>
                        <div class="row mt-1 justify-content-center">
                            <div class="col-10">
                                <a href="#" id="BtnGuardarAsiento" class="btn btn-md btn-block btn-success">GUARDAR ASIENTO</a>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>


