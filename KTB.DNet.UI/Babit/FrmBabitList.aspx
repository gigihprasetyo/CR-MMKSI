<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBabitList.aspx.vb" Inherits=".FrmBabitList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmBabitList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <%--<script language="javascript" src="../WebResources/FormFunctions.js"></script>--%>
    <script language="javascript">
        function ShowPopUpTO() {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var dealerCode;
            if (txtKodeDealer == null) {
                txtKodeDealer = document.getElementById("lblKodeDealer");
                dealerCode = txtKodeDealer.innerText.split("/")[0].replace(/\s/g, '');
            } else {
                dealerCode = txtKodeDealer.value;
            }
            showPopUp('../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 430, 800, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            //var hdnTemporaryOutlet = document.getElementById("hdnTempOut");
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            //hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeTempOut.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }

        function ShowPopUpDealer() {
            showPopUp('../PK/../PopUp/PopUpDealerSelectionOne.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            //var hdnTemporaryOutlet = document.getElementById("hdnDealer");
            //hdnTemporaryOutlet.value = selectedRefNumber;
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            //var lblKodeDealer = document.getElementById("lblKodeDealer");
            txtKodeDealer.value = selectedRefNumber.split(";")[0];
            //lblKodeDealer.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT -&nbsp; DAFTAR PENGAJUAN BABIT</td>
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
                            <td style="width: 50%" valign="top">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" width="13%">Kode
                                            <asp:Label ID="lblDealerSearch" runat="server">Dealer</asp:Label></td>
                                        <td width="1%">:</td>
                                        <td width="34%">
                                            <asp:TextBox ID="txtKodeDealer" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnDealer" runat="server" />
                                            <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kode Temporary Outlet</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtKodeTempOut" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTempOut" runat="server" />
                                            <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 5px">Tipe Babit</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:DropDownList ID="ddlBabitType" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 5px">Kategori Alokasi</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:DropDownList ID="ddlCategoryAlloc" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 5px">Status Alokasi</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:DropDownList ID="ddlAllocStatus" runat="server"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="width: 50%">
                                <table>
                                    <tr>
                                        <td class="titleField" width="30%" style="height: 18px">Periode Kegiatan</td>
                                        <td>:</td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td valign="Top">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nomor Reg Babit</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtBabitRegNo"
                                                runat="server" MaxLength="50" Width="150px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nomor Surat</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNomorSurat" runat="server"
                                                MaxLength="30" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="titleField">Status</td>
                                        <td>
                                            <asp:Label ID="lbltitik2" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:ListBox ID="lsStatus" runat="server" Width="150px" Rows="5"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>&nbsp;
                    <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 20px"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListBabit" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="10" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                type="checkbox">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Dealer">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Reg Babit">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Surat">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNoSurat" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Babit">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBabitType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Mulai">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodeStart" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Selesai">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodeEnd" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Pengajuan Biaya">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblPengajuanBiaya" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Subsidi">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblSubsidyAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Folio">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblApprovalNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDownload" runat="server" CommandName="Download" CausesValidation="False">
												            <img src="../images/download.gif" border="0" alt="Download Approval Doc"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status :</asp:Label>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlAction" runat="server">
                        <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                        <asp:ListItem Value="0">Validasi</asp:ListItem>
                        <asp:ListItem Value="1">Batal Validasi</asp:ListItem>
                        <asp:ListItem Value="2">Konfirmasi</asp:ListItem>
                        <asp:ListItem Value="3">Revisi</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Button ID="btnProses" runat="server" Text="Proses"></asp:Button>
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer"></asp:Button>
                    <asp:Button ID="btnTransferUlang" runat="server" Text="Transfer Ulang"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
