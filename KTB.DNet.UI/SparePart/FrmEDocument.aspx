<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEDocument.aspx.vb" Inherits=".FrmEDocument" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmEDocument</title>
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
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
        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
            txtDealerSelection.onchange();

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
                            <td class="titlePage">Download E-Document</td>
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
                <td class="titleField" width="20%">Kode Dealer</td>
                <td style="width: 13px" width="13">:</td>
                <td width="40%">
                    <asp:Label ID="lblDealerCode" runat="server" Text="lblDealerCode"></asp:Label>
                    <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                        runat="server" Width="152px" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
                <td class="titleField" width="15%">Nomor Pesanan</td>
                <td width="1%">:</td>
                <td width="28%">
                    <asp:TextBox ID="txtNoPesanan" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoPesanan');" runat="server" Width="152px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Nama Dealer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                </td>
                <td class="titleField">Nomor DO</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtDONumber" onkeypress="TxtKeypress();" onblur="TxtBlur('txtDONumber');" runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Tipe Dokumen</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlDocumentType" runat="server"></asp:DropDownList>
                </td>
                <td class="titleField">Nomor Faktur</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNomorFaktur" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNomorFaktur');"
                        runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField">Tanggal Pesanan</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 100px">
                                <cc1:IntiCalendar ID="icTglCetakFrom" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                            <td class="titleField">&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglCetakTo" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
                <td class="titleField">Nomor SO</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtSONumber" onkeypress="TxtKeypress();" onblur="TxtBlur('txtSONumber');" runat="server" Width="152px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Width="50px" Text="Cari"></asp:Button>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="div1" style="overflow: auto; height: 300px">
                        <asp:DataGrid ID="dgEDoc" runat="server" Width="100%" BorderColor="Gainsboro" BackColor="Gainsboro"
                            CellSpacing="1" CellPadding="3" BorderWidth="0px" AutoGenerateColumns="False" PageSize="20" AllowPaging="True"
                            AllowCustomPaging="True" AllowSorting="True">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblDealerCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor Pemesanan">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblNoPemesanan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor Penjualan (SO MMKSI)">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblSOMMKSI" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor DO MMKSI">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblDOMMKSI" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nomor Faktur">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="GridlblNoFaktur" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download Faktur">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadFaktur" runat="server" Text="Download Faktur" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download Penalti Pengembalian Barang">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadPenaltiPengembalianBarang" runat="server" Text="Download Penalti Pengembalian Barang" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download EO Packing List">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadEOPackListCase" runat="server" Text="Download EO Packing List" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download EO Packing List Summary" Visible="false">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadEOPackListSummary" runat="server" Text="Download EO Packing List Summary" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download RO Packing List">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadROPackListCase" runat="server" Text="Download RO Packing List" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download RO Packing List Summary" Visible="false">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadROPackListSummary" runat="server" Text="Download RO Packing List Summary" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Download Credit Memo Manual">
                                    <HeaderStyle Width="9%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GridLnkDownloadCreditMemoManual" runat="server" Text="Download RO Packing List Summary" Visible="false" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
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
