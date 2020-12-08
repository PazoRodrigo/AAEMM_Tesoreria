Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class CuentaCorriente

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Saldo() As Decimal = 0
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal IdEntidad As Integer)
            Dim objImportar As CuentaCorriente = TraerUno(IdEntidad)
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Saldo = objImportar.Saldo
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_CuentaCorriente)
            ' Entidad
            IdEntidad = DtODesde.IdEntidad
            Nombre = DtODesde.Nombre
            Saldo = DtODesde.Saldo
        End Sub
#End Region
#Region " Métodos Públicos "
        ' Traer
        Public Shared Function TraerUno(ByVal IdEntidad As Integer) As CuentaCorriente
            Dim result As CuentaCorriente = DAL_CuentaCorriente.TraerUno(IdEntidad)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of CuentaCorriente)
            Return DAL_CuentaCorriente.TraerTodos()
        End Function

        Public Shared Function TraerTodosSaldos() As List(Of CuentaCorriente)
            Return DAL_CuentaCorriente.TraerTodosSaldos()
        End Function

        Public Function toDto() As DTO.DTO_CuentaCorriente
            Dim result As New DTO.DTO_CuentaCorriente With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
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
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Saldo() As Decimal = 0
#End Region
    End Class ' DTO_CuentaCorriente
End Namespace ' DTO


Namespace DataAccessLibrary
    Public Class DAL_CuentaCorriente

#Region " Stored "
        Const storeTraerUno As String = "ADM.p_CuentaCorriente_TraerUnoXCUIT"
        Const storeTraerTodos As String = "[CONTABLE].[p_CuentaCorriente_TraerTodos]"
        Const storeTraerTodosSaldos As String = "[CONTABLE].[p_CuentaCorriente_TraerTodosSaldos]"
#End Region
#Region " Métodos Públicos "
        ' ABM
        ' Traer
        Public Shared Function TraerUno(ByVal IdEntidad As Integer) As CuentaCorriente
            Dim store As String = storeTraerUno
            Dim result As New CuentaCorriente
            Dim pa As New parametrosArray
            pa.add("@IdEntidad", IdEntidad)
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
        Friend Shared Function TraerTodos() As List(Of CuentaCorriente)
            Dim store As String = storeTraerTodos
            Dim listaResult As New List(Of CuentaCorriente)
            Dim pa As New parametrosArray
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            listaResult.Add(LlenarEntidad(dr))
                        Next
                    End If
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosSaldos() As List(Of CuentaCorriente)
            Dim store As String = storeTraerTodosSaldos
            Dim listaResult As New List(Of CuentaCorriente)
            Dim pa As New parametrosArray
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            listaResult.Add(LlenarEntidad(dr))
                        Next
                    End If
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function LlenarEntidad(ByVal dr As DataRow) As CuentaCorriente
            Dim entidad As New CuentaCorriente
            ' Entidad
            If dr.Table.Columns.Contains("Id") Then
                If dr.Item("Id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id"))
                End If
            End If
            If dr.Table.Columns.Contains("Nombre") Then
                If dr.Item("Nombre") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Nombre").ToString.ToUpper.Trim
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