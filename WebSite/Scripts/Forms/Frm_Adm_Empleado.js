var _ObjEmpleado;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empleados');
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
    Nuevo_Empresa();
    await Empleado.armarUC();
}
function Limpiar_Empleado() {
    $(".DatoFormulario").val('');
}
function Nuevo_Empresa() {
    Limpiar_Empleado();
    _ObjEmpleado = new Empleado;
}