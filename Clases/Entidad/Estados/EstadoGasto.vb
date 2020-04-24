Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class EstadoGasto

        Private Shared _Todos As List(Of EstadoGasto)
        Public Shared Property Todos() As List(Of EstadoGasto)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_EstadoGasto.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of EstadoGasto))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Estado() As String = ""
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As EstadoGasto = TraerUno(id)
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As EstadoGasto
            Dim result As EstadoGasto = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Private Shared Function TraerTodos() As List(Of EstadoGasto)
            Return Todos
        End Function
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_EstadoGasto
            Dim result As New DTO.DTO_EstadoGasto With {
                .IdEntidad = IdEntidad,
                .Estado = Estado,
                .Observaciones = Observaciones
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_EstadoGasto.TraerTodos
        End Sub
        ' Nuevos
#End Region
    End Class ' EstadoGasto
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_EstadoGasto

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Estado() As String = ""
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_EstadoGasto
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_EstadoGasto

#Region " Stored "
        Const storeTraerTodos As String = "ADM.p_EstadoGasto_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' Traer
        Public Shared Function TraerTodos() As List(Of EstadoGasto)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EstadoGasto)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As EstadoGasto
            Dim entidad As New EstadoGasto
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("Estado") Then
                If dr.Item("Estado") IsNot DBNull.Value Then
                    entidad.Estado = dr.Item("Estado").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_EstadoGasto
End Namespace ' DataAccessLibrary