<%@ Page Title="AAEMM. Indicadores" Language="VB"
MasterPageFile="~/Forms/MP.master" AutoEventWireup="false"
CodeFile="Frm_Indicadores.aspx.vb" Inherits="Forms_Frm_Indicadores" %> <%@
Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
TagPrefix="cc1" %>
<asp:Content
  ID="Content1"
  ContentPlaceHolderID="ContentPlaceHolder1"
  runat="Server"
>
  <script
    src='<%= ResolveClientUrl("Frm_Indicadores.js?version20210209")%>'
  ></script>
  <asp:Button
    ID="btnSubirOculto"
    runat="server"
    Style="visibility: hidden; display: none;"
  />
  <ul>
    <li>
      <div id="DivNombreFormulario100"><span id="NombreFormulario"></span></div>
    </li>
    <li class="linea">
      <div class="container-fluid">
        <div class="row mt-1" style="height: 290px">
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
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasSinDeudaSinBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Sin Deuda c/Boleta</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasSinDeudaConBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 1 Mes</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasDeuda1Mes"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 3 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasDeuda3Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasDeuda6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda > 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasDeudaMayor6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicadorWarning">
                          <a href="#">
                            <div class="LblIndicador">Pagos Intercalados</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasPagosIntercalados"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicadorDanger">
                          <a href="#">
                            <div class="LblIndicador">Inactivas</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpresasInactivas"
                            ></div>
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
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosSinDeudaSinBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Sin Deuda c/Boleta</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosSinDeudaConBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 1 Mes</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosDeuda1Mes"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 3 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosDeuda3Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosDeuda6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda > 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosDeudaMayor6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicadorDanger">
                          <a href="#">
                            <div class="LblIndicador">Inactivos</div>
                            <div
                              class="LblValorIndicador"
                              id="LblEmpleadosInactivos"
                            ></div>
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
                        <div class="LblIndicador">Recaudación Neta</div>
                        <div
                          class="LblValorIndicador"
                          id="LblRecaudacionNeta"
                        ></div>
                      </a>
                      <ul class="SubMenuInidicador">
                        <%--
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Recaudación Bruta</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionBruta"
                            ></div>
                          </a>
                        </li>
                        --%> <%--
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Por Cobrar c/Boleta</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionXCobrarSinBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Por Cobrar s/Boleta</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionXCobrarConBoleta"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 1 Mes</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionDeuda1Mes"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 3 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionDeuda3Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionDeuda6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicador">
                          <a href="#">
                            <div class="LblIndicador">Deuda > 6 Meses</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionDeudaMayor6Meses"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicadorDanger">
                          <a href="#">
                            <div class="LblIndicador">Inactivos</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionInactivos"
                            ></div>
                          </a>
                        </li>
                        <li class="BtnIndicadorWarning">
                          <a href="#">
                            <div class="LblIndicador">Fuera Término</div>
                            <div
                              class="LblValorIndicador"
                              id="LblRecaudacionFueraTermino"
                            ></div>
                          </a>
                        </li>
                        --%>
                      </ul>
                    </li>
                    <li class="MenuIndicador" style="margin-top: 2px">
                      <a href="#">
                        <div class="LblIndicador">Recaudación Bruta</div>
                        <div
                          class="LblValorIndicador"
                          id="LblRecaudacionBruta"
                        ></div>
                      </a>
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
                    <li class="BtnDistribuidorIndicadores">
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Configuracion.aspx")%>'
                        >Configuración</a
                      >
                    </li>
                    <li class="BtnDistribuidorIndicadores">
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Administracion.aspx")%>'
                        >Administración</a
                      >
                    </li>
                    <li class="BtnDistribuidorIndicadores">
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Reportes.aspx")%>'
                        >Reportes</a
                      >
                    </li>
                    <li class="BtnDistribuidorIndicadores">
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Ingresos.aspx")%>'
                        >Ingresos</a
                      >
                    </li>
                    <li class="BtnDistribuidorIndicadores">
                      <a
                        href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Gasto.aspx")%>'
                        >Gastos</a
                      >
                    </li>
                    <li
                      class="BtnDistribuidorIndicadores"
                      style="display: none"
                    >
                      <a
                        href='<%= ResolveClientUrl("~/Forms/Frm_Dist_Archivos.aspx")%>'
                        >Archivos</a
                      >
                    </li>
                    <li class="BtnDistribuidorIndicadoresDanger">
                      <a href="#">
                        <div>
                          Ch. Rechazados
                          <span
                            id="LblChequesRechazados"
                            style="float: right; padding-right: 10px"
                          ></span>
                        </div>
                      </a>
                    </li>
                  </ul>
                </nav>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-lg-8"></div>
          <div class="col-lg-4">
            <div class="row">
              <div class="col-lg-12">
                <div id="PnlResultado" style="display: none"></div>
                <div id="PnlArchivos">
                  <div
                    style="
                      width: 90%;
                      margin-left: auto;
                      margin-right: auto;
                      text-align: center;
                      padding-top: 10px;
                    "
                  >
                    <span
                      id="SpanIngresoArchivos"
                      class="AAEMM"
                      style="font-size: 18px; color: #fff"
                      >Ingreso de Archivos</span
                    >
                    <div
                      style="width: 95%; margin-right: auto; margin-left: auto"
                    >
                      <asp:FileUpload
                        ID="FileUpload1"
                        CssClass="form-control"
                        runat="server"
                      />
                      <br />
                      <asp:Button
                        ID="Upload"
                        CssClass="btn btn-primary"
                        runat="server"
                        Text="Subir"
                        UseSubmitBehavior="False"
                      />
                      <br />
                      <asp:Label
                        ID="LblOK"
                        runat="server"
                        CssClass="bg-light text-success "
                      ></asp:Label>
                      <asp:Label
                        ID="LblError"
                        runat="server"
                        CssClass="bg-light text-danger "
                      ></asp:Label>
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
