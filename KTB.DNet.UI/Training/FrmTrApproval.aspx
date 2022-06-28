<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmTrApproval.aspx.vb" Inherits=".FrmTrApproval" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Training Approval</title>
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
        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
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
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=ass', '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtkodeKategori");
            txtKodeKategori.value = selectedCourse;
        }

        function ShowPopupMRTC() {
            showPopUp('../PopUp/PopUpMRTC.aspx', '', 500, 760, mrtcSelection);
        }


        function mrtcSelection(selectedMrtc) {
            var data = selectedMrtc.split(";");
            var txtKodeMrtc = document.getElementById("txtKodeMrtc");
            txtKodeMrtc.value = data[0];
        }

        function refreshGrid() {
            alert('Mohon lengkapi data kelas sebelumnya');
            var div = document.getElementById('<%=dvGrid.ClientID%>');
            var div_position = document.getElementById('<%= div_position.ClientID%>');
            var position = parseInt('<%=Request.Form("div_position") %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
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
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
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
                                   <%-- <tr id="trDealer" runat="server" visible="false">
                                        <td class="titleField" width="200px" height="22">Kode Dealer</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtDealerCode" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowPPDealerSelection();"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="titleField" width="200px" height="22">Kode Kategori</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtkodeKategori" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lblkodeKategori" runat="server" Width="16px">
										        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" />
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="200px" height="22">Kode MRTC</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtKodeMrtc" runat="server" Width="264px"></asp:TextBox>
                                            <asp:Label ID="lbtnKodeMrtc" runat="server" Width="16px">
										        <img style="cursor:hand" alt="Klik Popup" onclick="ShowPopupMRTC();" src="../images/popup.gif" border="0" />
                                            </asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Width="48px" Text="Cari" CausesValidation="False"></asp:Button></td>
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
                    <div id="dvGrid" runat="server" style="overflow: auto; height: 275px">
                        <input type="hidden" id="div_position" runat="server" />
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
                                            <asp:DataGrid ID="dtgClass" Width="100%" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Kode Kelas" SortExpression="">
                                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hKodeKelas" Text='<%# DataBinder.Eval(Container, "DataItem.ClassCode")%>' runat="server">
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tanggal Mulai" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTanggalMulai" Text='<%# DataBinder.Eval(Container, "DataItem.TanggalMulai")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tanggal Selesai" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTanggalSelesai" Text='<%# DataBinder.Eval(Container, "DataItem.TanggalSelesai")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Lokasi" SortExpression="">
                                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLokasi" Text='<%# DataBinder.Eval(Container, "DataItem.Lokasi")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Kapasitas" SortExpression="">
                                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKapasitas" Text='<%# DataBinder.Eval(Container, "DataItem.Kapasitas")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Siswa Terdaftar" SortExpression="">
                                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiswaTerdaftar" Text='<%# DataBinder.Eval(Container, "DataItem.SiswaTerdaftar")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Sisa Kapasitas" SortExpression="">
                                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSisaKapasitas" Text='<%# DataBinder.Eval(Container, "DataItem.SisaKapasitas")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="" SortExpression="">
                                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnApproval" runat="server" CausesValidation="False" Text="Detail / Approval">
												               <%-- <img src="../images/detail.gif" border="0" alt="Daftar/Approval"/>--%>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kode Kategori" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Kategori" SortExpression="">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseName" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kategori" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kategori Kursus" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseCtg" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Jumlah Kelas" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahKelas" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Jumlah Pendaftar" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlahPendaftar" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Total Siswa Terdaftar" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPendaftar" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Hold Pendaftar" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblholdpendaftar" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownload" runat="server" CausesValidation="False" Text="Download Excel" CommandName="download">
												<img style="cursor:hand" alt="Download Excel" src="../images/Excel-icon.png" border="0">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnDownload" Visible="false" runat="server" Width="100px" Text="Download" TabIndex="15"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
    </form>

</body>
</html>

