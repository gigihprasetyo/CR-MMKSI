<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarPengajuanPencairanDepositA.aspx.vb" Inherits="FrmDaftarPengajuanPencairanDepositA" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDaftarPengajuanPencairanDepositA</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="117%" border="0">
            <tr>
                <td class="titlePage">Sales - DepositA - Proses Pengajuan Pencairan</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">
                                <asp:Label ID="lblCode" runat="server">Kode Dealer*</asp:Label></td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbPeriode" runat="server" AutoPostBack="True"></asp:CheckBox></td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:inticalendar id="icPeriodeTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tipe Pengajuan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlTipePengajuan" runat="server"></asp:DropDownList></td>
                            <td class="titleField" style="width: 146px">No. Ref Surat Pengajuan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNoPengajuan" runat="server" MaxLength="18"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Status</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlStatusPengajuan" runat="server"></asp:DropDownList></td>
                            <td class="titleField">No. Reg. Pengajuan&nbsp;</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoReg" runat="server" MaxLength="18"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Produk</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField"  >

                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                            <td> </td>
                              <td> </td>
                              <td> </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">
                                <asp:Button ID="btnSearch" runat="server" Width="72px" Text="Cari"></asp:Button></td>
                            <td style="width: 2px"></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 270px">
                        <asp:DataGrid ID="dgDaftarPengajuanPencairanDepositA" runat="server" Width="100%" AllowSorting="True"
                            PageSize="25" AllowCustomPaging="True" AllowPaging="True" ShowFooter="False" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro"
                            CellSpacing="1" CellPadding="3" BorderWidth="0px">
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
											document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No." Visible="False">
                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdukDetail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="NoReg" SortExpression="NoReg" ReadOnly="True" HeaderText="No. Reg">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NoSurat" SortExpression="NoSurat" ReadOnly="True" HeaderText="No.Ref">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DNNumber" SortExpression="DNNumber" ReadOnly="True" HeaderText="Nomor DN">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AssignmentNumber" SortExpression="AssignmentNumber" ReadOnly="True" HeaderText="Nomor SO">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CreatedTime" ReadOnly="True" HeaderText="Tanggal" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipe" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DealerAmount" ReadOnly="True" HeaderText="Jumlah Pengajuan" DataFormatString="{0:#,###}">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Disetujui">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJumlahDisetujui" runat="server" Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alasan">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAlasan" runat="server" Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle Width="24%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbViewDetail" runat="server" CommandName="ViewDetail" Visible="True">
												<img src="../images/detail.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lbViewFlow" runat="server" CommandName="lbViewFlow" Visible="True">
												<img src="../images/alur_flow.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbViewStatus" runat="server" CommandName="lbViewStatus" Visible="True">
												<img src="../images/popup.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" Visible="True">
												<img src="../images/trash.gif" border="0" style="cursor:hand">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnValidasi" runat="server" Text="Validasi" CommandName="Validasi"></asp:Button><asp:Button ID="btnBatalValidasi" runat="server" Text="Batal Validasi" CommandName="BatalValidasi"></asp:Button><asp:Button ID="btnKonfirmasi" runat="server" Text="Konfirmasi" CommandName="Konfirmasi" Visible="False"></asp:Button><asp:Button ID="btnBatalKonfirmasi" runat="server" Text="Batal Konfirmasi" CommandName="BatalKonfirmasi"
                        Visible="False"></asp:Button><asp:Button ID="btnSetuju" runat="server" Text="Setuju" CommandName="Setuju" Visible="False"></asp:Button><asp:Button ID="btnTolak" runat="server" Text="Tolak" CommandName="Tolak" Visible="False"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUbahStatus" runat="server" Visible="False" Font-Italic="True" Font-Bold="True">Mengubah Status :</asp:Label><asp:DropDownList ID="ddlAction" runat="server" Visible="False">
                        <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                        <asp:ListItem Value="Konfirmasi">Konfirmasi</asp:ListItem>
                        <asp:ListItem Value="BatalKonfirmasi">Batal Konfirmasi</asp:ListItem>
                        <asp:ListItem Value="Setuju">Setuju</asp:ListItem>
                        <asp:ListItem Value="BatalSetuju">Batal Setuju</asp:ListItem>
                        <%--<asp:ListItem Value="Tolak">Tolak</asp:ListItem>--%>
                        <asp:ListItem Value="Blok">Blok</asp:ListItem>
                    </asp:DropDownList><asp:Button ID="btnProses" runat="server" Text="Proses" Visible="False"></asp:Button></td>
            </tr>
        </table>
    </form>
</body>
</html>
