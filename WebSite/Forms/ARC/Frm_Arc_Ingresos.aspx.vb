Imports Clases.Entidad

Partial Class Forms_ARC_Frm_Arc_Ingresos
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
            LblError.Text = ""
            Dim resultadoImportacion As String = ""
            Dim pf As HttpPostedFile = FileUpload1.PostedFile
            Dim strFileName As String = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
            If FileUpload1.PostedFile Is Nothing OrElse String.IsNullOrEmpty(FileUpload1.PostedFile.FileName) OrElse FileUpload1.PostedFile.InputStream Is Nothing Then
                Throw New Exception("Por favor seleccione un archivo válido")
            End If
            Dim strArchivo = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName)
            If strArchivo.Length <> 10 Then
                Throw New Exception("El nombre del archivo debe ser BNaaaaMMdd o PFaaaaMMdd")
            End If
            nombreArchivo = strArchivo.Substring(0, 2)
            fechaArchivo = strArchivo.Substring(2, 8)
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            If Not (nombreArchivo = "BN" Or nombreArchivo = "PF") Then
                Throw New Exception("Por favor seleccione un archivo PF o BN")
            End If
            If Not (LCase(extension) = ".txt") Then
                Throw New Exception("Por favor seleccione un archivo PF o BN de extensión txt")
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
                Archivo.SubirArchivoTXT(1, strArchivo, file)
            End Using
            'msgValidacion.Mostrar(resultadoImportacion)
        Catch ex As Exception
            'msgValidacion.Mostrar(ex.Message.ToString)
            LblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
