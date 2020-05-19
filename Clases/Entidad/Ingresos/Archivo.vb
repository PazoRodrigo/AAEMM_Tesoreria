Option Explicit On
Option Strict On

Imports System.IO

Namespace Entidad
    Public Class Archivo

        Private Structure StrResultadoIngresos
            Public cantAcreditados As Integer
            Public cantAcreditadosPendiente As Integer
            Public cantPendientes As Integer
            Public cantRechazados As Integer
            Public cantNoEncontrados As Integer
        End Structure
#Region " Atributos / Propiedades "
        Public Property NombreArchivo As String = ""
#End Region

#Region " Públicos Estáticos"
        Public Shared Function TraerUltimosXTipo(cantidad As Integer) As List(Of Archivo)

        End Function
        Public Shared Function SubirArchivoTXT(idUsuario As Integer, nombreArchivo As String, archivo As System.IO.StreamReader) As String
            ValidarNombreArchivo(nombreArchivo)
            Dim lista As New List(Of Ingreso)
            'Dim importeTotalArchivoBN As Decimal = 0
            Select Case nombreArchivo.Substring(0, 2)
                Case "BN"
                    ArmarListaIngresos(Enumerador.TipoArchivo.BN, idUsuario, nombreArchivo, archivo, lista)
                    IngresarArchivoIngresos(Enumerador.TipoArchivo.BN, lista)
                    'Dim resultado As String = "<b> ERROR </b><br /><b>El archivo NO se ha ingresado</b> <br /> <br />" & result
                    'Throw New Exception(resultado)
                    'End If
                Case "PF"
                    ArmarListaIngresos(Enumerador.TipoArchivo.PF, idUsuario, nombreArchivo, archivo, lista)
                    IngresarArchivoIngresos(Enumerador.TipoArchivo.PF, lista)
                Case "TR"
                    ArmarListaIngresos(Enumerador.TipoArchivo.TR, idUsuario, nombreArchivo, archivo, lista)
                    IngresarArchivoIngresos(Enumerador.TipoArchivo.TR, lista)
                Case Else
                    Throw New Exception("Archivo NO soportado")
            End Select
            Return ArmarResultadoIngreso(lista)
        End Function
