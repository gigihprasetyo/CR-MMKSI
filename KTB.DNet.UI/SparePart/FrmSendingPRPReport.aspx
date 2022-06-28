<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSendingPRPReport.aspx.vb" Inherits="FrmSendingPRPReport" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSendingPRPReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" encType="multipart/form-data">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="3">PARTSHOP REWARD PROGRAM&nbsp;- Pengiriman Laporan 
						PRP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 121px" width="121">Kode Organisasi</TD>
					<TD style="WIDTH: 11px" width="11">:</TD>
					<TD><asp:label id="lblKodeOrganisasiValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 121px">Nama Organisasi</TD>
					<TD style="WIDTH: 11px">:</TD>
					<TD><asp:label id="lblNamaOrganisasiValue" runat="server"></asp:label></TD>
				</TR>
				<tr>
					<td height="5"></td>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 66px" vAlign="top">Penerima</TD>
					<TD style="HEIGHT: 66px" vAlign="top">:</TD>
					<TD style="HEIGHT: 66px" vAlign="top"><asp:listbox id="lbReceiver" runat="server" SelectionMode="Multiple" Width="328px"></asp:listbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 68px" vAlign="top">CC</TD>
					<TD style="HEIGHT: 68px" vAlign="top">:</TD>
					<TD style="HEIGHT: 68px" vAlign="top"><asp:listbox id="lbCC" runat="server" SelectionMode="Multiple" Width="328px"></asp:listbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 22px" width="20%">Nama File</TD>
					<TD style="HEIGHT: 22px" width="1%">:</TD>
					<td style="HEIGHT: 22px" vAlign="top"><INPUT id="inpSendedFile" onkeydown="return false;" style="WIDTH: 496px; HEIGHT: 20px"
							type="file" size="63" name="inpSendedFile" runat="server">
						<asp:Button id="btnUploadFile" runat="server" Text="Upload"></asp:Button><BR>
						<asp:Label id="lblFileName" runat="server"></asp:Label></td>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 22px">PIC</TD>
					<TD style="HEIGHT: 22px">:</TD>
					<TD vAlign="top" style="HEIGHT: 22px">
						<asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPIC)" id="txtPIC"
							runat="server" MaxLength="30" Width="208px"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="txtPIC"></asp:RequiredFieldValidator></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 22px"></TD>
					<TD style="HEIGHT: 22px"></TD>
					<TD style="HEIGHT: 22px"><INPUT id="btnUpload" type="button" value="Kirim" name="btnUpload" runat="server" style="WIDTH: 60px; HEIGHT: 21px">
						<asp:Button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<asp:datagrid id="dtgReportPRP" runat="server" AllowSorting="True" AllowPaging="True" PageSize="15"
							AutoGenerateColumns="False" Width="100%" AllowCustomPaging="True">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle HorizontalAlign="Center" Width="12%" CssClass="titleTableParts"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Kirim"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle HorizontalAlign="Center" Width="12%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Filename" SortExpression="Filename" HeaderText="Nama File">
									<HeaderStyle HorizontalAlign="Center" Width="37%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle HorizontalAlign="Center" Width="40%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle HorizontalAlign="Center" Width="11%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
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
