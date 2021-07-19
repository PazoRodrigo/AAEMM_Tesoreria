<%@ Page Title="AAEMM. Empresas Cta. Cte." Language="VB"
    MasterPageFile="~/Forms/MP.master" AutoEventWireup="false"
    CodeFile="Frm_Adm_EmpresaCtaCte.aspx.vb"
    Inherits="Forms_ADM_Frm_Adm_EmpresaCtaCte" %>

<asp:Content
    ID="Contenido"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <script
        src='<%= ResolveClientUrl("Frm_Adm_EmpresaCtaCte.js?version=20210718")%>'></script>
    <script>
        document.onkeyup = function (e) {
            let redirect = "";
            if (e.which == 27) {
                redirect =
          '<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>';
                window.location = redirect;
            }
        };
    </script>
    <ul>
        <li>
            <div id="BtnIndicadores" class="Cabecera Porc10_L">
                <a
                    id="LinkBtnInidicadores"
                    href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
                    class="LinkBtn"
                    title="Indicadores"><span class="icon-stats-dots"></span></a>
            </div>
            <div id="DivNombreFormulario" class="Cabecera Porc80_L">
                <span id="SpanNombreFormulario"></span>
            </div>
            <div id="BtnVolver" class="Cabecera Porc10_L">
                <a
                    href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Empresa.aspx")%>'
                    class="LinkBtn"
                    title="Volver a la Empresa"><span class="icon-circle-left"></span></a>
            </div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <h1 class="text-light" id="NombreEmpresa">Puert</h1>
                    </div>
                </div>
                <div class="row mb-3 text-light">
                    <div class="col-2 text-right pr-4">Fecha Desde :</div>
                    <div class="col-2 text-center">
                        <input id="TxtFechaDesde"
                            class="datepicker text-center" type="text"
                            placeholder="Fecha" autocomplete="off"
                            onkeypress="return jsNoEscribir(event)">
                    </div>
                    <div class="col-3">
                        <a href="#" id="BtnTraerMovimientos" class="btn btn-block btn-info">Traer Movimientos</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-7">
                        <div class="row" style="height: 300px; overflow-y: scroll">
                            <div class="col-12">
                                <div id="GrillaMovimientos"></div>
                            </div>
                        </div>
                        <div class="row justify-content-end text-light mt-3">
                            <div class="col-3 text-right pr-3">
                                <h2>Saldo :</h2>
                            </div>
                            <div class="col-4 text-right pr-4">
                                <h2 id="TxtSaldo"></h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-5 text-light">
                        <div class="row justify-content-center ">
                            <div class="col-12 text-center">
Movimiento
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-center pr-3">Tipo</div>
                            <div class="col-9 text-center pl-4">Nombre
                            </div>
                        </div>
                        <div class="row text-light">
                            <div class="col-3 text-right pr-3">
                                <div id="CboDH"></div>
                            </div>
                            <div class="col-9 text-right pl-4">
                                <div id="CboTipoMovimiento"></div>
                            </div>
                            <div class="row mt-3 text-light">
                                <div class="col-3 text-right">Importe</div>
                                <div class="col-4 text-right">
                                    <input type="text" id="TxtImporte"
                                    placeholder="Importe" onkeypress="return jsSoloNumeros(event)"
                                    class="form-control">
                                </div>
                                <div class="col-5 text-right">
                                    <a href="#" id="BtnGuardar" class="btn btn-block btn-success">Guardar Movimiento</a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</asp:Content>
