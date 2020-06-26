Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class MovimientoCtaCte
        Inherits DBE

        Private Shared _Todos As List(Of MovimientoCtaCte)
        Public Shared Property Todos() As List(Of MovimientoCtaCte)
            Get
                If _Todos Is Nothing Then
                    '_Todos = DAL_MovimientoCtaCte.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of MovimientoCtaCte))
                _Todos = value
            End Set
        End Property
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipoMovimiento() As Integer = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        'Public Property IdLazy() As Integer
        'Private _ObjLazy As Lazy
        'Public ReadOnly Property ObjLazy() As Lazy
        '    Get
        '        If _ObjLazy Is Nothing Then
        '            _ObjLazy = Lazy.TraerUno(IdLazy)
        '        End If
        '        Return _ObjLazy
        '    End Get
        'End Property
        Public ReadOnly Property IdEstado() As Integer
            Get
                Dim result As Integer = 0
                If FechaBaja.HasValue Then
                    result = 1
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As MovimientoCtaCte = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdTipoMovimiento = objImportar.IdTipoMovimiento
            Importe = objImportar.Importe
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_MovimientoCtaCte)
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
            IdTipoMovimiento = DtODesde.IdTipoMovimieno
            Importe = DtODesde.Importe
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As MovimientoCtaCte
            Dim result As MovimientoCtaCte = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of MovimientoCtaCte)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As MovimientoCtaCte
        '    Dim result As MovimientoCtaCte= DAL_MovimientoCtaCte.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of MovimientoCtaCte)
        '    Dim result As List(Of MovimientoCtaCte) = DAL_MovimientoCtaCte.TraerTodos()
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
            DAL_MovimientoCtaCte.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_MovimientoCtaCte.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_MovimientoCtaCte.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_MovimientoCtaCte
            Dim result As New DTO.DTO_MovimientoCtaCte With {
                .IdEntidad = IdEntidad,
                .IdTipoMovimieno = IdTipoMovimiento,
                .Importe = Importe,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub RefreshD()
            '_Todos = DAL_MovimientoCtaCte.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicadosD()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
            ValidarCampos()
            ValidarNoDuplicadosD()
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
        Private Sub ValidarNoDuplicadosD()

        End Sub
#End Region
    End Class ' MovimientoCtaCte
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_MovimientoCtaCte
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipoMovimieno() As Integer = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_MovimientoCtaCte
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_MovimientoCtaCte

#Region " Stored "
        Const storeAlta As String = "DIM.p_MovimientoCtaCte_Alta"
        Const storeBaja As String = "DIM.p_MovimientoCtaCte_Baja"
        Const storeModifica As String = "DIM.p_MovimientoCtaCte_Modifica"
        Const storeTraerUnoXId As String = "DIM.p_MovimientoCtaCte_TraerUnoXId"
        Const storeTraerTodosXEmpresa As String = "DIM.p_MovimientoCtaCte_TraerTodosXEmpresa"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As MovimientoCtaCte)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdTipoMovimiento", entidad.IdTipoMovimiento)
            pa.add("@Importe", entidad.Importe)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As MovimientoCtaCte)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As MovimientoCtaCte)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdTipoMovimiento", entidad.IdTipoMovimiento)
            pa.add("@Importe", entidad.Importe)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As MovimientoCtaCte
            Dim store As String = storeTraerUnoXId
            Dim result As New MovimientoCtaCte
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
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
        Public Shared Function TraerTodosXEmpresa(IdEmpresa As Integer) As List(Of MovimientoCtaCte)
            Dim store As String = storeTraerTodosXEmpresa
            Dim pa As New parametrosArray
            pa.add("@IdEmpresa", IdEmpresa)
            Dim listaResult As New List(Of MovimientoCtaCte)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As MovimientoCtaCte
            Dim entidad As New MovimientoCtaCte
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
            'If dr.Table.Columns.Contains("Nombre") Then
            '    If dr.Item("Nombre") IsNot DBNull.Value Then
            '        entidad.Nombre = dr.Item("Nombre").ToString.ToUpper.Trim
            '    End If
            'End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_MovimientoCtaCte
End Namespace ' DataAccessLibrary

