Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Empresa
        Inherits DBE

        Private Shared _Todos As List(Of Empresa)
        Public Shared Property Todos() As List(Of Empresa)
            Get
                If _Todos Is Nothing Then
                    _Todos = DAL_Empresa.TraerTodos
                End If
                Return _Todos
            End Get
            Set(ByVal value As List(Of Empresa))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Codigo() As Long = 0
        Public Property RazonSocial() As String = ""
        Public Property CUIT() As Long = 0
        Public Property Establecimiento() As Integer = 0
        Public Property CorreoElectronico() As String = ""
        Public Property IdEstado() As Enumeradores.EstadoEmpresa = Nothing
        Public Property FechaReactivacion() As Date? = Nothing
        Public Property ObjDomicilio() As Domicilio = Nothing
#End Region
#Region " Lazy Load "
        Public ReadOnly Property LngFechaReactivacion() As Long
            Get
                Dim result As Long = 0
                If FechaReactivacion.HasValue Then
                    result = CLng(Year(FechaReactivacion.Value).ToString & Right("00" & Month(FechaReactivacion.Value).ToString, 2) & Right("00" & Day(FechaReactivacion.Value).ToString, 2))
                End If
                Return result
            End Get
        End Property
        'Private _DatosCalculados As DatosCalculados
        'Public ReadOnly Property DatosCalculados() As DatosCalculados
        '    Get
        '        If _DatosCalculados Is Nothing Then
        '            _DatosCalculados = DatosCalculados.TraerUno(CUIT, Establecimiento)
        '        End If
        '        Return _DatosCalculados
        '    End Get
        'End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Empresa = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            RazonSocial = objImportar.RazonSocial
            CUIT = objImportar.CUIT
            CorreoElectronico = objImportar.CorreoElectronico
            IdEstado = objImportar.IdEstado
            ' Domicilio
            ObjDomicilio.Direccion = objImportar.ObjDomicilio.Direccion
            ObjDomicilio.CodigoPostal = objImportar.ObjDomicilio.CodigoPostal
            ObjDomicilio.IdLocalidad = objImportar.ObjDomicilio.IdLocalidad
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Empresa)
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
            RazonSocial = DtODesde.RazonSocial
            CUIT = DtODesde.CUIT
            CorreoElectronico = DtODesde.CorreoElectronico
            IdEstado = CType(DtODesde.IdEstado, Enumeradores.EstadoEmpresa)
            ' Domicilio
            ObjDomicilio.Direccion = DtODesde.ObjDomicilio.Direccion
            ObjDomicilio.CodigoPostal = DtODesde.ObjDomicilio.CodigoPostal
            ObjDomicilio.IdLocalidad = DtODesde.ObjDomicilio.IdLocalidad
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerUno(ByVal Id As Integer) As Empresa
            Dim result As Empresa = DAL_Empresa.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerUnoXCUIT(CUIT As Long) As Empresa
            Return DAL_Empresa.TraerUnoXCUIT(CUIT)
        End Function
        Public Shared Function TraerTodos() As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodos()
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCUIT(CUIT As Long) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXCUIT(CUIT)
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXRazonSocial(RazonSocial As String) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXRazonSocial(RazonSocial.Trim)
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXCentroCosto(IdCentroCosto As Integer) As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXCentroCosto(IdCentroCosto)
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXBusqueda(razonSocial As String, cUIT As Long, idCentroCosto As Integer) As List(Of Empresa)
            Dim result As New List(Of Empresa)
            Dim buscador As Integer = 0
            If razonSocial <> "" Then
                buscador += 1
            ElseIf cUIT > 0 Then
                buscador += 2
            ElseIf idCentroCosto > 0 Then
                buscador += 4
            End If
            Select Case buscador
                Case 1
                    result = TraerTodosXRazonSocial(razonSocial)
                Case 2
                    result = TraerTodosXCUIT(cUIT)
                Case 4
                    result = TraerTodosXCentroCosto(idCentroCosto)
                Case Else

            End Select
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        ' Nuevos
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            ValidarAlta()
            DAL_Empresa.Alta(Me)
            Refresh()
        End Sub
        Public Sub Baja()
            ValidarBaja()
            DAL_Empresa.Baja(Me)
            Refresh()
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            DAL_Empresa.Modifica(Me)
            Refresh()
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Empresa
            Dim result As New DTO.DTO_Empresa With {
                .IdEntidad = IdEntidad,
                .Codigo = Codigo,
                .RazonSocial = RazonSocial,
                .CUIT = CUIT,
                .CorreoElectronico = CorreoElectronico,
                .IdEstado = IdEstado,
                .FechaReactivacion = LngFechaReactivacion,
                .ObjDomicilio = ObjDomicilio.ToDto
            }
            Return result
        End Function
        Public Function ToDTOCabecera() As DTO.DTO_Empresa
            Dim result As New DTO.DTO_Empresa With {
                .IdEntidad = IdEntidad,
                .Codigo = Codigo,
                .RazonSocial = RazonSocial,
                .CUIT = CUIT,
                .CorreoElectronico = CorreoElectronico,
                .IdEstado = IdEstado,
                .FechaReactivacion = LngFechaReactivacion
            }
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Empresa.TraerTodos
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
            'Empresa.Refresh()
            'Dim result As Empresa = Todos.Find(Function(x) x.CUIT = CUIT)
            'If Not result Is Nothing Then
            '    Throw New Exception("El Nombre a ingresar ya existe")
            'End If
        End Sub
