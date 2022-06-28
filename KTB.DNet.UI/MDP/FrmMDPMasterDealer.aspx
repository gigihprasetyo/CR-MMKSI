<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMDPMasterDealer.aspx.vb" Inherits="FrmMDPMasterDealer" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MDP-Master Dealer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript">

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
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
                <td style="height: 17px" class="titlePage">MDP Master Dealer</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1" alt=""></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1" alt=""></td>
            </tr>
            <tr>
                <td style="height: 188px">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td style="width: 138px; height: 17px" class="titleField" width="138">
                                <asp:Label ID="lblDealer" runat="server">Kode Organisasi</asp:Label></td>
                            <td style="height: 17px" width="1%">:</td>
                            <td style="width: 311px;">
                                <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>

                        </tr>
                        <tr>
                            <td style="width: 108px; height: 17px" class="titleField" width="108">
                                <asp:Label runat="server">Nama Dealer</asp:Label></td>
                            <td style="height: 17px" width="1%">:</td>
                            <td style="width: 220px; height: 17px">
                                <asp:TextBox ID="txtNamaDealer" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 108px; height: 17px" class="titleField">
                                <asp:Label runat="server">Status</asp:Label></td>
                            <td style="height: 17px">:</td>
                            <td style="width: 120px; height: 17px">
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Width="200px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td style="width: 108px">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal"></asp:Button></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 120px"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 1px">
                    <div id="div1" style="width: 100%; height: 238px; overflow: auto">
                        <asp:DataGrid ID="dtgMain" runat="server" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True"
                            PageSize="25" AllowPaging="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
                            <SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
                            <EditItemStyle VerticalAlign="Top"></EditItemStyle>
                            <AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="IdList" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
                                    <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKota" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.SearchTerm1" HeaderText="Search 1/2">
                                    <HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSearch12" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Tanggal Update">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglUpdate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnPopUp" runat="server" Width="2%" CausesValidation="False"
                                            CommandName="btnHistDealer">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="History Dealer"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center"></td>
            </tr>
        </table>
    </form>
    <script language="javascript" type="text/javascript">
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
