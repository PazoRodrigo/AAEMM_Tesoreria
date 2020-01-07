<%@ Page Title="AAEMM. Indicadores" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Indicadores.aspx.vb" Inherits="Forms_Frm_Indicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Indicadores.js")%>'></script>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var chart = new CanvasJS.Chart("chartContainer", {
                title: {
                    text: "My First Chart in CanvasJS"
                },
                data: [
                    {
                        type: "column",
                        dataPoints: [
                            { label: "apple", y: 10 },
                            { label: "orange", y: 15 },
                            { label: "banana", y: 25 },
                            { label: "mango", y: 30 },
                            { label: "grape", y: 28 }
                        ]
                    }
                ]
            });
            chart.render();
        }
    </script>
    <div id="Contenido">
        <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        <div id="DivContenedor80">
            <div class="DivDistribuidor">
                <nav>
                    <ul>
                        <li class="MenuIndicador">
                            <a href="#">
                                <div class="LblIndicador">Empresas</div>
                                <div class="LblValorIndicador" id="LblCantidadEmpresas"></div>
                            </a>
                            <ul class="SubMenuInidicador">
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr01"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr02"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr03"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr04"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr05"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr06"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicadorWarning  ">
                                    <a href="#">
                                        <div class="LblIndicador">Pagos Intercalados</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr07"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicadorDanger">
                                    <a href="#">
                                        <div class="LblIndicador">Inactivas</div>
                                        <div class="LblValorIndicador" id="LblCtEmpr08"></div>
                                    </a>
                                </li>

                            </ul>
                        </li>
                    </ul>
                </nav>
            </div>
            <div class="DivDistribuidor">
                <nav>
                    <ul>
                        <li class="MenuIndicador">
                            <a href="#">
                                <div class="LblIndicador">Empleados</div>
                                <div class="LblValorIndicador" id="LblCantidadEmpleados"></div>
                            </a>
                            <ul class="SubMenuInidicador">
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl01"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl02"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl03"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl04"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl05"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl06"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicadorDanger">
                                    <a href="#">
                                        <div class="LblIndicador">Inactivos</div>
                                        <div class="LblValorIndicador" id="LblCtEmpl07"></div>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </nav>
            </div>
            <div class="DivDistribuidor">
                <nav>
                    <ul>
                        <li class="MenuIndicador">
                            <a href="#">
                                <div class="LblIndicador">Recaudación</div>
                                <div class="LblValorIndicador" id="LblRecaudacion"></div>
                            </a>
                            <ul class="SubMenuInidicador">
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Por Cobrar c/Boleta</div>
                                        <div class="LblValorIndicador" id="LblRec01"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Por Cobrar s/Boleta</div>
                                        <div class="LblValorIndicador" id="LblRec02"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                        <div class="LblValorIndicador" id="LblRec03"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                        <div class="LblValorIndicador" id="LblRec04"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicador">
                                    <a href="#">
                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                        <div class="LblValorIndicador" id="LblRec05"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicadorDanger">
                                    <a href="#">
                                        <div class="LblIndicador">Inactivos</div>
                                        <div class="LblValorIndicador" id="LblRec06"></div>
                                    </a>
                                </li>
                                <li class="BtnIndicadorWarning">
                                    <a href="#">
                                        <div class="LblIndicador">Fuera Término</div>
                                        <div class="LblValorIndicador" id="LblRec07"></div>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </nav>
            </div>
            <%--<div id="chartContainer" style="height: 100px; width: 100%;"></div>--%>
        </div>
        <div id="DivContenedor20">
            <nav>
                <ul id="UlDistribuidorIndicadores">
                    <li class="BtnDistribuidorIndicadores"><a href="Frm_Dist_Configuracion.aspx">Configuración</a></li>
                    <li class="BtnDistribuidorIndicadores"><a href="Frm_Dist_Administracion.aspx">Administración</a></li>
                    <li class="BtnDistribuidorIndicadores"><a href="Frm_Dist_Reportes.aspx">Reportes</a></li>
                    <li class="BtnDistribuidorIndicadores"><a href="#">Ingresos</a></li>
                    <li class="BtnDistribuidorIndicadores"><a href="#">Gastos</a></li>
                    <li class="BtnDistribuidorIndicadoresDanger">
                        <a href="#">Cheques Rechazados</a>
                    </li>
                </ul>
            </nav>
            <div id="Inferior" style="width: 100%; background-color: transparent; margin-top: 50px; height: 140px; border: 1px solid blue; border-radius: 8px;">
                <div style="width: 90%; margin-left: auto; margin-right: auto; text-align: center; padding-top: 10px;">
                    <span id="SpanIngresoArchivos" class="AAEMM" style="font-size: 15px; color: #fff;">Ingreso de Archivos</span>
                    <input type="text" style="width: 90%; height: 25px; margin-top: 15px; text-align: left; padding-left: 8px; margin-bottom: 10px;" placeholder="Archivo..." readonly="true" />
                    <div style="width:95%; margin-right:auto;margin-left:auto;">
                        <a href="#" id="BtnExaminar" title="Indicadores">
                            <div class="Btn">
                                <span title="Examinar....">Examinar ...</span>
                            </div>
                        </a>
                        <a href="#" id="IcVolver" title="Volver">
                            <div id="DivBtn2" class="Btn">
                                <span title="Subir Archivo">Ingresar</span>
                            </div>
                        </a>
                    </div>
                </div>

            </div>
        </div>
        <%--<div style="width: 1260px; height: 125px;">linea</div>--%>
        <%--<div style="clear: both; background-color: orange; position: absolute; top: 460px; width: 1260px; height: 125px;">linea</div>--%>
    </div>
</asp:Content>

