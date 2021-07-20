Option Explicit On
Option Strict On


Imports Clases
Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
            Catch ex As Exception
                MsgBox(ex.Message.ToString, MsgBoxStyle.DefaultButton1)
            End Try
        End If
    End Sub
End Class
