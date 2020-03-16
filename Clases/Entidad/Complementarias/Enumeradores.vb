Public Class Enumeradores
    Public Enum TipoCheque
        Propio = 1
        Tercero = 2
    End Enum
    Public Enum EstadoChequePropios
        Emitido = 1
        Vigente = 2
        Suspendido = 3
        Anulado = 4
    End Enum
    Public Enum EstadoChequeTerceros
        Recibido = 11
        Debitado = 12
        Rechazado = 13
        Vencido = 14
        Salvado = 15     ' Cheque que ha sido salvado por otro
        Salvador = 16    ' Cheque que ha salvado a otro, informa el cheque salvado
        DeBaja = 20
    End Enum
    Public Enum EstadoEmpresa
        Activa = 1
        Inactiva = 2
        DeBaja = 3
        SinDeudaConBoleta = 11
        SinDeudaSinBoleta = 12
        Deuda1Mes = 13
        Deuda3Meses = 14
        Deuda6Meses = 15
        DeudaMayor6Meses = 16
        ConPagosIntercalados = 17
    End Enum
    Public Enum EstadoEmpleado
        Activo = 1
        Inactivo = 2
        SinDeudaConBoleta = 11
        SinDeudaSinBoleta = 12
        Deuda1Mes = 13
        Deuda3Meses = 14
        Deuda6Meses = 15
        DeudaMayor6Meses = 16
    End Enum
    Public Enum EstadoGasto
        Abierto = 1
        Pagado = 2
        Anulado = 11
    End Enum
End Class
