Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class EstadoCheque

        Private Shared _Todos As List(Of EstadoCheque)
        Public Shared Property Todos() As List(Of EstadoCheque)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_EstadoCheque.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of EstadoCheque))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Estado() As String = ""
        Public Property Observaciones() As String = ""
        Public Property IdTipoCheque() As Enumeradores.TipoCheque
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As EstadoCheque = TraerUno(id)
            ' Entidad
            IdEntidad = objImportar.IdEntidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As EstadoCheque
            Dim result As EstadoCheque = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Private Shared Function TraerTodos() As List(Of EstadoCheque)
            Return Todos
        End Function
        Public Shared Function TraerTodos_ChequesPropios() As List(Of EstadoCheque)
            Dim result As List(Of EstadoCheque) = Todos.FindAll(Function(x) x.IdTipoCheque = Enumeradores.TipoCheque.Propio)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos_ChequesTerceros() As List(Of EstadoCheque)
            Dim result As List(Of EstadoCheque) = Todos.FindAll(Function(x) x.IdTipoCheque = Enumeradores.TipoCheque.Tercero)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_EstadoCheque
            Dim result As New DTO.DTO_EstadoCheque With {
                .IdEntidad = IdEntidad,
                .Estado = Estado,
                .Observaciones = Observaciones,
                .IdTipoCheque = IdTipoCheque
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_EstadoCheque.TraerTodos
        End Sub
        ' Nuevos
#End Region
    End Class ' EstadoCheque
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_EstadoCheque

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Estado() As String = ""
        Public Property Observaciones() As String = ""
        Public Property IdTipoCheque() As Enumeradores.TipoCheque
#End Region
    End Class ' DTO_EstadoCheque
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_EstadoCheque

#Region " Stored "
        Const storeTraerTodos As String = "ADM.p_EstadoCheque_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' Traer
        Public Shared Function TraerTodos() As List(Of EstadoCheque)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of EstadoCheque)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As EstadoCheque
            Dim entidad As New EstadoCheque
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
            If dr.Table.Columns.Contains("IdTipoCheque") Then
                If dr.Item("IdTipoCheque") IsNot DBNull.Value Then
                    entidad.IdTipoCheque = CType(CInt(dr.Item("IdTipoCheque")), Enumeradores.TipoCheque)
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_EstadoCheque
End Namespace ' DataAccessLibrary