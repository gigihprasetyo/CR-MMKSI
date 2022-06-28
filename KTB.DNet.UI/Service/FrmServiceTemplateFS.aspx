<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmServiceTemplateFS.aspx.vb" Inherits=".FrmServiceTemplateFS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Service Template</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = data[0];
        }

        function showDetail(dealerCode, vechileTypeId, fskindId, templateType) {
            showPopUp("../PopUp/PopUpServiceTemplateDetail.aspx?vTypeId=" + vechileTypeId + "&vFsKindId=" + fskindId + "&DealerCode=" + dealerCode + "&TemplateType=" + templateType, '', 500, 760);
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service Template</td>
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
                <td align="left" valign="top">
                    <table cellspacing="3" cellpadding="3" width="773" border="0" style="width: 773px; height: 64px">
                        <tr>
                            <td class="titleField" style="width: 150px">Kode Dealer</td>
                            <td>:</td>
                            <td valign="top">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server" ToolTip="Dealer Search"></asp:TextBox>
                                <asp:Label ID="lblPopupDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
					                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlTipe" runat="server">
                                    <asp:ListItem Value="0">Silahkan Pilih</asp:ListItem>
                                   <asp:ListItem Value="Free Service">Free Service</asp:ListItem>
                                   <asp:ListItem Value="Periodical Maintenance">Periodical Maintenance</asp:ListItem>
                                   <asp:ListItem Value="Field Fix Campaign">Field Fix Campaign</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Code</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKode" runat="server" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kategori</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKategori" runat="server" OnSelectedIndexChanged="ddlKategori_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="ddlVehicleModel" runat="server" OnSelectedIndexChanged="ddlVehicleModel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Kendaraan</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlVehicleType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px" OnClick="btnCari_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:DataGrid ID="dgServiceTemplate" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="10" 
                            OnPageIndexChanged="dgServiceTemplate_PageIndexChanged"
                            OnSortCommand="dgServiceTemplate_SortCommand"
                            OnItemDataBound="dgServiceTemplate_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="VechileTypeCode">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipe" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.VechileTypeCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe" SortExpression="TemplateType">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TemplateType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Code" SortExpression="KindCode">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="LaborDuration" HeaderText="Durasi Labor (Jam)">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDurasi" runat="server" ToolTip='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.LaborDuration"), 2)%>' Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.LaborDuration"), 2)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="LaborCost" HeaderText="Harga Labor">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCost" runat="server" ToolTip='<%# FormatCurrency(DataBinder.Eval(Container, "DataItem.LaborCost"), 2)%>' Text='<%# FormatCurrency(DataBinder.Eval(Container, "DataItem.LaborCost"), 2)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Valid Dari">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValidFrom" runat="server" ToolTip='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ValidFrom"), Microsoft.VisualBasic.DateFormat.ShortDate)%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ValidFrom"), Microsoft.VisualBasic.DateFormat.ShortDate)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Detail Spare Part">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDetail" runat="server">
                                            <img src="../images/detail.gif" border="0" alt="View Detail">
                                        </asp:LinkButton>
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
</body>
</html>
