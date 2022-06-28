<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrReminderNew.aspx.vb" Inherits="FrmTrReminderNew" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTrReminder</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
        function ShowPopupDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, dealerSelection);
        }


        function dealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var lblDealerName = document.getElementById("lblDealerName");
            var hdnDealerCode = document.getElementById("hdnDealerCode");
            txtKodeDealer.value = data[0];
            lblDealerName.innerHTML = data[1];
            hdnDealerCode.value = data[0];

        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
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
                <td valign="top">
                    <table id="Table2" cellspacing="1" cellpadding="1" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td nowrap width="420">
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="100px" onkeypress="return false;" BackColor="LightGray"></asp:TextBox>
                                <asp:Label ID="lblPopUpKodeDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPopupDealer()" border="0" /></asp:Label>
                                <asp:HiddenField id="hdnDealerCode" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Dealer</td>
                            <td>:</td>
                            <td width="420">
                                <p>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kota</td>
                            <td>:</td>
                            <td width="420">
                                <p>
                                    <asp:Label ID="lblCity" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Periode</td>
                            <td>:</td>
                            <td width="420">
                                <asp:Label ID="lblPeriode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td width="420">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" width="75px">Tahun</td>
                            <td>:</td>
                            <td width="150" height="30px">
                                <asp:TextBox ID="txtTahun" Width="90%" onkeypress="return numericOnlyUniv(event)" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtTahun"></asp:RequiredFieldValidator>
                            </td>
                            <td class="titleField" width="75px">Bulan</td>
                            <td>:</td>
                            <td width="150">
                                <asp:DropDownList ID="ddlBulan" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                            <td class="titleField" width="75px">Kelas</td>
                            <td>:</td>
                            <td width="150">
                                <asp:DropDownList ID="ddlKelas" Width="90%" runat="server"></asp:DropDownList></td>
                            <td>
                                <asp:Button ID="btnSearch" Width="75px" runat="server" Text="Cari" /></td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td valign="top">
                    <asp:DataGrid ID="dtgReminder" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                        AllowCustomPaging="True" AllowSorting="True" PageSize="75" ForeColor="GhostWhite" CellSpacing="1"
                        Font-Names="MS Reference Sans Serif" Width="100%">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Daftar">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No Reg">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTraineeID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama">
                                <HeaderStyle Width="22%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrClass.ID" HeaderText="Kelas">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFinishDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
                                <HeaderStyle Width="22%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 10px" valign="top" align="left">
                    <asp:Label ID="lblNotes" runat="server">Notes</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <asp:TextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine" ReadOnly="True" Rows="2"
                        BorderStyle="None" BackColor="#E0E0E0" Height="40px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center" height="40">&nbsp;
						<asp:Button ID="btnCetak" runat="server" Text="Cetak"></asp:Button>&nbsp;&nbsp;&nbsp;
						<asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
