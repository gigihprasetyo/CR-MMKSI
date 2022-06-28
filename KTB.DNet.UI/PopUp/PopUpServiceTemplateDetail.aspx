<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpServiceTemplateDetail.aspx.vb" Inherits=".PopUpServiceTemplateDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmServiceTemplateDetail</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
	<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<base target="_self" />
	<script language="javascript">
				
			
	</script>

</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
        <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
		    <tr>
                <td class="titleTableParts3" colspan="3">Service Template - Free Service - Part</td>
            </tr>
		    <tr>
                <td align="left" valign="top" class="titleField" style="width:150px">Kode Dealer</td>
                <td align="left" valign="top" style="width:10px">:</td>
                <td align="left" valign="top">
                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="titleField">Tipe</td>
                <td align="left" valign="top">:</td>
                <td align="left" valign="top">
                    <asp:Label ID="lblTipe" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="titleField">Code</td>
                <td align="left" valign="top">:</td>
                <td align="left" valign="top">
                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="titleField">Tipe Kendaraan</td>
                <td align="left" valign="top">:</td>
                <td align="left" valign="top">
                    <asp:Label ID="lblTipeKendaraan" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
            <td colspan="3">
                <div>
                    <asp:DataGrid ID="dgServiceTemplateDet" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" AllowPaging="false" PageSize="100" 
                        OnItemDataBound="dgServiceTemplateDet_ItemDataBound">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nomor Part">
                                <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPartNo" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.PartNumber")%>' Text='<%# DataBinder.Eval(Container, "DataItem.PartNumber")%>'>
                                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Part">
                                <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPartName" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.PartName")%>' Text='<%# DataBinder.Eval(Container, "DataItem.PartName")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jumlah Part">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJmlPart" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.PartQuantity")%>' Text='<%# DataBinder.Eval(Container, "DataItem.PartQuantity")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Harga Part">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPartAmount" runat="server" ToolTip='<%# FormatCurrency(DataBinder.Eval(Container, "DataItem.PartAmount"), 2)%>' Text='<%# FormatCurrency(DataBinder.Eval(Container, "DataItem.PartAmount"), 2)%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </td>
            </tr>
			<tr>
				<td align="center"colspan="3">
                    <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel" />
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
