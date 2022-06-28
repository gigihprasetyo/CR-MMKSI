<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSalesmanPartTargetHist.aspx.vb" Inherits="PopUpSalesmanPartTargetHist" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>History Status Penalty Parkir</title>
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
					<TD class="titlePage" colSpan="7">History Target Salesman Part</TD>
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
						Kode Salesman
					</TD>
					<td width="1%">:</td>
					<td>
						<asp:Label ID="lblSalesmanCode" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<TD class="titleField">
						Nama Salesman
					</TD>
					<td width="1%">:</td>
					<td>
						<asp:Label ID="lblSalesmanName" Runat="server"></asp:Label>
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
							<asp:datagrid id="dgHistory" runat="server" Width="100%" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Target" SortExpression="Target">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTarget runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Target")) %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Realisasi" SortExpression="Realization">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRealisasi" runat="server" Text='<%# String.Format("{0:#,###}",DataBinder.Eval(Container, "DataItem.Realization")) %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Prosentase (%)" SortExpression="Persentage">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPersentase" runat="server" Text='<%# String.Format("{0:0.00}",DataBinder.Eval(Container, "DataItem.Persentage")) %>'></asp:Label>
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
