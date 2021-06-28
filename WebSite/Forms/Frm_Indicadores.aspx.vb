Option Explicit On
Option Strict On

Imports Clases.Entidad

Partial Class Forms_Frm_Indicadores
    Inherits System.Web.UI.Page

#Region "Variables"
    Private fechaArchivo As String
    Private nombreArchivo As String
#End Region

    Protected Sub Upload_Click(sender As Object, e As EventArgs) Handles Upload.Click
        SubirArchivo()
    End Sub
    Private Sub SubirArchivo()
        Try
            LblOK.Text = ""
            LblError.Text = ""
            Dim resultadoImportacion As String = ""
            Dim pf As HttpPostedFile = FileUpload1.PostedFile
            Dim strFileName As String = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                Throw New Exception("Por favor seleccione un archivo válido")
            End If
            Dim strArchivo = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName)
            If strArchivo.Length <> 10 Then
                Throw New Exception("El nombre del archivo debe ser BNaaaaMMdd, PFaaaaMMdd o TRaaaaMMdd. Ej. BN20200826")
            End If
            nombreArchivo = strArchivo.Substring(0, 2)
            fechaArchivo = strArchivo.Substring(2, 8)
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            If Not (nombreArchivo = "BN" Or nombreArchivo = "PF" Or nombreArchivo = "MC" Or nombreArchivo = "MP") Then
                Throw New Exception("Por favor seleccione un archivo BN, PF, MC o MP")
            End If
            If nombreArchivo = "BN" Or nombreArchivo = "PF" Then
                If Not (LCase(extension) = ".txt") Then
                    Throw New Exception("Por favor seleccione un archivo PF o BN de extensión txt")
                End If
            End If
            If nombreArchivo = "MC" Or nombreArchivo = "MP" Then
                If Not (LCase(extension) = ".csv") Then
                    Throw New Exception("Por favor seleccione un archivo TF de extensión csv")
                End If
            End If
            Dim RutaDestino As String = "~/Archivos/TXT/" & nombreArchivo & "_" & Now.ToString("yyyyMMdd_hhmmss") & extension
            FileUpload1.SaveAs(Server.MapPath(RutaDestino))
            Dim Resultado As String = ""
            Using file As IO.StreamReader = New System.IO.StreamReader(Server.MapPath(RutaDestino))
                Resultado = Archivo.SubirArchivoTXT(1, strArchivo, file)
            End Using
            LblOK.Text = Resultado
        Catch et As TypeInitializationException
            LblError.Text = et.Message.ToString
        Catch ex As Exception
            LblError.Text = ex.Message.ToString
        End Try
    End Sub

End Class
