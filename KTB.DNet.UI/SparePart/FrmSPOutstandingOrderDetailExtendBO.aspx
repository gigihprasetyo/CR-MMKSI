<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPOutstandingOrderDetailExtendBO.aspx.vb" Inherits="FrmSPOutstandingOrderDetailExtendBO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp Extend Back Order</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
        <script language="javascript">
            function getElement(tipeElement, IdElement) {
                var selectbox;
                var inputs = document.getElementsByTagName(tipeElement);

                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].id.indexOf(IdElement) > -1) {
                        selectbox = inputs[i]
                        break;
                    }
                }
                return selectbox;
            }

            function refreshingParent() {
                alert('Proses Perpanjangan Back Order Sukses');
                window.close();
                if (navigator.appName != "Microsoft Internet Explorer") {
                    opener.dialogWin.returnFunc(0);
                }
                else {
                    window.returnValue = 0;
                }
            }

            function CheckAll(aspCheckBoxID) {
                var selectbox = getElement('input', 'chkAllItems')
                var inputs = document.getElementsByTagName("input");
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].id.indexOf(aspCheckBoxID) > -1) {
                        if (inputs[i].type == 'checkbox') {
                            if (selectbox.checked == true) {
                                inputs[i].checked = "checked"
                            }
                            else {
                                if (inputs[i].disabled == false)
                                {
                                    inputs[i].checked = ""
                                }
                            }
                        }
                    }
                }
            }

        </script>
	</HEAD>

	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="4" cellPadding="0" width="100%" border="0">
				<tr>
					<td height="20">
						<TABLE id=Table5 cellSpacing=0 cellPadding=0 width="100%" border=0>
						<TR>
							<TD class=titlePage>Perpanjang Masa Berlaku Back Order </TD></TR>
						<tr>
							<td background=../images/bg_hor.gif height=1><IMG height=1 src="../images/bg_hor.gif" border=0 ></TD></TR>
						<tr>
							<td height=10><IMG height=1 src="../images/dot.gif" border=0 ></TD></TR>
						</table>					
					</td>
				</tr>
				<TR>
					<TD align=center> <div id=div1 style="OVERFLOW: auto; HEIGHT: 320px">
                        <asp:datagrid id="dgExtendBO" runat="server" Width="100%" AutoGenerateColumns="False" BorderWidth="0px"
							CellSpacing="1" CellPadding="3" PageSize="50" BackColor="Gainsboro" BorderColor="Gainsboro"
							AllowPaging="True" AllowCustomPaging="True">
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('chkSelect')"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Nomor Pesanan" SortExpression="SparePartOutstandingOrder.SparePartPO.PONumber">
									<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPONo" runat="server" Text='<%# CType(Container.DataItem, SparePartOutstandingOrderDetail).SparePartOutstandingOrder.SparePartPO.PONumber%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="PartNumber" HeaderText="Nomor Barang">
									<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PartName" HeaderText="Nama Barang">
									<HeaderStyle Width="23%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OrderQty" HeaderText="Jumlah Pesanan" DataFormatString="{0:#,##0}">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AllocationQty" HeaderText="Jumlah Sudah Dialokasi" DataFormatString="{0:#,##0}">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OpenQty" HeaderText="Jumlah Belum Dialokasi" DataFormatString="{0:#,##0}">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" ></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Berlaku s/d" SortExpression="SparePartOutstandingOrder.ValidTo">
									<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblValidto" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartOutstandingOrderDetail).SparePartOutstandingOrder.ValidTo)%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div>
					</TD>
				</TR>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnProses" runat="server" style="WIDTH: 95px; HEIGHT: 24px" 
                            OnClientClick="return confirm('Item yang diperpanjang tidak dapat dibatalkan! lanjutkan proses?');"  
                            Text="Perpanjang BO"></asp:Button><span style="margin-right: 10px"></span>
						<INPUT style="WIDTH: 64px; HEIGHT: 24px" onclick="window.close()" type="button" value="Tutup">
                    </td>
                </tr>
			</TABLE>
		</form>
	</body>
</HTML>
