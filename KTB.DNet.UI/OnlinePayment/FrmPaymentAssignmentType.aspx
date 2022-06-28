<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPaymentAssignmentType.aspx.vb" Inherits="FrmPaymentAssignmentType" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPaymentAssignmentType</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function GetDDLValue(source,args)
		{
			if (args.Value < 0 )
			{
				args.IsValid=false;
				}
			else
			{
			args.IsValid=true;}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">INFORMASI PEMBAYARAN - Assignment</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kode</TD>
								<TD class="titleField" width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKode" onblur="HtmlCharBlur(txtKode)"
										runat="server" MaxLength="5" Width="100px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKode"></asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<STRONG>&nbsp;</STRONG></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Deskripsi</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtdescription" onblur="HtmlCharBlur(txtdescription)"
										runat="server" MaxLength="20" Width="304px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="txtdescription"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Status</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD style="HEIGHT: 28px"><asp:dropdownlist id="ddlStatus" Width="104px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Proses Manual</TD>
								<TD class="titleField" width="1%">:</TD>
								<TD style="HEIGHT: 28px"><asp:checkbox id="cboProcess" runat="server" Text="YA"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Tipe</TD>
								<TD class="titleField" width="1%"></TD>
								<TD style="HEIGHT: 28px"><asp:dropdownlist id="ddlPaymentObligationType" Width="104px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgPaymentAssignmentType" runat="server" Width="100%" CellSpacing="1" PageSize="25"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" GridLines="None" BorderStyle="None"
								BorderColor="#E0E0E0" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Proses Manual" SortExpression="SourceDocument">
										<HeaderStyle CssClass="titleTableSales" Width="5%"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSourceDoc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SourceDocument") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe" SortExpression="PaymentObligationType.Code">
										<HeaderStyle CssClass="titleTableSales" Width="5%"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PaymentObligationType.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblStatusAktif">
												<img src="../images/aktif.gif" border="0" alt="Aktif"></asp:Label>
											<asp:Label Runat="server" ID="lblStatusInAktif">
												<img src="../images/in-aktif.gif" border="0" alt="Tidak Aktif"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="PIC">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnviewPIC" runat="server" Width="20px" Text="Detail PIC" CausesValidation="False"
												CommandName="PIC">
												<img src="../images/detail.gif" border="0" alt="Detail PIC"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete"
												Visible="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
