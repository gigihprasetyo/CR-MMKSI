<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPExtendedUploadFakturPajak.aspx.vb" Inherits=".FrmMSPExtendedUploadFakturPajak" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmMSPExtendedUploadFakturPajak</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">E-Faktur - Daftar Dokumen eFaktur</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr runat="server" id="trUpload1">
                            <td class="titleField" style="height: 24px">Transaksi</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:DropDownList runat="server" ID="ddlTransaksiUpload" Width="161px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="trUpload2">
                            <td class="titleField" style="height: 24px">Upload excel VTA Online</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <input onkeypress="return false;" id="uplExcel" style="width: 300px; height: 20px" type="file" accept=".xls,.xlsx"
                                    size="46" name="fileUpload" runat="server">
                                * File Excel dengan ukuran maksimal 10Mb
                            </td>
                        </tr>
                        <tr runat="server" id="trUpload3">
                            <td class="titleField" style="height: 24px">Upload pdf VTA Online</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <input onkeypress="return false;" id="uplPdf" style="width: 300px; height: 20px" type="file"
                                    size="46" name="fileUpload" runat="server" accept="application/pdf">
                                * File Pdf dengan ukuran maksimal 10Mb
                            </td>
                        </tr>
                        <tr runat="server" id="trUpload4">
                            <td class="titleField" style="height: 24px"></td>
                            <td style="height: 24px"></td>
                            <td style="height: 24px">
                                <asp:Button ID="btnUpload" Width="50px" runat="server" Text="Upload"></asp:Button>
                            </td>
                        </tr>
                        <tr runat="server" id="trUpload5">
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Transaksi</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:DropDownList runat="server" ID="ddlTransaksiFind" Width="161px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Dealer</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                <asp:TextBox ID="txtKodeDealer" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Document No</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:TextBox runat="server" Width="150px" ID="txtDocumentNo" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Document Periode</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="DateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="DateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px"></td>
                            <td style="height: 24px"></td>
                            <td style="height: 24px">
                                <asp:Button ID="btnCancel" Width="50px" runat="server" Text="Batal"></asp:Button>
                                &nbsp;
                                <asp:Button ID="btnCari" Width="50px" runat="server" Text="Cari"></asp:Button>
                                <input id="hdnValSubmit" type="hidden" value="-1" name="hdnValSubmit" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dtgEFaktur" runat="server" Width="100%" CellSpacing="1" AllowCustomPaging="true"
                        AllowSorting="True" PageSize="10" AllowPaging="True" GridLines="Vertical" CellPadding="3"
                        BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Dealer">
                                <HeaderStyle Width="60%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Document No">
                                <HeaderStyle Width="50%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDocNo" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="titleTableSales" Width="5%"></HeaderStyle>
                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <div style="width: 70px; align-content: center">
                                        <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
										    <img src="../images/download.gif" border="0" alt="Download">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
										    <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <input id="hid_f" style="z-index: 101; left: 0px; position: absolute; top: 432px" type="hidden"
            value="0" name="hid_f" runat="server">
    </form>
</body>
</html>
