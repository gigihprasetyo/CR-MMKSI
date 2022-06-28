<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDownloadTrAdditionalClass.aspx.vb" Inherits=".FrmDownloadTrAdditionalClass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Download Daftar Kelas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td>
						<asp:datagrid id="dtgDwnload" runat="server" Width="904px" AutoGenerateColumns="False" CellPadding="3"
							BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#FFFFFF"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ClassCode" HeaderText="Kode Kelas">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ClassName" HeaderText="Nama Kelas">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
                              <asp:TemplateColumn HeaderText="Tipe Kelas">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                                  <ItemTemplate>
										<asp:Label id="lblClassType" runat="server"></asp:Label>
									</ItemTemplate>
							</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kategori">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblKategori" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Location" HeaderText="Lokasi">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Trainer1" HeaderText="Pengajar 1">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="StartDate" HeaderText="Tanggal Mulai (dd/MM/yyyy)" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FinishDate" HeaderText="Tanggal Selesai (dd/MM/yyyy)" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
                                 <asp:TemplateColumn HeaderText="Status">
									<HeaderStyle Font-Size="11px" Font-Names="Sans-Serif,Arial" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="White" BackColor="#666666"></HeaderStyle>
									<ItemStyle Font-Size="8" HorizontalAlign="Left" Font-Names="Sans-Serif,Arial" VerticalAlign="Top"></ItemStyle>
                                  <ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
							</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
