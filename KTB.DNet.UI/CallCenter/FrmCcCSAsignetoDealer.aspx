<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSAsignetoDealer.aspx.vb" Inherits=".FrmCcCSAsignetoDealer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Form CS Asigne to Dealer</title>
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
    <style type="text/css">
        
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">

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
        <table>
            <tr>
                <td class="titleField" style="width: 120px" height="1">Kode CS Employee
                </td>
                <td width="1%" height="1">:</td>
                <td style="width: 428px; height: 1px" nowrap width="428">
                    <asp:TextBox ID="txtSalesmanCode" runat="server" MaxLength="20"
                        Width="300px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 120px" height="1">Nama</td>
                <td width="1%" height="1">:</td>
                <td style="width: 428px; height: 1px" nowrap width="428">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="20"
                        Width="300px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="width: 120px" height="1">Posisi</td>
                <td width="1%" height="1">:</td>
                <td style="width: 428px; height: 1px" nowrap width="428">
                    <asp:TextBox ID="txtPosition" runat="server" MaxLength="50"
                        Width="300px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="left" width="100%">

                    <div id="div1" style="overflow: auto; height: 280px">
                        <asp:DataGrid ID="dtgDealer" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            ShowFooter="true" AllowSorting="true" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnIsMain" runat="server" />
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                        <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlDealer" Style="text-align: right" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="90%"></asp:DropDownList>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Dealer">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDealerNameF" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kota">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKota" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:Label ID="lblKotaF" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Username">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUserName" Width="95%" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="hapus">
									        <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="tambah">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button runat="server" ID="btnSimpan" Width="75px" CausesValidation="true" Text="Simpan" />&nbsp;
                    <asp:Button runat="server" ID="btnKembali" Width="75px" CausesValidation="false" Text="Kembali" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

