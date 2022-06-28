<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPODestination.aspx.vb" Inherits="FrmPODestination" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPODestination</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
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
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="titlePage">UMUM - Master Destination</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr runat="server" id="trDealer">
                            <td class="titleField">Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" Style="z-index: 0" ID="txtKodeDealer" Width="150px"
                                    onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:TextBox>
                                <asp:Label Style="z-index: 0" ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Kode Dealer Destinasi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDealerDestination)" ID="txtDealerDestination"
                                    runat="server" MaxLength="50" Width="150px"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Region</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtRegion)" ID="txtRegion"
                                    runat="server" MaxLength="50" Width="150px"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Kode Destinasi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKodeDestination)" ID="txtKodeDestination"
                                    runat="server" MaxLength="50" Width="150px"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nama Destinasi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtNamaDestinasi)" ID="txtNamaDestination"
                                    runat="server" MaxLength="50" Width="200px"></asp:TextBox>&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="height: 320px; overflow: auto" id="div1">
                        <asp:DataGrid ID="dgPODestination" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                            BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" CellSpacing="1"
                            AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="#4A3C8C" VerticalAlign="Top" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer Destinasi">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDesCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerDestinationCode.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="RegionDesc" SortExpression="RegionDesc" HeaderText="Region">
                                    <HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Code" SortExpression="Code" ReadOnly="True" HeaderText="Kode Destinasi">
                                    <HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Nama" SortExpression="Nama" HeaderText="Nama Destinasi">
                                    <HeaderStyle Width="14%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Alamat" SortExpression="Alamat" ReadOnly="True" HeaderText="Alamat">
                                    <HeaderStyle Width="18%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40" align="left">
                    <asp:Button ID="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:Button></td>
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
