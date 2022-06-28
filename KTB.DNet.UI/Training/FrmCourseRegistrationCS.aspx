<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCourseRegistrationCS.aspx.vb" Inherits=".FrmCourseRegistrationCS" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Data Status Siswa</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">

        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?category=cs&kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function getDetail(obj) {
            var kodeKelas = document.getElementById("hdnTempClass");
            kodeKelas.value = obj;
            var clickButton = document.getElementById("btnGet");
            clickButton.click();
        }

        function ShowPPDealerSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 600, 600, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = selectedDealer;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }
            else {
                txtDealerCodeSelection.onchange();
            }
        }

        function ShowPPCourseSelection2() {
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=cs', '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {
            var txtKodeKategori = document.getElementById("txtkodeKategori");
            txtKodeKategori.value = selectedCourse;
        }

    </script>
    <style type="text/css">
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
                    <asp:Label ID="lblPageTitle" Text="Training Customer Satisfaction - Pendaftaran" runat="server"></asp:Label>
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
                <td valign="top">
                    <table id="tbl7" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="60%">
                                <table id="table5">

                                    <tr>
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTahunFiscal" Width="120px" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label1" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td width="150" class="titleField">Bulan Mulai</td>
                                        <td style="height: 10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlBulan" Width="120px" runat="server" TabIndex="9"></asp:DropDownList><asp:Label ID="Label2" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td class="titleField" width="200px" height="22">Kode Kategori</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtkodeKategori" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lblkodeKategori" runat="server" Width="16px">
										        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"  onclick="ShowPPCourseSelection2()"/>
                                            </asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Width="48px" Text="Cari" CausesValidation="False"></asp:Button> &nbsp;&nbsp;&nbsp;
                                              <asp:Button ID="btnDownload" runat="server" Width="60px" Text="Download" CausesValidation="False"></asp:Button>
                                            <asp:Button ID="btnGet" runat="server" CssClass="hidden" Width="48px" Text="Cari" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 340px">
                        <asp:DataGrid ID="dtgClass"
                            Width="100%" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowPaging="true" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <Columns>
                                <asp:BoundColumn Visible="false" DataField="ID"></asp:BoundColumn>
                                  <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                  <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Kelas" SortExpression="ClassCode">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hKodeKelas" Text='<%# DataBinder.Eval(Container, "DataItem.ClassCode")%>' runat="server">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Kategori Kursus" SortExpression="TrCouse.CourseCode">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseCategory" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                   <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Mulai" SortExpression="StartDate">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTanggalMulai" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Selesai" SortExpression="FinishDate">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTanggalSelesai" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Lokasi" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLokasi" Text='<%# DataBinder.Eval(Container, "DataItem.Location")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Tahun Fiskal" SortExpression="FiscalYear">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFiscalYear" Text='<%# DataBinder.Eval(Container, "DataItem.FiscalYear")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                        
                                 <asp:TemplateColumn HeaderText="Kapasitas" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCapacity" Text='<%# DataBinder.Eval(Container, "DataItem.Capacity")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Siswa Terundang" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiswaTerundang"  runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                   <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Siswa Terdaftar" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiswaTerdaftar"  runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                   <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDaftar" runat="server" CausesValidation="False" Text="Tambah" CommandName="RegisterToClass">
												                <img src="../images/add.gif" border="0" alt="Daftar"></asp:LinkButton>
                                        <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="False" Text="Detail" CommandName="Detail">
												                <img src="../images/detail.gif" border="0" alt="Detail"/>
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
                <td></td>
            </tr>
        </table>
    </form>

</body>
</html>
