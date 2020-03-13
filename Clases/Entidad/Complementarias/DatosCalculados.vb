Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class DatosCalculados

#Region " Atributos / Propiedades "
        Public Property SaldoCuentaCorriente() As Decimal
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
                .IdEntidad = IdEntidad
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
        Public Property SaldoCuentaCorriente() As Decimal
#End Region
    End Class ' DTO_DatosCalculados
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_DatosCalculados

#Region " Stored "
        Const storeTraerUnoXId As String = "p_DatosCalculados_TraerUnoXId"
#End Region
#Region " Métodos Públicos "
        Public Shared Function TraerUno(ByVal id As Integer) As DatosCalculados
            Dim store As String = storeTraerUnoXId
            Dim result As New DatosCalculados
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa, "StrLibreriasEnlatadoSindical")
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
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.idEntidad = CInt(dr.Item("id"))
                End If
            End If
            ' VariableString
            '	If dr.Table.Columns.Contains("VariableString") Then
            '   	If dr.Item("VariableString") IsNot DBNull.Value Then
            '   		entidad.VariableString = dr.Item("VariableString").ToString.ToUpper.Trim
            '    	End If
            '	End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_DatosCalculados
End Namespace ' DataAccessLibrary