Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsChequePropio
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_ChequePropio)
            Entidad.ChequePropio.Refresh()
            Dim lista As List(Of Entidad.ChequePropio) = Entidad.ChequePropio.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.ChequePropio In lista
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
    Public Function AltaChequera(Desde As Long, Hasta As Long) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_ChequePropio)
            Dim IdUsuarioAlta As Integer = 1
            Dim lista As List(Of Entidad.ChequePropio) = Entidad.ChequePropio.AltaChequera(IdUsuarioAlta, Desde, Hasta)
            If Not lista Is Nothing Then
                For Each item As Entidad.ChequePropio In lista
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
    Public Function Modifica(entidad As Entidad.ChequePropio) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioModifica = 1
            entidad.Modifica()
            ws.data = entidad.IdEntidad
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
    Public Function TraerChequeProximo() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_ChequePropio)
            result.Add(Entidad.ChequePropio.TraerChequeProximo().ToDTO)
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