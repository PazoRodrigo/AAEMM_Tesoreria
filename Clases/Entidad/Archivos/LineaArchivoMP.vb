Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection
Imports System.IO

Namespace Entidad
    Public Class LineaArchivoMP
        Inherits LineaArchivo

#Region " Atributos / Propiedades "
        Public Property FechaMovimiento() As Date? = Nothing
        Public Property FechaValor() As Date? = Nothing
        Public Property Monto() As Decimal = 0
        Public Property Referencia() As String = ""
        Public Property Concepto() As String = ""
#End Region
        Public Overrides Function ArmarListaLineas(idUsuario As Integer, nombreArchivo As String, archivo As StreamReader) As List(Of LineaArchivo)
            Try
                Dim ListaResult As New List(Of LineaArchivo)
                Dim entidad As LineaArchivoMP
                Dim varTempo As String = ""
                Dim primeraLinea As Integer = 9
                Dim nroLinea As Integer = 0
                Dim linea As String
                While Not archivo.EndOfStream
                    linea = archivo.ReadLine
                    nroLinea += 1
                    If nroLinea >= primeraLinea Then
                        ContadorLinea += 1
                        entidad = New LineaArchivoMP
                        entidad.IdUsuarioAlta = idUsuario
                        entidad.IdTipoArchivo = Enumeradores.TipoOrigen.MP
                        entidad.NombreArchivo = nombreArchivo
                        'Public Property FechaMovimiento() As Date? = Nothing
                        varTempo = linea.Split(CChar(";"))(0).ToString.Trim
                        entidad.FechaMovimiento = CDate(varTempo)
                        'Public Property FechaValor() As Date? = Nothing
                        varTempo = linea.Split(CChar(";"))(1).ToString.Trim
                        entidad.FechaValor = CDate(varTempo)
                        'Public Property Monto() As Decimal = 0
                        varTempo = linea.Split(CChar(";"))(2).ToString.Trim
                        entidad.Monto = CDec(varTempo)
                        'Public Property Referencia() As String = ""
                        varTempo = linea.Split(CChar(";"))(3).ToString.Trim
                        entidad.Referencia = varTempo.Trim.ToUpper
                        'Public Property Concepto() As String = ""
                        varTempo = linea.Split(CChar(";"))(4).ToString.Trim
                        entidad.Concepto = varTempo.Trim.ToUpper
                        If ContadorLinea = 1 Then
                            ValidacionesPrimeraLinea(entidad)
                        End If
                        ListaResult.Add(entidad)
                    End If
                End While
                Return ListaResult
            Catch ex As Exception
                Throw ex
            Finally
                'Cierra el archivo
                archivo.Close()
            End Try
        End Function
        Protected Overrides Sub ValidacionesPrimeraLinea(Linea As LineaArchivo)
            Dim objEntidad As LineaArchivoMP = CType(Linea, LineaArchivoMP)
            ' Nombre archivo Correcto
            Dim strFecha As String = "MP" + Year(CDate(objEntidad.FechaMovimiento)).ToString + Right("00" + Month(CDate(objEntidad.FechaMovimiento)).ToString, 2) + Right("00" + Day(CDate(objEntidad.FechaMovimiento)).ToString, 2)
            If objEntidad.NombreArchivo <> strFecha Then
                Throw New Exception("El nombre del Archivo no se corresponde con su composición.")
            End If
            ' No Duplicados
            Dim Lista As List(Of LineaArchivoMP) = DAL_LineaArchivoMP.TraerTodosXFecha(objEntidad.FechaMovimiento)
            If Lista.Count > 0 Then
                Throw New Exception("Archivo ingresado anteriormente.")
            End If
        End Sub
        Public Overrides Sub IngresarListaLineas(ListaLineas As List(Of LineaArchivo))
            'Dim ListaTempIngresar As New List(Of LineaArchivoMP)
            'If ListaLineas.Count > 0 Then
            '    NombreArchivo = ListaLineas(0).NombreArchivo
            '    For Each item As LineaArchivoMP In ListaLineas
            '        If item.Monto > 0 AndAlso Left(item.Concepto, 2) = "TR" Then
            '            ListaTempIngresar.Add(item)
            '        End If
            '        item.Alta()
            '    Next
            'End If
            'Ingreso.AltaArchivoMP(NombreArchivo)
        End Sub
        Public Sub Alta()
            DAL_LineaArchivoMP.Alta(Me)
        End Sub
    End Class ' LineaArchivoBN
End Namespace ' Entidad

Namespace DataAccessLibrary
    Public Class DAL_LineaArchivoMP

#Region " Stored "
        Const storeAlta As String = "ARCHIVO.p_LineaArchivoMP_Alta"
        Const storeTraerTodosXFecha As String = "ARCHIVO.p_LineaArchivoMP_TraerTodosXFecha"
#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As LineaArchivoMP)
            Dim res As Integer = 0
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@FechaMovimiento", entidad.FechaMovimiento)
            pa.add("@FechaValor", entidad.FechaValor)
            pa.add("@Monto", entidad.Monto)
            pa.add("@NombreArchivo", entidad.NombreArchivo)
            pa.add("@Referencia", entidad.Referencia.Trim.ToUpper)
            pa.add("@Concepto", entidad.Concepto.Trim.ToUpper)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        res = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        Public Shared Function TraerTodosXFecha(ByVal FechaMovimiento As Date?) As List(Of LineaArchivoMP)
            Dim store As String = storeTraerTodosXFecha
            Dim ListaResult As New List(Of LineaArchivoMP)
            Dim pa As New parametrosArray
            pa.add("@FechaMovimiento", FechaMovimiento)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        ListaResult.Add(LlenarEntidad(dt.Rows(0)))
                    End If
                End If
            End Using
            Return ListaResult
        End Function
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As LineaArchivoMP
            Dim entidad As New LineaArchivoMP
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
            If dr.Table.Columns.Contains("NombreArchivo") Then
                If dr.Item("NombreArchivo") IsNot DBNull.Value Then
                    entidad.NombreArchivo = dr.Item("NombreArchivo").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("FechaMovimiento") Then
                If dr.Item("FechaMovimiento") IsNot DBNull.Value Then
                    entidad.FechaMovimiento = CDate(dr.Item("FechaMovimiento"))
                End If
            End If
            If dr.Table.Columns.Contains("FechaValor") Then
                If dr.Item("FechaValor") IsNot DBNull.Value Then
                    entidad.FechaValor = CDate(dr.Item("FechaValor"))
                End If
            End If
            If dr.Table.Columns.Contains("Monto") Then
                If dr.Item("Monto") IsNot DBNull.Value Then
                    entidad.Monto = CDec(dr.Item("Monto"))
                End If
            End If
            If dr.Table.Columns.Contains("Referencia") Then
                If dr.Item("Referencia") IsNot DBNull.Value Then
                    entidad.Referencia = dr.Item("Referencia").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Concepto") Then
                If dr.Item("Concepto") IsNot DBNull.Value Then
                    entidad.Concepto = dr.Item("Concepto").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Ingreso
End Namespace ' DataAccessLibrary
