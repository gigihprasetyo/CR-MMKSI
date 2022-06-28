<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerMaintenance.aspx.vb" Inherits="FrmDealerMaintenance" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>DEALER - Dealer Maintenance</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelectionOne() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtLegalStatus");
            txtDealer.value = tempParam[0];
        }
        function ShowPPDealerSelectionMainDealer() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, MainDealerSelection);
        }
        function MainDealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtMainDealer = document.getElementById("txtMainDealer");
            txtMainDealer.value = tempParam[0];
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">ADMIN SISTEM&nbsp;-
						<asp:Label ID="lblTitle" runat="server">Organisasi Baru</asp:Label></td>
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
                            <td class="titleField" style="height: 24px" width="24%">Tipe Organisasi</td>
                            <td style="height: 24px" width="76%">
                                <asp:DropDownList ID="ddlTitle" runat="server" Width="136px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%">Kode Organisasi</td>
                            <td style="height: 24px" width="76%">
                                <asp:TextBox ID="txtDealerCode" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDealerCode)"
                                    runat="server" Width="64px" MaxLength="6"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi kode dealer  (tidak boleh kosong)"
                                        ControlToValidate="txtDealerCode" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtDealerCode"
                                    Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%">Organisasi Tipe Branch</td>
                            <td style="height: 24px" width="76%">
                                <asp:RadioButtonList ID="rdOrgTipeBranch" runat="server" RepeatColumns="2" Height="26px"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama</td>
                            <td>
                                <asp:TextBox ID="txtName" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtName)"
                                    runat="server" Width="264px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi nama dealer  (tidak boleh kosong)"
                                        ControlToValidate="txtName" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">NickName Digital</td>
                            <td>
                                <asp:TextBox ID="txtNickNameDigital" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtNickNameDigital)"
                                    runat="server" Width="264px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Silahkan isi NickName Digital (tidak boleh kosong)"
                                        ControlToValidate="txtNickNameDigital" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ControlToValidate="txtNickNameDigital"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">NickName E-Commerce</td>
                            <td>
                                <asp:TextBox ID="txtNickNameEcomm" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtNickNameEcomm)"
                                    runat="server" Width="264px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Silahkan isi NickName E-Commerce (tidak boleh kosong)"
                                        ControlToValidate="txtNickNameEcomm" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtNickNameEcomm"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Longitude</td>
                            <td>
                                <asp:TextBox ID="txtLongitude" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtLongitude)"
                                    runat="server" Width="264px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Silahkan isi Longitude (tidak boleh kosong)"
                                        ControlToValidate="txtLongitude" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtLongitude"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Latitude</td>
                            <td>
                                <asp:TextBox ID="txtLatitude" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtLatitude)"
                                    runat="server" Width="264px" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Silahkan isi Latitude (tidak boleh kosong)"
                                        ControlToValidate="txtLatitude" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" ControlToValidate="txtLatitude"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%">Publish</td>
                            <td style="height: 24px" width="76%">
                                <asp:RadioButtonList ID="rdPublish" runat="server" RepeatColumns="2" Height="26px"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Term Cari 1/2</td>
                            <td style="height: 26px">
                                <asp:TextBox ID="txtSearch1" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch1)"
                                    runat="server" Width="136px" MaxLength="20"></asp:TextBox>/
									<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi Term Cari 1   (tidak boleh kosong)"
                                        ControlToValidate="txtSearch1" Display="None"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtSearch1"></asp:RequiredFieldValidator>&nbsp;
									<asp:TextBox ID="txtSearch2" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch2)"
                                        runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Silahkan isi Term Cari 2   (tidak boleh kosong)"
                                            ControlToValidate="txtSearch2" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtSearch2"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Main Area / Area 1 / Area 2</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlMainArea" runat="server" Width="144px" AutoPostBack="true"></asp:DropDownList>&nbsp;/&nbsp;<asp:DropDownList ID="ddlArea1" runat="server" Width="144px" AutoPostBack="true"></asp:DropDownList>&nbsp;/&nbsp;
									<asp:DropDownList ID="ddlArea2" runat="server" Width="136px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px">Kategori Kendaraan</td>
                            <td style="height: 14px">
                                <asp:Panel Height="80px" runat="server" ScrollBars="Vertical" Width="250px">
                                    <asp:CheckBoxList ID="chlVehicleCategory" runat="server"></asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px">Grup</td>
                            <td style="height: 14px">
                                <asp:DropDownList ID="ddlGroup" runat="server" Width="264px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><strong>Legal Status</strong>&nbsp;</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtLegalStatus" onblur="HtmlCharBlur(txtDealerCode)"
                                    runat="server" Width="96px" MaxLength="6"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>Dealer</strong>&nbsp;</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtMainDealer" onblur="HtmlCharBlur(txtMainDealer)"
                                    runat="server" Width="96px" MaxLength="6"></asp:TextBox>
                                <asp:Label ID="lblMainDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>&nbsp;
									<asp:RequiredFieldValidator ID="Requiredfieldvalidator11" runat="server" ErrorMessage="* Untuk dealer induk diisi dengan kode organisasinya sendiri. Untuk Branch diisi dengan kode organisasi dealer induk"
                                        ControlToValidate="txtMainDealer" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Alamat</td>
                            <td>
                                <asp:TextBox ID="txtAddress" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtAddress)"
                                    runat="server" Width="447px" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Silahkan isi alamat dealer (tidak boleh kosong)"
                                        ControlToValidate="txtAddress" Display="None"></asp:RequiredFieldValidator>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtAddress"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px">Provinsi</td>
                            <td style="height: 22px">
                                <asp:DropDownList ID="ddlProvince" runat="server" Width="144px" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Kota</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlCity" runat="server" Width="144px" Enabled="False"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kode 
									Pos&nbsp;
									<asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPostCode)" ID="txtPostCode"
                                        runat="server" Width="40px" MaxLength="5"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="Silahkan isi Kode Pos dengan 5 digit angka"
                                            ControlToValidate="txtPostCode" Display="None" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtPostCode"
                                                ValidationExpression="\d{5}"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Nomor SPA</td>
                            <td style="height: 18px">
                                <asp:TextBox ID="txtSPANumber" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSPANumber)"
                                    runat="server" Width="312px" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Tanggal SPA</td>
                            <td style="height: 18px">
                                <asp:TextBox ID="txtSPADate" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSPADate)"
                                    runat="server" Width="64px" MaxLength="8" ToolTip='Tanggal SPA diisi dengan format "ddMMyyyy", misal 20102005 berarti 20-10-2005'></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage='Silahkan isi tanggal SPA dengan format "ddMMyyyy",  misal "20102005 "'
                                        ControlToValidate="txtSPADate" Display="None" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtSPADate"
                                        ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">No Penunjukan Dealer</td>
                            <td style="height: 18px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoPersetujuan" onblur="HtmlCharBlur(txtSPANumber)"
                                    runat="server" Width="312px" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Tgl Penunjukan Dealer</td>
                            <td style="height: 18px">
                                <cc1:IntiCalendar ID="icTglPersetujuan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                        </tr>
                       <%-- <tr id="trTelepon" runat="server" visible="false">
                            <td class="titleField" style="height: 22px">Telpon</td>
                            <td style="height: 22px">
                                <asp:TextBox ID="txtTelpArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtTelpArea)"
                                    runat="server" Width="32" MaxLength="4"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Silahkan isi Kode Area telpon  dengan [3,4] digit angka "
                                        ControlToValidate="txtTelpArea" Display="None" ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ErrorMessage="*" ControlToValidate="txtTelpArea"
                                        ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>-
									<asp:TextBox ID="txtTelpNo" onkeypress="return HtmlCharUniv(event)" runat="server" Width="96px"
                                        MaxLength="45"></asp:TextBox>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Silahkan isi No Telp  dengan [5,45] digit angka dan / (garis miring)"
                                        ControlToValidate="txtTelpNo" Display="None" ValidationExpression="^[0-9/]{5,45}$"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtTelpNo"
                                            ValidationExpression="^[0-9/]{5,45}$"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Fax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:TextBox ID="txtFaxArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxArea)"
                                        runat="server" Width="32px" MaxLength="4"></asp:TextBox>-
									<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Silahkan isi Kode Area Fax dengan [3,4] digit angka"
                                        ControlToValidate="txtFaxArea" Display="None" ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="*" ControlToValidate="txtFaxArea"
                                        ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator><asp:TextBox ID="txtFaxNo" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxNo)"
                                            runat="server" Width="96px" MaxLength="14"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Silahkan isi No Fax dengan [5,14] digit angka"
                                                ControlToValidate="txtFaxNo" Display="None" ValidationExpression="\d{5,14}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage="*" ControlToValidate="txtFaxNo"
                                        ValidationExpression="\d{5,14}"></asp:RegularExpressionValidator></td>
                        </tr>--%>
                        <tr>
                            <td class="titleField" style="height: 22px">Telpon</td>
                            <td style="height: 22px">
                                <asp:TextBox ID="txtfullNoTelepon" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtfullNoTelepon)"
                                    runat="server" Width="150" MaxLength="16"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Fax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:TextBox ID="txtfullFax" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtfullFax)"
                                        runat="server" Width="150px" MaxLength="16"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Email</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtEmailAdd" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtEmailAdd)"
                                    runat="server" Width="248px" MaxLength="40"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Situs web</td>
                            <td style="height: 26px">
                                <asp:TextBox Width="200px" ID="txtWeb" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtWeb)"
                                    runat="server" MaxLength="100"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Bebas&nbsp;PPh22</td>
                            <td style="height: 26px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chbxFreePPh" runat="server"></asp:CheckBox>
                                        </td>
                                        <td>Selama Periode</td>
                                        <td>
                                            <asp:TextBox ID="txtValidFrom" runat="server" MaxLength="6" onkeypress="return numericOnlyUniv(event)"
                                                Width="72px"></asp:TextBox></td>
                                        <td>s/d</td>
                                        <td>
                                            <asp:TextBox ID="txtValidTo" runat="server" MaxLength="6" onkeypress="return numericOnlyUniv(event)"
                                                Width="80px"></asp:TextBox></td>
                                        <td>
                                            <asp:Label ID="lblFormat" runat="server" ForeColor="Red">Format : MMyyyy</asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 19px">Status</td>
                            <td style="height: 19px">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="104px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><strong>Area Bisnis</strong></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbSalesUnit" runat="server" AutoPostBack="True" Text="Sales Unit"></asp:CheckBox></td>
                                        <td>
                                            <asp:CheckBox ID="cbService" runat="server" AutoPostBack="True" Text="Service"></asp:CheckBox></td>
                                        <td>
                                            <asp:CheckBox ID="cbSpareParts" runat="server" AutoPostBack="True" Text="Spare Parts"></asp:CheckBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="titleTableSales" align="center">SALES UNIT</td>
                                        <td class="titleTableService" align="center">SERVICE</td>
                                        <td class="titleTableParts" align="center">SPARE PARTS</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="chkSU" runat="server">
                                                <asp:ListItem Value="1" Text="WholeSales"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Retail"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="chkSvc" runat="server">
                                                <asp:ListItem Value="1" Text="WholeSales"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Retail"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="chkSPart" runat="server">
                                                <asp:ListItem Value="1" Text="WholeSales"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Retail"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>Branch Manager</strong></td>
                            <td>
                                <asp:TextBox ID="txtNameBM" runat="server" Width="248px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>Email</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmailBM" runat="server" Width="248px" MaxLength="100"></asp:TextBox>
                               
                            </td>
                        </tr>
                        <tr>
                            <td><strong>No. HP</strong></td>
                            <td>
                                <asp:TextBox ID="txtHPBM" runat="server"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 12px"><strong>Perubahan Terakhir</strong>
                            </td>
                            <td style="height: 12px">
                                <asp:Label ID="LblLastChange" runat="server" Font-Size="Smaller"></asp:Label></td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
                        DisplayMode="SingleParagraph"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleTableSales" align="center">SALES UNIT</td>
                            <td class="titleTableService" align="center">SERVICE</td>
                            <td class="titleTableParts" align="center">SPARE PARTS</td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Panel ID="pnlSales" runat="server" Width="100%" Enabled="False">
                                    <table id="Table4" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td>Contact Person</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson11" onblur="HtmlCharBlur(txtContactPerson11)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP11" onblur="HtmlCharBlur(txtHP11)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail11" onblur="HtmlCharBlur(txtEmail11)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate11" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Dept. Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson12" onblur="HtmlCharBlur(txtContactPerson12)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP12" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP12" onblur="HtmlCharBlur(txtHP12)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail12" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail12" onblur="HtmlCharBlur(txtEmail12)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate12" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Section Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson13" onblur="HtmlCharBlur(txtContactPerson13)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP13" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP13" onblur="HtmlCharBlur(txtHP13)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail13" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail13" onblur="HtmlCharBlur(txtEmail13)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate13" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Sales AC</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson14" onblur="HtmlCharBlur(txtContactPerson14)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP14" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP14" onblur="HtmlCharBlur(txtHP14)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail14" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail14" onblur="HtmlCharBlur(txtEmail14)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate14" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>PIC Region</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPICRegion" onblur="HtmlCharBlur(txtPICRegion)"
                                                    runat="server" Width="140px" MaxLength="100"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>PIC Area</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPICArea" onblur="HtmlCharBlur(txtPICArea)"
                                                    runat="server" Width="140px" MaxLength="100"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>PIC Sub Area</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPICSubArea" onblur="HtmlCharBlur(txtPICSubArea)"
                                                    runat="server" Width="140px" MaxLength="100"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>
                            <td width="33%" valign="top">
                                <asp:Panel ID="pnlService" runat="server" Width="100%" Enabled="False">
                                    <table id="Table5" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td>Contact Person</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson21" onblur="HtmlCharBlur(txtContactPerson21)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP21" onblur="HtmlCharBlur(txtHP21)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>Email</p>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail21" onblur="HtmlCharBlur(txtEmail21)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate21" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Dept. Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson22" onblur="HtmlCharBlur(txtContactPerson22)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP22" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP22" onblur="HtmlCharBlur(txtHP22)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail22" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail22" onblur="HtmlCharBlur(txtEmail22)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate22" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Section Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson23" onblur="HtmlCharBlur(txtContactPerson23)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP23" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP23" onblur="HtmlCharBlur(txtHP23)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail23" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail23" onblur="HtmlCharBlur(txtEmail23)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate23" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Sales AC</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson24" onblur="HtmlCharBlur(txtContactPerson24)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP24" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP24" onblur="HtmlCharBlur(txtHP24)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail24" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail24" onblur="HtmlCharBlur(txtEmail24)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate24" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td width="33%" valign="top">
                                <asp:Panel ID="pnlSpareparts" runat="server" Width="100%" Enabled="False" Height="104px" HorizontalAlign="Center">
                                    <table id="Table6" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td>Contact Person</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson31" onblur="HtmlCharBlur(txtContactPerson31)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP31" onblur="HtmlCharBlur(txtHP31)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>Email</p>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail31" onblur="HtmlCharBlur(txtEmail31)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate31" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Dept. Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson32" onblur="HtmlCharBlur(txtContactPerson32)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP32" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP32" onblur="HtmlCharBlur(txtHP32)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail32" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail32" onblur="HtmlCharBlur(txtEmail32)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate32" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Section Head</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson33" onblur="HtmlCharBlur(txtContactPerson33)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP33" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP33" onblur="HtmlCharBlur(txtHP33)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr  id="trtxtEmail33" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail33" onblur="HtmlCharBlur(txtEmail33)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate33" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Sales AC</b></td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson34" onblur="HtmlCharBlur(txtContactPerson34)"
                                                    runat="server" Width="140px" MaxLength="30"></asp:TextBox></td>
                                        </tr>
                                        <tr id="trtxtHP34" runat="server">
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP34" onblur="HtmlCharBlur(txtHP34)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trtxtEmail34" runat="server">
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail34" onblur="HtmlCharBlur(txtEmail34)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate34" runat="server" Font-Size="Smaller"></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False"></asp:Button><asp:Button ID="btnSave" runat="server" Width="60px" Text="Simpan"></asp:Button></td>
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
