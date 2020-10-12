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
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodos()
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
    Public Function TraerTodosXBusqueda(Busqueda As Entidad.Ingreso.StrBusquedaIngreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Ingreso)
            Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXBusqueda(Busqueda)
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
    Public Function Baja(entidad As DTO.DTO_Ingreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Ingreso(entidad)
            objGuardar.Baja()
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
    Public Function Modifica(entidad As DTO.DTO_Ingreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Ingreso(entidad)
            objGuardar.Modifica()
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
    Public Function ExplotarIngreso(IdUsuario As Integer, IdIngreso As Integer, ListaIngresos As List(Of DTO.DTO_Ingreso)) As Transfer
        Dim ws As New Transfer
        Try
            Entidad.Ingreso.ExplotarIngreso(IdUsuario, IdIngreso, ListaIngresos)
            ws.data = IdIngreso
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
    Public Function GuardarIngresoExplotado(entidad As DTO.DTO_Ingreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Ingreso(entidad)
            objGuardar.GuardarIngresoExplotado()
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
    Public Function AltaExplosion(entidad As DTO.DTO_Ingreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Ingreso(entidad)
            objGuardar.AltaExplosion()
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
    Public Function EliminarExplotado(entidad As DTO.DTO_Ingreso) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Ingreso(entidad)
            objGuardar.EliminarExplotado()
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
End Class