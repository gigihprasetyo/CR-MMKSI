<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PODetails.aspx.vb" Inherits="PODetails" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PODetails</title>
    <meta content="True" name="vs_showGrid">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript">
        //function Back()
        //{
        //window.history.go(-1);
        //}
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="titlePage">PO HARIAN - Detail PO</td>
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
                    <td>
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" width="24%">Kode<asp:Label ID="Label1" runat="server"> Dealer</asp:Label></td>
                                <td width="1%">:</td>
                                <td width="25%">
                                    <asp:Label ID="lblDealerCodeValue" runat="server"></asp:Label></td>
                                <td class="titleField" width="20%">
                                    <asp:Label ID="label66" runat="server">Kota</asp:Label></td>
                                <td width="1%">:</td>
                                <td width="29%">
                                    <asp:Label ID="lblCityValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label5" runat="server">Nama Dealer</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblNameValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label6" runat="server">Nomor O/C</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblContractNumberValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblNoRegPO" runat="server">No Reg PO</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblNoRegPOValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label8" runat="server">Jenis O/C</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJenisMOValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label7" runat="server"> Nomor PO</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblDailyPONumberValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="label" runat="server"> Kategori</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblSalesOrgValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblTanggalPengajuan" runat="server">Tanggal Pengajuan</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTanggalPengajuanValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label11" runat="server">Tahun Perakitan / Impor</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblProductYearValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label2" runat="server"> Tanggal Permintaan Kirim</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblReqAllocValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label12" runat="server">Nama Pesanan Khusus</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblProjectNameValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px">
                                    <asp:Label ID="lblFactoring" runat="server">Factoring</asp:Label></td>
                                <td style="height: 17px">
                                    <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label></td>
                                <td style="height: 17px">
                                    <asp:CheckBox ID="chkFactoring" runat="server" Enabled="False" Text=" " Width="16px" AutoPostBack="True"></asp:CheckBox></td>
                                <td class="titleField" style="height: 17px">
                                    <asp:Label ID="label24" runat="server">Jenis Order</asp:Label></td>
                                <td style="height: 17px">:</td>
                                <td style="height: 17px">
                                    <asp:Label ID="lblOrderTypeValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px">
                                    <asp:Label ID="Label4" runat="server"> Cara Pembayaran</asp:Label></td>
                                <td style="height: 17px">:</td>
                                <td style="height: 17px">
                                    <asp:Label ID="lblTermOfPaymentValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Total" runat="server">Total Harga Tebus Kendaraan</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label9" runat="server">Rp</asp:Label>&nbsp;
										<asp:Label ID="lblTotalAmountValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblStatus" runat="server">Status</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblStatusValue" runat="server"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label18" runat="server">Total Biaya Kirim (incl. PPN)</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label19" runat="server">:</asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="Label20" runat="server">Rp</asp:Label>&nbsp;
										<asp:Label ID="lblTotalBiayaKirimValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="trSPL" runat="server">
                                <td class="titleField">
                                    <asp:Label ID="lblSPLNumber" runat="server">Nomor Applikasi</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblSPLNumberValue" runat="server"></asp:Label><asp:ImageButton ID="ibtnDownload" runat="server" ImageUrl="../images/download.gif" ToolTip="Download SPL"></asp:ImageButton></td>
                                <td class="titleField">Tanggal Jatuh Tempo</td>
                                <td>:</td>
                                <td class="titleField">
                                    <asp:Label ID="lblJatuhTempo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Visible="False">Tanggal Jatuh Tempo</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Visible="False">:</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Visible="False"></asp:Label></td>
                                <td class="titleField">
                                    <asp:CheckBox ID="chkFreePPh" runat="server" Width="16px" Text=" " Enabled="False"></asp:CheckBox>Bebas 
										PPh</td>
                                <td></td>
                                <td class="titleField">
                                    <asp:CheckBox ID="chkCash" runat="server" Enabled="False" Text=" " Width="16px"></asp:CheckBox>Pembayaran 
										Tunai</td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Visible="False"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Visible="False"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Visible="False"></asp:Label></td>
                                <td class="titleField">
                                    <asp:Label ID="lblPengirimanBy" runat="server" Font-Bold="True"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPengirimanBy2" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblPODestinationCode" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div id="div1" style="overflow: auto; height: 320px">
                            <asp:DataGrid ID="dtgDetail" runat="server" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
                                BackColor="#CDCDCD" OnItemDataBound="dtgDetail_ItemDataBound" BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3">
                                <AlternatingItemStyle BackColor="#EFEFEF"></AlternatingItemStyle>
                                <ItemStyle BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
                                    BackColor="Blue"></HeaderStyle>
                                <FooterStyle BackColor="White"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" HeaderImageUrl="ID" HeaderText="ID">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text='<%# container.itemindex+1 %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn HeaderText="Kode Tipe / Warna">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Model / Tipe / Warna">
                                        <HeaderStyle Width="13%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ReqQty" HeaderText="Order (unit)">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AllocQty" HeaderText="Alokasi (unit)">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Selisih (unit)">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga (Rp)">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="PPH 22 (Rp)">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Interest (Rp)">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Sub Total (Rp)">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Biaya Kirim Incl PPN (Rp)">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                    </asp:BoundColumn><asp:TemplateColumn HeaderText="Free Days">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreeDays" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Max TOP Days">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxTOPDays" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                            <p>
                                <table id="Table4" cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td style="height: 16px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:Label ID="lblInfo" runat="server" Width="432px" Height="15px">*) Interest dihitung dari harga kendaraan setelah dikurangi pembayaran tunai sebesar Rp.</asp:Label>
                                            <asp:Label ID="lblTotalGuarantee" runat="server" Width="144px" Height="15px">0</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="display: inline; width: 56px; height: 15px" ms_positioning="FlowLayout">
                                                Catatan 
													:
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <!--/P>
							<P-->
                                <table id="Table5" cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="height: 18px" width="2%">
                                                <div style="display: inline; width: 8px; height: 28px" ms_positioning="FlowLayout">
                                                    a.&nbsp; 
														&nbsp;
                                                </div>
                                            </td>
                                            <td style="height: 18px">
                                                <asp:Label ID="lblLine1" runat="server" Width="392px" Height="15px">Dokumen ini merupakan bagian yang tidak terpisahkan dari perjanjian Jual Beli no. </asp:Label><asp:Label ID="lblNoSurat" runat="server" Height="15px"></asp:Label>
                                                <asp:Label ID="lblLine2" runat="server" Width="392px" Height="15px"> termasuk setiap perubahan atau pembaharuannya. </asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="display: inline; width: 8px; height: 28px" ms_positioning="FlowLayout">
                                                    b.&nbsp; 
														&nbsp;
                                                </div>
                                            </td>
                                            <td>
                                                <div style="display: inline; width: 790px; height: 15px" ms_positioning="FlowLayout">
                                                    Dokumen 
														ini dibuat dalam bentuk dokumen elektronik dan merupakan bukti yang cukup dan 
														sah meskipun tidak ditandatangani.
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trFootNoteIsFactoring" runat="server" visible="false">
                                            <td>
                                                <div style="display: inline; width: 8px; height: 28px" ms_positioning="FlowLayout">
                                                    c.&nbsp; 
														    &nbsp;
                                                </div>
                                            </td>
                                            <td>
                                                <div style="display: inline; width: 100%; height: 15px" ms_positioning="FlowLayout">
                                                    Atas pemilihan Fasilitas TOP Factoring untuk pembelian kendaraan berdasarkan dokumen ini, 
                                                        Dealer dengan ini menegaskan untuk menerima dan menyetujui pengalihan piutang 
                                                        dari MMKSI ke PT Dipo Star Finance (DSF) dan menyatakan akan melakukan pembayaran ke DSF.
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </p>
                        </div>
                        <div></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnBack" Text="Kembali" runat="server"></asp:Button></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
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
    </TD></TR></TBODY>
		<p></p>
    <div></div>
    </TR></TBODY></TABLE></FORM>
		<p></p>
    <div></div>
    </TR></TBODY></TABLE>
</body>
</html>
