<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrClassSales.aspx.vb" Inherits="FrmTrClassSales" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Master Kelas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">

        function ShowPPCourseSelection() {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=sales', '', 550, 760, courseSelection);
        }
        function ShowPPErrorExcel() {
            showPopUp('../General/../PopUp/PopUpErrorexcel.aspx', '', 500, 760, null);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
        }

        function ShowPPClassSelection() {
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=1', '', 500, 760, classSelection);
        }

        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtClassCode = document.getElementById("txtClassCode");
            txtClassCode.value = tempParam[0];
        }

        function validateCapacity() {

        }

    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" defaultbutton="btnCari" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">Training Sales - Kelas</td>
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
                    <br>
                    <asp:Panel ID="pnl1" runat="server">
                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="100">
                            <tr>
                                <td class="titleField" width="14%">Kode Kategori</td>
                                <td width="1%">:</td>
                                <td width="39%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKategori" runat="server" MaxLength="20"
                                        Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpCourse" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                <td class="titleField" width="20%">Periode Tahun</td>
                                <td width="1%">:</td>
                                <td width="29%">
                                    <asp:TextBox ID="txtPeriod" runat="server" Width="60px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="14%">Kode Kelas</td>
                                <td width="1%">:</td>
                                <td width="39%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtClassCode" runat="server" MaxLength="20"
                                        Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpClass" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                <td class="titleField">Lokasi</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" Width="136px"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="14%">Tipe Kelas</td>
                                <td width="1%">:</td>
                                <td width="39%">
                                    <asp:DropDownList ID="ddlTipeKelas" runat="server"></asp:DropDownList></td>

                                <td class="titleField">Tahun Fiskal</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTahunFiscal" Width="120px" runat="server" TabIndex="9"></asp:DropDownList></td>
                            </tr>

                            <tr>
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnCari" runat="server" Width="60" CausesValidation="False" Text=" Cari "></asp:Button>
                                    <asp:Button ID="btnBaru" runat="server" Width="60" CausesValidation="False" Text="Baru"></asp:Button>
                                    <asp:Button ID="btnReport" runat="server" Width="60" CausesValidation="False" Text="Laporan"></asp:Button></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">&nbsp;</td>
                            </tr>
                            <tr id="trTemplate" runat="server">
                                <td class="titleField" width="14%">
                                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                <td style="height: 23px">
                                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
                                <td style="height: 23px" colspan="4">
                                    <asp:LinkButton ID="linkTemplate" runat="server" CausesValidation="false">Template Excel</asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="trUpload" runat="server">
                                <td class="titleField" width="14%">
                                    <asp:Label ID="lblUploadFile" runat="server">Upload File</asp:Label></td>
                                <td style="height: 23px">
                                    <asp:Label ID="lblSprtUpload" runat="server">:</asp:Label></td>
                                <td style="height: 23px" colspan="4">
                                    <input onkeypress="return false;" id="fileUpload" style="width: 300px; height: 20px" type="file"
                                        size="46" name="fileUpload" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 360px">
                        <asp:DataGrid ID="dtgTrClass" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="50"
                            AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
                            BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ClassCode" SortExpression="ClassCode" HeaderText="Kode Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ClassName" SortExpression="ClassName" HeaderText="Nama Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="ClassType" HeaderText="Tipe Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeKelas" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrCourse.CourseCode" HeaderText="Kategori">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="FiscalYear" SortExpression="FiscalYear" HeaderText="Tahun Fiskal">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="LocationName" SortExpression="LocationName" HeaderText="Lokasi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Trainer1" SortExpression="Trainer1" HeaderText="Pengajar 1">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="Trainer2" SortExpression="Trainer2" HeaderText="Pengajar 2">
                                    <HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="Trainer3" SortExpression="Trainer3" HeaderText="Pengajar 3">
                                    <HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Tanggal Mulai" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FinishDate" SortExpression="FinishDate" HeaderText="Tanggal Selesai"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Capacity" SortExpression="Capacity" HeaderText="Kapasitas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Sisa Kapasitas">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSelisih" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="Description" SortExpression="Description" HeaderText="Keterangan">
                                    <HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Materi" SortExpression="FilePath">
                                    <HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkMateri" runat="server" CommandName="DownloadMateri" Visible="false">
                                           <img src="../images/unduh.png" border="0" alt="Download">
                                       </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnUbah" runat="server" Width="16px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Width="8px" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAddAllocation" runat="server" Width="8px" Text="Alokasi" CausesValidation="False"
                                            Visible="False" CommandName="AddAlloc">
												<img src="../images/icon_mail.gif" border="0" alt="Kirim Email *Alokasi Tambahan*"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid><asp:DataGrid ID="dtgUpload" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="500"
                            AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD"
                            BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn Visible="True" HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUClassCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Kelas">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUClassName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ClassType" HeaderText="Tipe Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTipeKelas" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUCategory" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="FiscalYear" SortExpression="FiscalYear" HeaderText="Tahun Fiskal">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Lokasi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblULocation" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Pengajar 1">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTrainer1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Mulai">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUStartDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Selesai">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUEndDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kapasitas">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUCapacity" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sisa Kapasitas">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblURemain" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Pesan">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUMessage" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUDetail" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnUEdit" runat="server" Width="16px" Text="Ubah" CausesValidation="False"
                                            CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnUDelete" runat="server" Width="8px" Text="Hapus" CausesValidation="False"
                                            CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnUAllocation" runat="server" Width="8px" Text="Alokasi" CausesValidation="False"
                                            Visible="False" CommandName="AddAlloc">
												<img src="../images/icon_mail.gif" border="0" alt="Kirim Email *Alokasi Tambahan*"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnBatal" runat="server" Width="60px" CausesValidation="False" Text="Batal"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnDownload" runat="server" Text="Download" Visible="False"></asp:Button>
                    <asp:Button ID="btnShowPopup" runat="server" CssClass="hidden" OnClientClick="ShowPPErrorExcel()" CausesValidation="false" />
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
