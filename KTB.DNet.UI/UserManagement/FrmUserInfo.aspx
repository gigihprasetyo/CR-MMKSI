<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmUserInfo.aspx.vb" Inherits="FrmUserInfo" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form User Info</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpSelectingDealer.aspx', '', 500, 860, DealerSelection);
        }

        function FindDealer() {
            var txtDealerCodeSelection = document.getElementById("txtkodeorg");
            if (txtDealerCodeSelection.value == "") {
                return;
            }
            txtDealerCodeSelection.onchange();
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtkodeorg");
            txtDealerCodeSelection.value = selectedDealer;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }
            else {
                txtDealerCodeSelection.onchange();
            }
        }

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("txtkodeorg");
            var dealerCode = lblDealer.value;
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealerBranch) {
            if (selectedDealerBranch.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtBranchCode");
                var lblBranchName = document.getElementById("lblNamaSubOrg");
                txtDealerSelection.value = selectedDealerBranch.split(";")[0];
                lblBranchName.innerHTML = selectedDealerBranch.split(";")[2];
            }
            else {
                var txtDealerSelection = document.getElementById("txtBranchCode");
                txtDealerSelection.value = selectedDealerBranch;
            }
        }
    </script>
    <script language="javascript">
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
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">ADMIN SISTEM - User Baru</td>
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
                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kode Organisasi :</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtkodeorg" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
                                    onblur="omitSomeCharacter('txtkodeorg','<>?*%$;')" Width="192px" AutoPostBack="True"
                                    MaxLength="6"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Font-Bold="True" ControlToValidate="txtkodeorg"
                                            ErrorMessage="Kode Org harus di isi">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Organisasi :</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblNamaOrg" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="trA" runat="server" visible ="false">
                            <td class="titleField" width="24%">Kode Sub Organisasi :</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtBranchCode" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
                                    onblur="omitSomeCharacter('txtkodeorg','<>?*%$;')" Width="192px" AutoPostBack="True"
                                    MaxLength="6"></asp:TextBox><asp:Label ID="lblSearchDealerBranch" onclick="ShowPPDealerBranchSelection()" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trB" runat="server" visible ="false">
                            <td class="titleField">Nama Sub Organisasi :</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblNamaSubOrg" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">&nbsp;</td>
                            <td width="1%"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nama Login <font color="crimson">*</font></td>
                            <td style="height: 17px" width="1%">:</td>
                            <td style="height: 17px" width="75%">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtUserName" onblur="HtmlCharBlur(txtUserName)"
                                    runat="server" Width="192px" MaxLength="14"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Bold="True" ControlToValidate="txtUserName"
                                        ErrorMessage="Login Harus diisi ">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Font-Bold="True" ControlToValidate="txtUserName"
                                            ErrorMessage="Minimum 4 Karakter " ValidationExpression="\w{4,20}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 130px">Kata Kunci <font color="crimson">*</font>&nbsp;</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPassword" onblur="HtmlCharBlur(txtPassword)"
                                    runat="server" Width="192px" MaxLength="10" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Bold="True" ControlToValidate="txtPassword"
                                        ErrorMessage="Kata kunci harus diisi ">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Font-Bold="True" ControlToValidate="txtPassword"
                                            ErrorMessage="Minimum 4 Karakter" ValidationExpression="\w{4,20}">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 130px">Konfirmasi Kata Kunci <font color="crimson">
										*</font></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKonfirmasi" onblur="HtmlCharBlur(txtKonfirmasi)"
                                    runat="server" Width="192px" MaxLength="10" TextMode="Password"></asp:TextBox>&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" Font-Bold="True" ControlToValidate="txtKonfirmasi"
                                        ErrorMessage="Kata Kunci tidak sesuai !" ControlToCompare="txtPassword">*</asp:CompareValidator></td>
                            <tr>
                                <td class="titleField" style="width: 125px">Pertanyaan Rahasia <font color="crimson">*</font></td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPertanyaan" onblur="HtmlCharBlur(txtPertanyaan)"
                                        runat="server" Width="440px" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Bold="True" ControlToValidate="txtPertanyaan"
                                            ErrorMessage=" Pertanyaan harus diisi">*</asp:RequiredFieldValidator></td>
                                <tr>
                                    <td class="titleField" style="width: 125px">Jawaban <font color="crimson">*</font></td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtJawaban" onblur="HtmlCharBlur(txtJawaban)"
                                            runat="server" Width="440px" MaxLength="100" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Bold="True" ControlToValidate="txtJawaban"
                                                ErrorMessage="Jawaban harus diisi">*</asp:RequiredFieldValidator></td>
                                    <tr>
                                        <td class="titleField">Nama Depan <font color="crimson">*</font>&nbsp;
                                        </td>
                                        <td width="1%">:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="TxtNamaDepan" onblur="HtmlCharBlur(TxtNamaDepan)"
                                                runat="server" Width="192px" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Font-Bold="True" ControlToValidate="TxtNamaDepan"
                                                    ErrorMessage="Nama Depan harus diisi ">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Font-Bold="True" ControlToValidate="TxtNamaDepan"
                                                        ErrorMessage="< dan > bukan karakter valid " ValidationExpression="[^\<\>]+">*</asp:RegularExpressionValidator></td>
                                    </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Nama Belakang
                            </td>
                            <td style="height: 26px" width="1%">:</td>
                            <td style="height: 26px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="TxtNamablk" onblur="HtmlCharBlur(TxtNamablk)"
                                    runat="server" Width="192px" MaxLength="30"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Font-Bold="True" ControlToValidate="TxtNamablk"
                                        ErrorMessage="< dan > bukan karakter valid" ValidationExpression="[^\<\>]+">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 26px">Tanggal Lahir
                            </td>
                            <td style="height: 26px" width="1%">:</td>
                            <td style="height: 26px">
                                <cc1:IntiCalendar ID="icTglLahir" runat="server"></cc1:IntiCalendar></td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi /&nbsp; Jabatan <font color="crimson">*</font>
                            </td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlPosition" runat="server" Width="144px"></asp:DropDownList><asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="TxtPosisi" onblur="HtmlCharBlur(TxtPosisi)"
                                    runat="server" Width="192px" MaxLength="30" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField">Telpon <font color="crimson">*</font>
                            </td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="TxtTlp" onblur="HtmlCharBlur(TxtTlp)"
                                    runat="server" Width="192px" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Font-Bold="True" ControlToValidate="TxtTlp"
                                        ErrorMessage="Telpon harus diisi ">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Font-Bold="True" ControlToValidate="TxtTlp"
                                            ErrorMessage="Nomor Telpon Tidak Sesuai ! " ValidationExpression="^[0,1,2,3,4,5,6,7,8,9,(,),-]{4,15}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Email <font color="crimson">*</font>
                            </td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmailAdd" onblur="HtmlCharBlur(txtEmailAdd)"
                                    runat="server" Width="192px" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Font-Bold="True" ControlToValidate="txtEmailAdd"
                                        ErrorMessage="Email harus diisi">*</asp:RequiredFieldValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Font-Bold="True" ControlToValidate="txtEmailAdd"
                                        ErrorMessage="Email tidak sesuai ! " ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">HP
                            </td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtHP" onblur="HtmlCharBlur(txtHP)"
                                    runat="server" Width="192px" MaxLength="30"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Font-Bold="True" ControlToValidate="txtHP"
                                        ErrorMessage="Nomor HP Tidak Sesuai !" ValidationExpression="^[0-9]{6,20}$">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 23px">Status
                            </td>
                            <td style="height: 23px" width="1%">:</td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="DdlStat" runat="server" Width="192px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="3" class="titleField" style="height: 23px">
                                <asp:CheckBox ID="chkDspNotification" runat="server" Text="Display Notification message when new bulletin posted" Style="display: none;"></asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Label ID="TxtError" runat="server" ForeColor="Red"></asp:Label><asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="270px"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <asp:DataGrid ID="dtgUserRole1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3"
                                    GridLines="Horizontal" CellSpacing="1">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                    <ItemStyle BackColor="White"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="WhiteSmoke" BackColor="OrangeRed"></HeaderStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
                                            <HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle Width="0%" CssClass="titleTableGeneral"></HeaderStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbAll" onclick="CheckAll('cbItem',this.checked)" runat="server"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbItem" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="RoleName" SortExpression="RoleName" HeaderText="Role Name">
                                            <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
                                            <HeaderStyle Width="65%" CssClass="titleTableGeneral"></HeaderStyle>
                                        </asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid></td>
                        </tr>
                        <tr height="40">
                            <td align="center">
                                <asp:Button ID="btnReset" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button><asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button></td>
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
