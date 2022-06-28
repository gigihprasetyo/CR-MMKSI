<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDisplayDeposit.aspx.vb" Inherits="FrmDisplayDeposit" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>DEPOSIT - Daftar Deposit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var lblDealerName = document.getElementById("lblDealerName");
            var TextDealerName = document.getElementById("txtDealerName");
            txtDealerSelection.value = tempParam[0];
            lblDealerName.innerHTML = tempParam[1] + " - " + tempParam[3];
            TextDealerName.value = tempParam[1] + " - " + tempParam[3];
        }
        function ShowPPDealerSelectionKTB() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelectionKTB);
        }
        function DealerSelectionKTB(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 8px">DEPOSIT&nbsp;- Daftar Deposit</td>
            </tr>
            <tr>
                <td style="height: 1px" background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 8px" height="8">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td width="65%">
                                <table width="100%" style="background-color: #ffffff">
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="lblCode" runat="server" Font-Bold="True">Kode Dealer</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="lblColon1" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDealerCode" runat="server" Visible="False" Width="40px"></asp:Label><asp:TextBox ID="txtKodeDealer" runat="server" Width="80px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True">Nama Dealer</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="lblColon2" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDealerName" runat="server" Width="224px"></asp:Label><input id="txtDealerName" type="hidden" value="<%= lblDealerName.Text %>" name="txtDealerName"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">
                                            <asp:Label ID="lblPeriod" runat="server" Font-Bold="True">Sisa Deposit</asp:Label></td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                        <td class="titleField">
                                            <asp:DropDownList ID="ddlPeriod" runat="server" Width="80px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True">No. Dokumen</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtNoDocument" runat="server" Width="80px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True">Referensi</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtRef" runat="server" Width="80px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True">No. Faktur</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtNoFaktur" runat="server" Width="80px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True">Debit Amount</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtDebitAmount" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True">Credit Amount</asp:Label></td>
                                        <td width="1%">
                                            <asp:Label ID="Label12" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtCreditAmount" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="25%"></td>
                                        <td width="1%"></td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari " Height="22px"></asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table width="100%" style="background-color: #ffffff">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblBegBalc" runat="server" Font-Bold="True">Saldo Awal Bulan (Rp)</asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDr" runat="server" Font-Bold="True">Total Debet (Rp)</asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblCr" runat="server" Font-Bold="True">Total Kredit (Rp)</asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblEndBalc" runat="server" Font-Bold="True">Saldo Akhir (Rp)</asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblBeginBalance" runat="server"></asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDebit" runat="server"></asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblCredit" runat="server"></asp:Label></td>
                                                    <td align="right">
                                                        <asp:Label ID="lblEndingBalance" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%" valign="top">
                            <td width="33%" valign="top">
                                <table width="100%" style="background-color: #f1f6fb">
                                    <tr>
                                        <td width="25%" class="titleField">Available Deposit (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td width="25%" align="right">
                                            <asp:Label ID="lblDepositAwal" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="titleField">RO (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td align="right">
                                            <asp:Label ID="lblRO" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr runat="server" id="RODepC">
                                        <td width="25%" class="titleField">RO Deposit C (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td align="right">
                                            <asp:Label ID="lblRODeposit" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="titleField">Giro (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td width="25%" align="right">
                                            <asp:Label ID="lblGiroService" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="titleField">Service (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td align="right">
                                            <asp:Label ID="lblService" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="titleField">On Process (Rp)</td>
                                        <td width="1%" class="titleField">:</td>
                                        <td align="right">
                                            <asp:Label ID="lblOnProcess" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 320px">
                                    <asp:DataGrid ID="dgDepositList" runat="server" Width="100%" PageSize="25" AllowPaging="True"
                                        AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                                <HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="PostDateText" SortExpression="PostingDate" ReadOnly="True" HeaderText="Tgl Transaksi">
                                                <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Debit" SortExpression="Debit" ReadOnly="True" HeaderText="Debet (Rp)"
                                                DataFormatString="{0:#,##0}">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Credit" SortExpression="Credit" ReadOnly="True" HeaderText="Kredit (Rp)"
                                                DataFormatString="{0:#,##0}">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DocumentNo" SortExpression="DocumentNo" ReadOnly="True" HeaderText="No Dokumen">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ReferenceNo" SortExpression="ReferenceNo" ReadOnly="True" HeaderText="Referensi">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="InvoiceNo" SortExpression="InvoiceNo" ReadOnly="True" HeaderText="No Faktur">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="Remark" SortExpression="Remark" ReadOnly="True" HeaderText="Keterangan">
                                                <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <%--<asp:TemplateColumn HeaderText="Download Tanda Terima Titipan">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download" Text="Download CM">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn> --%>                                           
                                            <asp:BoundColumn Visible="False" DataField="LastUpdateTime" SortExpression="LastUpdateTime" HeaderText="Waktu Perubahan Terakhir">
                                                <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <asp:Button ID="btnDnLoad" runat="server" Text="Download Deposit" Enabled="False"></asp:Button><input id="btnPrint" type="button" value="Cetak Tampilan" name="btnPrint" runat="server"></td>
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
