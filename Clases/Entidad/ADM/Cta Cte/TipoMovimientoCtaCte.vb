Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class TipoMovimientoCtaCte
        Inherits DBE

        Private Shared _TodosD As List(Of TipoMovimientoCtaCte)
        Public Shared Property TodosD() As List(Of TipoMovimientoCtaCte)
            Get
                If _TodosD Is Nothing Then
                    _TodosD = DAL_TipoMovimientoCtaCte.TraerTodosD
                End If
                Return _TodosD
            End Get
            Set(ByVal value As List(Of TipoMovimientoCtaCte))
                _TodosD = value
            End Set
        End Property
        Private Shared _TodosH As List(Of TipoMovimientoCtaCte)
        Public Shared Property TodosH() As List(Of TipoMovimientoCtaCte)
            Get
                If _TodosH Is Nothing Then
                    _TodosH = DAL_TipoMovimientoCtaCte.TraerTodosH
                End If
                Return _TodosH
            End Get
            Set(ByVal value As List(Of TipoMovimientoCtaCte))
                _TodosH = value
            End Set
        End Property
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipo() As Enumeradores.TipoMovimientoDH
        Public Property Nombre() As String = ""
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
            Dim objImportar As TipoMovimientoCtaCte = TraerUno(id)
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
        Sub New(ByVal DtODesde As DTO.DTO_TipoMovimientoCtaCte)
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
        Public Shared Function TraerUno(ByVal Id As Integer) As TipoMovimientoCtaCte
            Dim result As TipoMovimientoCtaCte = TodosD.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of TipoMovimientoCtaCte)
            Return TodosD
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As TipoMovimientoCtaCte
        '    Dim result As TipoMovimientoCtaCte= DAL_TipoMovimientoCtaCte.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of TipoMovimientoCtaCte)
        '    Dim result As List(Of TipoMovimientoCtaCte) = DAL_TipoMovimientoCtaCte.TraerTodos()
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
            DAL_TipoMovimientoCtaCte.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_TipoMovimientoCtaCte.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_TipoMovimientoCtaCte.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_TipoMovimientoCtaCte
            Dim result As New DTO.DTO_TipoMovimientoCtaCte With {
                .IdEntidad = IdEntidad,
                .IdTipo = IdTipo,
                .Nombre = Nombre,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado
            }
            Return result
        End Function
        Public Shared Sub RefreshD()
            _TodosD = DAL_TipoMovimientoCtaCte.TraerTodosD
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
            TipoMovimientoCtaCte.RefreshD()
            Dim result As TipoMovimientoCtaCte = TodosD.Find(Function(x) x.Nombre.ToUpper = Nombre And x.IdEntidad <> IdEntidad)
            If Not result Is Nothing Then
                Throw New Exception("El Nombre a ingresar ya existe")
            End If
        End Sub
#End Region
    End Class ' TipoMovimientoCtaCte
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_TipoMovimientoCtaCte
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdTipo() As Enumeradores.TipoMovimientoDH
        Public Property Nombre() As String = ""
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_TipoMovimientoCtaCte
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_TipoMovimientoCtaCte

#Region " Stored "
        Const storeAlta As String = "DIM.p_TipoMovimientoCtaCte_Alta"
        Const storeBaja As String = "DIM.p_TipoMovimientoCtaCte_Baja"
        Const storeModifica As String = "DIM.p_TipoMovimientoCtaCte_Modifica"
        Const storeTraerUnoXId As String = "DIM.p_TipoMovimientoCtaCte_TraerUnoXId"
        Const storeTraerTodosD As String = "DIM.p_TipoMovimientoCtaCte_TraerTodosD"
        Const storeTraerTodosH As String = "DIM.p_TipoMovimientoCtaCte_TraerTodosH"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As TipoMovimientoCtaCte)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdTipo", entidad.IdTipo)
            pa.add("@Nombre", entidad.Nombre)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As TipoMovimientoCtaCte)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As TipoMovimientoCtaCte)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Nombre", entidad.Nombre)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As TipoMovimientoCtaCte
            Dim store As String = storeTraerUnoXId
            Dim result As New TipoMovimientoCtaCte
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
        Public Shared Function TraerTodosD() As List(Of TipoMovimientoCtaCte)
            Dim store As String = storeTraerTodosD
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of TipoMovimientoCtaCte)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosH() As List(Of TipoMovimientoCtaCte)
            Dim store As String = storeTraerTodosH
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of TipoMovimientoCtaCte)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As TipoMovimientoCtaCte
            Dim entidad As New TipoMovimientoCtaCte
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
    End Class ' DAL_TipoMovimientoCtaCte
End Namespace ' DataAccessLibrary
