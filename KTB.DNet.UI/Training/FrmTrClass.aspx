<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClass.aspx.vb" Inherits="FrmTrClass" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Master Kelas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
		
		function ShowPPCourseSelection()
		{
			showPopUp('../General/../PopUp/PopUpCourse.aspx','',550,760,courseSelection);
		}
		
		function courseSelection(selectedCourse)
		{
			
			var txtKodekategori = document.getElementById("txtKodeKategori");
			txtKodekategori.value = selectedCourse;	
		}
		
		function ShowPPClassSelection()
		{
			showPopUp('../PopUp/PopUpClassSelection.aspx','',500,760,classSelection);
		}
		
		function classSelection(selectedClass)
		{
			var tempParam= selectedClass.split(';');
			var txtClassCode = document.getElementById("txtClassCode");
			txtClassCode.value = tempParam[0];
		}
		
		function validateCapacity()
		{
			
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" defaultbutton="btnCari" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px">TRAINING - Kelas</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left"><br>
						<asp:panel id="pnl1" Runat="server">
							<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0" DESIGNTIMEDRAGDROP="100">
								<TR>
									<TD class="titleField" width="14%">Kode Kategori</TD>
									<TD width="1%">:</TD>
									<TD width="39%">
										<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeKategori" runat="server" MaxLength="20"
											Width="120px"></asp:textbox>
										<asp:label id="lblPopUpCourse" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
									<TD class="titleField" width="20%">Periode Tahun</TD>
									<TD width="1%">:</TD>
									<TD width="29%">
										<asp:TextBox id="txtPeriod" Runat="server" Width="60px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="titleField" width="14%">Kode Kelas</TD>
									<TD width="1%">:</TD>
									<TD width="39%">
										<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClassCode" runat="server" MaxLength="20"
											Width="120px"></asp:textbox>
										<asp:label id="lblPopUpClass" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
									<TD class="titleField">Lokasi</TD>
									<TD>:</TD>
									<TD>
										<asp:TextBox id="txtLocation" runat="server" Width="136px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="6">
										<asp:Button id="btnCari" runat="server" Width="60" CausesValidation="False" Text=" Cari "></asp:Button>
										<asp:button id="btnBaru" runat="server" Width="60" CausesValidation="False" Text="Baru"></asp:button>
										<asp:button id="btnReport" runat="server" Width="60" CausesValidation="False" Text="Laporan"></asp:button></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="6">&nbsp;</TD>
								</TR>
								<TR>
									<TD class="titleField" width="14%">
										<asp:label id="lblUploadFile" Runat="server">Upload File</asp:label></TD>
									<TD style="HEIGHT: 23px">
										<asp:label id="lblSprtUpload" Runat="server">:</asp:label></TD>
									<TD style="HEIGHT: 23px" colSpan="4"><INPUT onkeypress="return false;" id="fileUpload" style="WIDTH: 300px; HEIGHT: 20px" type="file"
											size="46" name="fileUpload" runat="server">
										<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgTrClass" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="50"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
								BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn Visible="False" HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ClassCode" SortExpression="ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClassName" SortExpression="ClassName" HeaderText="Nama Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="TrCourse.CourseCode" HeaderText="Kategori">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblKategori runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCourse.CourseCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Location" SortExpression="Location" HeaderText="Lokasi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Trainer1" SortExpression="Trainer1" HeaderText="Pengajar 1">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Trainer2" SortExpression="Trainer2" HeaderText="Pengajar 2">
										<HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Trainer3" SortExpression="Trainer3" HeaderText="Pengajar 3">
										<HeaderStyle Width="0px" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Tanggal Mulai" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FinishDate" SortExpression="FinishDate" HeaderText="Tanggal Selesai"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Capacity" SortExpression="Capacity" HeaderText="Kapasitas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Sisa Kapasitas">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSelisih" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="Description" SortExpression="Description" HeaderText="Keterangan">
										<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Width="16px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Width="8px" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnAddAllocation" runat="server" Width="8px" Text="Alokasi" CausesValidation="False"
												Visible="False" CommandName="AddAlloc">
												<img src="../images/icon_mail.gif" border="0" alt="Kirim Email *Alokasi Tambahan*"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="dtgUpload" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="500"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn Visible="True" HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUClassCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Kelas">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUClassName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kategori">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUCategory" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Lokasi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblULocation" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pengajar 1">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUTrainer1" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Mulai">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUStartDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Selesai">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUEndDate" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kapasitas">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUCapacity" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sisa Kapasitas">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblURemain" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pesan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUMessage" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnUDetail" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnUEdit" runat="server" Width="16px" Text="Ubah" CausesValidation="False"
												CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnUDelete" runat="server" Width="8px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnUAllocation" runat="server" Width="8px" Text="Alokasi" CausesValidation="False"
												Visible="False" CommandName="AddAlloc">
												<img src="../images/icon_mail.gif" border="0" alt="Kirim Email *Alokasi Tambahan*"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnBatal" runat="server" Width="60px" CausesValidation="False" Text="Batal"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnDownload" runat="server" Text="Download" Visible="False"></asp:button></TD>
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
