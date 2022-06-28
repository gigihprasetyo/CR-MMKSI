<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDataStatusSiswa.aspx.vb" Inherits=".FrmDataStatusSiswa" %>

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
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>


    <script type="text/javascript">

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

        function ShowJobPosSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=5', '', 600, 600, JobPosSelection);
        }
        function JobPosSelection(selectedJobPos) {
            var txtPosisi = document.getElementById("txtPosisi");
            selectedJobPos = selectedJobPos + ';';

            var arrValue = selectedJobPos.split(';');
            txtPosisi.value = arrValue[0];
        }

        function ShowJobPosSelectionMany(obj) {
            showPopUp('../PopUp/PopUpJobPositionMany.aspx?area=' + obj.toString(), '', 600, 600, JobPosSelectionMany);
        }
        function JobPosSelectionMany(selectedJobPos) {
            var txtPosisi = document.getElementById("txtPosisi");
            txtPosisi.value = selectedJobPos;
        }

        function ShowPPAreaSelection() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpArea2.aspx', '', 600, 600, AreaSelection);
        }

        function AreaSelection(selectedArea) {
            var txtAreaCodeSelection = document.getElementById("txtArea");
            txtAreaCodeSelection.value = selectedArea;
        }


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnAreaID" runat="server" />
                    <asp:HiddenField ID="hdnCategory" runat="server" />
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
                            <td width="50%">
                                <table id="table5">
                                    <tr>
                                        <td class="titleField" width="200px" height="15">Kategori Posisi</td>
                                        <td style="height: 15px" width="20px">:</td>
                                        <td style="height: 15px" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlJobPositionCategory" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="200px" height="22">Kode Dealer</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtDealerCode" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowPPDealerSelection();"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trRegion" runat="server">
                                        <td class="titleField" width="200px" height="22">Region</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtArea" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lblSearchRegion" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowPPAreaSelection();" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kursus telah lulus</td>
                                        <td>:</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtCourseCodePass" runat="server"></asp:TextBox>&nbsp;Gunakan 
									tanda ( ; ) sebagai pemisah</td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Kursus tidak lulus</td>
                                        <td>:</td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtCourseCodeNotPass" runat="server"></asp:TextBox>&nbsp;Gunakan 
									tanda ( ; ) sebagai pemisah</td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Width="72px" Text="Cari"></asp:Button>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%">
                                <table id="table6">
                                    <%--<tr>
                                        <td class="titleField" width="200px" height="15"></td>
                                        <td style="height: 15px" width="20px"></td>
                                        <td style="height: 15px" nowrap="nowrap"></td>
                                    </tr>--%>
                                    <tr>
                                        <td class="titleField" width="200px">Nomor Registrasi</td>
                                        <td width="20px">:</td>
                                        <td>
                                            <asp:TextBox ID="txtNomorReg" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="200px">Nama</td>
                                        <td width="20px">:</td>
                                        <td>
                                            <asp:TextBox ID="txtNama" runat="server" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>

                                        <td class="titleField" width="200px" height="22">Status</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlStatus" Width="130px" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>

                                        <td class="titleField">Posisi</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtPosisi" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblSearchJobPos" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" ></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Button" Visible="False"></asp:Button></td>
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
                        <asp:DataGrid ID="dtgHeader" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" id="imgPlus" src="../images/plus.gif" />
                                        <%--  <asp:Label ID="lblNo" runat="server">
                                        </asp:Label>--%>
                                        <asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                                            <asp:DataGrid ID="dtgDetail" Width="100%" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundColumn HeaderText="Kategori Kursus Wajib" DataField="CourseCategoryCode" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Kategori Kursus Wajib Telah Lulus" DataField="CourseCategoryIsPass" SortExpression="">
                                                        <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Kategori Kursus Wajib Belum Lulus" DataField="CourseCategoryIsNotPass" SortExpression="">
                                                        <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No.Reg" SortExpression="">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Lahir" SortExpression="">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTanggalLahir" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Gender" SortExpression="">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGender" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kode Dealer Region" SortExpression="">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealerRegion" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Mulai Bekerja" SortExpression="">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMulaiBekerja" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Pendidikan" SortExpression="">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendidikan" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Level" SortExpression="">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLevel" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status" SortExpression="">
                                    <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLihat" runat="server" CausesValidation="False" Text="Lihat" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Lihat/Ubah"></asp:LinkButton>
                                        <%--<asp:LinkButton ID="btnDaftar" runat="server" CausesValidation="False" Text="Ubah" CommandName="RegisterToClass">
												<img src="../images/add.gif" border="0" alt="Daftar"></asp:LinkButton>--%>
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr id="trAction" runat="server">
                <td align="center" height="40">
                    <asp:Button ID="btnBaru" runat="server" Width="48px" Text="Baru" CausesValidation="False"></asp:Button>
                    <asp:Button ID="btnPrint" runat="server" Width="101px" Text="Cetak/Download" CausesValidation="False"
                        Enabled="False"></asp:Button>
                    <asp:Button ID="btnConfirmation" runat="server" Width="80px" CausesValidation="False" Text="Konfirmasi"></asp:Button>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>

</body>
</html>

