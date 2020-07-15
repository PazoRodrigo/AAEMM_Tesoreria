Imports Clases.Entidad

Partial Class Forms_Adm_Frm_Adm_Empresa
    Inherits System.Web.UI.Page

    Private Sub Forms_Adm_Frm_Adm_Empresa_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Empresa.Refresh()
        End If
    End Sub
End Class
