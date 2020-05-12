Imports System.IO

Namespace Entidad
    Public Class Archivo

#Region " Atributos / Propiedades "
        Public Property NombreArchivo As String = ""
#End Region
        Public ReadOnly Property SubirArchivoCSV(id_User As Integer, nombreArchivo As String, file As StreamReader) As String
            Get
                Select Case nombreArchivo
                    Case "EX"
                        Throw New Exception("Archivo EX")
                    Case "MC"
                        Throw New Exception("Archivo MC")
                    Case Else
                        Throw New Exception("Archivo .csv NO Soportado")
                End Select
            End Get
        End Property
#Region " Públicos"
        Private Structure StrResultadoIngresos
            Public cantAcreditados As Integer
            Public cantAcreditadosPendiente As Integer
            Public cantPendientes As Integer
            Public cantRechazados As Integer
        End Structure
        Public Shared Sub SubirArchivoTXT(idUsuario As Integer, nombreArchivo As String, archivo As System.IO.StreamReader)
            Dim result As String = ""
            ValidarNombreArchivo(nombreArchivo)
            Dim lista As New List(Of Ingreso)
            Dim Resultado As New StrResultadoIngresos
            'Dim importeTotalArchivoBN As Decimal = 0
            Select Case nombreArchivo.Substring(0, 2)
                Case "BN"
                    ArmarListaIngresos(Enumerador.TipoIngreso.BN, idUsuario, nombreArchivo, archivo, lista)
                    IngresarArchivoIngresos(Enumerador.TipoIngreso.BN, lista)
                    'Dim resultado As String = "<b> ERROR </b><br /><b>El archivo NO se ha ingresado</b> <br /> <br />" & result
                    'Throw New Exception(resultado)
                    'End If
                    Resultado.cantAcreditados = 0
                    Resultado.cantAcreditadosPendiente = 0
                    Resultado.cantPendientes = 0
                    Resultado.cantRechazados = 0
                    If lista.Count > 0 Then
                        For Each item As Ingreso In lista
                            If item.IdEstado.ToString.Length = 1 Then
                                Select Case item.IdEstado
                                    Case "A"
                                        Resultado.cantAcreditados += 1
                                    Case "L"
                                        Resultado.cantAcreditadosPendiente += 1
                                    Case "P"
                                        Resultado.cantPendientes += 1
                                    Case "R"
                                        Resultado.cantRechazados += 1
                                    Case Else
                                        Throw New Exception("Identificador de Estado a Revisar: " + item.IdEstado.ToString)
                                End Select
                            End If
                        Next
                    End If
                Case "PF"
                    ArmarListaIngresos(Enumerador.TipoIngreso.PF, idUsuario, nombreArchivo, archivo, lista)
                    IngresarArchivoIngresos(Enumerador.TipoIngreso.PF, lista)
                Case Else
                    Throw New Exception("Archivo no soportado")
            End Select
            Throw New Exception("Se han registrado " + lista.Count.ToString + " Ingresos.")
        End Sub
