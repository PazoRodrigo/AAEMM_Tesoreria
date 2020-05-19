
Partial Class Frm_Rep_Ingresos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim parm As Microsoft.Reporting.WebForms.ReportParameter() = New Microsoft.Reporting.WebForms.ReportParameter() {}
            abrirReporte("/AAEMM2020/Reporte1", parm)
        End If
    End Sub
    Private Sub abrirReporte(ByVal url As String, ByVal parametros As Microsoft.Reporting.WebForms.ReportParameter())
        ReportViewer1.ShowCredentialPrompts = False
        ReportViewer1.ServerReport.ReportServerCredentials = New ReportCredentials("administrator", "1planVMW", "")
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerUrl = New System.Uri(ConfigurationManager.AppSettings("urlreportserver"))
        ReportViewer1.ServerReport.ReportPath = url
        'ReportViewer1.ServerReport.SetParameters(parametros)
        ReportViewer1.ServerReport.Refresh()
    End Sub
End Class
