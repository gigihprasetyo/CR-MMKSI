<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCourse.aspx.vb" Inherits="PopUpCourse" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>

<head>
    <title>Pencarian Kategori</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

        function getSelectedCourse() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgCourseSelection");
            for (i = 1; i < table.rows.length; i++) {
                var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (RadioButton != null && RadioButton.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var Course = table.rows[i].cells[1].innerText;
                        window.returnValue = Course;
                        bcheck = true;
                        break;
                    }
                    else if (navigator.appName == "Netscape") {
                        var Course = table.rows[i].cells[1].innerText;
                        window.opener.dialogWin.returnFunc(Course);
                        bcheck = true;
                        break;
                    }
                    else {
                        var Course = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
                        window.opener.dialogWin.returnFunc(Course);
                        bcheck = true;
                        break;
                    }
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih kategori");
            }

        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">TRAINING -&nbsp;Pencarian 
									Kategori</td>
                        </tr>
                        <tr>
                            <td style="height: 1px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr id="rowCategory" runat="server">

                            <td class="titleField" style="height: 21px" width="20%">Pilih Kategory</td>
                            <td style="height: 21px" width="1%">:</td>
                            <td style="height: 21px" width="25%">
                                <asp:DropDownList ID="ddlJobPositionCategory" runat="server" Width="152px"></asp:DropDownList></td>
                            <td style="width: 17px; height: 21px" width="2%"></td>

                        </tr>

                         <tr id="rowPaymentType" runat="server">

                            <td class="titleField" style="height: 21px" width="20%">Tipe Pembayaran</td>
                            <td style="height: 21px" width="1%">:</td>
                            <td style="height: 21px" width="25%">
                                <asp:DropDownList ID="ddlPaymentType" runat="server" Width="152px"></asp:DropDownList></td>
                            <td style="width: 17px; height: 21px" width="2%"></td>

                        </tr>

                        <tr>
                            <td class="titleField" style="height: 21px" width="20%">Kode&nbsp;Kategori</td>
                            <td style="height: 21px" width="1%">:</td>
                            <td style="height: 21px" width="25%">
                                <asp:TextBox ID="txtKodeKategori" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 21px" width="2%"></td>
                            <td class="titleField" style="height: 21px" width="20%">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:Button></td>
                            <td style="height: 21px" width="1%"></td>
                            <td style="height: 21px" width="33%">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="80px" Visible="False">
                                    <asp:ListItem Value="1" Selected="True">Aktif</asp:ListItem>
                                    <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 13px">Nama Kategori</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtNamaKategori" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px"></td>
                            <td style="height: 13px">
                                <input id="txtSelectedIndex" type="hidden" runat="server"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 350px">
                                    <asp:DataGrid ID="dtgCourseSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True"
                                        AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Kategori" SortExpression="CourseCode">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeKategori" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CourseCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="CourseName" HeaderText="Nama Kategori" SortExpression="CourseName">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                             <asp:TemplateColumn HeaderText="Tipe Pembayaran">
                                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Description" HeaderText="Keterangan" SortExpression="Description">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Status">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="getSelectedCourse()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
