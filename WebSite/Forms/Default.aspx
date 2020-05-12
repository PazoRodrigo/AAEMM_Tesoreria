<%@ Page Title="Default" Language="VB" MasterPageFile="~/Forms/MP_Inicio.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Forms_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="Upload" />
            </Triggers>
            <ContentTemplate>
                <br />
                <table class="mGridBusqueda" style="width: 400px;">
                    <tr>
                        <th colspan="2">
                            <asp:Label ID="Label7" runat="server" Font-Names="Tahoma" Text="Subir Archivo - CL / SE / RE"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Names="Tahoma" Text="Archivo"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Upload" runat="server" Text="Subir" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
                &nbsp;<br />
                &nbsp;
         <%--   <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="Upload">
            </cc1:ModalPopupExtender>--%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Upload" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>
