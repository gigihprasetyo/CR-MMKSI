<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditPO.aspx.vb" Inherits="EditPO" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EditPO</title>
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

        function Disable() {
            document.getElementById("btnKirim").disabled = true;
            document.getElementById("btnKirim2").click();
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
                hidPODestinationID.value = '';
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
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PO Harian -&nbsp; Edit PO&nbsp;</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label1" runat="server">Kode Dealer</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>&nbsp;/
									<asp:Label ID="lblSearchTerm1" runat="server"></asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="label66" runat="server">Kota</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label5" runat="server">Nama Dealer</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="titleField">
                                <asp:Label ID="Label6" runat="server">Nomor O/C</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblContractNumber" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nomor Reg PO</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNoPO" runat="server"></asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="Order" runat="server">Jenis O/C</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblOrderType" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label7" runat="server"> Nomor PO</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" onblur="alphaNumericPlusSpaceBlur(txtDealerPONumber)"
                                    ID="txtDealerPONumber" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nomor PO Tidak Boleh Kosong"
                                    ControlToValidate="txtDealerPONumber">*</asp:RequiredFieldValidator></td>
                            <td class="titleField">
                                <asp:Label ID="Label8" runat="server"> Kategori</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblSalesOrg" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px">
                                <asp:Label ID="lblFactoring" runat="server">Factoring</asp:Label></td>
                            <td style="height: 22px">
                                <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label></td>
                            <td style="height: 22px">
                                <asp:CheckBox ID="chkFactoring" runat="server" Enabled="False" Text=" " Width="16px" AutoPostBack="True"></asp:CheckBox></td>
                            <td class="titleField" style="height: 22px"></td>
                            <td style="height: 22px"></td>
                            <td style="height: 22px"></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label4" runat="server"> Cara Pembayaran</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlTermOfPayment" runat="server"></asp:DropDownList></td>
                            <td class="titleField">
                                <asp:Label ID="Label11" runat="server">Tahun Perakitan/Impor</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblProductYear" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="label24" runat="server">Jenis Order</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrderType" runat="server" Enabled="False"></asp:DropDownList></td>
                            <td class="titleField">
                                <asp:Label ID="Label12" runat="server">Nama Pesanan Khusus</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label2" runat="server"> Permintaan Kirim</asp:Label></td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="icPermintaanKirim" runat="server"></cc1:IntiCalendar></td>
                            <td class="titleField">
                                <asp:Label ID="Total" runat="server"> Total Harga Tebus Kendaraan</asp:Label></td>
                            <td>
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td><b></b>
                                <asp:Label ID="Label9" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
									<asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True">Tanggal Jatuh Tempo</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblJatuhTempo" runat="server"></asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="TotalBiayaKirim" runat="server"> Total Biaya Kirim (incl. PPN)</asp:Label></td>
                            <td>
                                <asp:Label ID="Label10X" runat="server">:</asp:Label></td>
                            <td><b></b>
                                <asp:Label ID="Label9X" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;
									<asp:Label ID="lblTotalBiayaKirim" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr style="visibility: visible">
                            <td>Av Ceiling</td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblF1" runat="server" Width="144px">0</asp:Label></td>
                            <td>
                                <asp:CheckBox ID="chkFreePPh" runat="server" Font-Bold="True" Width="19px" Text=" " Enabled="False"></asp:CheckBox><strong>Bebas 
										PPh22</strong></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="visibility: visible">
                            <td>Last PO</td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblF2" runat="server" Width="144px">0</asp:Label></td>
                            <td class="titleField">
                                <asp:RadioButton ID="rdoByKTB" GroupName="PengirimanBy" runat="server" Text="Pengiriman oleh MMKSI" /></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtPODestinationCode','<>?*%$')" ID="txtPODestinationCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hidPODestinationID" runat="server" />
                                <asp:Label ID="lblSearchPODestination" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr style="visibility: visible">
                            <td>Curr PO</td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblF3" runat="server" Width="144px">0</asp:Label></td>
                            <td class="titleField">
                                <asp:RadioButton ID="rdoByDealer" GroupName="PengirimanBy" runat="server" Text="Pengiriman oleh Dealer" /></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 240px">
                        <asp:DataGrid ID="dtgDetail" runat="server" CellSpacing="1" CellPadding="3" BorderWidth="0px"
                            BorderStyle="None" BorderColor="#CDCDCD" BackColor="#CDCDCD" OnItemDataBound="dtgDetail_ItemDataBound" AutoGenerateColumns="False" Width="100%"
                            ShowFooter="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#FFFFCC" VerticalAlign="Middle"
                                BackColor="#990000"></HeaderStyle>
                            <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
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
                                <asp:BoundColumn HeaderText="Sisa O/C (unit)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Order (unit)">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="54px" CssClass="textRight">0</asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Permintaan Melebihi Sisa Unit"
                                            Type="Integer" MinimumValue="0" MaximumValue="10000">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Order Unit tidak boleh Kosong">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Jaminan (Rp)">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Harga Unit (Rp)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="PPH 22 (Rp)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Interest (Rp)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Biaya Kirim Incl. PPN (Rp)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
                                </asp:BoundColumn>
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
                            <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td valign="top" class="titleField"></td>
            </tr>
            <tr>
                <td height="60">
                    <asp:Button ID="btnKirim" runat="server" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnKirim2" runat="server" Text="Simpan2" style="display:none"></asp:Button>
                    <asp:Button ID="btnHitung" runat="server" Text="Hitung"></asp:Button>&nbsp;
                    <asp:Button ID="btnBatal" runat="server" Text="Hapus" CausesValidation="False"></asp:Button>&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
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
