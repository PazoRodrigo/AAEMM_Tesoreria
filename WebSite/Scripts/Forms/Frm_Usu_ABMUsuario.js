
$(document).ready(function () {
    try {
        let ObjUser = JSON.parse(sessionStorage.getItem("User"));
        $("#SpanNombreFormulario").text('Usuario: ' + ObjUser.Apellido + ', ' + ObjUser.Nombre);
        //$("#SpanTituloGrillaDimensional").text('Centros de Costo Registrados');
        //$("#SpanTituloDimensional").text('Centros de Costo');
        //$("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        //$("#SpanBtnNuevo").text('Nuevo');
        //$("#SpanBtnGuardar").text('Guardar');
        //Inicio(ObjUser);
    } catch (e) {
        alertAlerta(e);
    }
});