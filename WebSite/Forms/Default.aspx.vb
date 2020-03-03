Imports Clases
Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim indicador As DTO.DTO_Indicadores = Entidad.Indicadores.TraerTodos.ToDTO
        End If
    End Sub
End Class
