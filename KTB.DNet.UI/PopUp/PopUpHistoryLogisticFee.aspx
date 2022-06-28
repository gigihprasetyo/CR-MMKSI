<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpHistoryLogisticFee.aspx.vb" Inherits="PopUpHistoryLogisticFee" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>History Status Logistic Cost</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="7">History Status Logistic Cost</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="7"></TD>
				</TR>
				<tr>
					<TD class="titleField" width="22%">
						Kode Dealer
					</TD>
					<td width="1%">:</td>
					<td width="35%">
						<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
					</td>
					<td></td>
				</tr>
				<tr>
					<TD class="titleField">
						Tanggal Invoice
					</TD>
					<td width="1%">:</td>
					<td>
						<asp:Label ID="lblTglInvoice" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<TD class="titleField">
						No. Debit Memo
					</TD>
					<td width="1%">:</td>
					<td>
						<asp:Label ID="lblDebitMemo" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<TD class="titleField"></TD>
					<td width="1%"></td>
					<td></td>
				</tr>
				<TR>
					<TD colSpan="7">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 150px">
							<asp:datagrid id="dgHistoryLogisticFee" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status Lama" SortExpression="OldStatus">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblOldStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status Baru" SortExpression="NewStatus">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNewStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CreatedTime" HeaderText="Diproses tanggal" SortExpression="CreatedTime"
										ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedBy" HeaderText="Diproses Oleh" SortExpression="CreatedBy" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</DIV>
					</TD>
				</TR>
				<TR>
					<TD colspan="7" align="center">&nbsp;&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
