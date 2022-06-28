<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarStatusSPAFGeneralDownload.aspx.vb" Inherits="FrmDaftarStatusSPAFGeneralDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDaftarStatusSPAFGeneralDownload</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						<asp:Label ID="lblTitle" Runat="server"></asp:Label>
					</td>
				</tr>
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
								<TD class="titleField" width="10" style="HEIGHT: 17px">
									Leasing
								</TD>
								<td width="10" align="left">
									<asp:Label ID="lblLeasing" Runat="server"></asp:Label></td>
								<td width="99%"></td>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									Periode Kirim :
									<asp:Label ID="lblPeriodeKirim" Runat="server">-</asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									Periode Persetujuan :
									<asp:Label ID="lblPeriodePersetujuan" Runat="server">-</asp:Label>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" rowspan="2" colspan="3">
									<asp:datagrid id="dtgSPAF" runat="server" 
									    Width="100%" PageSize="2" AllowSorting="True" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
				BackColor="White" CellPadding="3" AutoGenerateColumns="False" ShowFooter ="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<Columns>
											<asp:BoundColumn HeaderText="No">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="VehicleType" HeaderText="Vehicle Type">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="VehicleDescription" HeaderText="Vehicle Description">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="HargaAverage" HeaderText="Harga" DataFormatString="{0:#,##0.00}">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="UnitAvalis" HeaderText="Unit Avalis">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="AmountAvalist" HeaderText="Amount Avalist" DataFormatString="{0:#,##0.00}">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="NonAvalUnit" HeaderText="Non Aval Unit">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="NonAvalAmount" HeaderText="Non Aval Amount" DataFormatString="{0:#,##0.00}">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="PercentSPAFAverage" HeaderText="%SPAF" DataFormatString="{0:=0*1}">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="TotalUnitSPAF" HeaderText="Total Unit SPAF">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="TotalUnitAmount" HeaderText="Total Unit Amount" DataFormatString="{0:#,##0.00}">
												<HeaderStyle ForeColor="White"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
