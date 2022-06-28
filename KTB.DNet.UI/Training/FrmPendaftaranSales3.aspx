<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPendaftaranSales3.aspx.vb" Inherits="FrmPendaftaranSales3" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrClassRegistration3</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function RedirectPage()
			{
				var txt = document.getElementById("txtIsM2");
				var urlDefault = "../Training/FrmPendaftaranSales.aspx";
				var urlInhouse="../Training/FrmTrInhouse.aspx";
				
				if(txt.value=="1")
				{
					if(confirm("Teruskan ke buat laporan In House Training M2?"))
					{
						window.location = urlInhouse;
					}
					else
					{
						window.location=urlDefault;
					}						
				}
				else if(txt.value=="0")
				{
					window.location=urlDefault;
				}
			}		
			function RedirectAfterSave()
			{
			    var urlDefault = "../Training/FrmPendaftaranSales.aspx";
				alert("Simpan Berhasil");
				location.replace(urlDefault);
			}

			function RedirectAfterSwitch() {
			    var urlDefault = "../Training/FrmDaftarPendaftaranSales.aspx";
			    alert("Simpan Berhasil");
			    location.replace(urlDefault);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="RedirectPage();" >
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="740" border="0">
				<tr>
					<td class="titlePage">TRAINING SALES - Konfirmasi Pendaftaran</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 80px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="740" border="0">
							<colgroup>
								<col width="14%">
								<col width="1%">
								<col width="25%">
								<col width="14%">
								<col width="1%">
								<col width="45%">
							</colgroup>
							<TR>
								<TD class="titleField">Kode Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassCode" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtISM2" style="VISIBILITY: hidden" Width="5px" Runat="server" Height="8px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Mulai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblStartDate" runat="server"></asp:label></TD>
								<TD class="titleField">Selesai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblFinishDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Lokasi</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblLocation" runat="server"></asp:label></TD>
								<TD class="titleField">Alokasi</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblAllocatedTot" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="right"></TD>
				</TR>
                <TR>
					<TD class="titleField"><asp:label id="lblErrorMessage" runat="server" ForeColor="Red"></asp:label><BR>
                        <asp:label id="lblRegSuccessNote" runat="server" Visible="False">Nama siswa di bawah ini akan diikutsertakan pada kelas diatas : </asp:label></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgClassRegSuccess" runat="server" Width="740px" CellSpacing="1" GridLines="Vertical"
							CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
							AutoGenerateColumns="False" Font-Size="Small">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.TrTraineeSalesmanHeader.SalesmanCode" HeaderText="No. Reg Siswa">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblSalesmanCode runat="server">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
				<TR>
					<TD class="titleField">
						<asp:label id="lblClassRegCancel" runat="server">Nama siswa di bawah ini dibatalkan keikutsertaannya pada kelas diatas : </asp:label></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgClassRegCancel" runat="server" Width="740px" CellSpacing="1" GridLines="Vertical"
							CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
							AutoGenerateColumns="False" Font-Size="Small">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.TrTraineeSalesmanHeader.SalesmanCode" HeaderText="No. Reg Siswa">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSalesmanCode" runat="server" >
										</asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label16" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <ItemStyle BackColor="OrangeRed" />
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				
				<TR>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblRegFailNote" runat="server">Maaf, nama siswa di bawah ini tidak dapat didaftarkan pada kelas diatas, dikarenakan telah terdaftar pada kelas lain yang jadwalnya bersamaan :</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgClassRegFail" runat="server" Width="740px" CellSpacing="1" GridLines="Vertical"
							CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
							AutoGenerateColumns="False" Font-Size="Small">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.TrTraineeSalesmanHeader.SalesmanCode" HeaderText="No. Reg Siswa">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblSalesmanCode" runat="server">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kelas Bentrok">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="titleField">&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center"><input id="btnBack" style="FONT-WEIGHT: bold" type="button" value="Kembali" name="btnBack"
							runat="server">
						<asp:button id="btnSubmit" runat="server" Width="80px" Font-Bold="True" Text="OK"></asp:button><asp:button id="btnUbahAction" runat="server" Visible="False" Text="Ubah" CausesValidation="False"></asp:button></TD>
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
