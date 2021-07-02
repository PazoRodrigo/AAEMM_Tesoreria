Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsRecibo
    Inherits System.Web.Services.WebService

    ' Recibo
    <WebMethod()>
    Public Function Alta(entidad As DTO.DTO_Recibo) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Recibo(entidad)
            objGuardar.Alta()
            ws.data = objGuardar.IdEntidad
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
    Public Function Baja(entidad As DTO.DTO_Recibo) As Transfer
        Dim ws As New Transfer
        Try
            Dim obj As New Entidad.Recibo(entidad)
            obj.Baja()
            ws.data = obj.IdEntidad
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
    Public Function TraerTodosXBusqueda(Busqueda As Entidad.Recibo.StrBusquedaRecibo) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Recibo)
            Dim lista As List(Of Entidad.Recibo) = Entidad.Recibo.TraerTodosXBusqueda(Busqueda)
            If Not lista Is Nothing Then
                For Each item As Entidad.Recibo In lista
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
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Recibo)
            Dim lista As List(Of Entidad.Recibo) = Entidad.Recibo.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Recibo In lista
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
    ' Pagos
    <WebMethod()>
    Public Function AltaPago(entidad As Entidad.Recibo.StrPago) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Recibo
            objGuardar.AltaPago(entidad)
            ws.data = objGuardar.IdEntidad
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
    Public Function TraerTodosPagosXRecibo(IdRecibo As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As List(Of Entidad.Recibo.StrPago) = Entidad.Recibo.TraerTodosPagosXRecibo(IdRecibo)
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
    ' Periodo
    <WebMethod()>
    Public Function AltaPeriodo(entidad As Entidad.Recibo.StrPeriodo) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Recibo
            objGuardar.AltaPeriodo(entidad)
            ws.data = objGuardar.IdEntidad
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
    Public Function TraerTodosPeriodosXRecibo(IdRecibo As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As List(Of Entidad.Recibo.StrPeriodo) = Entidad.Recibo.TraerTodosPeriodosXRecibo(IdRecibo)
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