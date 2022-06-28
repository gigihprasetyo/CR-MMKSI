<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmClassCancelRegistration.aspx.vb" Inherits="FrmClassCancelRegistration" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmNewTrTrainee</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function FuncSuccess() {
            alert("Siswa Berhasil ditukar.")
            var btnBack = document.getElementById("btnKembali");
            btnBack.click();
        }

        function ShowPPCourseSelection() {
            showPopUp('../General/../PopUp/PopUpCourse.aspx', '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {

            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
        }


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="width: 825px; height: 20px">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 825px" background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 825px" height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 826px" valign="top">
                    <table id="Table2" style="width: 100%; height: 300px" cellspacing="1" cellpadding="1" width="800"
                        border="0">
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1">Detail Kelas :
                            </td>
                            <td width="1%" height="1"></td>
                            <td style="width: 428px; height: 1px" nowrap width="428"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1">Kode Kelas</td>
                            <td width="1%" height="1">:</td>
                            <td style="width: 428px; height: 1px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKelas" runat="server" MaxLength="20"
                                    Width="428px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1">Nama Kelas</td>
                            <td width="1%" height="1">:</td>
                            <td style="width: 428px; height: 1px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNamaKelas" runat="server" MaxLength="50"
                                    Width="428px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 1px" width="177" height="6">Lokasi</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtLokasi" runat="server" MaxLength="100"
                                    Width="428px" ReadOnly="True"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 177px; height: 12px" width="177" height="6">Tanggal Mulai</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="vertical-align:central; width: 428px; height: 6px" nowrap width="428">
                                <cc1:IntiCalendar ID="ICTanggalMulai" runat="server" TextBoxWidth="80" Enabled="False"></cc1:IntiCalendar></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 1px" width="177" height="6">Tanggal Selesai</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="vertical-align:central; width: 428px; height: 6px" nowrap width="428" >
                                <cc1:IntiCalendar ID="ICTanggalSelesai" runat="server" TextBoxWidth="80" Enabled="False"></cc1:IntiCalendar></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1">&nbsp;</td>
                            <td width="1%" height="1"></td>
                            <td style="width: 428px; height: 1px" nowrap width="428"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1">Detail Siswa :</td>
                            <td width="1%" height="1"></td>
                            <td style="width: 428px; height: 1px" nowrap width="428"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">No. Reg</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txNoReg" runat="server" Width="120px"
                                    MaxLength="100" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">Nama</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txName" runat="server" Width="424px"
                                    MaxLength="100" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">Posisi</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPosition" runat="server" Width="424px"
                                    MaxLength="100" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">Tipe Pertukaran</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px"  width="428">
                                <asp:DropDownList ID="ddlTipePertukaran"  runat="server" AutoPostBack="true" Width="300px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">No. Reg</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px"  width="428">
                                <asp:DropDownList ID="ddlSiswa" runat="server" AutoPostBack="true" Width="300px"></asp:DropDownList>
                            </td>
                        </tr>
                       
                        <tr id="trPosisi" runat="server">
                            <td class="titleField" style="width: 177px; height: 24px" width="177" height="6">Posisi</td>
                            <td style="height: 6px" width="1%" height="6">:</td>
                            <td style="width: 428px; height: 6px" nowrap width="428">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtPosisi2" runat="server" Width="424px"
                                    MaxLength="100" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1"></td>
                            <td width="1%" height="1"></td>
                            <td style="width: 428px; height: 1px" nowrap width="428">
                                </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 177px" width="177" height="1" colspan="3">
                                <hr style="width: 605px; height: 2px" size="2">
                            </td>
                        </tr>
                        
                        <tr>
                            <td >
                                <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button> &nbsp;
                                <asp:Button ID="btnBatal" runat="server" Width="60px" CausesValidation="False" Text="Batal"></asp:Button> &nbsp;
                                <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>&nbsp;</td> 
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
