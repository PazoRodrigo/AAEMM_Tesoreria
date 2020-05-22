Option Explicit On
Option Strict On

Imports System.IO

Namespace Entidad
    Public Class LineaArchivoBN
        Inherits LineaArchivo

#Region " Atributos / Propiedades "
        Public Property FechaPago() As Date? = Nothing
        Public Property FechaAcreditacion() As Date? = Nothing
        Public Property IdCentroCosto() As Integer = 0
        Public Property CUIT() As Long = 0
        Public Property Periodo() As Integer = 0
        Public Property NroCheche() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property IdEstado() As Char = CChar("A")
#End Region
        Public Overrides Function ArmarListaLineas(idUsuario As Integer, nombreArchivo As String, archivo As StreamReader) As List(Of LineaArchivo)
            Try
                Dim ListaResult As New List(Of LineaArchivo)
                Dim entidad As LineaArchivoBN
                Dim varTempo As String = ""
                Dim primeraLinea As Integer = 10
                Dim nroLinea As Integer = 0
                Dim linea As String
                While Not archivo.EndOfStream
                    nroLinea += 1
                    entidad = New LineaArchivoBN
                    entidad.IdUsuarioAlta = idUsuario
                    entidad.IdTipoArchivo = Enumeradores.TipoArchivo.BN
                    entidad.NombreArchivo = nombreArchivo
                    linea = archivo.ReadLine
                    Dim linea2 As String = LTrim(linea)
                    'Public Property FechaPago() As Date? = Nothing
                    entidad.FechaPago = CDate(linea2.Substring(24, 2) & "/" & linea2.Substring(22, 2) & "/" & linea2.Substring(18, 4))
                    'Public Property FechaAcreditacion() As Date? = Nothing
                    entidad.FechaAcreditacion = CDate(linea2.Substring(32, 2) & "/" & linea2.Substring(30, 2) & "/" & linea2.Substring(26, 4))
                    'Public Property IdCentroCosto() As Integer = 0
                    entidad.IdCentroCosto = CInt(linea2.Substring(62, 2))
                    'Public Property CUIT() As Long = 0
                    entidad.CUIT = CLng(linea2.Substring(69, 11))
                    'Public Property Periodo() As Integer = 0
                    entidad.Periodo = CInt(linea2.Substring(80, 6))
                    'Public Property NroCheche() As Long = 0
                    entidad.NroCheche = CLng(linea2.Substring(139, 15))
                    If CLng(entidad.NroCheche) > 0 Then
                        ' Es cheque
                        If linea2.Length >= 155 Then
                            If CChar(linea2.Substring(154, 1)) <> " " Then
                                entidad.IdEstado = CChar(linea2.Substring(154, 1))
                            End If
                        End If
                    End If
                    'Public Property Importe() As Decimal = 0
                    entidad.Importe = CDec(linea2.Substring(43, 14).Insert(12, ","))
                    'Public Property IdEstado() As Char = CChar("A")
                    If nroLinea = 1 Then
                        ValidacionesPrimeraLinea(entidad)
                    End If
                    ListaResult.Add(entidad)
                End While
                Return ListaResult
            Catch ex As Exception
                Throw ex
            Finally
                'Cierra el archivo
                archivo.Close()
            End Try
        End Function
        Protected Overrides Sub ValidacionesPrimeraLinea(Linea As LineaArchivo)
            Dim objEntidad As LineaArchivoBN = CType(Linea, LineaArchivoBN)
            ' Nombre archivo Correcto
            Dim strFecha As String = "BN" + Year(CDate(objEntidad.FechaAcreditacion)).ToString + Right("00" + Month(CDate(objEntidad.FechaAcreditacion)).ToString, 2) + Right("00" + Day(CDate(objEntidad.FechaAcreditacion)).ToString, 2)
            If objEntidad.NombreArchivo <> strFecha Then
                Throw New Exception("El nombre del Archivo no se corresponde con su composición.")
            End If
            ' No Duplicados
            Dim Lista As List(Of Ingreso) = Ingreso.TraerTodosXFechasXAcreditacion(CDate(objEntidad.FechaAcreditacion), CDate(objEntidad.FechaAcreditacion), Enumeradores.TipoArchivo.BN)
            If Lista.Count > 0 Then
                Throw New Exception("Archivo ingresado anteriormente.")
            End If
        End Sub
        Public Overrides Sub IngresarListaLineas(ListaLineas As List(Of LineaArchivo))
            If ListaLineas.Count > 0 Then
                For Each item As LineaArchivoBN In ListaLineas
                    Dim objIngreso As New Ingreso(item)
                    objIngreso.Alta()
                Next
            End If
        End Sub
    End Class ' LineaArchivoBN
End Namespace ' Entidad
