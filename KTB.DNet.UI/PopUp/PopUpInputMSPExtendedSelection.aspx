<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpInputMSPExtendedSelection.aspx.vb" Inherits=".PopUpInputMSPExtendedSelection" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>MSP Registration Selection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <base target="_self">
    <script language="javascript">
        function GetSelectedMSP() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgMSPRegistration");
            var MSP = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {

                        MSP = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + table.rows[i].cells[3].innerText + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');

                        window.returnValue = MSP;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {

                        MSP = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + table.rows[i].cells[3].innerText + ';' + replace(table.rows[i].cells[4].innerText, ' ', '');

                        window.opener.dialogWin.returnFunc(MSP);
                        bcheck = true;
                    }
                    else {

                        MSP = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[3].innerHTML + ';' + table.rows[i].cells[4].innerHTML;

                        window.opener.dialogWin.returnFunc(MSP);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih cabang");
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
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">MSP Extended -&nbsp;Pencarian Nomor MSP</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">Nomor MSP</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtChassisNumber" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td width="33%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 20px">Periode Pengajuan</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="DateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="DateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 17px; height: 20px"></td>
                            <td class="titleField" style="height: 20px"></td>
                            <td style="height: 20px"></td>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="65px"></asp:Button>
                            </td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 260px">
                                    <asp:DataGrid ID="dtgMSPRegistration" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#FDF1F2" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No MSP">
                                                <HeaderStyle Width="31%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoMSP" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No Rangka">
                                                <HeaderStyle Width="31%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Nama Kendaraan">
                                                <HeaderStyle Width="31%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedMSP()" type="button" value="Pilih" name="btnChoose" runat="server">
                                &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Batal" name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
