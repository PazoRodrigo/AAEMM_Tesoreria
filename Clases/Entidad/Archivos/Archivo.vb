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
        Public Shared Function SubirArchivoTXT(idUsuario As Integer, nombreArchivo As String, archivo As System.IO.StreamReader) As String
            ValidarNombreArchivo(nombreArchivo)
            Dim lista As New List(Of LineaArchivo)
            Dim objLinea As LineaArchivo
            Select Case nombreArchivo.Substring(0, 2)
                Case "BN"
                    objLinea = New LineaArchivoBN
                Case "PF"
                    objLinea = New LineaArchivoPF
                Case "MC"
                    objLinea = New LineaArchivoMC
                Case "MP"
                    objLinea = New LineaArchivoMP
                Case Else
                    Throw New Exception("Archivo NO soportado")
            End Select
            lista = objLinea.ArmarListaLineas(idUsuario, nombreArchivo, archivo)
            objLinea.IngresarListaLineas(lista)
            Return nombreArchivo + " ha ingresado correctamente."
        End Function
#End Region
#Region " Privados Listas"
        Private Shared Function ArmarResultadoIngreso(lista As List(Of LineaArchivo)) As String
            Dim Resultado As New StrResultadoIngresos
            Resultado.cantAcreditados = 0
            Resultado.cantAcreditadosPendiente = 0
            Resultado.cantPendientes = 0
            Resultado.cantRechazados = 0
            Resultado.cantNoEncontrados = 0
            If lista.Count = 0 Then
                Throw New Exception("No se han ingresado registros")
            End If
            'For Each item As Ingreso In lista
            '    If item.IdEstado.ToString.Length = 1 Then
            '        Select Case item.IdEstado
            '            Case CChar("A")
            '                Resultado.cantAcreditados += 1
            '            Case CChar("L")
            '                Resultado.cantAcreditadosPendiente += 1
            '            Case CChar("P")
            '                Resultado.cantPendientes += 1
            '            Case CChar("R")
            '                Resultado.cantRechazados += 1
            '            Case CChar("T")
            '                Resultado.cantNoEncontrados += 1
            '            Case Else
            '                Throw New Exception("Identificador de Estado a Revisar: " + item.IdEstado.ToString)
            '        End Select
            '    End If
            'Next
            Return "Se han registrado " + lista.Count.ToString + " ingresos."
        End Function
#End Region
#Region " Validaciones y Funciones"
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
                Case "MC"
                    Ingreso.ValidarNombreArchivoDuplicado(nombreArchivo)
                Case Else

            End Select
        End Sub

#End Region
    End Class ' Archivo
End Namespace ' Entidad
