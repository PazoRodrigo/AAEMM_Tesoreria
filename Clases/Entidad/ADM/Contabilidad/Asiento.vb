Option Explicit On
Option Strict On


Imports LUM
Imports Connection
Imports Clases.Entidad_Asiento
Imports Clases.DataAccessLibrary_Asiento

Namespace Entidad_Asiento
    Public Class Asiento
        Inherits DBE

        Public Structure strAsientoImpresion
            Public IdAsiento As Integer
            Public Fecha As Long
            Public ImporteTotal As Decimal
            Public IdCuenta As Integer
            Public Cuenta As String
            Public DH As Integer
            Public ImporteLinea As Decimal
        End Structure
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Fecha() As Date? = Nothing
        Public Property Importe() As Decimal = 0
        Public Property ListaLineas() As List(Of Entidad_Asiento.AsientoLinea)
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
            Dim objImportar As Asiento = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Fecha = objImportar.Fecha
            Importe = objImportar.Importe
        End Sub
        Sub New(ByVal ObjDesde As DTO_Asiento.DTO_Asiento)
            ' DBE
            IdUsuarioAlta = ObjDesde.IdUsuarioAlta
            IdUsuarioBaja = ObjDesde.IdUsuarioBaja
            IdUsuarioModifica = ObjDesde.IdUsuarioModifica
            IdMotivoBaja = ObjDesde.IdMotivoBaja
            If ObjDesde.FechaAlta > 0 Then
                Dim TempFecha As String = Right(ObjDesde.FechaAlta.ToString, 2) + "/" + Left(Right(ObjDesde.FechaAlta.ToString, 4), 2) + "/" + Left(ObjDesde.FechaAlta.ToString, 4)
                FechaAlta = CDate(TempFecha)
            End If
            If ObjDesde.FechaBaja > 0 Then
                Dim TempFecha As String = Right(ObjDesde.FechaBaja.ToString, 2) + "/" + Left(Right(ObjDesde.FechaBaja.ToString, 4), 2) + "/" + Left(ObjDesde.FechaBaja.ToString, 4)
                FechaBaja = CDate(TempFecha)
            End If
            ' Entidad
            IdEntidad = ObjDesde.IdEntidad
            If ObjDesde.Fecha > 0 Then
                Dim TempFecha As String = Right(ObjDesde.Fecha.ToString, 2) + "/" + Left(Right(ObjDesde.Fecha.ToString, 4), 2) + "/" + Left(ObjDesde.Fecha.ToString, 4)
                Fecha = CDate(TempFecha)
            End If
            Importe = ObjDesde.Importe
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Asiento
            Return DAL_Asiento.TraerUno(Id)
        End Function
        Public Shared Function TraerTodos() As List(Of Asiento)
            Return DAL_Asiento.TraerTodos()
        End Function
        Public Shared Function TraerTodosXCuenta_DTO(IdCuenta As Integer) As List(Of Asiento)
            Return DAL_Asiento.TraerTodosXCuenta(IdCuenta)
        End Function
        Public Shared Function TraerTodosXCuentaImpresion(IdCuenta As Integer) As List(Of Asiento.strAsientoImpresion)
            Return DAL_Asiento.TraerTodosXCuentaImpresion(IdCuenta)
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Asiento.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Asiento.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Asiento.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO_Asiento.DTO_Asiento
            Dim result As New DTO_Asiento.DTO_Asiento
            result.IdEntidad = IdEntidad
            result.Fecha = LngFecha
            result.Importe = Importe
            Dim lista As New List(Of DTO_Entidad.DTO_AsientoLinea)
            If Not IsNothing(ListaLineas) AndAlso ListaLineas.Count > 0 Then
                For Each item As Entidad_Asiento.AsientoLinea In ListaLineas
                    lista.Add(item.ToDTO)
                Next
                result.ListaLineas = lista
            End If
            Return result
        End Function
        Public Shared Function ToListDTO(ListaOriginal As List(Of Asiento)) As List(Of DTO_Asiento.DTO_Asiento)
            Dim ListaResult As New List(Of DTO_Asiento.DTO_Asiento)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As Asiento In ListaOriginal
                    ListaResult.Add(item.ToDTO)
                Next
            End If
            Return ListaResult
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
            If Not Me.Fecha.HasValue Then
                sError &= "<b>Fecha</b> Debe ingresar la Fecha del Asiento. <br />"
            End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_Asiento.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' Asiento
End Namespace ' Entidad

Namespace DTO_Asiento
    Public Class DTO_Asiento
        Inherits Clases.DTO.DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Fecha() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property ListaLineas() As List(Of DTO_Entidad.DTO_AsientoLinea)
#End Region
    End Class ' DTO_Asiento
End Namespace ' DTO

Namespace DataAccessLibrary_Asiento
    Public Class DAL_Asiento

