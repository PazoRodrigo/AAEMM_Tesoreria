<%@ Page Title="AAEMM. Reportes" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Reportes.aspx.vb" Inherits="Forms_Frm_Dist_Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Dist_Reportes.js")%>'></script>

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
                <ul class="Menu">
                    <li class="BtnDistribuidor">
                        <ul class="SubMenu">
                            <li><a href="#">Familiares hasta 5 Años</a></li>
                            <li><a href="#">Familiares hasta 18 Años</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>

