<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpBabitMasterLocation.aspx.vb" Inherits=".PopUpBabitMasterLocation" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>FrmBabitMasterLocation</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <base target="_self">
    <script language="javascript">

        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {
                    return pair[1];
                }
            }
            return "nothing";
        }

        function GetSelectedLocation() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgLocationSelection");
            var Location = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (getQueryVariable("x") == "Territory") {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].innerText;
                        }
                        else {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].innerText;
                        }
                        window.returnValue = Location;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (getQueryVariable("x") == "Territory") {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].innerText;
                        }
                        else {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].innerText;
                        }
                        window.close();
                        window.opener.dialogWin.returnFunc(Location);
                        bcheck = true;
                    }
                    else {
                        if (getQueryVariable("x") == "Territory") {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
                        }
                        else {
                            Location = table.rows[i].cells[0].innerText + ';' + table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
                        }
                        window.close();
                        window.opener.dialogWin.returnFunc(Location);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(Location); }
            }
            else {
                alert("Silahkan pilih lokasi");
            }
        }

        function replace(string, text, by) {
            var strLength = string.length, txtLength = text.length;
            if ((strLength == 0) || (txtLength == 0)) return string;

            var i = string.indexOf(text);
            if ((!i) && (text != string.substring(0, txtLength))) return string;
            if (i == -1) return string;

            var newstr = string.substring(0, i) + by;

            if (i + txtLength < strLength)
                newstr += replace(string.substring(i + txtLength, strLength), text, by);

            return newstr;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Master Lokasi -&nbsp;Pencarian Lokasi &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1" style="height: 1px !important;"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                    <TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td class="titleField" style="height: 13px">Nama Lokasi</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px">
                                <asp:TextBox ID="txtLocationName" runat="server" Width="152px"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Cari"></asp:Button>
                            </td>
                           <%-- <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px">Provinsi</td>
                            <td style="height: 13px">:</td>
                            <td style="width: 225px; height: 13px">
                                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true"></asp:DropDownList>

                            </td>--%>
                        </tr>

                        <%--<tr valign="top">
                            <td class="titleField" width="20%">Kota</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button>
                            </td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px"></td>
                            <td style="width: 225px; height: 13px"></td>
                        </tr>--%>

                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgLocationSelection" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True">
                                        <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Lokasi">
                                                <HeaderStyle Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocationName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LocationName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Region">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MainArea.Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Provinsi">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvinsi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kota">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKota" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedLocation()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;
                                <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                    name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
