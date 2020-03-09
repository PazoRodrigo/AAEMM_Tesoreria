<%@ Page Title="AAEMM. Indicadores" Language="VB" MasterPageFile="~/Forms/MP.master" AutoEventWireup="false" CodeFile="Frm_Indicadores.aspx.vb" Inherits="Forms_Frm_Indicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= ResolveClientUrl("~/Scripts/Forms/Frm_Indicadores.js")%>'></script>
    <%--<script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>--%>
    <%--<script type="text/javascript">
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
    </script>--%>
    <ul>
        <li>
            <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
        </li>
        <li class="linea">
            <div class="container-fluid">
                <div class="row mt-1">
                    <div class="col-lg-10">
                        <div class="row">
                            <div class="col-lg-4">
                                <nav>
                                    <ul>
                                        <li class="MenuIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Empresas</div>
                                                <div class="LblValorIndicador" id="LblEmpresas"></div>
                                            </a>
                                            <ul class="SubMenuInidicador">
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasSinDeudaSinBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasSinDeudaConBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasDeuda1Mes"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasDeuda3Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasDeuda6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasDeudaMayor6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorWarning  ">
                                                    <a href="#">
                                                        <div class="LblIndicador">Pagos Intercalados</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasPagosIntercalados"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorDanger">
                                                    <a href="#">
                                                        <div class="LblIndicador">Inactivas</div>
                                                        <div class="LblValorIndicador" id="LblEmpresasInactivas"></div>
                                                    </a>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                            <div class="col-lg-4">
                                <nav>
                                    <ul>
                                        <li class="MenuIndicador">
                                            <a href="#">
                                                <div class="LblIndicador">Empleados</div>
                                                <div class="LblValorIndicador" id="LblEmpleados"></div>
                                            </a>
                                            <ul class="SubMenuInidicador">
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda s/Boleta</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosSinDeudaSinBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Sin Deuda c/Boleta</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosSinDeudaConBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosDeuda1Mes"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosDeuda3Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosDeuda6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosDeudaMayor6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorDanger">
                                                    <a href="#">
                                                        <div class="LblIndicador">Inactivos</div>
                                                        <div class="LblValorIndicador" id="LblEmpleadosInactivos"></div>
                                                    </a>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                            <div class="col-lg-4">
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
                                                        <div class="LblValorIndicador" id="LblRecaudacionXCobrarSinBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Por Cobrar s/Boleta</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionXCobrarConBoleta"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 1 Mes</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionDeuda1Mes"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 3 Meses</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionDeuda3Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionDeuda6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicador">
                                                    <a href="#">
                                                        <div class="LblIndicador">Deuda > 6 Meses</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionDeudaMayor6Meses"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorDanger">
                                                    <a href="#">
                                                        <div class="LblIndicador">Inactivos</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionInactivos"></div>
                                                    </a>
                                                </li>
                                                <li class="BtnIndicadorWarning">
                                                    <a href="#">
                                                        <div class="LblIndicador">Fuera Término</div>
                                                        <div class="LblValorIndicador" id="LblRecaudacionFueraTermino"></div>
                                                    </a>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="row">
                            <div class="col-lg-12">
                                <nav>
                                    <ul id="UlDistribuidorIndicadores">
                                        <li class="BtnDistribuidorIndicadores"><a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>'>Configuración</a></li>
                                        <li class="BtnDistribuidorIndicadores"><a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>'>Administración</a></li>
                                        <li class="BtnDistribuidorIndicadores"><a href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>'>Reportes</a></li>
                                        <li class="BtnDistribuidorIndicadores"><a href="#">Ingresos</a></li>
                                        <li class="BtnDistribuidorIndicadores"><a href="#">Gastos</a></li>
                                        <li class="BtnDistribuidorIndicadoresDanger">
                                            <a href="#">
                                                <div>Ch. Rechazados <span id="LblChequesRechazados" style="float:right; padding-right:10px;"></span> </div>
                                            </a>
                                        </li>
                                    </ul>
                                </nav>
                                <div id="Inferior" style="width: 100%; background-color: transparent; margin-top: 50px; height: 140px; border: 1px solid blue; border-radius: 8px;">
                                    <div style="width: 90%; margin-left: auto; margin-right: auto; text-align: center; padding-top: 10px;">
                                        <span id="SpanIngresoArchivos" class="AAEMM" style="font-size: 15px; color: #fff;">Ingreso de Archivos</span>
                                        <input type="text" style="width: 90%; height: 25px; margin-top: 15px; text-align: left; padding-left: 8px; margin-bottom: 10px;" placeholder="Archivo..." readonly="readonly" />
                                        <div style="width: 95%; margin-right: auto; margin-left: auto;">
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
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>

</asp:Content>

