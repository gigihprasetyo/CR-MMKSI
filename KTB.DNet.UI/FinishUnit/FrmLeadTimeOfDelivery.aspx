<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmLeadTimeOfDelivery.aspx.vb" Inherits=".FrmLeadTimeOfDelivery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDOStatusList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$');
        }
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }

    </script>
    <script type="text/javascript">

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
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td colspan="10">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage">Master Data - Lead Time Pengiriman</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="5%">&nbsp;Kode Dealer</td>
                <td style="width: 1%">:</td>
                <td width="40%">
                    <asp:TextBox ID="txtKodeDealer" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeDealer');" runat="server" Width="152px"></asp:TextBox>
                    <%--&nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="5%">&nbsp;Kode Destinasi</td>
                <td style="width: 1px">:</td>
                <td width="40%">
                    <asp:TextBox ID="txtKodeDest" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeDest');" runat="server" Width="152px"></asp:TextBox>
                    <%--&nbsp;<asp:Label ID="Label1" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="5%">&nbsp;Kode Dealer Destinasi</td>
                <td style="width: 1%">:</td>
                <td width="40%">
                    <asp:TextBox ID="TxtDealerDestination" onkeypress="TxtKeypress();" onblur="TxtBlur('TxtDealerDestination');" runat="server" Width="152px"></asp:TextBox>
                    <%--&nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="5%"></td>
                <td style="width: 1px"></td>
                <td width="40%">
                    <asp:Button ID="btnCari" runat="server" Width="80px" Text="Cari"></asp:Button>
                </td>

            </tr>

        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 300px;">
                        <asp:DataGrid ID="dgDeliveryOrder" runat="server" Width="100%" BorderColor="#E0E0E0"
                            BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" CellSpacing="1"
                            AutoGenerateColumns="False" PageSize="25" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Destinasi">
                                    <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDest" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer Destinasi">
                                    <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDestination" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDestinationCode.DealerCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama">
                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nama")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alamat">
                                    <HeaderStyle Width="18%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlamat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Alamat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kota">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City.CityName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Regional">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RegionDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Lead Time (Hari)">
                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LeadTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnDownload" runat="server" Width="80px" Text="Download"></asp:Button>
                </td>
            </tr>
        </table>
        </td>
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