#End Region
#Region " Privados Listas"
        Private Shared Sub ArmarListaIngresos(Tipo As Enumerador.TipoArchivo, idUsuario As Integer, nombreArchivo As String, archivo As StreamReader, lista As List(Of Ingreso))
            Dim sError As String = ""
            Dim contador As Integer = 0
            Select Case Tipo
                Case Enumerador.TipoArchivo.BN
                    Try
                        Dim entidad As New Ingreso
                        While Not archivo.EndOfStream
                            contador += 1
                            entidad = New Ingreso
                            entidad.IdUsuarioAlta = idUsuario
                            entidad.IdOrigen = Enumerador.TipoArchivo.BN
                            entidad.NombreArchivo = nombreArchivo
                            Dim linea1 As String = archivo.ReadLine
                            Dim linea2 As String = LTrim(linea1)
                            entidad.FechaPago = CDate(linea2.Substring(24, 2) & "/" & linea2.Substring(22, 2) & "/" & linea2.Substring(18, 4))
                            entidad.FechaAcreditacion = CDate(linea2.Substring(32, 2) & "/" & linea2.Substring(30, 2) & "/" & linea2.Substring(26, 4))
                            entidad.Importe = CDec(linea2.Substring(43, 14).Insert(12, ","))
                            entidad.IdCentroCosto = CInt(linea2.Substring(62, 2))
                            entidad.Periodo = CInt(linea2.Substring(80, 6))
                            entidad.CUIT = CLng(linea2.Substring(69, 11))
                            entidad.IdEstado = CChar("A")
                            entidad.NroCheche = CLng(linea2.Substring(139, 15))
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
                            ResolverCodigoEntidad(entidad)
                            ' Es el primero y valida internamente para que no se repita
                            If lista.Count = 0 Then
                                Dim temp As String = nombreArchivo.Substring(2, 8)
                                Dim fechaArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
                                If fechaArchivo <> entidad.FechaAcreditacion Then
                                    Throw New Exception("Valide la fecha del Archivo, parece incorrecta.")
                                End If
                            End If
                            lista.Add(entidad)
                        End While
                    Catch ex As Exception
                        Throw ex
                    Finally
                        'Cierra el archivo
                        archivo.Close()
                    End Try
                Case Enumerador.TipoArchivo.PF
                    Try
                        Dim cantTotal As Integer = 0
                        Dim fechaArchivo As Date
                        Dim fechaProceso As Date
                        Dim entidad As New Ingreso
                        While Not archivo.EndOfStream
                            Dim linea As String = archivo.ReadLine
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
                                entidad = New Ingreso
                                entidad.IdUsuarioAlta = idUsuario
                                entidad.IdOrigen = Enumerador.TipoArchivo.PF
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
                                ResolverCodigoEntidad(entidad)
                                '    cuit = Configuration.ConfigurationManager.AppSettings("CUIT_UTEDYC")
                            ElseIf linea.StartsWith("7") Then
                                lista.Add(entidad)
                            ElseIf linea.StartsWith("8") Then
                                cantTotal = CInt(linea.Substring(15, 7))
                            End If
                        End While
                    Catch ex As Exception
                        Throw ex
                    Finally
                        'Cierra el archivo
                        archivo.Close()
                    End Try
                Case Enumerador.TipoArchivo.TR
                    Try
                        Dim entidad As New Ingreso
                        Dim varTempo As String = ""
                        Dim primeraLinea As Integer = 9
                        Dim nroLinea As Integer = 0
                        Dim linea As String
                        Dim ComienzoConcepto As String = ""
                        Dim opcionCUIT_1 As String = ""
                        Dim opcionCUIT_2 As String = ""
                        While Not archivo.EndOfStream
                            linea = archivo.ReadLine
                            nroLinea += 1
                            If nroLinea >= primeraLinea Then
                                ' Para que ingrese sólo Transferencias
                                ComienzoConcepto = linea.Split(CChar(";"))(4).ToString.Trim.Substring(0, 2)
                                If ComienzoConcepto = "TR" Then
                                    contador += 1
                                    entidad = New Ingreso
                                    entidad.IdUsuarioAlta = idUsuario
                                    entidad.IdOrigen = Enumerador.TipoArchivo.TR
                                    entidad.NombreArchivo = nombreArchivo
                                    ' Fecha Valor
                                    varTempo = linea.Split(CChar(";"))(1).ToString.Trim
                                    entidad.FechaAcreditacion = CDate(varTempo)
                                    entidad.FechaPago = CDate(varTempo)
                                    ' Monto
                                    varTempo = linea.Split(CChar(";"))(2).ToString.Trim
                                    entidad.Importe = CDec(varTempo)
                                    ' Referencia / Nro de operacion para que por SP (Fecha, Importe, Nro Operacion) no duplique el registro si se carga 2 veces
                                    'varTempo = linea.Split(CChar(";"))(3).ToString.Trim & " - " & linea.Split(CChar(";"))(4).ToString.Trim
                                    varTempo = linea.Split(CChar(";"))(3).ToString.Trim
                                    entidad.Observaciones = varTempo
                                    ' Referencia
                                    entidad.Periodo = calcularPeriodoPago(entidad.FechaAcreditacion)
                                    ' Concepto
                                    varTempo = linea.Split(CChar(";"))(4).ToString.Trim
                                    If IsNumeric(varTempo.Substring(4, 1)) Then
                                        opcionCUIT_1 = varTempo.Substring(4, 11) ' TR. 30663409391 HIPERBAIRES SRL  
                                        entidad.CUIT = CLng(opcionCUIT_1)
                                    Else
                                        opcionCUIT_2 = varTempo.Substring(5, 11) ' TRANS30570194646SIGVART G J SIMO
                                        entidad.CUIT = CLng(opcionCUIT_2)
                                    End If
                                    ResolverCodigoEntidad(entidad)
                                    entidad.IdEstado = CChar("A")
                                    If entidad.CodigoEntidad = 0 Then
                                        entidad.IdEstado = CChar("T")
                                    End If
                                    ' Es el primero y valida internamente para que no se repita
                                    'If lista.Count = 0 Then
                                    '    Dim temp As String = nombreArchivo.Substring(2, 8)
                                    '    Dim fechaArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
                                    '    If fechaArchivo <> entidad.FechaAcreditacion Then
                                    '        Throw New Exception("Valide la fecha del Archivo, parece incorrecta.")
                                    '    End If
                                    'End If
                                    lista.Add(entidad)
                                End If
                            End If
                        End While
                    Catch ex As Exception
                        Throw ex
                    Finally
                        'Cierra el archivo
                        archivo.Close()
                    End Try
                Case Else
                    Throw New Exception("Tipo de Ingreso no Soportado")
            End Select
            If contador <> lista.Count Then
                Throw New Exception("Validar Archivo con Sistemas. Diferencia de registros a Ingresar.")
            End If
            If sError.Length > 0 Then
                Throw New Exception(sError)
            End If
        End Sub
        Private Shared Sub IngresarArchivoIngresos(tipo As Enumerador.TipoArchivo, lista As List(Of Ingreso))
            If lista.Count > 0 Then
                For Each item As Ingreso In lista
                    item.Alta()
                Next
            End If
        End Sub
        Private Shared Function ArmarResultadoIngreso(lista As List(Of Ingreso)) As String
            Dim Resultado As New StrResultadoIngresos
            Resultado.cantAcreditados = 0
            Resultado.cantAcreditadosPendiente = 0
            Resultado.cantPendientes = 0
            Resultado.cantRechazados = 0
            Resultado.cantNoEncontrados = 0
            If lista.Count = 0 Then
                Throw New Exception("No se han ingresado registros")
            End If
            For Each item As Ingreso In lista
                If item.IdEstado.ToString.Length = 1 Then
                    Select Case item.IdEstado
                        Case CChar("A")
                            Resultado.cantAcreditados += 1
                        Case CChar("L")
                            Resultado.cantAcreditadosPendiente += 1
                        Case CChar("P")
                            Resultado.cantPendientes += 1
                        Case CChar("R")
                            Resultado.cantRechazados += 1
                        Case CChar("T")
                            Resultado.cantNoEncontrados += 1
                        Case Else
                            Throw New Exception("Identificador de Estado a Revisar: " + item.IdEstado.ToString)
                    End Select
                End If
            Next
            Return "Se han registrado " + lista.Count.ToString + " ingresos."
        End Function
