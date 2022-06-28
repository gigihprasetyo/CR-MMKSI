<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClaimStatusChanges.aspx.vb" Inherits="PopUpClaimStatusChanges" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpClaimStatusChanges</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="7">CLAIM - Perubahan Status</td>
				</tr>
				<tr>
					<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table border="0" cellpadding="4" cellspacing="1">
				<TR>
					<TD class="titleField" width="20%">Nomor Claim</TD>
					<TD width="1%">:</TD>
					<TD><asp:literal id="ltrClaimNo" runat="server"></asp:literal></TD>
				</TR>
				<tr>
					<TD class="titleField" width="20%">Dealer</TD>
					<TD width="1%">:</TD>
					<TD><asp:literal id="ltrDealerCode" runat="server"></asp:literal></TD>
				</tr>
			</table>
			<table border="0" cellpadding="4" cellspacing="1" width="100%">
				<tr>
					<td><asp:datagrid id="dtgEntryClaimEdit" runat="server" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
							CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
							ShowFooter="True" PageSize="50">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblNo"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Lama">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblOldStatus" Runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Progress Lama">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblOldProgress" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status Baru">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNewStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Progress Baru">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNewProgress" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Keterangan">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtKeterangan" runat="server" Width="140px" Text='<%# DataBinder.Eval(Container, "DataItem.Keterangan") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Diproses Tanggal">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblProcessDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Diproses Oleh">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblProcessBy" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedBy") %>'/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
									CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
									EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
									<HeaderStyle CssClass="titleTableParts" Width="5%"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
								</asp:EditCommandColumn>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<HeaderStyle CssClass="titleTableParts" Width="3%"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="btnClose" runat="server" Text="Tutup"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
