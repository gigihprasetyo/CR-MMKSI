<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCSendEmailBB.aspx.vb" Inherits="FrmWSCSendEmailBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Permintaan Kirim Bukti WSC Special</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<base target="_self">
		<script language="javascript">
			
		//function Back()
		//{
		//var hidden = document.getElementById("Hidden1")
		//var i = hidden.value * -1
		//window.history.go(i);
		//}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="3" width="100%" border="0">
				<tr>
					<td colSpan="3">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">WSC -&nbsp; Daftar Permintaan Bukti WSC (Special)</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
						<asp:Label id="lblError" runat="server" Font-Bold="True" Visible="False" Font-Size="X-Small"
							ForeColor="Red">Email Tujuan Atau User Login Tidak Terdaftar</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<TR>
					<TD width="15%" nowrap><asp:label id="Label1" runat="server" Font-Bold="True">Kepada Yth</asp:label></TD>
					<TD width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD width="84%"><asp:label id="lblKepadaYthValue" runat="server" Font-Bold="True"></asp:label>
						&nbsp;/&nbsp;[
						<asp:label id="lblEmailTo" runat="server" Font-Bold="True"></asp:label>&nbsp;]</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblDi" runat="server" Font-Bold="True">Di</asp:label></TD>
					<TD width="1%">:</TD>
					<TD><asp:label id="lblKodeDealer" runat="server" Font-Bold="True"></asp:label>&nbsp;/
						<asp:label id="lblSearchTerm" runat="server" Font-Bold="True"></asp:label></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="HEIGHT: 16px"></TD>
					<TD style="HEIGHT: 16px"><asp:label id="lblDealerNameValue" runat="server" Font-Bold="True"></asp:label></TD>
				</TR>
				<TR>
					<TD nowrap><asp:label id="lblClaimNumber" runat="server" Font-Bold="True">Nomor Claim</asp:label></TD>
					<TD style="HEIGHT: 16px">:</TD>
					<TD style="HEIGHT: 16px"><asp:label id="lblClaimNo" runat="server" Font-Bold="True" Width="104px"></asp:label></TD>
				</TR>
				<TR id="OpClient1" runat="server">
					<TD></TD>
					<TD style="HEIGHT: 16px"></TD>
					<TD style="HEIGHT: 16px" align="right"></TD>
				</TR>
				<TR vAlign="top">
					<TD vAlign="top" style="WIDTH: 110px"><asp:label id="lblDescription" runat="server" Font-Bold="True">Deskripsi</asp:label></TD>
					<TD>:</TD>
					<TD valign="top"><table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td valign="top"><asp:textbox id="txtDescription" runat="server" TextMode="MultiLine" rows="20" Columns="60" MaxLength="500"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Deskripsi Harus Diisi"
										ControlToValidate="txtDescription">*</asp:requiredfieldvalidator>
								</td>
								<td valign="top">
									<div id="div1" style="WIDTH: 100%; HEIGHT: 280px; OVERFLOW: auto"><asp:datagrid id="dtgSendEmail" runat="server" Width="100%" AutoGenerateColumns="False" align="right"
											BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" BackColor="Olive"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="Id" HeaderText="Id">
													<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id=lbnNo runat="server" CommandName="No" text="<%# container.itemindex+1 %>" CausesValidation="False">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Sender" HeaderText="Dikirim Oleh">
													<HeaderStyle Width="45%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="Description" HeaderText="Deskripsi"></asp:BoundColumn>
												<asp:BoundColumn DataField="CreatedTime" HeaderText="Tgl Kirim">
													<HeaderStyle Width="45%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 110px"></TD>
					<TD></TD>
					<TD><asp:validationsummary id="ValidationSummary1" runat="server" Width="160px" Height="32px"></asp:validationsummary></TD>
				</TR>
				<TR id="OpClient2" runat="server">
					<TD style="WIDTH: 110px"></TD>
					<TD></TD>
					<TD><asp:button id="btnSend" runat="server" Text="Kirim" Width="64px"></asp:button>&nbsp;
						<INPUT id="btnClose" style="WIDTH: 64px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Tutup"></TD>
				</TR>
				<TR id="opClient3" runat="server">
					<TD style="WIDTH: 110px"></TD>
					<TD></TD>
					<TD><asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
