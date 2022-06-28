<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPODraftList.aspx.vb" Inherits=".FrmPODraftList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>DaftarPODraft</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
            var HFKodeDealer = document.getElementById("HFKodeDealer");
            HFKodeDealer.value = selectedDealer;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div runat="server" id="divPage">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">Daftar PO Draft</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img alt="" height="1" src="../images/bg_hor.gif" border="0">
                    </td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" alt="" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                            <tr>
                                <td class="titleField" width="22%">
                                    <asp:Label ID="Label1" runat="server">Kode Dealer</asp:Label></td>
                                <td width="1%">
                                    <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                <td width="35%">
                                    <asp:HiddenField ID="HFKodeDealer" runat="server" />
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        AutoPostBack="true" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>
                                </td>
                                <td style="width: 239px" class="titleField" width="239">Produk</td>
                                <td width="12" style="width: 12px">:</td>
                                <td width="20%">
                                    <%--<asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="130px" AutoPostBack="True"></asp:DropDownList>--%>
                                    <asp:Label ID="lblProductCategory" runat="server" Text="Label">MMC</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="22%">
                                    <asp:Label ID="Label3" runat="server">Status</asp:Label>
                                </td>
                                <td width="1%">
                                    <asp:Label ID="Label5" runat="server">:</asp:Label>
                                </td>
                                <td width="35%">
                                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 239px" class="titleField" width="239">
                                    <asp:Label ID="Label8" runat="server">Kategori</asp:Label></td>
                                <td width="12" style="width: 12px">
                                    <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                <td width="20%">
                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="22%">
                                    <asp:Label ID="Label9" runat="server">Periode MDP</asp:Label>
                                </td>
                                <td width="1%">
                                    <asp:Label ID="Label31" runat="server">:</asp:Label>
                                </td>
                                <td width="35%">
                                    <asp:DropDownList ID="ddlPeriodeMDP" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td style="width: 239px" class="titleField">
                                    <asp:Label ID="Label16" runat="server">Jenis Order</asp:Label>
                                </td>
                                <td style="width: 12px">
                                    <asp:Label ID="Label11" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderType" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="Label2" runat="server">Periode Tanggal Kirim</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPeriodeTanggalKirim" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 239px" class="titleField">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="True">Nomor PO Draft</asp:Label>
                                </td>
                                <td style="width: 12px">:</td>
                                <td>
                                    <asp:TextBox onblur="return alphaNumericPlusBlur(txtDealerPO)" ID="txtNoDraftPO"
                                        onkeypress="return alphaNumericPlusUniv(event)" runat="server" Width="140px" MaxLength="20">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><strong>Total Quantity</strong></td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblQuantity" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 239px" class="titleField">
                                    <asp:Label ID="Label18" runat="server">Cara Pembayaran</asp:Label>
                                </td>
                                <td style="width: 12px">
                                    <asp:Label ID="Label14" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 2px"></td>
                                <td style="height: 2px"></td>
                                <td style="height: 2px"></td>
                                <td style="width: 239px; height: 2px"><strong>Bebas PPh</strong></td>
                                <td style="width: 12px; height: 2px">:</td>
                                <td style="height: 2px">
                                    <asp:DropDownList ID="ddlFreePPh" runat="server" Width="140px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 2px"></td>
                                <td style="height: 2px"></td>
                                <td style="height: 2px"></td>
                                <td style="width: 239px; height: 2px" width="239"><strong>
                                    <asp:Label ID="lblFactoring" runat="server">Factoring</asp:Label></strong></STRONG></td>
                                <td style="height: 2px">
                                    <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label></td>
                                <td style="height: 2px">
                                    <asp:DropDownList ID="ddlFactoring" runat="server" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnFind" runat="server" Width="60px" Text="Cari"></asp:Button>
                                    <asp:Button ID="btnBatalCari" runat="server" Text="Batal" />
                                    <br>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="6">
                        <div style="height: 270px; overflow: auto" id="div1">
                            <asp:DataGrid ID="dtgPO" runat="server" Width="100%" BackColor="#CDCDCD" OnItemCommand="dtgPO_ItemCommand"
                                OnItemDataBound="dtgPO_ItemDataBound" AutoGenerateColumns="False" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3"
                                AllowSorting="True" AllowCustomPaging="True" PageSize="50" AllowPaging="True" OnPageIndexChanged="dtgPO_PageIndexChanged">
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle BackColor="White"></ItemStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn SortExpression="Status" HeaderText="Status">
                                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
                                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="DraftPONumber" SortExpression="DraftPONumber" HeaderText="Nomor Draft PO">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="CreatedTime" HeaderText="Tgl Create PO draft">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="ReqAllocationDateTime" HeaderText="Tgl Permintaan Kirim">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="POHeader.SubmitPODate" HeaderText="Tgl Submit PO">
                                        <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="ContractHeader.Category.CategoryCode" HeaderText="Kategori">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="POType" HeaderText="Jenis Order">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="TermOfPayment.Description" HeaderText="Cara Pembayaran">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Bebas PPh">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga Tebus VH (Rp)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga Tebus PP (Rp)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga Tebus IT (Rp)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Harga Tebus LC (Rp)">
                                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnOperator" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmitPO" runat="server" Width="60px" Text="Submit PO"></asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
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
