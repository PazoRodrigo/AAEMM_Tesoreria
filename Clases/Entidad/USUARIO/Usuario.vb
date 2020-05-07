Option Explicit On
Option Strict On

Imports Clases.DataAccessLibrary
Imports Clases.Entidad
Imports LUM
Imports Connection
Imports System.Configuration
Imports System.Net.Mail

Namespace Entidad
    Public Class Usuario
        Inherits DBE

        Private Shared _Todos As List(Of Usuario)
        Public Shared Property Todos() As List(Of Usuario)
            Get
                Return DAL_Usuario.TraerTodos
                'If _Todos Is Nothing Then
                '    _Todos = DAL_Usuario.TraerTodos
                'End If
                'Return _Todos
            End Get
            Set(ByVal value As List(Of Usuario))
                _Todos = value
            End Set
        End Property

#Region " Atributos / Propiedades "
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Apellido() As String = ""
        Public Property UserName() As String = ""
        Public Property Password() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property NroInterno() As Integer = 0
        Public Property AccesosSistema() As Integer = 0
        Public Property Observaciones() As String = ""
#End Region
#Region " Lazy Load "
        Public ReadOnly Property ListaPerfiles() As List(Of Perfil)
            Get
                Return Perfil.TraerTodosXUsuario(IdEntidad)
                'If _ListaPerfiles Is Nothing Then
                '    _ListaPerfiles = Perfil.TraerTodosXUsuario(IdEntidad)
                'End If
                'Return _ListaPerfiles
            End Get
        End Property
        Public ReadOnly Property IdEstado() As Integer
            Get
                Dim result As Integer = 0
                If FechaBaja.HasValue Then
                    result = 1
                End If
                Return result
            End Get
        End Property
#End Region
#Region " Constructores "
        Sub New()

        End Sub
        Sub New(ByVal id As Integer)
            Dim objImportar As Usuario = TraerUno(id)
            ' DBE
            IdUsuarioAlta = objImportar.IdUsuarioAlta
            IdUsuarioBaja = objImportar.IdUsuarioBaja
            IdMotivoBaja = objImportar.IdMotivoBaja
            FechaAlta = objImportar.FechaAlta
            FechaBaja = objImportar.FechaBaja
            ' Entidad
            IdEntidad = objImportar.IdEntidad
            Nombre = objImportar.Nombre
            Apellido = objImportar.Apellido
            UserName = objImportar.UserName
            Password = objImportar.Password
            CorreoElectronico = objImportar.CorreoElectronico
            NroInterno = objImportar.NroInterno
            AccesosSistema = objImportar.AccesosSistema
            Observaciones = objImportar.Observaciones
        End Sub
        'Sub New(ByVal UserName As String, ByVal Password As String)
        '    Dim objImportar As Usuario = TraerUno(UserName, Password)
        '    If objImportar.FechaBaja.HasValue Then
        '        Throw New Exception("Usuario NO Autorizado")
        '    End If
        '    AlmacenarAccesoSistema(objImportar.IdEntidad)
        '    ' DBE
        '    IdUsuarioAlta = objImportar.IdUsuarioAlta
        '    IdUsuarioBaja = objImportar.IdUsuarioBaja
        '    IdMotivoBaja = objImportar.IdMotivoBaja
        '    FechaAlta = objImportar.FechaAlta
        '    FechaBaja = objImportar.FechaBaja
        '    ' Entidad
        '    IdEntidad = objImportar.IdEntidad
        '    Nombre = objImportar.Nombre
        '    Apellido = objImportar.Apellido
        '    UserName = objImportar.UserName
        '    Password = objImportar.Password
        '    CorreoElectronico = objImportar.CorreoElectronico
        '    NroInterno = objImportar.NroInterno
        '    ' Para que sume la Actual
        '    AccesosSistema = objImportar.AccesosSistema + 1
        '    Observaciones = objImportar.Observaciones
        'End Sub
        Sub New(ByVal DtODesde As DTO.DTO_Usuario)
            ' DBE
            IdUsuarioAlta = DtODesde.IdUsuarioAlta
            IdUsuarioBaja = DtODesde.IdUsuarioBaja
            IdMotivoBaja = DtODesde.IdMotivoBaja
            If DtODesde.FechaAlta > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaAlta.ToString, 2) + "/" + Left(Right(DtODesde.FechaAlta.ToString, 4), 2) + "/" + Left(DtODesde.FechaAlta.ToString, 4)
                FechaAlta = CDate(TempFecha)
            End If
            If DtODesde.FechaBaja > 0 Then
                Dim TempFecha As String = Right(DtODesde.FechaBaja.ToString, 2) + "/" + Left(Right(DtODesde.FechaBaja.ToString, 4), 2) + "/" + Left(DtODesde.FechaBaja.ToString, 4)
                FechaBaja = CDate(TempFecha)
            End If
            ' Entidad
            IdEntidad = DtODesde.IdEntidad
            Nombre = DtODesde.Nombre
            Apellido = DtODesde.Apellido
            UserName = DtODesde.UserName
            Password = DtODesde.Password
            CorreoElectronico = DtODesde.CorreoElectronico
            NroInterno = DtODesde.NroInterno
            AccesosSistema = DtODesde.AccesosSistema
            Observaciones = DtODesde.Observaciones
        End Sub
