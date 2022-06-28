<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmPhisingGuardImageList.aspx.vb" Inherits="frmPhisingGuardImagaList" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MAINTENANCE - Pengaturan Gambar Pilihan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MAINTENANCE -&nbsp;Pengaturan Gambar Pilihan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td width="60%">
									<table>
										<TR valign=top>
											<TD nowrap class="titleField" style="HEIGHT: 28px" width="24%">Upload Gambar Pilihan 2</TD>
											<TD style="HEIGHT: 28px" width="1%">:</TD>
											<td style="WIDTH: 263px; HEIGHT: 28px" width="263"><INPUT onkeypress="return false;" id="photoSrc" type="file" size="29" name="File1" runat="server"></td>
										</TR>
										<TR>
											<TD style="HEIGHT: 28px">
											</TD>
											<TD style="HEIGHT: 28px"></TD>
											<TD style="WIDTH: 263px; HEIGHT: 28px"><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button> <asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
										</TR>
										<TR>
											<TD></TD>
											<TD></TD>
											<td style="WIDTH: 263px"></td>
										</TR>
									</table>
								</td>
								<td width="40%">
									<table>
										<tr>
											<td>
												<DIV id="divPhoto" style="OVERFLOW: auto; WIDTH: 184px; HEIGHT: 96px" align="right">
													<asp:image id="photoView" runat="server"></asp:image></DIV>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 330px">
							<asp:datagrid id="dtgPhising" runat="server" Width="60%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal"
								AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="10" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" BackColor="#FDC0C0"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ImageCode" SortExpression="ImageCode" HeaderText="Kode Gambar">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Type" Visible=False HeaderText="Jenis Image">
										<HeaderStyle Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblType"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit"
												Visible="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
