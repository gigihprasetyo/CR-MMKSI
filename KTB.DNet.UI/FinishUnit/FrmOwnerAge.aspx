<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmOwnerAge.aspx.vb" Inherits="FrmOwnerAge" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FAKTUR KENDARAAN - Usia Pemilik</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
		
		function HtmlCharNoTagBlur(controlName)
		{	
			var key = controlName.value;
			var newValue = "";
			for (i=0;i<key.length;i++)	
			{
				if (key.charCodeAt(i) !=60)
				{
					newValue = newValue + key.charAt(i);
				}				
			}			
			controlName.value = newValue;
		}
		function HtmlCharNoTag(event)
		{	
			if(navigator.appName == "Microsoft Internet Explorer")	
				pressedKey = event.keyCode;
			else
				pressedKey = event.which
			
			if (pressedKey !=60)
			{
				return true;
			}
			else
			{	
				return false;
			}
		}
		
	</script>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">FAKTUR KENDARAAN - Usia Pemilik</TD>
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
								<TD class="titleField" width="24%">Kode</TD>
								<td width="1%">:</td>
								<TD width="75%"><asp:textbox onkeypress="return HtmlCharNoTag(event)" onblur="HtmlCharNoTagBlur(txtCodeOwnerAge)" id="txtCodeOwnerAge" runat="server" MaxLength="3"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" EnableClientScript="False" ControlToValidate="txtCodeOwnerAge"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<td>:</td>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDeskripsiOwnerAge)" id="txtDeskripsiOwnerAge" runat="server" MaxLength="30"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" EnableClientScript="False" ControlToValidate="txtDeskripsiOwnerAge"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<td>:</td>
								<TD><asp:checkbox id="chkStatusOwnerAge" runat="server" Text="Aktif" Checked="True"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD></TD>
								<td></td>
								<TD><asp:button id="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:button><asp:button id="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgOwnerAge" runat="server" Width="100%" CellSpacing="1" CellPadding="3" BackColor="#CDCDCD"
							BorderColor="#CDCDCD" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" GridLines="None"
							AllowSorting="True" AllowPaging="True" AllowCustomPaging="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode">
									<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="Status" HeaderText="StatusTemp">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
											CommandName="View">
											<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
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
