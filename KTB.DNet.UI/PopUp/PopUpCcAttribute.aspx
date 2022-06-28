<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCcAttribute.aspx.vb" Inherits=".PopUpCcAttribute" %>

<html>
<head>
    <title>CS Team</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=10; IE=11; IE=edge" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <style>
        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function GetSelectedUser() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgAttribute");
            var Kategori = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (Kategori == '') {
                            Kategori = table.rows[i].cells[1].innerText;
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
                        opener.dialogWin.returnFunc(Part);
                        bcheck = true;
                    }
                    else {
                        if (Kategori == '') {
                            Kategori = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            Kategori = Kategori + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        opener.dialogWin.returnFunc(Part);
                        bcheck = true;
                    }
                }
            }
            if (bcheck) {
                window.close();
                //if (navigator.appName != "Microsoft Internet Explorer")
                //{ opener.dialogWin.returnFunc(Kategori); }
            }
            else {
                alert("Silahkan pilih data");
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
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 18px" colspan="7">Atribut</td>
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
                            <td class="titleField" style="height: 22px" width="20%">Tipe Kendaraan</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:DropDownList ID="ddlVehicleCategory" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList></td>
                            <td style="width: 17px; height: 22px" width="2%"></td>
                            <td class="titleField" style="height: 22px" width="20%">
                                <asp:Button ID="btnSearch" runat="server" Width="80px" Text=" Cari "></asp:Button></td>
                            <td style="height: 22px" width="1%"></td>
                            <td style="height: 22px" width="33%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 22px" width="20%">Faktor</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:DropDownList ID="ddlFactor" runat="server" Width="200px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>

                        <%--<TR>
								<TD class="titleField" style="HEIGHT: 15px">Semua Employee</TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD style="HEIGHT: 15px">
                                    <asp:CheckBox ID="chxAllEmployee" runat="server" OnCheckedChanged="chxAllEmployee_CheckedChanged1" AutoPostBack="true"/>
                                </TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>--%>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 300px">
                                    <asp:DataGrid ID="dtgAttribute" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
                                        CellSpacing="1" ForeColor="GhostWhite" BorderColor="#CDCDCD"
                                        BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
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
                                            <asp:TemplateColumn SortExpression="ID" HeaderText="ID">
                                                <HeaderStyle CssClass="hidden" Width="1px"></HeaderStyle>
                                                <ItemStyle CssClass="hidden" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="FactorID" HeaderText="Faktor">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFactor" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="VehicleCategory" HeaderText="Tipe Kendaraan">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleCategory" runat="server">
                                                    </asp:Label>
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
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedUser()" type="button" value="Pilih"
                                    name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

