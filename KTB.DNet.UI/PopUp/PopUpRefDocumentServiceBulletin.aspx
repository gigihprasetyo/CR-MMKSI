<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpRefDocumentServiceBulletin.aspx.vb" Inherits="PopUpRefDocumentServiceBulletin" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function GetSelectedBulletin() {
            var Hidden1 = document.getElementById("Hidden1");
            var table;
            table = document.getElementById("dgBulletin");
            var find = false;
            for (i = 1; i < table.rows.length; i++) {
                var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioButton != null && radioButton.checked) {
                    var RecallRegNo = '';
                    var Desc = '';

                    if (navigator.appName == "Microsoft Internet Explorer") {
                        var RecallRegNo = replace(table.rows[i].cells[1].innerText,' ','');
                        var Desc = replace(table.rows[i].cells[2].innerText, ' ', '');
                        window.returnValue = RecallRegNo + ";" + Desc
                    }
                    else if (navigator.appName == "Netscape") {
                        var RecallRegNo = replace(table.rows[i].cells[1].innerText, ' ', '');
                        var Desc = replace(table.rows[i].cells[2].innerText, ' ', '');
                        window.opener.InfoDokumenSBSelection(RecallRegNo + ";" + Desc);
                    }
                    else {
                        var result = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML;
                        window.opener.InfoDokumenSBSelection(result);
                    }
                    //break;
                    find = true;
                    break;
                }
            }
            if (find)

                window.close();
            else
                alert("Silahkan pilih Field Fix Campaign No.");
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Referensi Service Bulletin</td>
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
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>Field Fix Campaign No</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtRecallRegNo" onblur="omitSomeCharacter('txtRecallRegNo','<>?*%$;')"
                                    runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Buletin Number</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:TextBox ID="txtBuletinNumber" runat="server" Width="152px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Deskripsi</td>
                            <td style="height: 22px" width="1%">:</td>
                            <td style="height: 22px" width="25%">
                                <asp:TextBox ID="txtBuletinDescription" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px">
                                <asp:Button ID="btnSearch" runat="server" Width="80px" Text=" Cari "></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; width:550px; height: 330px">
                                    <asp:DataGrid ID="dgBulletin" runat="server" CellPadding="3" BorderWidth="0px" CellSpacing="1"
                                        BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True"
                                        Width="100%">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <input type="radio" name="radio">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RecallRegNo" HeaderText="Field Fix Campaign No">
                                                <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblRecallRegNo" Text='<%# DataBinder.Eval(Container, "DataItem.RecallRegNo")%>'>
                                                    </asp:Label>
                                                    <%--<asp:Label runat="server" ID="lblDescription" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                                                    </asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="BuletinDescription" HeaderText=" Buletin Number">
                                                <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblBuletinDescription" Text='<%# DataBinder.Eval(Container, "DataItem.BuletinDescription")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                                <HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDescription" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
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
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedBulletin()" type="button"
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
