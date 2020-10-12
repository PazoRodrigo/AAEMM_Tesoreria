Option Explicit On
Option Strict On

Imports Clases.Entidad
Imports Connection
Imports Clases.DataAccessLibrary
Imports LUM

Namespace Entidad
    Public Class TipoPagoManual
        Inherits DBE

        Private Shared _Todos As List(Of TipoPagoManual)
        Public Shared Property Todos() As List(Of TipoPagoManual)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_TipoPagoManual.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of TipoPagoManual))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Abreviatura() As String = ""
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
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As TipoPagoManual = TraerUno(id)
            ' DBE
            idUsuarioAlta = objImportar.idUsuarioAlta
            idUsuarioBaja = objImportar.idUsuarioBaja
            idUsuarioModifica = objImportar.idUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            fechaAlta = objImportar.fechaAlta
            fechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_TipoPagoManual)
            ' DBE
            IdUsuarioAlta = DtODesde.IdUsuarioAlta
            IdUsuarioBaja = DtODesde.IdUsuarioBaja
            IdUsuarioModifica = DtODesde.IdUsuarioModifica
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
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As TipoPagoManual
            Dim result As TipoPagoManual = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of TipoPagoManual)
            Return Todos
        End Function
        Public Shared Function TraerTodos_DTO() As List(Of DTO.DTO_TipoPagoManual)
            Return ToListDTO(Todos)
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As TipoPagoManual
        '    Dim result As TipoPagoManual= DAL_TipoPagoManual.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of TipoPagoManual)
        '    Dim result As List(Of TipoPagoManual) = DAL_TipoPagoManual.TraerTodos()
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
            DAL_TipoPagoManual.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_TipoPagoManual.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_TipoPagoManual.Modifica(Me)
        End Sub
        ' Otros
        Private Function ToDTO() As DTO.DTO_TipoPagoManual
            Dim result As New DTO.DTO_TipoPagoManual
            result.IdEntidad = IdEntidad
            result.Nombre = Nombre
            result.Abreviatura = Abreviatura
            Return result
        End Function
        Private Shared Function ToListDTO(lista As List(Of TipoPagoManual)) As List(Of DTO.DTO_TipoPagoManual)
            Dim Result As New List(Of DTO.DTO_TipoPagoManual)
            If lista IsNot Nothing AndAlso lista.Count > 0 Then
                For Each item As TipoPagoManual In lista
                    Result.Add(item.ToDTO)
                Next
            End If
            Return Result
        End Function
        Public Shared Sub refresh()
            _Todos = DAL_TipoPagoManual.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.idUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.idUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.idUsuarioModifica)
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
            'Dim cantidad As Integer = DAL_TipoPagoManual.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' TipoPagoManual
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_TipoPagoManual
        Inherits DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Abreviatura() As String = ""
#End Region
    End Class ' DTO_TipoPagoManual
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_TipoPagoManual

#Region " Stored "
        Const storeAlta As String = "p_TipoPagoManual_Alta"
        Const storeBaja As String = "p_TipoPagoManual_Baja"
        Const storeModifica As String = "p_TipoPagoManual_Modifica"
        Const storeTraerUnoXId As String = "p_TipoPagoManual_TraerUnoXId"
        Const storeTraerTodos As String = "DIM.p_TipoPagoManual_TraerTodos"
        Const storeTraerTodosActivos As String = "p_TipoPagoManual_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As TipoPagoManual)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.idUsuarioAlta)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As TipoPagoManual)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.idUsuarioBaja)
            pa.add("@id", entidad.idEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As TipoPagoManual)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.idUsuarioModifica)
            pa.add("@id", entidad.idEntidad)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As TipoPagoManual
            Dim store As String = storeTraerUnoXId
            Dim result As New TipoPagoManual
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
        Public Shared Function TraerTodos() As List(Of TipoPagoManual)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of TipoPagoManual)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As TipoPagoManual
            Dim entidad As New TipoPagoManual
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.idUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.idUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.idUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.fechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.fechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.idEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("Nombre") Then
                If dr.Item("Nombre") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Nombre").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Abreviatura") Then
                If dr.Item("Abreviatura") IsNot DBNull.Value Then
                    entidad.Abreviatura = dr.Item("Abreviatura").ToString.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_TipoPagoManual
End Namespace ' DataAccessLibrary