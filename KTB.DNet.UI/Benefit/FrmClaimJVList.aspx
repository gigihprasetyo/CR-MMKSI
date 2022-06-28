<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmClaimJVList.aspx.vb" Inherits=".FrmClaimJVList" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Pencairan Claim</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var hdnTemporaryOutlet = document.getElementById("hdnDealer");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeDealer.value = selectedRefNumber;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">SALES CAMPAIGN -&nbsp; DAFTAR PENCAIRAN CLAIM</td>
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
                <td align="left">
                    <table id="Table5" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="5%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="50%">
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                <asp:LinkButton ID="lnkBtnPopUpDealer" runat="server" Width="16px">
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="5%">No. Kuitansi</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtNoReceipt" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="5%">No. JV</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtNoJV" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="5%">No. Reg Claim</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtNoReg" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="5%">Status Pencairan</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="5%" style="height: 18px">Periode Kuitansi</td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeStart" runat="server" textboxwidth="70"></cc1:inticalendar>
                                        </td>
                                        <td valign="Top">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeEnd" runat="server" textboxwidth="70"></cc1:inticalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="7%" style="height: 18px">Periode Posting JV</td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:CheckBox ID="cbPaymentDate" runat="server" Width="2px"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <cc1:inticalendar id="icPaymentDateStart" runat="server" textboxwidth="70"></cc1:inticalendar>
                                        </td>
                                        <td valign="Top">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icPaymentDateEnd" runat="server" textboxwidth="70"></cc1:inticalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>&nbsp;
                                <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 20px"></asp:Button>&nbsp;
                                <asp:Button ID="btnDownloadExcelJV" runat="server" Text=" Download JV " Style="margin-left: 20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListJV" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                type="checkbox">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="BenefitClaimHeader.Dealer.DealerCode">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Term Cari 1" SortExpression="BenefitClaimHeader.Dealer.SearchTerm1">
                        <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Reg Claim" SortExpression="BenefitClaimHeader.ClaimRegNo">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblClaimRegNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Rencana Pembayaran">
                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Aktual Pembayaran">
                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblActualPaymentDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Kuitansi" SortExpression="ReceiptNo">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblNoReceive" runat="server" CommandName="ViewKuitansi" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No JV">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoJV" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkJV" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="addJv" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
								<img width="16" alt="JV" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACO0lEQVR42mNkgALH4Pj/TEzMDEzMQAyimZiQ2Kj81dNaGWH6GM+cOWMFpH1K2ydXzuqoYWBhYWFgZWXFSYdl12AYMPP27du2s9bs0AQJkOoKsAFr165Ni4yMZFi+fDnDHxk9BmJBd3YkI9iA/fv3q929d89BTVWV4Tm7JEOamwlBr5RNWwE3IB1oWDwfv4DlxUtXGE69+M7w/cd/BnM9ZYa///4z/P/3F0z/+w/EIPrfPzj71oMXCC8YGhoyvHv3jsHFxYVhz549DH5RCQyvvv5k+PPnH8Pfv/8YfgPxn79/wew/QCzKw86wcs95iAGXLl1SY2fncHj37i3DvUdPGRj+/GQoqm9luPTgOYOXvhKG35cdusKgKiPGsGr3GUQ0ComIVF6/84jB3tqM4fydZwyyUuIMRy7fZvC30ccwYMmOEwwGagoMq3adZABHRWVl5X+YF2DANTiaYcfx8wyB7raYLti4j8FMV4Nhzc6jEAMKCwsTTExM5yN7IbOigWHt3mMM/4EBBkQQGqQYyrcx1mdYu+MQAzxF3Xvw4P/VW/cZbC3NGKqmr2PIjfFhuPvqEzzQwAH4B8IGicmI8DCs3X4AYQAoMIE4Dca38QlluP3iI8SAP3+hhvyDGviXQU6Uj2Hdtr2oBhw6eS7t8YN7YC8kFFQz3Hz+nuHPb1A6+AeOzj9A+i/UMHlxfob1W3eiGACODRMTk6qk+qn/cxJCIYnmHyTR/P3/H8GHJqRFqzcgDEAGMRXd/4nNDwCe61SQWGtvIQAAAABJRU5ErkJggg==" border="0">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
							    <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer ke SAP"></asp:Button><span style="margin-right: 10px"></span>
                    <asp:Button Visible="false" ID="btnTransferUlang" runat="server" Text="Transfer Ulang ke SAP"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
