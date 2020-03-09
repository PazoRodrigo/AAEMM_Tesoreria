Imports Clases
Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim lista As Entidad.Empresa = Entidad.Empresa.TraerUno(5)
            'Dim lista0 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodos
            Dim lista1 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCUIT(0)
            Dim lista2 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXRazonSocial("NAVEGAC")
            Dim lista3 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCentroCosto(2)

            'Const storeTraerTodosXCUIT As String = "ADM.p_Empresa_TraerTodosXCUIT"
            'Const storeTraerTodosXRazonSocial As String = "ADM.p_Empresa_TraerTodosXRazonSocial"
            'Const storeTraerTodosXCentroCosto As String = "ADM.p_Empresa_TraerTodosXCentroCosto"
        End If
    End Sub
End Class
