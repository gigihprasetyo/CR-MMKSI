<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerSalesOrganization.aspx.vb" Inherits=".FrmDealerSalesOrganization" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Service Grade</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPopUp() {
        }

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
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Umum - Sales Organization</td>
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
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 110px; height: 14px" width="110">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="30%">
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="170px" onkeypress="return alphaNumericExcept(event,'<>?*%$');"
                                    onblur="omitSomeCharacter('txtKodeDealer','<>?*%$');"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td class="titleField" style="width: 110px; height: 14px" width="110">Distric</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="120px"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField">Sales Organization</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBoxList ID="chkSalesOrg" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                            </td>
                            <td class="titleField" style="width: 110px; height: 14px" width="110">Customer Group</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="120px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField">Distribution Channel</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBoxList ID="chkDistributionChannel" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                        
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button>
                                &nbsp;
                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="height: 300px; overflow: auto">
                        <asp:DataGrid ID="dgSalesOrganization" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowPaging="true" AllowSorting="True" PageSize="10" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealerBranch" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Sales Organization">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table id="tblSalesOrganization" runat="server" width="100%">
                                            
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Distribution Channel">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table id="tblDistributionChannel" runat="server" width="100%">
                                           
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sales Distric">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table id="tblSalesDistric" runat="server" width="100%">
                                           
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Customer Group">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table id="tblCustomerGroup" runat="server" width="100%">
                                            
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td style="height: 40px" align="left">&nbsp;
						<asp:Button ID="btnDownload" runat="server" Width="70px" Text="Download" Height="24px" Enabled="False"></asp:Button></td>
            </tr>--%>
        </table>
    </form>
</body>
</html>

