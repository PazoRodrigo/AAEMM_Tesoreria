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
        Banco.Refresh();
        TipoMovimientoCtaCte.Refresh();
        spinnerClose();
        await ArmarPopMensajeria();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnMensajero', async function (e) {
    try {
        spinner()
        await ArmarPopMensajeria();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Mensajero_BtnEnviarMensaje', async function (e) {
    try {
        $("#spanErrrorMensaje").text('');
        let IdPerfil = $("#_CboPerfil").val();
        let IdUsuario = $("#_CboUsuario").val();
        let TextoMensaje = $("#Mensajero_Mensaje").val();
        if (IdPerfil == 0 || TextoMensaje.length == 0) {
            alertAlerta('Seleccione Perfil, Seleccione Usuario y agregue el Texto para enviar el mensaje')

        } else {
            spinner();
            let ObjMensaje = new Mensaje;
            ObjMensaje.IdHastaPerfil = parseInt(IdPerfil);
            ObjMensaje.IdHastaUsuario = parseInt(IdUsuario);
            ObjMensaje.TextoMensaje = TextoMensaje;
            console.log(ObjMensaje);
            await ObjMensaje.AgregarMensaje()
            $("#_CboPerfil").val(0);
            $("#Mensajero_CboUsuario").html('');
            $("#Mensajero_Mensaje").val('');
            spinnerClose();
            alertOk('El Mensaje se ha creado correctamente');
        }
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
async function ArmarPopMensajeria() {
    let lista = await Mensaje.TraerTodosXUsuario();
    let control = "";
    ($("#Modal-PopUpMensajero").remove());
    control += '<div id="Modal-PopUpMensajero" class="modal" tabindex="-1" role="dialog" >';
    control += '    <div class="modal-dialog modal-lg">';
    control += '        <div class="modal-content">';
    control += '            <div class="modal-header bg-warning">';
    control += '                <h3 class="modal-title ">Mensajero</h3>';
    control += '            </div>';
    control += '            <div class="row">';
    control += '                <div class="modal-body">';
    control += '                    <div class="row">';
    control += '                        <div class="col-md-12">';
    control += '                            <table class="table table-bordered">';
    control += '                                <tr><th style="width: 120px;" class="text-center">Fecha</th><th style="width: 500px;" class="text-center">Mensaje</th><th class="text-center">Remitente</th></tr>';
    //control += '                                <tr><th class="text-center">Fecha</th><th class="text-center">Mensaje</th><th class="text-center">Remitente</th><th class="text-center">Ocultar</th></tr>';
    control += '                            </table>';
    control += '                            <div style="max-height: 250px; overflow-y: scroll;width:100%;">';
    if (lista.length > 0) {
        control += '                            <table class="table table-bordered">';
        let i = 0;
        while (i <= lista.length - 1) {
            let item = lista[i];
            console.log(item);
            let ObjUsuario = await Usuario.TraerUno(item.IdUsuarioAlta);
            let color = 'dark';
            if (lista[i].IdUsuarioLeido > 0) {
                color = 'danger';
            }

            control += String.format("<tr><td class='text-center text-small text-" + color + "' style ='width: 120px;'>{0}</td><td class='text-small text-" + color + "' style='width: 500px;' >{1}</td><td class='text-small text-" + color + "'>{2}</td></tr>", Date_LongToString(item.FechaAlta), item.TextoMensaje, ObjUsuario.Nombre);
            //control += String.format("<tr><td class='text-center text-small text-" + color + "'>{0}</td><td class='text-small text-" + color + "'>{1}</td><td class='text-small text-" + color + "'>{2}</td><td class='text-" + color + "'>{3}</td></tr>", Date_LongToString(item.FechaAlta), item.TextoMensaje, item.IdUsuarioAlta, "");
            i++;
        }
        control += '                                </table>';
    }
    control += '                            </div>';
    control += '                        </div>';
    control += '                    </div>';
    control += '                    <div class="row mt-2">';
    control += '                        <div class="col-md-12 text-primary text-center"><h4>Nuevo Mensaje</h4></div>';
    control += '                    </div>';
    control += '                    <div class="row">';
    control += '                        <div class="col-md-2 text-primary text-right"> Perfil :</div>';
    control += '                        <div class="col-md-4 text-primary text-left"><div id="Mensajero_CboPerfil"></div></div>';
    control += '                        <div class="col-md-2 text-primary text-right"> Usuario : </div>';
    control += '                        <div class="col-md-4 text-primary text-left"><div id="Mensajero_CboUsuario"></div></div>';
    control += '                    </div>';
    control += '                    <div class="row mt-1">';
    control += '                        <div class="col-md-9 text-primary text-center"><textarea id="Mensajero_Mensaje" style="width:100%;" rows="4"></textarea></div>';
    control += '                    </div>';
    control += '                    <div class="row mt-1">';
    control += '                        <div class="col-md-9 text-primary text-center"><span id="spanErrrorMensaje" class="text-danger"><span></div>';
    control += '                        <div class="col-md-3 text-center">';
    control += '                            <button type="button" class="btn btn-success" id="Mensajero_BtnEnviarMensaje">Enviar Mensaje</button>';
    control += '                        </div>';
    control += '                    </div>';
    control += '                </div>';
    control += '            </div>';
    control += '            <div class="modal-footer">';
    control += '                <button type="button" class="btn btn-warning" text-dark data-dismiss="modal">Cerrar</button>';
    control += '            </div>';
    control += '        </div>';
    control += '    </div>';
    control += '</div>';
    $("body").append(control);
    $("#Modal-PopUpMensajero").modal({ show: true, backdrop: 'static', keyboard: false });

    await Perfil.ArmarCombo(await Perfil.Todos(), 'Mensajero_CboPerfil', 'Mensajero_IdCboPerfil', 'Seleccionar Perfil...', 'Mensajero_EventoSeleccionPerfil', 'form-control');
    $("#Mensajero_IdCboPerfil").val(2);
    //_ListaU = await Usuario.TraerTodos();
    //let ObjU = await Usuario.UsuarioLogueado();
    //let ListaPosibles = $.grep(listaUsuarios, function (entidad, index) {
    //    return entidad.IdEntidad != ObjU.IdEntidad;
    //});
    //await Usuario.ArmarCombo(ListaPosibles, 'Mensajero_CboUsuario', 'Mensajero_IdCboUsuario', 'Seleccionar Usuario...', 'Mensajero_EventoSeleccionUsuario', 'form-control');
    //$("#Mensajero_IdCboUsuario").val(4);
    spinnerClose();
}
document.addEventListener('Mensajero_EventoSeleccionPerfil', async function (e) {
    try {
        spinner();
        let ObjPerfilSelecionado = e.detail;
        let listaUsuarios = await Usuario.TraerTodosXPerfil(ObjPerfilSelecionado.IdEntidad);
        let ObjU = await Usuario.UsuarioLogueado();
        let ListaPosibles = $.grep(listaUsuarios, function (entidad, index) {
            return entidad.IdEntidad != ObjU.IdEntidad;
        });
        await Usuario.ArmarCombo(ListaPosibles, 'Mensajero_CboUsuario', 'Mensajero_IdCboUsuario', 'Seleccionar Usuario...', 'Mensajero_EventoSeleccionUsuario', 'form-control');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);
