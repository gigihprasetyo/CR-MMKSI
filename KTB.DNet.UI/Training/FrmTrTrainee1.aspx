<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrTrainee1.aspx.vb" Inherits="FrmTrTrainee1" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Data Status Siswa</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',600,600,DealerSelection);
		}
	
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}
		}
		
		function ShowJobPosSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=5','',600,600,JobPosSelection);
		}
		function JobPosSelection(selectedJobPos)
		{
			var txtPosisi = document.getElementById("txtPosisi");
			selectedJobPos = selectedJobPos + ';';
			
			var arrValue = selectedJobPos.split(';');
			txtPosisi.value = arrValue[0];
		}
		
		function ShowJobPosSelectionMany()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpJobPositionMany.aspx?Menu=5','',600,600,JobPosSelectionMany);
		}
		function JobPosSelectionMany(selectedJobPos)
		{
			var txtPosisi = document.getElementById("txtPosisi");
			txtPosisi.value = selectedJobPos;
		}
		
		function ShowPPAreaSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpArea2.aspx','',600,600,AreaSelection);
		}
	
		function AreaSelection(selectedArea)
		{
			var txtAreaCodeSelection = document.getElementById("txtArea");
			txtAreaCodeSelection.value = selectedArea;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">TRAINING - Data Status Siswa</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%" height="15">Kode Organisasi</TD>
								<TD style="HEIGHT: 15px" width="1%">:</TD>
								<TD style="HEIGHT: 15px" noWrap width="75%"><asp:label id="lblOrganizationCode" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblSearchTerm1" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="15">Nama Organisasi</TD>
								<TD style="HEIGHT: 15px" width="1%">:</TD>
								<TD style="HEIGHT: 15px" noWrap width="75%"><asp:label id="lblOrganizationName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="15">Kota</TD>
								<TD style="HEIGHT: 15px" width="1%">:</TD>
								<TD style="HEIGHT: 15px" noWrap width="75%"><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="15"></TD>
								<TD style="HEIGHT: 15px" width="1%"></TD>
								<TD style="HEIGHT: 15px" noWrap width="75%"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="22">Kode Dealer</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" noWrap width="75%"><asp:textbox id="txtDealerCode" runat="server" Width="264px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" height="22">Status</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" noWrap width="75%"><asp:dropdownlist id="ddlStatus" Width="130px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Registrasi</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtNomorReg" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtNama" runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Posisi</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txtPosisi" runat="server" ></asp:textbox>
									<asp:label id="lblSearchJobPos" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Telah Lulus Kategori</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox id="txtClassCategory" runat="server"></asp:textbox>&nbsp;Gunakan 
									tanda ( ; ) sebagai pemisah</TD>
							</TR>
							<TR>
								<TD class="titleField">Belum Lulus Kategori</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox id="txtClassCategory2" runat="server"></asp:textbox>&nbsp;Gunakan 
									tanda ( ; ) sebagai pemisah</TD>
							</TR>
							<TR>
								<TD class="titleField">Region</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox id="txtArea" runat="server"></asp:textbox>
									<asp:label id="lblSearchRegion" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD><asp:button id="btnCari" runat="server" Width="72px" Text="Cari"></asp:button>
									<asp:Button id="Button1" runat="server" Text="Button" Visible="False"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dtgTrainee" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
								CellSpacing="1" ForeColor="GhostWhite" PageSize="50" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White" VerticalAlign="Top"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Reg">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Siswa">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BirthDate" SortExpression="BirthDate" HeaderText="Tanggal Lahir" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Gender" HeaderText="Gender">
										<HeaderStyle Width="6px" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGender" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer - Region">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" HeaderText="Mulai Bekerja"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" HeaderText="Posisi">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EducationLevel" SortExpression="EducationLevel" HeaderText="Pendidikan">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Telah Lulus">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="15%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblCourses" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="13%"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Belum Lulus MSTEP">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMSTEP" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Belum Lulus General Wajib">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGeneral" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Lulus Junior">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblJr" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Lulus Senior">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSr" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Lulus Specialist E/G & F/S">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSrE" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Lulus Specialist D/T & E/C">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSrD" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl Lulus Master">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMr" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" CausesValidation="False" Text="Lihat" CommandName="Detail">
												<img src="../images/edit.gif" border="0" alt="Lihat/Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnDaftar" runat="server" CausesValidation="False" Text="Ubah" CommandName="RegisterToClass">
												<img src="../images/add.gif" border="0" alt="Daftar"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="40">
						<asp:button id="btnBaru" runat="server" Width="48px" Text="Baru" CausesValidation="False"></asp:button>
						<asp:button id="btnPrint" runat="server" Width="101px" Text="Cetak/Download" CausesValidation="False"
							Enabled="False"></asp:button>
						<asp:Button ID="btnConfirmation" Runat="server" Width="80px" Text="Konfirmasi"></asp:Button>
					</TD>
				</TR>
				<TR>
					<TD></TD>
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
