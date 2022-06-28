<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPengajuanDummyFaktur.aspx.vb" Inherits=".FrmPengajuanDummyFaktur" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FAKTUR KENDARAAN - Pengajuan Temporary Faktur</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPTujuanSelection() {
            showPopUp('../PopUp/PopUpCustomerSelectionOne.aspx?FilterLoginDealer=True', '', 500, 760, TujuanSelection);
        }

        function TujuanSelection(selectedTujuan) {
            var txtCustomerCode = document.getElementById('txtCustomerCode');
            var hdnCustomerCode = document.getElementById('hdnCustomerCode');
            var lblName = document.getElementById('lblName');
            var lblAlamat = document.getElementById('lblAlamat');
            var lblGedung = document.getElementById('lblGedung');
            var lblKelurahan = document.getElementById('lblKelurahan');
            var lblKecamatan = document.getElementById('lblKecamatan');
            var lblKodePos = document.getElementById('lblKodePos');
            var lblKodya = document.getElementById('lblKodya');
            var lblPropinsi = document.getElementById('lblPropinsi');
            var lblEmail = document.getElementById('lblEmail');
            var lblPhone = document.getElementById('lblPhone');
            var lblName2 = document.getElementById('lblName2');
            var lblNoKTP = document.getElementById('lblNoKTP');

            selectedTujuan = selectedTujuan.replace(/&amp;/g, '&');//&amp=>'&'
            hdnCustomerCode.value = selectedTujuan
            var arrValue = selectedTujuan.split(';');
            txtCustomerCode.value = arrValue[0];
            lblName.innerHTML = arrValue[1];
            lblGedung.innerHTML = arrValue[2];
            lblAlamat.innerHTML = arrValue[3];
            lblKelurahan.innerHTML = arrValue[4];
            lblKecamatan.innerHTML = arrValue[5];
            lblKodePos.innerHTML = arrValue[6];
            lblKodya.innerHTML = arrValue[7];
            lblPropinsi.innerHTML = arrValue[8];
            lblName2.innerHTML = arrValue[12];
            lblNoKTP.innerHTML = arrValue[13];

            if (navigator.appName == 'Microsoft Internet Explorer') {
                //txtCustomerCode.focus();
                txtCustomerCode.blur();
            }
            else {
                //txtCustomerCode.onchange();
            }
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            var chk = document.getElementById('chkAllPages')
		    re = new RegExp(':' + aspCheckBoxID + '$')
		    for (i = 0; i < document.forms[0].elements.length; i++) {
		        elm = document.forms[0].elements[i]
		        if (elm.type == 'checkbox') {
		            if (re.test(elm.name)) {
		                if (!elm.disabled) {
		                    elm.checked = checkVal
		                }
		            }
		        }
		    }
        }

        function KonfirmasiSimpan(obj) {
            var btnSave = document.getElementById(obj.id);
            if (confirm('Anda yakin ingin simpan ?')) {
                btnSave.click();
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" width="100%">
            <tr>
                <td class="titlePage" style="height: 17px">FAKTUR KENDARAAN&nbsp;- Buat Permohonan Temporary Faktur</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table4" cellspacing="1" cellpadding="2" width="100%">
            <tr>
                <td class="titleField" style="width: 24%">Kode Dealer</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Nama Dealer</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">
                    <asp:Label ID="Label3" runat="server">Kode Konsumen</asp:Label></td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="txtCustomerCode" runat="server" Width="104px" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblCustomerCode" runat="server" Width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:HiddenField ID="hdnCustomerCode" runat="server" />
                </td>
                <td class="titleField" width="24%"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Nama 1</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label></td>
                <td class="titleField" width="24%"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Nama 2</td>
                <td width="1%">:</td>
                <td width="25%">
                    <asp:Label ID="lblName2" runat="server"></asp:Label></td>
                <td class="titleField" width="24%"></td>
                <td width="1%"></td>
                <td width="25%"></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Gedung</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblGedung" runat="server"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Alamat</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblAlamat" runat="server"></asp:Label></td>
                <td>
                    <asp:CheckBox ID="CBTglCetakDO" runat="server" OnClick="showAlert(this)" Text=" Tanggal Cetak DO" class="titleField"></asp:CheckBox></td>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <cc1:IntiCalendar ID="icCetakDoStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                            <td>&nbsp;s.d&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="icCetakDoSEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Kelurahan</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblKelurahan" runat="server"></asp:Label></td>
                <td class="titleField" width="24%">Kategori</td>
                <td width="1%">:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlKategory" Width="50%" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Kecamatan</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblKecamatan" runat="server"></asp:Label></td>
                <td class="titleField" width="24%">Model</td>
                <td width="1%">:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlModel" Width="50%" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Kode Pos</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblKodePos" runat="server"></asp:Label></td>
                <td class="titleField" width="24%">Tipe</td>
                <td width="1%">:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTipe" Width="50%" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Kodya/Kabupaten</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblKodya" runat="server"></asp:Label></td>
                <td class="titleField" width="24%">Warna</td>
                <td width="1%">:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlWarna" Width="50%"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Propinsi</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblPropinsi" runat="server"></asp:Label></td>
                <td class="titleField" width="24%">Chassis Number</td>
                <td width="1%"></td>
                <td>
                    <asp:TextBox ID="txtFilterChassisNumber" runat="server" Width="50%"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Nomor KTP/TDP</td>
                <td width="1%">:</td>
                <td>
                    <asp:Label ID="lblNoKTP" runat="server"></asp:Label></td>
                <td class="titleField" width="24%"></td>
                <td width="1%"></td>
                <td width="25%">
                    <asp:Button runat="server" ID="btnCari" Text="Cari" Width="20%" /></td>
            </tr>
            <tr>
                <td class="titleField">Tanggal Faktur</td>
                <td width="1%">:</td>
                <td>
                    <cc1:IntiCalendar ID="icFakturDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                <td></td>
                <td width="1%"></td>
                <td></td>
            </tr>

            <tr>
                <td class="titleField"></td>
                <td width="1%">&nbsp;</td>
                <td></td>
                <td></td>
                <td width="1%"></td>
                <td></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:CheckBox ID="chkAllPages" runat="server" AutoPostBack="True" Text=" Pilih Data Pada Semua Halaman" visible="false" />
                </td>
                <td width="1%">&nbsp;</td>
                <td></td>
                <td>
                </td>
                <td width="1%"></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:DataGrid ID="dtgPengajuanFaktur" runat="server" Width="100%" BackColor="#E0E0E0" 
                        AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AllowCustomPaging="True" AllowPaging="True" PageSize="25">
                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Check">
                                <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <input id="chkAllItems" name="chkAllItems" onclick="CheckAll('chkSelect', document.forms[0].chkAllItems.checked)"
                                        type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Dealer">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kategori">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKategori" runat="server" Text=''>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Model">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleModel.Description")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleType.VechileTypeCode")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Warna">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblWarna" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleColor.ColorCode") + " - " + DataBinder.Eval(Container, "DataItem.VehicleColor.ColorIndName")%> '>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Pesanan Khusus">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPesananKhusus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectName")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nomor Rangka">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomorRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Sudah Terdaftar">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text=''>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Konsumen">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaKonsumen" runat="server" Text=''>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tanggal Cetak DO">
                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTglCetakDO" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DODate"), "dd/MM/yyyy")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="btnSimpan" OnClientClick="return KonfirmasiSimpan(this)" Text="Simpan" Width="10%" />
        <asp:Button ID="btnUpdateProfil" runat="server" Width="10%" Text="Update Profil" Enabled="false" Visible="false"></asp:Button>
        <input id="hdnValSimpan" type="hidden" value="-1" name="hdnValSimpan" runat="server">
        <input id="hdnValsessGuid" type="hidden" value="" name="hdnValsessGuid" runat="server">
    </form>
</body>
</html>
