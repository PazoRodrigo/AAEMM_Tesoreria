'Imports System.Collections.Generic
'Imports System.Configuration
Imports Clases.Entidad

Partial Class Forms_ARC_Frm_Arc_Ingresos
    Inherits System.Web.UI.Page

#Region "Variables"
    Private fechaArchivo As Date
    Private nombreArchivo As String
#End Region

    Protected Sub Upload_Click(sender As Object, e As EventArgs) Handles Upload.Click
        SubirArchivo()
    End Sub
    Private Sub SubirArchivo()
        Try
            Dim resultadoImportacion As String = ""
            Dim pf As HttpPostedFile = FileUpload1.PostedFile
            Dim strFileName As String = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                Throw New Exception("Por favor seleccione un archivo válido")
            End If
            nombreArchivo = FileUpload1.PostedFile.FileName.Substring(0, 2)
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            If Not (nombreArchivo = "PF" Or nombreArchivo = "BN") And LCase(extension) = ".txt" Then
                Throw New Exception("Por favor seleccione un archivo PF o BN")
            End If
            Dim nomArc As String = "~/Archivos/TXT/"
            If nombreArchivo = "PF" Then
                nomArc &= "PF_"
            ElseIf nombreArchivo = "BN" Then
                nomArc &= "BN_"
            End If
            nomArc &= Now.ToString("yyyyMMdd_hhmmss") & extension
            FileUpload1.SaveAs(Server.MapPath(nomArc))
            Using file As IO.StreamReader = New System.IO.StreamReader(Server.MapPath(nomArc))
                Dim arc As New Archivo
                resultadoImportacion = arc.SubirArchivoTXT(1, nombreArchivo, fechaArchivo, file)
            End Using
            'msgValidacion.Mostrar(resultadoImportacion)
        Catch ex As Exception
            'msgValidacion.Mostrar(ex.Message.ToString)
            LblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
