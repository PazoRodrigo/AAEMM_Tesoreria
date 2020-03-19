Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Comprobante
        Inherits DBE

        Private Shared _Todos As List(Of Comprobante)
        Public Shared Property Todos(IdGasto As Integer) As List(Of Comprobante)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Comprobante.TraerTodosXGasto(IdGasto)
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Comprobante))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdGasto() As Integer = 0
        Public Property IdOriginario() As Integer = 0
        Public Property IdProveedor() As Integer = 0
        Public Property IdCentroCosto() As Integer = 0
        Public Property IdCuenta() As Integer = 0
        Public Property IdTipoPago() As Integer = 0
        Public Property FechaGasto() As Date? = Nothing
        Public Property FechaPago() As Date? = Nothing
        Public Property NroComprobante() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaGasto() As Long
            Get
                Dim result As Long = 0
                If FechaGasto.HasValue Then
                    result = CLng(Year(FechaGasto.Value).ToString & Right("00" & Month(FechaGasto.Value).ToString, 2) & Right("00" & Day(FechaGasto.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaPago() As Long
            Get
                Dim result As Long = 0
                If FechaPago.HasValue Then
                    result = CLng(Year(FechaPago.Value).ToString & Right("00" & Month(FechaPago.Value).ToString, 2) & Right("00" & Day(FechaPago.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Comprobante = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdGasto = objImportar.IdGasto
            IdOriginario = objImportar.IdOriginario
            IdProveedor = objImportar.IdProveedor
            IdCentroCosto = objImportar.IdCentroCosto
            IdCuenta = objImportar.IdCuenta
            IdTipoPago = objImportar.IdTipoPago
            FechaGasto = objImportar.FechaGasto
            FechaPago = objImportar.FechaPago
            NroComprobante = objImportar.NroComprobante
            Importe = objImportar.Importe
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Comprobante)
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
            IdGasto = DtODesde.IdGasto
            IdOriginario = DtODesde.IdOriginario
            IdProveedor = DtODesde.IdProveedor
            IdCentroCosto = DtODesde.IdCentroCosto
            IdCuenta = DtODesde.IdCuenta
            IdTipoPago = DtODesde.IdTipoPago
            If DtODesde.FechaGasto > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaGasto.ToString, 2) + "/" + Left(Right(DtODesde.FechaGasto.ToString, 4), 2) + "/" + Left(DtODesde.FechaGasto.ToString, 4)
                FechaGasto = CDate(TempFecha)
            End If
            If DtODesde.FechaPago > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaPago.ToString, 2) + "/" + Left(Right(DtODesde.FechaPago.ToString, 4), 2) + "/" + Left(DtODesde.FechaPago.ToString, 4)
                FechaPago = CDate(TempFecha)
            End If
            NroComprobante = DtODesde.NroComprobante
            Importe = DtODesde.Importe
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        'Public Shared Function TraerUno(ByVal Id As Integer) As Comprobante
        '    Dim result As Comprobante = Todos.Find(Function(x) x.IdEntidad = Id)
        '    If result Is Nothing Then
        '        Throw New Exception("No existen Comprobantes para la búsqueda")
        '    End If
        '    Return result
        'End Function
        'Public Shared Function TraerTodos() As List(Of Comprobante)
        '    Return Todos
        'End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Comprobante
            Dim result As Comprobante = DAL_Comprobante.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Comprobantes para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXGasto(IdGasto As Integer) As List(Of Comprobante)
            Return DAL_Comprobante.TraerTodosXGasto(IdGasto)
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Comprobante.Alta(Me)
            Refresh(IdGasto)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Comprobante.Baja(Me)
            Refresh(IdGasto)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Comprobante.Modifica(Me)
            Refresh(IdGasto)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Comprobante
            Dim result As New DTO.DTO_Comprobante With {
                .IdEntidad = IdEntidad,
                .IdGasto = IdGasto,
                .IdOriginario = IdOriginario,
                .IdProveedor = IdProveedor,
                .IdCentroCosto = IdCentroCosto,
                .IdCuenta = IdCuenta,
                .IdTipoPago = IdTipoPago,
                .FechaGasto = LngFechaGasto,
                .FechaPago = LngFechaPago,
                .NroComprobante = NroComprobante,
                .Importe = Importe,
                .Observaciones = Observaciones
            }
            Return result
        End Function
        Public Shared Sub Refresh(IdGasto As Integer)
            _Todos = DAL_Comprobante.TraerTodosXGasto(IdGasto)
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
            'Comprobante.Refresh(IdGasto)
            'Dim result As Comprobante = Todos.Find(Function(x) x.Observaciones.ToUpper = Observaciones)
            'If Not result Is Nothing Then
            '    Throw New Exception("El Observaciones a ingresar ya existe")
            'End If
        End Sub
#End Region
    End Class ' Comprobante
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Comprobante
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdGasto() As Integer = 0
        Public Property IdOriginario() As Integer = 0
        Public Property IdProveedor() As Integer = 0
        Public Property IdCentroCosto() As Integer = 0
        Public Property IdCuenta() As Integer = 0
        Public Property IdTipoPago() As Integer = 0
        Public Property FechaGasto() As Long = 0
        Public Property FechaPago() As Long = 0
        Public Property NroComprobante() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_Comprobante
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Comprobante

#Region " Stored "
        Const storeAlta As String = "ADM.p_Comprobante_Alta"
        Const storeBaja As String = "ADM.p_Comprobante_Baja"
        Const storeModifica As String = "ADM.p_Comprobante_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_Comprobante_TraerUnoXId"
        Const storeTraerTodosXGasto As String = "ADM.p_Comprobante_TraerTodosXGasto"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Comprobante)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdGasto", entidad.IdGasto)
            pa.add("@IdOriginario", entidad.IdOriginario)
            pa.add("@IdProveedor", entidad.IdProveedor)
            pa.add("@IdCentroCosto", entidad.IdCentroCosto)
            pa.add("@IdCuenta", entidad.IdCuenta)
            pa.add("@IdTipoPago", entidad.IdTipoPago)
            pa.add("@FechaGasto", entidad.FechaGasto)
            pa.add("@FechaPago", entidad.FechaPago)
            pa.add("@NroComprobante", entidad.NroComprobante)
            pa.add("@Importe", entidad.Importe)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Comprobante)
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
        Public Shared Sub Modifica(ByVal entidad As Comprobante)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdGasto", entidad.IdGasto)
            pa.add("@IdOriginario", entidad.IdOriginario)
            pa.add("@IdProveedor", entidad.IdProveedor)
            pa.add("@IdCentroCosto", entidad.IdCentroCosto)
            pa.add("@IdCuenta", entidad.IdCuenta)
            pa.add("@IdTipoPago", entidad.IdTipoPago)
            pa.add("@FechaGasto", entidad.FechaGasto)
            pa.add("@FechaPago", entidad.FechaPago)
            pa.add("@NroComprobante", entidad.NroComprobante)
            pa.add("@Importe", entidad.Importe)
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
        Public Shared Function TraerUno(ByVal id As Integer) As Comprobante
            Dim store As String = storeTraerUnoXId
            Dim result As New Comprobante
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
        Public Shared Function TraerTodosXGasto(IdGasto As Integer) As List(Of Comprobante)
            Dim store As String = storeTraerTodosXGasto
            Dim pa As New parametrosArray
            pa.add("@IdGasto", IdGasto)
            Dim listaResult As New List(Of Comprobante)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Comprobante
            Dim entidad As New Comprobante
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
            If dr.Table.Columns.Contains("IdGasto") Then
                If dr.Item("IdGasto") IsNot DBNull.Value Then
                    entidad.IdGasto = CInt(dr.Item("IdGasto"))
                End If
            End If
            If dr.Table.Columns.Contains("IdOriginario") Then
                If dr.Item("IdOriginario") IsNot DBNull.Value Then
                    entidad.IdOriginario = CInt(dr.Item("IdOriginario"))
                End If
            End If
            If dr.Table.Columns.Contains("IdProveedor") Then
                If dr.Item("IdProveedor") IsNot DBNull.Value Then
                    entidad.IdProveedor = CInt(dr.Item("IdProveedor"))
                End If
            End If
            If dr.Table.Columns.Contains("IdProveedor") Then
                If dr.Item("IdProveedor") IsNot DBNull.Value Then
                    entidad.IdProveedor = CInt(dr.Item("IdProveedor"))
                End If
            End If
            If dr.Table.Columns.Contains("IdCentroCosto") Then
                If dr.Item("IdCentroCosto") IsNot DBNull.Value Then
                    entidad.IdCentroCosto = CInt(dr.Item("IdCentroCosto"))
                End If
            End If
            If dr.Table.Columns.Contains("IdCuenta") Then
                If dr.Item("IdCuenta") IsNot DBNull.Value Then
                    entidad.IdCuenta = CInt(dr.Item("IdCuenta"))
                End If
            End If
            If dr.Table.Columns.Contains("IdTipoPago") Then
                If dr.Item("IdTipoPago") IsNot DBNull.Value Then
                    entidad.IdTipoPago = CInt(dr.Item("IdTipoPago"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaGasto") Then
                If dr.Item("fechaGasto") IsNot DBNull.Value Then
                    entidad.FechaGasto = CDate(dr.Item("fechaGasto"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaPago") Then
                If dr.Item("fechaPago") IsNot DBNull.Value Then
                    entidad.FechaPago = CDate(dr.Item("fechaPago"))
                End If
            End If
            If dr.Table.Columns.Contains("NroComprobante") Then
                If dr.Item("IdTiNroComprobantepoPago") IsNot DBNull.Value Then
                    entidad.NroComprobante = CLng(dr.Item("NroComprobante"))
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If

            'Public Property () As Integer = 0
            'Public Property () As Integer = 0
            'Public Property IdProveedor() As Integer = 0
            'Public Property IdCentroCosto() As Integer = 0
            'Public Property IdCuenta() As Integer = 0
            'Public Property IdTipoPago() As Integer = 0
            'Public Property FechaGasto() As Long = 0
            'Public Property FechaPago() As Long = 0
            'Public Property NroComprobante() As Long = 0
            'Public Property Importe() As Decimal = 0
            Return entidad
        End Function
#End Region
    End Class ' DAL_Comprobante
End Namespace ' DataAccessLibrary