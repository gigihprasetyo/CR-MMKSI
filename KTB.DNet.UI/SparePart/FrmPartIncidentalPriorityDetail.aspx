<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartIncidentalPriorityDetail.aspx.vb" Inherits="FrmPartIncidentalPriorityDetail"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmBannedWord</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">Permintaan Khusus&nbsp;- Daftar Prioritas</td>
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
					<TD class="titleField" width="10%">Prioritas</TD>
					<td width="2%">:</td>
					<TD><asp:label id="lblPrioritas" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">Deskripsi</TD>
					<td>:</td>
					<TD><asp:label id="lblDeskripsi" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD>
						<asp:Button id="btnKembali" runat="server" Text="Kembali"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dgPriorityDetail" runat="server" Width="50%" PageSize="100" AllowSorting="True"
								AutoGenerateColumns="False" ShowFooter="True" BackColor="#CDCDCD" CellPadding="3" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Tipe" SortExpression="TypeCode">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeTipe" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TypeCode") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditKode" MaxLength="4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TypeCode") %>'>
											</asp:TextBox>
										</EditItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="txtInputKode" MaxLength="4" runat="server">
											</asp:TextBox>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Awal Tahun Produksi" SortExpression="StartProdYear">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTahun" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartProdYear") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList id="ddlEditTahun" runat="server">
											</asp:DropDownList>
										</EditItemTemplate>
										<FooterTemplate>
											<asp:DropDownList id="ddlInputTahun" runat="server">
											</asp:DropDownList>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lnkSaveInput" runat="server" Text="Simpan" Width="16px" CausesValidation="False"
												CommandName="SaveInput">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSaveEdit" tabIndex="49" CommandName="SaveEdit" text="Simpan" Runat="server">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="CancelEdit" text="Batal" Runat="server">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
										
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
