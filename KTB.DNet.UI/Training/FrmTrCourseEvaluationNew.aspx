<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrCourseEvaluationNew.aspx.vb" Inherits=".FrmTrCourseEvaluationNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Form Training Course Evaluation</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPCourseSelection1() {
            var areas = document.getElementById("IDArea").value;
            showPopUp('../PopUp/PopUpCourse.aspx?category=' + areas, '', 550, 760, courseSelection1);
        }

        function courseSelection1(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtCourseCode");
            txtKodeKategori.value = selectedCourse;
        }
        function ShowPPCourseSelection2() {
            var areas = document.getElementById("IDArea").value;
            showPopUp('../PopUp/PopUpCourse.aspx?category=' + areas, '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {

            var txtKodeKategori = document.getElementById("txtCodeRef");
            txtKodeKategori.value = selectedCourse;
        }
        function Confirm() {
            var confirm_value = document.getElementById("ConfirmDelete");
            if (confirm("Yakin data ini akan dihapus?")) {
                confirm_value.value = "yes";
            } else {
                confirm_value.value = "no";
            }
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <asp:HiddenField ID="IDProccess" runat="server" />
        <asp:HiddenField ID="IDCourse" runat="server" />
        <asp:HiddenField ID="IDArea" runat="server" />
        <asp:HiddenField ID="ConfirmDelete" runat="server" />
        <table id="Table0" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">

            <tr>
                <td class="titleField" style="height: 29px" width="24%">Kode Kategori</td>
                <td width="1%" style="height: 29px">:</td>
                <td width="75%" style="height: 29px">
                    <asp:TextBox ID="txtCourseCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                        Width="120px"></asp:TextBox>
                    <asp:Label ID="lblPopUpCourse" runat="server" Width="16px" onclick="ShowPPCourseSelection1();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ErrorMessage="* Kode kategori harus diisi" ControlToValidate="txtCourseCode"></asp:RequiredFieldValidator></td>
            </tr>
            <tr id="trReferensi" runat="server">
                <td class="titleField" style="height: 29px" width="24%">
                    <asp:CheckBox ID="chkItemChecked" Text="Reference Kategori" runat="server" AutoPostBack="true"></asp:CheckBox></td>
                <td width="1%" style="height: 29px">:</td>
                <td width="75%" style="height: 29px" runat="server" id="tdRef">
                    <asp:TextBox ID="txtCodeRef" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                        Width="120px"></asp:TextBox>
                    <asp:Label ID="lblPopUpCourseRef" runat="server" Width="16px" onclick="ShowPPCourseSelection2();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    &nbsp;<asp:Button ID="btncheck" runat="server" Text="Check" OnClick="btncheck_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <p>
                        <asp:Button ID="BtnLihat" runat="server" Text="Lihat" Width="60px"></asp:Button>&nbsp;
						<asp:Button ID="BtEdit" runat="server" Text="Edit" Width="60px"></asp:Button>
                    </p>
                </td>
            </tr>

        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="left">
                    <asp:Menu
                        ID="Menu1"
                        Width="168px"
                        runat="server"
                        Orientation="Horizontal"
                        StaticEnableDefaultPopOutImage="False"
                        OnMenuItemClick="Menu1_MenuItemClick" BorderWidth="0px" DynamicMenuStyle-BackColor="#738A9C">
                        <Items>
                            <asp:MenuItem Text="Angka" ImageUrl="../images/aktif.gif" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="Sikap" Value="1"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td align="left" width="100%">
                    <asp:MultiView ID="MultiTabs" runat="server" ActiveViewIndex="0">
                        <asp:View ID="Tab1" runat="server">

                            <table id="table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:LinkButton ID="LinkAdd1" CausesValidation="False" CommandName="add" alt="Tambah" Text="Tambah" runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah">Tambah</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right"></td>
                                </tr>
                            </table>
                            <div id="div1" style="overflow: auto; height: 280px">
                                <asp:DataGrid ID="dtgNilaiAngka" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
                                    AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" CellPadding="3" BorderWidth="0px" AllowSorting="True"
                                    CellSpacing="1" PageSize="25">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle CssClass="titleTableService" Width="2%" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo1" runat="server" NAME="lblNo1" Text="1"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Size="Small" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ID">
                                            <HeaderStyle CssClass="titleTableService" Width="2%" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID1" runat="server" NAME="lblID1" Text="1"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Size="Small" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jenis Tes">
                                            <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDEva1" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nama Evaluasi">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="NamaEvaluasi1" Width="90%" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>

                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Deskripsi">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="Description1" Width="90%" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="2%" CssClass="titleTableService" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" alt="Hapus" OnClientClick="Confirm();" CommandName="Delete">
									            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </div>
                        </asp:View>
                        <asp:View ID="Tab2" runat="server">
                            <table id="table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:LinkButton ID="LinkAdd2" CausesValidation="False" CommandName="add" alt="Tambah" Text="Tambah" runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah">Tambah</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                            <div id="div2" style="overflow: auto; height: 280px">
                                <asp:DataGrid ID="dtgNilaiSikap" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
                                    AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" CellPadding="3" BorderWidth="0px" AllowSorting="True"
                                    CellSpacing="1" PageSize="25">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle CssClass="titleTableService" Width="2%" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo2" runat="server" NAME="lblNo2" Text="1"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Size="Small" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ID">
                                            <HeaderStyle CssClass="titleTableService" Width="2%" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID2" runat="server" NAME="lblID2" Text="1"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Size="Small" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jenis Tes">
                                            <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDEva2" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nama Evaluasi">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="NamaEvaluasi2" Width="90%" runat="server"></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Deskripsi">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="Description2" Width="90%" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="2%" CssClass="titleTableService" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" OnClientClick="Confirm();" CausesValidation="False" alt="Hapus" CommandName="Delete">
									            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button runat="server" ID="btnSimpan" CausesValidation="true" Text="Simpan" />&nbsp;
                    <asp:Button runat="server" ID="btnBatal" CausesValidation="false" Text="Batal" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
