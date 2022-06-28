<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSearchBabitIklan.aspx.vb" Inherits=".PopUpSearchBabitIklan" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pencarian Babit Iklan</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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

        function GetSelectedEventProposal() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgEventProposalSelection");
            var EventInfo = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        EventInfo = table.rows[i].cells[1].innerText;

                        window.returnValue = EventInfo;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        EventInfo = table.rows[i].cells[1].innerText;

                        opener.dialogWin.returnFunc(EventInfo);
                        bcheck = true;
                    }
                    else {
                        EventInfo = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;

                        opener.dialogWin.returnFunc(EventInfo);
                        bcheck = true;
                    }
                    break;
                }
            }

            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Babit Pameran");
            }
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 80px;
        }
        .auto-style2 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 80px;
        }
        .auto-style3 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 15px;
            width: 80px;
        }
        .auto-style4 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 100px;
        }
        .auto-style5 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 13px;
            width: 100px;
        }
        .auto-style6 {
            height: 15px;
            width: 100px;
        }
        .auto-style7 {
            width: 88px;
        }
        .auto-style8 {
            height: 10px;
            width: 88px;
        }
        .auto-style9 {
            height: 15px;
            width: 88px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">Pencarian Babit Iklan</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10" class="auto-style1">
                                <img height="1" alt="" src="../images/dot.gif" border="0" /></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">No. Reg Babit</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox ID="txtEventRegNumber" runat="server" Width="172px"></asp:TextBox></td>
                            <td></td>
                            <td class="titleField" >Nomor Surat</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox ID="txtEventName" runat="server" Width="192px" /></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Bulan Periode</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList>
                            </td>
                            <td style="width: 17px; height: 13px"></td>
                            <td></td>
                            <td style="height: 13px">&nbsp;</td>
                            <td valign="bottom" style="width: 225px; height: 13px">
                                <asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Tahun Periode</td>
                            <td style="height: 10px" width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList>
                            </td>
                            <td style="width: 17px; height: 13px"></td>
                            <td></td>
                            <td style="height: 13px">&nbsp;</td>
                            <td valign="bottom" style="width: 225px; height: 13px"></td>
                        </tr>
                        <tr>
                            <td class="auto-style3"></td>
                            <td style="height: 15px"></td>
                            <td class="auto-style9"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td class="auto-style6"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dtgEventProposalSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="#F7F7F7" BackColor="#4A3C8C" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="BabitRegNumber" HeaderText="Reg Number">
                                                <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BabitRegNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="BabitDealerNumber" SortExpression="BabitDealerNumber" HeaderText="No. Surat">
                                                <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Periode Awal" SortExpression="PeriodStart">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodStart" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodStart"), "dd/MM/yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Periode Akhir" SortExpression="PeriodEnd">
                                                <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriodEnd" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodEnd"), "dd/MM/yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedEventProposal()" type="button" value="Pilih" name="btnChoose" runat="server">
                                &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                    name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
