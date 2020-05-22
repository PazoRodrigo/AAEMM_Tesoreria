Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsIngreso
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerUno(IdEntidad As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso) From {
                Entidad.Ingreso.TraerUno(IdEntidad).ToDTO
            }
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
    Public Function TraerTodosXCentroCosto(IdCentroCosto As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXCentroCosto(IdCentroCosto)
            If Not lista Is Nothing Then
                For Each item As Entidad.Ingreso In lista
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
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXCUIT(CUIT)
            If Not lista Is Nothing Then
                For Each item As Entidad.Ingreso In lista
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
    Public Function TraerTodosXFechasXAcreditacion(Desde As Date, Hasta As Date) As Transfer
        Dim ws As New Transfer
        Try
            'Dim result As New List(Of DTO.DTO_Ingreso)
            'Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXFechasXAcreditacion(Desde, Hasta)
            'If Not lista Is Nothing Then
            '    For Each item As Entidad.Ingreso In lista
            '        result.Add(item.ToDTO)
            '    Next
            'End If
            'ws.data = result
            'ws.todoOk = True
            'ws.mensaje = ""
        Catch ex As Exception
            ws.todoOk = False
            ws.mensaje = ex.Message
            ws.data = Nothing
        End Try
        Return ws
    End Function
    <WebMethod()>
    Public Function TraerTodosXFechasXPago(Desde As Date, Hasta As Date) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXFechasXPago(Desde, Hasta)
            If Not lista Is Nothing Then
                For Each item As Entidad.Ingreso In lista
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
    Public Function TraerTodosXPeriodo(Periodo As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXPeriodo(Periodo)
            If Not lista Is Nothing Then
                For Each item As Entidad.Ingreso In lista
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