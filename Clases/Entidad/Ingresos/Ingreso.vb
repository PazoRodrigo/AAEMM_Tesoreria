Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection
Imports System.IO
Imports Newtonsoft.Json

Namespace Entidad
    Public Class Ingreso
        Inherits DBE

        Public Structure StrBusquedaIngreso
            Public Desde As Long
            Public Hasta As Long
            Public CUIT As Long
            Public RazonSocial As String
            Public Importe As Decimal
            Public NroRecibo As Long
            Public NroCheque As Long
            Public Estados As String
            Public Tipos As String
        End Structure
#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property IdEstado() As Char = CChar("A")
        Public Property IdCentroCosto() As Integer = 0
        Public Property IdExplotado() As Integer = 0
        Public Property CodigoEntidad() As Long = 0
        Public Property CUIT() As Long = 0
        Public Property Periodo() As Integer = 0
        Public Property NroCheque() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property IdOrigen() As Integer = 0
        Public Property NroRecibo() As Long = 0

        Friend Shared Function TraerTodosXRecibo(idEntidad As Integer) As List(Of Ingreso)
            Throw New NotImplementedException()
        End Function

        Public Property NombreArchivo() As String = ""
        Public Property FechaPago() As Date? = Nothing
        Public Property FechaAcreditacion() As Date? = Nothing
        Public Property Observaciones() As String = ""
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
        Public ReadOnly Property LazyRazonSocial() As String
            Get
                Dim Result As String = ""
                Dim ListaEmpresa As List(Of Empresa) = Empresa.TraerTodosLazy
                Dim Temp As Empresa = ListaEmpresa.Find(Function(x) x.CUIT = CUIT)
                If Not Temp Is Nothing Then
                    Result = Temp.RazonSocial
                End If
                Return Result
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
            NroCheque = objImportar.NroCheque
            IdEstado = objImportar.IdEstado
            Importe = objImportar.Importe
            IdOrigen = objImportar.IdOrigen
            NroRecibo = objImportar.NroRecibo
            NombreArchivo = objImportar.NombreArchivo
            FechaPago = objImportar.FechaPago
            FechaAcreditacion = objImportar.FechaAcreditacion
            Observaciones = objImportar.Observaciones
        End Sub
        Sub New(ByVal ObjLineaArchivo As LineaArchivoBN)
            IdUsuarioAlta = ObjLineaArchivo.IdUsuarioAlta
            IdUsuarioBaja = ObjLineaArchivo.IdUsuarioBaja
            IdMotivoBaja = ObjLineaArchivo.IdMotivoBaja
            FechaAlta = ObjLineaArchivo.FechaAlta
            FechaBaja = ObjLineaArchivo.FechaBaja
            ' Entidad
            IdCentroCosto = ObjLineaArchivo.IdCentroCosto
            CodigoEntidad = ResolverCodigoEntidad(ObjLineaArchivo.CUIT)
            CUIT = ObjLineaArchivo.CUIT
            Periodo = ObjLineaArchivo.Periodo
            NroCheque = ObjLineaArchivo.NroCheche
            IdEstado = ObjLineaArchivo.IdEstado
            Importe = ObjLineaArchivo.Importe
            IdOrigen = ObjLineaArchivo.IdTipoArchivo
            NombreArchivo = ObjLineaArchivo.NombreArchivo
            FechaPago = ObjLineaArchivo.FechaPago
            FechaAcreditacion = ObjLineaArchivo.FechaAcreditacion
        End Sub
        Sub New(ByVal ObjLineaArchivo As LineaArchivoPF)
            IdUsuarioAlta = ObjLineaArchivo.IdUsuarioAlta
            IdUsuarioBaja = ObjLineaArchivo.IdUsuarioBaja
            IdMotivoBaja = ObjLineaArchivo.IdMotivoBaja
            FechaAlta = ObjLineaArchivo.FechaAlta
            FechaBaja = ObjLineaArchivo.FechaBaja
            ' Entidad
            IdCentroCosto = ObjLineaArchivo.IdCentroCosto
            CodigoEntidad = ResolverCodigoEntidad(ObjLineaArchivo.CUIT)
            CUIT = ObjLineaArchivo.CUIT
            Periodo = ObjLineaArchivo.Periodo
            NroCheque = ObjLineaArchivo.NroCheche
            IdEstado = ObjLineaArchivo.IdEstado
            Importe = ObjLineaArchivo.Importe
            IdOrigen = ObjLineaArchivo.IdTipoArchivo
            NombreArchivo = ObjLineaArchivo.NombreArchivo
            FechaPago = ObjLineaArchivo.FechaPago
            FechaAcreditacion = ObjLineaArchivo.FechaAcreditacion
        End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Ingreso)
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
            IdExplotado = DtODesde.IdExplotado
            IdCentroCosto = DtODesde.IdCentroCosto
            CodigoEntidad = DtODesde.CodigoEntidad
            CUIT = DtODesde.CUIT
            Periodo = DtODesde.Periodo
            NroCheque = DtODesde.NroCheque
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
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerTodos() As List(Of Ingreso)
            Return DAL_Ingreso.TraerTodos()
        End Function
        Public Shared Function TraerTodosXBusqueda(busqueda As StrBusquedaIngreso) As List(Of Ingreso)
            Dim TablaAcreditados As String = "Ingreso.Ingresos_Acreditados Ing"
            Dim TablaNOAcreditados As String = "Ingreso.Ingresos_NOAcreditados Ing"
            Dim sqlQuery As String = "SELECT * FROM "
            Dim tablas As Integer = 0
            If busqueda.Estados.Length = 0 Then
                tablas = 1
            Else
                If (busqueda.Estados.Contains("A") Or busqueda.Estados.Contains("L") Or busqueda.Estados.Contains("T")) Then
                    tablas += 1
                End If
                If (busqueda.Estados.Contains("P") Or busqueda.Estados.Contains("R")) Then
                    tablas += 2
                End If
            End If
            If tablas < 3 Then
                If tablas = 1 Then
                    sqlQuery += TablaAcreditados
                    sqlQuery += " LEFT JOIN ADM.Empresa emp ON emp.cuit = ing.cuit "
                    sqlQuery += ArmarStringSQL(busqueda)
                ElseIf tablas = 2 Then
                    sqlQuery += TablaNOAcreditados
                    sqlQuery += " LEFT JOIN ADM.Empresa emp ON emp.cuit = ing.cuit "
                    sqlQuery += ArmarStringSQL(busqueda)
                End If
            Else
                sqlQuery += TablaAcreditados
                sqlQuery += " LEFT JOIN ADM.Empresa emp ON emp.cuit = ing.cuit "
                sqlQuery += ArmarStringSQL(busqueda)
                sqlQuery += " UNION "
                sqlQuery += TablaNOAcreditados
                sqlQuery += " LEFT JOIN ADM.Empresa emp ON emp.cuit = ing.cuit "
                sqlQuery += ArmarStringSQL(busqueda)
            End If
            Dim result As List(Of Ingreso) = DAL_Ingreso.TraerTodosXBusqueda(sqlQuery)
            Return result
        End Function
        Private Shared Function ArmarStringSQL(busqueda As StrBusquedaIngreso) As String
            Dim existeParametro As Boolean = True
            Dim result As String = "WHERE Ing.FechaBaja IS NULL "
            If busqueda.Desde > 0 Then
                Dim Fecha As String = Left(busqueda.Desde.ToString, 4) & "-" & Right("00" & Left(busqueda.Desde.ToString, 6), 2) & "-" & Right("00" & busqueda.Desde.ToString, 2)
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "FechaAcreditacion >= '" + Fecha + "'"
            End If
            If busqueda.Hasta > 0 Then
                Dim Fecha As String = Left(busqueda.Hasta.ToString, 4) & "-" & Right("00" & Left(busqueda.Hasta.ToString, 6), 2) & "-" & Right("00" & busqueda.Hasta.ToString, 2)
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "FechaAcreditacion <= '" + Fecha + "'"
            End If
            If busqueda.CUIT > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "Ing.CUIT = '" + busqueda.CUIT.ToString + "'"
            End If
            If busqueda.RazonSocial.Length > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "razonsocial LIKE '%" + busqueda.RazonSocial + "%'"
            End If
            If busqueda.Importe > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "Importe = " + Replace(CStr(busqueda.Importe), ",", ".")
            End If
            If busqueda.NroRecibo > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "NroRecibo = '" + busqueda.NroRecibo.ToString + "'"
            End If
            If busqueda.NroCheque > 0 Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "NroCheque = '" + busqueda.NroCheque.ToString + "'"
            End If
            If busqueda.Estados <> "" Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "IdEstado IN ('" + busqueda.Estados(0) & "'"
                Dim i As Integer = 1
                While i <= busqueda.Estados.Length - 1
                    result += ", '" & busqueda.Estados(i) & "'"
                    i += 1
                End While
                result += ")"
            End If
            If busqueda.Tipos <> "" Then
                If Not existeParametro Then
                    existeParametro = True
                    result += " WHERE "
                Else
                    result += " AND "
                End If
                result += "IdOrigen IN (" + busqueda.Tipos(0)
                Dim i As Integer = 1
                While i <= busqueda.Tipos.Length - 1
                    result += ", " & busqueda.Tipos(i)
                    i += 1
                End While
                result += ")"
            End If
            result += "  ORDER BY fechaAcreditacion, Periodo "

            Return result
        End Function
        Public Shared Function TraerUno(ByVal Id As Integer) As Ingreso
            Dim result As Ingreso = DAL_Ingreso.TraerUno(Id)
            If result Is Nothing Then
                Throw New Exception("No existen Ingresos para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodosXFechasXAcreditacion(Desde As Date, Hasta As Date, IdOrigen As Integer) As List(Of Ingreso)
            Return DAL_Ingreso.TraerTodosXFechasXAcreditacion(Desde, Hasta, IdOrigen)
        End Function
        Friend Shared Sub ValidarNombreArchivoDuplicado(nombreArchivo As String)
            Dim ListaIngresos As List(Of Ingreso) = DAL_Ingreso.TraerTodosXNombreArchivo(nombreArchivo)
            If ListaIngresos.Count > 0 Then
                Throw New Exception("<b>El archivo NO se ha ingresado</b> <br />Archivo ya existente en el sistema.")
            End If
        End Sub
        Friend Shared Function TraerTodosXNombreArchivo(nombreArchivo As String) As List(Of Ingreso)
            Return DAL_Ingreso.TraerTodosXNombreArchivo(nombreArchivo)
        End Function
        ' Nuevos
        Friend Shared Function TraerUltimosXOrigen(IdOrigen As Enumeradores.TipoOrigen, Cantidad As Integer) As List(Of Ingreso)
            Return DAL_Ingreso.TraerUltimos30XOrigen(IdOrigen)
        End Function
        Friend Shared Sub IngresarArchivoMC(nombreArchivo As String)
            AltaArchivoMC(nombreArchivo)
        End Sub
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
        Public Sub GuardarIngresoExplotado()
            ValidarModifica()
            DAL_Ingreso.GuardarIngresoExplotado(Me)
        End Sub
        Public Sub AltaExplosion()
            ValidarModifica()
            DAL_Ingreso.AltaExplosion(Me)
        End Sub
        Public Sub EliminarExplotado()
            ValidarModifica()
            DAL_Ingreso.EliminarExplotado(Me)
        End Sub
        Public Sub Modifica()
            ValidarModifica()
            If IdEstado = "T" Then
                IdEstado = CChar("A")
            End If
            DAL_Ingreso.Modifica(Me)
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Ingreso
            Dim result As New DTO.DTO_Ingreso With {
                .IdEntidad = IdEntidad,
                .IdEstado = IdEstado,
                .IdCentroCosto = IdCentroCosto,
                .IdExplotado = IdExplotado,
                .CodigoEntidad = CodigoEntidad,
                .CUIT = CUIT,
                .Periodo = Periodo,
                .NroCheque = NroCheque,
                .Importe = Importe,
                .IdOrigen = IdOrigen,
                .NroRecibo = NroRecibo,
                .NombreArchivo = NombreArchivo,
                .FechaPago = LngFechaPago,
                .FechaAcreditacion = LngFechaAcreditacion,
                .Observaciones = Observaciones,
                .RazonSocial = LazyRazonSocial()
            }
            Return result
        End Function
        ' Nuevos
        Public Shared Sub AltaArchivoMC(nombreArchivo As String)
            DAL_Ingreso.AltaArchivoMC(nombreArchivo)
        End Sub
        Public Shared Sub ExplotarIngreso(ByVal IdUsuario As Integer, IdEntidad As Integer, ByVal Lista As List(Of DTO.DTO_Ingreso))
            DAL_Ingreso.ExplotarIngreso(IdUsuario, IdEntidad, Lista)
        End Sub
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
            ElseIf Not IsNumeric(Me.CUIT) Then
                sError &= "<b>CUIT</b> Debe ser numérico. <br />"
            End If
            If Me.CodigoEntidad.ToString = "" Then
                sError &= "<b>Código Entidad</b> Debe ingresar el Código Entidad. <br />"
            ElseIf Not IsNumeric(Me.CodigoEntidad) Then
                sError &= "<b>Código Entidad</b> Debe ser numérico. <br />"
            End If
            If Not Me.FechaAcreditacion.HasValue Then
                sError &= "<b>Fecha de Acreditación</b> Debe ingresar la Fecha de Acreditación. <br />"
            End If
            If Me.Periodo.ToString = "" Then
                sError &= "<b>Período</b> Debe ingresar el Período. <br />"
            ElseIf Not IsNumeric(Me.Periodo) Then
                sError &= "<b>Período </b> Debe ser numérico. <br />"
            End If
            If Me.Periodo.ToString = "" Then
                sError &= "<b>Importe</b> Debe ingresar el Importe. <br />"
            ElseIf Not IsNumeric(Me.Periodo) Then
                sError &= "<b>Importe </b> Debe ser numérico. <br />"
            End If
            'If Me.RazonSocial = "" Then
            '    sError &= "<b>VariableString</b> Debe ingresar VariableString. <br />"
            'ElseIf Me.apellido.Length > 50 Then
            '    sError &= "<b>VariableString</b>Debe tener como máximo 50 caracteres. <br />"
            'End If

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
        ' Otros
        Private Shared Function ResolverCodigoEntidad(CUIT As Long) As Integer
            Dim CodigoEntidad As Integer = 0
            If CUIT > 0 Then
                Try
                    Dim Empresa As Empresa = Empresa.TraerUnaXCUIT(CUIT)
                    If Empresa IsNot Nothing Then
                        CodigoEntidad = Empresa.Codigo
                    End If
                Catch ex As Exception
                    ' No hace nada
                End Try
            End If
            Return CodigoEntidad
        End Function
        Private Shared ReadOnly Property calcularPeriodoPago(fechaAcreditacion As Date?) As Integer
            Get
                Dim result As Integer = 0
                Dim mes As Integer
                Dim anio As Integer
                If fechaAcreditacion.HasValue Then
                    anio = CInt(fechaAcreditacion.Value.Year)
                    mes = CInt(fechaAcreditacion.Value.Month)
                    If mes = 1 Then
                        anio = anio - 1
                        mes = 12
                    End If
                    result = CInt(anio & Right("00" & mes.ToString, 2))
                End If
                Return result
            End Get
        End Property
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
        Public Property IdExplotado() As Integer = 0
        Public Property CodigoEntidad() As Long = 0
        Public Property CUIT() As Long = 0
        Public Property Periodo() As Integer = 0
        Public Property NroCheque() As Long = 0
        Public Property Importe() As Decimal = 0
        Public Property IdOrigen() As Integer = 0
        Public Property NroRecibo() As Long = 0
        Public Property NombreArchivo() As String = ""
        Public Property FechaPago() As Long = 0
        Public Property FechaAcreditacion() As Long = 0
        Public Property Observaciones() As String = ""
        Public Property RazonSocial() As String = ""
#End Region
    End Class ' DTO_Ingreso
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Ingreso

#Region " Stored "
        Const storeAlta As String = "INGRESO.p_Ingreso_Alta"
        Const storeBaja As String = "INGRESO.p_Ingreso_Baja"
        Const storeAltaArchivoMC As String = "INGRESO.p_Ingreso_AltaMC"
        Const storeModifica As String = "INGRESO.p_Ingreso_Modifica"
        Const storeTraerTodosXFechasXAcreditacion As String = "INGRESO.p_Ingreso_TraerTodosXFechasXAcreditacion"
        Const storeTraerTodos As String = "INGRESO.p_Ingreso_TraerTodos"
        Const storeTraerTodosAcreditados As String = "INGRESO.p_Ingreso_TraerTodosAcreditados"
        Const storeTraerTodosNOAcreditados As String = "INGRESO.p_Ingreso_TraerTodosNOAcreditados"
        Const storeTraerTodosXBusqueda As String = "INGRESO.p_Ingreso_TraerXBusquedaLibre"
        Const storeTraerTodosXNombreArchivo As String = "INGRESO.p_Ingreso_TraerTodosXNombreArchivo"

        Const storeGuardarIngresoExplotado As String = "INGRESO.p_Ingreso_GuardarIngresoExplotado"
        Const storeAltaExplosion As String = "INGRESO.p_Ingreso_AltaExplosion"
        Const storeEliminarExplotado As String = "INGRESO.p_Ingreso_EliminarExplotado"
        Const storeExplotarIngresos As String = "INGRESO.p_Ingreso_ExplotarIngresos"








        Const storeTraerUnoXId As String = "ADM.p_Ingreso_TraerUnoXId"
        Const storeTraerUltimos30XOrigen As String = "INGRESO.p_Ingreso_storeTraerUltimos30XOrigen"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Ingreso)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@FechaPago", entidad.FechaPago)
            pa.add("@FechaAcreditacion", entidad.FechaAcreditacion)
            pa.add("@IdOrigen", entidad.IdOrigen)
            pa.add("@NombreArchivo", entidad.NombreArchivo)
            pa.add("@Importe", entidad.Importe)
            pa.add("@Periodo", entidad.Periodo)
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@CodigoEntidad", entidad.CodigoEntidad)
            pa.add("@IdCentroCosto", entidad.IdCentroCosto)
            pa.add("@NroRecibo", entidad.NroRecibo)
            pa.add("@NroCheque", entidad.NroCheque)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@Observaciones", entidad.Observaciones.Trim.ToUpper)
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
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Structure SrtJSON
            Public IdUsuario As Integer
            Public IdEntidad As Integer
            Public ListaIngresos As List(Of DTO.DTO_Ingreso)
        End Structure
        Public Shared Sub ExplotarIngreso(ByVal IdUsuario As Integer, IdEntidad As Integer, ByVal Lista As List(Of DTO.DTO_Ingreso))
            Dim str As New DAL_Ingreso.SrtJSON
            str.IdEntidad = IdEntidad
            str.IdUsuario = IdUsuario
            str.ListaIngresos = Lista
            Dim EntidadAJson As String = JsonConvert.SerializeObject(str)
            Dim store As String = storeExplotarIngresos
            Dim pa As New parametrosArray
                pa.add("@Json", EntidadAJson)

        End Sub



        Public Shared Sub GuardarIngresoExplotado(ByVal entidad As Ingreso)
            Dim store As String = storeGuardarIngresoExplotado
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@Id_a_Explotar", entidad.IdEntidad)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub AltaExplosion(ByVal entidad As Ingreso)
            Dim store As String = storeAltaExplosion
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@Id_a_Explotar", entidad.IdExplotado)
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
        Public Shared Sub EliminarExplotado(ByVal entidad As Ingreso)
            Dim store As String = storeEliminarExplotado
            Dim pa As New parametrosArray
            pa.add("@Id_a_Explotar", entidad.IdEntidad)
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
            pa.add("@CUIT", entidad.CUIT)
            pa.add("@CodigoEntidad", entidad.CodigoEntidad)
            pa.add("@Periodo", entidad.Periodo)
            pa.add("@Importe", entidad.Importe)
            pa.add("@IdEstado", entidad.IdEstado)
            pa.add("@NroCheque", entidad.NroCheque)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerTodos() As List(Of Ingreso)
            Dim store As String = storeTraerTodos
            Dim listaResult As New List(Of Ingreso)
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
        Public Shared Function TraerTodosAcreditados() As List(Of Ingreso)
            Dim store As String = storeTraerTodosAcreditados
            Dim listaResult As New List(Of Ingreso)
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
        Public Shared Function TraerTodosNOAcreditados() As List(Of Ingreso)
            Dim store As String = storeTraerTodosNOAcreditados
            Dim listaResult As New List(Of Ingreso)
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
        Public Shared Function TraerTodosXFechasXAcreditacion(ByVal Desde As Date, ByVal Hasta As Date, IdOrigen As Integer) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXFechasXAcreditacion
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@Desde", Desde)
            pa.add("@Hasta", Hasta)
            pa.add("@IdOrigen", IdOrigen)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Function TraerUltimos30XOrigen(IdOrigen As Enumeradores.TipoOrigen) As List(Of Ingreso)
            Dim store As String = storeTraerUltimos30XOrigen
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@IdOrigen", IdOrigen)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Public Shared Function TraerTodosXNombreArchivo(ByVal nombreArchivo As String) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXNombreArchivo
            Dim listaResult As New List(Of Ingreso)
            Dim pa As New parametrosArray
            pa.add("@nombreArchivo", nombreArchivo)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        Friend Shared Sub AltaArchivoMC(nombreArchivo As String)
            Dim result As Integer = 0
            Dim store As String = storeAltaArchivoMC
            Dim pa As New parametrosArray
            pa.add("@nombreArchivo", nombreArchivo)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Function TraerTodosXBusqueda(sqlQuery As String) As List(Of Ingreso)
            Dim store As String = storeTraerTodosXBusqueda
            Dim listaResult As New List(Of Ingreso)
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
            If dr.Table.Columns.Contains("Id") Then
                If dr.Item("Id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("Id"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAcreditacion") Then
                If dr.Item("fechaAcreditacion") IsNot DBNull.Value Then
                    entidad.FechaAcreditacion = CDate(dr.Item("fechaAcreditacion"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaPago") Then
                If dr.Item("fechaPago") IsNot DBNull.Value Then
                    entidad.FechaPago = CDate(dr.Item("fechaPago"))
                End If
            End If
            If dr.Table.Columns.Contains("IdOrigen") Then
                If dr.Item("IdOrigen") IsNot DBNull.Value Then
                    entidad.IdOrigen = CInt(dr.Item("IdOrigen"))
                End If
            End If
            If dr.Table.Columns.Contains("NombreArchivo") Then
                If dr.Item("NombreArchivo") IsNot DBNull.Value Then
                    entidad.NombreArchivo = dr.Item("NombreArchivo").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Importe") Then
                If dr.Item("Importe") IsNot DBNull.Value Then
                    entidad.Importe = CDec(dr.Item("Importe"))
                End If
            End If
            If dr.Table.Columns.Contains("Periodo") Then
                If dr.Item("Periodo") IsNot DBNull.Value Then
                    entidad.Periodo = CInt(dr.Item("Periodo"))
                End If
            End If
            If dr.Table.Columns.Contains("CUIT") Then
                If dr.Item("CUIT") IsNot DBNull.Value Then
                    entidad.CUIT = CLng(dr.Item("CUIT"))
                End If
            End If
            If dr.Table.Columns.Contains("CodigoEntidad") Then
                If dr.Item("CodigoEntidad") IsNot DBNull.Value Then
                    entidad.CodigoEntidad = CLng(dr.Item("CodigoEntidad"))
                End If
            End If
            If dr.Table.Columns.Contains("IdCentroCosto") Then
                If dr.Item("IdCentroCosto") IsNot DBNull.Value Then
                    entidad.IdCentroCosto = CInt(dr.Item("IdCentroCosto"))
                End If
            End If
            If dr.Table.Columns.Contains("IdEstado") Then
                If dr.Item("IdEstado") IsNot vbNullChar AndAlso dr.Item("IdEstado") IsNot DBNull.Value Then
                    If dr.Item("IdEstado").ToString = "" Then
                        entidad.IdEstado = CChar("A")
                    Else
                        entidad.IdEstado = CChar(dr.Item("IdEstado"))
                    End If
                End If
            End If
            If dr.Table.Columns.Contains("NroCheque") Then
                If dr.Item("NroCheque") IsNot DBNull.Value Then
                    entidad.NroCheque = CLng(dr.Item("NroCheque"))
                End If
            End If
            If dr.Table.Columns.Contains("NroRecibo") Then
                If dr.Item("NroRecibo") IsNot DBNull.Value Then
                    entidad.NroRecibo = CLng(dr.Item("NroRecibo"))
                End If
            End If
            If dr.Table.Columns.Contains("IdExplotado") Then
                If dr.Item("IdExplotado") IsNot DBNull.Value Then
                    entidad.IdExplotado = CInt(dr.Item("IdExplotado"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Ingreso
End Namespace ' DataAccessLibrary