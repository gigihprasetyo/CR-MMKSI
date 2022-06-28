<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpBenefit.aspx.vb" Inherits="PopUpBenefit" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Benefit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

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

        function GetSelectedValue() {
            var table;
            var bcheck = false;
            table = document.getElementById('dgCompetitorType');
            var val = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        val = table.rows[i].cells[1].innerText;
                        window.returnValue = val;
                        bcheck = true;
                    }
                    else {
                        val = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        opener.dialogWin.returnFunc(val);
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Benefit");
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmSalesmanLevel" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">SALES CAMPAIGN - Daftar Benefit</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 390px">
                        <asp:DataGrid ID="dgCompetitorType" runat="server" Width="100%" AllowSorting="True" HorizontalAlign="Center"
                            AllowPaging="True" BackColor="#CDCDCD" GridLines="None" BorderColor="Gainsboro" AutoGenerateColumns="False"
                            CellPadding="3" CellSpacing="1" AllowCustomPaging="True" PageSize="10">
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                    <ItemTemplate>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Benefit Reg">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BenefitRegNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nomor Surat">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NomorSurat")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Formula">
                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Formula")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>


                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40" align="center">
                    <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedValue()" type="button"
                        value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                            name="btnCancel"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
