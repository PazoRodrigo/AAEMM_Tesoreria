$(document).ready(async function () {
    try {
        spinner();
        await ValidarPermisosXPerfil();
        $("#NombreFormulario").text('Indicadores');
        spinnerClose();
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
    $("#LblPagoUltimoMes").text(ObjIndicadores.PagoUltimoMes);
    $("#LblEmpresasSinDeuda").text(ObjIndicadores.EmpresasSinDeuda);
    $("#LblEmpresasDeuda1Mes").text(ObjIndicadores.EmpresasDeuda1Mes);
    $("#LblEmpresasDeuda3Meses").text(ObjIndicadores.EmpresasDeuda3Meses);
    $("#LblEmpresasDeuda6Meses").text(ObjIndicadores.EmpresasDeuda6Meses);
    $("#LblEmpresasDeudaMayor6Meses").text(ObjIndicadores.EmpresasDeudaMayor6Meses);
    $("#LblEmpresasSinPagosUltimos12Meses").text(ObjIndicadores.EmpresasSinPagosUltimos12Meses);
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

    $("#LblRecaudacionNeta").text(separadorMiles(ObjIndicadores.RecaudacionNeta.toFixed(2)));
    $("#LblRecaudacionBruta").text(separadorMiles(ObjIndicadores.RecaudacionBruta.toFixed(2)));

    $("#LblChequesRechazados").text(ObjIndicadores.ChequesRechazados);
    spinnerClose();
}

$('body').on('click', '#FORM0_INDICADORES_EMPRESAS', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinBaja();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_SINDEUDA', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinDeuda();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_DEUDA1', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda1();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_DEUDA3', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda3();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_DEUDA6', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda6();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_DEUDAMAYOR6', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeudaMayor6();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_INDICADORES_EMPRESAS_SINPAGOSULTIMOS12', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinPagoUltimos12();
        await Empresa.ArmarGrillaImpresion('ContainerPop', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Limpiar', async function (e) {
    try {
        let ListaEmpresas = [];
        $("#ContainerPop").html("");
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Imprimir', async function (e) {
    try {
        spinner()
        let nombreArchivo = "Reporte Empresas-" + FechaHoyLng() + ".xls";
        $("#ContainerPop").table2excel({ filename: nombreArchivo, sheetName: "Reporte Empresas" });
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_RECAUDACIONNETA', async function (e) {
    try {
        spinner()
        await ArmarPopRecaudacion(0);
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#FORM0_RECAUDACIONBRUTA', async function (e) {
    try {
        spinner()
        await ArmarPopRecaudacion(1);
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});


async function ArmarPopRecaudacion(Tipo) {
    let lista = [];
    let Titulo = '';
    let fecha = new Date();
    if (Tipo == 0) {
        lista = await IngresoReporte.TraerRecaudacionNeta(Date_PrimerDiaMes_LngToLng(fecha), Date_UltimoDiaMes_LngToLng(fecha));
        Titulo = 'Recaudación Neta';
    } else {
        lista = await IngresoReporte.TraerRecaudacionBruta(Date_PrimerDiaMes_LngToLng(fecha), Date_UltimoDiaMes_LngToLng(fecha));
        Titulo = 'Recaudación Bruta';
    }
    let ValoresADepositar_Depositados = await ChequeTercero.TraerTodosXEstado(1);
    let ValoresADepositar_Recibidos = await ChequeTercero.TraerTodosXEstado(0);
    console.log(ValoresADepositar_Depositados);
    console.log(ValoresADepositar_Recibidos);
    let control = "";
    ($("#Modal-PopUpConsumosAfiliado").remove());
    control += '<div id="Modal-PopUpConsumosAfiliado" class="modal" tabindex="-1" role="dialog" >';
    control += '    <div class="modal-dialog modal-lg">';
    control += '        <div class="modal-content">';
    control += '            <div class="modal-header bg-primary">';
    control += '                <h3 class="modal-title ">' + Titulo + '</h3>';
    control += '            </div>';
    control += '            <div class="row">';
    control += '                <div class="modal-body">';
    control += '                    <div class="container col-md-12">';
    control += '                        <div class="row">';
    control += '                            <div class="col-md-12">';
    control += '                                <h4>' + '' + '</h4>';
    control += '                            </div>';
    control += '                        </div>';
    control += '                    </div>';
    control += '                        <div class="row">';
    control += '                            <div class="col-md-12">';
    control += '                                <div style="max-height: 400px; overflow-y: scroll;width:100%;">';
    control += '                                    <table class="table table-bordered">';
    control += '                                        <tr><th style="text-align: center;">Fecha <br> Acreditación</th><th style="text-align: center;">CUIT</th><th>Razón Social</th><th style="text-align: center;width: 150px;">Importe</th><th style="text-align: center;">Origen</th></tr>';
    let i = 0;
    let totalDia = 0;
    let total = 0;
    let dia;
    while (i <= lista.length - 1) {
        dia = lista[i].FechaAcreditacion;
        totalDia = 0;
        while (i <= lista.length - 1 && dia == lista[i].FechaAcreditacion) {
            totalDia += lista[i].Importe;
            total += lista[i].Importe;
            let color = '';
            let CUIT = lista[i].CUIT;
            let RazonSocial = lista[i].RazonSocial;
            if (lista[i].CUIT == 0) {
                color = 'color: red;';
                CUIT = '';
                RazonSocial = 'A DETERMINAR';
            }
            control += String.format("<tr><td>{0}</td><td style='" + color + "'>{1}</td><td style='" + color + "'>{2}</td><td style='text-align: right;" + color + "'>{3}</td><td style='text-align: center;'>{4}</td></tr>", Date_LongToString(lista[i].FechaAcreditacion), CUIT, RazonSocial, MonedaDecimales2(lista[i].Importe), lista[i].Origen);
            i++;
        }
        control += '<tr><td colspan=3 style="text-align:right"><span>Total Parcial  ' + Date_LongToString(dia) + ': </td><th colspan=2>' + MonedaDecimales2(totalDia) + '</span></th></tr>';
    }
    control += '<tr><th colspan=5 style="text-align:right">' + MonedaDecimales2(total) + '</span></th></tr>';
    control += '                            </table>';
    control += '                        </div>';
    control += '                        </div>';
    control += '                    </div>';
    //if (ValoresADepositar_Recibidos.length > 0) {
    //control += '                        <div class="row" style="margin-top: 15px">';
    //control += '                            <div class="col-md-8 text-primary text-right"> Valores Recibidos: </div>';
    //control += '                            <div class="col-md-4 text-primary"> ' + MonedaDecimales2(valorCheque) + '</div>';
    //control += '                        </div>';
    //}   
    if (ValoresADepositar_Recibidos.length > 0) {
        let importeTotal = 0;
        let cont = 0;
        while (cont <= ValoresADepositar_Recibidos.length - 1) {
            console.log(ValoresADepositar_Recibidos[cont].Importe);
            importeTotal += ValoresADepositar_Recibidos[cont].Importe;
            cont++;
        }
        control += '                        <div class="row" style="margin-top: 15px">';
        control += '                            <div class="col-md-7 text-primary text-right"> Valores Recibidos: </div>';
        control += '                            <div class="col-md-1 text-primary text-right">(' + ValoresADepositar_Recibidos.length + ')</div>';
        control += '                            <div class="col-md-3 text-primary text-right"> ' + MonedaDecimales2(importeTotal) + '</div>';
        control += '                        </div>';
    }   
    if (ValoresADepositar_Depositados.length > 0) {
        let importeTotal = 0;
        let cont = 0;
        while (cont <= ValoresADepositar_Depositados.length - 1) {
            console.log(ValoresADepositar_Depositados[cont].Importe);
            importeTotal += ValoresADepositar_Depositados[cont].Importe;
            cont++;
        }
        control += '                        <div class="row" style="margin-top: 15px">';
        control += '                            <div class="col-md-7 text-primary text-right"> Valores Depositados: </div>';
        control += '                            <div class="col-md-1 text-primary text-right">(' + ValoresADepositar_Depositados.length + ')</div>';
        control += '                            <div class="col-md-3 text-primary text-right"> ' + MonedaDecimales2(importeTotal) + '</div>';
        control += '                        </div>';
    }   
    control += '                </div>';
    control += '            </div>';
    control += '            <div class="modal-footer">';
    control += '                <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>';
    control += '            </div>';
    control += '        </div>';
    control += '    </div>';
    control += '</div>';
    $("body").append(control);
    $("#Modal-PopUpConsumosAfiliado").modal({ show: true, backdrop: 'static', keyboard: false })
    spinnerClose();
}
