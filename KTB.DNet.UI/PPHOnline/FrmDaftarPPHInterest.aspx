<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarPPHInterest.aspx.vb" Inherits=".FrmDaftarPPHInterest" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PPH Interest</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = selectedDealer;
        }
		</script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">Daftar PPH Interest</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>    
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="width:200px">Tanggal Invoice</td>
                            <td class="titleField">:</td>
                            <td class="titleField">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="width:100px">Status</td>
                            <td class="titleField">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="width:200px">Kode Dealer</td>
                            <td class="titleField">:</td>
                            <td>
                                <asp:TextBox ID="txtDealerCode" runat="server"></asp:TextBox>
                                <asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection()"></asp:label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="width:100px">No SO</td>
                            <td class="titleField">:</td>
                            <td>
                                <asp:TextBox ID="txtSONumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Cari" />
                    <br />
                </td>
            </tr>
            <tr>
                <asp:DataGrid ID="dgListSOInterest" runat="server" Width="100%" AllowPaging="True" PageSize="25" AllowCustomPaging="True" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
                    CellSpacing="1" AllowSorting="True">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
                    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn Visible="False" HeaderText="ID">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text=''>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text=''>
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn>
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkAdd', document.all.chkAllItems.checked)"
                                    type="checkbox">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAdd" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn SortExpression="ID" HeaderText="No">
                            <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="PONumber" HeaderText="Dealer PO No">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoPO" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="SONumber" HeaderText="SO No">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblSO" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        

                        <asp:TemplateColumn SortExpression="BillingNumber" HeaderText="Billing No">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBilling" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="TrType" HeaderText="Type">
                            <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                                                
                        <asp:TemplateColumn  HeaderText="%">
                            <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPercentage" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="DPPAmount" HeaderText="DPP">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDPP" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="PPHAmount" HeaderText="PPH">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPPH" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Nilai Setelah PPH">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAfterPPH" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Tanggal Billing">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBillingDate" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                          <asp:TemplateColumn HeaderText="Doc Number">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDocNumber" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>


                        <asp:TemplateColumn SortExpression="Status" HeaderText="Status Terakhir">
                            <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblLastStatus" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="No Pengajuan Terakhir">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblLastSubmission" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="History" Visible="false">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblHistory" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                    </Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnDownload" runat="server" Text="Download" />
                    <asp:Button ID="btnGenerateBuktiPotong" runat="server" Text="Generate Bukti potong" />

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
