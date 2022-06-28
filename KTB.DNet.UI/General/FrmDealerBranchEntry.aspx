<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerBranchEntry.aspx.vb" Inherits=".FrmDealerBranchEntry" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>DEALER - Cabang Dealer</title>

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
            var txtDealer = document.getElementById("txtDealerCode");
            txtDealer.value = tempParam[0];
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Organisasi&nbsp;-
						<asp:Label ID="lblTitle" runat="server">Cabang Baru</asp:Label></td>
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
                            <td class="titleField" style="height: 24px" width="24%">Tipe Cabang</td>
                            <td style="height: 24px" width="76%">
                                <asp:DropDownList ID="ddlTYpe" runat="server" Width="136px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%">Kode Organisasi</td>
                            <td style="height: 24px" width="76%">
                                <asp:TextBox ID="txtDealerCode" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDealerCode)"
                                    runat="server" Width="128px" MaxLength="6"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                &nbsp;
                                    
										
										

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi kode dealer  (tidak boleh kosong)"
                                        ControlToValidate="txtDealerCode" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtDealerCode"
                                    Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%">Kode Cabang / Temporary Outlet</td>
                            <td style="height: 24px" width="76%">
                                <asp:TextBox ID="txtBranchCode" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtBranchCode)"
                                    runat="server" Width="128px" MaxLength="6"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Silahkan isi kode Cabang  (tidak boleh kosong)" ControlToValidate="txtBranchCode" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ControlToValidate="txtBranchCode"
                                    Display="Dynamic"></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr>
                            <td class="titleField">Nama</td>
                            <td>
                                <asp:TextBox ID="txtName" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtName)"
                                    runat="server" Width="264px" MaxLength="35"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi nama dealer  (tidak boleh kosong)"
                                        ControlToValidate="txtName" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Term Cari 1/2</td>
                            <td style="height: 26px">
                                <asp:TextBox ID="txtSearch1" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch1)"
                                    runat="server" Width="136px" MaxLength="20"></asp:TextBox>/
									<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi Term Cari 1   (tidak boleh kosong)"
                                        ControlToValidate="txtSearch1" Display="None"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtSearch1"></asp:RequiredFieldValidator>&nbsp;
									<asp:TextBox ID="txtSearch2" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch2)"
                                        runat="server" Width="90px" MaxLength="10"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Silahkan isi Term Cari 2   (tidak boleh kosong)"
                                            ControlToValidate="txtSearch2" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtSearch2"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Main Area / Area 1 / Area 2</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlMainArea" runat="server" Width="144px" AutoPostBack="true"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" initialvalue="0" runat="server" ErrorMessage="*" ControlToValidate="ddlMainArea"
                                    Display="static"></asp:RequiredFieldValidator>

                                &nbsp;/&nbsp;<asp:DropDownList ID="ddlArea1" runat="server" Width="144px" AutoPostBack="true"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" initialvalue="0" runat="server" ErrorMessage="*" ControlToValidate="ddlArea1"
                                    Display="static"></asp:RequiredFieldValidator>
                                &nbsp;/&nbsp;
									<asp:DropDownList ID="ddlArea2" runat="server" Width="136px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" initialvalue="0" runat="server" ErrorMessage="*" ControlToValidate="ddlArea1"
                                    Display="static"></asp:RequiredFieldValidator>

                            </td>
                        </tr>


                        <tr>
                            <td class="titleField" style="height: 22px">Provinsi</td>
                            <td style="height: 22px">
                                <asp:DropDownList ID="ddlProvince" runat="server" Width="144px" AutoPostBack="True"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" initialvalue="0" runat="server" ErrorMessage="*" ControlToValidate="ddlProvince"
                                    Display="static"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Kota</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlCity" runat="server" Width="144px" Enabled="False"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" initialvalue="0" runat="server" ErrorMessage="*" ControlToValidate="ddlCity"
                                    Display="static"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kode 
									Pos&nbsp;
									<asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPostCode)" ID="txtPostCode"
                                        runat="server" Width="40px" MaxLength="5"></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtPostCode"  Display="Static"></asp:RequiredFieldValidator>

                            </td>
                        </tr>

                        <tr>
                            <td class="titleField">Alamat</td>
                            <td>
                                <asp:TextBox ID="txtAddress" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtAddress)"
                                    runat="server" Width="447px" MaxLength="60"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Silahkan isi alamat dealer (tidak boleh kosong)"
                                        ControlToValidate="txtAddress" Display="None"></asp:RequiredFieldValidator>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtAddress"></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="height: 22px">Telpon</td>
                            <td style="height: 22px">
                                <asp:TextBox ID="txtTelpArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtTelpArea)"
                                    runat="server" Width="32px" MaxLength="4" Height="20px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Silahkan isi Kode Area telpon  dengan [3,4] digit angka "
                                        ControlToValidate="txtTelpArea" Display="None" ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ErrorMessage="*" ControlToValidate="txtTelpArea"
                                        ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>-
									<asp:TextBox ID="txtTelpNo" onkeypress="return HtmlCharUniv(event)" runat="server" Width="96px"
                                        MaxLength="25"></asp:TextBox>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Silahkan isi No Telp  dengan [5,45] digit angka dan / (garis miring)"
                                        ControlToValidate="txtTelpNo" Display="None" ValidationExpression="^[0-9/]{5,45}$"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtTelpNo"
                                            ValidationExpression="^[0-9/]{5,45}$"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Fax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:TextBox ID="txtFaxArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxArea)"
                                        runat="server" Width="32px" MaxLength="4"></asp:TextBox>-
									<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Silahkan isi Kode Area Fax dengan [3,4] digit angka"
                                        ControlToValidate="txtFaxArea" Display="None" ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="*" ControlToValidate="txtFaxArea"
                                        ValidationExpression="\d{3,4}"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtFaxNo" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxNo)"
                                            runat="server" Width="96px" MaxLength="20"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Silahkan isi No Fax dengan [5,14] digit angka"
                                                ControlToValidate="txtFaxNo" Display="None" ValidationExpression="\d{5,14}"></asp:RegularExpressionValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage="*" ControlToValidate="txtFaxNo"
                                        ValidationExpression="\d{5,14}"></asp:RegularExpressionValidator></td>
                        </tr>


                        <tr>
                            <td class="titleField">Email</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtEmailAdd" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtEmailAdd)"
                                    runat="server" Width="248px" MaxLength="40"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Silahkan isi email dealer dengan format email"
                                        ControlToValidate="txtEmailAdd" Display="None" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>&nbsp;
									 </td>
                        </tr>

                        <tr>
                            <td class="titleField" style="height: 26px">Situs web</td>
                            <td style="height: 26px">
                                <asp:TextBox Width="200px" ID="txtWeb" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtWeb)"
                                    runat="server" MaxLength="100"></asp:TextBox></td>
                        </tr>


                        <tr>
                            <td class="titleField" style="height: 18px">No Penunjukan Cabang Dealer</td>
                            <td style="height: 18px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtBranhAssigment" onblur="HtmlCharBlur(txtSPANumber)"
                                    runat="server" Width="312px" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtBranhAssigment"  Display="Static"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Tgl Penunjukan Cabang Dealer</td>
                            <td style="height: 18px">
                                <cc1:inticalendar id="icTglPersetujuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                        </tr>




                        <tr>
                            <td class="titleField" style="height: 19px">Status</td>
                            <td style="height: 19px">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="104px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><strong>Area Bisnis</strong></td>
                            <td>
                                <asp:CheckBox ID="cbSalesUnit" runat="server" AutoPostBack="True" Text="Sales Unit"></asp:CheckBox>&nbsp;&nbsp;
                                <asp:CheckBox ID="cbService" runat="server" AutoPostBack="True" Text="Service"></asp:CheckBox>&nbsp;&nbsp;
                                <asp:CheckBox ID="cbSpareParts" runat="server" AutoPostBack="True" Text="Spare Parts"></asp:CheckBox>&nbsp;&nbsp;

                            </td>
                        </tr>
                        <tr style="display: none">
                            <td><strong>Branch Manager</strong></td>
                            <td>
                                <asp:TextBox ID="txtNameBM" runat="server" Width="248px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td><strong>No. HP</strong></td>
                            <td>
                                <asp:TextBox ID="txtHPBM" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" Display="None" ControlToValidate="txtHPBM"
                                    ErrorMessage="Silahkan Isi No HP Sales Unit dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ControlToValidate="txtHPBM" ErrorMessage="*"
                                    ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson1" onblur="HtmlCharBlur(txtContactPerson1)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP1" onblur="HtmlCharBlur(txtHP1)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="txtHP1"
                                                    ErrorMessage="Silahkan Isi No HP Sales Unit dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="txtHP1" ErrorMessage="*"
                                                    ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 21px">
                                                <p>Email</p>
                                            </td>
                                            <td style="height: 21px">
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail1" onblur="HtmlCharBlur(txtEmail1)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="None" ControlToValidate="txtEmail1"
                                                    ErrorMessage="Email Sales Unit diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ControlToValidate="txtEmail1" ErrorMessage="*"
                                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                        </tr>

                                        <tr>
                                            <td>Dept. Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtDeptHead1" onblur="HtmlCharBlur(txtDeptHead1)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Section Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSectionHead1" onblur="HtmlCharBlur(txtSectionHead1)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sales AC</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesAC1" onblur="HtmlCharBlur(txtSalesAC1)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate1" runat="server" Font-Size="Smaller"></asp:Label></td>
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
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson2" onblur="HtmlCharBlur(txtContactPerson2)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP2" onblur="HtmlCharBlur(txtHP2)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" ControlToValidate="txtHP2"
                                                    ErrorMessage="Silahkan Isi No HP Servis dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="txtHP2" ErrorMessage="*"
                                                    ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>Email</p>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail2" onblur="HtmlCharBlur(txtEmail2)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" Display="None" ControlToValidate="txtEmail2"
                                                    ErrorMessage="Email Servis diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="txtEmail2" ErrorMessage="*"
                                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                        </tr>


                                        <tr>
                                            <td>Dept. Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtDeptHead2" onblur="HtmlCharBlur(txtDeptHead2)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Section Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSectionHead2" onblur="HtmlCharBlur(txtSectionHead2)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sales AC</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesAC2" onblur="HtmlCharBlur(txtSalesAC2)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate2" runat="server" Font-Size="Smaller"></asp:Label></td>
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
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtContactPerson3" onblur="HtmlCharBlur(txtContactPerson3)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>HP</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP3" onblur="HtmlCharBlur(txtHP3)"
                                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None" ControlToValidate="txtHP3"
                                                    ErrorMessage="Silahkan Isi No HP Spare Part dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="txtHP3" ErrorMessage="*"
                                                    ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>Email</p>
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail3" onblur="HtmlCharBlur(txtEmail3)"
                                                    runat="server" Width="140px" MaxLength="40"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" Display="None" ControlToValidate="txtEmail3"
                                                    ErrorMessage="Email Spare Part diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ControlToValidate="txtEmail3" ErrorMessage="*"
                                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                        </tr>


                                        <tr>
                                            <td>Dept. Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtDeptHead3" onblur="HtmlCharBlur(txtDeptHead3)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Section Head</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSectionHead3" onblur="HtmlCharBlur(txtSectionHead3)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sales AC</td>
                                            <td>
                                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesAC3" onblur="HtmlCharBlur(txtSalesAC3)"
                                                    runat="server" Width="140px" MaxLength="35"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>Perubahan Terakhir</td>
                                            <td>
                                                <asp:Label ID="lblLastUpdate3" runat="server" Font-Size="Smaller"></asp:Label></td>
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
                    <asp:Button ID="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False"></asp:Button><asp:Button ID="btnSave" runat="server" Width="60px" Text="Simpan"  ></asp:Button></td>
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
