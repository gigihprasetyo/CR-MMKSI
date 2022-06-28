<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpTOPSPBilling.aspx.vb" Inherits=".PopUpTOPSPBilling" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PopUp TOP SparePart Billing</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script>

        function ShowPPDealerSelection() {
            var txtDealerGroup = document.getElementById("lblGroup");
            var txtDealer = document.getElementById("txtDealerCode");
            showPopUp('../PopUp/PopUpDealerSelectionTOP.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtDealerCode");
            txtDealer.value = selectedDealer;
        }

        function GetSelectedBillNo() {
            var table;
            var bcheck = false;
            var valid = true;

            table = document.getElementById("dtgBillingNumber");

            var Billing = '';
            //alert('1');
            var x = 1;
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer" || navigator.appName == "Netscape") {
                        if (x == 1) {
                            Billing = table.rows[i].cells[5].innerText;
                        } else {
                            if (Billing != table.rows[i].cells[5].innerText) {
                                valid = false;
                                i = table.rows.length;
                                alert("Pilih Tanggal Jatuh Tempo yang Sama");
                            } else {
                                Billing = table.rows[i].cells[5].innerText;
                            }
                        }
                    }
                    else{
                        if (x == 1) {
                            Billing = table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML;
                        } else {
                            if (Billing != table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML) {
                                valid = false;
                                i = table.rows.length;
                                alert("Pilih Tanggal Jatuh Tempo yang Sama");
                            } else {
                                Billing = table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML;
                            }
                        }
                    }
                    x++;
                }
            }

            if (valid) {
                Billing = '';
                for (i = 1; i < table.rows.length; i++) {
                    CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                    if (CheckBox != null && CheckBox.checked) {
                        if (navigator.appName == "Microsoft Internet Explorer") {
                            Billing = Billing + table.rows[i].cells[3].innerText + ';';
                            window.returnValue = Billing;
                            bcheck = true;
                        }
                        else if (navigator.appName == "Netscape") {
                            Billing = Billing + table.rows[i].cells[3].innerText + ';';
                            bcheck = true;
                        }
                        else {
                            Billing = Billing + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';';
                            bcheck = true;
                        }
                    }
                }
            }

            if (bcheck) {
                try {
                  
                    if (navigator.appName != "Microsoft Internet Explorer") {
                        window.opener.BillingSelection(Billing);
                    }
                    window.close();
                } catch (e ) {
                    alert(e.message);
                }
            }
            else {
                if (valid == false) {
                    alert("Silahkan Pilih Data terlebih dahulu");
                }
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

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table width="100%">
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px">Payment - Pencarian Billing Number</td>
            </tr>
            <tr>
                <td colspan="2" class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="titleField" valign="top">Creadit Account</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblCreditAccount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" valign="top">Kode Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerCode" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
                            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                <asp:Label runat="server" ID="lblGroup" Style="display: none"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" valign="top">Billing Number</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="TxtBillingNumber" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>

                <td>
                    <table width="100%">
                        <tr>
                            <td class="titleField" valign="top">Tanggal Billing</td>
                            <td valign="top">:</td>
                            <td>
                                <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chxTanggalBilling" /></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTanggalBillingFrom" runat="server"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTanggalBillingUntil" runat="server"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" valign="top">Tanggal Jatuh Tempo</td>
                            <td valign="top">:</td>
                            <td>
                                <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                                    <tr>

                                        <td>
                                            <asp:CheckBox runat="server" ID="chxTanggalJatuhTempo" Checked="true" Visible="false" /></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTanggalJatuhTempoFrom" runat="server"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTanggalJatuhTempoUntil" runat="server"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="2" align="center"></td>
            </tr>

            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="div1" style="overflow: auto; height: 310px">
                        <asp:DataGrid ID="dtgBillingNumber" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
                            AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="15">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn Visible="false">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="BillingNumber" HeaderText="Billing Number">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillingNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillingNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="BillingDate" SortExpression="BillingDate" HeaderText="Tanggal Billing"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTanggalJatuhTempo" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Amount Billing + Tax">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountBillingTax" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Amount C2">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountC2" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Total">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountTotal" runat="server">
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
                <td colspan="3" align="center">
                    <input id="btnChoose" onclick="GetSelectedBillNo()" type="button" value="Pilih"
                        name="btnChoose" runat="server" />
                    <input id="btnCancel" onclick="window.close();" type="button" value="Tutup" name="btnCancel" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
