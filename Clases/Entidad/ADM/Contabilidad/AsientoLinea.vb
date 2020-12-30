Option Explicit On
Option Strict On


Imports LUM
Imports Connection
Imports Clases.Entidad_Asiento
Imports Clases.DataAccessLibrary_Asiento

Namespace Entidad_Asiento
    Public Class AsientoLinea
        Inherits DBE

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdAsiento() As Integer = 0
        Public Property IdCuentaContable() As Integer = 0
        Public Property TipoDH() As Enumeradores.TipoMovimientoDH = Enumeradores.TipoMovimientoDH.SinDH
        Public Property Importe() As Decimal = 0
#End Region
#Region " Lazy Load / ReadOnly"
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
            Dim objImportar As AsientoLinea = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdUsuarioModifica = objImportar.IdUsuarioModifica
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdAsiento = objImportar.IdAsiento
            IdCuentaContable = objImportar.IdCuentaContable
            TipoDH = objImportar.TipoDH
            Importe = objImportar.Importe
        End Sub
        Sub New(ByVal ObjDesde As DTO_Entidad.DTO_AsientoLinea)
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
            IdAsiento = ObjDesde.IdAsiento
            IdCuentaContable = ObjDesde.IdCuentaContable
            TipoDH = ObjDesde.TipoDH
            Importe = ObjDesde.Importe
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As AsientoLinea
            Return DAL_AsientoLinea.TraerUno(Id)
        End Function
        Public Shared Function TraerTodos() As List(Of AsientoLinea)
            Return DAL_AsientoLinea.TraerTodos()
        End Function
        Friend Shared Function TraerTodosXAsiento(IdAsiento As Integer) As List(Of AsientoLinea)
            Return DAL_AsientoLinea.TraerTodosXAsiento(IdAsiento)
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_AsientoLinea.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_AsientoLinea.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_AsientoLinea.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO_Entidad.DTO_AsientoLinea
            Dim result As New DTO_Entidad.DTO_AsientoLinea
            result.IdEntidad = IdEntidad
            result.IdAsiento = IdAsiento
            result.IdCuentaContable = IdCuentaContable
            result.TipoDH = TipoDH
            result.Importe = Importe
            Return result
        End Function
        Public Shared Function ToListDTO(ListaOriginal As List(Of AsientoLinea)) As List(Of DTO_Entidad.DTO_AsientoLinea)
            Dim ListaResult As New List(Of DTO_Entidad.DTO_AsientoLinea)
            If ListaOriginal IsNot Nothing AndAlso ListaOriginal.Count > 0 Then
                For Each item As AsientoLinea In ListaOriginal
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
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            'Dim cantidad As Integer = DAL_AsientoLinea.TraerTodosXDenominacionCant(Me.denominacion)
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
    End Class ' AsientoLinea
End Namespace ' Entidad

Namespace DTO_Entidad
    Public Class DTO_AsientoLinea
        Inherits Clases.DTO.DTO_DBE

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdAsiento() As Integer = 0
        Public Property IdCuentaContable() As Integer = 0
        Public Property TipoDH() As Enumeradores.TipoMovimientoDH = Enumeradores.TipoMovimientoDH.SinDH
        Public Property Importe() As Decimal = 0
#End Region
    End Class ' DTO_AsientoLinea
End Namespace ' DTO

Namespace DataAccessLibrary_Asiento
    Public Class DAL_AsientoLinea

#Region " Stored "
        Const storeAlta As String = "Contable.p_AsientoLinea_Alta"
        Const storeBaja As String = "p_AsientoLinea_Baja"
        Const storeModifica As String = "p_AsientoLinea_Modifica"
        Const storeTraerUnoXId As String = "p_AsientoLinea_TraerUnoXId"
        Const storeTraerTodos As String = "p_AsientoLinea_TraerTodos"
        Const storeTraerTodosXAsiento As String = "Contable.p_AsientoLinea_TraerTodosXAsiento"
        Const storeTraerTodosActivos As String = "p_AsientoLinea_TraerTodosActivos"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As AsientoLinea)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdAsiento", entidad.IdAsiento)
            pa.add("@IdCuentaContable", entidad.IdCuentaContable)
            pa.add("@TipoDH", entidad.TipoDH)
            pa.add("@Importe", entidad.Importe)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As AsientoLinea)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        Public Shared Sub Modifica(ByVal entidad As AsientoLinea)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdAsiento", entidad.IdAsiento)
            pa.add("@IdCuentaContable", entidad.IdCuentaContable)
            pa.add("@TipoDH", entidad.TipoDH)
            pa.add("@Importe", entidad.Importe)
            Connection.Connection.Ejecutar(store, pa)
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As AsientoLinea
            Dim store As String = storeTraerUnoXId
            Dim result As New AsientoLinea
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
        Public Shared Function TraerTodos() As List(Of AsientoLinea)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of AsientoLinea)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXAsiento(IdAsiento As Integer) As List(Of AsientoLinea)
            Dim store As String = storeTraerTodosXAsiento
            Dim pa As New parametrosArray
            pa.add("@IdAsiento", IdAsiento)
            Dim listaResult As New List(Of AsientoLinea)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As AsientoLinea
            Dim entidad As New AsientoLinea
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
            If dr.Table.Columns.Contains("IdAsiento") Then
                If dr.Item("IdAsiento") IsNot DBNull.Value Then
                    entidad.IdAsiento = CInt(dr.Item("IdAsiento"))
                End If
            End If
            If dr.Table.Columns.Contains("IdCuentaContable") Then
                If dr.Item("IdCuentaContable") IsNot DBNull.Value Then
                    entidad.IdCuentaContable = CInt(dr.Item("IdCuentaContable"))
                End If
            End If
            If dr.Table.Columns.Contains("DH") Then
                If dr.Item("DH") IsNot DBNull.Value Then
                    entidad.TipoDH = CType(CInt(dr.Item("DH")), Enumeradores.TipoMovimientoDH)
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_AsientoLinea
End Namespace ' DataAccessLibrary