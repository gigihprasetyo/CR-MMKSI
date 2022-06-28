<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrMasterScheduleUpload.aspx.vb" Inherits="FrmTrMasterScheduleUpload" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrMasterScheduleUpload</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Training&nbsp;-&nbsp;Upload Jadwal
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Tahun</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Nama Jadwal</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<td style="HEIGHT: 16px">
									<P><asp:textbox id="txtScheduleName" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
											Width="320px"></asp:textbox></P>
										<!--<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtScheduleName"
											ErrorMessage="*"></asp:RequiredFieldValidator>-->
								</td>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 24px">Deskripsi</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px"><asp:textbox id="txtDesc" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="250"
										Width="328px" TextMode="MultiLine" Height="64px"></asp:textbox>&nbsp;
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="*" ValidationExpression="^[\s\S]{0,250}$"
										ControlToValidate="txtDesc" Display="Dynamic"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Lokasi File</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="HEIGHT: 23px"><INPUT onkeypress="return false" id="File" type="file" size="35" name="File1" runat="server"></TD>
							</TR>
							<TR>
								<TD></TD>
								<td></td>
								<td><asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button>&nbsp;
									<asp:button id="btnBatal" runat="server" Text="Batal" width="60px"></asp:button>
									<asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgSchedule" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
								CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True"
								Width="100%" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ScheduleYear" SortExpression="ScheduleYear" HeaderText="Tahun">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Jadwal">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UploadDate" SortExpression="UploadDate" HeaderText="Tanggal Upload" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDownload" runat="server" CommandName="Download" CausesValidation="False">Download</asp:LinkButton>
											<asp:Label id="lblDownload" runat="server" Visible="False"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
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
