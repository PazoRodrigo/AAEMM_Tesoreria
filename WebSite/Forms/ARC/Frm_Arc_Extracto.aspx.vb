Imports Clases.Entidad

Partial Class Forms_ARC_Frm_Arc_Extracto
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
            If Not (nombreArchivo = "EX" Or nombreArchivo = "MC") Then
                Throw New Exception("Por favor seleccione un archivo EX o MC")
            End If
            If Not (LCase(extension) = ".csv") Then
                Throw New Exception("Por favor seleccione un archivo EX o MC de extensión csv")
            End If
            Dim nomArc As String = "~/Archivos/CSV/"
            If nombreArchivo = "EX" Then
                nomArc &= "EX_"
            ElseIf nombreArchivo = "MC" Then
                nomArc &= "MC_"
            End If
            nomArc &= Now.ToString("yyyyMMdd_hhmmss") & extension
            FileUpload1.SaveAs(Server.MapPath(nomArc))
            Using file As IO.StreamReader = New System.IO.StreamReader(Server.MapPath(nomArc))
                Dim arc As New Archivo
                resultadoImportacion = arc.SubirArchivoCSV(1, nombreArchivo, fechaArchivo, file)
            End Using
        Catch ex As Exception
            LblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
