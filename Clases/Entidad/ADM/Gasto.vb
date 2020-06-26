Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Gasto
        Inherits DBE

        Public Structure StrBusquedaGasto
            Public Desde As Long
            Public Hasta As Long
            Public Estados As String
        End Structure
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdEstado() As Enumeradores.EstadoGasto = Enumeradores.EstadoGasto.Abierto
        Public Property Importe() As Decimal = 0
        Public Property CantidadComprobantes() As Integer = 0
        Public Property FechaGasto() As Date? = Nothing
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
        Public ReadOnly Property Estado() As String
            Get
                Dim result As String = ""
                Select Case IdEstado
                    Case Enumeradores.EstadoGasto.Abierto
                        result = "Abierto"
                    Case Enumeradores.EstadoGasto.Cerrado
                        result = "Cerrado"
                    Case Enumeradores.EstadoGasto.Anulado
                        result = "Anulado"
                    Case Else
                        result = "Sin Estado"
                End Select
                Return result
            End Get
        End Property
        Private Property _listaComprobantes As List(Of Comprobante)
        Public ReadOnly Property ListaComprobantes() As List(Of Comprobante)
            Get
                If _listaComprobantes Is Nothing Then
                    _listaComprobantes = Comprobante.TraerTodosXGasto(IdEntidad)
                End If
                Return _listaComprobantes
            End Get
        End Property

