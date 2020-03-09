Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports Connection

Namespace Entidad
    Public Class Indicadores

#Region " Atributos / Propiedades "
        Public Property Empresas() As Integer = 0
        Public Property EmpresasSinDeudaSinBoleta() As Integer = 0
        Public Property EmpresasSinDeudaConBoleta() As Integer = 0
        Public Property EmpresasDeuda1Mes() As Integer = 0
        Public Property EmpresasDeuda3Meses() As Integer = 0
        Public Property EmpresasDeuda6Meses() As Integer = 0
        Public Property EmpresasDeudaMayor6Meses() As Integer = 0
        Public Property EmpresasPagosIntercalados() As Integer = 0
        Public Property EmpresasInactivas() As Integer = 0

        Public Property Empleados() As Integer = 0
        Public Property EmpleadosSinDeudaSinBoleta() As Integer = 0
        Public Property EmpleadosSinDeudaConBoleta() As Integer = 0
        Public Property EmpleadosDeuda1Mes() As Integer = 0
        Public Property EmpleadosDeuda3Meses() As Integer = 0
        Public Property EmpleadosDeuda6Meses() As Integer = 0
        Public Property EmpleadosDeudaMayor6Meses() As Integer = 0
        Public Property EmpleadosInactivos() As Integer = 0

        Public Property Recaudacion() As Decimal = 0
        Public Property RecaudacionXCobrarSinBoleta() As Decimal = 0
        Public Property RecaudacionXCobrarConBoleta() As Decimal = 0
        Public Property RecaudacionDeuda1Mes() As Decimal = 0
        Public Property RecaudacionDeuda3Meses() As Decimal = 0
        Public Property RecaudacionDeuda6Meses() As Decimal = 0
        Public Property RecaudacionDeudaMayor6Meses() As Decimal = 0
        Public Property RecaudacionInactivos() As Decimal = 0
        Public Property RecaudacionFueraTermino() As Decimal = 0

        Public Property ChequesRechazados() As Integer = 0
#End Region
#Region " Lazy Load "

#End Region
#Region " Constructores "
        Sub New()

        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        Public Shared Function TraerTodos() As Indicadores
            Return DAL_Indicadores.TraerTodos
        End Function

#End Region
#Region " Métodos Públicos"
        ' Otros
        Public Function ToDTO() As DTO.DTO_Indicadores
            Dim result As New DTO.DTO_Indicadores With {
                .Empresas = Empresas,
                .EmpresasSinDeudaSinBoleta = EmpresasSinDeudaSinBoleta,
                .EmpresasSinDeudaConBoleta = EmpresasSinDeudaConBoleta,
                .EmpresasDeuda1Mes = EmpresasDeuda1Mes,
                .EmpresasDeuda3Meses = EmpresasDeuda3Meses,
                .EmpresasDeuda6Meses = EmpresasDeuda6Meses,
                .EmpresasDeudaMayor6Meses = EmpresasDeudaMayor6Meses,
                .EmpresasPagosIntercalados = EmpresasPagosIntercalados,
                .EmpresasInactivas = EmpresasInactivas,
                .Empleados = Empleados,
                .EmpleadosSinDeudaSinBoleta = EmpleadosSinDeudaSinBoleta,
                .EmpleadosSinDeudaConBoleta = EmpleadosSinDeudaConBoleta,
                .EmpleadosDeuda1Mes = EmpleadosDeuda1Mes,
                .EmpleadosDeuda3Meses = EmpleadosDeuda3Meses,
                .EmpleadosDeuda6Meses = EmpleadosDeuda6Meses,
                .EmpleadosDeudaMayor6Meses = EmpleadosDeudaMayor6Meses,
                .EmpleadosInactivos = EmpleadosInactivos,
                .Recaudacion = Recaudacion,
                .RecaudacionXCobrarSinBoleta = RecaudacionXCobrarSinBoleta,
                .RecaudacionXCobrarConBoleta = RecaudacionXCobrarConBoleta,
                .RecaudacionDeuda1Mes = RecaudacionDeuda1Mes,
                .RecaudacionDeuda3Meses = RecaudacionDeuda3Meses,
                .RecaudacionDeuda6Meses = RecaudacionDeuda6Meses,
                .RecaudacionDeudaMayor6Meses = RecaudacionDeudaMayor6Meses,
                .RecaudacionInactivos = RecaudacionInactivos,
                .RecaudacionFueraTermino = RecaudacionFueraTermino,
                .ChequesRechazados = ChequesRechazados
            }
            Return result
        End Function
#End Region
    End Class ' Indicadores
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Indicadores

