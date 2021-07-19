<%@ Page Title="AAEMM. Cuenta Contable" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_ABM_CuentaContable.aspx.vb" Inherits="Forms_Frm_ABM_CuentaContable" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_ABM_CuentaContable.js?version=20210717")%>'></script>
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
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>' class="LinkBtn" title="Volver a Configuración"><span class="icon-circle-left"></span></a>
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
                        </div>
                        <div class="row mt-1 text-center mh-100" style="height: 350px;">
                            <div class="col-12 mh-100" id="GrillaRegistrados"></div>
                        </div>
                      <div class="row mt-3">
                            <div class="col-1"></div>
                            <div class="col-10 text-center">
                                <div class="Boton BtnImprimir">
                                    <a id="LinkBtnImprimir" href="#"><span id="SpanBtnImprimir"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                 <%--Entidad--%>
                    <div class="col-lg-5">
                        <div class="row mt-1">
                            <div class="col-12 text-center">
                                <div class="TituloDimensional">
                                    <span id="SpanTituloDimensional"></span>
                                </div>
                            </div>
                        </div>
                        <div id="DatosEntidad" style="height: 365px;">
                            <div class="row mt-1">
                                <div class="col-1"></div>
                                <div class="col-4">
                                    <div class="Boton BtnNuevo">
                                        <a id="LinkBtnNuevo" href="#"><span id="SpanBtnNuevo"></span></a>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Nombre</span>
                                </div>
                                <div class="col-7">
                                    <input id="TxtNombre" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nombre" autocomplete="off">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                    <span class="SpanDatoFormulario">Tipo</span>
                                </div>
                                <div class="col-8">
                                    <div id="CboTipoCuenta"></div>
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
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-7"></div>
                            <div class="col-lg-4">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnGuardar" href="#"><span id="SpanBtnGuardar"></span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>
