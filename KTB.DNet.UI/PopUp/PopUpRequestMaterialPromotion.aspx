<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpRequestMaterialPromotion.aspx.vb" Inherits="PopUpRequestMaterialPromotion" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopUpRequestMaterialPromotion</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript">

        function GetSelectedRequest() {
            var Hidden1 = document.getElementById("Hidden1");
            var table;
            table = document.getElementById("dtgRequestNumber");
            var find = false;
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioButton != null && radioButton.checked) {
                    var reqNo = '';
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var reqNo = replace(table.rows[i].cells[1].innerText, ' ', '');
                        window.returnValue = reqNo;
                    }
                    else if (navigator.appName == "Netscape") {
                        var reqNo = replace(table.rows[i].cells[1].innerText, ' ', '');
                        opener.dialogWin.returnFunc(reqNo);
                    }
                    else {
                        reqNo = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
                        opener.dialogWin.returnFunc(reqNo);
                    }
                    find = true;
                    break;
                }
            }
            if (find)
                window.close();
            else
                alert("Silahkan pilih Permintaan");
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">UMUM&nbsp;- Salesman Area</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 17px" width="24%">Nomor Permintaan</td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td style="height: 17px" width="25%">
                                <asp:TextBox ID="txtRequestNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server" onblur="omitSomeCharacter('txtRequestNo','<>?*%$;')" MaxLength="20" size="22"></asp:TextBox></td>
                            <td class="titleField" style="height: 17px" width="20%"></td>
                            <td style="height: 17px" width="1%"></td>
                            <td style="height: 17px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 17px" width="24%">
                                <asp:CheckBox ID="chkTanggalPermintaan" runat="server"></asp:CheckBox>Tanggal 
									Permintaan</td>
                            <td style="height: 17px" width="1%">:</td>
                            <td style="height: 17px" width="25%">
                                <cc1:IntiCalendar ID="icTanggalPermintaan" runat="server"></cc1:IntiCalendar></td>
                            <td class="titleField" style="height: 17px" width="20%">
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button></td>
                            <td style="height: 17px" width="1%"></td>
                            <td style="height: 17px" width="29%"></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 320px">
                                    <asp:DataGrid ID="dtgRequestNumber" runat="server" Width="100%" AllowPaging="True" AllowCustomPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" CellPadding="3" BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro"
                                        PageSize="25">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <input type="radio" name="radio">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Permintaan">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblRequestNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RequestDate" HeaderText="Tanggal Permintaan">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblRequestDate" Text='<%Format(DataBinder.Eval(Container, "DataItem.RequestDate"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedRequest()" type="button"
                                    value="Pilih" name="btnChoose">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
        <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
    </form>
</body>
</html>
