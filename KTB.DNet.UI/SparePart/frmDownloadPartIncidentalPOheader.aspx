<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmDownloadPartIncidentalPOheader.aspx.vb" Inherits="frmDownloadPartIncidentalPOheader" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmDownloadPartIncidentalPOheader</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD colSpan="4">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">PERMINTAAN KHUSUS &nbsp;- Daftar PO Permintaan Khusus
								</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgPartIncidentalDetail" runat="server" Width="100%" BackColor="#CDCDCD" CellPadding="3"
								BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label1"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox1" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('Textbox1','<>?*%$')"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.RequestNumber" HeaderText="No Permintaan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label7"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox2"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Nomor PO">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="PartIncidentalHeader.IncidentalDate" HeaderText="Tgl Pesanan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label8"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" ID="Textbox3"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Tanggal Proses">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Quantity" SortExpression="Quantity" HeaderText="Qty Order">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Part">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Part">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
