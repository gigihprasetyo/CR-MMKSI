<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDetailSiswa.aspx.vb" Inherits="FrmDetailSiswa" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmDetailTrTrainee</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerBranchSelection() {
            var dealerCode = document.getElementById("lblDealerCode").value;
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer.split(";")[0];
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }

        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function changeDeletePhoto(checked) {
            var varPhotoSrc = document.getElementById("photoSrc");
            if (checked)
                varPhotoSrc.style.visibility = "hidden";
            else
                varPhotoSrc.style.visibility = "visible";
        }

        function ShowJobPosSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpJobPosition.aspx?ServiceOnly=1', '', 600, 600, JobPosSelection);
        }
        function JobPosSelection(selectedJobPos) {
            var txtPosisi = document.getElementById("txtJobPosition");
            selectedJobPos = selectedJobPos + ';';

            var arrValue = selectedJobPos.split(';');
            txtPosisi.value = arrValue[0];
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <asp:HiddenField ID="hdnCategory" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
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
                <td valign="top">
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 24px" width="24%" height="24">Nama Siswa</td>
                            <td style="height: 24px" width="1%" height="24">:</td>
                            <td style="height: 24px" nowrap width="420">
                                <asp:Label ID="lblTraineeName" runat="server"></asp:Label></td>
                            <td style="height: 219px" valign="top" align="right" width="200" height="219" rowspan="8">
                                <div id="divPhoto" style="overflow: auto; width: 200px; height: 200px" align="right">
                                    <asp:Image ID="photoView" runat="server" ImageUrl="../WebResources/GetPhoto.aspx"></asp:Image>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 23px">Dealer</td>
                            <td style="height: 23px">:</td>
                            <td style="height: 23px" width="420">
                                <p>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                    <asp:HiddenField ID="lblDealerCode" runat="server" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 23px">Kota</td>
                            <td style="height: 23px">:</td>
                            <td style="height: 23px" width="420">
                                <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 23px">Kode Cabang Dealer</td>
                            <td style="height: 23px">:</td>
                            <td style="height: 23px">
                                <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 23px">Mulai Bekerja</td>
                            <td style="height: 23px">:</td>
                            <td style="height: 23px">
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 138px; height: 18px">Tanggal Lahir</td>
                            <td style="height: 18px">:</td>
                            <td style="width: 428px; height: 18px" valign="middle">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icBirthDate" runat="server"></cc1:IntiCalendar></td>
                                        <td>&nbsp; Format dd/mm/yyyy</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 138px">Jenis Kelamin</td>
                            <td>:</td>
                            <td id="tdddl" runat="server" width="428">
                                <asp:DropDownList ID="ddlGender" TabIndex="7" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlGender" ErrorMessage="Jenis Kelamin Harus dipilih">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">Email</td>
                            <td>:</td>
                            <td width="420">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEmail" runat="server" Width="200px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email harus diisi" Enabled="false">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Format email salah" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="true"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">No KTP</td>
                            <td>:</td>
                            <td width="420">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKTP" runat="server" Width="200px"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtKTP" ErrorMessage="No. KTP harus diisi" Enabled="false">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtKTP" ErrorMessage="Format No KTP salah" ValidationExpression="[0-9]{16}" Enabled="true"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi Pekerjaan</td>
                            <td>:</td>
                            <td width="420">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtJobPosition" runat="server" Width="340px"
                                    MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" Display="Dynamic" ControlToValidate="txtJobPosition"
                                        ErrorMessage="Posisi Pekerjaan harus diisi">*</asp:RequiredFieldValidator>
                                <asp:Label ID="lblSearchJobPos" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Level Pendidikan</td>
                            <td>:</td>
                            <td width="420">
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtEducationLevel" runat="server" Width="340px"
                                    MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" Display="Dynamic" ControlToValidate="txtEducationLevel"
                                        ErrorMessage="Level Pendidikan harus diisi">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Foto (Maks. 20KB)</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px" width="420">
                                <input id="photoSrc" onkeydown="return false" style="width: 340px; height: 20px" type="file"
                                    size="47" name="photoSrc" runat="server">
                                <asp:CheckBox ID="cbDeletePhoto" runat="server" Text="Hapus Foto" onclick="changeDeletePhoto(this.checked);"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr id="trSize" runat="server">
                            <td class="titleField" style="height: 16px">Ukuran Baju</td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px" width="420">
                                <asp:DropDownList ID="ddlShirtSize" runat="server" ForeColor="Black"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Status</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px" width="420">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="130px"></asp:DropDownList><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                            <td rowspan="2">
                                <asp:ValidationSummary ID="messageValidationSummary" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" align="right"></td>
                            <td></td>
                            <td width="420">
                                <p>
                                    <asp:Button class="hideButtonOnPrint" ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button><asp:Button class="hideButtonOnPrint" Text="Proses Cetak" ID="btnCetak" runat="server"></asp:Button><asp:Button class="hideButtonOnPrint" ID="btnRegister" runat="server" Text="Daftar" CausesValidation="False"
                                        ToolTip="Mendaftar Kelas"></asp:Button>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 826px" valign="top">
                    <div id="div1" style="overflow: auto; height: 150px">
                        <asp:DataGrid ID="dtgCourseClass" runat="server" Width="126%" GridLines="Horizontal" CellPadding="3"
                            BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif"
                            AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="White" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kode Kategori">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlClass" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Rank" HeaderText="Rangking">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="hlRank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.Location") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Materi">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnPath" runat="server" />
                                        <asp:LinkButton ID="btnDownload" runat="server" CausesValidation="False" CommandName="unduh" Text="Unduh">
											    <img src="../images/unduh.png" border="0" alt="Unduh"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                  <asp:TemplateColumn HeaderText="Sertifikat">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                         <asp:HiddenField ID="hdnId" runat="server" />
                                        <asp:LinkButton ID="btnDownloadCertificate" runat="server" CausesValidation="False" CommandName="downloadCertificate" Text="Unduh">
											    <img src="../images/unduh.png" border="0" alt="Unduh"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>

                </td>
            </tr>
            <tr>
               
                <td>
                    <br />
                    <br />
                    <asp:Label ID="lblCertificate" runat="server" Text="History Level :" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 826px" valign="top">
                    <div id="divCertificate" runat="server" style="overflow: auto; height: 150px">
                        <asp:DataGrid ID="dgCertificate" runat="server" Width="100%" GridLines="Horizontal" CellPadding="3"
                            BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" PageSize="10" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif"
                            AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="White" BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNo">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori Kursus" SortExpression="TrCourseCategory.Code">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCourseCategory">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Level" SortExpression="TrTraineeLevel.Description">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeLevel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Lulus" SortExpression="Tanggal Lulus">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTanggalLulus">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sertifikat">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownload" runat="server" CausesValidation="False" CommandName="unduh" Text="Unduh">
											    <img src="../images/unduh.png" border="0" alt="Unduh"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <asp:Button class="hideButtonOnPrint" ID="btnKembali" runat="server" Text="Kembali" Width="60px"
                        CausesValidation="False"></asp:Button>
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
