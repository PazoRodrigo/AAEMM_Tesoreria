<%@ Page Title="AAEMM. Tipos de Gasto" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_ABM_TipoGasto.aspx.vb" Inherits="Forms_Frm_ABM_TipoGasto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_ABM_TipoGasto.js")%>'></script>

    <div id="Contenido">
        <div>
            <a href="Frm_Indicadores.aspx" id="IcIndicadores" title="Indicadores">
                <div id="DivBtnIndicadores">
                    <span class="icon-stats-dots"></span>
                </div>
            </a>
            <div id="DivNombreFormulario80"><span id="NombreFormulario"></span></div>
            <a href="Frm_Dist_Configuracion.aspx" id="IcVolver" title="Volver">
                <div id="DivBtnVolver">
                    <span class="icon-circle-left"></span>
                </div>
            </a>
        </div>
        <div id="DivContenedor60" style="margin-top: 15px; text-align: center;">
            <div id="TituloGrillaDimensional" style="font-family: 'Anton', sans-serif; color: #ffffff;">
                <span id="SpanTituloGrillaDimensional"></span>
            </div>
            <div id="DivGrillaRegistrados" style="margin-top: 10px; width: 90%; margin-left: auto; margin-right: auto; height: 350px; overflow-y: scroll;">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left; padding-left: 10px;"><a href="#" style="display: block;">Item  1</a></td>
                        <td style="width: 5%; font-size: 25px;"><a href="#" style="display: block; color: red;"><span class="icon-bin"></span></a></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-left: 10px;"><a href="#" style="display: block;">Item  2</a></td>
                        <td style="width: 5%; font-size: 25px;"><a href="#" style="display: block; color: red;"><span class="icon-bin"></span></a></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; padding-left: 10px;"><a href="#" style="display: block;">Item  3</a></td>
                        <td style="width: 5%; font-size: 25px;"><a href="#" style="display: block; color: red;"><span class="icon-bin"></span></a></td>
                    </tr>
                </table>
            </div>
            <input id="BtnImprimir" type="button" style="width: 75%; margin-top: 15px; margin-left: auto; margin-right: auto; background-color: #ffffff; height: 40px; color: gray; border-radius: 15px; font-size: 15px;">
        </div>
        <div id="DivContenedor40" style="margin-top: 15px; text-align: center;">
            <div id="TituloDimensional" style="font-family: 'Anton', sans-serif; color: #ffffff;">
                <span id="SpanTituloDimensional"></span>
            </div>
            <div id="DivContenidoDimensional" style="margin-top: 10px; width: 90%; margin-left: auto; margin-right: auto; height: 350px; padding-top: 10px;">
                <div style="height: 40px; line-height: 40px;">
                    <span style="float: left; padding-left: 15px; font-family: 'Karla', sans-serif;">Nombre :</span>
                    <input style="font-family: 'Karla', sans-serif; background-color: transparent; width: 200px; border: none; border-bottom: 1px double; font-size: 20px; padding-left: 15px;" id="txtNombre" type="text" placeholder="Nombre">
                </div>
            </div>
            <div style="float: right;">
                <div style="width: 125px; margin-top: 5px; background-color: green; height: 40px; line-height: 40px; color: darkgreen; border-radius: 15px; font-size: 15px;">
                    <a id="BtnGuardar" href="#" style="display: block; color: #fff;"><span class="icon-floppy-disk"></span><span style="font-family: 'Anton', sans-serif; padding-left: 10px;">Guardar</span></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

