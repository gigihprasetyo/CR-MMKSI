<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCUploadBB.aspx.vb" Inherits="FrmWSCUploadBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmWSCUpload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Upload WSC (Special)</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="24%" class="titleField">
									Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nama Dealer</TD>
								<TD width="1%"></TD>
								<TD width="75%">
									<asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Pilih File</TD>
								<TD>:</TD>
								<TD><INPUT id="infWSCData" style="WIDTH: 293px" type="file" size="29" name="File1" runat="server">
									<asp:button id="btnUpload" runat="server" Width="70px" Text="Upload"></asp:button></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD valign="top"><div id="div1" style="HEIGHT: 370px; OVERFLOW: auto"><asp:datagrid id="dtgWSC" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								BackColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ClaimType" HeaderText="Jenis WSC">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblClaimType" runat="server" Text='<%# CType(Container.DataItem, WSCHeaderBB).ClaimType %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ClaimNumber" HeaderText="Nomor WSC">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblClaimNumber" runat="server" Text='<%# CType(Container.DataItem, WSCHeaderBB).ClaimNumber %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblChassisNumber" runat="server" Text='<%# IIf(IsNothing(Databinder.Eval(Container,"DataItem.ChassisMasterBB")), String.Empty, Databinder.Eval(Container,"DataItem.ChassisMasterBB.ChassisNumber")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl Service">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblServiceDate" runat="server" Text='<%# CType(Container.DataItem, WSCHeaderBB).StringServiceDate %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Miliage" HeaderText="Jarak Tempuh">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblMileage" runat="server" Text='<%# String.Format("{0:#,###}", CType(Container.DataItem, WSCHeaderBB).Miliage) %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EvidenceInvoice" HeaderText="Kuitansi">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkIsKuitansi" runat="server" Checked = '<%# CType(Container.DataItem, WSCHeaderBB).EvidenceInvoice = "X" %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EvidencePhoto" HeaderText="Foto">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkIsPhoto" runat="server" Checked = '<%# CType(Container.DataItem, WSCHeaderBB).EvidencePhoto = "X" %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EvidenceDmgPart" HeaderText="Part">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkIsDgmPart" runat="server" Checked = '<%# CType(Container.DataItem, WSCHeaderBB).EvidenceDmgPart = "X" %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Error Message">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblErrorMessage runat="server" Text='<%# CType(Container.DataItem, WSCHeaderBB).ErrorMessage %>' ForeColor="#ff0000">Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button></TD>
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
