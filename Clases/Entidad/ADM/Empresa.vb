Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection

Namespace Entidad
    Public Class Empresa
        Inherits DBE

        Public Structure StrBusquedaEmpresa
            Public CUIT As Long
            Public RazonSocial As String
            Public IdCentroCosto As Integer
            Public IncluirAlta As Integer
            Public IncluirBaja As Integer
            Public Incluir0 As Integer
        End Structure

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
        Public Property Codigo() As Integer = 0
        Public Property RazonSocial() As String = ""
        Public Property Carnet() As String = ""
        Public Property CUIT() As Long = 0
        Public Property IdCentroCosto() As Integer = 0
        Public Property CorreoElectronico() As String = ""
        Public Property FechaReactivacion() As Date? = Nothing
        Public Property ObjDomicilio() As Domicilio = Nothing
        Public Property IdEstado() As Enumeradores.EstadoEmpresa = Nothing
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
            RazonSocial = DtODesde.RazonSocial
            CUIT = DtODesde.CUIT
            CorreoElectronico = DtODesde.CorreoElectronico
            IdEstado = CType(DtODesde.IdEstado, Enumeradores.EstadoEmpresa)
            ' Domicilio
            Dim objD As New Domicilio
            objD.Direccion = DtODesde.ObjDomicilio.Direccion
            objD.CodigoPostal = DtODesde.ObjDomicilio.CodigoPostal
            objD.IdLocalidad = DtODesde.ObjDomicilio.IdLocalidad
            ObjDomicilio = objD
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
        Public Shared Function TraerTodos() As List(Of Empresa)
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodos()
            If result.Count = 0 Then
                Throw New Exception("No existen Empresas para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosLazy() As List(Of Empresa)
            Return Todos()
        End Function
        Public Shared Function TraerTodosXBusqueda(busqueda As StrBusquedaEmpresa) As List(Of Empresa)
            Dim sqlQuery As String = "SELECT * FROM ADM.Empresa"
            Dim existeParametro As Boolean = False
            If busqueda.CUIT > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    sqlQuery += " WHERE "
                Else
                    sqlQuery += " AND "
                End If
                sqlQuery += " CUIT = " + busqueda.CUIT.ToString + ""
            End If
            If busqueda.RazonSocial.Length > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    sqlQuery += " WHERE "
                Else
                    sqlQuery += " AND "
                End If
                sqlQuery += "RazonSocial LIKE '%" + busqueda.RazonSocial + "%'"
            End If
            'If busqueda.IdCentroCosto > 0 Then
            '    If Not existeParametro Then
            '        existeParametro = True
            '        sqlQuery += " WHERE "
            '    Else
            '        sqlQuery += " AND "
            '    End If
            '    sqlQuery += "IdCentroCosto = '" + busqueda.IdCentroCosto.ToString + "'"
            'End If
            ' Fecha Baja
            If Not existeParametro Then
                existeParametro = True
                sqlQuery += " WHERE "
            Else
                sqlQuery += " AND "
            End If
            If busqueda.IncluirAlta = 1 AndAlso busqueda.IncluirBaja = 0 Then
                sqlQuery += " FechaBaja IS NULL"
            ElseIf busqueda.IncluirAlta = 0 AndAlso busqueda.IncluirBaja = 1 Then
                sqlQuery += " FechaBaja IS NOT NULL"
            Else
                sqlQuery += " FechaBaja IS NULL"
            End If
            Dim result As List(Of Empresa) = DAL_Empresa.TraerTodosXBusqueda(sqlQuery)
            Return result
        End Function
        Public Shared Function TraerUnaXCUIT(CUIT As Long) As Empresa
            Return DAL_Empresa.TraerUnoXCUIT(CUIT)
        End Function
#End Region
#Region " Métodos Públicos"
        ' ABM
        Public Sub Alta()
            Me.IdEstado = Enumeradores.EstadoEmpresa.Activa
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
                .ObjDomicilio = ObjDomicilio.ToDto,
                .FechaAlta = LngFechaAlta(),
                .FechaBaja = LngFechaBaja()
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
            If Me.CUIT.ToString = "" Then
                sError &= "<b>CUIT</b> Debe ingresar el CUIT. <br />"
            Else
                If Me.CUIT.ToString.Length <> 11 Then
                    sError &= "<b>CUIT</b> Debe tener 11 dígitos. <br />"
                Else
                    If Not IsNumeric(Me.CUIT) Then
                        sError &= "<b>CUIT</b> Debe ser numérico. <br />"
                    End If

                End If
            End If
            If Me.RazonSocial = "" Then
                sError &= "<b>Razón Social</b> Debe ingresar la Razón Social. <br />"
            ElseIf Me.RazonSocial.Length > 100 Then
                sError &= "<b>Razón Social</b>Debe tener como máximo 100 caracteres. <br />"
            End If

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
            Dim result As Empresa = Todos.Find(Function(x) x.CUIT = CUIT And x.IdEntidad <> IdEntidad)
            If Not result Is Nothing Then
                Throw New Exception("El CUIT ya existe")
            End If
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
        Const storeTraerTodosXBusqueda As String = "ADM.p_Empresa_TraerXBusquedaLibre"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Empresa)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@RazonSocial", entidad.RazonSocial.ToString.ToUpper.Trim)
            pa.add("@Carnet", entidad.Carnet.ToString.ToUpper.Trim)
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@IdCentroCosto", entidad.IdCentroCosto)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToString.ToUpper.Trim)
            pa.add("@Direccion", entidad.ObjDomicilio.Direccion.ToString.ToUpper.Trim)
            pa.add("@CodigoPostal", entidad.ObjDomicilio.CodigoPostal)
            pa.add("@Localidad", entidad.ObjDomicilio.Localidad.ToString.ToUpper.Trim)
            pa.add("@IdLocalidad", entidad.ObjDomicilio.IdLocalidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                        entidad.Codigo = CInt(dt.Rows(0)(1))
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
            pa.add("@Carnet", entidad.Carnet.ToString.ToUpper.Trim)
            pa.add("@IdCentroCosto", entidad.IdCentroCosto)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToString.ToUpper.Trim)
            pa.add("@Direccion", entidad.ObjDomicilio.Direccion.ToString.ToUpper.Trim)
            pa.add("@CodigoPostal", entidad.ObjDomicilio.CodigoPostal)
            pa.add("@Localidad", entidad.ObjDomicilio.Localidad.ToString.ToUpper.Trim)
            pa.add("@IdLocalidad", entidad.ObjDomicilio.IdLocalidad)
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
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
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
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
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
            Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerTodosXBusqueda(sqlQuery As String) As List(Of Empresa)
            Dim store As String = storeTraerTodosXBusqueda
            Dim listaResult As New List(Of Empresa)
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





        'Public Shared Function TraerTodosXCUIT(ByVal CUIT As Long) As List(Of Empresa)
        '    Dim store As String = storeTraerTodosXCUIT
        '    Dim listaResult As New List(Of Empresa)
        '    Dim pa As New parametrosArray
        '    pa.add("@CUIT", CUIT)
        '    Using dt As DataTable = Connection.Connection.TraerDT(store, pa)
        '        If dt.Rows.Count > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                listaResult.Add(LlenarEntidad(dr))
        '            Next
        '        End If
        '    End Using
        '    Return listaResult
        'End Function
        'Public Shared Function TraerTodosXRazonSocial(ByVal RazonSocial As String) As List(Of Empresa)
        '    Dim store As String = storeTraerTodosXRazonSocial
        '    Dim listaResult As New List(Of Empresa)
        '    Dim pa As New parametrosArray
        '    pa.add("@RazonSocial", RazonSocial)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If dt.Rows.Count > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                listaResult.Add(LlenarEntidad(dr))
        '            Next
        '        End If
        '    End Using
        '    Return listaResult
        'End Function
        'Public Shared Function TraerTodosXCentroCosto(ByVal IdCentroCosto As Integer) As List(Of Empresa)
        '    Dim store As String = storeTraerTodosXCentroCosto
        '    Dim listaResult As New List(Of Empresa)
        '    Dim pa As New parametrosArray
        '    pa.add("@IdCentroCosto", IdCentroCosto)
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
            If dr.Table.Columns.Contains("fec_Baja") Then
                If dr.Item("fec_Baja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fec_Baja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("Codigo") Then
                If dr.Item("Codigo") IsNot DBNull.Value Then
                    entidad.Codigo = CInt(dr.Item("Codigo"))
                End If
            End If
            If dr.Table.Columns.Contains("RazonSocial") Then
                If dr.Item("RazonSocial") IsNot DBNull.Value Then
                    entidad.RazonSocial = dr.Item("RazonSocial").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Carnet") Then
                If dr.Item("Carnet") IsNot DBNull.Value Then
                    entidad.Carnet = dr.Item("Carnet").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(Replace(dr.Item("CUIT").ToString, "-", ""))
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