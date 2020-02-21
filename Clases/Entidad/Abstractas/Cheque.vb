Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public MustInherit Class Cheque
        Inherits DBE

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdBanco() As Integer = 0
        Public Property IdEstado() As Enumeradores.EstadoCheque = Nothing
        Public Property Numero() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region

    End Class ' Cheque
End Namespace ' Entidad

Namespace DTO
    Public MustInherit Class DTO_Cheque
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdBanco() As Integer = 0
        Public Property Numero() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_CentroCosto
End Namespace ' DTO

