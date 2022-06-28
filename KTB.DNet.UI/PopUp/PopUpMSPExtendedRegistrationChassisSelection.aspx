<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMSPExtendedRegistrationChassisSelection.aspx.vb" Inherits=".PopUpMSPExtendedRegistrationSelection" SmartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>MSP Registration Selection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">

        function GetSelectedCChassis() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgMSPRegistration");
            var MSPReg = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];

                if (CheckBox != null && CheckBox.checked) {

                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (MSPReg == '') {
                            MSPReg = replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        else {
                            MSPReg = MSPReg + ';' + replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        window.returnValue = MSPReg;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (MSPReg == '') {
                            MSPReg = replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        else {
                            MSPReg = MSPReg + ';' + replace(table.rows[i].cells[1].innerText, ' ', '') + '|' + replace(table.rows[i].cells[4].innerText, ' ', '');
                        }
                        bcheck = true;
                    }
                    else {
                        if (MSPReg == '') {
                            MSPReg = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + '|' + replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        else {
                            MSPReg = MSPReg + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + '|' + replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        }
                        bcheck = true;
                    }
                }
            }

            if (bcheck) {
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer")
                { opener.dialogWin.returnFunc(MSPReg); }
            }
            else {
                alert("Silahkan Pilih No Rangka terlebih dahulu");
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
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">MSP Extended -&nbsp;Pencarian No Rangka</td>
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
                            <td class="titleField" width="20%">No Rangka</td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:TextBox ID="txtChassisNumber" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" width="20%"><%--Tipe MSP--%></td>
                            <td width="1%"></td>
                            <td width="33%">
                                <%--<asp:dropdownlist runat="server" ID="ddlMSPType" AutoPostBack="true"></asp:dropdownlist>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 20px">Kode Dealer</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 20px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 20px">Dealer Registrasi</td>
                            <td style="height: 20px">:</td>
                            <td style="height: 20px">
                                <asp:TextBox ID="txtDealerReg" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
                                    onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:TextBox></td>
                            <td style="width: 17px; height: 20px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 20px"></td>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td width="17%" class="titleField">
                                <asp:Label ID="Label2" runat="server">Tgl Billing</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="32%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="DateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 17px; height: 20px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 20px"></td>
                            <td style="height: 20px"></td>
                        </tr>
                        <tr>
                            <td width="17%" class="titleField">
                                <asp:Label ID="Label1" runat="server">Tipe Program</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="32%">
                                <asp:DropDownList ID="ddlTipeProgram" runat="server"></asp:DropDownList>
                            </td>
                            <td style="width: 17px; height: 20px"></td>
                            <td class="titleField" style="height: 20px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
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
                                        <HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
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

                                            <asp:TemplateColumn HeaderText="MSP Registration ID" Visible="false">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMSPRegID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No Rangka">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Credit Account">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Dealer Registrasi">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerReg" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Registration No">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegistrationNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Tipe Pengajuan">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Tanggal Billing">
                                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedCChassis()" type="button"
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
