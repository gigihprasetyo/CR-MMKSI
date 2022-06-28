<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEmailPaymentTransfer.aspx.vb" Inherits=".FrmEmailPaymentTransfer" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form Vehicle Type</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
	</script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">TRANSFER - Penerima Email Notifikasi Payment Transfer</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" width="100%" cellpadding="2" border="0">
                        <tbody>
                            <TR>
								<td class="titleField" width="24%">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeDealer" runat="server" Width="256px"></asp:textbox>
                                    <asp:label id="lblPopUpDealer" runat="server" width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
                                </TD>
							</TR>
                            <tr>
                                <td class="titleField" width="24%">Nama</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKode)" ID="txtName"
                                        runat="server" size="50" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Email</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKode)" ID="txtEmail"
                                        runat="server" size="50" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                            ErrorMessage="* " EnableClientScript="False"></asp:RequiredFieldValidator></td>
                            </tr>

                            <tr>
                                <td class="titleField" style="height: 23px" width="24%">Tipe</td>
                                <td style="height: 23px" width="1%">:</td>
                                <td style="height: 23px" width="75%">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="140" AutoPostBack="True"></asp:DropDownList></td>
                            </tr>
                            <%--<tr>
                                <td class="titleField">Grup Notifikasi</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlGroup" runat="server" Width="140" AutoPostBack="True"></asp:DropDownList></td>
                            </tr>--%>
                            <tr valign="top">
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"  ></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>&nbsp;
										<asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:Button></td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 360px">
                        <asp:DataGrid ID="dtgEmail" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None"
                            BorderWidth="0px" BackColor="#CDCDCD" GridLines="None" BorderColor="#CDCDCD" CellPadding="3" PageSize="50" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" CellSpacing="1" Font-Names="Microsoft Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
                                BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email">
                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Tipe">
                                    <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblType"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn HeaderText="Grup Notifikasi">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGroup"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                            CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
                                            CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
