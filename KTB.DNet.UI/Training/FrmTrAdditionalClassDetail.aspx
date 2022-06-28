<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrAdditionalClassDetail.aspx.vb" Inherits=".FrmTrAdditionalClassDetail" %>

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
                Alert("Tidak terdapat kode kelas sebelumnya")
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
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=ass', '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
            document.getElementById('btnTriggerKategori').click();
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
                <td style="width: 825px; height: 20px" class="titlePage" colspan="2">TRAINING - INPUT TRAINING KELAS INHOUSE DAN FLEET</td>
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
                                                Width="120px" AutoPostBack="false" TabIndex="1"></asp:TextBox><asp:Label ID="lblPopUpCourse" runat="server" Width="16px">
													<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowPPCourseSelection();"></asp:Label>&nbsp; 
                                            <asp:CustomValidator CssClass="style-warning" ID="cvKodeKategori" runat="server" ControlToValidate="" OnServerValidate="cvKodeKategori_ServerValidate" ErrorMessage="*"></asp:CustomValidator></td>
                                      
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Kode Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:Label ID="lblKodeKelas" runat="server"></asp:Label>
                                            &nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td width="150" class="titleField" height="1">Nama Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:Label ID="lblNamaKelas" Visible="false" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtNamaKelas" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="100"
                                                Width="320px" TabIndex="8"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ErrorMessage="*Nama harus diisi" ControlToValidate="txtNamaKelas"></asp:RequiredFieldValidator></td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Tipe Kelas</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTipeKelas" AutoPostBack="true" CausesValidation="false" runat="server" TabIndex="4"></asp:DropDownList>
                                              <asp:CustomValidator ID="cvTipeKelas" runat="server" ControlToValidate="" OnServerValidate="cvTipeKelas_ServerValidate"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td width="150" class="titleField" height="1">Tahun Fiskal</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlFiscalYear" runat="server" TabIndex="4"></asp:DropDownList>
                                            <asp:CustomValidator ID="cvFiscalYear" runat="server" ControlToValidate="" OnServerValidate="cvFiscalYear_ServerValidate"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Provinsi</td>
                                        <td width="1%" height="1">:</td>
                                        <td width="80%">
                                            <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True" TabIndex="5"></asp:DropDownList>
                                            <asp:CustomValidator CssClass="style-warning" ID="cvPropinsi" runat="server" ControlToValidate="" OnServerValidate="cvPropinsi_ServerValidate" ErrorMessage="*Propinsi Harus Dipilih"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="1">Kota</td>
                                        <td width="1%" height="1">:</td>
                                        <td width="80%">
                                            <asp:DropDownList ID="ddlKota" runat="server" TabIndex="7"></asp:DropDownList>
                                            <asp:CustomValidator CssClass="style-warning" ID="cvKota" runat="server" ControlToValidate="" OnServerValidate="cvKota_ServerValidate" ErrorMessage="*Kota Harus Dipilih"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="21">Nama Lokasi</td>
                                        <td style="height: 21px" height="21" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtLocationName" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="100"
                                                Width="320px" TabIndex="8"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ErrorMessage="*Lokasi harus diisi" ControlToValidate="txtLocationName"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="21">Alamat</td>
                                        <td style="height: 21px" height="21" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtLokasi" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="100"
                                                Width="320px" TabIndex="9"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="*Alamat harus diisi" ControlToValidate="txtLokasi"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField" height="23">Pengajar 1</td>
                                        <td style="height: 23px" height="23" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar1" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="9"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Pengajar 1 harus diisi" ControlToValidate="txtPengajar1"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Pengajar 2</td>
                                        <td style="height: 14px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar2" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="10"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Pengajar 3</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtPengajar3" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                Width="320px" TabIndex="11"></asp:TextBox>&nbsp;</td>
                                    </tr>


                                    <tr valign="top">
                                        <td width="150" class="titleField">Keterangan</td>
                                        <td style="height: 67px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtKeterangan" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="250"
                                                Width="320px" Height="60px" TextMode="MultiLine" TabIndex="12"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKeterangan"
                                                Display="Dynamic" ValidationExpression="^[\s\S]{0,250}$"></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Maksimum 250 karakter"
                                                ControlToValidate="txtKeterangan" ValidationExpression="^[\s\S]{0,250}$"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="150" class="titleField" height="1">Nama Report</td>
                                        <td height="1" width="1%">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtNamaMateri" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                                TabIndex="22"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvMateri" ControlToValidate="txtNamaMateri" runat="server" ErrorMessage="*Nama Report harus diisi" EnableClientScript="false"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr id="trUpload" runat="server">
                                        <td width="150" class="titleField">Upload Report</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <input onkeypress="return false;" id="photoSrc" tabindex="23" type="file" size="29" name="File1"
                                                runat="server">
                                            &nbsp;
                                            <asp:Button ID="btnUploadMateri" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                                            &nbsp;
                                            <asp:CustomValidator ID="cvMateri" ControlToValidate="" runat="server" OnServerValidate="cvMateri_ServerValidate"></asp:CustomValidator>

                                        </td>
                                    </tr>
                                    <tr id="trDownload" runat="server">
                                        <td width="150" class="titleField">Download Report</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%">
                                            <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download Report" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;
                                              <asp:LinkButton ID="lbnDelete" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr id="trUploadList" runat="server">
                                        <td width="150" class="titleField">Upload List Siswa</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <input onkeypress="return false;" id="listSrc" tabindex="23" type="file" size="29" name="File1"
                                                runat="server">
                                            &nbsp;
                                            <asp:Button ID="btnUploadList" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                                            <asp:HiddenField ID="hdnFilePathList" runat="server" />
                                            &nbsp;
                                              <asp:CustomValidator ID="cvListSiswa" ControlToValidate="" runat="server" OnServerValidate="cvListSiswa_ServerValidate"></asp:CustomValidator>

                                        </td>
                                    </tr>
                                    <tr id="trDownloadList" runat="server">
                                        <td width="150" class="titleField">Download List Siswa</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%">
                                            <asp:LinkButton ID="lbtnDownloadList" runat="server" Text="Download List Siswa" CausesValidation="false"></asp:LinkButton>
                                            &nbsp;
                                              <asp:LinkButton ID="lbtnDeleteList" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tanggal Mulai</td>
                                        <td style="height: 12px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <cc1:IntiCalendar ID="ICTanggalMulai" runat="server" TextBoxWidth="80" TabIndex="24"></cc1:IntiCalendar>&nbsp;
                                            <asp:CustomValidator CssClass="style-warning" ID="cvTanggalMulai" runat="server" ControlToValidate="" OnServerValidate="cvTanggalMulai_ServerValidate" ErrorMessage="*"></asp:CustomValidator></td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Tanggal Selesai</td>
                                        <td style="height: 17px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <cc1:IntiCalendar ID="ICTanggalSelesai" runat="server" TextBoxWidth="80" TabIndex="25"></cc1:IntiCalendar>&nbsp;</td>
                                    </tr>
                                    <tr id="rowRevisi" runat="server" valign="top">
                                        <td width="150" class="titleField">Revisi</td>
                                        <td style="height: 67px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtRevisi" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="250"
                                                Width="320px" Height="60px" TextMode="MultiLine" TabIndex="11"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField"></td>
                                        <td></td>
                                        <td width="80%" nowrap="nowrap">
                                            <p>
                                                <asp:Button ID="btnApprove" runat="server" Width="60px" Text="Approve" TabIndex="26" CausesValidation="false"></asp:Button>
                                                <asp:Button ID="btnRevisi" runat="server" Width="60px" Text="Revisi" TabIndex="27" CausesValidation="false"></asp:Button>
                                                <asp:Button ID="btnRequest" runat="server" Width="60px" Text="Request" TabIndex="28"></asp:Button>
                                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"
                                                    TabIndex="29" Height="22px"></asp:Button>
                                                <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="30"></asp:Button>
                                                  <asp:Button ID="btnTriggerKategori" runat="server" CausesValidation="false" CssClass="hidden" />
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
    <script type="text/javascript">
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
