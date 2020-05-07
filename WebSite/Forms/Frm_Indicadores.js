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
    let ObjInidicadores = await Indicadores.Todos();
    $("#LblEmpresas").text(ObjInidicadores.Empresas);
    $("#LblEmpresasSinDeudaSinBoleta").text(ObjInidicadores.EmpresasSinDeudaSinBoleta);
    $("#LblEmpresasSinDeudaConBoleta").text(ObjInidicadores.EmpresasSinDeudaConBoleta);
    $("#LblEmpresasDeuda1Mes").text(ObjInidicadores.EmpresasDeuda1Mes);
    $("#LblEmpresasDeuda3Meses").text(ObjInidicadores.EmpresasDeuda3Meses);
    $("#LblEmpresasDeuda6Meses").text(ObjInidicadores.EmpresasDeuda6Meses);
    $("#LblEmpresasDeudaMayor6Meses").text(ObjInidicadores.EmpresasDeudaMayor6Meses);
    $("#LblEmpresasPagosIntercalados").text(ObjInidicadores.EmpresasPagosIntercalados);
    $("#LblEmpresasInactivas").text(ObjInidicadores.EmpresasInactivas);

    $("#LblEmpleados").text(ObjInidicadores.Empleados);
    $("#LblEmpleadosSinDeudaSinBoleta").text(ObjInidicadores.EmpleadosSinDeudaSinBoleta);
    $("#LblEmpleadosSinDeudaConBoleta").text(ObjInidicadores.EmpleadosSinDeudaConBoleta);
    $("#LblEmpleadosDeuda1Mes").text(ObjInidicadores.EmpleadosDeuda1Mes);
    $("#LblEmpleadosDeuda3Meses").text(ObjInidicadores.EmpleadosDeuda3Meses);
    $("#LblEmpleadosDeuda6Meses").text(ObjInidicadores.EmpleadosDeuda6Meses);
    $("#LblEmpleadosDeudaMayor6Meses").text(ObjInidicadores.EmpleadosDeudaMayor6Meses);
    $("#LblEmpleadosInactivos").text(ObjInidicadores.EmpleadosInactivos);

    $("#LblRecaudacion").text(ObjInidicadores.Recaudacion);
    $("#LblRecaudacionXCobrarSinBoleta").text(ObjInidicadores.RecaudacionXCobrarSinBoleta);
    $("#LblRecaudacionXCobrarConBoleta").text(ObjInidicadores.RecaudacionXCobrarConBoleta);
    $("#LblRecaudacionDeuda1Mes").text(ObjInidicadores.RecaudacionDeuda1Mes);
    $("#LblRecaudacionDeuda3Meses").text(ObjInidicadores.RecaudacionDeuda3Meses);
    $("#LblRecaudacionDeuda6Meses").text(ObjInidicadores.RecaudacionDeuda6Meses);
    $("#LblRecaudacionDeudaMayor6Meses").text(ObjInidicadores.RecaudacionDeudaMayor6Meses);
    $("#LblRecaudacionInactivos").text(ObjInidicadores.RecaudacionInactivos);
    $("#LblRecaudacionFueraTermino").text(ObjInidicadores.RecaudacionFueraTermino);

    $("#LblChequesRechazados").text(ObjInidicadores.ChequesRechazados);
    spinnerClose();
}