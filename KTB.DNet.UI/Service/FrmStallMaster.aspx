<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmStallMaster.aspx.vb" Inherits="FrmStallMaster" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmStallMaster</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionStallMaster.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var txtNamaDealerSelection = document.getElementById("txtNamaDealer");
            var hdnterm1 = document.getElementById("HiddenField1");
            var hdnterm2 = document.getElementById("HiddenField2");
            txtDealerSelection.value = temp[0];
            txtNamaDealerSelection.value = temp[1];
            hdnterm1.value = temp[0];
            hdnterm2.value = temp[1];

        }
        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:TextBox ID="temp" Visible="False" runat="server"></asp:TextBox>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">Stall and Service Booking&nbsp;- Stall Master</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="3" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <%--<img height="1" src="../images/dot.gif" border="0"></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table3" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr>
                <td class="titleField" style="width: 200px;">Kode Dealer</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:TextBox ID="txtKodeDealer" OnTextChanged="txtKodeDealer_TextChanged" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                        runat="server" ReadOnly="true"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Nama Dealer</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNamaDealer" runat="server" Width="160px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label1" runat="server">Kode Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtKodeStall" runat="server" Width="134px" ReadOnly="false" ToolTip="Auto Generate"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label2" runat="server">Kode Stall Dealer</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:TextBox ID="txtKodeStallDealer" runat="server" Width="134px" MaxLength="13"></asp:TextBox>
                    <%--</div>--%>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label3" runat="server">Nama Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNamaStall" runat="server" Width="134px"></asp:TextBox>&nbsp;</td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label4" runat="server">Lokasi Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:DropDownList runat="server" ID="ddlLokasiStall" Width="120">
                        <%-- <asp:ListItem>Silahkan Pilih</asp:ListItem>
                                <asp:ListItem>Inside</asp:ListItem>
                                <asp:ListItem>Outside</asp:ListItem>--%>
                    </asp:DropDownList>
                    <%--</div>--%>
                </td>

            </tr>

            <tr>
                <td class="titleField">
                    <asp:Label ID="Label5" runat="server">Tipe Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTipeStall" Width="120">
                        <%--<asp:ListItem>Silahkan Pilih</asp:ListItem>
                        <asp:ListItem>MQP</asp:ListItem>
                        <asp:ListItem>Washing</asp:ListItem>
                        <asp:ListItem>Booking</asp:ListItem>
                        <asp:ListItem>Walk In</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label6" runat="server">Kategori Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:DropDownList runat="server" ID="ddlKategoriStall" Width="120">
                        <%--<asp:ListItem>Silahkan Pilih</asp:ListItem>
                                <asp:ListItem>Stall Lift</asp:ListItem>
                                <asp:ListItem>Without Lift</asp:ListItem>
                                <asp:ListItem>Washing</asp:ListItem>--%>
                    </asp:DropDownList>
                    <%--</div>--%>
                </td>

            </tr>

            <tr>
                <td class="titleField">
                    <asp:Label ID="Label7" runat="server">Body Paint</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlBodyPaint" Width="120">
                        <%--<asp:ListItem>Silahkan Pilih</asp:ListItem>
                        <asp:ListItem Value="1">Ya</asp:ListItem>
                        <asp:ListItem Value="2">Tidak</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label8" runat="server">Status</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:DropDownList runat="server" ID="ddlStatus" Width="120" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        <%--<asp:ListItem>Silahkan Pilih</asp:ListItem>
                                <asp:ListItem Value="1">Aktif</asp:ListItem>
                                <asp:ListItem Value="2">Tidak Aktif</asp:ListItem>--%>
                    </asp:DropDownList>
                    <%--</div>--%>
                </td>

            </tr>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="75px" Text="Simpan" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCari" runat="server" Width="75px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                    <asp:Button ID="btnBatal" runat="server" Width="75px" Text="Batal" OnClick="btnBatal_Click"></asp:Button>

                    <asp:HiddenField runat="server" ID="hdnValid" Value="0" />
                                        <input id="hdConfirm" type="hidden" value="-1" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="dtgStallMaster" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                        AllowPaging="True">
                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle VerticalAlign="Top"></HeaderStyle>
                        <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbKodeStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Stall Dealer">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbKodeStallDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallCodeDealer")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNamaStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Lokasi Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbLokasiStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallLocation")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbTipeStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kategori Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbKategoriStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StallCategory")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Body Paint">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbBodyPaint" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsBodyPaint")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Detail">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
										<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False" OnClick="btnDownload_Click"></asp:Button></td>
            </tr>
        </table>
        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
    </form>
</body>
</html>
