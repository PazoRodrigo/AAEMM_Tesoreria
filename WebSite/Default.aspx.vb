﻿Imports Clases.Entidad

Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        '    Response.Redirect("~/Forms/Login/Frm_Login.aspx")
        'End If

        Dim chP As List(Of ChequePropio) = ChequePropio.TraerTodos()
        Dim chT As List(Of ChequeTercero) = ChequeTercero.TraerTodos()
    End Sub
End Class
