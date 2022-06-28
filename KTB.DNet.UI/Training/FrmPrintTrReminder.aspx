<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintTrReminder.aspx.vb" Inherits="FrmPrintTrReminder" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPrintTrReminder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="625" border="0">
				<TR>
					<TD><asp:image id="Image1" runat="server" ImageUrl="../images/header_email.gif"></asp:image></TD>
				</TR>
				<TR>
					<TD>Tanggal printout :
						<asp:label id="lblToday" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD bgColor="#cdcdcd" height="60">
						<P align="center">JADWAL TRAINING<br>
							Bulan
							<asp:label id="lblMonth" runat="server"></asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD>Kepada Yth.
						<br>
						<asp:label id="lblDealerName" runat="server"></asp:label><br>
						<asp:label id="lblCityName" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD><br>
						<P><asp:Label ID="lblInfoManager" runat="server"></asp:Label> </P>
					</TD>
				</TR>
				<TR>
					<TD><br>
						Dengan ini kami sampaikan perihal tersebut di atas, dengan penjelasan sebagai 
						berikut :</TD>
				</TR>
				<TR>
					<TD><br>
						<asp:datagrid id="dtgReminder" runat="server" Width="625px" Font-Names="MS Reference Sans Serif"
							ForeColor="GhostWhite" PageSize="75" AllowSorting="True" AllowCustomPaging="True" BorderColor="#666666"
							BorderStyle="None" BorderWidth="1px" BackColor="Gainsboro" CellPadding="2" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="8%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No Reg">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
                                        <asp:Label ID ="lblSalesmanCode" runat="server"></asp:Label>
										<asp:Label id="lblTraineeID" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Siswa">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="15%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblName" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode Kelas">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="15%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblClass" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tgl Pendaftaran">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="15%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="Label1" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.RegistrationDate", "{0:dd/MM/yyyy}") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Mulai">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStartDate" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Selesai">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblFinishDate" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lokasi">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="20%" CssClass="titleTablePrint2"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblLocation" runat="server">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD><br>
						<p>
							<asp:label id="lblInfo1" Runat="server">
						Demikian pemberitahuan kami. Harap informasi ini disampaikan kepada yang tsb di atas.<br>
							Hal-hal yang perlu disiapkan :<br>
							1. Harap perlihatkan surat ini ke instruktur sebagai Surat Pengantar bagi 
							siswa yang akan training.<br>
							2. Menggunakan seragam celana panjang berwarna gelap.<br>
							3. Pas photo ukuran 3x4, 1 pc
						</asp:label>
						</p>
						<P>
							<asp:label ID="lblInfo2" Runat="server">Pemberitahuan ini dapat dipergunakan sebagai Surat Keterangan bahwa yang 
						berhubungan tersebut di atas selama periode tertentu di wilayah DKI Jakarta dan 
						sekitarnya. Apabila ada masalah dengannya mohon dibantu seperlunya atau 
						hubungi No. (021) 486 1608, Ext.1150 - 1156</asp:label></P>
						<P><asp:label ID="lblInfo3" Runat="server">Hormat kami,<br>
						MMKSI Service Training Department.<br>
						PT Mitsubishi Motors Krama Yudha Sales Indonesia</asp:label></P>
					</TD>
				</TR>
				<TR>
					<TD align="center">&nbsp;
						<asp:button id="btnBack" runat="server" CssClass="hideButtonOnPrint" Text="Kembali"></asp:button>&nbsp;
						<INPUT class="hideButtonOnPrint" style="WIDTH: 43px; HEIGHT: 21px" type="button" value="Cetak"
							id="btnPrint" name="btnPrint" onclick="window.print()">
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
