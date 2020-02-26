<%@ Page Title="AAEMM. Cheques Propios" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_ChequesPropios.aspx.vb" Inherits="Forms_Administracion_ChequesPropios" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Adm_ChequesPropios.js")%>'></script>
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
                    <div class="col-lg-7">
                        <div class="col-12 text-center">
                            <div class="TituloDimensional">
                                <span id="SpanTituloGrillaDimensional"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-9">
                                <input id="TxtBuscadorCheque" class="InputDatoFormulario" type="text" placeholder="Número / Importe / Fecha (dd/MM/aaaa)" autocomplete="off" />
                            </div>
                            <div class="col-lg-2">
                                <a href="#" type="button" class="btn btn-primary btn-md btn-block">
                                    <span class="glyphicon glyphicon-search"></span>Buscar
                                </a>
                            </div>
                        </div>
                        <div class="row mt-1 text-center" style="height: 350px; overflow-y: scroll;">
                            <div class="col-11 mh-100" id="GrillaRegistrados"></div>
                        </div>
                    </div>
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="">Cheque Propio</span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                Lista de Estados
                            </div>
                            <div class="col-lg-9">
                                <div class="row">
                                    <div class="col-lg-5"><span class="SpanDatoFormulario">Número :</span></div>
                                    <div class="col-7">
                                        <input id="TxtNumero" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5"><span class="SpanDatoFormulario">Importe :</span></div>
                                    <div class="col-7">
                                        <input id="TxtImporte" class="InputDatoFormulario" type="text" placeholder="Importe" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5"><span class="SpanDatoFormulario">Fecha Emisión :</span></div>
                                    <div class="col-7">
                                        <input id="lblFechaEmision" class="InputDatoFormulario" type="text" placeholder="Emisión" autocomplete="off" readonly="readonly">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5"><span class="SpanDatoFormulario">Fecha Débito :</span></div>
                                    <div class="col-7">
                                        <input id="lblFechaDebito" class="InputDatoFormulario datepicker" type="text" placeholder="Débito" autocomplete="off">
                                    </div>
                                </div>

                                <%--                                
                                <div class="col-4">Fecha Emisión</div>
                                <div class="col-8">
                                    <input id="lblFechaEmision" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                </div>
                                <div class="col-4">fecha Débito</div>
                                <div class="col-8">
                                    <input id="TxtFechaDebito" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                </div>--%>
                            </div>
                        </div>
                        <%--     --%>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>
