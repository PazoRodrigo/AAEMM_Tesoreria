Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases.Entidad
Imports Clases.DTO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsMensaje
    Inherits System.Web.Services.WebService


    <WebMethod()>
    Public Function TraerTodosXUsuario(IdUsuario As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim Result As New List(Of DTO_Mensaje)
            Dim ListaEntidades As List(Of Mensaje) = Mensaje.TraerTodosXUsuario(IdUsuario)
            If ListaEntidades IsNot Nothing AndAlso ListaEntidades.Count > 0 Then
                For Each item As Mensaje In ListaEntidades
                    Result.Add(item.ToDTO)
                Next
            End If
            ws.data = Result
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
    Public Function AgregarMensaje(entidad As Mensaje) As Transfer
        Dim ws As New Transfer
        Try
            entidad.AgregarMensaje()
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
    Public Function Modifica(entidad As Mensaje) As Transfer
        Dim ws As New Transfer
        Try
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
    Public Function Baja(entidad As Mensaje) As Transfer
        Dim ws As New Transfer
        Try
            entidad.Baja()
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
    Public Function Ocultar(entidad As Mensaje) As Transfer
        Dim ws As New Transfer
        Try
            entidad.Ocultar()
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
End Class