<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrAdditionalClass.aspx.vb" Inherits=".FrmTrAdditionalClass" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>Training Kelas Khusus</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
        function ShowPPErrorExcel() {
            showPopUp('../General/../PopUp/PopUpErrorexcel.aspx', '', 500, 760, null);
        }

        function ShowPPCourseSelection() {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=ass', '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
        }

        function ShowPPClassSelection() {
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=2', '', 500, 760, classSelection);
        }

        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtKodeKelas = document.getElementById("txtKodeKelas");
            txtKodeKelas.value = tempParam[0];
        }

        function ShowPopupDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, dealerSelection);
        }


        function dealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = data[0];

        }

    </script>

    <style type="text/css">
        .style-Label {
            width: 150px;
        }

        .style-Colon {
            width: 5px;
        }

        .style-Field {
            width: 200px;
        }

        .style-Separator {
            width: 100px;
        }

        .style-RowSeparator {
            height: 10px;
        }

        .hidden {
            display: none;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" Text="Training After Sales - Kelas Inhouse dan Fleet" runat="server"></asp:Label>
                </td>
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
                    <table>
                        <tr>
                            <td class="style-Label">Kode Kategori</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtKodeKategori" runat="server" Width="80%"></asp:TextBox>
                                <asp:Label ID="lblPopUpKodeKategori" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPPCourseSelection();" border="0" /></asp:Label>
                            </td>
                            <td class="style-Separator"></td>
                            <td class="style-Label">Lokasi</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtLokasi" runat="server" Width="100%"></asp:TextBox>

                            </td>
                            <td class="style-Separator"></td>
                            <div id="divKodeDealer" runat="server" visible="true">
                                <td class="style-Label">Kode Dealer</td>
                                <td class="style-Colon">:</td>
                                <td class="style-Field">
                                    <asp:TextBox ID="txtKodeDealer" runat="server" Width="80%"></asp:TextBox>
                                    <asp:Label ID="lblPopUpKodeDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPopupDealer();" border="0" /></asp:Label>
                                </td>
                            </div>
                        </tr>
                        <tr>
                            <td class="style-Label">Kode Kelas</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtKodeKelas" runat="server" Width="80%"></asp:TextBox>
                                <asp:Label ID="lblPopUpKodeKelas" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPPClassSelection();" border="0" /></asp:Label>
                            </td>
                            <td class="style-Separator"></td>
                            <td class="style-Label">Tipe Kelas</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:DropDownList ID="ddlTipeKelas" runat="server"></asp:DropDownList>
                            </td>
                            <td class="style-Separator"></td>
                            <td class="style-Label">Status</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style-Label">Tanggal Periode Training</td>
                            <td class="style-Colon">:</td>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalMulaiFrom" runat="server" Value=""></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalMulaiTo" runat="server" Value=""></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="3"></td>
                            <td class="style-Label">Tahun Fiskal</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr id="trTglSelesai" runat="server" visible="false">
                            <td class="style-Label">Tanggal Selesai</td>
                            <td class="style-Colon">:</td>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalSelesaiFrom" runat="server" Value=""></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICTanggalSelesaiTo" runat="server" Value=""></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td class="style-Field">
                                <asp:LinkButton ID="lbtnDownloadTemplate" Text="Download Template Excel" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <div id="divUpload" runat="server" visible="true">
                                <td class="style-Label">Upload Kelas</td>
                                <td class="style-Colon">:</td>
                                <td class="style-Field">
                                    <input type="file" id="fileUpload" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="80%" /></td>
                            </div>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td class="style-Field">
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="75px" />
                                &nbsp;
                                <asp:Button ID="btnBaru" runat="server" Text="Baru" Width="75px" />
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                 Jumlah data : 
                    <asp:Label ID="lblRowTotal" runat="server" Text ="0"></asp:Label>
                </td>
            </tr>
            <tr class="style-RowSeparator">
                <td>
                    <asp:Button ID="btnShowPopup" runat="server" CssClass="hidden" OnClientClick="ShowPPErrorExcel()" CausesValidation="false" /></td>
            </tr>
            <tr id="rowList" runat="server">
                <td>
                    <asp:DataGrid ID="dgList" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="50"
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
                             <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
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
                                <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn DataField="LocationName" SortExpression="LocationName" HeaderText="Lokasi">
                                <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="Trainer1" SortExpression="Trainer1" HeaderText="Pengajar 1">
                                <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Tanggal Mulai" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FinishDate" SortExpression="FinishDate" HeaderText="Tanggal Selesai"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>


                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="0%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Download">
                                <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownloadFile" Text="Download Report" runat="server" CommandName="DownloadMateri"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDownloadList" Text="Download List Siswa" runat="server" CommandName="DownloadList"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                    <asp:LinkButton ID="btnUbah" runat="server" Width="16px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton ID="btnHapus" runat="server" Width="8px" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                     <asp:Button ID="btnDownload" runat="server" Text="Download" Width="75px" />
                </td>

            </tr>


            <tr id="rowUpload" runat="server" visible="false">
                <td>
                    <%--  <asp:DataGrid ID="dtgTrClass" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="50"
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
                                <ItemStyle Font-Size="XX-Small" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ClassName" SortExpression="ClassName" HeaderText="Nama Kelas">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
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
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           
                            <asp:BoundColumn DataField="LocationName" SortExpression="LocationName" HeaderText="Lokasi">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Trainer1" SortExpression="Trainer1" HeaderText="Pengajar 1">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="Trainer2" SortExpression="Trainer2" HeaderText="Pengajar 2">
                                <HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="Trainer3" SortExpression="Trainer3" HeaderText="Pengajar 3">
                                <HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Tanggal Mulai" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FinishDate" SortExpression="FinishDate" HeaderText="Tanggal Selesai"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                          
                         
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
                    </asp:DataGrid>--%>
                    <asp:DataGrid ID="dgUpload" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="500"
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
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" />
                   
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
