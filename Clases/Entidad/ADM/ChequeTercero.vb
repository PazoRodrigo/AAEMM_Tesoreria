Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class ChequeTercero
        Inherits Cheque

        Private Shared _Todos As List(Of ChequeTercero)
        Public Shared Property Todos() As List(Of ChequeTercero)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_ChequeTercero.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of ChequeTercero))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property FechaVencimiento() As Date? = Nothing
        Public Property FechaDeposito() As Date? = Nothing
        Public Property IdEstado() As Enumeradores.EstadoChequeTerceros = Nothing
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaVencimiento() As Long
            Get
                Dim result As Long = 0
                If FechaVencimiento.HasValue Then
                    result = CLng(Year(FechaVencimiento.Value).ToString & Right("00" & Month(FechaVencimiento.Value).ToString, 2) & Right("00" & Day(FechaVencimiento.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaDeposito() As Long
            Get
                Dim result As Long = 0
                If FechaDeposito.HasValue Then
                    result = CLng(Year(FechaDeposito.Value).ToString & Right("00" & Month(FechaDeposito.Value).ToString, 2) & Right("00" & Day(FechaDeposito.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As ChequeTercero = TraerUno(id)
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
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_ChequeTercero)
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
                Case 11
                    IdEstado = Enumeradores.EstadoChequeTerceros.Recibido
                Case 12
                    IdEstado = Enumeradores.EstadoChequeTerceros.Debitado
                Case 13
                    IdEstado = Enumeradores.EstadoChequeTerceros.Rechazado
                Case 14
                    IdEstado = Enumeradores.EstadoChequeTerceros.Vencido
                Case 15
                    IdEstado = Enumeradores.EstadoChequeTerceros.Salvado
                Case 16
                    IdEstado = Enumeradores.EstadoChequeTerceros.Salvador
                Case 17
                    IdEstado = Enumeradores.EstadoChequeTerceros.DeBaja
                Case Else
                    Throw New Exception("Error")
            End Select
            Observaciones = DtODesde.Observaciones
            If DtODesde.FechaVencimiento > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaVencimiento.ToString, 2) + "/" + Left(Right(DtODesde.FechaVencimiento.ToString, 4), 2) + "/" + Left(DtODesde.FechaVencimiento.ToString, 4)
                FechaVencimiento = CDate(TempFecha)
            End If
            If DtODesde.FechaDeposito > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaDeposito.ToString, 2) + "/" + Left(Right(DtODesde.FechaDeposito.ToString, 4), 2) + "/" + Left(DtODesde.FechaDeposito.ToString, 4)
                FechaDeposito = CDate(TempFecha)
            End If
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As ChequeTercero
            Dim result As ChequeTercero = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of ChequeTercero)
            Return Todos
        End Function
        'Public Shared Function TraerUno(ByVal Id As Integer) As ChequeTercero
        '    Dim result As ChequeTercero= DAL_ChequeTercero.TraerUno(Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of ChequeTercero)
        '    Dim result As List(Of ChequeTercero) = DAL_ChequeTercero.TraerTodos()
        '    If result Is Nothing Then
        '        Throw New Exception("No existen resultados para la búsqueda")
        '    End If
        '    Return result
        'End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta(Desde As Long, Hasta As Long)
            ValidarAlta()
            DAL_ChequeTercero.Alta(Me, Desde, Hasta)
            Refresh()
        End Sub
        'Public Sub Baja()
        '    ValidarBaja()
        '    DAL_ChequeTercero.Modifica(Me)
        '    Refresh()
        'End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_ChequeTercero.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_ChequeTercero
            Dim result As New DTO.DTO_ChequeTercero With {
                .IdUsuarioAlta = IdUsuarioAlta,
                .IdUsuarioBaja = IdUsuarioBaja,
                .IdUsuarioModifica = IdUsuarioModifica,
                .FechaAlta = LngFechaAlta,
                .FechaBaja = LngFechaBaja,
                .IdEntidad = IdEntidad,
                .IdBanco = IdBanco,
                    .Numero = Numero,
                .Importe = Importe,
                .IdEstado = IdEstado,
                .Observaciones = Observaciones,
                .FechaVencimiento = LngFechaVencimiento,
                .FechaDeposito = LngFechaDeposito
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_ChequeTercero.TraerTodos
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
            '    ChequeTercero.Refresh()
            '    Dim result As ChequeTercero = Todos.Find(Function(x) x.Nombre.ToUpper = Nombre)
            '    If Not result Is Nothing Then
            '        Throw New Exception("El Nombre a ingresar ya existe")
            '    End If
        End Sub
#End Region
    End Class ' ChequeTercero
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_ChequeTercero
        Inherits DTO_Cheque


#Region " Atributos / Propiedades"
        Public Property FechaVencimiento() As Long = 0
        Public Property FechaDeposito() As Long = 0
#End Region
    End Class ' DTO_ChequeTercero
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_ChequeTercero

#Region " Stored "
        Const storeAlta As String = "ADM.p_ChequeTercero_Alta"
        Const storeBaja As String = "ADM.p_ChequeTercero_Baja"
        Const storeModifica As String = "ADM.p_ChequeTercero_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_ChequeTercero_TraerUnoXId"
        Const storeTraerTodos As String = "ADM.p_ChequeTercero_TraerTodos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(entidad As ChequeTercero, desde As Long, hasta As Long)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdBanco", entidad.IdBanco)
            pa.add("@desde", desde)
            pa.add("@hasta", hasta)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        'Public Shared Sub Baja(ByVal entidad As ChequeTercero)
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
        Public Shared Sub Modifica(ByVal entidad As ChequeTercero)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Importe", entidad.Importe)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@FechaDeposito", entidad.FechaDeposito)
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
        Public Shared Function TraerUno(ByVal id As Integer) As ChequeTercero
            Dim store As String = storeTraerUnoXId
            Dim result As New ChequeTercero
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
        Public Shared Function TraerTodos() As List(Of ChequeTercero)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of ChequeTercero)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As ChequeTercero
            Dim entidad As New ChequeTercero
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
                    entidad.IdEstado = CType(dr.Item("IdEstado"), Enumeradores.EstadoChequeTerceros)
                End If
            End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("FechaVencimiento") Then
                If dr.Item("FechaVencimiento") IsNot DBNull.Value Then
                    entidad.FechaVencimiento = CDate(dr.Item("FechaVencimiento"))
                End If
            End If
            If dr.Table.Columns.Contains("FechaDeposito") Then
                If dr.Item("FechaDeposito") IsNot DBNull.Value Then
                    entidad.FechaDeposito = CDate(dr.Item("FechaDeposito"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_ChequeTercero
End Namespace ' DataAccessLibrary