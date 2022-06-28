<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmPendaftaranSalesDetail.aspx.vb" Inherits="FrmPendaftaranSalesDetail" %>

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
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
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
                <td>
                    <TABLE id="Table2" cellSpacing="1" cellPadding="2" border="0">
							<colgroup>
								<col width="14%">
								<col width="1%">
								<col width="25%">
								<col width="24%">
								<col width="1%">
								<col width="35%">
							</colgroup>
							<TR>
								<TD class="titleField">Kode Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Mulai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblStartDate" runat="server"></asp:label></TD>
								<TD class="titleField">Selesai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblFinishDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Lokasi</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblLocation" runat="server"></asp:label></TD>
								<TD class="titleField">Alokasi</TD>
								<td width="1%">:</td>
								<TD>
									<div id="divAllocatedTot" style="WIDTH: 100%"><asp:label id="lblAllocatedTot" runat="server"></asp:label></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
							</TR>
						</TABLE>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div2" style="overflow: auto; height: 200px">
                        <asp:DataGrid ID="dtgBooking" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>

                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No. Reg" SortExpression="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="Name">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Mulai Bekerja" SortExpression="StartWorkingDate">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMulaiKerja" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi" SortExpression="RefJobPosition.Description">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                        <asp:HyperLink ID="hKodeKelas" runat="server">
                                        </asp:HyperLink>
                                    </ItemTemplate>
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
                    <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="14"></asp:Button>

                </td>
            </tr>
        </table>
    </form>

</body>
</html>

