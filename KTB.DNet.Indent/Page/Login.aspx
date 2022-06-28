<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/MLogin.Master" CodeBehind="Login.aspx.vb" Inherits="KTB.DNet.Indent.Login" Async="true" %>



<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4></h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="control-group" style="display:inline;">
                        <asp:Label runat="server" AssociatedControlID="txtSPK" CssClass="col-md-2 control-label">Kode Booking</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtSPK" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSPK"
                                CssClass="text-danger" ErrorMessage="Harap di isi dengan Kode Booking" />
                        </div>
                    </div>
                    <div class="control-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Password diperlukan" />
                        </div>
                    </div>

                    <div class="control-group">
                        <asp:Label runat="server" AssociatedControlID="CodeNumberTextBox" CssClass="col-md-2 control-label">Captcha</asp:Label>
                        <div class="col-md-10">
                            <%--<table style="border: none; padding: 0; margin: 0;">
                                <tr>
                                    <td>--%>
                              <div class="control-group" style="display:inline-block">
                                        <asp:Image ID="captchaImg" runat="server" Width="120px" Height="32px" AlternateText="Input Captcha"></asp:Image>
                            <%--</td>
                                    <td>--%>
                                  <asp:LinkButton runat="server" ID="f5" ToolTip="Refresh" CausesValidation="false">
                                        <asp:Image ID="Image1" runat="server" Width="16px" Height="16px" ImageUrl="../Content/rsz_refresh.png" AlternateText="Refresh"></asp:Image>
                                      </asp:LinkButton>
                                  </div>
<%--                                    </td>
                                </tr>
                            </table>--%>

                            <asp:TextBox runat="server" ID="CodeNumberTextBox" TextMode="Password" CssClass="form-control" onmouseover="return escape('<b>Deskripsi:</b> <br>Huruf sepanjang 5 karakter.<br>Semua karakter akan dianggap huruf kapital.<br><b>Contoh:</b><br> SHRDLU <BR>EtAoIn')" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="CodeNumberTextBox" CssClass="text-danger" ErrorMessage="Captcha diperlukan" />
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Lanjutkan" CssClass="btn btn-default" Style="text-decoration-color: white; color: white; background-color: blue; text-decoration: double; font-weight: bold;" />
                        </div>
                    </div>
                </div>

            </section>
        </div>


    </div>
</asp:Content>
