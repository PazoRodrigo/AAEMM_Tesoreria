<%@ Page Title="AAEMM. Archivos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Archivos.aspx.vb" Inherits="Forms_Frm_Dist_Archivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Dist_Archivos.js?version20200428_1")%>'></script>
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
                <div class="row mt-1">
                    <div class="col-lg-4">
                        <nav>
                            <ul class="Menu">
                                <li class="BtnDistribuidor">
                                    <ul class="SubMenu">
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Arc/Frm_Arc_Extracto.aspx")%>'>(Alt + E)  - Extracto / Conformados</a></li>
                                        <li><a href='<%= ResolveClientUrl("~/Forms/Arc/Frm_Arc_Ingresos.aspx")%>'>(Alt + P)  - Ingresos</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </nav>
                    </div>
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </li>
    </ul>

</asp:Content>

