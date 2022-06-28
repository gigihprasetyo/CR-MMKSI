<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmEquipmentList.aspx.vb" Inherits="frmEquipmentList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmEquipmentList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td colspan="4">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">EQUIPMENT REPAIR - Daftar Equipment</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD WIDTH="24%" class="titleField"><asp:label id="Label1" runat="server"> Kode Equipment</asp:label></TD>
					<TD WIDTH="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
					<TD WIDTH="20%"><asp:textbox id="txtEquipmentNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtEquipmentNumber','<>?*%$;')" runat="server" Width="140px" MaxLength="18"></asp:textbox></TD>
					<TD WIDTH="55%"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label3" runat="server">Nama Equipment</asp:label></TD>
					<TD><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtDescription" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"
							runat="server" Width="140px" MaxLength="40"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField">
						<asp:label id="Label7" runat="server">Spesifikasi</asp:label></TD>
					<TD>
						<asp:label id="Label8" runat="server">:</asp:label></TD>
					<TD>
						<asp:textbox id="txtSpesifikasi" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtSpesifikasi','<>?*%$;')"
							runat="server" MaxLength="40" Width="140px"></asp:textbox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label5" runat="server">Status</asp:label></TD>
					<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlStatus" runat="server">
							<asp:ListItem Value="0" Selected="True">Aktif</asp:ListItem>
							<asp:ListItem Value="1">Tidak Aktif</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD><asp:button id="btnSearch" runat="server" Text="Cari" Width="60px"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="4"><div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgEquipmentList" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								BackColor="#CDCDCD" PageSize="50" AllowCustomPaging="True" AutoGenerateColumns="False" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BackColor="#CDCDCD"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="EquipmentNumber" SortExpression="EquipmentNumber" HeaderText="Kode Equipment">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Nama Equipment">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="Price" HeaderText="Harga (Rp)" DataFormatString="{0:#,###}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LastUpdateTime" SortExpression="LastUpdateTime" HeaderText="Perubahan Terakhir"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Specification" HeaderText="Spek">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnSpek" CommandName="Active" text="Aktif" Runat="server" CausesValidation="False"
												Enabled="false">
												<img border="0" src="../images/aktif.gif" alt="Ada" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PhotoFileName" HeaderText="Photo">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnPhoto" CommandName="Active" text="Aktif" Runat="server" CausesValidation="False"
												Enabled="false">
												<img border="0" src="../images/aktif.gif" alt="Ada" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Header BOM">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnHeader" CommandName="Active" text="Aktif" Runat="server" CausesValidation="False"
												Enabled="false">
												<img border="0" src="../images/aktif.gif" alt="Ada" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="TotalQty" HeaderText="Quantity">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkButton1" runat="server" CommandName="edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="LinkButton2" runat="server" CommandName="detail">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<asp:button id="btnDownload" runat="server" Text="Download" Width="76px" Enabled="False"></asp:button>
						<br>
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
