<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMDPDailyStock.aspx.vb" Inherits=".FrmMDPDailyStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MDP-Harian</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css" />
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <style>
        /*html {
            background-color: white;
        }*/

        .tabs {
            position: relative;
            top: 1px;
            left: 0px;
            width: 511px;
        }

        .tab {
            border: solid 1px gray;
            color: black;
            background-color: white;
            /*padding: 5px 10px;*/
        }

        .selectedTab {
            background-color: gray;
            border-bottom: solid 1px white;
        }

        .tabContents {
            border: solid 1px white;
            padding: 10px;
            background-color: white;
        }

        .grid td, .grid th{
            text-align:center;
        }

        .grid td.hModel{
            text-align:left;
        }
    </style>
    <script type="text/javascript">

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function OpenRedirect(tgl) {
            var date = new Date();
            var gyear = date.getFullYear();
            var gmonth = date.getMonth() + 1;
            var gdate = date.getDate();
            var date1 = gyear + "/" + gmonth + "/" + gdate;
            var dateNow = new Date(date1);
            var dateParam = new Date(tgl);
            if (dateParam <= dateNow)
            {
                alert("Tanggal permintaan kirim harus lebih besar dari hari ini")
            }
            else {
                window.location = "../MDP/CreatePODraft.aspx?dateValue=" + tgl
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="height: 17px" class="titlePage">Rencana Alokasi Unit Harian</td>
            </tr>
            <tr>
                <td height="1">
                    <img border="0" src="../images/bg_hor.gif" height="1" alt="" /></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1" alt="" /></td>
            </tr>
            <tr>
                <td style="height: 188px">
                    <div>
                        <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                            <tr>
                                <td style="width: 138px; height: 17px" class="titleField" width="138">
                                    <asp:Label ID="lblDealer" runat="server">Kode Dealer</asp:Label></td>
                                <td style="height: 17px" width="1%">:</td>
                                <td style="width: 211px; height: 17px" width="211">
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>

                            </tr>
                            <tr>
                                <td style="width: 108px; height: 17px" class="titleField" width="108">
                                    <asp:Label ID="Label6" runat="server"> Kategori</asp:Label></td>
                                <td style="height: 17px" width="1%">:</td>
                                <td style="width: 120px; height: 17px" width="120">
                                    <asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 30px" class="titleField" valign="middle">Model</td>
                                <td style="height: 30px">:</td>
                                <td style="width: 120px; height: 30px" valign="middle">
                                    <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 17px" class="titleField">
                                    <asp:Label ID="lblDealerPO" runat="server"> Tipe</asp:Label></td>
                                <td style="height: 17px">:</td>
                                <td style="width: 120px; height: 17px">
                                    <asp:DropDownList ID="ddlTipe" runat="server" AutoPostBack="True" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td width="20%" class="titleField">
                                    <asp:Label ID="lblPeriod" runat="server">Periode</asp:Label></td>
                                <td width="1%">
                                    <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                <td width="29%">
                                    <asp:DropDownList ID="ddlPeriode" runat="server" Width="104px"></asp:DropDownList>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="20%" class="titleField">
                                    <asp:Label runat="server">Tahun Perakitan/Impor</asp:Label></td>
                                <td width="1%">
                                    <asp:Label runat="server">:</asp:Label></td>
                                <td width="29%">
                                    <asp:DropDownList ID="ddlTahunPerakitan" runat="server" Width="104px"></asp:DropDownList>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td style="width: 108px">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                                            <td>&nbsp;</td>

                                            <td>
                                                <asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal"></asp:Button></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 120px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" Font-Size="Large"
                            StaticSelectedStyle-CssClass="selectedTab" StaticMenuItemStyle-HorizontalPadding="50px"
                            StaticSelectedStyle-BackColor="Gray" CssClass="tabs" runat="server">
                            <Items>
                            </Items>
                        </asp:Menu>
                        <div class="tabContents">
                            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="grid" AutoGenerateColumns="False" PageSize="25" CellPadding="4" ShowFooter="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />

                                        <FooterStyle BackColor="#99CCCC" ForeColor="Black" Font-Bold="True" Height="15px"></FooterStyle>

                                        <HeaderStyle CssClass="titleTableSales" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="grid" AutoGenerateColumns="False" PageSize="25" CellPadding="4" ShowFooter="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />

                                        <FooterStyle BackColor="#99CCCC" ForeColor="Black" Font-Bold="True" Height="15px"></FooterStyle>

                                        <HeaderStyle CssClass="titleTableSales" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <asp:GridView ID="GridView3" runat="server" CssClass="grid" AutoGenerateColumns="False" PageSize="25" CellPadding="4" ShowFooter="True" BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />

                                        <FooterStyle BackColor="#99CCCC" ForeColor="Black" Font-Bold="True" Height="15px"></FooterStyle>

                                        <HeaderStyle CssClass="titleTableSales" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                    </asp:GridView>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
