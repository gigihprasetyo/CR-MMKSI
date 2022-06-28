<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCity.aspx.vb" Inherits="FrmCity" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCity</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MAINTENANCE - Kota</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD class="titleField" width="24%">Propinsi</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlProvince" runat="server"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlProvince"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Kode Kota</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCityCode" onblur="HtmlCharBlur(txtCityCode)"
										runat="server" Width="88px" MaxLength="10"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="* " ControlToValidate="txtCityCode"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Kota</TD>
								<TD>:</TD>
								<td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCityName" onblur="HtmlCharBlur(txtCityName)"
										runat="server" Width="329px" MaxLength="40"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCityName"></asp:requiredfieldvalidator></td>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>&nbsp;<asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
									<asp:button id="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button></td>
							</tr>
						</TABLE>
						&nbsp;
					</TD>
				</TR>
				<TR vAlign="top">
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgCity" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None"
								BorderWidth="0px" BackColor="#CDCDCD" GridLines="None" BorderColor="#CDCDCD" CellPadding="3" PageSize="50" AllowCustomPaging="True" AllowPaging="True"
								AllowSorting="True" CellSpacing="1" Font-Names="Microsoft Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
									BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Province.ProvinceName" HeaderText="Propinsi">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Province.ProvinceName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CityCode" SortExpression="CityCode" HeaderText="Kode Kota">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CityName" SortExpression="CityName" HeaderText="Nama Kota">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="linkButonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Activate">
												<img src="../images/in-aktif.gif" border="0" alt="Klik untuk Aktifkan data"></asp:LinkButton>
											<asp:LinkButton id="LinkButtonNonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
												<img src="../images/aktif.gif" border="0" alt="Klik untuk non-Aktifkan data"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD></TD>
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
