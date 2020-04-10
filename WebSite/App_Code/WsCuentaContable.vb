Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsCuentaContable
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_CuentaContable)
            Entidad.CuentaContable.Refresh()
            Dim lista As List(Of Entidad.CuentaContable) = Entidad.CuentaContable.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.CuentaContable In lista
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
    Public Function Alta(entidad As Entidad.CuentaContable) As Transfer
        Dim ws As New Transfer
        Try
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
    Public Function Modifica(entidad As Entidad.CuentaContable) As Transfer
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
    Public Function Baja(entidad As Entidad.CuentaContable) As Transfer
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
End Class