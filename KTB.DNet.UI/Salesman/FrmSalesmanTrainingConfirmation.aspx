<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTrainingConfirmation.aspx.vb" Inherits="FrmSalesmanTrainingConfirmation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanTrainingConfirmation</title>
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
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">PELATIHAN TENAGA PENJUAL - Daftar 
						Peserta Pelatihan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style=" HEIGHT: 17px" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:TextBox id="txtKodeDealer" runat="server" ReadOnly="True"></asp:TextBox></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style=" HEIGHT: 17px" width="20%">Kode Training</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:DropDownList id="ddlKodeTraining" runat="server"></asp:DropDownList>
									<asp:Button id="btnFind" runat="server" Width="56px" Text="Cari"></asp:Button></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style=" HEIGHT: 17px" width="20%">Nama Training</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblNamaTraining" runat="server" Width="136px"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style=" HEIGHT: 17px" width="20%">Periode Training</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblPeriodeTraining" runat="server" Width="300px"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="20%">Periode Pendaftaran</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblPeriodePendaftaran" runat="server" Width="300px"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style=" HEIGHT: 17px" width="20%">Jenis Training</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblJenisTraining" runat="server"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="20%">Tempat Training</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="HEIGHT: 17px">
									<asp:Label id="lblTrainingPlace" runat="server"></asp:Label></TD>
								<TD class="titleField" style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="5">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dgParticipant" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:CheckBox ID="chkKonfirmasi" Runat="server"></asp:CheckBox>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px">
						<asp:Button id="btnSimpan" runat="server" Width="72px" Text="Simpan"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
