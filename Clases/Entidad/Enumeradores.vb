Public Class Enumeradores
    Public Enum TipoCheque
        Propio = 1
        Tercero = 2
    End Enum
    Public Enum TipoCuenta
        Principal = 1
        Gasto = 2
        Resultado = 3
        Anticipos = 4
        Prestamos = 5
    End Enum
    Public Enum EstadoChequePropios
        Emitido = 0
        Entregado = 1
        Suspendido = 5
        Anulado = 10
    End Enum
    Public Enum EstadoChequeTerceros
        Recibido = 0
        Depositado = 1
        Acreditado = 2
        Rechazado = 10
        Vencido = 11
        Salvado = 15     ' Cheque que ha sido salvado por otro
        Salvador = 16    ' Cheque que ha salvado a otro, informa el cheque salvado
        Anulado = 20
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
    Public Enum EstadoRecibo
        Activo = 1
        Anulado = 2
    End Enum
    Public Enum TipoPagoRecibo
        Cheque = 1
        Transferencia = 2
    End Enum
    Public Enum EstadoPagoRecibo
        Recibido = 0
        Acreditado = 1
        Rechazado = 1
        Devuelto = 1
    End Enum
    Public Enum EstadoGasto
        Abierto = 1
        Cerrado = 2
        Anulado = 11
    End Enum
    Public Enum EstadoFlazoFijo
        NoVigente = 0
        Vigente = 1
        ProximoVencer = 2
    End Enum
    Public Enum TipoPermiso
        Pagina = 1
        Boton = 2
    End Enum
    Public Enum TipoOrigen
        BN = 1
        PF = 2
        MC = 3
        CJ = 4
        MP = 5
    End Enum
    Public Enum TipoMovimientoDH
        SinDH = 0
        D = 1
        H = 2
    End Enum
    Public Enum TipoDestinatarioMensaje
        Perfil = 1
        Usuario = 2
    End Enum
End Class