#Region " Atributos / Propiedades"
        Public Property Empresas() As Integer = 0
        Public Property EmpresasSinDeudaSinBoleta() As Integer = 0
        Public Property EmpresasSinDeudaConBoleta() As Integer = 0
        Public Property EmpresasDeuda1Mes() As Integer = 0
        Public Property EmpresasDeuda3Meses() As Integer = 0
        Public Property EmpresasDeuda6Meses() As Integer = 0
        Public Property EmpresasDeudaMayor6Meses() As Integer = 0
        Public Property EmpresasPagosIntercalados() As Integer = 0
        Public Property EmpresasInactivas() As Integer = 0

        Public Property Empleados() As Integer = 0
        Public Property EmpleadosSinDeudaSinBoleta() As Integer = 0
        Public Property EmpleadosSinDeudaConBoleta() As Integer = 0
        Public Property EmpleadosDeuda1Mes() As Integer = 0
        Public Property EmpleadosDeuda3Meses() As Integer = 0
        Public Property EmpleadosDeuda6Meses() As Integer = 0
        Public Property EmpleadosDeudaMayor6Meses() As Integer = 0
        Public Property EmpleadosInactivos() As Integer = 0

        Public Property Recaudacion() As Decimal = 0
        Public Property RecaudacionXCobrarSinBoleta() As Decimal = 0
        Public Property RecaudacionXCobrarConBoleta() As Decimal = 0
        Public Property RecaudacionDeuda1Mes() As Decimal = 0
        Public Property RecaudacionDeuda3Meses() As Decimal = 0
        Public Property RecaudacionDeuda6Meses() As Decimal = 0
        Public Property RecaudacionDeudaMayor6Meses() As Decimal = 0
        Public Property RecaudacionInactivos() As Decimal = 0
        Public Property RecaudacionFueraTermino() As Decimal = 0

        Public Property ChequesRechazados() As Integer = 0

#End Region
    End Class ' DTO_Indicadores
End Namespace ' DTO


Namespace DataAccessLibrary
    Public Class DAL_Indicadores

#Region " Stored "
        Const storeTraerTodos As String = "p_Indicadores_TraerTodos"