#End Region
#Region " Métodos Estáticos"
        ' Traer
        ''' <summary>
        ''' CU.Usuario.04 - Buscando Usuario
        ''' </summary>
        ''' <param name="Id"></param>
        ''' <returns></returns>
        Public Shared Function TraerUno(ByVal Id As Integer) As Usuario
            Dim result As Usuario = Todos.Find(Function(x) x.IdEntidad = Id)
            If result Is Nothing Then
                Throw New Exception("No existen Usuarios para la búsqueda")
            End If
            Return result
        End Function
        Public Shared Function TraerTodos() As List(Of Usuario)
            Return Todos
        End Function
        ' Nuevos
        ''' <summary>
        ''' CU.Usuario.06 – Accediendo al Sistema
        ''' </summary>
        Public Shared Function AccederAlSistema(ByVal UserName As String, ByVal Password As String) As Usuario
            Dim result As Usuario = Todos.Find(Function(x) x.UserName.Trim.ToUpper = UserName.Trim.ToUpper And x.Password.Trim = Password.Trim)
            If result Is Nothing Then
                Throw New Exception("Usuario y Contraseña inválido. Intente nuevamente.")
            End If
            result.RegistrarAccesoSistema()
            Return result
        End Function
        ''' <summary>
        ''' CU.Usuario.09 – Registrando Accesos al Sistema
        ''' </summary>
        Private Sub RegistrarAccesoSistema()
            DAL_Usuario.RegistrarAccesoSistema(Me.IdEntidad)
        End Sub
        ''' <summary>
        ''' CU. Perfil.06 – Agregando Perfil a Usuario
        ''' </summary>
        ''' <param name="IdPerfil"></param>
        Public Sub AgregarPerfil(IdPerfil As Integer)
            Dim ObjPerfil As New Perfil(IdPerfil)
            Dim ListaTemp As List(Of Perfil) = ListaPerfiles
            If Not ListaTemp Is Nothing AndAlso ListaTemp.Count > 0 Then
                Dim encontrado As Boolean = False
                For Each item As Perfil In ListaTemp
                    If item.IdEntidad = IdPerfil Then
                        encontrado = True
                        ObjPerfil = item
                    End If
                Next
                If Not encontrado Then
                    ObjPerfil.AltaPerfilEnUsuario(IdUsuarioModifica, IdEntidad)
                Else
                    If ObjPerfil.FechaBaja.HasValue Then
                        ObjPerfil.ModificarPerfilEnUsuario(IdUsuarioModifica, IdEntidad)
                    End If
                End If
            Else
                ObjPerfil.AltaPerfilEnUsuario(IdUsuarioModifica, IdEntidad)
            End If
        End Sub
        ''' <summary>
        ''' CU. Perfil.07 – Quitando Perfil a Usuario
        ''' </summary>
        ''' <param name="IdPerfil"></param>
        Public Sub QuitarPerfil(IdPerfil As Integer)
            Dim ObjPerfil As New Perfil(IdPerfil)
            Dim ListaTemp As List(Of Perfil) = ListaPerfiles
            If Not ListaTemp Is Nothing AndAlso ListaTemp.Count > 0 Then
                Dim encontrado As Boolean = False
                For Each item As Perfil In ListaTemp
                    If item.IdEntidad = IdPerfil Then
                        encontrado = True
                        ObjPerfil = item
                    End If
                Next
                If encontrado Then
                    If Not ObjPerfil.FechaBaja.HasValue Then
                        ObjPerfil.BajaPerfilEnUsuario(IdUsuarioModifica, IdEntidad)
                    End If
                End If
            End If
        End Sub
        Public Shared Sub AlmacenarAccesoFormulario(IdUsuario As Integer, IdFormulario As Integer)
            DAL_Usuario.AccesoFormulario(IdUsuario, IdFormulario)
        End Sub
        Public Shared Sub EnviarPassword(identificador As String)
            Dim result As Usuario = Todos.Find(Function(x) x.UserName.Trim.ToUpper = identificador.Trim.ToUpper Or x.CorreoElectronico.Trim.ToUpper = identificador.Trim.ToUpper)
            If result Is Nothing Then
                Throw New Exception("No Existen Usuarios con ese Identificador.")
            End If
            result.MailEnviarPassword()
        End Sub
        Public Shared Sub ModificaPassword(idUsuario As Integer, anterior As String, nueva As String)
            Dim ObjU As New Usuario(idUsuario)
            If Not ObjU Is Nothing Then
                If ObjU.Password.Trim.ToUpper = anterior.Trim.ToUpper Then
                    ValidarPassword(nueva)
                    ObjU.Password = nueva.Trim
                End If
            End If
            ObjU.IdUsuarioModifica = idUsuario
            DAL_Usuario.ModificaPassword(ObjU)
            ObjU.MailCambioPassword()
        End Sub
#End Region
#Region " Métodos Públicos"
        ' ABM
        ''' <summary>
        ''' CU.Usuario.01 - Creando Usuario
        ''' </summary>
        Public Sub Alta()
            ValidarAlta()
            DAL_Usuario.Alta(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU.Usuario.03 - Eliminando Usuario
        ''' </summary>
        Public Sub Baja()
            ValidarBaja()
            DAL_Usuario.Baja(Me)
            Refresh()
        End Sub
        ''' <summary>
        ''' CU.Usuario.02 - Modificando Usuario
        ''' </summary>
        Public Sub Modifica()
            ValidarModifica()
            DAL_Usuario.Modifica(Me)
            Me.MailNotificarCambios()
            Refresh()
        End Sub
        ''' <summary>
        ''' CU.Usuario.08 – Reseteando  Contraseña
        ''' </summary>
        Public Sub ResetearPassword()
            Dim fromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Dim smtpPasswordSecAdm As String = ConfigurationManager.AppSettings("smtpPassword").ToString
            Dim smtpFromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Using Mail As New MailMessage()
                Dim men As String = ""
                men = "Contraseña: " & vbCrLf & Me.Password & vbCrLf & "Saludos."
                Dim Smtp = New SmtpClient
                Mail.From = New MailAddress(smtpFromSecAdm, fromSecAdm)
                Mail.To.Add(New MailAddress(CorreoElectronico))
                Mail.Subject = "Reseteo Contraseña Sistema AAEMM"
                Mail.Body = men
                Mail.IsBodyHtml = False
                Mail.Priority = MailPriority.Normal
                Smtp.Host = "smtp.gmail.com"
                Smtp.Port = 587
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(smtpFromSecAdm, smtpPasswordSecAdm)
                Smtp.EnableSsl = True
                Smtp.Send(Mail)
            End Using
        End Sub
        ''' <summary>
        ''' Mails
        ''' </summary>
        Private Sub MailEnviarPassword()
            Dim fromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Dim smtpPasswordSecAdm As String = ConfigurationManager.AppSettings("smtpPassword").ToString
            Dim smtpFromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Using Mail As New MailMessage()
                Dim men As String = ""
                men = "Usted ha solicitado un recupero de Contraseña." & vbCrLf & vbCrLf &
                    "Usuario: " & Me.UserName & vbCrLf &
                    "Contraseña: " & Me.Password & vbCrLf &
                    vbCrLf & "Saludos !!" & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                    "Ingreso al Sistema: http://localhost:14162/Forms/Login/Frm_Login.aspx"
                Dim Smtp = New SmtpClient
                Mail.From = New MailAddress(smtpFromSecAdm, fromSecAdm)
                Mail.To.Add(New MailAddress(CorreoElectronico))
                Mail.Subject = "Sistema AAEMM. Recupero de Contraseña"
                Mail.Body = men
                Mail.IsBodyHtml = False
                Mail.Priority = MailPriority.Normal
                Smtp.Host = "smtp.gmail.com"
                Smtp.Port = 587
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(smtpFromSecAdm, smtpPasswordSecAdm)
                Smtp.EnableSsl = True
                Smtp.Send(Mail)
            End Using
        End Sub
        Private Sub MailNotificarCambios()
            Dim fromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Dim smtpPasswordSecAdm As String = ConfigurationManager.AppSettings("smtpPassword").ToString
            Dim smtpFromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Using Mail As New MailMessage()
                Dim men As String = ""
                men = "Usted ha modificado los datos de Usuario." & vbCrLf & vbCrLf &
                    "Nombre: " & Me.Nombre & vbCrLf &
                    "Apellido: " & Me.Apellido & vbCrLf &
                    "Usuario: " & Me.UserName & vbCrLf &
                    "Correo Electrónico: " & Me.CorreoElectronico & vbCrLf &
                    "Nro. Interno: " & Me.NroInterno & vbCrLf &
                    vbCrLf & "Saludos !!" & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                    "Ingreso al Sistema: http://localhost:14162/Forms/Login/Frm_Login.aspx"
                Dim Smtp = New SmtpClient
                Mail.From = New MailAddress(smtpFromSecAdm, fromSecAdm)
                Mail.To.Add(New MailAddress(CorreoElectronico))
                Mail.Subject = "Sistema AAEMM. Cambios de Usuario"
                Mail.Body = men
                Mail.IsBodyHtml = False
                Mail.Priority = MailPriority.Normal
                Smtp.Host = "smtp.gmail.com"
                Smtp.Port = 587
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(smtpFromSecAdm, smtpPasswordSecAdm)
                Smtp.EnableSsl = True
                Smtp.Send(Mail)
            End Using
        End Sub
        Private Sub MailCambioPassword()
            Dim fromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Dim smtpPasswordSecAdm As String = ConfigurationManager.AppSettings("smtpPassword").ToString
            Dim smtpFromSecAdm As String = ConfigurationManager.AppSettings("smtpFrom").ToString
            Using Mail As New MailMessage()
                Dim men As String = ""
                men = "Usted ha modificado su contraseña." & vbCrLf & vbCrLf &
                    "Usuario: " & Me.UserName & vbCrLf &
                    "Correo Electrónico: " & Me.CorreoElectronico & vbCrLf &
                    "Contraseña: " & Me.Password & vbCrLf &
                    vbCrLf & "Saludos !!" & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                    "Ingreso al Sistema: http://localhost:14162/Forms/Login/Frm_Login.aspx"
                Dim Smtp = New SmtpClient
                Mail.From = New MailAddress(smtpFromSecAdm, fromSecAdm)
                Mail.To.Add(New MailAddress(CorreoElectronico))
                Mail.Subject = "Sistema AAEMM. Cambio de Contaseña"
                Mail.Body = men
                Mail.IsBodyHtml = False
                Mail.Priority = MailPriority.Normal
                Smtp.Host = "smtp.gmail.com"
                Smtp.Port = 587
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(smtpFromSecAdm, smtpPasswordSecAdm)
                Smtp.EnableSsl = True
                Smtp.Send(Mail)
            End Using
        End Sub
        ' Otros
        Public Function ToDTO() As DTO.DTO_Usuario
            Dim result As New DTO.DTO_Usuario With {
                .IdEntidad = IdEntidad,
                .Nombre = Nombre,
                .Apellido = Apellido,
                .UserName = UserName,
                .Password = Password,
                .CorreoElectronico = CorreoElectronico,
                .NroInterno = NroInterno,
                .Observaciones = Observaciones,
                .IdEstado = IdEstado
            }
            Dim DTO_ListaPerfiles As New List(Of DTO.DTO_Perfil)
            If Not ListaPerfiles Is Nothing AndAlso ListaPerfiles.Count > 0 Then
                For Each item As Perfil In ListaPerfiles
                    DTO_ListaPerfiles.Add(item.ToDTO)
                Next
            End If
            result.ListaPerfiles = DTO_ListaPerfiles
            Return result
        End Function
        Public Shared Sub Refresh()
            _Todos = DAL_Usuario.TraerTodos
        End Sub
        ' Nuevos
#End Region
#Region " Métodos Privados "
        ' ABM
        Private Sub ValidarAlta()
            ValidarUsuario(Me.IdUsuarioAlta)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        Private Sub ValidarBaja()
            ValidarUsuario(Me.IdUsuarioBaja)
        End Sub
        Private Sub ValidarModifica()
            ValidarUsuario(Me.IdUsuarioModifica)
            ValidarCampos()
            ValidarNoDuplicados()
        End Sub
        ' Validaciones
        Private Sub ValidarUsuario(ByVal idUsuario As Integer)
            If idUsuario = 0 Then
                Throw New Exception("Debe ingresar al sistema")
            End If
        End Sub
        Private Sub ValidarCampos()
            Dim sError As String = ""
            ValidarCaracteres()
            ' Campo Integer/Decimal
            '	If Me.VariableNumero.toString = "" Then
            '   	sError &= "<b>VariableNumero</b> Debe ingresar VariableNumero. <br />"
            '	ElseIf Not isnumeric(Me.VariableNumero) Then
            '   	sError &= "<b>VariableNumero</b> Debe ser numérico. <br />"
            '	End If
            If Me.Nombre = "" Then
                sError &= "<b>Nombre</b> Debe ingresar su Nombre. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Nombre</b>Debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.Apellido = "" Then
                sError &= "<b>Apellido</b> Debe ingresar su Apellido. <br />"
            ElseIf Me.Nombre.Length > 50 Then
                sError &= "<b>Apellido</b> Debe tener como máximo 50 caracteres. <br />"
            End If
            If Me.UserName = "" Then
                sError &= "<b>Usuario</b> Debe ingresar su Usuario. <br />"
            ElseIf Me.UserName.Length > 20 Then
                sError &= "<b>Usuario</b> Debe tener como máximo 20 caracteres. <br />"
            Else
                If UserName.Contains(" ") Then
                    sError &= "<b>Usuario</b> No debe contener espacios. <br />"
                End If
            End If
            'If Me.Password = "" Then
            '    sError &= "<b>Contraseña</b> Debe ingresar su Contraseña. <br />"
            'ElseIf Me.Password.Length > 10 Then
            '    sError &= "<b>Contraseña</b> Debe tener como máximo 10 caracteres. <br />"
            'End If
            If Me.CorreoElectronico = "" Then
                sError &= "<b>Correo Electrónico</b> Debe ingresar su Correo Electrónico. <br />"
            Else
                If Not LUM.LUM.validateEmail(CorreoElectronico) Then
                    sError &= "<b>Correo Electrónico</b> Debe ser válido. <br />"
                Else
                    If Me.CorreoElectronico.Length > 100 Then
                        sError &= "<b>Correo Electrónico</b> Debe tener como máximo 100 caracteres. <br />"
                    End If
                End If
            End If
            If Me.NroInterno.ToString <> "" Then
                If Not IsNumeric(Me.NroInterno) Then
                    sError &= "<b>NroInterno</b> Debe ser numérico. <br />"
                End If
            End If
            If Me.Observaciones <> "" AndAlso Me.Observaciones.Length > 500 Then
                sError &= "<b>Observaciones</b> Debe tener como máximo 500 caracteres. <br />"
            End If
            ' Campo Date
            '	If Not Me.VariableFecha.has value Then
            '		sError &= "<b>VariableFecha</b> Debe ingresar VariableFecha. <br />"
            '	End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarCaracteres()
            Dim sError As String = ""
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
        Private Sub ValidarNoDuplicados()
            Usuario.Refresh()
            If Todos.Count > 0 Then
                Dim result As Usuario = Todos.Find(Function(x) x.UserName.ToString.ToUpper = UserName.ToString.ToUpper)
                If Not result Is Nothing Then
                    If IdEntidad = 0 Then
                        ' Alta
                        Throw New Exception("El UserName a ingresar ya existe")
                    Else
                        ' Modifica
                        If IdEntidad <> result.IdEntidad Then
                            Throw New Exception("El UserName a ingresar ya existe")
                        End If
                    End If
                End If
            End If
        End Sub
        Private Shared Sub ValidarPassword(PasswordNueva As String)
            Dim sError As String = ""
            If PasswordNueva = "" Then
                sError &= "<b>Contraseña</b> Debe ingresar la Contraseña. <br />"
            ElseIf PasswordNueva.Length > 10 Then
                sError &= "<b>Contraseña</b> Debe tener como máximo 10 caracteres. <br />"
            End If
            If sError <> "" Then
                sError = "<b>Debe corregir los siguientes errores</b> <br /> <br />" & sError
                Throw New Exception(sError)
            End If
        End Sub
#End Region
    End Class ' Usuario
End Namespace ' Entidad

Namespace DTO
    Public Class DTO_Usuario
        Inherits DTO_Dimensional

#Region " Atributos / Propiedades"
        Public Property IdEntidad() As Integer = 0
        Public Property Nombre() As String = ""
        Public Property Apellido() As String = ""
        Public Property UserName() As String = ""
        Public Property Password() As String = ""
        Public Property CorreoElectronico() As String = ""
        Public Property NroInterno() As Integer = 0
        Public Property AccesosSistema() As Integer = 0
        Public Property Observaciones() As String = ""
        Public Property ListaPerfiles() As List(Of DTO_Perfil)
#End Region
    End Class ' DTO_Usuario
End Namespace ' DTO

Namespace DataAccessLibrary
    Public Class DAL_Usuario

#Region " Stored "
        Const storeAlta As String = "USUARIO.p_Usuario_Alta"
        Const storeBaja As String = "USUARIO.p_Usuario_Baja"
        Const storeModifica As String = "USUARIO.p_Usuario_Modifica"
        Const storeModificaPassword As String = "USUARIO.p_Usuario_ModificaPassword"
        Const storeTraerTodos As String = "USUARIO.p_Usuario_TraerTodos"
        ' Otros
        Const storeRegistrarAccesoSistema As String = "USUARIO.p_Usuario_RegistrarAccesoSistema"
        Const storeRegistrarAccesoFormulario As String = "USUARIO.p_Usuario_AccesoFormulario"

#End Region
#Region " Métodos Públicos "
        ' ABM
        Public Shared Sub Alta(ByVal entidad As Usuario)
            Dim store As String = storeAlta
            Dim pa As New parametrosArray
            pa.add("@idUsuarioAlta", entidad.IdUsuarioAlta)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
            pa.add("@Apellido", entidad.Apellido.ToUpper.Trim)
            pa.add("@UserName", entidad.UserName.ToUpper.Trim)
            pa.add("@Password", entidad.Password.Trim)
            pa.add("@NroInterno", entidad.NroInterno)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToUpper.Trim)
            pa.add("@Observaciones", entidad.Observaciones.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Baja(ByVal entidad As Usuario)
            Dim store As String = storeBaja
            Dim pa As New parametrosArray
            pa.add("@idUsuarioBaja", entidad.IdUsuarioBaja)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@IdMotivoBaja", entidad.IdMotivoBaja)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub Modifica(ByVal entidad As Usuario)
            Dim store As String = storeModifica
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Nombre", entidad.Nombre.ToUpper.Trim)
            pa.add("@Apellido", entidad.Apellido.ToString.ToUpper.Trim)
            pa.add("@UserName", entidad.UserName.ToString.ToUpper.Trim)
            pa.add("@NroInterno", entidad.NroInterno)
            pa.add("@CorreoElectronico", entidad.CorreoElectronico.ToString.ToUpper.Trim)
            pa.add("@Observaciones", entidad.Observaciones.ToString.ToUpper.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Friend Shared Sub ModificaPassword(ByVal entidad As Usuario)
            Dim store As String = storeModificaPassword
            Dim pa As New parametrosArray
            pa.add("@idUsuarioModifica", entidad.IdUsuarioModifica)
            pa.add("@id", entidad.IdEntidad)
            pa.add("@Password", entidad.Password.ToString.Trim)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        entidad.IdEntidad = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        ' Traer
        'Public Shared Function TraerUno(ByVal id As Integer) As Usuario
        '    Dim store As String = storeTraerUno
        '    Dim result As New Usuario
        '    Dim pa As New parametrosArray
        '    pa.add("@id", id)
        '    Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
        '        If Not dt Is Nothing Then
        '            If dt.Rows.Count = 1 Then
        '                result = LlenarEntidad(dt.Rows(0))
        '            ElseIf dt.Rows.Count = 0 Then
        '                result = Nothing
        '            End If
        '        End If
        '    End Using
        '    Return result
        'End Function
        Public Shared Function TraerTodos() As List(Of Usuario)
            Dim store As String = storeTraerTodos
            Dim pa As New parametrosArray
            Dim listaResult As New List(Of Usuario)
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        listaResult.Add(LlenarEntidad(dr))
                    Next
                End If
            End Using
            Return listaResult
        End Function
        ' Otros
        Public Shared Sub RegistrarAccesoSistema(IdUsuario As Integer)
            Dim store As String = storeRegistrarAccesoSistema
            Dim pa As New parametrosArray
            pa.add("@idUsuario", IdUsuario)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
        Public Shared Sub AccesoFormulario(IdUsuario As Integer, IdFormulario As Integer)
            Dim store As String = storeRegistrarAccesoFormulario
            Dim pa As New parametrosArray
            pa.add("@idUsuario", IdUsuario)
            pa.add("@IdFormulario", IdFormulario)
            Dim result As Integer = 0
            Using dt As DataTable = Connection.Connection.TraerDt(store, pa)
                If Not dt Is Nothing Then
                    If dt.Rows.Count = 1 Then
                        result = CInt(dt.Rows(0)(0))
                    End If
                End If
            End Using
        End Sub
#End Region
#Region " Métodos Privados "
        Private Shared Function LlenarEntidad(ByVal dr As DataRow) As Usuario
            Dim entidad As New Usuario
            ' DBE
            If dr.Table.Columns.Contains("idUsuarioAlta") Then
                If dr.Item("idUsuarioAlta") IsNot DBNull.Value Then
                    entidad.IdUsuarioAlta = CInt(dr.Item("idUsuarioAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioBaja") Then
                If dr.Item("idUsuarioBaja") IsNot DBNull.Value Then
                    entidad.IdUsuarioBaja = CInt(dr.Item("idUsuarioBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("idUsuarioModifica") Then
                If dr.Item("idUsuarioModifica") IsNot DBNull.Value Then
                    entidad.IdUsuarioModifica = CInt(dr.Item("idUsuarioModifica"))
                End If
            End If
            If dr.Table.Columns.Contains("IdMotivoBaja") Then
                If dr.Item("IdMotivoBaja") IsNot DBNull.Value Then
                    entidad.IdMotivoBaja = CInt(dr.Item("IdMotivoBaja"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaAlta") Then
                If dr.Item("fechaAlta") IsNot DBNull.Value Then
                    entidad.FechaAlta = CDate(dr.Item("fechaAlta"))
                End If
            End If
            If dr.Table.Columns.Contains("fechaBaja") Then
                If dr.Item("fechaBaja") IsNot DBNull.Value Then
                    entidad.FechaBaja = CDate(dr.Item("fechaBaja"))
                End If
            End If
            ' Entidad
            If dr.Table.Columns.Contains("id") Then
                If dr.Item("id") IsNot DBNull.Value Then
                    entidad.IdEntidad = CInt(dr.Item("id"))
                End If
            End If
            If dr.Table.Columns.Contains("Nombre") Then
                If dr.Item("Nombre") IsNot DBNull.Value Then
                    entidad.Nombre = dr.Item("Nombre").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Apellido") Then
                If dr.Item("Apellido") IsNot DBNull.Value Then
                    entidad.Apellido = dr.Item("Apellido").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("UserName") Then
                If dr.Item("UserName") IsNot DBNull.Value Then
                    entidad.UserName = dr.Item("UserName").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("Password") Then
                If dr.Item("Password") IsNot DBNull.Value Then
                    entidad.Password = dr.Item("Password").ToString.Trim
                End If
            End If
            If dr.Table.Columns.Contains("CorreoElectronico") Then
                If dr.Item("CorreoElectronico") IsNot DBNull.Value Then
                    entidad.CorreoElectronico = dr.Item("CorreoElectronico").ToString.ToUpper.Trim
                End If
            End If
            If dr.Table.Columns.Contains("NroInterno") Then
                If dr.Item("NroInterno") IsNot DBNull.Value Then
                    entidad.NroInterno = CInt(dr.Item("NroInterno"))
                End If
            End If
            If dr.Table.Columns.Contains("AccesosSistema") Then
                If dr.Item("AccesosSistema") IsNot DBNull.Value Then
                    entidad.AccesosSistema = CInt(dr.Item("AccesosSistema"))
                End If
            End If
            If dr.Table.Columns.Contains("Observaciones") Then
                If dr.Item("Observaciones") IsNot DBNull.Value Then
                    entidad.Observaciones = dr.Item("Observaciones").ToString.ToUpper.Trim
                End If
            End If
            Return entidad
        End Function
#End Region
    End Class ' DAL_Usuario
End Namespace ' DataAccessLibrary