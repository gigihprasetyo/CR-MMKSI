<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDOStatusList.aspx.vb" Inherits="FrmDOStatusList" SmartNavigation="False" %>

<%@ Register TagPrefix="uc1" TagName="Clock" Src="../UserControl/Clock.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDOStatusList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function PopUpHistory(Kosong) {
            var btnRefresh = document.getElementById("btnRefresh");
            btnRefresh.click();
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
                            <td class="titlePage">DO STATUS - Daftar Parkir Kendaraan</td>
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
                <td class="titleField" width="20%">&nbsp;Kode Dealer</td>
                <td style="width: 13px" width="13">:</td>
                <td width="40%">
                    <asp:TextBox ID="txtKodeDealer" onkeypress="TxtKeypress();" onblur="TxtBlur('txtKodeDealer');"
                        runat="server" Width="152px"></asp:TextBox>&nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                <td class="titleField" width="15%">Nomor DO</td>
                <td width="1%">:</td>
                <td width="28%">
                    <asp:TextBox ID="txtNoDO" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoDO');" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:CheckBox ID="chkTglCetak" runat="server" Height="10px" Checked="True" Text="Tanggal Cetak DO"></asp:CheckBox>&nbsp;</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 100px">
                                <cc1:IntiCalendar ID="ICDari" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                            <td class="titleField">&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="ICSampai" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
                <td class="titleField">Nomor Rangka</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoChassis" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoChassis');"
                        runat="server"></asp:TextBox></td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:CheckBox ID="chkTglKeluar" runat="server" Height="10px" Text="Tanggal Keluar"></asp:CheckBox>&nbsp;</td>
                <td>:</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 100px">
                                <cc1:IntiCalendar ID="ICDari2" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                            <td class="titleField">&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="ICSampai2" runat="server" TextBoxWidth="80"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
                <td class="titleField">Nomor PO</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNoPo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoPo');" runat="server"></asp:TextBox>&nbsp;</td>
            </tr>
            <tr>
                <td class="titleField">Total Estimasi Biaya Parkir&nbsp;&nbsp;</td>
                <td>:</td>
                <td>Rp.<asp:Label ID="lblTotalBiayaParkir" runat="server"></asp:Label></td>
                <td class="titleField">Status</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="130px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 31px">Total Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblTotalChassis" runat="server"></asp:Label></td>
                <td class="titleField">Lokasi Carpool</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtLocation" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNoPo');" runat="server"></asp:TextBox>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td class="titleField">
                    <asp:Button ID="btnRefresh" runat="server" Width="50px" Text="Cari"></asp:Button>
                    <asp:TextBox Style="visibility: hidden" ID="txtColorGreen" runat="server" Width="32px" BackColor="PaleGreen"></asp:TextBox></td>

                <td class="titleField" style="height: 31px"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td valign="top" colspan="10" height="30">
                    <div id="div1" style="overflow: auto; height: 260px">
                        <asp:DataGrid ID="dgDeliveryOrder" runat="server" Width="100%" CellSpacing="1" AllowPaging="True"
                            PageSize="25" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" 
                            BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                            AllowSorting="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChooseItem" runat="server" Width="5px"></asp:CheckBox>
                                        <asp:TextBox ID="txtSONumber" runat="server" Width="0px" Style="visibility: hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="PONumber" SortExpression="PONumber" HeaderText="Nomor PO">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DONumber" SortExpression="DONumber" HeaderText="Nomor DO">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SerialNumber" SortExpression="SerialNumber" HeaderText="Nomor Seri">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Kode Warna">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" HeaderText="Nomor Rangka">
                                    <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Height="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DODate" SortExpression="DODate" HeaderText="Tgl. Cetak DO" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Estimasi Tanggal Pengiriman">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblETD" runat="server" Height="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="GIDate" HeaderText="Aktual Tanggal Pengiriman">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGIDate" runat="server" Height="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Estimasi Tanggal Kedatangan">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblETA" runat="server" Height="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Aktual Tanggal Kedatangan">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblATA" runat="server" Height="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Lama Parkir (hari)">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLamaParkir" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Penalty Parkir (hari)">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPenaltyParkir" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ParkingAmount" HeaderText="Estimasi Biaya Parkir (Rp)" DataFormatString="{0:#,##0}">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Keterangan">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarkStatus" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Keterangan Penerimaan">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarkATA" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="false" DataField="ParkingDays"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Lokasi Carpool">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Location.Location")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Destinasi">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Location.PODestination.Nama")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Input Tanggal ATA">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td style="width: 99px" width="99">
                                <asp:Button ID="btnDownload" runat="server" Width="80px" Text="Download" Enabled="False"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td width="20%">
                                <asp:Button ID="btnHapus" runat="server" Width="80px" Text="Hapus" Enabled="False"></asp:Button></td>
                            <td align="right" width="20%"></td>
                            <td width="30%"></td>
                            <td width="10%"></td>
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
