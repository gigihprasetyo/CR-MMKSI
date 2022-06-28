<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmListPendaftaranASS.aspx.vb" Inherits="FrmListPendaftaranASS" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTrCertificateLine3</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function ShowPPDealerSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 600, 600, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtKodeDealer");
            txtDealerCodeSelection.value = selectedDealer;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }

        }

        function ShowPPCourseSelection(obj) {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=' + obj.toString(), '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
        }


        function ClassSelection(selectedCode) {
            var tempParam = selectedCode.split(';');
            var str1 = document.getElementById("txtClassCode");
            {
                str1.value = tempParam[0];
                str1.focus()
            }
        }

        function ShowPopupClassSelection() {
            var txtYear = document.getElementById("ddlFiscalYear");
            var txtCourse = document.getElementById("txtKodeKategori");
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=2&fiscalyear=' + txtYear.value + '&CourseCode=' + txtCourse.value, '', 500, 760, ClassSelection)
        }

        function GetCatatan() {
            return false
        }

        function SelectAll(CheckBoxControl) {
            if (CheckBoxControl.checked == true) {
                var i;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') &&
					(document.forms[0].elements[i].name.indexOf('dgNumEval') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }
            else {
                var i;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') &&
                    (document.forms[0].elements[i].name.indexOf('dgNumEval') > -1)) {
                        document.forms[0].elements[i].checked = false;
                    }
                }
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
    </script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 175px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnAreaID" runat="server" />
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 750px" class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 750px" height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td style="width: 750px" height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>

            <tr>
                <td style="width: 100%" align="left">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2">
                        <tr>
                            <td class="titleField" width="100px" style="height: 18px">Kode Organisasi</td>
                            <td width="1%" style="height: 18px">:</td>
                            <td style="width: 350px; height: 18px">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>

                        </tr>
                        <tr>
                            <td class="titleField" width="100px">Nama Organisasi</td>
                            <td width="1%">:</td>
                            <td style="width: 350px">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            <td class="titleField" width="20%">No Registrasi</td>
                            <td width="1%">:</td>
                            <td width="216" style="width: 216px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoReg" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr id="trSearch" runat="server">
                            <td class="titleField" width="100px">Kode Dealer</td>
                            <td style="height: 10px">:</td>
                            <td width="350px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeDealer" runat="server" Width="150px"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>

                            </td>
                            <td class="titleField" width="20%" style="height: 26px">Nama Siswa</td>
                            <td width="1%" style="height: 26px">:</td>
                            <td width="216" style="width: 350px; height: 26px">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtTraineeName" runat="server" Width="256px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td width="100px" class="titleField">Kode Kategori</td>
                            <td style="height: 10px">:</td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtKodeKategori" onkeypress="return HtmlCharUniv(event)" TabIndex="2" runat="server" Width="150px" MaxLength="20"
                                    AutoPostBack="False"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblPopUpCourse" runat="server" Width="16px">
													<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                            <td class="titleField">Periode Kelas
                            </td>
                            <td>:</td>
                            <td style="width: 216px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDate" runat="server"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <cc1:IntiCalendar ID="icStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server">s.d</asp:Label>
                                        </td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="100px">Kelas</td>
                            <td style="height: 19px">:</td>
                            <td style="height: 19px">
                                <asp:TextBox ID="txtClassCode" TabIndex="3" onkeypress="return HtmlCharUniv(event)" runat="server" Width="150px"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblPopUpClass" runat="server" ToolTip="Klik PopUp" Width="16px">
										<img style="cursor:hand" src="../images/popup.gif" border="0" /></asp:Label></td>
                               <td class="titleField" width="20%" style="height: 26px">Tahun Fiskal</td>
                            <td width="1%" style="height: 26px">:</td>
                            <td width="216" style="width: 350px; height: 26px">
                              <asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList></asp:TextBox></td>

                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnCari" runat="server" Width="75px" Text="Cari" />&nbsp;
                    <asp:Button ID="btnBatal" runat="server" Width="75px" Text="Batal" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="titleField">&nbsp;</td>
            </tr>


        </table>

        <asp:Panel ID="panelGrid" runat="server" ScrollBars="Horizontal" Width="100%">
            <asp:DataGrid ID="dtgHeader" runat="server" Width="2000px" AutoGenerateColumns="False"
                GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                AllowCustomPaging="True" AllowPaging="true" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle BackColor="White"></ItemStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No" SortExpression="">
                        <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnID" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Tanggal Mulai Kerja" SortExpression="TrTrainee.StartWorkingDate">
                        <HeaderStyle Width="100px" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTanggalMulaiKerja" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="No. Reg" SortExpression="TrTrainee.ID">
                        <HeaderStyle Width="100px" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="TrTrainee.Name">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNamaSiswa" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" ></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kota Dealer" SortExpression="Dealer.City.CityName">
                        <HeaderStyle  CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCity" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Kelas" SortExpression="TrClass.ClassCode">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkClass" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Kelas" SortExpression="TrClass.ClassName">
                        <HeaderStyle  CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblClassName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanggal Mulai" SortExpression="TrClass.StartDate">
                        <HeaderStyle  CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTanggalMulai" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanggal Selesai" SortExpression="TrClass.FinishDate">
                        <HeaderStyle  CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTanggalSelesai" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Posisi" SortExpression="TrTrainee.RefJobPosition.Description">
                        <HeaderStyle  CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPosisi" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Size" SortExpression="TrTrainee.ShirtSize">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSize" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Gender" SortExpression="TrTrainee.Gender">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblGender" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Remark" SortExpression="">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="" SortExpression="">
                        <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="add" CausesValidation="False" ToolTip="Pilih Siswa">
												<img src="../images/add.gif" border="0" alt="Daftar MKS"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnReplace" runat="server" Text="Tukar" CausesValidation="False" CommandName="replace">
												<img src="../images/unregistered.gif" border="0" alt="Tukar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </asp:Panel>
        <table style="background-color:gainsboro; width:100%">
            <tr>
                <td class="titleField">Terdaftar</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Tidak ada tagihan / Tagihan Sudah Lunas</td>
                <td style="height: 10px"></td>
                <td class="titleField">Proses Transfer</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Pembayaran masih dalam Proses Transfer</td>
            </tr>
            <tr>
                <td class="titleField">Belum Disetujui</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Daftar Siswa Berbayar belum di approve</td>
                <td style="height: 10px"></td>
                <td class="titleField">Tidak Lulus</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Siswa tidak lulus Training</td>
            </tr>
            <tr>
                <td class="titleField">Proses Debit note</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Pembayaran masih dalam Proses menggunakan Debit note</td>
                <td style="height: 10px"></td>
                <td class="titleField">Lulus</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Siswa lulus training</td>
            </tr>
            <tr>
                <td class="titleField">Proses Pencairan Deposit B</td>
                <td style="height: 10px">=></td>
                <td class="titleField">Pembayaran masih dalam Proses Pencairan deposit B</td>
            </tr>
            
        </table>
        <table>
            <tr>
                <td class="titleField" width="100px"></td>
                <td style="height: 10px"></td>
                <td width="350px"></td>
                <td class="titleField" width="20%" style="height: 26px"></td>
                <td width="1%" style="height: 26px"></td>
                <td width="216" style="width: 350px; height: 26px"></td>
            </tr>
            <tr id="trAction" runat="server">
                <td align="center" colspan="6">
                    <asp:Button ID="btnCancel" Width="75px" runat="server" Text="Cancel" />&nbsp;
                    <asp:Button ID="btnDownload" Width="75px" runat="server" Text="Download" />
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
