Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Perfil
        Inherits DBE

        Private Shared _Todos As List(Of Perfil)
        Public Shared Property Todos() As List(Of Perfil)
            Get
                Return DAL_Perfil.TraerTodos
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Perfil.TraerTodos
                'End If
                'Return _Todos
            End Get
            Set(ByVal value As List(Of Perfil))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        Private _ListaPermisos As List(Of Permiso)
        Public ReadOnly Property ListaPermisos() As List(Of Permiso)
            Get
                Return Permiso.TraerTodosXPerfil(IdEntidad)
                'If _ListaPermisos Is Nothing Then
                '    _ListaPermisos = Permiso.TraerTodosXPerfil(IdEntidad)
                'End If
                'Return _ListaPermisos
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
            Dim objImportar As Perfil = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Perfil)
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
            Nombre = DtODesde.Nombre
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Perfil
            Dim result As Perfil = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen perfiles para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Perfil)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Perfil
        '    Dim result As Perfil= DAL_Perfil.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen perfiles para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Perfil)
        '    Dim result As List(Of Perfil) = DAL_Perfil.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen perfiles para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
        Public Shared Function TraerTodosXUsuario(IdUsuario As Integer) As List(Of Perfil)
            Return DAL_Perfil.TraerTodosXUsuario(IdUsuario)
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        ''' <summary>
        ''' CU.Perfil.01 - Creando Perfil
        ''' </summary>
        Public Sub Alta()
            ValidarAlta()
            DAL_Perfil.Alta(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU. Perfil.03 - Eliminando Perfil
        ''' </summary>
        Public Sub Baja()
            ValidarBaja()
            DAL_Perfil.Baja(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU. Perfil.02 - Modificando Perfil
        ''' </summary>
        Public Sub Modifica()
            ValidarModifica()
            DAL_Perfil.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Perfil
            Dim result As New DTO.DTO_Perfil With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado
            }
            Dim DTO_ListaPermisos As New List(Of DTO.DTO_Permiso)
            If Not ListaPermisos Is Nothing AndAlso ListaPermisos.Count > 0 Then
                For Each item As Permiso In ListaPermisos
                    DTO_ListaPermisos.Add(item.ToDTO)
                Next
            End If
            result.ListaPermisos = DTO_ListaPermisos
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Perfil.TraerTodos
        End Sub
        Friend Sub AltaPerfilEnUsuario(idUsuarioGenera As Integer, idUsuario As Integer)
            DAL_Perfil.AltaPerfilEnUsuario(idUsuarioGenera, idUsuario, Me.IdEntidad)
        End Sub
        Friend Sub ModificarPerfilEnUsuario(idUsuarioGenera As Integer, idUsuario As Integer)
            DAL_Perfil.ModificarPerfilEnUsuario(idUsuarioGenera, idUsuario, Me.IdEntidad)
        End Sub
        Friend Sub BajaPerfilEnUsuario(idUsuarioGenera As Integer, idUsuario As Integer)
            DAL_Perfil.BajaPerfilEnUsuario(idUsuarioGenera, idUsuario, Me.IdEntidad)
        End Sub
        ''' <summary>
        ''' CU. Formulario.06 – Agregando Formulario a Perfil
        ''' </summary>
        ''' <param name="IdPermiso"></param>
        Public Sub AgregarPermiso(IdPermiso As Integer)
            Dim ObjPermiso As New Permiso(IdPermiso)
            Dim ListaTemp As List(Of Permiso) = ListaPermisos
            If Not ListaTemp Is Nothing AndAlso ListaTemp.Count > 0 Then
                Dim encontrado As Boolean = False
                For Each item As Permiso In ListaTemp
                    If item.IdEntidad = IdPermiso Then
                        encontrado = True
                        ObjPermiso = item
                    End If
                Next
                If Not encontrado Then
                    ObjPermiso.AltaPermisoEnPerfil(IdUsuarioModifica, IdEntidad)
                Else
                    If ObjPermiso.FechaBaja.HasValue Then
                        ObjPermiso.ModificarPermisoEnPerfil(IdUsuarioModifica, IdEntidad)
                    End If
                End If
            Else
                ObjPermiso.AltaPermisoEnPerfil(IdUsuarioModifica, IdEntidad)
            End If
        End Sub
        ''' <summary>
        ''' CU. Formulario.07 – Quitando Formulario a Perfil
        ''' </summary>
        ''' <param name="IdPermiso"></param>
        Public Sub QuitarPermiso(IdPermiso As Integer)
            Dim ObjPermiso As New Permiso(IdPermiso)
            Dim ListaTemp As List(Of Permiso) = ListaPermisos
            If Not ListaTemp Is Nothing AndAlso ListaTemp.Count > 0 Then
                Dim encontrado As Boolean = False
                For Each item As Permiso In ListaTemp
                    If item.IdEntidad = IdPermiso Then
                        encontrado = True
                        ObjPermiso = item
                    End If
                Next
                If encontrado Then
                    If Not ObjPermiso.FechaBaja.HasValue Then
                        ObjPermiso.BajaPermisoEnPerfil(IdUsuarioModifica, IdEntidad)
                    End If
                End If
            End If
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
            If Me.Nombre = "" Then
                sError &= "<b>Nombre</b> Debe ingresar el Nombre.<br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre</b>Debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.Observaciones <> "" AndAlso Me.Observaciones.Length > 500 Then
                sError &= "<b>Observaciones</b> Debe tener como máximo 500 caracteres. <br />"
            End If
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
            Perfil.Refresh()
            If Todos.Count > 0 Then
                Dim result As Perfil = Todos.Find(Function(x) x.Nombre.ToUpper = Nombre.ToUpper)
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
    End Class ' Perfil
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Perfil
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Observaciones() As String = ""
        Public Property ListaPermisos() As List(Of DTO_Permiso)
#End Region
    End Class ' DTO_Perfil
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Perfil

#Region " Stored "
        Const storeAlta As String = "USUARIO.p_Perfil_Alta"
        Const storeBaja As String = "USUARIO.p_Perfil_Baja"
        Const storeModifica As String = "USUARIO.p_Perfil_Modifica"
        ' Traer
        Const storeTraerUnoXId As String = "USUARIO.p_Perfil_TraerUnoXId"
        Const storeTraerTodos As String = "USUARIO.p_Perfil_TraerTodos"
        ' Otros
        Const storeTraerTodosXUsuario As String = "USUARIO.[p_Perfil_TraerTodosXUsuario]"
        'Const storeTraerTodosXUsuario As String = "USUARIO.p_Usuario_TraerTodosXUsuario"
        Const storeAltaPerfilEnUsuario As String = "USUARIO.p_Usuario_PerfilEnUsuarioAlta"
        Const storeModificarPerfilEnUsuario As String = "USUARIO.p_Usuario_PerfilEnUsuarioModifica"
        Const storeBajaPerfilEnUsuario As String = "USUARIO.p_Usuario_PerfilEnUsuarioBaja"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Perfil)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
            pa.add("@Observaciones", entidad.Observaciones.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Perfil)
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
        Public Shared Sub Modifica(ByVal entidad As Perfil)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
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
        Public Shared Function TraerUno(ByVal id As Integer) As Perfil
            Dim store As String = storeTraerUnoXId
            Dim result As New Perfil
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
        Public Shared Function TraerTodos() As List(Of Perfil)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Perfil)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        ' Otros
        Friend Shared Function TraerTodosXUsuario(idUsuario As Integer) As List(Of Perfil)
            Dim store As String = storeTraerTodosXUsuario
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Perfil)
            pa.add("@idUsuario", idUsuario)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Sub AltaPerfilEnUsuario(idUsuarioAlta As Integer, idUsuario As Integer, idPerfil As Integer)
            Dim store As String = storeAltaPerfilEnUsuario
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", idUsuarioAlta)
            pa.add("@idUsuario", idUsuario)
            pa.add("@idPerfil", idPerfil)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub ModificarPerfilEnUsuario(idUsuarioModifica As Integer, idUsuario As Integer, idPerfil As Integer)
            Dim store As String = storeModificarPerfilEnUsuario
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", idUsuarioModifica)
            pa.add("@idUsuario", idUsuario)
            pa.add("@idPerfil", idPerfil)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Sub BajaPerfilEnUsuario(idUsuarioBaja As Integer, idUsuario As Integer, idPerfil As Integer)
            Dim store As String = storeBajaPerfilEnUsuario
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", idUsuarioBaja)
            pa.add("@idUsuario", idUsuario)
            pa.add("@idPerfil", idPerfil)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Perfil
            Dim entidad As New Perfil
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
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Perfil
End Namespace ' DataAccessLibrary