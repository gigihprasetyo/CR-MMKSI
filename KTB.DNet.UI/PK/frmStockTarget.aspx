<%@ Register Assembly="KTB.DNet.WebCC" Namespace="KTB.DNet.WebCC" TagPrefix="cc1" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmStockTarget.aspx.vb" Inherits=".frmStockTarget" SmartNavigation="False" %>

<%@ Import Namespace="KTB.DNet.Domain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Stock Ratio</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
	<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = selectedDealer;
        }
    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td colspan="7">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="titlePage" colspan="6">PESANAN KENDARAAN - Stock Ratio</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="6" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td colspan="6" height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%" height="22">
                    <asp:Label ID="lblPilihLokasiFile" runat="server">Pilih Lokasi File</asp:Label></td>
                <td width="1%">
                    <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                <td colspan="5" width="79%">
                    <input id="DataFile" style="width: 340px" type="file" name="File1" runat="server" onkeypress="return false;">&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button>&nbsp;</td>
            </tr>
            <tr>
                <td width="20%" class="titleField">Dealer</td>
                <td width="1%">
                    <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                <td colspan="2">
                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label><asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
                        runat="server" Width="144px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label><asp:Button ID="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:Button></td>

            </tr>
            <tr>
                <td class="titleField">Kategori</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                
            </tr>
            <tr>
                <td class="titleField">Model</td>
                <td>
                    <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                
            </tr>
            <tr valign="top">
                <td class="titleField">Target</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtTarget" runat="server" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox></td>
                
            </tr>
            <tr valign="top">
                <td class="titleField">Stock ratio</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtStockRatio" runat="server" onkeypress="return NumericOnlyWith(event,',');"></asp:TextBox></td>
                
            </tr>
            <tr valign="top">
                <td class="titleField">Mulai Berlaku</td>
                <td>:</td>
                <td>
                    <cc1:IntiCalendar ID="ccValidDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                    <asp:CheckBox ID="chkAllValid" AutoPostBack="True" runat="server" Text="Semua" OnCheckedChanged="chkAllValid_CheckedChanged"></asp:CheckBox></td>
            </tr>
            <tr valign="top" id="trBlockRow" runat="server">
                <td class="titleField">Block</td>
                <td>:</td>
                <td>
                    <asp:CheckBox ID="chkDealer" runat="server" Text="Dealer"></asp:CheckBox>
                    <br />

                    <asp:CheckBox ID="chkKTB" runat="server" Text="MMKSI"></asp:CheckBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Enabled="False"></asp:Button>
                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div id="div1" style="height: 300px; overflow: auto">
                        <asp:DataGrid ID="dtgStockTarget" runat="server" Width="100%" CellSpacing="1" OnItemDataBound="dtgStockTarget_ItemDataBound"
                            GridLines="Horizontal" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
                            AllowCustomPaging="True" AllowPaging="True" PageSize="25" 
                            AllowSorting="True" ShowFooter="True" OnItemCommand="dtgStockTarget_ItemCommand" >
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn Visible="False" HeaderText="id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# CType(Container.DataItem, StockTarget).Dealer.DealerCode%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VechileModel.Description" HeaderText="Model">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%# CType(Container.DataItem, StockTarget).VechileModel.Description %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Mulai Berlaku">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValidFrom" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, StockTarget).ValidFrom)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Target" HeaderText="Target (Unit)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTarget" runat="server" Text='<%# CType(Container.DataItem, StockTarget).Target %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TargetRatio" HeaderText="Stock Ratio">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTargetRatio" runat="server" Text='<%# CType(Container.DataItem, StockTarget).TargetRatio %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Block Dari Dealer"></asp:Label><br />
                                        <asp:CheckBox ID="chkIsDealerBlockAll" AutoPostBack="True" OnCheckedChanged="chkIsDealerBlockAll_CheckedChanged" runat="server"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsDealerBlock" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Block Dari MMKSI"></asp:Label><br />
                                        <asp:CheckBox ID="chkIsKTBBlockAll" AutoPostBack="True" OnCheckedChanged="chkIsKTBBlockAll_CheckedChanged" runat="server"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsKTBBlock" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="" CommandName="Rubah" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="" CommandName="Hapus" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        &nbsp;
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" ReadOnly="True" HeaderText="Pesan">
                                    <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#404040" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <p>
            <asp:Button ID="btnDownLoad" runat="server" Text="DownLoad" Enabled="False" Width="64px"></asp:Button>
            &nbsp;
			<asp:Button ID="btnBlock" runat="server" Text="Block" Enabled="True" Width="64px"></asp:Button>
        </p>
    </form>
    <script language="javascript">
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
