<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleKindMaster.aspx.vb" Inherits="FrmVehicleKindMaster" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">UMUM - Model Kendaraan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="2" cellPadding="2">
							<TR>
								<TD class="titleField" width="24%">Jenis Kendaraan</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlCategory" runat="server" Width="250px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Model</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtModel)" id="txtModel"
										runat="server" MaxLength="50" Width="250px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="rfvKode" runat="server" ErrorMessage="*" ControlToValidate="txtModel" InitialValue=" "
										Width="16px" EnableClientScript="False"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDeskripsi)" id="txtDeskripsi"
										runat="server" MaxLength="100" Width="250px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="rfvDeskripsi" runat="server" ErrorMessage="*" ControlToValidate="txtDeskripsi"
										Width="11px" EnableClientScript="False"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td><asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px" style="display:none"></asp:button>&nbsp;
									<asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button>&nbsp;
									<asp:Button id="btnCari" runat="server" Text=" Cari " width="60px" CausesValidation="False"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgModel" runat="server" Width="100%" PageSize="25" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Model Kendaraan" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VehicleKindGroupID" HeaderText="Jenis Kendaraan">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Model">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' ID="Label2" NAME="Label2">
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' ID="Textbox1" NAME="Textbox1">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit" style="display:none">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
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