#End Region
#Region " Métodos Públicos "
        Public Shared Function TraerTodos() As Indicadores
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim Result As New Indicadores
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count = 1 Then
                    For Each dr As DataRow In dt.Rows
                        Result = LlenarEntidad(dr)
                    Next
                End If
            End Using
            Return Result
        End Function
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Indicadores
            Dim entidad As New Indicadores
            ' Empresas
            If dr.Table.Columns.Contains("Empresas") Then
                If dr.Item("Empresas") IsNot DBNull.Value Then
                    entidad.Empresas = CInt(dr.Item("Empresas"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasSinDeudaSinBoleta") Then
                If dr.Item("EmpresasSinDeudaSinBoleta") IsNot DBNull.Value Then
                    entidad.EmpresasSinDeudaSinBoleta = CInt(dr.Item("EmpresasSinDeudaSinBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasSinDeudaConBoleta") Then
                If dr.Item("EmpresasSinDeudaConBoleta") IsNot DBNull.Value Then
                    entidad.EmpresasSinDeudaConBoleta = CInt(dr.Item("EmpresasSinDeudaConBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasDeuda1Mes") Then
                If dr.Item("EmpresasDeuda1Mes") IsNot DBNull.Value Then
                    entidad.EmpresasDeuda1Mes = CInt(dr.Item("EmpresasDeuda1Mes"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasDeuda3Meses") Then
                If dr.Item("EmpresasDeuda3Meses") IsNot DBNull.Value Then
                    entidad.EmpresasDeuda3Meses = CInt(dr.Item("EmpresasDeuda3Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasDeuda6Meses") Then
                If dr.Item("EmpresasDeuda6Meses") IsNot DBNull.Value Then
                    entidad.EmpresasDeuda6Meses = CInt(dr.Item("EmpresasDeuda6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasDeudaMayor6Meses") Then
                If dr.Item("EmpresasDeudaMayor6Meses") IsNot DBNull.Value Then
                    entidad.EmpresasDeudaMayor6Meses = CInt(dr.Item("EmpresasDeudaMayor6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasPagosIntercalados") Then
                If dr.Item("EmpresasPagosIntercalados") IsNot DBNull.Value Then
                    entidad.EmpresasPagosIntercalados = CInt(dr.Item("EmpresasPagosIntercalados"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpresasInactivas") Then
                If dr.Item("EmpresasInactivas") IsNot DBNull.Value Then
                    entidad.EmpresasInactivas = CInt(dr.Item("EmpresasInactivas"))
                End If
            End If

            ' Empleados
            If dr.Table.Columns.Contains("Empleados") Then
                If dr.Item("Empleados") IsNot DBNull.Value Then
                    entidad.Empleados = CInt(dr.Item("Empleados"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosSinDeudaSinBoleta") Then
                If dr.Item("EmpleadosSinDeudaSinBoleta") IsNot DBNull.Value Then
                    entidad.EmpleadosSinDeudaSinBoleta = CInt(dr.Item("EmpleadosSinDeudaSinBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosSinDeudaConBoleta") Then
                If dr.Item("EmpleadosSinDeudaConBoleta") IsNot DBNull.Value Then
                    entidad.EmpleadosSinDeudaConBoleta = CInt(dr.Item("EmpleadosSinDeudaConBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosDeuda1Mes") Then
                If dr.Item("EmpleadosDeuda1Mes") IsNot DBNull.Value Then
                    entidad.EmpleadosDeuda1Mes = CInt(dr.Item("EmpleadosDeuda1Mes"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosDeuda3Meses") Then
                If dr.Item("EmpleadosDeuda3Meses") IsNot DBNull.Value Then
                    entidad.EmpleadosDeuda3Meses = CInt(dr.Item("EmpleadosDeuda3Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosDeuda6Meses") Then
                If dr.Item("EmpleadosDeuda6Meses") IsNot DBNull.Value Then
                    entidad.EmpleadosDeuda6Meses = CInt(dr.Item("EmpleadosDeuda6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosDeudaMayor6Meses") Then
                If dr.Item("EmpleadosDeudaMayor6Meses") IsNot DBNull.Value Then
                    entidad.EmpleadosDeudaMayor6Meses = CInt(dr.Item("EmpleadosDeudaMayor6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("EmpleadosInactivos") Then
                If dr.Item("EmpleadosInactivos") IsNot DBNull.Value Then
                    entidad.EmpleadosInactivos = CInt(dr.Item("EmpleadosInactivos"))
                End If
            End If

            ' Recaudacion
            If dr.Table.Columns.Contains("Recaudacion") Then
                If dr.Item("Recaudacion") IsNot DBNull.Value Then
                    entidad.Recaudacion = CDec(dr.Item("Recaudacion"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionXCobrarSinBoleta") Then
                If dr.Item("RecaudacionXCobrarSinBoleta") IsNot DBNull.Value Then
                    entidad.RecaudacionXCobrarSinBoleta = CDec(dr.Item("RecaudacionXCobrarSinBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionXCobrarConBoleta") Then
                If dr.Item("RecaudacionXCobrarConBoleta") IsNot DBNull.Value Then
                    entidad.RecaudacionXCobrarConBoleta = CDec(dr.Item("RecaudacionXCobrarConBoleta"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionDeuda1Mes") Then
                If dr.Item("RecaudacionDeuda1Mes") IsNot DBNull.Value Then
                    entidad.RecaudacionDeuda1Mes = CDec(dr.Item("RecaudacionDeuda1Mes"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionDeuda3Meses") Then
                If dr.Item("RecaudacionDeuda3Meses") IsNot DBNull.Value Then
                    entidad.RecaudacionDeuda3Meses = CDec(dr.Item("RecaudacionDeuda3Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionDeuda6Meses") Then
                If dr.Item("RecaudacionDeuda6Meses") IsNot DBNull.Value Then
                    entidad.RecaudacionDeuda6Meses = CDec(dr.Item("RecaudacionDeuda6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionDeudaMayor6Meses") Then
                If dr.Item("RecaudacionDeudaMayor6Meses") IsNot DBNull.Value Then
                    entidad.RecaudacionDeudaMayor6Meses = CDec(dr.Item("RecaudacionDeudaMayor6Meses"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionInactivos") Then
                If dr.Item("RecaudacionInactivos") IsNot DBNull.Value Then
                    entidad.RecaudacionInactivos = CDec(dr.Item("RecaudacionInactivos"))
                End If
            End If
            If dr.Table.Columns.Contains("RecaudacionFueraTermino") Then
                If dr.Item("RecaudacionFueraTermino") IsNot DBNull.Value Then
                    entidad.RecaudacionFueraTermino = CDec(dr.Item("RecaudacionFueraTermino"))
                End If
            End If

            ' Otros
            If dr.Table.Columns.Contains("ChequesRechazados") Then
                If dr.Item("ChequesRechazados") IsNot DBNull.Value Then
                    entidad.ChequesRechazados = CInt(dr.Item("ChequesRechazados"))
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_nombreClase
End Namespace ' DataAccessLibrary