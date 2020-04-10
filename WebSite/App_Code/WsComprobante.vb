Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsComprobante
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodosXGasto(IdGasto As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Comprobante)
            Dim lista As List(Of Entidad.Comprobante) = Entidad.Comprobante.TraerTodosXGasto(IdGasto)
            If Not lista Is Nothing Then
                For Each item As Entidad.Comprobante In lista
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
    Public Function Alta(entidad As DTO.DTO_Comprobante) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Comprobante(entidad)
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
    Public Function Modifica(entidad As DTO.DTO_Comprobante) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Comprobante(entidad)
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
    Public Function Baja(entidad As DTO.DTO_Comprobante) As Transfer
        Dim ws As New Transfer
        Try
            Dim objGuardar As New Entidad.Comprobante(entidad)
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
End Class