#Region " Stored "
        Const storeAlta As String = "Contable.p_Asiento_Alta"
        Const storeBaja As String = "p_Asiento_Baja"
        Const storeModifica As String = "p_Asiento_Modifica"
        Const storeTraerUnoXId As String = "p_Asiento_TraerUnoXId"
        Const storeTraerTodos As String = "p_Asiento_TraerTodos"
        Const storeTraerTodosActivos As String = "p_Asiento_TraerTodosActivos"
        Const storeTraerTodosXCuenta As String = "Contable.p_Asiento_TraerTodosXCuenta"
        Const storeTraerTodosXCuentaImpresion As String = "Contable.p_Asiento_TraerTodosXCuentaImpresion"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Asiento)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Fecha", entidad.Fecha)
            pa.add("@Importe", entidad.Importe)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Asiento)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Asiento)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Fecha", entidad.Fecha)
            pa.add("@Importe", entidad.Importe)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Asiento
            Dim store As String = storeTraerUnoXId
            Dim result As New Asiento
            Dim pa As New parametrosArray
            pa.add("@id", id)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = LlenarEntidad(dt.Rows(0))
                    End If
                End If
            End Using
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Asiento)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Asiento)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosXCuenta(IdCuenta As Integer) As List(Of Asiento)
            Dim store As String = storeTraerTodosXCuenta
            Dim pa As New parametrosArray
            pa.add("@IdCuenta", IdCuenta)
            Dim listaResult As New List(Of Asiento)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosXCuentaImpresion(IdCuenta As Integer) As List(Of Asiento.strAsientoImpresion)
            Dim store As String = storeTraerTodosXCuentaImpresion
            Dim pa As New parametrosArray
            pa.add("@IdCuenta", IdCuenta)
            Dim listaResult As New List(Of Asiento.strAsientoImpresion)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidadImpresion(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Asiento
            Dim entidad As New Asiento
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
            If dr.Table.Columns.Contains("Fecha") Then
                If dr.Item("Fecha") IsNot DBNull.Value Then
                    entidad.Fecha = CDate(dr.Item("Fecha"))
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            Dim lista As List(Of Entidad_Asiento.AsientoLinea) = Entidad_Asiento.AsientoLinea.TraerTodosXAsiento(entidad.IdEntidad)
            If Not IsNothing(lista) AndAlso lista.Count > 0 Then
                entidad.ListaLineas = lista
            End If
            Return entidad
        End Function
        Private Shared Function LlenarEntidadImpresion(ByVal dr As DataRow) As Asiento.strAsientoImpresion
            Dim entidadImpresion As New Asiento.strAsientoImpresion
            If dr.Table.Columns.Contains("IdAsiento") Then
                If dr.Item("IdAsiento") IsNot DBNull.Value Then
                    entidadImpresion.IdAsiento = CInt(dr.Item("IdAsiento").ToString)
                End If
            End If
            If dr.Table.Columns.Contains("Fecha") Then
                If dr.Item("Fecha") IsNot DBNull.Value Then
                    Dim result As Long = 0
                    Dim resultFecha As Date? = CDate(dr.Item("Fecha").ToString.Trim)
                    If resultFecha.HasValue Then
                        result = CLng(Year(resultFecha.Value).ToString & Right("00" & Month(resultFecha.Value).ToString, 2) & Right("00" & Day(resultFecha.Value).ToString, 2))
                    End If
                    entidadImpresion.Fecha = result
                End If
            End If
            If dr.Table.Columns.Contains("ImporteAsiento") Then
                If dr.Item("ImporteAsiento") IsNot DBNull.Value Then
                    entidadImpresion.ImporteTotal = CDec(dr.Item("ImporteAsiento").ToString.Trim)
                End If
            End If
            If dr.Table.Columns.Contains("IdCuentaContable") Then
                If dr.Item("IdCuentaContable") IsNot DBNull.Value Then
                    entidadImpresion.IdCuenta = CInt(dr.Item("IdCuentaContable").ToString.Trim)
                End If
            End If
            If dr.Table.Columns.Contains("Cuenta") Then
                If dr.Item("Cuenta") IsNot DBNull.Value Then
                    entidadImpresion.Cuenta = dr.Item("Cuenta").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("DH") Then
                If dr.Item("DH") IsNot DBNull.Value Then
                    entidadImpresion.DH = CInt(dr.Item("DH").ToString.Trim)
                End If
            End If
            If dr.Table.Columns.Contains("ImporteLinea") Then
                If dr.Item("ImporteLinea") IsNot DBNull.Value Then
                    entidadImpresion.ImporteLinea = CDec(dr.Item("ImporteLinea").ToString.Trim)
                End If
            End If
            Return entidadImpresion
        End Function
#End Region
    End Class ' DAL_Asiento
End Namespace ' DataAccessLibrary