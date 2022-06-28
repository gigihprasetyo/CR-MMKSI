<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCertificateConfig.aspx.vb" Inherits=".PopUpCertificateConfig" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>PopUpJobPosition</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

        function getSelectedCourse() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgCertificate");
            for (i = 1; i < table.rows.length; i++) {
                var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (RadioButton != null && RadioButton.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var Course = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText;
                        window.returnValue = Course;
                        bcheck = true;
                        break;
                    }
                    else if (navigator.appName == "Netscape") {
                        var Course = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText;
                        window.opener.dialogWin.returnFunc(Course);
                        bcheck = true;
                        break;
                    }
                    else {
                        var Course = table.rows[i].cells[1].innerHTML + ';' + table.rows[i].cells[2].innerHTML;
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
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">Konfigurasi Sertifikat&nbsp;- Pencarian Konfigurasi</td>
                        </tr>
                        <tr>
                            <td style="height: 1px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px" width="20%">Nama</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:TextBox ID="txtNama" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 22px" width="2%"></td>
                            <td class="titleField" style="height: 22px" width="20%">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:Button></td>
                            <td style="height: 22px" width="1%"></td>
                            <td style="height: 22px" width="33%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 13px">Jabatan</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtJabatan" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px"></td>
                            <td style="height: 13px"></td>
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
                                    <asp:DataGrid ID="dtgCertificate" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
                                        CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowCustomPaging="True"
                                        AllowSorting="True" PageSize="100" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="ID">
                                                <HeaderStyle Width="5%" CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="NamaTTD" SortExpression="NamaTTD" HeaderText="Nama">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="JabatanTTD" SortExpression="JabatanTTD" HeaderText="Jabatan">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Keterangan">
                                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <input id="btnChoose" style="width: 60px" disabled onclick="getSelectedCourse()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Batal"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
