<%@ Page Title="AAEMM. Empleados"  Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Empleados.aspx.vb" Inherits="Forms_Adm_Frm_Adm_Empleados" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Adm_Empleados.js")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = '';
            if (e.which == 27) {
                redirect = '<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>';
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
            <div class="Porc60_L">
               <%-- <ul>
                    <li>
                        <div class="TituloDimensional" style="">
                            <span id="SpanTituloGrillaDimensional"></span>
                        </div>
                    </li>
                    <li>
                        <div id="DivGrillaRegistrados" style="margin-top: 10px; width: 90%; margin-left: auto; margin-right: auto; height: 350px; overflow-y: scroll;">
                            <div id="GrillaRegistrados"></div>
                        </div>
                    </li>
                </ul>--%>
            </div>
            <div class="Porc40_L">
              <%--  <ul>
                    <li>
                        <div class="TituloDimensional">
                            <span id="SpanTituloDimensional"></span>
                        </div>
                    </li>
                    <li class="Formulario">
                        <div class="Boton BtnNuevo" style="float: left; margin-left: 30px; margin-bottom: 15px;">
                            <a id="LinkBtnNuevo" href="#"><span id="SpanBtnNuevo"></span></a>
                        </div>
                    </li>
                    <li class="Formulario">
                        <span class="SpanDatoFormulario">Nombre</span>
                        <input id="TxtNombre" class="DatoFormulario InputDatoFormulario" type="text" placeholder="Nombre" autocomplete="off">
                    </li>
                    <li class="Formulario">
                        <span class="SpanDatoFormulario">Observaciones</span>
                        <textarea id="TxtObservaciones" class="DatoFormulario TextareaDatoFormulario" placeholder="Observaciones"></textarea>
                    </li>
                </ul>--%>
            </div>
        </li>
        <li>
            <div class="Porc60_L">
               <%-- <ul>
                    <li>
                        <div class="Boton BtnImprimir">
                            <a id="LinkBtnImprimir" href="#"><span id="SpanBtnImprimir"></span></a>
                        </div>
                    </li>
                </ul>--%>
            </div>
            <div class="Porc40_L">
            <%--    <ul>
                    <li class="Formulario">
                        <div class="Boton BtnGuardar" style="float: right; margin-right: 30px;">
                            <a id="LinkBtnGuardar" href="#"><span id="SpanBtnGuardar"></span></a>
                        </div>
                    </li>
                </ul>--%>
            </div>
        </li>
    </ul>
</asp:Content>



