<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmCreatePO.aspx.vb" Inherits="frmCreatePO" SmartNavigation="False" EnableViewStateMac="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>CreatePO</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        //function Back()
        //{
        //var hidden = document.getElementById("Hidden1")
        //var i = hidden.value * -1
        //window.history.go(i);
        //}
        function ConfirmCreatePO(btn) {
            return confirm('Apakah Yakin Buat PO?');
            var isOk = confirm('Apakah Yakin Buat PO?');
            alert(btn.id);
            if (isOk == true) {
                //IsReadOnly(true);
                alert(btn.id);
                btn.disabled = false;
                return true;
            }
            else {
                alert(btn.id + '1');
                IsReadOnly(false);
                return false;
            }
        }
        function IsReadOnly(IsIt) {
            var nodes = document.getElementById("Form1").getElementsByTagName("*");
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].id != "btnKirim")
                    nodes[i].disabled = IsIt;
            }
            return true;
        }


        function ShowPPPODestination() {
            showPopUp('../General/../PopUp/PopUpPODestinationSelection.aspx', '', 500, 760, PODestinationSelection);
        }

        function PODestinationSelection(selectedPODestination) {
            var tempParam = selectedPODestination.split(';');
            var hidPODestinationID = document.getElementById("hidPODestinationID");
            var txtPODestinationCode = document.getElementById("txtPODestinationCode");
            hidPODestinationID.value = tempParam[0];
            txtPODestinationCode.value = tempParam[1] + '/ ' + tempParam[2];

        }


        function SetPODestinationByKTB() {
            var hidPODestinationID = document.getElementById("hidPODestinationID");
            var txtPODestinationCode = document.getElementById("txtPODestinationCode");
            if (hidPODestinationID.value == '1') {
                hidPODestinationID.value = '1';
                txtPODestinationCode.value = '';

            }

        }


        function SetPODestinationByDealer() {
            var hidPODestinationID = document.getElementById("hidPODestinationID");
            var txtPODestinationCode = document.getElementById("txtPODestinationCode");

            hidPODestinationID.value = '1';
            txtPODestinationCode.value = '';


        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <div runat="server" id="divPage">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">PO Harian&nbsp;- Detil Pengajuan PO</td>
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
                                <td class="titleField" width="24%">
                                    <asp:Label ID="Label1" runat="server">Kode Dealer</asp:Label><asp:TextBox ID="txtID" Style="display: none" runat="server" Width="24px"></asp:TextBox></td>
                                <td width="1%">:</td>
                                <td width="25%">
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                                <td class="titleField" style="width: 149px" width="149">
                                    <asp:Label ID="label66" runat="server">Kota</asp:Label></td>
                                <td width="1%">:</td>
                                <td width="29%">
                                    <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label5" runat="server">Nama Dealer</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblName" runat="server"></asp:Label></td>
                                <td class="titleField" style="width: 149px"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 18px">
                                    <asp:Label ID="Label7" runat="server"> Nomor O/C</asp:Label></td>
                                <td style="height: 18px">:</td>
                                <td style="height: 18px">
                                    <asp:Label ID="lblContractNumber" runat="server"></asp:Label></td>
                                <td class="titleField" style="width: 149px; height: 18px">
                                    <asp:Label ID="Order" runat="server">Jenis O/C</asp:Label></td>
                                <td style="height: 18px">:</td>
                                <td style="height: 18px">
                                    <asp:Label ID="lblOrderType" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label6" runat="server">Nomor PO</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" ID="txtDealerPONumber" onblur="alphaNumericPlusSpaceBlur(txtDealerPONumber)"
                                        runat="server" Width="140px" MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nomor PO Harus Diisi"
                                            ControlToValidate="txtDealerPONumber" EnableClientScript="False">*</asp:RequiredFieldValidator></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:Label ID="Label8" runat="server"> Kategori</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblSalesOrg" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblFactoring" runat="server">Factoring</asp:Label></td>
                                <td>
                                    <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:CheckBox ID="chkFactoring" runat="server" Width="16px" AutoPostBack="True" Text=" " Enabled="False"></asp:CheckBox></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:Label ID="Label11" runat="server">Tahun Perakitan/Impor</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblProductYear" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label4" runat="server"> Cara Pembayaran</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="140px"></asp:DropDownList></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:Label ID="Label12" runat="server">Nama Pesanan Khusus</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="label24" runat="server">Jenis Order</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderType" runat="server" Width="140px"></asp:DropDownList></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:Label ID="Total" runat="server">Total Harga Tebus Kendaraan</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                <td>&nbsp;
										<asp:Label ID="Label9" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
										<asp:Label ID="lblTotalHargaValue" runat="server" Font-Bold="True">0</asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label2" runat="server"> Permintaan Kirim</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <cc1:IntiCalendar ID="icPermintaanKirim" runat="server"></cc1:IntiCalendar></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:Label ID="TotalX" runat="server">Total Biaya Kirim (incl. PPN)</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label10X" runat="server">:</asp:Label></td>
                                <td>&nbsp;
										<asp:Label ID="Label9X" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
										<asp:Label ID="lblTotalBiayaKirimValue" runat="server" Font-Bold="True">0</asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">Ceiling Tersedia</td>
                                <td>:</td>
                                <td><strong>Rp&nbsp;</strong>&nbsp;
										<asp:Label ID="lblAvailable" Style="text-align: left" runat="server" Width="144px" Font-Bold="True">0</asp:Label></td>
                                <td class="titleField" style="width: 149px">
                                    <asp:CheckBox ID="chkFreePPh" runat="server" Width="16px" Text=" " Enabled="False"></asp:CheckBox>Bebas 
										PPh22</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td class="titleField">
                                    <asp:RadioButton ID="rdoByKTB" GroupName="PengirimanBy" runat="server" Text="Pengiriman oleh MMKSI" /></td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtPODestinationCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hidPODestinationID" runat="server" />
                                    <asp:Label ID="lblSearchPODestination" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td class="titleField">
                                    <asp:RadioButton ID="rdoByDealer" GroupName="PengirimanBy" runat="server" Text="Pengiriman oleh Dealer" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr style="displayx: none">
                                <td colspan="6">
                                    <table cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="titleField" colspan="13">Detail Perhitungan Ceiling:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ceiling</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblCeiling" Text="0" runat="server"></asp:Label></td>
                                            <td>Diajukan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblProposed" Text="0" runat="server"></asp:Label></td>
                                            <td>AkanCair</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblLiquified" Text="0" runat="server"></asp:Label></td>
                                            <td>Outstanding</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblOutstanding" Text="0" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Av.CeilingToday</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblTodayAvCeiling" Text="0" runat="server"></asp:Label></td>
                                            <td>Av.Ceiling(Today+1)</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblTomorrowAvCeiling" Text="0" runat="server"></asp:Label></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="titleField">Formula A
										<asp:Label ID="lblAvCeilingFirst" runat="server" Width="104px"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblA" runat="server" Width="216px"></asp:Label></td>
                                <td class="titleField" style="width: 149px">Formula B</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblB" runat="server" Width="216px"></asp:Label></td>
                            </tr>
                            <tr style="display: none">
                                <td class="titleField">Formula C</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblC" runat="server" Width="216px"></asp:Label></td>
                                <td class="titleField" style="width: 149px">Formula D</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblD" runat="server" Width="216px"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="div1" style="height: 250px; overflow: auto">
                            <asp:DataGrid ID="dtgDetail" runat="server" Width="100%" AutoGenerateColumns="False" OnItemDataBound="dtgDetail_ItemDataBound"
                                BackColor="#CDCDCD" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" CellPadding="3" CellSpacing="1" ShowFooter="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#EDD0B5"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#FFFFCC" VerticalAlign="Middle"
                                    BackColor="#990000"></HeaderStyle>
                                <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
                                        <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text='<%# container.itemindex+1 %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn HeaderText="Kode  Tipe / Warna">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Model / Tipe / Warna">
                                        <HeaderStyle ForeColor="White" Width="13%" CssClass="titleTableSales"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TargetQty" HeaderText="Sisa O/C (unit)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Order (unit)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="TextBox1" runat="server" Width="54px"
                                                CssClass="textRight">0</asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" EnableClientScript="False" ControlToValidate="TextBox1"
                                                ErrorMessage="Order Unit Melebihi Sisa Unit O/C" Type="Integer" MaximumValue="1000" MinimumValue="0"
                                                Display="Dynamic">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="False" ControlToValidate="TextBox1"
                                                ErrorMessage="Order Unit Tidak boleh Kosong" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotal" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Jaminan (Rp)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeposit" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotalDeposit" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Harga (Rp)">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHarga" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotalHarga" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="PPH 22 (Rp)">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPPh22" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotalPPh22" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Interest (Rp)">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInterest" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotalInterest" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Biaya Kirim Incl. PPN (Rp)">
                                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBiayaKirimPPN" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSubTotalBiayaKirimPPN" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Free Days">
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
                                    <asp:TemplateColumn HeaderText="Is MDP">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsMDP" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                            </asp:DataGrid><asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="titleField" valign="top"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnKirim" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;
							<asp:Button ID="btnHitung" runat="server" Width="60px" Text="Hitung"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Hapus" CausesValidation="False"></asp:Button>&nbsp;
							<asp:Button ID="btnBack" Text="Kembali" runat="server"></asp:Button>
                        <input id="CtlTimeElapsed" type="hidden" name="CtlTimeElapsed" runat="server" value="1">
                    </td>
                </tr>
            </table>
        </div>
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
