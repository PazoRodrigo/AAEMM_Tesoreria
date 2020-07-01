
Partial Class Forms_Reportes_Frm_Rep_Gasto
    Inherits System.Web.UI.Page

    Private Sub BtnOcultoImprimirGastos_Click(sender As Object, e As EventArgs) Handles BtnOcultoImprimirGastos.Click
        Dim parm As Microsoft.Reporting.WebForms.ReportParameter() = New Microsoft.Reporting.WebForms.ReportParameter(0) {}
        parm(0) = New Microsoft.Reporting.WebForms.ReportParameter("sqlQuery", hfBusqueda.Value.ToString, False)
        AbrirReporte("/AAEMM2020/Rpt_GastosFiltros", parm)
    End Sub
    Private Sub AbrirReporte(ByVal url As String, ByVal parametros As Microsoft.Reporting.WebForms.ReportParameter())
        ReportViewer1.ShowCredentialPrompts = False
        ReportViewer1.SizeToReportContent = True
        ReportViewer1.ShowBackButton = True
        ReportViewer1.HyperlinkTarget = "_self"
        ReportViewer1.AsyncRendering = True
        ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage
        ReportViewer1.ServerReport.ReportServerCredentials = New ReportCredentials("administrator", "1planVMW", "")
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New System.Uri(ConfigurationManager.AppSettings("urlreportserver"))
        ReportViewer1.ServerReport.ReportPath = url
        ReportViewer1.ServerReport.SetParameters(parametros)
        ReportViewer1.ServerReport.Refresh()
    End Sub
    Public Sub reportViewer1_Back(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.BackEventArgs)
        ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
    End Sub
    Protected Sub ReportViewer1_Drillthrough(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.DrillthroughEventArgs) Handles ReportViewer1.Drillthrough

    End Sub

End Class
