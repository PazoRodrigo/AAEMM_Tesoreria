Option Explicit On
Option Strict On

Imports System.IO
Imports Clases

Namespace Entidad
    Public Class LineaArchivoPF
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
                Dim entidad As LineaArchivoPF
                Dim varTempo As String = ""
                Dim primeraLinea As Integer = 10
                Dim nroLinea As Integer = 0
                Dim linea As String
                Dim contador As Integer = 0
                Dim cantTotal As Integer = 0
                Dim fechaArchivo As Date
                Dim fechaProceso As Date
                While Not archivo.EndOfStream
                    linea = archivo.ReadLine
                    If linea.StartsWith("1") Then
                        ' Registro Cabecera
                        ' ValidaArchivo
                        fechaArchivo = New Date(CInt(linea.Substring(1, 4)), CInt(linea.Substring(5, 2)), CInt(linea.Substring(7, 2)))
                        If contador = 0 Then
                            ' Es el primero y valida internamente para que no se repita
                            Dim temp As String = nombreArchivo.Substring(2, 8)
                            Dim fechaNombreArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
                            If fechaArchivo <> fechaNombreArchivo Then
                                Throw New Exception("Valide la fecha del Archivo, parece incorrecta.")
                            Else
                                ' Debe validar que no esté ingresado en base. ya lo hace antes.
                            End If
                        End If
                    ElseIf linea.StartsWith("5") Then
                        nroLinea += 1
                        entidad = New LineaArchivoPF
                        entidad.IdUsuarioAlta = idUsuario
                        entidad.IdTipoArchivo = Enumeradores.TipoArchivo.PF
                        entidad.NombreArchivo = nombreArchivo
                        ' Información detallada de cada transaccion
                        entidad.Importe = CDec(linea.Substring(48, 10).Insert(8, ","))
                        contador += 1
                        ' Fecha de CPF = Fecha de Proceso
                        fechaProceso = New Date(CInt(linea.Substring(8, 4)), CInt(linea.Substring(12, 2)), CInt(linea.Substring(14, 2)))
                        ' Fecha de Pago = Fecha de Acreditación
                        entidad.FechaPago = New Date(CInt(linea.Substring(64, 4)), CInt(linea.Substring(68, 2)), CInt(linea.Substring(70, 2)))
                        entidad.FechaAcreditacion = entidad.FechaPago
                    ElseIf linea.StartsWith("6") Then
                        '' Código de Barras
                        entidad.IdCentroCosto = CInt(linea.Substring(21, 2))
                        entidad.Periodo = CInt(linea.Substring(23, 6))
                        entidad.CUIT = CLng(linea.Substring(12, 11))
                        '    cuit = Configuration.ConfigurationManager.AppSettings("CUIT_UTEDYC")
                    ElseIf linea.StartsWith("7") Then
                        If contadorLinea = 1 Then
                            ValidacionesPrimeraLinea(entidad)
                        End If
                        ListaResult.Add(entidad)
                    ElseIf linea.StartsWith("8") Then
                        cantTotal = CInt(linea.Substring(15, 7))
                    End If
                End While
                If contador <> cantTotal Then
                    Throw New Exception("Las Líneas a Ingresar no coinciden con el Total de líneas informadas.")
                End If
                Return ListaResult
            Catch ex As Exception
                Throw ex
            Finally
                'Cierra el archivo
                archivo.Close()
            End Try
        End Function
        Protected Overrides Sub ValidacionesPrimeraLinea(Linea As LineaArchivo)
            Dim objEntidad As LineaArchivoPF = CType(Linea, LineaArchivoPF)
            ' Nombre archivo Correcto
            Dim strFecha As String = "PF" + Year(CDate(objEntidad.FechaAcreditacion)).ToString + Right("00" + Month(CDate(objEntidad.FechaAcreditacion)).ToString, 2) + Right("00" + Day(CDate(objEntidad.FechaAcreditacion)).ToString, 2)
            If objEntidad.NombreArchivo <> strFecha Then
                Throw New Exception("El nombre del Archivo no se corresponde con su composición.")
            End If
            ' No Duplicados
            Dim Lista As List(Of Ingreso) = Ingreso.TraerTodosXFechasXAcreditacion(CDate(objEntidad.FechaAcreditacion), CDate(objEntidad.FechaAcreditacion), Enumeradores.TipoArchivo.PF)
            If Lista.Count > 0 Then
                Throw New Exception("Archivo ingresado anteriormente.")
            End If
        End Sub
        Public Overrides Sub IngresarListaLineas(ListaLineas As List(Of LineaArchivo))
            If ListaLineas.Count > 0 Then
                For Each item As LineaArchivoPF In ListaLineas
                    Dim objIngreso As New Ingreso(item)
                    objIngreso.Alta()
                Next
            End If
        End Sub
    End Class ' LineaArchivoBN
End Namespace ' Entidad