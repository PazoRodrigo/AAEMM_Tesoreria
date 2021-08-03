$(document).ready(async function () {
    try {
        await LlenarCombos();
    } catch (e) {
        alertAlerta(e);
    }
});
async function LlenarCombos() {
    let AñoInicio = 2020;
    let AñoActual = new Date().getFullYear();
    let MesActual = new Date().getMonth();
    let ListaAño = [];
    while (AñoInicio <= AñoActual) {
        ListaAño.push({ IdEntidad: AñoInicio, Nombre: AñoInicio });
        AñoInicio++;
    }
    let ListaMes = Meses();
    await ArmarComboAños(ListaAño, 'CboAño', 'SelectorAnio', 'CboAñoId', 'Seleccionar Año', '');
    await ArmarComboMeses(ListaMes, 'CboMes', 'SelectorMes', 'CboMesId', 'Seleccionar Mes', '');
}
$('body').on('click', '#LinkBtnImprimir', async function (e) {
    try {
        spinner();
        console.log($("#CboAño"));
        let valorAño = $('#_CboAño').val();
        let valorMes = $('#_CboMes').val();
        let Periodo = valorAño +''+ Right('00' + valorMes, 2);
        spinnerClose();
        let url = 'http://localhost:14162/Forms/Reportes/Frm_Rep_IngresosMensuales.aspx?periodo=' + Periodo
        window.open(url, '_blank');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});