#End Region
#Region " Privados TXT "
        Private Shared Sub ArmarListaIngresos(Tipo As Enumerador.TipoIngreso, idUsuario As Integer, nombreArchivo As String, archivo As StreamReader, lista As List(Of Ingreso))
            Dim sError As String = ""
            Dim contador As Integer = 0
            Select Case Tipo
                Case Enumerador.TipoIngreso.BN
                    Try
                        While Not archivo.EndOfStream
                            contador += 1
                            Dim entidad As New Ingreso()
                            Dim linea1 As String = archivo.ReadLine
                            Dim linea2 As String = LTrim(linea1)
                            entidad.IdUsuarioAlta = idUsuario
                            entidad.FechaPago = CDate(linea2.Substring(24, 2) & "/" & linea2.Substring(22, 2) & "/" & linea2.Substring(18, 4))
                            entidad.FechaAcreditacion = CDate(linea2.Substring(32, 2) & "/" & linea2.Substring(30, 2) & "/" & linea2.Substring(26, 4))
                            entidad.IdOrigen = Enumerador.TipoIngreso.BN
                            entidad.NombreArchivo = nombreArchivo
                            entidad.Importe = CDec(linea2.Substring(43, 14).Insert(12, ","))
                            entidad.IdCentroCosto = linea2.Substring(62, 2)
                            entidad.Periodo = linea2.Substring(80, 6)
                            entidad.CUIT = linea2.Substring(69, 11)
                            ' Validar CUIT para traer Entidad
                            'Throw New Exception("Cuit Buscado no encontrado:  " & entidad.codEntidad & ". Línea " & listaIngresos.Count - 1)
                            entidad.CodigoEntidad = 22
                            entidad.IdCentroCosto = 1
                            entidad.IdEstado = CChar("A")
                            entidad.NroCheche = linea2.Substring(139, 15)
                            If CLng(entidad.NroCheche) > 0 Then
                                ' Es cheque
                                entidad.IdEstado = CChar("A")
                                If linea2.Length >= 155 Then
                                    If CChar(linea2.Substring(154, 1)) <> " " Then
                                        entidad.IdEstado = CChar(linea2.Substring(154, 1))
                                    End If
                                End If
                            Else
                                entidad.IdEstado = CChar("A")
                            End If
                            ' Es el primero y valida internamente para que no se repita
                            If lista.Count = 0 Then
                                Dim temp As String = nombreArchivo.Substring(2, 8)
                                Dim fechaArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
                                If fechaArchivo <> entidad.FechaAcreditacion Then
                                    Throw New Exception("Valide la fecha del Archivo, parece incorrecta.")
                                End If
                                'Dim listaValidar As List(Of Ingreso) = TraerTodosEntreFechasXTipo(entidad.NombreArchivo, entidad.FechaAcreditacion, entidad.FechaAcreditacion)
                                'If Not listaValidar Is Nothing Then
                                '    If listaValidar.Count > 0 Then
                                '        For Each item As IngresoXSeccional In listaValidar
                                '            If (item.fechaAcreditacion = entidad.FechaAcreditacion) And (item.importe = entidad.Importe) And (item.codEntidad = entidad.codEntidad) Then
                                '                Dim fecha As String = Right("00" & item.fechaArchivo.Value.Day, 2) & Right("00" & item.fechaArchivo.Value.Month, 2) & Right("00" & item.fechaArchivo.Value.Year, 4)
                                '                Throw New Exception("<b> ERROR </b><br /><b>El archivo NO se ha ingresado</b> <br /> <br />Ya se encuentra en el sistema con el nombre " & item.nombreArchivo & fecha)
                                '            End If
                                '        Next
                                '    End If
                                'End If
                            End If
                            lista.Add(entidad)
                        End While
                    Catch ex As Exception
                        Throw ex
                    Finally
                        'Cierra el archivo
                        archivo.Close()
                    End Try
                Case Enumerador.TipoIngreso.PF

                Case Else

            End Select
            If sError.Length > 0 Then
                Throw New Exception(sError)
            End If
        End Sub
        Private Shared Sub IngresarArchivoIngresos(tipo As Enumerador.TipoIngreso, lista As List(Of Ingreso))
            If lista.Count > 0 Then
                For Each item As Ingreso In lista
                    item.Alta()
                Next
            End If
        End Sub
#End Region
#Region " Privados"
        Private Shared Sub ValidarNombreArchivo(nombreArchivo As String)
            Dim temp As String = nombreArchivo.Substring(2, 8)
            Dim fechaArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
            If fechaArchivo > Today Then
                Throw New Exception("<b> ERROR </b><br /><b>El archivo NO se ha ingresado</b> <br /> <br />La fecha del Archivo es incorrecta")
            End If
            Ingreso.ValidarNombreArchivoDuplicado(nombreArchivo)

        End Sub
        Private Function ArmarListaIngresosPF(idUsuario As Integer, fechaArchivo As Date, archivo As StreamReader, lista As List(Of Ingreso)) As String
            'IdOrigen Tipo 2
            Throw New NotImplementedException()
        End Function
#End Region

    End Class ' Archivo
End Namespace ' Entidad
