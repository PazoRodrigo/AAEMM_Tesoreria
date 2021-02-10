<%@ Page Title="AAEMM. Acceso al Sistema" Language="VB" MasterPageFile="~/Forms/MP_Inicio.master" AutoEventWireup="false" CodeFile="Frm_Login.aspx.vb" Inherits="Forms_Login_Frm_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Login.js?version20210209")%>'></script>
    <div id="Contenido">
        <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        <div id="DivLogin">
            <div id="DivImagenLogIn">
                <img id="ImagenLogIn" src="../../Imagenes/ImagenLogIN.png" />
            </div>
            <div class="DivInputLogIN">
                <span class="icon-user IconoLogIN"></span>
                <input class="InputLogIN" id="txtUser" type="text" placeholder="Usuario" maxlength="20">
            </div>
            <div class="DivInputLogIN">
                <span class="icon-key2 IconoLogIN"></span>
                <input class="InputLogIN" id="txtPass" type="password" placeholder="Contraseña" maxlength="8">
            </div>
            <div id="BtnInputLogIN">
                <a id="BtnLogin" style="display: block;">Acceder al Sistema</a>
            </div>
            <div style="margin-top: 10px; text-align: right; font-size: 10px;">
                <a href='<%= ResolveClientUrl("~/Forms/Usuario/Frm_Usu_EnviarPassword.aspx") %>'style="color: yellow;">Ha olvidado su Contraseña?</a>
            </div>
        </div>
    </div>
</asp:Content>

