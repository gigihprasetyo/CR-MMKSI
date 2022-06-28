<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrReplacement.aspx.vb" Inherits=".FrmTrReplacement" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPCourseSelection1() {
            showPopUp('../PopUp/PopUpCourse.aspx?category=ass', '', 550, 760, courseSelection1);
        }

        function courseSelection1(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtCourseCode");
            txtKodeKategori.value = selectedCourse;
        }
        function ShowPPCourseSelection2() {
            //showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection2);
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=ass', '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtReplacementCode");
            txtKodeKategori.value = selectedCourse;
        }
       
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage"><asp:Label ID="lblTitle" Text="Training - Kategori Pengganti (After Sales)" runat="server"></asp:Label>
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
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">Kode Kategori</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox ID="txtCourseCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                                    Width="120px"></asp:TextBox>
                                <asp:Label ID="lblPopUpCourse" runat="server" Width="16px" onclick="ShowPPCourseSelection1();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ErrorMessage="* Kode kategori harus diisi" ControlToValidate="txtCourseCode"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 16px">Kode Pengganti Lulus</td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px">
                                <p>
                                    <asp:TextBox ID="txtReplacementCode" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
                                        Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpReplacement" runat="server" Width="16px" onclick="ShowPPCourseSelection2();">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ErrorMessage="* Kode pengganti lulus harus diisi" ControlToValidate="txtReplacementCode"></asp:RequiredFieldValidator></td>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Deskripsi</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <asp:TextBox ID="txtDesc" onkeypress="return HtmlCharUniv(event)" runat="server" Width="300px"
                                    MaxLength="250" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <p>
                                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                    <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:Button>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgReplacement" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowSorting="True" PageSize="25"
                            Width="100%" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrCourse.CourseCode" HeaderText="Kode Kategori">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrCourseCode" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Pengganti Lulus">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseReplacement" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Anda yakin ingin menghapus data ini?')">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40"></td>
            </tr>
            <tr>
                <td></td>
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

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
</body>
</html>
