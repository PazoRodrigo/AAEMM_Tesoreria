Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsUsuario
    Inherits System.Web.Services.WebService



    <WebMethod()>
    Public Function Alta(IdUsuario As Integer, entidad As Entidad.Usuario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioAlta = IdUsuario
            entidad.Alta()
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
    Public Function Modifica(IdUsuario As Integer, entidad As Entidad.Usuario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioModifica = IdUsuario
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
    Public Function Baja(IdUsuario As Integer, entidad As Entidad.Usuario) As Transfer
        Dim ws As New Transfer
        Try
            entidad.IdUsuarioBaja = IdUsuario
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
    <WebMethod()>
    Public Function AccederAlSistema(User As String, Pass As String) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Usuario)
            result.Add(Entidad.Usuario.AccederAlSistema(User.Trim, Pass.Trim).ToDTO)
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
    Public Function EnviarPassword(Identificador As String) As Transfer
        Dim ws As New Transfer
        Try
            Entidad.Usuario.EnviarPassword(Identificador)
            ws.data = Nothing
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
    Public Function ModificaPassword(IdUsuario As Integer, anterior As String, nueva As String) As Transfer
        Dim ws As New Transfer
        Try
            Entidad.Usuario.ModificaPassword(IdUsuario, anterior.Trim, nueva.Trim)
            ws.data = Nothing
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
    Public Function TraerTodosXPerfil(IdPerfil As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Usuario)
            Dim lista As List(Of Entidad.Usuario) = Entidad.Usuario.TraerTodosXPerfil(IdPerfil)
            If Not lista Is Nothing Then
                For Each item As Entidad.Usuario In lista
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
            Dim result As New List(Of DTO.DTO_Usuario)
            Dim lista As List(Of Entidad.Usuario) = Entidad.Usuario.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Usuario In lista
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