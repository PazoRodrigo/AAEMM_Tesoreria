Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsEstadoCheque
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos_ChequesPropios() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_EstadoCheque)
            Entidad.CentroCosto.Refresh()
            Dim lista As List(Of Entidad.EstadoCheque) = Entidad.EstadoCheque.TraerTodos_ChequesPropios()
            If Not lista Is Nothing Then
                For Each item As Entidad.EstadoCheque In lista
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
    Public Function TraerTodos_ChequesTerceros() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_EstadoCheque)
            Entidad.CentroCosto.Refresh()
            Dim lista As List(Of Entidad.EstadoCheque) = Entidad.EstadoCheque.TraerTodos_ChequesTerceros()
            If Not lista Is Nothing Then
                For Each item As Entidad.EstadoCheque In lista
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