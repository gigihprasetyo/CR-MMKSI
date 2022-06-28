<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpEventNational.aspx.vb" Inherits=".PopUpEventNational" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmEventSelection</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script type="text/javascript" language="javascript">

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

        function GetSelectedEvent() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgNationalEventSelection");
            var Event = '';
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (getQueryVariable("x") == "Territory") {
                            Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        else {
                            //Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        window.returnValue = Event;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        if (getQueryVariable("x") == "Territory") {
                            Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        else {
                            //Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
                            Event = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
                        }
                        window.opener.dialogWin.returnFunc(Event);
                        bcheck = true;
                    }
                    else {
                        if (getQueryVariable("x") == "Territory") {
                            Event = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' + table.rows[i].cells[6].innerHTML;
                        }
                        else {
                            //Event = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].innerHTML, ' ', '');
                            Event = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' + table.rows[i].cells[6].innerHTML;
                        }
                        window.opener.dialogWin.returnFunc(Event);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Event");
            }
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
                            <td class="titlePage" colspan="7">DAFTAR EVENT - &nbsp;Pencarian Event Nasional &nbsp;</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" alt="" src="../images/dot.gif" border="0" /></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Nama Kota</td>
                            <td width="1%">:</td>
                            <td style="width: 225px" width="225">
                                <asp:DropDownList ID="ddlCityPopUp" Width="100px" runat="server" AutoPostBack="true" />
                            </td>
                            <td style="width: 17px" width="10%"></td>
                            <td class="titleField" width="10%">Periode Event</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td style="width: 215px; height: 10px" width="215">
                                <table id="Table20" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkTanggal" runat="server"></asp:CheckBox></td>
                                        <td style="width: 101px">
                                            <cc1:inticalendar id="icEventDateFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEventDateTo" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="20%">Nama Venue</td>
                            <td width="1%">:</td>
                            <td style="width: 225px" width="225">
                                <asp:DropDownList ID="ddlVenuePopUp" Width="100px" runat="server" />
                            </td>
                            <td style="width: 17px" width="10%"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px">&nbsp;</td>
                            <td valign="bottom" style="width: 225px; height: 13px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgNationalEventSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="RegNumber" SortExpression="RegNumber" HeaderText="Kode Event">
                                                <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="NationalEventType.ID" HeaderText="Nama Event">
                                                <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNationalEventID" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>;'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventTypeHeaderID" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventType.ID")%>;'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventTypeyName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventType.Name")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="NationalEventCity.ID" HeaderText="Kota">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNationalEventID2" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventCityID" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventCity.ID")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventCityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventCity.City.CityName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="NationalEventVenue.ID" HeaderText="Venue">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNationalEventID3" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventVenueID" Style="display: none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventVenue.ID")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNationalEventVenueName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NationalEventVenue.VenueName")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Periode Event Mulai" SortExpression="PeriodStart">
                                                <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodStart" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodStart"), "dd/MM/yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Periode Event Selesai" SortExpression="PeriodEnd">
                                                <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodEnd" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodEnd"), "dd/MM/yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedEvent()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
