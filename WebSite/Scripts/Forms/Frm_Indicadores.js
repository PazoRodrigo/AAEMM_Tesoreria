$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Indicadores');
        $("#LblCantidadEmpresas").text('3026');
        $("#LblCtEmpr01").text('1500');
        $("#LblCtEmpr02").text('1250');
        $("#LblCtEmpr03").text('10');
        $("#LblCtEmpr04").text('200');
        $("#LblCtEmpr05").text('20');
        $("#LblCtEmpr06").text('30');
        $("#LblCtEmpr07").text('12');
        $("#LblCtEmpr08").text('4');
        $("#LblCantidadEmpleados").text('4322');
        $("#LblCtEmpl01").text('2301');
        $("#LblCtEmpl02").text('1550');
        $("#LblCtEmpl03").text('300');
        $("#LblCtEmpl04").text('95');
        $("#LblCtEmpl05").text('12');
        $("#LblCtEmpl06").text('0');
        $("#LblCtEmpl07").text('58');
        $("#LblCtEmpl08").text('6');
        $("#LblRecaudacion").text('150536,35');
        $("#LblRec01").text('26840,45');
        $("#LblRec02").text('19200,35');
        $("#LblRec03").text('30650,65');
        $("#LblRec04").text('6598,00');
        $("#LblRec05").text('48536,15');
        $("#LblRec06").text('6805,68');
        $("#LblRec07").text('1560,58');
        $("#LblRec08").text('200,26');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
})