#End Region
#Region " Validaciones y Funciones"
        Private Shared Sub ResolverCodigoEntidad(entidad As Ingreso)
            entidad.CodigoEntidad = 0
            If entidad.CUIT > 0 Then
                Try
                    Dim Empresa As Empresa = Empresa.TraerUnaXCUIT(entidad.CUIT)
                    If Empresa IsNot Nothing Then
                        entidad.CodigoEntidad = Empresa.IdEntidad
                    End If
                Catch ex As Exception
                    ' No hace nada
                End Try
            End If
        End Sub
        Private Shared Sub ValidarNombreArchivo(nombreArchivo As String)
            ' Solo valida que el Nombre del Archivo NO exista en el sistema, NO su interior
            Dim temp As String = nombreArchivo.Substring(2, 8)
            Dim fechaArchivo As Date = CDate(Right(temp, 2) + "-" + Left(Right(temp, 4), 2) + "-" + Left(temp, 4))
            If fechaArchivo > Today Then
                'Throw New Exception("<b> ERROR </b><br /><b>El archivo NO se ha ingresado</b> <br /> <br />La fecha del Archivo es incorrecta")
                Throw New Exception("<b>El archivo NO se ha ingresado</b> <br />La fecha del Archivo es incorrecta.")
            End If
            Select Case nombreArchivo.Substring(0, 2)
                Case "BN"
                    Ingreso.ValidarNombreArchivoDuplicado(nombreArchivo)
                Case "PF"
                    Ingreso.ValidarNombreArchivoDuplicado(nombreArchivo)
                Case "TR"
                    Ingreso.ValidarNombreArchivoDuplicado(nombreArchivo)
                Case Else

            End Select
        End Sub
        Private Shared ReadOnly Property calcularPeriodoPago(fechaAcreditacion As Date?) As Integer
            Get
                Dim result As Integer = 0
                Dim mes As Integer
                Dim anio As Integer
                If fechaAcreditacion.HasValue Then
                    anio = CInt(fechaAcreditacion.Value.Year)
                    mes = CInt(fechaAcreditacion.Value.Month)
                    If mes = 1 Then
                        anio = anio - 1
                        mes = 12
                    End If
                    result = CInt(anio & Right("00" & mes.ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
    End Class ' Archivo
End Namespace ' Entidad
