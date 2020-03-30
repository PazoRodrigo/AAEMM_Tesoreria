Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Gasto
        Inherits DBE

        Private Shared _Todos As List(Of Gasto)
        Public Shared Property Todos() As List(Of Gasto)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Gasto.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Gasto))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdEstado() As Enumeradores.EstadoGasto = Nothing
        Public Property CantidadComprobantes() As Integer = 0
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        Public ReadOnly Property Importe() As Decimal
            Get
                Dim resultImporte As Decimal = 0
                Dim resultCantidad As Integer = 0
                Try
                    Dim listaComprobantes As List(Of Comprobante) = Comprobante.TraerTodosXGasto(IdEntidad)
                    If Not listaComprobantes Is Nothing AndAlso listaComprobantes.Count > 0 Then
                        For Each item As Comprobante In listaComprobantes
                            If Not item.FechaBaja.HasValue Then
                                resultImporte += item.Importe
                                resultCantidad += 1
                            End If
                        Next
                    End If
                    CantidadComprobantes = resultCantidad
                Catch ex As Exception
                End Try
                Return resultImporte
            End Get
        End Property
        Public ReadOnly Property Estado() As String
            Get
                Dim result As String = ""
                Select Case IdEstado
                    Case Enumeradores.EstadoGasto.Abierto
                        result = "Abierto"
                    Case Enumeradores.EstadoGasto.Pagado
                        result = "Pagado"
                    Case Enumeradores.EstadoGasto.Anulado
                        result = "Anulado"
                    Case Else
                        result = "Sin Estado"
                End Select
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Gasto = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Gasto)
            ' DBE
            IdUsuarioAlta = DtODesde.IdUsuarioAlta
            IdUsuarioBaja = DtODesde.IdUsuarioBaja
            IdMotivoBaja = DtODesde.IdMotivoBaja
            If DtODesde.FechaAlta > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaAlta.ToString, 2) + "/" + Left(Right(DtODesde.FechaAlta.ToString, 4), 2) + "/" + Left(DtODesde.FechaAlta.ToString, 4)
                FechaAlta = CDate(TempFecha)
            End If
            If DtODesde.FechaBaja > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaBaja.ToString, 2) + "/" + Left(Right(DtODesde.FechaBaja.ToString, 4), 2) + "/" + Left(DtODesde.FechaBaja.ToString, 4)
                FechaBaja = CDate(TempFecha)
            End If
            ' Entidad
            IdEntidad = DtODesde.IdEntidad
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Gasto
            Dim result As Gasto = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Gasto)
            Return Todos
        End Function
        Public Shared Function TraerGastoAbierto() As Gasto
            Dim result As Gasto = Todos.Find(Function(x) CInt(x.IdEstado) = CInt(Enumeradores.EstadoGasto.Abierto))
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Gasto
        '    Dim result As Gasto= DAL_Gasto.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Gasto)
        '    Dim result As List(Of Gasto) = DAL_Gasto.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Gasto.Alta(Me)
            Refresh()
        End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_Gasto.Baja(Me)
        '    Refresh()
        'End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Gasto.Modifica(Me)
        '    Refresh()
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Gasto
            Dim result As New DTO.DTO_Gasto With {
                .IdEntidad = IdEntidad,
                .Importe = Importe,
                .CantidadComprobantes = CantidadComprobantes,
                .Estado = Estado,
                .Observaciones = Observaciones,
                .FechaAlta = LngFechaAlta
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Gasto.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        ' Validaciones
        Private Sub ValidarUsuario(ByVal idUsuario As Integer)
            If idUsuario = 0 Then
                Throw New Exception("Debe ingresar al sistema")
            End If
        End Sub
        Private Sub ValidarCampos()
            Dim sError As String = ""
            ValidarCaracteres()
            ' Campo Integer/Decimal
            '	If Me.VariableNumero.toString = "" Then
            '   	sError &= "<b>VariableNumero</b> Debe ingresar VariableNumero. <br />"
            '	ElseIf Not isnumeric(Me.VariableNumero) Then
            '   	sError &= "<b>VariableNumero</b> Debe ser numérico. <br />"
            '	End If

            ' Campo String
            '	If Me.VariableString = "" Then
            '		sError &= "<b>VariableString</b> Debe ingresar VariableString. <br />"
            '	ElseIf Me.apellido.Length > 50 Then
            '		sError &= "<b>VariableString</b>Debe tener como máximo 50 caracteres. <br />"
            '	End If

            ' Campo Date
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarCaracteres()
            Dim sError As String = ""
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            Gasto.Refresh()
            Dim result As Gasto = Todos.Find(Function(x) x.Observaciones.ToUpper = Observaciones)
            If Not result Is Nothing Then
                Throw New Exception("El Observaciones a ingresar ya existe")
            End If
        End Sub
#End Region
    End Class ' Gasto
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Gasto
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Observaciones() As String = ""
        Public Property Estado() As String = ""
        Public Property Importe() As Decimal = 0
        Public Property CantidadComprobantes() As Integer = 0
#End Region
    End Class ' DTO_Gasto
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Gasto

#Region " Stored "
        Const storeAlta As String = "ADM.p_Gasto_Alta"
        Const storeBaja As String = "ADM.p_Gasto_Baja"
        Const storeModifica As String = "ADM.p_Gasto_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_Gasto_TraerUnoXId"
        Const storeTraerTodos As String = "ADM.p_Gasto_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Gasto)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Gasto)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Gasto)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        '' Traer
        'Public Shared Function TraerUno(ByVal id As Integer) As Gasto
        '    Dim store As String = storeTraerUnoXId
        '    Dim result As New Gasto
        '    Dim pa As New parametrosArray
        '    pa.add("@id", id)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                result = LlenarEntidad(dt.Rows(0))
        '            ElseIf dt.Rows.Count = 0 Then
        '                result = Nothing
        '            End If
        '        End If
        '    End Using
        '    Return result
        'End Function
        Public Shared Function TraerTodos() As List(Of Gasto)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Gasto)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                Else
                    listaResult = Nothing
                End If
            End Using
            Return listaResult
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Gasto
            Dim entidad As New Gasto
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("IdEstado") Then
                If dr.Item("IdEstado") IsNot DBNull.Value Then
                    entidad.IdEstado = CType(dr.Item("IdEstado"), Enumeradores.EstadoGasto)
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
    End Class ' DAL_Gasto
End Namespace ' DataAccessLibrary