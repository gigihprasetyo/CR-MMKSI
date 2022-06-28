<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDownLoadEHT.aspx.vb" Inherits="FrmDownLoadEHT" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDownLoadEHT</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!--<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet"> -->
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtDealerSelection");
				
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				txtDealer.innerText = selectedCode;				
				}
				else
				{
				txtDealer.value = selectedCode;
				}
				txtDealer.focus();
			}
			
		function cetak()
		{
		window.print();
		
		}
				

			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="740"><STRONG><FONT color="#993333" size="3">TRAINING - Evaluasi&nbsp;Hasil 
								Training</FONT></STRONG></td>
				</tr>
				<tr>
					<TD height="10"></TD>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<asp:panel id="pnlDealer" Runat="server" Visible="False"></asp:panel><asp:panel id="pnlCriteria" Runat="server" Visible="true"></asp:panel>
							<TR>
								<TD style="WIDTH: 105px; HEIGHT: 17px" width="105"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="WIDTH: 142px; HEIGHT: 17px" width="142"><asp:label id="lblKodeKelas" runat="server">Kode Kelas</asp:label></TD>
								<TD style="WIDTH: 149px; HEIGHT: 17px" width="149"></TD>
								<TD style="WIDTH: 186px; HEIGHT: 17px" width="186"><asp:label id="lblClassCode" runat="server" Width="96px"></asp:label></TD>
								<TD style="WIDTH: 47px; HEIGHT: 17px" width="47"></TD>
								<TD style="WIDTH: 47px; HEIGHT: 17px" width="47"><asp:label id="lblNamaKelas" runat="server" Width="80px">Nama Kelas</asp:label></TD>
								<TD style="WIDTH: 126px; HEIGHT: 17px" width="126"></TD>
								<TD style="HEIGHT: 17px" width="60%">
									<P>&nbsp;
										<asp:label id="lblClassName" runat="server"></asp:label>&nbsp;</P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 105px"></TD>
								<TD></TD>
								<TD style="WIDTH: 142px"><asp:label id="lblMulai" runat="server" Width="66px">Mulai</asp:label></TD>
								<TD style="WIDTH: 149px"></TD>
								<TD style="WIDTH: 186px"><asp:label id="lblStart" runat="server" Width="144px"></asp:label></TD>
								<TD style="WIDTH: 47px"></TD>
								<TD style="WIDTH: 47px"><asp:label id="lblSelesai" runat="server" Width="83px">     Selesai</asp:label></TD>
								<TD style="WIDTH: 126px"></TD>
								<TD>&nbsp;
									<asp:label id="lblFinish" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><asp:datagrid id="dtgClassRegistration" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray"
							PageSize="25" AllowSorting="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
							BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No. Reg">
									<HeaderStyle Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblRegCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Siswa">
									<HeaderStyle Width="12%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblTraineeName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Dealer">
									<HeaderStyle Width="14%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' ID="lblDealerName">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kota">
									<HeaderStyle Width="10%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Initial">
									<HeaderStyle Width="4%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblInitial" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 1">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest1" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 2">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest2" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 3">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest3" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 4">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest4" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 5">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest5" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 6">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTest6" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Test 7">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LblTest7" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Test 8">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LblTest8" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Test 9">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LblTest9" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="Test 10">
									<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LblTest10" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Final">
									<HeaderStyle Width="5%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblFinal" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Rata-Rata">
									<HeaderStyle Width="5%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblAverage" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Rank">
									<HeaderStyle Width="5%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblRank" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status">
									<HeaderStyle Width="5%" ForeColor="#FFFFFF" BackColor="#666666"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<DIV></DIV>
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
