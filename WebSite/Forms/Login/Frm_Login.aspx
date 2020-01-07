<%@ Page Title="AAEMM. Acceso al Sistema" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Login.aspx.vb" Inherits="Forms_Login_Frm_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Login/Frm_Login.js")%>'></script>

    <div id="Contenido">
        <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        <div id="Login" style="margin-top: 50px; width: 350px; margin-right: auto; margin-left: auto; border-radius: 15px; border: 1px solid aqua; padding: 15px;">
            <div id="DivImagenLogIn" style="width: 100px; margin-right: auto; margin-left: auto;">
                <img id="ImagenLogIn" style="border-radius: 150px; height: 100px; width: 100px;" src="../../Imagenes/ImagenLogIN.png" />
            </div>
            <div style="width: 75%; margin-top: 15px; margin-left: auto; margin-right: auto; background-color: #ffffff; height: 30px; color: gray; border-radius: 15px; font-size: 20px; padding-left: 15px; padding-top: 10px;">
                <span class="icon-exit"></span>
                <input style="font-family: 'Karla', sans-serif; width: 200px; border: none; font-size: 18px; padding-left: 15px;" id="txtUser" type="text" placeholder="Usuario" maxlength="20">
            </div>
            <div style="width: 75%; margin-top: 5px; margin-left: auto; margin-right: auto; background-color: #ffffff; height: 30px; color: gray; border-radius: 15px; font-size: 20px; padding-left: 15px; padding-top: 10px;">
                <span class="icon-exit"></span>
                <input style="font-family: 'Karla', sans-serif; width: 200px; border: none; font-size: 18px; padding-left: 15px;" id="txtPass" type="password" placeholder="Contraseña" maxlength="8">
            </div>
            <div style="font-family: 'Anton', sans-serif;width: 80%; margin-top: 15px; text-align: center; margin-left: auto; margin-right: auto; background-color: #ffffff; height: 30px; color: gray; border-radius: 15px; font-size: 20px; padding-left: 15px; padding-top: 10px;">
                <a id="BtnLogin" href="../Frm_Indicadores.aspx" style="display: block;">Acceder al Sistema</a>
            </div>
            <div style="margin-top: 10px; text-align: right; font-size: 10px;">
                <a href="#" style="color: yellow;">Ha olvidado su Contraseña?</a>
            </div>
        </div>
    </div>
</asp:Content>

