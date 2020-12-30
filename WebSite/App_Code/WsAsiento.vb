Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsAsiento
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function Alta(entidad As DTO_Asiento.DTO_Asiento) As Transfer
        Dim ws As New Transfer
        Try
            Dim Obj As New Entidad_Asiento.Asiento(entidad)
            Obj.Alta()
            ws.data = Obj.IdEntidad
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
    Public Function TraerTodosXCuenta(IdCuenta As Integer) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO_Asiento.DTO_Asiento)
            Dim lista As List(Of Entidad_Asiento.Asiento) = Entidad_Asiento.Asiento.TraerTodosXCuenta_DTO(IdCuenta)
            If Not lista Is Nothing Then
                For Each item As Entidad_Asiento.Asiento In lista
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