<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpChassisCBUReturn.aspx.vb" Inherits=".PopUpChassisCBUReturn" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD html 4.0 Transitional//EN">
<html>
    <HEAD>
        <title>Pencarian Chassis/DO</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
        <base target="_self">
        <script type="text/javascript">
		
            function GetSelected() {
                var table;
                var bcheck = false;

                table = document.getElementById("dgInfoChassis");
                var selected = '';
                for (i = 1; i < table.rows.length; i++) {
                    var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                    if (radioBtn != null && radioBtn.checked) {
                        if (navigator.appName == "Microsoft Internet Explorer") {
                            if (selected == '') {
                                selected = replace(table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText, ' ', '');
                            }
                            window.returnValue = selected;
                            bcheck = true;
                        }
                        else if (navigator.appName == "Netscape") {
                            if (selected == '') {
                                selected = replace(table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[5].innerText, ' ', '');
                            }
                            opener.dialogWin.returnFunc(selected);
                            bcheck = true;
                        }
                        else {
                            if (table.rows[i].cells[1].getElementsByTagName("span").length > 0)
                                selected = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[5].getElementsByTagName("span")[0].innerHTML;
                            else
                                selected = table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML;

                            opener.dialogWin.returnFunc(selected);
                            bcheck = true;
                        }
                    }
                }

                if (bcheck) {
                    window.close();
                }
                else {
                    alert("Silahkan pilih terlebih dahulu");
                }
            }
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <table id="table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" colSpan="7"><asp:label ID="lblTitle" runat="server" Text="Pencarian Chassis"></asp:label></td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1"><img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="height: 6px" height="6"><img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table id="table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField">Nomor DO</td>
                                <td>:</td>
                                <td><asp:textbox id="txtNoDO" runat="server"></asp:textbox></td>
                                <td class="titleField">Nomor PO</td>
                                <td>:</td>
                                <td><asp:textbox id="txtNoPO" runat="server"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td class="titleField">Nomor Rangka</td>
                                <td>:</td>
                                <td><asp:textbox id="txtNoRangka" runat="server"></asp:textbox></td>                                
                                <td class="titleField">Tanggal Cetak DO</td>
                                <td>:</td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CBTglCetak" runat="server" />
                                            </td>
                                            <td>
                                                <cc1:IntiCalendar ID="icTglCetakDOFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </td>
                                            <td>&nbsp;s/d&nbsp;</td>
                                            <td>
                                                <cc1:IntiCalendar ID="icTglCetakDOTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>                                
                                <td class="titleField">Tanggal Keluar</td>
                                <td>:</td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CBTglKeluar" runat="server" />
                                            </td>
                                            <td>
                                                <cc1:IntiCalendar ID="icTglKeluarFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </td>
                                            <td>&nbsp;s/d&nbsp;</td>
                                            <td>
                                                <cc1:IntiCalendar ID="icTglKeluarTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td></td>
                                <td><asp:button id="btnCari" runat="server" Width="60px" Text="Cari" OnClick="btnCari_Click"></asp:button></td>
                                <td colspan="2"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="div1" style="overflow: auto; max-height:300px; height: auto; margin-bottom:25px">
                <asp:DataGrid ID="dgInfoChassis" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowSorting="True" BackColor="Gainsboro" CellPadding="3" BorderColor="#738A9C" PageSize="10"
                    AllowCustomPaging="True" AllowPaging="True"
                    OnItemDataBound="dgInfoChassis_ItemDataBound"
                    OnPageIndexChanged="dgInfoChassis_PageIndexChanged"
                    OnSortCommand="dgInfoChassis_SortCommand">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                    <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="2%" CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <input type=radio name="radio">
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nomor Rangka" SortExpression="ChassisNumber">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nomor PO" SortExpression="PONumber">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoPO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Lokasi Carpool" SortExpression="Location.Location">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblLokasi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Location.Location")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nomor DO" SortExpression="DONumber">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoDO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DONumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </div>
            <div>
                <table border="0" cellspacing="1" cellpadding="2" width="100%">
                    <tr style="padding-bottom: 5px">
                        <td align="center" width="30%">
                            <input id="inputPilih" style="width: 60px" onclick="GetSelected()" type="button" value="Pilih" name="btnChoose" runat="server" disabled>&nbsp; 
                            <input id="inputTutup" style="width: 60px" onclick="window.close()" type="button" value=" Tutup " name="btnCancel">
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>
