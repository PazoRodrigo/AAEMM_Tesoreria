Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class ChequePropio
        Inherits Cheque

        Private Shared _Todos As List(Of ChequePropio)
        Public Shared Property Todos() As List(Of ChequePropio)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_ChequePropio.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of ChequePropio))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property FechaEmision() As Date? = Nothing
        Public Property FechaDebito() As Date? = Nothing
        Public Property IdEstado() As Enumeradores.EstadoChequePropios = Nothing
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaEmision() As Long
            Get
                Dim result As Long = 0
                If FechaEmision.HasValue Then
                    result = CLng(Year(FechaEmision.Value).ToString & Right("00" & Month(FechaEmision.Value).ToString, 2) & Right("00" & Day(FechaEmision.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaDebito() As Long
            Get
                Dim result As Long = 0
                If FechaDebito.HasValue Then
                    result = CLng(Year(FechaDebito.Value).ToString & Right("00" & Month(FechaDebito.Value).ToString, 2) & Right("00" & Day(FechaDebito.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property Estado() As String
            Get
                Dim result As String = ""
                If IdEstado <> Nothing Then
                    Select Case IdEstado
                        Case Enumeradores.EstadoChequePropios.Emitido
                            result = "Emitido"
                        Case Enumeradores.EstadoChequePropios.Vigente
                            result = "Vigente"
                        Case Enumeradores.EstadoChequePropios.Suspendido
                            result = "Suspendido"
                        Case Enumeradores.EstadoChequePropios.Anulado
                            result = "Anulado"
                        Case Else
                    End Select
                    Return result
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As ChequePropio = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdBanco = objImportar.IdBanco
            Numero = objImportar.Numero
            Importe = objImportar.Importe
            IdEstado = objImportar.IdEstado
            Observaciones = objImportar.Observaciones
            FechaEmision = objImportar.FechaEmision
            FechaDebito = objImportar.FechaDebito
        End Sub

        Sub New(ByVal DtODesde As DTO.DTO_ChequePropio)
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
            IdBanco = DtODesde.IdBanco
            Numero = DtODesde.Numero
            Importe = DtODesde.Importe
            Select Case DtODesde.IdEstado
                Case 1
                    IdEstado = Enumeradores.EstadoChequePropios.Emitido
                Case 2
                    IdEstado = Enumeradores.EstadoChequePropios.Vigente
                Case 3
                    IdEstado = Enumeradores.EstadoChequePropios.Suspendido
                Case 4
                    IdEstado = Enumeradores.EstadoChequePropios.Anulado
                Case Else
                    Throw New Exception("Error")
            End Select
            Observaciones = DtODesde.Observaciones
            If DtODesde.FechaEmision > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaEmision.ToString, 2) + "/" + Left(Right(DtODesde.FechaEmision.ToString, 4), 2) + "/" + Left(DtODesde.FechaEmision.ToString, 4)
                FechaEmision = CDate(TempFecha)
            End If
            If DtODesde.FechaDebito > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaDebito.ToString, 2) + "/" + Left(Right(DtODesde.FechaDebito.ToString, 4), 2) + "/" + Left(DtODesde.FechaDebito.ToString, 4)
                FechaDebito = CDate(TempFecha)
            End If
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerChequeProximo() As ChequePropio
            Dim result As ChequePropio = DAL_ChequePropio.TraerChequeProximo()
            If result Is Nothing Then
                Throw New Exception("No existen un nuvo cheque Vigente. Cree una nueva chequera.")
            End If
            Return result
        End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As ChequePropio
            Dim result As ChequePropio = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of ChequePropio)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As ChequePropio
        '    Dim result As ChequePropio= DAL_ChequePropio.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of ChequePropio)
        '    Dim result As List(Of ChequePropio) = DAL_ChequePropio.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Shared Function AltaChequera(IdUsuario As Integer, Desde As Long, Hasta As Long) As List(Of ChequePropio)
            Dim ListaResult As New List(Of ChequePropio)
            Dim obj As ChequePropio
            For index = Desde To Hasta
                obj = New ChequePropio With {
                    .IdUsuarioAlta = IdUsuario,
                    .Numero = Desde
                }
                obj.Alta()
                Desde += 1
                If obj.IdEntidad > 0 Then
                    ListaResult.Add(obj)
                End If
            Next
            Return ListaResult
        End Function
        Public Sub Alta()
            ValidarAlta()
            Me.IdEstado = Enumeradores.EstadoChequePropios.Vigente
            DAL_ChequePropio.Alta(Me)
            Refresh()
        End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_ChequePropio.Modifica(Me)
        '    Refresh()
        'End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_ChequePropio.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_ChequePropio
            Dim result As New DTO.DTO_ChequePropio With {
                .IdUsuarioAlta = IdUsuarioAlta,
                .IdUsuarioBaja = IdUsuarioBaja,
                .IdUsuarioModifica = IdUsuarioModifica,
                .FechaAlta = LngFechaAlta,
                .FechaBaja = LngFechaBaja,
                .IdEntidad = IdEntidad,
                .IdBanco = IdBanco,
                .Numero = Numero,
                .Importe = Importe,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado,
                .Estado = Estado,
                .FechaEmision = LngFechaEmision,
                .FechaDebito = LngFechaDebito
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_ChequePropio.TraerTodos
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
        Private Sub ValidarCaracteres()
            Dim sError As String = ""
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            '    ChequePropio.Refresh()
            '    Dim result As ChequePropio = Todos.Find(Function(x) x.Nombre.ToUpper = Nombre)
            '    If Not result Is Nothing Then
            '        Throw New Exception("El Nombre a ingresar ya existe")
            '    End If
        End Sub
#End Region
    End Class ' ChequePropio
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_ChequePropio
        Inherits DTO_Cheque


#Region " Atributos / Propiedades"
        Public Property FechaEmision() As Long = 0
        Public Property FechaDebito() As Long = 0
#End Region
    End Class ' DTO_ChequePropio
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_ChequePropio

#Region " Stored "
        Const storeAlta As String = "ADM.p_ChequePropio_Alta"
        Const storeBaja As String = "ADM.p_ChequePropio_Baja"
        Const storeModifica As String = "ADM.p_ChequePropio_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_ChequePropio_TraerUnoXId"
        Const storeTraerChequeProximo As String = "ADM.p_ChequePropio_TraerChequeProximo"
        Const storeTraerTodos As String = "ADM.p_ChequePropio_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(entidad As ChequePropio)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdBanco", entidad.IdBanco)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Numero", entidad.Numero)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        'Public Shared Sub Baja(ByVal entidad As ChequePropio)
        '    Dim store As String = storeBaja
        '    Dim pa As New parametrosArray
        '    pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
        '    pa.add("@id", entidad.IdEntidad)
        '    pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                entidad.IdEntidad = CInt(dt.Rows(0)(0))
        '            End If
        '        End If
        '    End Using
        'End Sub
        Public Shared Sub Modifica(ByVal entidad As ChequePropio)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Importe", entidad.Importe)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@FechaDebito", entidad.FechaDebito)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As ChequePropio
            Dim store As String = storeTraerUnoXId
            Dim result As New ChequePropio
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
        Public Shared Function TraerTodos() As List(Of ChequePropio)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of ChequePropio)
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
        Public Shared Function TraerChequeProximo() As ChequePropio
            Dim store As String = storeTraerChequeProximo
            Dim result As New ChequePropio
            Dim pa As New parametrosArray
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
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As ChequePropio
            Dim entidad As New ChequePropio
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
            If dr.Table.Columns.Contains("IdBanco") Then
                If dr.Item("IdBanco") IsNot DBNull.Value Then
                    entidad.IdBanco = CInt(dr.Item("IdBanco"))
                End If
            End If
            If dr.Table.Columns.Contains("Numero") Then
                If dr.Item("Numero") IsNot DBNull.Value Then
                    entidad.Numero = CLng(dr.Item("Numero"))
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            If dr.Table.Columns.Contains("IdEstado") Then
                If dr.Item("IdEstado") IsNot DBNull.Value Then
                    entidad.IdEstado = CType(dr.Item("IdEstado"), Enumeradores.EstadoChequePropios)
                End If
            End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("FechaEmision") Then
                If dr.Item("FechaEmision") IsNot DBNull.Value Then
                    entidad.FechaEmision = CDate(dr.Item("FechaEmision"))
                End If
            End If
            If dr.Table.Columns.Contains("FechaDebito") Then
                If dr.Item("FechaDebito") IsNot DBNull.Value Then
                    entidad.FechaDebito = CDate(dr.Item("FechaDebito"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_ChequePropio
End Namespace ' DataAccessLibrary