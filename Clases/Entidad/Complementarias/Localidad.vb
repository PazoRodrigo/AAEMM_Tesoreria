Option Explicit On
Option Strict On

Imports LUM
Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Localidad
        Inherits LUM.DBE

        Private Shared _Todos As List(Of Localidad)
        Public Shared Property Todos() As List(Of Localidad)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Localidad.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Localidad))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Property IdEntidad() As Integer = 0
        Property Descripcion As String = ""
        Property CP As Integer = 0
        Property Sec As String = ""
        Property IdProvincia As Integer = 0
        Property IdSeccional As Integer = 0
#End Region
#Region " Lazy Load "

        'Public ReadOnly Property ObjProvincia() As Provincia
        '    Get
        '        If _ObjProvincia Is Nothing Then
        '            _ObjProvincia = Provincia.TraerUno(IdProvincia)
        '        End If
        '        Return _ObjProvincia
        '    End Get
        'End Property

        'Property _ObjSeccional As Seccional
        'Public ReadOnly Property ObjSeccional() As Seccional
        '    Get
        '        If _ObjSeccional Is Nothing Then
        '            _ObjSeccional = DAL_Seccional.TraerUno(IdSeccional)
        '        End If
        '        Return _ObjSeccional
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Localidad = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Descripcion = objImportar.Descripcion
            CP = objImportar.CP
            Sec = objImportar.Sec
            IdProvincia = objImportar.IdProvincia
            IdSeccional = objImportar.IdSeccional
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Localidad
        '    Dim result As Localidad = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Localidad)
        '    Return Todos
        'End Function

        Public Shared Function TraerUno(ByVal Id As Integer) As Localidad
            Dim result As Localidad = DAL_Localidad.TraerUno(Id)
            If result Is Nothing Then
                ' Throw New Exception("No existen localidades con ese Identificador")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Localidad)
            Dim result As List(Of Localidad) = DAL_Localidad.TraerTodos()
            If result Is Nothing Then
                ' Throw New Exception("No existen localidades")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCP(CP As Integer) As List(Of Localidad)
            Dim result As List(Of Localidad) = DAL_Localidad.TraerTodosXCP(CP)
            'If result Is Nothing Then
            '    Throw New Exception("No existen localidades para el Código Postal")
            'End If
            Return result
        End Function
        'Public Function ExecuteAsync(ByVal cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)
        '    Dim response As HttpResponseMessage = New HttpResponseMessage(HttpStatusCode.InternalServerError)
        '    response.Content = New StringContent(Content)
        '    response.RequestMessage = Request
        '    Return New Task.FromResult(response)
        'End Function
        'Public Function TraerTodosXCP(CP As Integer) As Task(Of List(Of Localidad))
        '    Dim result As List(Of Localidad) = DAL_Localidad.TraerTodosXCP(CP)
        '    Dim r As TResult = DAL_Localidad.TraerTodosXCP(CP)
        '    'Dim response = _service.Process(Of List(Of Localidad))(BackOfficeEndpoint.CountryEndpoint, "returnCountries")
        '    Return Task.FromResult(response)
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_Localidad
            Dim result As New DTO.DTO_Localidad With {
                .IdEntidad = IdEntidad,
                .Descripcion = Descripcion,
                .CP = CP,
                .Sec = Sec,
                .IdProvincia = IdProvincia,
                .IdSeccional = IdSeccional
            }
            Return result
        End Function

        Public Shared Sub Refresh()
            _Todos = DAL_Localidad.TraerTodos
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
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Localidad.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Localidad
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Localidad

#Region " Atributos / Propiedades"
        Property IdEntidad() As Integer = 0
        Property Descripcion As String = ""
        Property CP As Integer = 0
        Property Sec As String = ""
        Property IdProvincia As Integer = 0
        Property IdSeccional As Integer = 0
#End Region
    End Class ' DTO_Localidad
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Localidad

#Region " Stored "
        Const storeTraerUnoXId As String = "DIM.p_Localidad_TraerUnoXId"
        Const storeTraerTodosXCP As String = "DIM.p_Localidad_TraerTodasXCodigoPostal"
        Const storeTraerTodos As String = "DIM.p_Localidad_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Localidad
            Dim store As String = storeTraerUnoXId
            Dim result As New Localidad
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
        Friend Shared Function TraerTodosXCP(CP As Integer) As List(Of Localidad)
            Dim store As String = storeTraerTodosXCP
            Dim result As New Localidad
            Dim pa As New parametrosArray
            pa.add("@cp", CP)
            Dim listaResult As New List(Of Localidad)
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
        Public Shared Function TraerTodos() As List(Of Localidad)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Localidad)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Localidad
            Dim entidad As New Localidad
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
            If dr.Table.Columns.Contains("Id_Localidad") Then
                If dr.Item("Id_Localidad") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id_Localidad"))
                End If
            End If
            If dr.Table.Columns.Contains("cp") Then
                If dr.Item("cp") IsNot DBNull.Value Then
                    entidad.CP = CInt(dr.Item("cp"))
                End If
            End If
            'If dr.Table.Columns.Contains("cpa") Then
            '    If dr.Item("cpa") IsNot DBNull.Value Then
            '        entidad.CPA = CStr(dr.Item("cpa"))
            '    End If
            'End If
            ' VariableString
            If dr.Table.Columns.Contains("Localidad") Then
                If dr.Item("Localidad") IsNot DBNull.Value Then
                    entidad.Descripcion = dr.Item("Localidad").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Id_Sec") Then
                If dr.Item("Id_Sec") IsNot DBNull.Value Then
                    entidad.IdSeccional = CInt(dr.Item("Id_Sec").ToString.Trim)
                End If
            End If
            If dr.Table.Columns.Contains("sec") Then
                If dr.Item("sec") IsNot DBNull.Value Then
                    entidad.Sec = dr.Item("sec").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("id_Provincia") Then
                If dr.Item("id_Provincia") IsNot DBNull.Value Then
                    entidad.IdProvincia = CInt(dr.Item("id_Provincia").ToString.Trim)
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Localidad
End Namespace ' DataAccessLibrary