#End Region
    End Class ' Empresa
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Empresa
        Inherits DTO_Dimensional


#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Codigo() As Long = 0
        Public Property RazonSocial() As String = ""
        Public Property CUIT() As Long = 0
        Public Property CorreoElectronico() As String = ""
        Public Property FechaReactivacion() As Long = 0
        Public Property ObjDomicilio() As DTO.DTO_Domicilio = Nothing
#End Region
    End Class ' DTO_Empresa
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Empresa

#Region " Stored "
        Const storeAlta As String = "ADM.p_Empresa_Alta"
        Const storeBaja As String = "ADM.p_Empresa_Baja"
        Const storeModifica As String = "ADM.p_Empresa_Modifica"
        Const storeTraerUnoXId As String = "ADM.p_Empresa_TraerUnoXId"
        Const storeTraerUnoXCUIT As String = "ADM.p_Empresa_TraerUnoXCUIT"
        Const storeTraerTodos As String = "ADM.p_Empresa_TraerTodos"
        Const storeTraerTodosXCUIT As String = "ADM.p_Empresa_TraerTodosXCUIT"
        Const storeTraerTodosXRazonSocial As String = "ADM.p_Empresa_TraerTodosXRazonSocial"
        Const storeTraerTodosXCentroCosto As String = "ADM.p_Empresa_TraerTodosXCentroCosto"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Empresa)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@RazonSocial", entidad.RazonSocial.ToString.ToUpper.Trim)
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Empresa)
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
        Public Shared Sub Modifica(ByVal entidad As Empresa)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@RazonSocial", entidad.RazonSocial.ToString.ToUpper.Trim)
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToString.ToUpper.Trim)
            pa.add("@FechaReactivacion", LUM.Convertidor.DateToDB(entidad.FechaReactivacion))
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerUno(ByVal id As Integer) As Empresa
            Dim store As String = storeTraerUnoXId
            Dim result As New Empresa
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
        Friend Shared Function TraerUnoXCUIT(CUIT As Long) As Empresa
            Dim store As String = storeTraerUnoXCUIT
            Dim result As New Empresa
            Dim pa As New parametrosArray
            pa.add("@CUIT", CUIT)
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
        Public Shared Function TraerTodos() As List(Of Empresa)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Empresa)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXCUIT(ByVal CUIT As Long) As List(Of Empresa)
            Dim store As String = storeTraerTodosXCUIT
            Dim listaResult As New List(Of Empresa)
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
        Public Shared Function TraerTodosXRazonSocial(ByVal RazonSocial As String) As List(Of Empresa)
            Dim store As String = storeTraerTodosXRazonSocial
            Dim listaResult As New List(Of Empresa)
            Dim pa As New parametrosArray
            pa.add("@RazonSocial", RazonSocial)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXCentroCosto(ByVal IdCentroCosto As Integer) As List(Of Empresa)
            Dim store As String = storeTraerTodosXCentroCosto
            Dim listaResult As New List(Of Empresa)
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
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Empresa
            Dim entidad As New Empresa
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
            If dr.Table.Columns.Contains("cod_ent") Then
                If dr.Item("icod_entd") IsNot DBNull.Value Then
                    entidad.Codigo = CLng(dr.Item("cod_ent"))
                End If
            End If
            If dr.Table.Columns.Contains("RazonSocial") Then
                If dr.Item("RazonSocial") IsNot DBNull.Value Then
                    entidad.RazonSocial = dr.Item("RazonSocial").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("CorreoElectronico") Then
                If dr.Item("CorreoElectronico") IsNot DBNull.Value Then
                    entidad.CorreoElectronico = dr.Item("CorreoElectronico").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("FechaReactivacion") Then
                If dr.Item("FechaReactivacion") IsNot DBNull.Value Then
                    entidad.FechaReactivacion = CDate(dr.Item("FechaReactivacion"))
                End If
            End If
            ' Domicilio
            entidad.ObjDomicilio = Domicilio.LlenarDomicilio(dr)
            Return entidad
        End Function
#End Region
    End Class ' DAL_Empresa
End Namespace ' DataAccessLibrary