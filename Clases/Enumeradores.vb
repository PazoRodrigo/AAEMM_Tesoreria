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
End Class
