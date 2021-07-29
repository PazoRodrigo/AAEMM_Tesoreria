Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection


Namespace Entidad
    Public Class PlazoFijo
        Inherits DBE

        Public Structure StrBusquedaPlazoFijo
            Public IncluirAfiliados As Integer
            Public IncluirNoAfiliados As Integer
            Public Nombre As String
            Public CUIL As Long
            Public DNI As Integer
            Public CUIT As Long
            Public RazonSocial As String
        End Structure

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdBanco() As Integer = 0
        Public Property FechaConstitucion() As Date? = Nothing
        Public Property FechaVencimiento() As Date? = Nothing
        Public Property ImporteCapital() As Decimal = 0
        Public Property ImporteInteres() As Decimal = 0
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaConstitucion() As Long
            Get
                Dim result As Long = 0
                If FechaConstitucion.HasValue Then
                    result = CLng(Year(FechaConstitucion.Value).ToString & Right("00" & Month(FechaConstitucion.Value).ToString, 2) & Right("00" & Day(FechaConstitucion.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property LngFechaVencimiento() As Long
            Get
                Dim result As Long = 0
                If FechaConstitucion.HasValue Then
                    result = CLng(Year(FechaVencimiento.Value).ToString & Right("00" & Month(FechaVencimiento.Value).ToString, 2) & Right("00" & Day(FechaVencimiento.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property EstadoPlazoFijo() As Enumeradores.EstadoFlazoFijo
            Get
                Dim result As Enumeradores.EstadoFlazoFijo = Enumeradores.EstadoFlazoFijo.Vigente
                If FechaVencimiento.HasValue Then
                    If FechaVencimiento.Value < Today Then
                        result = Enumeradores.EstadoFlazoFijo.NoVigente
                    Else
                        If DateDiff("d", Now, FechaVencimiento.Value) < 7 Then
                            result = Enumeradores.EstadoFlazoFijo.ProximoVencer
                        End If
                    End If
                End If
                Return result
            End Get
        End Property
        Public ReadOnly Property ImporteTotal() As Decimal
            Get
                Return ImporteCapital + ImporteInteres
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As PlazoFijo = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            IdBanco = objImportar.IdBanco
            FechaConstitucion = objImportar.FechaConstitucion
            FechaVencimiento = objImportar.FechaVencimiento
            ImporteCapital = objImportar.ImporteCapital
            ImporteInteres = objImportar.ImporteInteres
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_PlazoFijo)
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
            If DtODesde.FechaConstitucion > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaConstitucion.ToString, 2) + "/" + Left(Right(DtODesde.FechaConstitucion.ToString, 4), 2) + "/" + Left(DtODesde.FechaConstitucion.ToString, 4)
                FechaConstitucion = CDate(TempFecha)
            End If
            If DtODesde.FechaVencimiento > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaVencimiento.ToString, 2) + "/" + Left(Right(DtODesde.FechaVencimiento.ToString, 4), 2) + "/" + Left(DtODesde.FechaVencimiento.ToString, 4)
                FechaVencimiento = CDate(TempFecha)
            End If
            ImporteCapital = DtODesde.ImporteCapital
            ImporteInteres = DtODesde.ImporteInteres
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As PlazoFijo
            Dim result As PlazoFijo = DAL_PlazoFijo.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen PlazoFijos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of PlazoFijo)
            Dim result As List(Of PlazoFijo) = DAL_PlazoFijo.TraerTodos()
            If result Is Nothing Then
                Throw New Exception("No existen PlazoFijos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXEstado(IdEstado As Enumeradores.EstadoFlazoFijo) As List(Of PlazoFijo)
            'Dim listaTodos As List(Of PlazoFijo) = DAL_PlazoFijo.TraerTodos
            'Dim result As New List(Of PlazoFijo)
            'If listaTodos IsNot Nothing Then
            '    For Each item As PlazoFijo In listaTodos
            '        Select Case IdEstado
            '            Case Enumeradores.EstadoFlazoFijo.NoVigente
            '                If item.FechaVencimiento.Value < Now Then
            '                    result.Add(item)
            '                End If
            '            Case Enumeradores.EstadoFlazoFijo.ProximoVencer
            '                If DateDiff(DateInterval.Day, item.FechaVencimiento.Value, Now) >= 7 Then
            '                    result.Add(item)
            '                End If
            '            Case Enumeradores.EstadoFlazoFijo.Vigente
            '                If item.FechaVencimiento.Value >= Now Then
            '                    result.Add(item)
            '                End If
            '            Case Else

            '        End Select

            '    Next
            'End If
            Dim result As List(Of PlazoFijo) = DAL_PlazoFijo.TraerTodosXEstado(IdEstado)
            If result.Count = 0 Then
                Throw New Exception("No existen PlazoFijos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXBanco(IdBanco As Integer) As List(Of PlazoFijo)
            Dim result As List(Of PlazoFijo) = DAL_PlazoFijo.TraerTodosXBanco(IdBanco)
            If result.Count = 0 Then
                Throw New Exception("No existen PlazoFijos para la búsqueda")
            End If
            Return result
        End Function

        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_PlazoFijo.Alta(Me)
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_PlazoFijo.Baja(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_PlazoFijo.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_PlazoFijo
            Dim result As New DTO.DTO_PlazoFijo With {
                .IdEntidad = IdEntidad,
                .IdBanco = IdBanco,
                .FechaConstitucion = LngFechaConstitucion(),
                .FechaVencimiento = LngFechaVencimiento(),
                .ImporteCapital = ImporteCapital,
                .ImporteInteres = ImporteInteres
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
            'PlazoFijo.Refresh()
            'Dim result As PlazoFijo = Todos.Find(Function(x) x.CUIT = CUIT)
            'If Not result Is Nothing Then
            '    Throw New Exception("El Nombre a ingresar ya existe")
            'End If
        End Sub
#End Region
    End Class ' PlazoFijo
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_PlazoFijo
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property IdBanco() As Integer = 0
        Public Property FechaConstitucion() As Long = 0
        Public Property FechaVencimiento() As Long = 0
        Public Property ImporteCapital() As Decimal = 0
        Public Property ImporteInteres() As Decimal = 0
#End Region
    End Class ' DTO_PlazoFijo
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_PlazoFijo

#Region " Stored "
        Const storeAlta As String = "ADM.p_PlazoFijo_Alta"
        Const storeBaja As String = "ADM.p_PlazoFijo_Baja"
        Const storeModifica As String = "ADM.p_PlazoFijo_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_PlazoFijo_TraerUnoXId"
        Const storeTraerTodos As String = "ADM.p_PlazoFijo_TraerTodos"
        Const storeTraerTodosXEstado As String = "ADM.p_PlazoFijo_TraerTodosXEstado"
        Const storeTraerTodosXBanco As String = "ADM.p_PlazoFijo_TraerTodosXBanco"

#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As PlazoFijo)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@IdBanco", entidad.IdBanco)
            pa.add("@FechaConstitucion", LUM.Convertidor.DateToDB(entidad.FechaConstitucion))
            pa.add("@FechaVencimiento", LUM.Convertidor.DateToDB(entidad.FechaVencimiento))
            pa.add("@ImporteCapital", entidad.ImporteCapital)
            pa.add("@ImporteInteres", entidad.ImporteInteres)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As PlazoFijo)
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
        Public Shared Sub Modifica(ByVal entidad As PlazoFijo)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdBanco", entidad.IdBanco)
            pa.add("@FechaConstitucion", LUM.Convertidor.DateToDB(entidad.FechaConstitucion))
            pa.add("@FechaVencimiento", LUM.Convertidor.DateToDB(entidad.FechaVencimiento))
            pa.add("@ImporteCapital", entidad.ImporteCapital)
            pa.add("@ImporteInteres", entidad.ImporteInteres)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As PlazoFijo
            Dim store As String = storeTraerUnoXId
            Dim result As New PlazoFijo
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

        Public Shared Function TraerTodos() As List(Of PlazoFijo)
            Dim store As String = storeTraerTodos
            Dim listaResult As New List(Of PlazoFijo)
            Dim pa As New parametrosArray
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXEstado(ByVal IdEstado As Enumeradores.EstadoFlazoFijo) As List(Of PlazoFijo)
            Dim store As String = storeTraerTodosXEstado
            Dim listaResult As New List(Of PlazoFijo)
            Dim pa As New parametrosArray
            pa.add("@IdEstado", IdEstado)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXBanco(ByVal IdBanco As Integer) As List(Of PlazoFijo)
            Dim store As String = storeTraerTodosXBanco
            Dim listaResult As New List(Of PlazoFijo)
            Dim pa As New parametrosArray
            pa.add("@IdBanco", IdBanco)
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
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As PlazoFijo
            Dim entidad As New PlazoFijo
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
            ElseIf dr.Table.Columns.Contains("fe_Alta") Then
                If dr.Item("fe_Alta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fe_Alta"))
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
            If dr.Table.Columns.Contains("FechaConstitucion") Then
                If dr.Item("FechaConstitucion") IsNot DBNull.Value Then
                    entidad.FechaConstitucion = CDate(dr.Item("FechaConstitucion"))
                End If
            End If
            If dr.Table.Columns.Contains("FechaVencimiento") Then
                If dr.Item("FechaVencimiento") IsNot DBNull.Value Then
                    entidad.FechaVencimiento = CDate(dr.Item("FechaVencimiento"))
                End If
            End If
            If dr.Table.Columns.Contains("ImporteCapital") Then
                If dr.Item("ImporteCapital") IsNot DBNull.Value Then
                    entidad.ImporteCapital = CDec(dr.Item("ImporteCapital"))
                End If
            End If
            If dr.Table.Columns.Contains("ImporteInteres") Then
                If dr.Item("ImporteInteres") IsNot DBNull.Value Then
                    entidad.ImporteInteres = CDec(dr.Item("ImporteInteres"))
                End If
            End If

            Return entidad
        End Function
#End Region
    End Class ' DAL_PlazoFijo
End Namespace ' DataAccessLibrary