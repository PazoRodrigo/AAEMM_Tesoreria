$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Indicadores');
        Inicio();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
})
async function Inicio() {
    spinner();
    let ObjIndicadores = await Indicadores.Todos();
    $("#LblEmpresas").text(ObjIndicadores.Empresas);
    $("#LblEmpresasSinDeudaSinBoleta").text(ObjIndicadores.EmpresasSinDeudaSinBoleta);
    $("#LblEmpresasSinDeudaConBoleta").text(ObjIndicadores.EmpresasSinDeudaConBoleta);
    $("#LblEmpresasDeuda1Mes").text(ObjIndicadores.EmpresasDeuda1Mes);
    $("#LblEmpresasDeuda3Meses").text(ObjIndicadores.EmpresasDeuda3Meses);
    $("#LblEmpresasDeuda6Meses").text(ObjIndicadores.EmpresasDeuda6Meses);
    $("#LblEmpresasDeudaMayor6Meses").text(ObjIndicadores.EmpresasDeudaMayor6Meses);
    $("#LblEmpresasPagosIntercalados").text(ObjIndicadores.EmpresasPagosIntercalados);
    $("#LblEmpresasInactivas").text(ObjIndicadores.EmpresasInactivas);

    $("#LblEmpleados").text(ObjIndicadores.Empleados);
    $("#LblEmpleadosSinDeudaSinBoleta").text(ObjIndicadores.EmpleadosSinDeudaSinBoleta);
    $("#LblEmpleadosSinDeudaConBoleta").text(ObjIndicadores.EmpleadosSinDeudaConBoleta);
    $("#LblEmpleadosDeuda1Mes").text(ObjIndicadores.EmpleadosDeuda1Mes);
    $("#LblEmpleadosDeuda3Meses").text(ObjIndicadores.EmpleadosDeuda3Meses);
    $("#LblEmpleadosDeuda6Meses").text(ObjIndicadores.EmpleadosDeuda6Meses);
    $("#LblEmpleadosDeudaMayor6Meses").text(ObjIndicadores.EmpleadosDeudaMayor6Meses);
    $("#LblEmpleadosInactivos").text(ObjIndicadores.EmpleadosInactivos);

    console.log(ObjIndicadores);
    $("#LblRecaudacionNeta").text(separadorMiles(ObjIndicadores.RecaudacionNeta.toFixed(2)));
    $("#LblRecaudacionBruta").text(separadorMiles(ObjIndicadores.RecaudacionBruta.toFixed(2)));

    //$("#LblRecaudacion").text(separadorMiles(ObjIndicadores.Recaudacion.toFixed(2)));
    //$("#LblRecaudacionXCobrarSinBoleta").text(ObjIndicadores.RecaudacionXCobrarSinBoleta);
    //$("#LblRecaudacionXCobrarConBoleta").text(ObjIndicadores.RecaudacionXCobrarConBoleta);
    //$("#LblRecaudacionDeuda1Mes").text(ObjIndicadores.RecaudacionDeuda1Mes);
    //$("#LblRecaudacionDeuda3Meses").text(ObjIndicadores.RecaudacionDeuda3Meses);
    //$("#LblRecaudacionDeuda6Meses").text(ObjIndicadores.RecaudacionDeuda6Meses);
    //$("#LblRecaudacionDeudaMayor6Meses").text(ObjIndicadores.RecaudacionDeudaMayor6Meses);
    //$("#LblRecaudacionInactivos").text(ObjIndicadores.RecaudacionInactivos);
    //$("#LblRecaudacionFueraTermino").text(ObjIndicadores.RecaudacionFueraTermino);

    $("#LblChequesRechazados").text(ObjIndicadores.ChequesRechazados);
    spinnerClose();
}