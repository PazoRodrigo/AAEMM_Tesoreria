<%@ Page Title="AAEMM. Chequera" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Chequera.aspx.vb" Inherits="Forms_Administracion_Frm_Adm_Chequera" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Adm_Chequera.js")%>'></script>
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
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloGrillaDimensional"></span>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="row mt-1">
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Desde :</span></div>
                                    <div class="col-lg-3">
                                        <input id="TxtBuscadorDesde" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Hasta :</span></div>
                                    <div class="col-lg-3">
                                        <input id="TxtBuscadorHasta" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Estado :</span></div>
                                    <div class="col-lg-9"></div>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="row mt-5">
                                    <div class="col-12">
                                        <a href="#" type="button" class="btn btn-primary btn-md btn-block">
                                            <span class="glyphicon glyphicon-search"></span>Buscar
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1 text-center" style="height: 200px;">
                            <div class="col-12 mh-100" id="GrillaRegistrados"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="">Modificar de Estado de Cheque</span>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="row mt-1">
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Desde :</span></div>
                                    <div class="col-lg-3">
                                        <input id="TxtModificadorDesde" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Hasta :</span></div>
                                    <div class="col-lg-3">
                                        <input id="TxtModificadorHasta" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-2"><span class="SpanDatoFormulario">Estado :</span></div>
                                    <div class="col-lg-9"></div>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="row mt-3">
                                    <div class="col-12">
                                        <a href="#" type="button" class="btn btn-primary btn-md btn-block">
                                            <span class="glyphicon glyphicon-search"></span>Modificar Estado
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="">Próximo Cheque Disponible</span>
                                </div>
                            </div>
                            <div class="col-12 text-center">
                                <span id="SpanProximoCheque" style="font-size: 25px;">Próximo cheque</span>
                            </div>
                            <div class="row" style="margin-top: 120px;">
                                <div class="col-12 text-center">
                                    <div class="TituloDimensional">
                                        <span id="">Crear Chequera</span>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-10">
                                        <div class="row mt-1">
                                            <div class="col-lg-4"><span class="SpanDatoFormulario">Desde :</span></div>
                                            <div class="col-lg-8">
                                                <input id="TxtCrearDesde" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                            </div>
                                            <div class="col-lg-4"><span class="SpanDatoFormulario">Hasta :</span></div>
                                            <div class="col-lg-8">
                                                <input id="TxtCrearHasta" class="InputDatoFormulario" type="text" placeholder="Número" maxlength="10" autocomplete="off" onkeypress="return jsSoloNumeros(event);">
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-1"></div>
                                            <div class="col-10">
                                                <a href="#" type="button" class="btn btn-primary btn-md btn-block">
                                                    <span class="glyphicon glyphicon-search"></span>Crear Chequera
                                                </a>
                                            </div>
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
