<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrPreRequire.aspx.vb" Inherits="FrmTrPreRequire" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Form Prasyarat</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPCourseSelection1() {
            var hdnCategory = document.getElementById("hdnCategory");
            showPopUp('../PopUp/PopUpCourse.aspx?category=' + hdnCategory.value, '', 550, 760, courseSelection1);
        }

        function courseSelection1(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtCourseCode");
            txtKodeKategori.value = selectedCourse;
        }
        function ShowPPCourseSelection2() {
            var hdnCategory = document.getElementById("hdnCategory");
            //showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection2);
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=' + hdnCategory.value, '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtPreRequireCode");
            var txtDurasi = document.getElementById("txtDurasi");
            txtDurasi.disabled = false;
            txtKodeKategori.value = selectedCourse;
        }
        function ShowPPCourseSelection3() {
            var hdnCategory = document.getElementById("hdnCategory");
            //showPopUp('../PopUp/PopUpCourse.aspx','',500,760,courseSelection2);
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=' + hdnCategory.value, '', 550, 760, courseSelection3);
        }

        function courseSelection3(selectedCourse) {

            var txtPreRequireCodeNoPass = document.getElementById("txtPreRequireCodeNoPass");
            txtPreRequireCodeNoPass.value = selectedCourse;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
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
                            <td width="60%">
                                <table cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="height: 16px">Kode Kategori</td>
                                        <td style="height: 16px">:</td>
                                        <td style="height: 16px">
                                            <asp:TextBox ID="txtCourseCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                                                Width="120px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpCourse1" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ErrorMessage="*" ControlToValidate="txtCourseCode"></asp:RequiredFieldValidator></td>

                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 16px">Kode Prasyarat Lulus</td>
                                        <td style="height: 16px">:</td>
                                        <td style="height: 16px">
                                            <asp:TextBox ID="txtPreRequireCode" onkeypress="return HtmlCharUniv(event)" runat="server" OnTextChanged="txtPreRequireCode_TextChanged" MaxLength="20"
                                                Width="120px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpCourse2" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 16px">Lama Waktu</td>
                                        <td style="height: 16px">:</td>
                                        <td style="height: 16px">
                                            <asp:TextBox ID="txtDurasi" onkeypress="return onlyNumbers(event)" runat="server" MaxLength="2"
                                                Width="50px"></asp:TextBox>
                                            <asp:Label ID="lblDurasiDesc" runat="server" ForeColor="Red" Font-Size="XX-Small">*dalam bulan</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 16px">Kode Prasyarat Belum Lulus</td>
                                        <td style="height: 16px">:</td>
                                        <td style="height: 16px">
                                            <asp:TextBox ID="txtPreRequireCodeNoPass" onkeypress="return HtmlCharUniv(event)" runat="server"
                                                MaxLength="20" Width="120px"></asp:TextBox>
                                            <asp:Label ID="lblPreRequireCodeNoPass" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 24px">Deskripsi</td>
                                        <td style="height: 24px">:</td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtDesc" onkeypress="return HtmlCharUniv(event)" runat="server" Width="300px"
                                                MaxLength="250" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[\s\S]{0,250}$"
                                                ErrorMessage="*" ControlToValidate="txtDesc" Display="Dynamic"></asp:RegularExpressionValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                            <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="titleField" style="height: 16px;width:75px">Status </td>
                                        <td style="height: 16px">:</td>
                                        <td style="height: 16px">
                                            <asp:DropDownList ID="DdlStatus" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    <tr><td class="titleField" style="height: 16px"></tr>
                                    
                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>

                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgPreRequire" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowSorting="True" PageSize="25"
                            Width="100%" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
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
                                        <asp:Label ID="Label1" runat="server">
												<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="PreRequireCourseCode" SortExpression="PreRequireCourseCode" HeaderText="Kode Prasyarat">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Type Prasyarat" SortExpression="RequireType">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Lama Waktu (Dalam Bulan)" SortExpression="Prerequireduration">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrerequireduration" onkeypress="return onlyNumbers(event)" MaxLength="2" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSave" runat="server" Text="Simpan" CausesValidation="False" CommandName="Save" Visible="false">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
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
                <td>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                    <asp:HiddenField ID="hdnCriteria" runat="server" />
                </td>
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
