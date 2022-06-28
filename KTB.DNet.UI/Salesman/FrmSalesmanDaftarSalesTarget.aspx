<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="domain" Namespace="KTB.DNet.Domain" Assembly="KTB.DNet.Domain" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSalesmanDaftarSalesTarget.aspx.vb" Inherits=".FrmSalesmanDaftarSalesTarget" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ListContract</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = temp[0];
        }
    </script>
</head>
<body>
    <body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
        <form id="Form1" method="post" runat="server">
            <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 17px">Pesanan Kendaraan&nbsp;-&nbsp; 
						Daftar Dealer Sales Target</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="height: 6px" height="6">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" width="10%">
                                    <asp:Label ID="lblDealerCode" runat="server">Kode Dealer</asp:Label></td>
                                <td width="1%">
                                    <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblCategory" runat="server"> Kategori</asp:Label></td>
                                <td>
                                    <asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lbPKT" runat="server" Text="Tgl Mulai Berlaku"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                                <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icHandoverDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icHandoverDateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="5%"/>
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" colspan="6">
                                    <div id="div1" style="overflow: auto; height: 360px">
                                        <asp:DataGrid ID="dgSaleStarget" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                            BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                            AllowCustomPaging="True">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="VehicleModel.ID" HeaderText="Model Kendaraan">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleModel.VechileModelIndCode")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Sequence" HeaderText="Sequence">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerBranch" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Sequence")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="FreeDays" HeaderText="Free Days">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FreeDays") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="MaxQuantity" HeaderText="Max Quantity">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPKCreatedDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxQuantity")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Valid From">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidFrom" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ValidFrom"),"dd/MM/yyyy")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="MaxTOPDay" HeaderText="Max TOP Day">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaxTOPDay" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxTOPDay")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
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
