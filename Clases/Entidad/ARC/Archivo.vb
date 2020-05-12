Imports System.IO

Namespace Entidad
    Public Class Archivo
        Public ReadOnly Property SubirArchivoTXT(id_User As Integer, nombreArchivo As String, fechaArchivo As Date, file As StreamReader) As String
            Get
                Select Case nombreArchivo
                    Case "BN"
                        Throw New Exception("Archivo BN")

                    Case "PF"
                        Throw New Exception("Archivo PF")

                    Case Else
                        Throw New Exception("Archivo .txt NO Soportado")

                End Select
            End Get
        End Property
        Public ReadOnly Property SubirArchivoCSV(id_User As Integer, nombreArchivo As String, fechaArchivo As Date, file As StreamReader) As String
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
    End Class ' Archivo
End Namespace ' Entidad
