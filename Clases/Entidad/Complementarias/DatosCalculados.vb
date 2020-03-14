Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class DatosCalculados

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property CUIT() As Long
        Public Property SaldoCuentaCorriente() As Decimal
        Public Property Empleados() As Integer
        Public Property Afiliados() As Integer
        Public Property NoAfiliados() As Integer
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As DatosCalculados = TraerUno(id)
            ' Entidad
            SaldoCuentaCorriente = objImportar.SaldoCuentaCorriente
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As DatosCalculados
            Dim result As DatosCalculados = DAL_DatosCalculados.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_DatosCalculados
            Dim result As New DTO.DTO_DatosCalculados With {
                .IdEntidad = IdEntidad,
                .CUIT = CUIT,
                .SaldoCuentaCorriente = SaldoCuentaCorriente,
                .Empleados = Empleados,
                .Afiliados = Afiliados,
                .NoAfiliados = NoAfiliados
            }
            Return result
        End Function
        ' Nuevos
#End Region
    End Class ' DatosCalculados
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_DatosCalculados

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property CUIT() As Long
        Public Property SaldoCuentaCorriente() As Decimal
        Public Property Empleados() As Integer
        Public Property Afiliados() As Integer
        Public Property NoAfiliados() As Integer
#End Region
    End Class ' DTO_DatosCalculados
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_DatosCalculados

#Region " Stored "
        Const storeTraerUnoXId As String = "p_DatosCalculados_TraerUnoXID"
#End Region
#Region " Métodos Públicos "
        Public Shared Function TraerUno(ByVal Id As Integer) As DatosCalculados
            Dim store As String = storeTraerUnoXId
            Dim result As New DatosCalculados
            Dim pa As New parametrosArray
            pa.add("@Id", Id)
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
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As DatosCalculados
            Dim entidad As New DatosCalculados
            ' Entidad
            If dr.Table.Columns.Contains("IdEntidad") Then
                If dr.Item("IdEntidad") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("IdEntidad"))
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("SaldoCuentaCorriente") Then
                If dr.Item("SaldoCuentaCorriente") IsNot DBNull.Value Then
                    entidad.SaldoCuentaCorriente = CDec(dr.Item("SaldoCuentaCorriente"))
                End If
            End If
            If dr.Table.Columns.Contains("Empleados") Then
                If dr.Item("Empleados") IsNot DBNull.Value Then
                    entidad.Empleados = CInt(dr.Item("Empleados"))
                End If
            End If
            If dr.Table.Columns.Contains("Afiliados") Then
                If dr.Item("Afiliados") IsNot DBNull.Value Then
                    entidad.Afiliados = CInt(dr.Item("Afiliados"))
                End If
            End If
            If dr.Table.Columns.Contains("NoAfiliados") Then
                If dr.Item("NoAfiliados") IsNot DBNull.Value Then
                    entidad.NoAfiliados = CInt(dr.Item("NoAfiliados"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_DatosCalculados
End Namespace ' DataAccessLibrary