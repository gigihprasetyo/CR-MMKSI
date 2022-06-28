<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventProposalExcelDownload.aspx.vb" Inherits="FrmEventProposalExcelDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventProposalExcelDownload</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Content-Type" content="application/x-download">
		<meta http-equiv="Content-Disposition" content="attachment;filename=EventProposal.xls">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" width="20%">
						<asp:Label ID="lblTitle" Runat="server" Font-Bold="True" Font-Size="16">Daftar Proposal</asp:Label>
					</td>
					<td width="80%">
						<asp:Label ID="lblSubTitle" Runat="server" Font-Size="16"></asp:Label>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<TR vAlign="top">
					<TD class="titleField" rowSpan="2" colspan="2">
						<asp:datagrid id="dtgExcel" runat="server" Width="100%" PageSize="2" AllowSorting="True" CellPadding="3"
							BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle ForeColor="White" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EventName" HeaderText="Nama Kegiatan">
									<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActivityName" HeaderText="Jenis Kegiatan">
									<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DealerName" HeaderText="Nama Dealer">
									<HeaderStyle ForeColor="White" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActivitySchedule" HeaderText="Tgl Kegiatan" DataFormatString="{0:dd MMM yyyy}">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActivityPlace" HeaderText="Tempat">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataFormatString="{0:0}" DataField="TotalCost" HeaderText="Total Biaya">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GuestType" HeaderText="Tipe Tamu">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TamuName" HeaderText="Nama Tamu">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JabatanName" HeaderText="Jabatan">
									<HeaderStyle ForeColor="White" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<asp:datagrid id="dtgAgreement" runat="server" Width="100%" PageSize="2" AllowSorting="True" CellPadding="3"
							BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False">
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle ForeColor="White" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode Dealer">
									<HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Dealer">
									<HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ActivitySchedule" HeaderText="Tgl Kegiatan" DataFormatString="{0:dd MMM yyyy}">
									<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ActivityPlace" HeaderText="Tempat">
									<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="InvitationNumber" HeaderText="Jumlah Undangan">
									<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TotalCost" HeaderText="Biaya Diajukan" DataFormatString="{0:0}">
									<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ApproveCost" HeaderText="Biaya Disetujui" DataFormatString="{0:0}">
									<HeaderStyle ForeColor="White" Width="20%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
