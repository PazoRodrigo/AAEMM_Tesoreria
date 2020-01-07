<%@ Page Title="AAEMM. Administración" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Administracion.aspx.vb" Inherits="Forms_Frm_Dist_Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Dist_Administracion.js")%>'></script>

    <div id="Contenido">
        <div>
            <a href="Frm_Indicadores.aspx" id="IcIndicadores" title="Indicadores">
                <div id="DivBtnIndicadores">
                    <span class="icon-stats-dots"></span>
                </div>
            </a>
            <div id="DivNombreFormulario90"><span id="NombreFormulario"></span></div>
        </div>
        <div class="DivDistribuidor">
            <nav>
                <ul>
                    <li class="BtnDistribuidor">
                        <ul class="SubMenu">
                           <li><a href="#">Empresas</a></li>
                            <li><a href="#">Empleados</a></li>
                            <li><a href="#">Cheques Propios</a></li>
                            <li><a href="#">Cheques Rechazados</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>

