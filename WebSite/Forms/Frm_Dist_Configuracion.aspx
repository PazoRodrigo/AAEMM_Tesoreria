<%@ Page Title="AAEMM. Configuración" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Dist_Configuracion.aspx.vb" Inherits="Forms_Frm_Dist_Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Dist_Configuracion.js")%>'></script>

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
                            <li><a href="Frm_ABM_CentroCostos.aspx">Centro de Costos</a></li>
                            <li><a href="Frm_ABM_Convenios.aspx">Convenios</a></li>
                            <li><a href="Frm_ABM_OriginarioGasto.aspx">Originario de Gasto</a></li>
                            <li><a href="Frm_ABM_PlanCuentas.aspx">Plan de Cuentas</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
        </div>
        <div class="DivDistribuidor">
            <nav>
                <ul>
                    <li class="BtnDistribuidor">
                        <ul class="SubMenu">
                            <li><a href="Frm_ABM_TipoContacto.aspx">Tipo de Contacto</a></li>
                            <li><a href="Frm_ABM_TipoDomicilio.aspx">Tipo de Domicilio</a></li>
                            <li><a href="Frm_ABM_TipoGasto.aspx">Tipo de Gasto</a></li>
                            <li><a href="Frm_ABM_TipoPago.aspx">Tipo de Pago</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
        </div>
        <div class="DivDistribuidor">
            <nav>
                <ul>
                    <li class="BtnDistribuidor">
                        <ul class="SubMenu">
                            <li><a href="Frm_ABM_Proveedores.aspx">Proveedores</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>

