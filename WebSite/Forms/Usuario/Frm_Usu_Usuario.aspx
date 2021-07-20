<%@ Page Title="" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Usu_Usuario.aspx.vb" Inherits="Forms_Usuario_Frm_Usu_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Usu_Usuario.js?version=20210718_")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
                window.location = redirect;
            }
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
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-4">
                    <div class="col-lg-12">
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Nombre</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtNombre" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nombre" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Apellido</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtApellido" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Apellido" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Usuario</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtUserName" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Usuario" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Correo Electrónico</span>
                                    </div>
                                    <div class="col-7">
                                        <input id="TxtCorreoElectronico" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Correo Electrónico" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Nro. Interno</span>
                                    </div>
                                    <div class="col-2">
                                        <input id="TxtNroInterno" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nro. Interno" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-2">
                                <div class="Boton BtnAlerta">
                                    <a id="LinkBtnModificarPassword" href="#"><span id="SpanBtnModificarPassword"></span></a>
                                </div>
                            </div>
                            <div class="col-lg-2"></div>
                            <div class="col-lg-2">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnGuardar" href="#"><span id="SpanBtnGuardar"></span></a>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                    </div>
                </div>
                <div class="row mt-4" id="DivPassword" style="display: none;">
                    <div class="col-lg-12">
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Contraseña Anterior</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtPasswordAnterior" class="DatoFormulario InputDatoFormulario" type="password" placeholder="Anterior" autocomplete="off" maxlength="10">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Contraseña Nueva</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtPasswordNueva" class="DatoFormulario InputDatoFormulario" type="password" placeholder="Nueva" autocomplete="off" maxlength="10">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-4">
                                        <span class="SpanDatoFormulario">Contraseña Valida</span>
                                    </div>
                                    <div class="col-3">
                                        <input id="TxtPasswordValida" class="DatoFormulario InputDatoFormulario" type="password" placeholder="Valida" autocomplete="off" maxlength="10">
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-4"></div>
                            <div class="col-lg-2">
                                <div class="Boton BtnGuardar">
                                    <a id="LinkBtnGuardarPassword" href="#"><span id="SpanBtnGuardarPassword"></span></a>
                                </div>
                            </div>
                            <div class="col-lg-3"></div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

