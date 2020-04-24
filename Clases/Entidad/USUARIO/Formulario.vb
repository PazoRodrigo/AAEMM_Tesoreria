Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Formulario
        Inherits DBE

        Private Shared _Todos As List(Of Formulario)
        Public Shared Property Todos() As List(Of Formulario)
            Get
                Return DAL_Formulario.TraerTodos
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Formulario.TraerTodos
                'End If
                'Return _Todos
            End Get
            Set(ByVal value As List(Of Formulario))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdPermiso() As Integer = 0
        Public Property ASPX() As String = ""
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
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
            Dim objImportar As Formulario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdPermiso = objImportar.IdPermiso
            ASPX = objImportar.ASPX
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Formulario)
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
            IdPermiso = DtODesde.IdPermiso
            ASPX = DtODesde.ASPX
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Formulario
            Dim result As Formulario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen formularios para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXPermiso(ByVal IdPermiso As Integer) As Formulario
            Dim result As Formulario = Todos.Find(Function(x) x.IdPermiso = IdPermiso)
            If result Is Nothing Then
                Throw New Exception("No existen formularios para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Formulario)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As Formulario
        '    Dim result As Formulario= DAL_Formulario.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen formularios para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Formulario)
        '    Dim result As List(Of Formulario) = DAL_Formulario.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen formularios para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        ''' <summary>
        ''' CU.Formulario.01 - Creando Formulario
        ''' </summary>
        Public Sub Alta()
            ValidarAlta()
            DAL_Formulario.Alta(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU. Formulario.03 - Eliminando Formulario
        ''' </summary>
        Public Sub Baja()
            ValidarBaja()
            DAL_Formulario.Baja(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU. Formulario.02 - Modificando Formulario
        ''' </summary>
        Public Sub Modifica()
            ValidarModifica()
            DAL_Formulario.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Formulario
            Dim result As New DTO.DTO_Formulario With {
                .IdEntidad = IdEntidad,
                .ASPX = ASPX,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Formulario.TraerTodos
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
            If Me.IdPermiso.ToString = "" Then
                sError &= "<b>Permiso</b> Debe ingresar el Permiso. <br />"
            ElseIf Not IsNumeric(Me.IdPermiso) Then
                sError &= "<b>Permiso</b> Debe ser numérico. <br />"
            Else
                If Me.IdPermiso = 0 Then
                    sError &= "<b>Permiso</b> Debe ingresar el Permiso. <br />"
                End If
            End If
            If Me.ASPX = "" Then
                sError &= "<b>ASPX</b> Debe ingresar el Formulario ASPX. <br />"
            ElseIf Me.ASPX.Length > 50 Then
                sError &= "<b>ASPX</b>Debe tener como máximo 50 caracteres. <br />"
            ElseIf ASPX.Contains(" ") Then
                sError &= "<b>ASPX</b> No debe contener espacios. <br />"
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
            Formulario.Refresh()
            If Todos.Count > 0 Then
                Dim result As Formulario = Todos.Find(Function(x) x.ASPX.ToUpper = ASPX.ToUpper)
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
    End Class ' Formulario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Formulario
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdPermiso() As Integer = 0
        Public Property ASPX() As String = ""
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_Formulario
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Formulario

#Region " Stored "
        Const storeAlta As String = "USUARIO.p_Formulario_Alta"
        Const storeBaja As String = "USUARIO.p_Formulario_Baja"
        Const storeModifica As String = "USUARIO.p_Formulario_Modifica"
        'Const storeTraerUnoXId As String = "USUARIO.p_Formulario_TraerUnoXId"
        Const storeTraerTodos As String = "USUARIO.p_Formulario_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Formulario)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@ASPX", entidad.ASPX.ToUpper.Trim)
            pa.add("@IdPermiso", entidad.IdPermiso)
            pa.add("@Observaciones", entidad.Observaciones.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Formulario)
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
        Public Shared Sub Modifica(ByVal entidad As Formulario)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@ASPX", entidad.ASPX.ToUpper.Trim)
            pa.add("@IdPermiso", entidad.IdPermiso)
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
        'Public Shared Function TraerUno(ByVal id As Integer) As Formulario
        '    Dim store As String = storeTraerUnoXId
        '    Dim result As New Formulario
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
        Public Shared Function TraerTodos() As List(Of Formulario)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Formulario)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Formulario
            Dim entidad As New Formulario
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
            If dr.Table.Columns.Contains("IdPermiso") Then
                If dr.Item("IdPermiso") IsNot DBNull.Value Then
                    entidad.IdPermiso = CInt(dr.Item("IdPermiso"))
                End If
            End If
            If dr.Table.Columns.Contains("ASPX") Then
                If dr.Item("ASPX") IsNot DBNull.Value Then
                    entidad.ASPX = dr.Item("ASPX").ToString.ToUpper.Trim
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
    End Class ' DAL_Formulario
End Namespace ' DataAccessLibraryPublic Class Formulario
