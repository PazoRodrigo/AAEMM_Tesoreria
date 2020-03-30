Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Permiso
        Inherits DBE

        Private Shared _Todos As List(Of Permiso)
        Public Shared Property Todos() As List(Of Permiso)
            Get
                Return DAL_Permiso.TraerTodos
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Permiso.TraerTodos
                'End If
                'Return _Todos
            End Get
            Set(ByVal value As List(Of Permiso))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipoPermiso() As Enumeradores.TipoPermiso = Enumeradores.TipoPermiso.Pagina
        Public Property Nombre() As String = ""
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        Private _ObjFormulario As Formulario
        Public ReadOnly Property ObjFormulario() As Formulario
            Get
                If _ObjFormulario Is Nothing Then
                    _ObjFormulario = Formulario.TraerUnoXPermiso(IdEntidad)
                End If
                Return _ObjFormulario
            End Get
        End Property
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
            Dim objImportar As Permiso = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdTipoPermiso = objImportar.IdTipoPermiso
            IdTipoPermiso = objImportar.IdTipoPermiso
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Permiso)
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
            IdTipoPermiso = DtODesde.IdTipoPermiso
            Nombre = DtODesde.Nombre
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Permiso
            Dim result As Permiso = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen permisos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Permiso)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Permiso
        '    Dim result As Permiso= DAL_Permiso.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen permisos para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Permiso)
        '    Dim result As List(Of Permiso) = DAL_Permiso.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen permisos para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        Public Shared Function TraerTodosXPerfil(IdPerfil As Integer) As List(Of Permiso)
            Return DAL_Permiso.TraerTodosXPerfil(IdPerfil)
            'Dim result As List(Of Permiso) = DAL_Permiso.TraerTodos()
            'If result Is Nothing Then
            '    Throw New Exception("No existen permisos para la búsqueda")
            'End If
            'Return result
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Permiso.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Permiso.Baja(Me)
            Refresh()
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Permiso.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Permiso
            Dim result As New DTO.DTO_Permiso With {
                .IdEntidad = IdEntidad,
                .IdTipoPermiso = IdTipoPermiso,
                .Nombre = Nombre,
                .Observaciones = Observaciones,
                .ObjFormulario = ObjFormulario.ToDTO,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Permiso.TraerTodos
        End Sub
        ' Nuevos
        Friend Sub AltaPermisoEnPerfil(idUsuarioAlta As Integer, IdPerfil As Integer)
            DAL_Permiso.AltaPermisoEnPerfil(idUsuarioAlta, IdPerfil, Me.IdEntidad)
        End Sub
        Friend Sub ModificarPermisoEnPerfil(idUsuarioModifica As Integer, IdPerfil As Integer)
            DAL_Permiso.ModificarPermisoEnPerfil(idUsuarioModifica, IdPerfil, Me.IdEntidad)
        End Sub

        Friend Sub BajaPermisoEnPerfil(idUsuarioBaja As Integer, IdPerfil As Integer)
            DAL_Permiso.BajaPermisoEnPerfil(idUsuarioBaja, IdPerfil, Me.IdEntidad)
        End Sub
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
            Permiso.Refresh()
            If Not Todos Is Nothing AndAlso Todos.Count > 0 Then
                Dim result As Permiso = Todos.Find(Function(x) x.IdTipoPermiso = IdTipoPermiso)
                If Not result Is Nothing Then
                    If IdEntidad = 0 Then
                        ' Alta
                        Throw New Exception("El Nombre a ingresar ya existe")
                    Else
                        ' Modifica
                        If IdEntidad <> result.IdEntidad Then
                            Throw New Exception("El Nombre a ingresar ya existe")
                        End If
                    End If
                End If
            End If
        End Sub
#End Region
    End Class ' Permiso
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Permiso
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipoPermiso() As Enumeradores.TipoPermiso = Enumeradores.TipoPermiso.Pagina
        Public Property Nombre() As String = ""
        Public Property Observaciones() As String = ""
        Public Property ObjFormulario() As DTO_Formulario
#End Region
    End Class ' DTO_Permiso
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Permiso

#Region " Stored "
        ' ABM
        Const storeAlta As String = "USUARIO.p_Permiso_Alta"
        Const storeBaja As String = "USUARIO.p_Permiso_Baja"
        Const storeModifica As String = "USUARIO.p_Permiso_Modifica"
        ' Traer
        Const storeTraerUnoXId As String = "USUARIO.p_Permiso_TraerUnoXId"
        Const storeTraerTodos As String = "USUARIO.p_Permiso_TraerTodos"
        ' Otros
        Const storeTraerTodosXPerfil As String = "USUARIO.p_Permiso_TraerTodosXPerfil"
        Const storeAltaPermisoEnPerfil As String = "USUARIO.p_Permiso_PermisoEnPerfilAlta"
        Const storeModificarPermisoEnPerfil As String = "USUARIO.p_Permiso_PermisoEnPerfilModifica"
        Const storeBajaPermisoEnPerfil As String = "USUARIO.p_Permiso_PermisoEnPerfilBaja"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Permiso)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Nombre", entidad.IdTipoPermiso)
            pa.add("@IdTipoPermiso", entidad.IdTipoPermiso)
            pa.add("@Observaciones", entidad.Observaciones.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Permiso)
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
        Public Shared Sub Modifica(ByVal entidad As Permiso)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Nombre", entidad.IdTipoPermiso)
            pa.add("@IdTipoPermiso", entidad.IdTipoPermiso)
            pa.add("@Observaciones", entidad.Observaciones.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Permiso
            Dim store As String = storeTraerUnoXId
            Dim result As New Permiso
            Dim pa As New parametrosArray
            pa.add("@id", id)
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
        Public Shared Function TraerTodos() As List(Of Permiso)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Permiso)
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
        ' Otros
        Public Shared Function TraerTodosXPerfil(IdPerfil As Integer) As List(Of Permiso)
            Dim store As String = storeTraerTodosXPerfil
            Dim pa As New parametrosArray
            pa.add("IdPerfil", IdPerfil)
            Dim listaResult As New List(Of Permiso)
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
        Friend Shared Sub AltaPermisoEnPerfil(idUsuarioAlta As Integer, IdPerfil As Integer, idEntidad As Integer)
            Dim store As String = storeAltaPermisoEnPerfil
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", idUsuarioAlta)
            pa.add("@IdPermiso", idEntidad)
            pa.add("@idPerfil", IdPerfil)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Sub ModificarPermisoEnPerfil(idUsuarioModifica As Integer, IdPerfil As Integer, idEntidad As Integer)
            Dim store As String = storeModificarPermisoEnPerfil
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", idUsuarioModifica)
            pa.add("@IdPermiso", idEntidad)
            pa.add("@idPerfil", IdPerfil)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Sub BajaPermisoEnPerfil(idUsuarioBaja As Integer, IdPerfil As Integer, idEntidad As Integer)
            Dim store As String = storeBajaPermisoEnPerfil
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", idUsuarioBaja)
            pa.add("@IdPermiso", idEntidad)
            pa.add("@idPerfil", IdPerfil)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Permiso
            Dim entidad As New Permiso
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
            If dr.Table.Columns.Contains("Nombre") Then
                If dr.Item("Nombre") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Nombre").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("IdTipoPermiso") Then
                If dr.Item("IdTipoPermiso") IsNot DBNull.Value Then
                    entidad.IdTipoPermiso = CType(CInt(dr.Item("IdTipoPermiso")), Enumeradores.TipoPermiso)
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
    End Class ' DAL_Permiso
End Namespace ' DataAccessLibraryPublic Class Permiso
