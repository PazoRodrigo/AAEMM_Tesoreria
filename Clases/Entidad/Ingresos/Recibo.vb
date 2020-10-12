Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports LUM
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Recibo
        Inherits DBE

        Public Structure StrBusquedaRecibo
            Public Desde As Long
            Public Hasta As Long
            Public CUIT As Long
            Public RazonSocial As String
            Public Importe As Decimal
            Public NroRecibo As Long
            Public NroCheque As Long
        End Structure

        Public Structure StrPago
            Public IdEntidad As Integer
            Public IdRecibo As Integer
            Public IdTipoPagoManual As Integer
            Public Importe As Decimal
            Public IdBanco As Integer
            Public Numero As Long
            Public Vencimiento As Long
        End Structure

        Public Structure StrPeriodo
            Public IdEntidad As Integer
            Public IdRecibo As Integer
            Public Periodo As String
            Public Importe As Decimal
        End Structure

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property CUIT() As Long = 0
        Public Property CodigoEntidad() As Integer = 0
        Public Property NroReciboInicio() As Integer = 0
        Public Property NroReciboFin() As Integer = 0
        Public Property Fecha() As Date? = Nothing
        Public Property ImporteTotal() As Decimal = 0
        Public Property ImporteEfectivo() As Decimal = 0
        Public Property IdEstado() As Integer = 0
        'Public Property IdEstado() As Enumeradores.EstadoRecibo = Enumeradores.EstadoRecibo.Activo
        Public Property Observaciones() As String = ""

