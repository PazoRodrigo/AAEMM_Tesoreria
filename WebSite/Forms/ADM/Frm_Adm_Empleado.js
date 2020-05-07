var _ObjEmpleado;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empleados');
        $("#SpanBtnNuevo").text('Nuevo Empleado');
        $("#SpanBtnBuscar").text('Buscar Empleado');
        $("#SpanBtnEliminar").text('Eliminar');
        $("#SpanBtnGuardar").text('Guardar');
        $("#SpanBtnFamiliares").text('Familiares (2)');
        $("#SpanBtnImprimir").text('Imprimir Empleado');
        //$("#SpanTituloGrillaDimensional").text('Centros de Costo Registrados');
        //$("#SpanTituloDimensional").text('Centros de Costo');
        //$("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        //$("#SpanBtnNuevo").text('Nuevo');
        //$("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    Nuevo_Empleado();
    await Empleado.armarUC();
}
function Limpiar_Empleado() {
    $(".DatoFormulario").val('');
}
function Nuevo_Empleado() {
    Limpiar_Empleado();
    _ObjEmpleado = new Empleado;
}
$('body').on('click', '#LinkBtnArmarUC', async function (e) {
    try {
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});