#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Gasto = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Gasto)
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
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Gasto
            Dim result As Gasto = DAL_Gasto.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Gastos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Gasto)
            Dim result As List(Of Gasto) = DAL_Gasto.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen Gastos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosUltimos5() As List(Of Gasto)
            'Dim result As List(Of Gasto) = DAL_Gasto.TraerTodosUltimos5()
            'If result Is Nothing Then
            '    Throw New Exception("No existen Gastos para la búsqueda")
            'End If
            Return Nothing
        End Function

        Public Shared Function TraerTodosXBusqueda(busqueda As StrBusquedaGasto) As List(Of Gasto)
            Dim existeParametro As Boolean = False
            Dim resultStr As String = ""
            If busqueda.Desde > 0 Then
                Dim Fecha As String = Left(busqueda.Desde.ToString, 4) & "-" & Right("00" & Left(busqueda.Desde.ToString, 6), 2) & "-" & Right("00" & busqueda.Desde.ToString, 2)
                If Not existeParametro Then
                    existeParametro = True
                    resultStr += " WHERE "
                End If
                resultStr += "FechaGasto >= '" + Fecha + "'"
            End If
            If busqueda.Hasta > 0 Then
                Dim Fecha As String = Left(busqueda.Hasta.ToString, 4) & "-" & Right("00" & Left(busqueda.Hasta.ToString, 6), 2) & "-" & Right("00" & busqueda.Hasta.ToString, 2)
                If Not existeParametro Then
                    existeParametro = True
                    resultStr += " WHERE "
                Else
                    resultStr += " AND "
                End If
                resultStr += "FechaGasto <= '" + Fecha + "'"
            End If
            If busqueda.Estados <> "" Then
                If Not existeParametro Then
                    existeParametro = True
                    resultStr += " WHERE "
                Else
                    resultStr += " AND "
                End If
                resultStr += "g.IdEstado IN ('" + busqueda.Estados(0) & "'"
                Dim i As Integer = 1
                While i <= busqueda.Estados.Length - 1
                    resultStr += ", '" & busqueda.Estados(i) & "'"
                    i += 1
                End While
                resultStr += ")"
            End If
            If Not existeParametro Then
                existeParametro = True
                resultStr += " WHERE "
            Else
                resultStr += " AND "
            End If
            resultStr += "g.FechaBaja IS NULL"
            'WHERE FechaGasto BETWEEN '20200101' AND '20200301' and g.IdEstado IN (2)
            Dim result As List(Of Gasto) = DAL_Gasto.TraerTodosXBusqueda(resultStr)
            Return result
        End Function

        Private Shared Function TraerTodosXEstado(idEstado As Enumeradores.EstadoGasto) As List(Of Gasto)
            Dim result As New List(Of Gasto)
            Dim Todos As List(Of Gasto) = TraerTodos()
            If Todos.Count > 0 Then
                result = Todos.FindAll(Function(x) CInt(x.IdEstado) = idEstado)
                If result Is Nothing Then
                    Throw New Exception("No existen Gastos para la búsqueda")
                End If
            End If
            Return result
        End Function
        Public Shared Function TraerGastosAbiertos() As List(Of Gasto)
            Dim result As New List(Of Gasto)
            Dim Todos As List(Of Gasto) = TraerTodos()
            If Todos.Count > 0 Then
                result = TraerTodosXEstado(Enumeradores.EstadoGasto.Abierto)
                If result Is Nothing Then
                    Throw New Exception("No existen Gastos abiertos")
                End If
            End If
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Gasto.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            IdEstado = Enumeradores.EstadoGasto.Anulado
            DAL_Gasto.Baja(Me)
        End Sub
        Public Sub Cerrar()
            ValidarCerrar()
            IdEstado = Enumeradores.EstadoGasto.Cerrado
            DAL_Gasto.Modifica(Me)
        End Sub
        'Public Sub Modifica()
        '    ValidarModifica()
        '    DAL_Gasto.Modifica(Me)
        '    Refresh()
        'End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Gasto
            Dim result As New DTO.DTO_Gasto With {
                .IdEntidad = IdEntidad,
                .Importe = Importe,
                .FechaGasto = LngFechaGasto,
                .CantidadComprobantes = CantidadComprobantes,
                .IdEstado = IdEstado,
                .Estado = Estado,
                .Observaciones = Observaciones,
                .FechaAlta = LngFechaAlta
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
            ValidarComprobantesActivos()
        End Sub
        Private Sub ValidarCerrar()
            ValidarUsuario(Me.IdUsuarioModifica)
            ValidarComprobantesPagados()
            'ValidarCampos()
            'ValidarNoDuplicados()
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
        Private Sub ValidarComprobantesPagados()
            Dim cantidadErrores As Integer = 0
            If ListaComprobantes.Count > 0 Then
                For Each item As Comprobante In ListaComprobantes
                    If Not item.FechaPago.HasValue Or item.IdTipoPago = 0 Then
                        cantidadErrores += 1
                    End If
                Next
            End If
            If cantidadErrores > 0 Then
                Throw New Exception("No se puede Cerrar el Gasto. Existen Comprobantes sin pagar.")
            End If
        End Sub
        Private Sub ValidarComprobantesActivos()
            Dim cantidadErrores As Integer = 0
            If ListaComprobantes.Count > 0 Then
                For Each item As Comprobante In ListaComprobantes
                    If Not item.FechaBaja.HasValue Then
                        cantidadErrores += 1
                    End If
                Next
            End If
            If cantidadErrores > 0 Then
                Throw New Exception("No se puede Anular el Gasto. Existen Comprobantes sin anulars.")
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
            'Gasto.Refresh()
            'Dim result As Gasto = Todos.Find(Function(x) x.Observaciones.ToUpper = Observaciones)
            'If Not result Is Nothing Then
            '    Throw New Exception("El Observaciones a ingresar ya existe")
            'End If
        End Sub
#End Region
    End Class ' Gasto
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Gasto
        Inherits DTO_DBE


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Observaciones() As String = ""
        Public Property IdEstado() As Integer = 0
        Public Property Estado() As String = ""
        Public Property Importe() As Decimal = 0
        Public Property FechaGasto() As Long = 0
        Public Property CantidadComprobantes() As Integer = 0
#End Region
    End Class ' DTO_Gasto
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Gasto

#Region " Stored "
        Const storeAlta As String = "ADM.p_Gasto_Alta"
        Const storeBaja As String = "ADM.p_Gasto_Baja"
        Const storeModifica As String = "ADM.p_Gasto_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_Gasto_TraerUnoXId"
        Const storeTraerTodos As String = "ADM.p_Gasto_TraerTodos"
        Const storeTraerTodosXEstado As String = "ADM.p_Gasto_TraerTodosXEstado"
        Const storeTraerTodosXBusqueda As String = "ADM.p_Gasto_TraerXBusquedaLibre"
        Const storeTraerTodosUltimos5 As String = "ADM.p_Gasto_TraerTodosUltimos5"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Gasto)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Gasto)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@idEstado", entidad.IdEstado)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Gasto)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        '' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Gasto
            Dim store As String = storeTraerUnoXId
            Dim result As New Gasto
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
        Public Shared Function TraerTodos() As List(Of Gasto)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Gasto)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosXBusqueda(sqlQuery As String) As List(Of Gasto)
            Dim store As String = storeTraerTodosXBusqueda
            Dim listaResult As New List(Of Gasto)
            Dim pa As New parametrosArray
            pa.add("@sqlQuery", sqlQuery)
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        'Public Shared Function TraerTodosXEstado(IdEstado As Integer) As List(Of Gasto)
        '    Dim store As String = storeTraerTodosXEstado
        '    Dim pa As New parametrosArray
        '    pa.add("@IdEstado", IdEstado)
        '    Dim listaResult As New List(Of Gasto)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If dt.Rows.Count > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                listaResult.Add(LlenarEntidad(dr))
        '            Next
        '        End If
        '    End Using
        '    Return listaResult
        'End Function
        'Public Shared Function TraerTodosUltimos5() As List(Of Gasto)
        '    Dim store As String = storeTraerTodosUltimos5
        '    Dim pa As New parametrosArray
        '    Dim listaResult As New List(Of Gasto)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If dt.Rows.Count > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                listaResult.Add(LlenarEntidad(dr))
        '            Next
        '        End If
        '    End Using
        '    Return listaResult
        'End Function

#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Gasto
            Dim entidad As New Gasto
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
            If dr.Table.Columns.Contains("IdEstado") Then
                If dr.Item("IdEstado") IsNot DBNull.Value Then
                    entidad.IdEstado = CType(dr.Item("IdEstado"), Enumeradores.EstadoGasto)
                End If
            End If
            If dr.Table.Columns.Contains("CantidadComprobantes") Then
                If dr.Item("CantidadComprobantes") IsNot DBNull.Value Then
                    entidad.CantidadComprobantes = CInt(dr.Item("CantidadComprobantes"))
                End If
            End If
            If dr.Table.Columns.Contains("importe") Then
                If dr.Item("importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("importe"))
                End If
            End If
            If dr.Table.Columns.Contains("FechaGasto") Then
                If dr.Item("FechaGasto") IsNot DBNull.Value Then
                    entidad.FechaGasto = CDate(dr.Item("FechaGasto"))
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
    End Class ' DAL_Gasto
End Namespace ' DataAccessLibrary