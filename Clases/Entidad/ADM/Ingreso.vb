Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Ingreso
        Inherits DBE

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdEstado() As Char = CChar("")
        Public Property IdCentroCosto() As Integer = 0
        Public Property CodigoEntidad() As Long = 0
        Public Property CUIT() As Long = 0
        Public Property Periodo() As Integer = 0
        Public Property NroCheche() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property IdOrigen() As Integer = 0
        Public Property NroRecibo() As Integer = 0
        Public Property NombreArchivo() As String = ""
        Public Property FechaPago() As Date? = Nothing
        Public Property FechaAcreditacion() As Date? = Nothing
        Public Property FechaPagoFacil() As Date? = Nothing
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaPago() As Long
            Get
                Dim result As Long = 0
                If FechaPago.HasValue Then
                    result = CLng(Year(FechaPago.Value).ToString & Right("00" & Month(FechaPago.Value).ToString, 2) & Right("00" & Day(FechaPago.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaAcreditacion() As Long
            Get
                Dim result As Long = 0
                If FechaAcreditacion.HasValue Then
                    result = CLng(Year(FechaAcreditacion.Value).ToString & Right("00" & Month(FechaAcreditacion.Value).ToString, 2) & Right("00" & Day(FechaAcreditacion.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaPagoFacil() As Long
            Get
                Dim result As Long = 0
                If FechaPagoFacil.HasValue Then
                    result = CLng(Year(FechaPagoFacil.Value).ToString & Right("00" & Month(FechaPagoFacil.Value).ToString, 2) & Right("00" & Day(FechaPagoFacil.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Ingreso = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdCentroCosto = objImportar.IdCentroCosto
            CodigoEntidad = objImportar.CodigoEntidad
            CUIT = objImportar.CUIT
            Periodo = objImportar.Periodo
            NroCheche = objImportar.NroCheche
            IdEstado = objImportar.IdEstado
            Importe = objImportar.Importe
            IdOrigen = objImportar.IdOrigen
            NroRecibo = objImportar.NroRecibo
            NombreArchivo = objImportar.NombreArchivo
            FechaPago = objImportar.FechaPago
            FechaAcreditacion = objImportar.FechaAcreditacion
            FechaPagoFacil = objImportar.FechaPagoFacil
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Ingreso)
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
            IdCentroCosto = DtODesde.IdCentroCosto
            CodigoEntidad = DtODesde.CodigoEntidad
            CUIT = DtODesde.CUIT
            Periodo = DtODesde.Periodo
            NroCheche = DtODesde.NroCheche
            IdEstado = DtODesde.IdEstado
            Importe = DtODesde.Importe
            IdOrigen = DtODesde.IdOrigen
            NroRecibo = DtODesde.NroRecibo
            NombreArchivo = DtODesde.NombreArchivo
            If DtODesde.FechaPago > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaPago.ToString, 2) + "/" + Left(Right(DtODesde.FechaPago.ToString, 4), 2) + "/" + Left(DtODesde.FechaPago.ToString, 4)
                FechaPago = CDate(TempFecha)
            End If
            If DtODesde.FechaAcreditacion > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaAcreditacion.ToString, 2) + "/" + Left(Right(DtODesde.FechaAcreditacion.ToString, 4), 2) + "/" + Left(DtODesde.FechaAcreditacion.ToString, 4)
                FechaAcreditacion = CDate(TempFecha)
            End If
            If DtODesde.FechaPagoFacil > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaPagoFacil.ToString, 2) + "/" + Left(Right(DtODesde.FechaPagoFacil.ToString, 4), 2) + "/" + Left(DtODesde.FechaPagoFacil.ToString, 4)
                FechaPagoFacil = CDate(TempFecha)
            End If
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Ingreso
            Dim result As Ingreso = DAL_Ingreso.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCentroCosto(IdCentroCosto As Integer) As List(Of Ingreso)
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXCentroCosto(IdCentroCosto)
            If result.Count = 0 Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCUIT(CUIT As Long) As List(Of Ingreso)
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXCUIT(CUIT)
            If result.Count = 0 Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXFechasXAcreditacion(Desde As Date, Hasta As Date) As List(Of Ingreso)
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXFechasXAcreditacion(Desde, Hasta)
            If result.Count = 0 Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXFechasXPago(Desde As Date, Hasta As Date) As List(Of Ingreso)
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXFechasXPago(Desde, Hasta)
            If result.Count = 0 Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXPeriodo(Periodo As Integer) As List(Of Ingreso)
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXPeriodo(Periodo)
            If result.Count = 0 Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Ingreso.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Ingreso.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Ingreso.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Ingreso
            Dim result As New DTO.DTO_Ingreso With {
                .IdEntidad = IdEntidad,
                .IdEstado = IdEstado,
                .IdCentroCosto = IdCentroCosto,
                .CodigoEntidad = CodigoEntidad,
                .CUIT = CUIT,
                .Periodo = Periodo,
                .NroCheche = NroCheche,
                .Importe = Importe,
                .IdOrigen = IdOrigen,
                .NroRecibo = NroRecibo,
                .NombreArchivo = NombreArchivo,
                .FechaPago = LngFechaPago,
                .FechaAcreditacion = LngFechaAcreditacion,
                .FechaPagoFacil = LngFechaPagoFacil
            }
            Return result
        End Function
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

        End Sub
#End Region
    End Class ' Ingreso
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Ingreso
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdEstado() As Char = CChar("")
        Public Property IdCentroCosto() As Integer = 0
        Public Property CodigoEntidad() As Long = 0
        Public Property CUIT() As Long = 0
        Public Property Periodo() As Integer = 0
        Public Property NroCheche() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property IdOrigen() As Integer = 0
        Public Property NroRecibo() As Integer = 0
        Public Property NombreArchivo() As String = ""
        Public Property FechaPago() As Long = 0
        Public Property FechaAcreditacion() As Long = 0
        Public Property FechaPagoFacil() As Long = 0
#End Region
    End Class ' DTO_Ingreso
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Ingreso

#Region " Stored "
        Const storeAlta As String = "ADM.p_Ingreso_Alta"
        Const storeBaja As String = "ADM.p_Ingreso_Baja"
        Const storeModifica As String = "ADM.p_Ingreso_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_Ingreso_TraerUnoXId"
        Const storeTraerTodosXCentroCosto As String = "ADM.p_Ingreso_TraerTodosXCentroCosto"
        Const storeTraerTodosXCUIT As String = "ADM.p_Ingreso_TraerTodosXCUIT"
        Const storeTraerTodosXFechasXAcreditacion As String = "ADM.p_Ingreso_TraerTodosXFechasXAcreditacion"
        Const storeTraerTodosXFechasXPago As String = "ADM.p_Ingreso_TraerTodosXFechasXPago"
        Const storeTraerTodosXPeriodo As String = "ADM.p_Ingreso_TraerTodosXPeriodo"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Ingreso)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            'pa.add("@Nombre", entidad.Nombre)
            'pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Ingreso)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            'pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Ingreso)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            'pa.add("@Nombre", entidad.Nombre)
            'pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        '' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Ingreso
            Dim store As String = storeTraerUnoXId
            Dim result As New Ingreso
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
        Public Shared Function TraerTodosXCentroCosto(ByVal IdCentroCosto As Integer) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXCentroCosto
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@IdCentroCosto", IdCentroCosto)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXCUIT(ByVal CUIT As Long) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXCUIT
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@CUIT", CUIT)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXFechasXAcreditacion(ByVal Desde As Date, ByVal Hasta As Date) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXFechasXAcreditacion
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@Desde", Desde)
            pa.add("@Hasta", Hasta)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXFechasXPago(ByVal Desde As Date, ByVal Hasta As Date) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXFechasXPago
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@Desde", Desde)
            pa.add("@Hasta", Hasta)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXPeriodo(ByVal Periodo As Integer) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXPeriodo
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@Periodo", Periodo)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Ingreso
            Dim entidad As New Ingreso
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
            If dr.Table.Columns.Contains("id_movimiento") Then
                If dr.Item("id_movimiento") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id_movimiento"))
                End If
            End If
            If dr.Table.Columns.Contains("sec") Then
                If dr.Item("sec") IsNot DBNull.Value Then
                    entidad.IdCentroCosto = CInt(dr.Item("sec"))
                End If
            End If
            If dr.Table.Columns.Contains("cod_ent") Then
                If dr.Item("cod_ent") IsNot DBNull.Value Then
                    entidad.CodigoEntidad = CLng(dr.Item("cod_ent"))
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("Periodo") Then
                If dr.Item("Periodo") IsNot DBNull.Value Then
                    If dr.Item("Periodo").ToString.Length = 7 Then
                        Dim temp As String = Replace(dr.Item("Periodo").ToString, "/", "")
                        temp = Right(temp, 4) & Left(temp, 2)
                        If IsNumeric(temp) Then
                            entidad.Periodo = CInt(temp)
                        End If
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("cheque") Then
                If dr.Item("cheque") IsNot DBNull.Value Then
                    entidad.NroCheche = CLng(dr.Item("cheque"))
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            If dr.Table.Columns.Contains("origen") Then
                If dr.Item("origen") IsNot DBNull.Value Then
                    entidad.IdOrigen = CInt(dr.Item("origen"))
                End If
            End If
            If dr.Table.Columns.Contains("nro_rec") Then
                If dr.Item("nro_rec") IsNot DBNull.Value Then
                    If IsNumeric(dr.Item("nro_rec").ToString) Then
                        entidad.NroRecibo = CInt(dr.Item("nro_rec"))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("estado") Then
                If dr.Item("estado") IsNot vbNullChar AndAlso dr.Item("estado") IsNot DBNull.Value Then
                    If dr.Item("estado").ToString = " " Then
                        entidad.IdEstado = CChar("A")
                    Else
                        entidad.IdEstado = CChar(dr.Item("estado"))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("fec_pago") Then
                If dr.Item("fec_pago") IsNot DBNull.Value Then
                    entidad.FechaPago = CDate(dr.Item("fec_pago"))
                End If
            End If
            If dr.Table.Columns.Contains("fec_acr") Then
                If dr.Item("fec_acr") IsNot DBNull.Value Then
                    entidad.FechaAcreditacion = CDate(dr.Item("fec_acr"))
                End If
            End If
            If dr.Table.Columns.Contains("fec_cpf") Then
                If dr.Item("fec_cpf") IsNot DBNull.Value Then
                    entidad.FechaPagoFacil = CDate(dr.Item("fec_cpf"))
                End If
            End If
            If dr.Table.Columns.Contains("NombreArchivo") Then
                If dr.Item("NombreArchivo") IsNot DBNull.Value Then
                    entidad.NombreArchivo = dr.Item("NombreArchivo").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Ingreso
End Namespace ' DataAccessLibrary