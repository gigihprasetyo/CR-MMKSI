<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMRTCInstruktur.aspx.vb" Inherits=".PopUpMRTCInstruktur" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Popup Head MRTC</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <base target="_self">

    <script type="text/javascript">

        function GetSelectedUser() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgUserInfo");
            var Kategori = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Kategori == '') {
                            Kategori =table.rows[i].cells[1].innerText;
                        }
                        else {
                            Kategori = Kategori + ';' + table.rows[i].cells[1].innerText;
                        }
                        window.returnValue = Kategori;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (Kategori == '') {
                            Kategori = table.rows[i].cells[1].innerText;
                        }
                        else {
                            Kategori = Kategori + ';' + table.rows[i].cells[1].innerText;
                        }
                        bcheck = true;
                    }
                    else {
                        if (Kategori == '') {
                            Kategori = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Kategori = Kategori + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(Kategori); }
            }
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
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Instruktur MRTC</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                           <tr>
                            <td class="titleField" width="24%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtDealerCode" onblur="omitSomeCharacter('txtEmail','<>?*%$;')"
                                    runat="server" Width="248px" MaxLength="50" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Nama Trainee</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtName" onblur="omitSomeCharacter('txtUserName','<>?*%$;')"
                                    runat="server" Width="248px" MaxLength="20"></asp:TextBox></td>
                        </tr>
                     
                        <tr>
                            <td class="titleField" width="24%"></td>
                            <td width="1%"></td>
                            <td width="75%">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="64px"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" colspan="3">
                    <div id="div1" style="overflow: auto; height: 330px">
                        <asp:DataGrid ID="dtgUserInfo" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
                            CellPadding="3" GridLines="None" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
                                BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="2%"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ID" HeaderText="ID Trainee">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblID"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama Trainee">
                                    <HeaderStyle Width="35%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeName"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerCode"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40" align="center">&nbsp;<input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedUser()" type="button"
                    value="Pilih" name="btnChoose" runat="server"><input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
