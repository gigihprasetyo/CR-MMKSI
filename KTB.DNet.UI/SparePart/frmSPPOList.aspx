<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSPPOList.aspx.vb" Inherits="frmSPPOList" smartNavigation="False" %>
<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmSPPOList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
			function newLocation(loc)
			{
			window.location=loc
			}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">PEMESANAN - Daftar&nbsp;Pesanan</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Jenis Pesanan</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlOrderType" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlProcessCode" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Tanggal Pesanan</TD>
								<TD>:</TD>
								<TD><table border="0" cellpadding="2" cellspacing="0">
										<tr>
											<td><cc1:inticalendar id="icPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</tr>
									</table>
								</TD>
							</TR>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
								<td></td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td width="1%"></td>
                                <td>
                                    <asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button>
                                </td>
								<td></td>
                            </tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgSPPO" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="50"
								AllowPaging="True" AllowSorting="True" AllowCustomPaging="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="Gainsboro"
								BorderWidth="0px">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor Pesanan">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPONumber" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).PONumber %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PODate" HeaderText="Tanggal Pesanan">
										<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPO).PODate) %>'>Label</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="SentPODate" HeaderText="Tanggal Kirim">
										<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSentPODate" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", CType(Container.DataItem, SparePartPO).SentPODate) %>'>Label</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>

									<asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Pesanan">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).OrderTypeDesc %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
									<asp:TemplateColumn SortExpression="TermOfPayment.ID" HeaderText="Cara Pembayaran">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<%--<asp:Label id="Label1" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).TermOfPayment.Description%>'>Label</asp:Label>--%>
											<asp:Label id="LabelTOP" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String))%>'>Label</asp:Label>
                                            
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProcessCode" HeaderText="Status">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).ProcessCodeDesc %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDetail" runat="server">
												<img style="cursor:hand" alt="Rincian" src="../images/detail.gif" border="0" height="17px"
													width="17px">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
