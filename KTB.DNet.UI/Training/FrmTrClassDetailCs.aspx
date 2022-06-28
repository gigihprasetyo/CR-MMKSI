﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrClassDetailCs.aspx.vb" Inherits=".FrmTrClassDetailCs" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmNewTrTrainee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        function ShowPPClassReference() {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            var txtKodeKelas = document.getElementById("txtKodeKelas");

            if (txtKodekategori.value == '' || txtKodeKelas.value == '') {
                Alert("Tidk terdapat kode kelas sebelumnya")
            }
            else {
                showPopUp('../General/../PopUp/PopUpClassReference.aspx?coursecode=' + txtKodekategori.value, '', 500, 760, clasSelection);
            }
        }
        function clasSelection(selectedClass) {
            var txtRefClass = document.getElementById("txtRefClass");
            txtRefClass.value = selectedClass;
        }
        function ShowPPCourseSelection() {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=cs', '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
            document.getElementById("getCode").click();
        }


    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" enctype="multipart/form-data" method="post" runat="server">

        <table id="Table99" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 825px; height: 20px" class="titlePage" colspan="2">Training Customer Satisfaction&nbsp;- 
						Detail Kelas</td>
            </tr>
            <tr>
                <td style="width: 825px" height="1" background="../images/bg_hor.gif" colspan="2">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td style="width: 825px" height="1" colspan="2">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table id="Table1" style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td valign="top">
                                <table style="width: 100%; height: 300px" id="Table2" border="0" cellspacing="1" cellpadding="1">
                                    <tr>
                                        <td width="150" class="titleField">Kode Kategori</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtKodeKategori" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
                                                Width="120px" AutoPostBack="False" TabIndex="8"></asp:TextBox><asp:Label ID="lblPopUpCourse" runat="server" Width="16px">
													<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ErrorMessage="*" ControlToValidate="txtKodeKategori" EnableClientScript="false"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Kode Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtKodeKelas" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
                                                Width="120px" TabIndex="1" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKodeKelas" EnableClientScript="false"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trRefClas" runat="server">
                                        <td width="150" class="titleField" height="1">Referensi Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtRefClass" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
                                                Width="120px" TabIndex="1"></asp:TextBox><asp:Label ID="lblPopupClass" runat="server" Width="16px">
													<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>&nbsp;&nbsp;
                                            <asp:Button ID="BtnLoad" runat="server" Width="60px" CausesValidation="false" Text="Load" TabIndex="15"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Nama Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtNamaKelas" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="2"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="txtNamaKelas" EnableClientScript="false"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Tipe Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTipeKelas" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Provinsi</td>
                                        <td width="1%" height="1">:</td>
                                        <td width="80%">
                                            <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            <asp:CustomValidator CssClass="style-warning" ID="cvPropinsi" runat="server" ControlToValidate="" OnServerValidate="cvPropinsi_ServerValidate" ErrorMessage="*Propinsi Harus Dipilih"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Kota</td>
                                        <td width="1%" height="1">:</td>
                                        <td width="80%">
                                            <asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList>
                                            <asp:CustomValidator CssClass="style-warning" ID="cvKota" runat="server" ControlToValidate="" OnServerValidate="cvKota_ServerValidate" ErrorMessage="*Kota Harus Dipilih"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="21">Nama Lokasi</td>
                                        <td style="height: 21px" height="21" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtLocationName" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="100"
                                                Width="320px" TabIndex="3"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ErrorMessage="*" ControlToValidate="txtLocationName" EnableClientScript="false"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="21">Alamat</td>
                                        <td style="height: 21px" height="21" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtLokasi" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="100"
                                                Width="320px" TabIndex="3"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="*" ControlToValidate="txtLokasi" EnableClientScript="false"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="23">Pengajar 1</td>
                                        <td style="height: 23px" height="23" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar1" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtPengajar1" EnableClientScript="false"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Pengajar 2</td>
                                        <td style="height: 14px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar2" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="5"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Pengajar 3</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar3" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="6"></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Kapasitas</td>
                                        <td style="height: 15px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtKapasitas" onkeypress="return numericOnlyUniv(event)" runat="server" MaxLength="3"
                                                Width="120px" TabIndex="7"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" ErrorMessage="*" ControlToValidate="txtKapasitas" EnableClientScript="false"></asp:RequiredFieldValidator>&nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimal 1 Orang" ControlToValidate="txtKapasitas"
                                                    MinimumValue="1" MaximumValue="10000" Type="Integer" Display="Dynamic"></asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTahunFiscal" Width="120px" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label1" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Status</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlStatus" Width="120px" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr valign="top">
                                        <td width="150" class="titleField">Keterangan</td>
                                        <td style="height: 67px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtKeterangan" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="250"
                                                Width="320px" Height="60px" TextMode="MultiLine" TabIndex="11"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKeterangan"
                                                Display="Dynamic" ValidationExpression="^[\s\S]{0,250}$"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Maksimum 250 karakter"
                                                ControlToValidate="txtKeterangan" ValidationExpression="^[\s\S]{0,250}$"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                        </td>
                                    </tr>
                                    <tr id="trUpload" runat="server">
                                        <td width="150" class="titleField">Upload Materi</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                                                runat="server">
                                            &nbsp;
                                            <asp:Button ID="btnUploadMateri" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                                            &nbsp;

                                        </td>
                                    </tr>
                                    <tr id="trDownload" runat="server">
                                        <td width="150" class="titleField">Download Materi</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%">
                                            <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download Materi" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;
                                              <asp:LinkButton ID="lbnDelete" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                                        </td>
                                    </tr>
                                     <tr>
                                        <td width="150" class="titleField">Url Materi</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtUrlPath" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="200"
                                                Width="320px" TabIndex="6"></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tanggal Mulai</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <cc1:IntiCalendar ID="ICTanggalMulai" runat="server" TextBoxWidth="80" TabIndex="12"></cc1:IntiCalendar>&nbsp;
                                              <asp:CustomValidator CssClass="style-warning" ID="cvTanggalMulai" runat="server" ControlToValidate="" OnServerValidate="cvTanggalMulai_ServerValidate" ErrorMessage=""></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tanggal Selesai</td>
                                        <td style="height: 17px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <cc1:IntiCalendar ID="ICTanggalSelesai" runat="server" TextBoxWidth="80" TabIndex="13"></cc1:IntiCalendar>&nbsp;
                                               <asp:CustomValidator CssClass="style-warning" ID="cvTanggalSelesai" runat="server" ControlToValidate="" OnServerValidate="cvTanggalSelesai_ServerValidate" ErrorMessage=""></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tanggal Batas Konfirmasi</td>
                                        <td style="height: 17px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <cc1:IntiCalendar ID="ICConfirmDueDate" runat="server" TextBoxWidth="80" TabIndex="13"></cc1:IntiCalendar>&nbsp;
                                               <asp:CustomValidator CssClass="style-warning" ID="cvConfirmDueDate" runat="server" ControlToValidate="" OnServerValidate="cvConfirmDueDate_ServerValidate" ErrorMessage=""></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField"></td>
                                        <td></td>
                                        <td width="80%" nowrap="nowrap">
                                            <p>
                                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" TabIndex="15"></asp:Button>
                                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"
                                                    TabIndex="16" Height="22px"></asp:Button>
                                                <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="14"></asp:Button>
                                                <asp:Button ID="getCode" runat="server" CssClass="hidden" CausesValidation="false" />
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
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
