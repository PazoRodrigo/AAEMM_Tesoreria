<%@ Page Title="AAEMM. Ingresos" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Arc_Ingresos.aspx.vb" Inherits="Forms_ARC_Frm_Arc_Ingresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("Frm_Arc_Ingresos.js?version20200428_1")%>'></script>
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
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario"></span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Archivos.aspx")%>' class="LinkBtn" title="Volver a Archivos"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1 justify-content-center">
                    <div class="col-lg-3">
                        <div style="width: 90%; margin-left: auto; margin-right: auto; text-align: center; padding-top: 10px;">
                            <span id="SpanIngresoArchivos" class="AAEMM" style="font-size: 15px; color: #fff;">Ingreso de Archivos</span>
                            <div style="width: 95%; margin-right: auto; margin-left: auto;">
                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                <br />
                                <asp:Button ID="Upload" CssClass="btn btn-primary" runat="server" Text="Subir" UseSubmitBehavior="False" />
                                <br />
                                <asp:Label ID="LblError" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1"></div>
                    <div class="col-lg-2 mt-2">
                        <h4 class="bg-warning text-center">Archivos BN </h4>
                        <div id="GrillaArchivos1" style="height:370px;overflow-y:scroll;"></div>
                    </div>
                    <div class="col-lg-2 mt-2">
                        <h4 class="bg-warning text-center">Archivos PF</h4>
                        <div id="GrillaArchivos2" style="height:370px;overflow-y:scroll;"></div>
                    </div>
                    <div class="col-lg-2 mt-2">
                        <h4 class="bg-warning text-center">Archivos TR</h4>
                        <div id="GrillaArchivos3" style="height:370px;overflow-y:scroll;"></div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>

