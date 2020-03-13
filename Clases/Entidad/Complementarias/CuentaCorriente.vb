Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class CuentaCorriente

#Region " Atributos / Propiedades "
        Public Property CUIT() As Long = 0
        Public Property Saldo() As Decimal = 0
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal CUIT As Long)
            Dim objImportar As CuentaCorriente = TraerUno(CUIT)
            ' Entidad
            CUIT = objImportar.CUIT
            Saldo = objImportar.Saldo
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_CuentaCorriente)
            ' Entidad
            CUIT = DtODesde.CUIT
            Saldo = DtODesde.Saldo
        End Sub
#End Region
#Region " Métodos Públicos "
        ' Traer

        Public Shared Function TraerUno(ByVal CUIT As Long) As CuentaCorriente
            Dim result As CuentaCorriente = DAL_cuentaCorriente.TraerUno(CUIT)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Friend Shared Function LlenarCuentaCorriente(dr As DataRow) As CuentaCorriente
            Dim result As New CuentaCorriente
            If Not dr Is Nothing Then
                result = DAL_CuentaCorriente.LlenarEntidad(dr)
            End If
            Return result
        End Function
        Friend Function toDto() As DTO.DTO_CuentaCorriente
            Dim result As New DTO.DTO_CuentaCorriente With {
                .CUIT = CUIT,
                .Saldo = Saldo
            }
            Return result
        End Function
#End Region



    End Class ' CuentaCorriente
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_CuentaCorriente

#Region " Atributos / Propiedades"
        Public Property CUIT() As Long = 0
        Public Property Saldo() As Decimal = 0
#End Region
    End Class ' DTO_CuentaCorriente
End Namespace ' DTO


Namespace DataAccessLibrary
    Public Class DAL_cuentaCorriente

#Region " Stored "
        Const storeTraerUnoXCUIT As String = "ADM.p_CuentaCorriente_TraerUnoXCUIT"
#End Region
#Region " Métodos Públicos "
        ' ABM
        ' Traer
        Public Shared Function TraerUno(ByVal CUIT As Long) As CuentaCorriente
            Dim store As String = storeTraerUnoXCUIT
            Dim result As New CuentaCorriente
            Dim pa As New parametrosArray
            pa.add("@CUIT", CUIT)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    ElseIf dt.Rows.Count = 0 Then
                        result = Nothing
                    End If
                End If
            End Using
            Return result
        End Function
        Public Shared Function LlenarEntidad(ByVal dr As DataRow) As CuentaCorriente
            Dim entidad As New CuentaCorriente
            ' Entidad
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT").ToString.ToUpper.Trim)
                End If
            End If
            If dr.Table.Columns.Contains("Saldo") Then
                If dr.Item("Saldo") IsNot DBNull.Value Then
                    entidad.Saldo = CDec(dr.Item("Saldo"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Empresa
End Namespace ' DataAccessLibrary