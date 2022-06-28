<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMDPPeriodStock.aspx.vb" Inherits="FrmMDPPeriodStock" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MDP-Bulanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript">

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function Spaning() {
            var dtgMain = document.getElementById("dtgMain");
            var i = 0;
            for (i = 1; i < dtgMain.rows.length; i++) {
                if ((i - 1) % 3 == 0) {
                    dtgMain.rows[i].cells[0].rowSpan = "3";
                    dtgMain.rows[i].cells[1].rowSpan = "3";
                    dtgMain.rows[i + 1].deleteCell(0);
                    dtgMain.rows[i + 1].deleteCell(0);
                    dtgMain.rows[i + 2].deleteCell(0);
                    dtgMain.rows[i + 2].deleteCell(0);
                }
            }
        }
    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <input style="visibility: hidden" onclick="Spaning();" type="button">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="height: 17px" class="titlePage">Rencana Alokasi Unit Bulanan</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1" alt=""></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1" alt=""></td>
            </tr>
            <tr>
                <td style="height: 188px">
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
                </td>
            </tr>
            <tr>
                <td style="height: 1px">
                    <div id="div1" style="width: 100%; height: 238px; overflow: auto">
                        <asp:DataGrid ID="dtgMain" runat="server" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True"
                            PageSize="25" AllowPaging="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True">
                            <SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
                            <EditItemStyle VerticalAlign="Top"></EditItemStyle>
                            <AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Tipe/Warna">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeWarna" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Model/Tipe/Warna">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModelTipeWarna" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Tahun Perakitan/Impor">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTahunPerakitan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductionYear")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Periode Mulai">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodStart" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Periode Akhir">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodEnd" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Stok Sebelumnya">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStok" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CarryOverStock")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Rencana Alokasi">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PlanStock")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Sisa Stok">
                                    <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSisaStok" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RemainingStock")%>'>
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
                <td height="40" align="left">
                    <asp:Button ID="btnDownload" runat="server" Width="60px" Text="Download" Enabled="False"></asp:Button></td>
            </tr>
            <tr>
                <td align="center"></td>
            </tr>
        </table>
    </form>
    <script language="javascript" type="text/javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
