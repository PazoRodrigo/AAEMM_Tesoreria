$('body').on('click', '#LogoHeader', async function (e) {
    try {
        spinner();
        CentroCosto.Refresh();
        Convenio.Refresh();
        CuentaContable.Refresh();
        OriginarioGasto.Refresh();
        Proveedor.Refresh();
        TipoContacto.Refresh();
        TipoDomicilio.Refresh();
        TipoGasto.Refresh();
        TipoPago.Refresh();
        Gasto.Refresh();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});