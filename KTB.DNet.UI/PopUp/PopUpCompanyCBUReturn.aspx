<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCompanyCBUReturn.aspx.vb" Inherits=".PopUpCompanyCBUReturn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD html 4.0 Transitional//EN">
<html>
    <HEAD>
        <title>Pencarian Company Logistic</title>
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

                table = document.getElementById("dgInfoCompany");
                var selected = '';
                for (i = 1; i < table.rows.length; i++) {
                    var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                    if (radioBtn != null && radioBtn.checked) {
                        if (navigator.appName == "Microsoft Internet Explorer") {
                            if (selected == '') {
                                selected = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[1].innerText;
                            }
                            window.returnValue = selected;
                            bcheck = true;
                        }
                        else if (navigator.appName == "Netscape") {
                            if (selected == '') {
                                selected = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[1].innerText;
                            }
                            opener.dialogWin.returnFunc(selected);
                            bcheck = true;
                        }
                        else {
                            if (table.rows[i].cells[1].getElementsByTagName("span").length > 0)
                                selected = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
                            else
                                selected = table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[1].innerHTML;

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
                    <td class="titlePage" colSpan="7"><asp:label ID="lblTitle" runat="server" Text="Pencarian Company Logistic"></asp:label></td>
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
                                <td class="titleField">Nama Vendor</td>
                                <td>:</td>
                                <td><asp:textbox id="txtCompanyName" runat="server"></asp:textbox></td>
                                <td class="titleField">Short List</td>
                                <td>:</td>
                                <td><asp:textbox id="txtKode" runat="server"></asp:textbox></td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td><asp:button id="btnCari" runat="server" Width="60px" Text="Cari" OnClick="btnCari_Click"></asp:button></td>
                                <td colspan="3"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="div1" style="overflow: auto; max-height:300px; height: auto; margin-bottom:25px">
                <asp:DataGrid ID="dgInfoCompany" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowSorting="True" BackColor="Gainsboro" CellPadding="3" BorderColor="#738A9C" PageSize="10"
                    AllowCustomPaging="True" AllowPaging="True"
                    OnPageIndexChanged="dgInfoCompany_PageIndexChanged"
                    OnSortCommand="dgInfoCompany_SortCommand">
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
                        <asp:TemplateColumn HeaderText="Nama Vendor" SortExpression="Name">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Short List" SortExpression="Kode">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblKode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Alamat" SortExpression="Address" ItemStyle-Wrap="true">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kota" SortExpression="City.CityName">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nomor Telepon" SortExpression="NoTelfon">
                            <HeaderStyle CssClass="titletableService" ForeColor="White"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoTelp" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoTelfon")%>'></asp:Label>
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
