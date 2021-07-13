<%@ Page Title="" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Adm_Sueldos.aspx.vb" Inherits="Forms_ADM_Frm_Adm_Sueldos" %>

<%@ Register
    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="cc1" %>
<asp:Content
    ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <script type="text/javascript">

    </script>
    <script
        src='<%= ResolveClientUrl("Frm_Indicadores.js?version=20210712")%>'></script>
    <asp:Button
        ID="btnSubirSueuldosOculto"
        runat="server"
        Style="visibility: hidden; display: none;" />
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
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-4">
                                </div>
                                <div class="col-7">
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-4">
                                </div>
                                <div class="col-8">
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-7">
                             <asp:Button
                                                ID="Upload"
                                                CssClass="btn btn-primary"
                                                runat="server"
                                                Text="Subir Archivo Sueldo"
                                                UseSubmitBehavior="False" />
                                            <br />
                                            <asp:Label
                                                ID="LblOK"
                                                runat="server"
                                                CssClass="bg-light text-success "></asp:Label>
                                            <asp:Label
                                                ID="LblError"
                                                runat="server"
                                                CssClass="bg-light text-danger "></asp:Label>
                        </div></div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

