var _ObjIngreso;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Ingresos');
        $("#SpanBtnNuevo").text('Nuevo Ingreso');
        $("#SpanBtnBuscar").text('Buscar Ingreso');
        $("#SpanBtnEliminar").text('Eliminar');
        $("#SpanBtnGuardar").text('Guardar');
        $("#SpanBtnImprimir").text('Imprimir Ingreso');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    //Nuevo_Ingreso();
    await Ingreso.armarUC();
    //await ArmarComboCentroCosto();
    //await ArmarComboConvenio();
}