#End Region
#Region " Lazy Load / ReadOnly"
        Public ReadOnly Property LngFecha() As Long
            Get
                Dim result As Long = 0
                If Fecha.HasValue Then
                    result = CLng(Year(Fecha.Value).ToString & Right("00" & Month(Fecha.Value).ToString, 2) & Right("00" & Day(Fecha.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Recibo = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            CUIT = objImportar.CUIT
            CodigoEntidad = objImportar.CodigoEntidad
            NroReciboInicio = objImportar.NroReciboInicio
            NroReciboFin = objImportar.NroReciboFin
            Fecha = objImportar.Fecha
            ImporteTotal = objImportar.ImporteTotal
            ImporteEfectivo = objImportar.ImporteEfectivo
            IdEstado = objImportar.IdEstado
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Recibo)
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
            CUIT = DtODesde.CUIT
            CodigoEntidad = DtODesde.CodigoEntidad
            NroReciboInicio = DtODesde.NroReciboInicio
            NroReciboFin = DtODesde.NroReciboFin
            If DtODesde.Fecha > 0 Then
                Dim TempFecha As String = Right(DtODesde.Fecha.ToString, 2) + "/" + Left(Right(DtODesde.Fecha.ToString, 4), 2) + "/" + Left(DtODesde.Fecha.ToString, 4)
                Fecha = CDate(TempFecha)
            End If
            ImporteTotal = DtODesde.ImporteTotal
            ImporteEfectivo = DtODesde.ImporteEfectivo
            IdEstado = CType(DtODesde.IdEstado, Enumeradores.EstadoRecibo)
            Observaciones = DtODesde.Observaciones
            'ListaPagos = DtODesde.ListaPagos
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Recibo
            Dim result As Recibo = DAL_Recibo.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Recibo)
            Dim result As List(Of Recibo) = DAL_Recibo.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen resultados para la búsqueda")
            End If
            Return result
        End Function
        ' Nuevos
        Public Shared Function TraerTodosXBusqueda(busqueda As StrBusquedaRecibo) As List(Of Recibo)
            Dim sqlQuery As String = "SELECT DISTINCT rec.Id, rec.CUIT, rec.fecha, rec.importeTotal, rec.nroReciboFin FROM Ingreso.Recibo rec "
            sqlQuery += "INNER JOIN ingreso.Recibo_Periodo rpe ON rpe.IdRecibo = rec.Id  "
            sqlQuery += "LEFT JOIN adm.chequetercero ch ON ch.IdRecibo = rec.Id "
            sqlQuery += "WHERE rec.FechaBaja IS NULL "
            If busqueda.Desde > 0 Then
                'SELECT * FROM Ingreso.Recibo rec LEFT JOIN ingreso.Recibo_Periodo rpe ON rpe.IdRecibo = rec.Id INNER JOIN adm.chequetercero ch ON ch.IdRecibo = rec.Id WHERE rec.FechaBaja IS NULL  ORDER BY fecha desc 
                Dim Fecha As String = Left(busqueda.Desde.ToString, 4) & "-" & Right("00" & Left(busqueda.Desde.ToString, 6), 2) & "-" & Right("00" & busqueda.Desde.ToString, 2)
                sqlQuery += " AND rec.Fecha >= '" + Fecha + "'"
            End If
            If busqueda.Hasta > 0 Then
                Dim Fecha As String = Left(busqueda.Hasta.ToString, 4) & "-" & Right("00" & Left(busqueda.Hasta.ToString, 6), 2) & "-" & Right("00" & busqueda.Hasta.ToString, 2)
                sqlQuery += "AND rec.Fecha <= '" + Fecha + "'"
            End If
            If busqueda.CUIT > 0 Then
                sqlQuery += "AND rec.CUIT = '" + busqueda.CUIT.ToString + "'"
            End If
            If busqueda.Importe > 0 Then
                sqlQuery += "AND rec.ImporteTotal = " + Replace(CStr(busqueda.Importe), ",", ".")
            End If
            If busqueda.NroRecibo > 0 Then
                sqlQuery += "AND rec.NroReciboFin LIKE = '" + busqueda.NroRecibo.ToString + "'"
            End If
            If busqueda.NroCheque > 0 Then
                sqlQuery += "AND ch.Numero = '" + busqueda.NroCheque.ToString + "'"
            End If
            sqlQuery += "  ORDER BY fecha desc "
            Return DAL_Recibo.TraerTodosXBusqueda(sqlQuery)
        End Function

#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            IdEstado = Enumeradores.EstadoRecibo.Activo
            DAL_Recibo.Alta(Me)
        End Sub
        Public Sub AltaPago(entidad As StrPago)
            If entidad.IdTipoPagoManual = Enumeradores.TipoPagoRecibo.Cheque Then
                Dim objRecibo As New Recibo(entidad.IdRecibo)
                Dim objCheque As New ChequeTercero
                objCheque.IdUsuarioAlta = objRecibo.IdUsuarioAlta
                objCheque.IdRecibo = objRecibo.IdEntidad
                objCheque.Importe = entidad.Importe
                objCheque.Numero = CLng(entidad.Numero)
                If entidad.Vencimiento > 0 Then
                    Dim TempVencimiento As String = Right(entidad.Vencimiento.ToString, 2) + "/" + Left(Right(entidad.Vencimiento.ToString, 4), 2) + "/" + Left(entidad.Vencimiento.ToString, 4)
                    objCheque.FechaVencimiento = CDate(TempVencimiento)
                End If
                objCheque.IdBanco = entidad.IdBanco
                objCheque.Alta()
            Else
                Throw New Exception("Error")
            End If
            DAL_Recibo.AltaPago(entidad)
        End Sub
        Public Sub AltaPeriodo(entidad As StrPeriodo)
            Dim strPeriodo As String = Right(entidad.Periodo.ToString, 4).ToString & Left(entidad.Periodo.ToString, 2).ToString
            entidad.Periodo = strPeriodo
            DAL_Recibo.AltaPeriodo(entidad)
            Dim objRecibo As New Recibo(entidad.IdRecibo)
            Dim objIngreso As New Ingreso()
            objIngreso.CUIT = objRecibo.CUIT
            objIngreso.CodigoEntidad = objRecibo.CodigoEntidad
            objIngreso.Periodo = CInt(entidad.Periodo)
            objIngreso.Importe = entidad.Importe
            objIngreso.NroRecibo = objRecibo.NroReciboFin
            objIngreso.IdOrigen = Enumeradores.TipoOrigen.CJ
            objIngreso.FechaPago = objRecibo.Fecha
            objIngreso.FechaAcreditacion = objRecibo.Fecha
            objIngreso.IdUsuarioAlta = objRecibo.IdUsuarioAlta
            objIngreso.Alta()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Recibo.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Recibo.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Recibo
            Dim result As New DTO.DTO_Recibo
            result.IdEntidad = IdEntidad
            result.CUIT = CUIT
            result.CodigoEntidad = CodigoEntidad
            result.NroReciboInicio = NroReciboInicio
            result.NroReciboFin = NroReciboFin
            result.Fecha = LngFecha
            result.ImporteTotal = ImporteTotal
            result.ImporteEfectivo = ImporteEfectivo
            result.IdEstado = IdEstado
            result.Observaciones = Observaciones
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
            'Dim cantidad As Integer = DAL_Recibo.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Recibo
End Namespace ' Entidad
Namespace DTO
    Public Class DTO_Recibo
        Inherits DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property CUIT() As Long = 0
        Public Property CodigoEntidad() As Integer = 0
        Public Property NroReciboInicio() As Integer = 0
        Public Property NroReciboFin() As Integer = 0
        Public Property Fecha() As Long = 0
        Public Property ImporteTotal() As Decimal = 0
        Public Property ImporteEfectivo() As Decimal = 0
        Public Property IdEstado() As Integer = 0
        'Public Property IdEstado() As Integer = Enumeradores.EstadoRecibo.Activo
        Public Property Observaciones() As String = ""
#End Region
    End Class ' DTO_Recibo
End Namespace ' DTO
Namespace DataAccessLibrary
    Public Class DAL_Recibo

#Region " Stored "
        Const storeAlta As String = "[INGRESO].[p_Recibo_Alta]"
        Const storeAltaPago As String = "[INGRESO].[p_Recibo_AltaPago]"
        Const storeAltaPeriodo As String = "[INGRESO].[p_Recibo_AltaPeriodo]"
        Const storeBaja As String = "p_Recibo_Baja"
        Const storeModifica As String = "p_Recibo_Modifica"
        Const storeTraerUnoXId As String = "[INGRESO].p_Recibo_TraerUnoXId"
        Const storeTraerTodos As String = "INGRESO.p_Recibo_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Recibo_TraerTodosActivos"
        Const storeTraerTodosXBusqueda As String = "p_TraerXBusquedaLibre"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Recibo)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@CodigoEntidad", entidad.CodigoEntidad)
            pa.add("@NroReciboInicio", entidad.NroReciboInicio)
            pa.add("@NroReciboFin", entidad.NroReciboFin)
            pa.add("@Fecha", entidad.Fecha)
            pa.add("@ImporteTotal", entidad.ImporteTotal)
            pa.add("@ImporteEfectivo", entidad.ImporteEfectivo)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Structure StrPago
            Public IdEntidad As Integer
            Public IdRecibo As Integer
            Public IdTipoPagoManual As Integer
            Public Importe As Decimal
            Public NroCheque As String
        End Structure
        Friend Shared Sub AltaPeriodo(entidad As Recibo.StrPeriodo)
            Dim store As String = storeAltaPeriodo
            Dim pa As New parametrosArray
            pa.add("@IdRecibo", entidad.IdRecibo)
            pa.add("@Periodo", entidad.Periodo)
            pa.add("@Importe", entidad.Importe)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Sub AltaPago(entidad As Recibo.StrPago)
            Dim store As String = storeAltaPago
            Dim pa As New parametrosArray
            pa.add("@IdRecibo", entidad.IdRecibo)
            pa.add("@IdTipoPagoManual", entidad.IdTipoPagoManual)
            pa.add("@Importe", entidad.Importe)
            pa.add("@IdBanco", entidad.IdBanco)
            pa.add("@Numero", entidad.Numero)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Recibo)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Recibo)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            'pa.add("@CUIT", entidad.CUIT)
            'pa.add("@NroReciboInicio", entidad.NroReciboInicio)
            'pa.add("@NroReciboFin", entidad.NroReciboFin)
            'pa.add("@Fecha", entidad.Fecha)
            'pa.add("@Importe", entidad.Importe)
            'pa.add("@IdEstado", entidad.IdEstado)
            'pa.add("@Observaciones", entidad.Observaciones)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Recibo
            Dim store As String = storeTraerUnoXId
            Dim result As New Recibo
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
        Public Shared Function TraerTodos() As List(Of Recibo)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Recibo)
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
        Friend Shared Function TraerTodosXBusqueda(sqlQuery As String) As List(Of Recibo)
            Dim store As String = storeTraerTodosXBusqueda
            Dim listaResult As New List(Of Recibo)
            Dim pa As New parametrosArray
            pa.add("@sqlQuery", sqlQuery)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Recibo
            Dim entidad As New Recibo
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
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("CodigoEntidad") Then
                If dr.Item("CodigoEntidad") IsNot DBNull.Value Then
                    entidad.CodigoEntidad = CInt(dr.Item("CodigoEntidad"))
                End If
            End If
            If dr.Table.Columns.Contains("NroReciboInicio") Then
                If dr.Item("NroReciboInicio") IsNot DBNull.Value Then
                    entidad.NroReciboInicio = CInt(dr.Item("NroReciboInicio"))
                End If
            End If
            If dr.Table.Columns.Contains("NroReciboFin") Then
                If dr.Item("NroReciboFin") IsNot DBNull.Value Then
                    entidad.NroReciboFin = CInt(dr.Item("NroReciboFin"))
                End If
            End If
            If dr.Table.Columns.Contains("Fecha") Then
                If dr.Item("Fecha") IsNot DBNull.Value Then
                    entidad.Fecha = CDate(dr.Item("Fecha"))
                End If
            End If
            If dr.Table.Columns.Contains("ImporteTotal") Then
                If dr.Item("ImporteTotal") IsNot DBNull.Value Then
                    entidad.ImporteTotal = CDec(dr.Item("ImporteTotal"))
                End If
            End If
            If dr.Table.Columns.Contains("ImporteEfectivo") Then
                If dr.Item("ImporteEfectivo") IsNot DBNull.Value Then
                    entidad.ImporteEfectivo = CDec(dr.Item("ImporteEfectivo"))
                End If
            End If
            If dr.Table.Columns.Contains("IdEstado") Then
                If dr.Item("IdEstado") IsNot DBNull.Value Then
                    entidad.IdEstado = CType(dr.Item("IdEstado"), Enumeradores.EstadoRecibo)
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Recibo
End Namespace ' DataAccessLibrary