Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsEmpleado
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodosXBusqueda(Busqueda As Entidad.Empleado.StrBusquedaEmpleado) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empleado)
            Dim lista As List(Of Entidad.Empleado) = Entidad.Empleado.TraerTodosXBusqueda(Busqueda)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empleado In lista
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
    Public Function TraerTodosXCUIL(CUIL As Long) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empleado)
            Dim lista As List(Of Entidad.Empleado) = Entidad.Empleado.TraerTodosXCUIL(CUIL)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empleado In lista
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
    Public Function TraerTodosXNroDocumento(NroDocumento As Long) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empleado)
            Dim lista As List(Of Entidad.Empleado) = Entidad.Empleado.TraerTodosXNroDocumento(NroDocumento)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empleado In lista
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
    Public Function TraerTodosXNombre(Nombre As String) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empleado)
            Dim lista As List(Of Entidad.Empleado) = Entidad.Empleado.TraerTodosXNombre(Nombre)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empleado In lista
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