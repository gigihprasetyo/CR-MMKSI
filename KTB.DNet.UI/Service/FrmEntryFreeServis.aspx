<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryFreeServis.aspx.vb" Inherits="FrmEntryFreeServis" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Free Service</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/ocr/jquery-1.12.4.min.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblDealerCode");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                var txtBranchName = document.getElementById("txtBranchName");
                txtDealerSelection.value = selectedDealer.split(";")[0];
                txtBranchName.value = selectedDealer.split(";")[1];

                //do post back to filter grid data based on selected dealerbranchcode
                __doPostBack("<%= txtDealerBranchCode.ClientID()%>", "");
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }

        function firstFocus() {
            document.all.txtChassisMaster.focus();
        }

        function enter(controlAfter) {

            var charPressed = event.keyCode;
            if (charPressed == 13) {
                controlAfter.focus();
                return false;
            }

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

        function checkFSPeriod() {
            var d = new Date('<%= Now.toString("yyyy/M/d HH:mm:ss") %>');
            var a = new Date("2013/1/1 00:00:00");
            var b = new Date("2013/1/11 00:00:00");
            var str = "";

            str = str + "<table width='100%'>";
            str = str + "	<tr height='100px'>";
            str = str + "		<td></td>";
            str = str + "	</tr>";
            str = str + "	<tr height='200px'>";
            str = str + "		<td align='center'>";
            str = str + "		Sehubungan dengan adanya perubahan proses pengiriman data kupon Free Service,<br>maka untuk sementara menu ini tidak dapat diakses mulai tanggal 1 Januari 2013 s/d 10 Januari 2013	";
            str = str + "		</td>";
            str = str + "	</tr>";
            str = str + "	<tr>";
            str = str + "		<td></td>";
            str = str + "	</tr>";
            str = str + "</table>";

            //alert(d);
            if (d.valueOf() > a.valueOf()) {
                if (d.valueOf() < b.valueOf()) {
                    //alert(str);
                    document.write(str);
                }
            }
        }

        function validateFile()
        {
            var warning = document.getElementById('lblWarning');
            var lblUploadedFile  = document.getElementById('lblUploadedFile')
            var f = document.getElementById('iFSEvidence');
            var maxSize = parseInt(document.getElementById('lblMaxFileSize').innerHTML);
            var size = f.files.item(0).size / 1024;
            if (size > maxSize)
            {

                //ukuran dokumen terlalu besar. Ukuran maksimum 1 MB
                lblUploadedFile.innerHTML = '';
                if (maxSize > 1000)
                {
                    warning.innerHTML = 'Ukuran dokumen terlalu besar, Ukuran maksimum ' + (maxSize / 1024).toString() + ' MB.<br/>File anda ' + (size / 1024).toFixed(0).toString() + ' MB';
                }
                else
                {
                    warning.innerHTML = 'Ukuran dokumen terlalu besar, Ukuran maksimum ' + maxSize.toString() + ' KB.<br/>File anda ' + size.toFixed(0).toString() + ' KB';
                }

              
               
                return;
            }

            var MinSize = parseInt(document.getElementById('hdnMinSize').val());

            if (MinSize>0 && size < MinSize) {

                //ukuran dokumen terlalu besar. Ukuran maksimum 1 MB
                lblUploadedFile.innerHTML = '';
                if (MinSize > 1000) {
                    warning.innerHTML = 'Ukuran dokumen terlalu kecil, Ukuran maksimum ' + (MinSize / 1024).toString() + ' MB.<br/>File anda ' + (size / 1024).toFixed(0).toString() + ' MB';
                }
                else {
                    warning.innerHTML = 'Ukuran dokumen terlalu kecil, Ukuran minimum ' + MinSize.toString() + ' KB.<br/>File anda ' + size.toFixed(0).toString() + ' KB';
                }



                return;
            }



            var validFormat = document.getElementById('lblSupportedFormat').innerHTML.split(', ');
            const extension = f.files.item(0).name.split('.').pop();
            if (!validFormat.includes(extension))
            {
                lblUploadedFile.innerHTML = '';
                warning.innerHTML = 'Format file tidak didukung';
                return;
            }

            warning.innerHTML = '';
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 2px;
        }
    </style>
</head>
<body onload="firstFocus();checkFSPeriod();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">FREE SERVICE -&nbsp; Data Free Service</td>
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
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%" colspan="2">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td width="10">:</td>
                            <td colspan="3">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2">Nama Dealer</td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2">Kode Cabang</td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img alt="Klik Popup" border="0" src="../images/popup.gif" style="cursor:hand"></img>
                                </asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDealerBranchCode"
                                    Display="None" ErrorMessage="Silahkan isi Cabang Dealer (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                <asp:HiddenField runat="server" ID="hdnTitle" Value="DEALER" />
                            </td>
                            <td colspan="6">
                                <asp:Label ID="Label6" runat="server" Text="Catatan:" ForeColor="Red"></asp:Label><br />
                                <asp:Label ID="lblKeterangan" runat="server" Text="1. Kendaraan PKT sebelum September, validasi dari 
                                    tanggal faktur. Kendaraan PKT September, validasi dari tanggal PKT" ForeColor="Red"></asp:Label><br />
                                <div style="display:none">
                                <asp:Label ID="Label5" runat="server" Text="2. Untuk Kendaraan Expired Periode 13 Maret - 31 Mei 2020, 
                                    MMKSI akan menerima Claim Gratis Service sampai batas waktu yg wajar" ForeColor="Red"  ></asp:Label><br /></div>
                                <asp:Label ID="Label9" runat="server" Text="2. Rilis dapat dilakukan pada hari yang sama dengan tanggal penginputan data Free Service, untuk menghindari penolakan/ data tidak bisa masuk 14 hari dari tanggal rilis ke tanggal service"
                                    ForeColor="Red"></asp:Label><br />
                                <asp:Label ID="Label7" runat="server" Text="3. Maksimum berkas " ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblMaxFileSize" runat="server" ForeColor="Red" style="display:none"></asp:Label> 
                                <asp:Label ID="Label8" runat="server" Text=" KB, File yang didukung : " ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblSupportedFormat" runat="server" ForeColor="Red"></asp:Label> 
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2">Nama Cabang</td>
                            <td>:</td>
                            <td colspan="3">
                                <asp:textbox id="txtBranchName" Width="150px" Runat="server" disabled=""></asp:textbox>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2">Berkas</td>
                            <td>:</td>
                            <td colspan="3">
                                <INPUT id="iFSEvidence" type="file" size="34" name="File1" runat="server" onkeypress="return false;" onchange="validateFile()" style="border:thin">                                
                                <asp:Button id="btnUpload" runat="server" Text="Upload" CausesValidation="False"></asp:Button>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2"></td>
                            <td></td>
                            <td colspan="3">
                                <label id="lblWarning" style="color:red"></label>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2"></td>
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblUploadedFile" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" height="10">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChassisMaster" Display="None" ErrorMessage="Silahkan isi Kode Jenis Servis + No Rangka (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEngineNumber" Display="None" ErrorMessage="Silahkan isi Nomor Mesin (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKM" Display="None" ErrorMessage="Silahkan isi jarak tempuh (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTglServis" Display="None" ErrorMessage="Silahkan isi tgl servis dengan format 'ddmmyyyy'"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtKM" Display="None" ErrorMessage="Silahkan isi jarak tempuh dengan angka" ValidationExpression="\d{1,6}"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTglServis" Display="None" ErrorMessage="Silahkan isi tgl Servis dengan format  'ddMMyyyy' misal 01122005" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtTglJual" Display="None" ErrorMessage="Silahkan isi tgl Jual dengan 8 digit angka," ValidationExpression="\d{8}"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTglJual" Display="None" ErrorMessage="Silahkan isi tgl Jual dengan format  'ddMMyyyy' mis 01122005" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style1"></td>
                            <td style="height: 2px" width="1%"></td>
                            <td style="height: 2px" width="10%">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                            </td>
                            <td style="height: 2px" width="10%"></td>
                            <td style="height: 2px" width="10%">
                            </td>
                            <td style="height: 2px" width="10%"></td>
                            <td style="height: 2px" width="10%">
                                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text="Simpan" Visible="False" Width="60"></asp:Button><asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Batal" Visible="False" Width="60"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="titleField">Kind</td>
                            <td class="titleField">No. Rangka</td>
                            <td class="titleField">No. Mesin</td>
                            <td class="titleField">
                                <asp:Label ID="lblKM" runat="server">Jarak Tempuh (KM)</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="LblTglServis" runat="server" Width="73px">Tgl. Service</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblTgl" runat="server">Tgl Penjualan/Tgl PKT</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblVisitType" runat="server">Tipe Visit</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblWONumber" runat="server">WO Number</asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlKind" runat="server" ToolTip="Silahkan pilih Kode Kind"></asp:DropDownList></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtChassisMaster" runat="server"
                                    onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="154px" MaxLength="20" ToolTip="Silakan isi dengan nomor rangka (tanpa spasi). Contoh: SA00FFF4K000008"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtEngineNumber" runat="server"
                                    onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="124px" MaxLength="20" ToolTip="Silakan isi dengan nomor mesin (tanpa spasi). Contoh: 4G15-C97564"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtKM" onkeypress="return numericOnlyUniv(event)" runat="server"
                                    onblur="omitSomeCharacter('txtKM','<>?*%$;')" Width="120px" MaxLength="6" ToolTip="Harus dimasukan dengan angka"
                                    Height="18px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtTglServis" runat="server"
                                    onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="120px" MaxLength="8" Height="17px"
                                    ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtTglJual" onblur="omitSomeCharacter('txtTglJual','<>?*%$;')"
                                    runat="server" Width="120px" MaxLength="8" Height="18px" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005. Kendaraan PKT sebelum September, validasi dari tanggal faktur. Kendaraan PKT September, validasi dari tanggal PKT"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlVisitType" runat="server" ToolTip="Silahkan pilih Tipe Visit"></asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtWONumber" runat="server" Width="124px" MaxLength="50" ToolTip="Maksimal 50 karakter"></asp:TextBox></td>
                            <td style="margin: 20px">
                                <input id="btnSimpan" style="width: 50px" type="button" value="Simpan" name="btnSimpan"
                                    runat="server" causesvalidation="False">
                                <input id="btnBatal" style="width: 50px" type="button" value="Batal" name="btnBatal" runat="server"
                                    causesvalidation="False"></td>
                        </tr>
                        <tr>
                            <asp:Button ID="lbtnChassisLoad" runat="server" Width="20px" Text="LoadChassis" Style="display: none"></asp:Button>
                            <td style="width: 169px; height: 10px" colspan="7"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 385px">
                    <div id="div1" style="height: 350px; overflow: auto">
                        <asp:DataGrid ID="dgFreeServisEntry" runat="server" Width="100%" GridLines="Vertical" CellPadding="3"
                            BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                            PageSize="1000">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('cbSelect', document.forms[0].chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Dealer Code">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="53px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="FSKind.KindDescription" HeaderText="Jenis Free Servis">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSKind" runat="server" Width="149px" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka ">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' Width="173px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" SortExpression="ChassisMaster.Category.ProductCategory.Code" HeaderText="Produk">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.ProductCategory.Code") %>' Width="173px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn Visible="false" SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>' Width="65px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="MileAge" HeaderText="Jarak Tempuh KM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDatKm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl. Free Service">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SoldDate" HeaderText="Tgl. Penjualan">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="tglPenjualan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SoldDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVisitType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VisitType")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="FleetRequest.NoRegRequest" HeaderText="No Extended Free Service" Visible="false">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoFleetReq" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetRequest.NoRegRequest")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:LinkButton id="btnDownload" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <asp:Button ID="btnRelease" runat="server" CausesValidation="False" Text="Rilis" Visible="False"
                        Width="60px"></asp:Button><input id="btnRilis" style="width: 60px" type="button" value="Rilis" name="btnRilis" runat="server"
                            causesvalidation="False"></td>
            </tr>
            <tr>
                <td style="height: 11px" align="left">&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnMinSize" runat="server" Value="50" />
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
