<%@ Page Title="" Language="VB" MasterPageFile="~/Forms/MP_Inicio.master" AutoEventWireup="false" CodeFile="Frm_Usu_EnviarPassword.aspx.vb" Inherits="Forms_Usuario_Frm_Usu_EnviarPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Usu_EnviarPassword.js?version20210321")%>'></script>

    <ul>
        <li>
            <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-4">
                    <div class="col-lg-12">
                        <div class="row mt-1">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-2">
                                        <span class="SpanDatoFormulario">Acceso</span>
                                    </div>
                                    <div class="col-8">
                                        <input id="TxtAcceso" class="DatoFormulario InputDatoFormulario" type="text" placeholder="UserName o Correo Electrónico" autocomplete="off">
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
                                    <a id="LinkBtnEnviarPassword" href="#"><span id="SpanBtnEnviarPassword"></span></a>
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

