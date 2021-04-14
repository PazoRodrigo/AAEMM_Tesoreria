Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Clases

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WsEmpresa
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function TraerTodos() As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodos()
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
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
    Public Function TraerTodosXBusqueda(Busqueda As Entidad.Empresa.StrBusquedaEmpresa) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXBusqueda(Busqueda)
            If Not lista Is Nothing Then
                For Each item As Entidad.Empresa In lista
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
    '<WebMethod()>
    'Public Function TraerTodosConDeudaImpresion(intDeuda As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        ws.data = Entidad.Empresa.TraerTodosDeuda_DTO(intDeuda)
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    <WebMethod()>
    Public Function TraerTodosSinDeuda() As Transfer
        Dim ws As New Transfer
        Try
            ws.data = Entidad.Empresa.TraerTodosDeuda_Impresion(0)
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
    Public Function TraerTodosDeuda1() As Transfer
        Dim ws As New Transfer
        Try
            ws.data = Entidad.Empresa.TraerTodosDeuda_Impresion(1)
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
    Public Function TraerTodosDeuda3() As Transfer
        Dim ws As New Transfer
        Try
            ws.data = Entidad.Empresa.TraerTodosDeuda_Impresion(3)
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
    Public Function TraerTodosDeuda6() As Transfer
        Dim ws As New Transfer
        Try
            ws.data = Entidad.Empresa.TraerTodosDeuda_Impresion(6)
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
    Public Function TraerUnaXCUIT(CUIT As Long) As Transfer
        Dim ws As New Transfer
        Try
            Dim result As New List(Of DTO.DTO_Empresa)
            result.Add(Entidad.Empresa.TraerUnaXCUIT(CUIT).ToDTO)
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
    Public Function Alta(entidad As Entidad.Empresa) As Transfer
        Dim ws As New Transfer
        Try
            entidad.Alta()
            ws.data = entidad
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
    Public Function Modifica(entidad As DTO.DTO_Empresa) As Transfer
        Dim ws As New Transfer
        Try
            Dim objEntidad As New Entidad.Empresa(entidad)
            objEntidad.Modifica()
            ws.data = objEntidad
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
    Public Function Baja(entidad As Entidad.Empresa) As Transfer
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
    '<WebMethod()>
    'Public Function TraerUno(IdEntidad As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_Empresa)
    '        result.Add(Entidad.Empresa.TraerUno(IdEntidad).ToDTO)
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerTodosXBusqueda(Busqueda As Entidad.Empresa.StrBusquedaEmpresa) As Transfer
    '    Dim ws As New Transfer
    '    'Try
    '    '    Dim result As New List(Of DTO.DTO_Ingreso)
    '    '    Dim lista As List(Of Entidad.Ingreso) = Entidad.Ingreso.TraerTodosXBusqueda(Busqueda)
    '    '    If Not lista Is Nothing Then
    '    '        For Each item As Entidad.Ingreso In lista
    '    '            result.Add(item.ToDTO)
    '    '        Next
    '    '    End If
    '    '    ws.data = result
    '    '    ws.todoOk = True
    '    '    ws.mensaje = ""
    '    'Catch ex As Exception
    '    '    ws.todoOk = False
    '    '    ws.mensaje = ex.Message
    '    '    ws.data = Nothing
    '    'End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerTodosXCUIT(CUIT As Long) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_Empresa)
    '        Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCUIT(CUIT)
    '        If Not lista Is Nothing Then
    '            For Each item As Entidad.Empresa In lista
    '                result.Add(item.ToDTOCabecera)
    '            Next
    '        End If
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerTodosXCentroCosto(IdCentroCosto As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_Empresa)
    '        Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCentroCosto(IdCentroCosto)
    '        If Not lista Is Nothing Then
    '            For Each item As Entidad.Empresa In lista
    '                result.Add(item.ToDTOCabecera)
    '            Next
    '        End If
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerTodosXRazonSocial(RazonSocial As String) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_Empresa)
    '        Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXRazonSocial(RazonSocial)
    '        If Not lista Is Nothing Then
    '            For Each item As Entidad.Empresa In lista
    '                result.Add(item.ToDTOCabecera)
    '            Next
    '        End If
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerTodosXBusqueda(RazonSocial As String, CUIT As Long, IdCentroCosto As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_Empresa)
    '        Dim lista As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXBusqueda(RazonSocial, CUIT, IdCentroCosto)
    '        If Not lista Is Nothing Then
    '            For Each item As Entidad.Empresa In lista
    '                result.Add(item.ToDTOCabecera)
    '            Next
    '        End If
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
    '<WebMethod()>
    'Public Function TraerDatosCalculados(CUIT As Long, IdEstablecimiento As Integer) As Transfer
    '    Dim ws As New Transfer
    '    Try
    '        Dim result As New List(Of DTO.DTO_DatosCalculados)
    '        result.Add(Entidad.DatosCalculados.TraerUno(CUIT, IdEstablecimiento).ToDTO)
    '        ws.data = result
    '        ws.todoOk = True
    '        ws.mensaje = ""
    '    Catch ex As Exception
    '        ws.todoOk = False
    '        ws.mensaje = ex.Message
    '        ws.data = Nothing
    '    End Try
    '    Return ws
    'End Function
End Class