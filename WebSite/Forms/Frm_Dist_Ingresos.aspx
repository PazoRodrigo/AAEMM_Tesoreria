<%@ Page Title="AAEMM. Ingresos" Language="VB"
MasterPageFile="~/Forms/MP.master" AutoEventWireup="false"
CodeFile="Frm_Dist_Ingresos.aspx.vb" Inherits="Forms_Frm_Dist_Ingresos" %>

<asp:Content
  ID="Content1"
  ContentPlaceHolderID="ContentPlaceHolder1"
  runat="Server"
>
  <script
    src='<%= ResolveClientUrl("Frm_Dist_Ingresos.js?version20210321")%>'
  ></script>
  <script>
    document.onkeyup = function (e) {
      let redirect = "";
      if (e.which == 27) {
        redirect = '<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>';
      } else if (e.ctrlKey && e.altKey && (e.which == 73 || e.which == 105)) {
        // Ctrol + Alt + I
        redirect =
          '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Ingresos.aspx")%>';
      } else if (e.ctrlKey && e.altKey && (e.which == 71 || e.which == 103)) {
        // Ctrol + Alt + G
        redirect =
          '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Gastos.aspx")%>';
      } else if (e.ctrlKey && e.altKey && (e.which == 67 || e.which == 99)) {
        // Ctrol + Alt + C
        redirect =
          '<%= ResolveClientUrl("~/Forms/Reportes/Frm_Rep_Comprobantes.aspx")%>';
      }
      window.location = redirect;
    };
  </script>
  <ul>
    <li>
      <div id="BtnIndicadores" class="Cabecera Porc10_L">
        <a
          id="LinkBtnInidicadores"
          href='<%= ResolveClientUrl("~/Forms/Frm_Indicadores.aspx")%>'
          class="LinkBtn"
          title="Indicadores"
          ><span class="icon-stats-dots"></span
        ></a>
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
                    <li>
                      <a
                        href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_Ingreso.aspx")%>'
                        >(Alt + I) - Ingresos</a
                      >
                    </li>
                    <li>
                      <a
                        href='<%= ResolveClientUrl("~/Forms/ADM/Frm_Adm_IngresoManual.aspx")%>'
                        >(Alt + M) - Ingresos Manuales</a
                      >
                    </li>
                  </ul>
                </li>
              </ul>
            </nav>
          </div>
          <div class="col-lg-4"></div>
          <div class="col-lg-4"></div>
        </div>
      </div>
    </li>
  </ul>
</asp:Content>
