Option Explicit On
Option Strict On

Namespace DTO
    Public MustInherit Class DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdUsuarioAlta() As Integer = 0
        Public Property IdUsuarioBaja() As Integer = 0
        Public Property IdUsuarioModifica() As Integer = 0
        Public Property FechaAlta() As Long = 0
        Public Property FechaBaja() As Long = 0
        Public Property IdMotivoBaja() As Integer = 0
        Public Property IdEstado() As Integer = 0
#End Region
    End Class ' DTO_CentroCosto
End Namespace ' DTO