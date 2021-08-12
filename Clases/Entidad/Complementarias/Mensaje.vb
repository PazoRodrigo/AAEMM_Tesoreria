Option Explicit On
Option Strict On

Imports Clases.Entidad
Imports Connection
Imports Clases.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class Mensaje
        Inherits DBE

        Private Shared _Todos As List(Of Mensaje)
        Public Shared Property Todos() As List(Of Mensaje)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Mensaje.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Mensaje))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer
        Public Property IdHastaPerfil() As Integer
        Public Property IdHastaUsuario() As Integer
        Public Property TextoMensaje() As String
        Public Property IdUsuarioOcultar() As Integer
        Public Property FechaOcultar As Date?

#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaOcultar() As Long
            Get
                Dim result As Long = 0
                If FechaOcultar.HasValue Then
                    result = CLng(Year(FechaOcultar.Value).ToString & Right("00" & Month(FechaOcultar.Value).ToString, 2) & Right("00" & Day(FechaOcultar.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        'Public Property IdLazy() As Integer
        'Private _ObjLazy As Lazy
        'Public ReadOnly Property Mensajero() As List(Of Mensaje)
        '    'Get
        '    '    If _ObjLazy Is Nothing Then
        '    '        _ObjLazy = Lazy.TraerUno(IdLazy)
        '    '    End If
        '    '    Return _ObjLazy
        '    'End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Mensaje = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdHastaPerfil = objImportar.IdHastaPerfil
            IdHastaUsuario = objImportar.IdHastaUsuario
            TextoMensaje = objImportar.TextoMensaje
            IdUsuarioOcultar = objImportar.IdUsuarioOcultar
            FechaOcultar = objImportar.FechaOcultar
        End Sub
#End Region
#Region " Métodos Estáticos"

        Public Sub AgregarMensaje()
            If IdHastaUsuario = 0 Then
                ' Va todos los del Perfil
                Alta(Enumeradores.TipoDestinatarioMensaje.Perfil)
            Else
                ' Va a un usuario único
                Alta(Enumeradores.TipoDestinatarioMensaje.Usuario)
            End If
        End Sub
        Public Shared Function TraerUno(ByVal Id As Integer) As Mensaje
            Dim result As Mensaje = DAL_Mensaje.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Mensaje)
            Return DAL_Mensaje.TraerTodos()
        End Function
        Public Shared Function TraerTodosXUsuario(idUsuario As Integer) As List(Of Mensaje)
            Return DAL_Mensaje.TraerTodosXUsuario(idUsuario)
        End Function

        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Private Sub Alta(IdTipoDestinatario As Enumeradores.TipoDestinatarioMensaje)
            ValidarAlta()
            DAL_Mensaje.Alta(Me, IdTipoDestinatario)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Mensaje.Baja(Me)
        End Sub
        Public Sub Ocultar()
            ValidarOcultar()
            DAL_Mensaje.Ocultar(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Mensaje.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Mensaje
            Dim result As New DTO.DTO_Mensaje
            result.IdUsuarioAlta = IdUsuarioAlta
            result.IdEntidad = IdEntidad
            result.TextoMensaje = TextoMensaje
            result.IdHastaPerfil = IdHastaPerfil
            result.IdHastaUsuario = IdHastaUsuario
            result.IdUsuarioOcultar = IdUsuarioOcultar
            result.FechaAlta = LngFechaAlta
            result.FechaBaja = LngFechaBaja
            result.FechaOcultar = LngFechaOcultar
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Mensaje.TraerTodos
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
        Private Sub ValidarOcultar()
            ValidarUsuario(Me.IdUsuarioOcultar)
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
            '		sError &= "<b>VariableString</b> El campo debe tener como máximo 50 caracteres. <br />"
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
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Mensaje.TraerTodosXDenominacionCant(Me.denominacion)
            'If Me.idEntidad = 0 Then
            '    ' Alta
            '    If cantidad > 0 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'Else
            '    ' Modifica
            '    If cantidad > 1 Then
            '        Throw New Exception("La denominación a ingresar ya existe")
            '    End If
            'End If
        End Sub
#End Region
    End Class ' Mensaje
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Mensaje
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer
        Public Property IdHastaPerfil() As Integer
        Public Property IdHastaUsuario() As Integer
        Public Property TextoMensaje() As String
        Public Property IdUsuarioOcultar() As Integer
        Public Property FechaOcultar() As Long

#End Region
    End Class ' DTO_Mensaje
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Mensaje

#Region " Stored "
        Const storeAltaXPerfil As String = "p_Mensaje_AltaXPerfil"
        Const storeAltaXUsuario As String = "p_Mensaje_AltaXUsuario"
        Const storeBaja As String = "p_Mensaje_Baja"
        Const storeOcultar As String = "p_Mensaje_Ocultar"
        Const storeModifica As String = "p_Mensaje_Modifica"
        Const storeTraerUnoXId As String = "p_Mensaje_TraerUnoXId"
        Const storeTraerTodos As String = "p_Mensaje_TraerTodos"
        Const storeTraerTodosXUsuario As String = "p_Mensaje_TraerTodosXUsuario"
        Const storeTraerTodosActivos As String = "p_Mensaje_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Mensaje, IdTipoDestinatario As Enumeradores.TipoDestinatarioMensaje)
            Dim store As String
            If IdTipoDestinatario = Enumeradores.TipoDestinatarioMensaje.Usuario Then
                store = storeAltaXUsuario
            ElseIf IdTipoDestinatario = Enumeradores.TipoDestinatarioMensaje.Perfil Then
                store = storeAltaXPerfil
            End If
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Mensaje", entidad.TextoMensaje.ToUpper.Trim)
            pa.add("@IdHastaPerfil", entidad.IdHastaPerfil)
            pa.add("@IdHastaUsuario", entidad.IdHastaUsuario)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Baja(ByVal entidad As Mensaje)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Ocultar(ByVal entidad As Mensaje)
            Dim store As String = storeOcultar
            Dim pa As New parametrosArray
            pa.add("@IdUsuarioOcultar", entidad.IdUsuarioOcultar)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Mensaje)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Mensaje", entidad.TextoMensaje.ToUpper.Trim)
            pa.add("@IdHastaPerfil", entidad.IdHastaPerfil)
            pa.add("@IdHastaUsuario", entidad.IdHastaUsuario)
            pa.add("@IdUsuarioOcultar", entidad.IdUsuarioOcultar)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Mensaje
            Dim store As String = storeTraerUnoXId
            Dim result As New Mensaje
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
        Public Shared Function TraerTodos() As List(Of Mensaje)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Mensaje)
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
        Public Shared Function TraerTodosXUsuario(idUsuario As Integer) As List(Of Mensaje)
            Dim store As String = storeTraerTodosXUsuario
            Dim pa As New parametrosArray
            pa.add("@idUsuario", idUsuario)
            Dim listaResult As New List(Of Mensaje)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Mensaje
            Dim entidad As New Mensaje
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
            If dr.Table.Columns.Contains("TextoMensaje") Then
                If dr.Item("TextoMensaje") IsNot DBNull.Value Then
                    entidad.TextoMensaje = dr.Item("TextoMensaje").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("IdHastaPerfil") Then
                If dr.Item("IdHastaPerfil") IsNot DBNull.Value Then
                    entidad.IdHastaPerfil = CInt(dr.Item("IdHastaPerfil"))
                End If
            End If
            If dr.Table.Columns.Contains("IdHastaUsuario") Then
                If dr.Item("IdHastaUsuario") IsNot DBNull.Value Then
                    entidad.IdHastaUsuario = CInt(dr.Item("IdHastaUsuario"))
                End If
            End If
            If dr.Table.Columns.Contains("IdUsuarioOcultar") Then
                If dr.Item("IdUsuarioOcultar") IsNot DBNull.Value Then
                    entidad.IdUsuarioOcultar = CInt(dr.Item("IdUsuarioOcultar"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaOcultar") Then
                If dr.Item("fechaOcultar") IsNot DBNull.Value Then
                    entidad.FechaOcultar = CDate(dr.Item("fechaOcultar"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Mensaje
End Namespace ' DataAccessLibrary