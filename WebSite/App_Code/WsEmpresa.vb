Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WsEmpresa
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Entidad.Empresa.Refresh()
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
                    result.Add(item.ToDTO)
                Next
            End If
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXCUIT(CUIT As Long) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Entidad.Empresa.Refresh()
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCUIT(CUIT)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
                    result.Add(item.ToDTO)
                Next
            End If
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXIdCentroCosto(IdCentroCosto As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Entidad.Empresa.Refresh()
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCentroCosto(IdCentroCosto)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
                    result.Add(item.ToDTO)
                Next
            End If
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXRazonSocial(RazonSocial As String) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Entidad.Empresa.Refresh()
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXRazonSocial(RazonSocial)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
                    result.Add(item.ToDTO)
                Next
            End If
            ws.data = result
            ws.todoOk = True
            ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
End Class