<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassAllocation.aspx.vb" Inherits="FrmTrClassAllocation" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Master Alokasi Kelas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
		
		function ShowCategoryManySelection()
		{
			showPopUp('../PopUp/PopUpCourseMany.aspx','',500,760,CategorySelection);
		}
		
		function CategorySelection(selectedCategory)
		{			
			var txtKode = document.getElementById("txtKodeKategori");			
			txtKode.value = selectedCategory			
		}
		
		function ShowPPClassSelectionMany()
		{
			var txtKode = document.getElementById("txtKodeKategori");
			showPopUp('../PopUp/PopUpClassSelectionMany.aspx?CourseCode=' + txtKode.value,'',500,760,classSelectionMany);
		}		
		function classSelectionMany(selectedClass)
		{
			var txtKode = document.getElementById("txtKodeKelas");
			txtKode.value = selectedClass;			
		}
		
		function ShowPPClassSelection()
		{
			showPopUp('../PopUp/PopUpClassSelection.aspx','',500,760,classSelection);
		}		
		function classSelection(selectedClass)
		{
			var tempParam= selectedClass.split(';');
			var txtKode = document.getElementById("txtKodeKelas");
			var txtNama = document.getElementById("txtNamaKelas");
			var txtKap = document.getElementById("txtKapasitas");
			txtKode.value = tempParam[0];
			txtNama.value = tempParam[1];
			txtKap.value = tempParam[2];
		}

		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtKodeDealer = document.getElementById("txtKodeDealer");
			txtKodeDealer.value = selectedDealer;			
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TRAINING - Alokasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 57px" align="left"><asp:panel id="pnl1" Runat="server">
							<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<TD class="titleField" style="HEIGHT: 21px" width="24%">Kode Kategori</TD>
									<TD style="HEIGHT: 21px" width="1%">:</TD>
									<TD style="HEIGHT: 21px" width="75%">
										<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeKategori" runat="server" MaxLength="20"
											Width="100"></asp:textbox>&nbsp;
										<asp:label id="lblSearchKodeKategori" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 21px" width="24%">Kode Kelas</TD>
									<TD style="HEIGHT: 21px" width="1%">:</TD>
									<TD style="HEIGHT: 21px" width="75%">
										<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeKelas" runat="server" MaxLength="20"
											Width="100"></asp:textbox>&nbsp;
										<asp:label id="lblPopUpClass" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
										<asp:textbox id="txtNamaKelas" runat="server" Width="200px" Visible="False" ReadOnly="True"></asp:textbox>
										<asp:textbox id="txtKapasitas" runat="server" Width="100" Visible="False" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 23px">Kode Dealer</TD>
									<TD style="HEIGHT: 23px">:</TD>
									<TD style="HEIGHT: 23px">
										<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeDealer" runat="server" Width="300px"></asp:textbox>&nbsp;
										<asp:label id="lblPopUpDealer" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 23px">
										<asp:CheckBox id="chkAllocation" runat="server" Text="Alokasi > 0"></asp:CheckBox>&nbsp;
										<asp:CheckBox id="chkBatal" runat="server" Text="Batal"></asp:CheckBox>&nbsp;&nbsp;
									</TD>
									<TD style="HEIGHT: 23px"></TD>
									<TD style="HEIGHT: 23px"><STRONG>Periode</STRONG> :
										<asp:TextBox id="txtPeriod" Runat="server" Width="60px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 23px"></TD>
									<TD style="HEIGHT: 23px"></TD>
									<TD style="HEIGHT: 23px">
										<asp:button id="btnCari" runat="server" Width="80px" Text="Cari" CausesValidation="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="btnSetAllocation" runat="server" Width="150px" Text="Masukkan Jumlah Alokasi"
											CausesValidation="False"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="4">&nbsp;</TD>
								</TR>
								<TR>
									<TD class="titleField" style="HEIGHT: 23px">Upload File</TD>
									<TD style="HEIGHT: 23px">:</TD>
									<TD style="HEIGHT: 23px"><INPUT onkeypress="return false;" id="fileUpload" style="WIDTH: 392px; HEIGHT: 20px" type="file"
											size="46" name="fileUpload" runat="server">
										<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgTrClassAllocation" runat="server" Width="100%" AutoGenerateColumns="False"
								BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AllowCustomPaging="True"
								AllowSorting="True" PageSize="25" ForeColor="Gray" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False" HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kapasitas">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKapasitas" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Selisih Alokasi">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Allocated" HeaderText="Jumlah Alokasi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtAllocated Runat="server" MaxLength="3" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.Allocated") %>'>
											</asp:TextBox>
											<asp:RangeValidator id="RangeValidator1" runat="server" MinimumValue="0" MaximumValue="100" Type="Integer"
												ControlToValidate="txtAllocated" ErrorMessage="X"></asp:RangeValidator>
											<asp:TextBox id="txtTemp" runat="server" Width="64px" Visible="False"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi Diambil">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAllocationTaken" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastAllocated" HeaderText="Alokasi Sebelum">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastAllocated" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastAllocated") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastUpdateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CancelReason" HeaderText="Alasan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtCancelReason" runat="server" style="visibility:hidden;" Width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.CancelReason") %>'>
											</asp:TextBox>
											<asp:Label id="lblCancelReason" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CancelReason") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnUbah" runat="server" Text="Edit" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="dtgUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AllowCustomPaging="True"
								AllowSorting="True" PageSize="25" ForeColor="Gray" CellSpacing="1">
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
											<asp:TextBox Runat="server" ID="txtClassID" Width="0px" Text='<%# iif(isnothing(DataBinder.Eval(Container, "DataItem.TrClass")),0, DataBinder.Eval(Container, "DataItem.TrClass.ID")) %>' style="visibility:hidden;" >
											</asp:TextBox>
											<asp:TextBox Runat="server" ID="txtDealerID" Width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>' style="visibility:hidden;" >
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Kelas">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblClassCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kota">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCity" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kapasitas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKapasitas" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Selisih Alokasi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSelisih" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah Alokasi">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:TextBox id="txtAllocated" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Allocated") %>' style="text-align:right;" >
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Alokasi Sebelum">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastAllocated" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Last Update">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblLastUpdateUpload" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Error">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblError" Width="50px" runat="server"></asp:Label>
											<asp:TextBox Runat="server" ID="txtErrorFlag" Width="10px" style="visibility:hidden;"></asp:TextBox>
											<asp:TextBox Runat="server" ID="txtErrorOverLimit" Width="10px" style="visibility:hidden;"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="27"><asp:datagrid id="grid" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
							AllowCustomPaging="False" AllowSorting="False" PageSize="25" ForeColor="Gray" CellSpacing="1">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode Kelas">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode Dealer">
									<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Dealer">
									<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kota">
									<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kapasitas">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Selisih Alokasi">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Allocated" HeaderText="Jumlah Alokasi">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Alokasi Diambil">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUsedAllocation" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								<asp:BoundColumn DataField="LastAllocated" HeaderText="Alokasi Sebelum">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LastUpdateTime" HeaderText="Update Terakhir">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
									<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblLastUpdateTimeInGrid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CancelReason" HeaderText="Alasan">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<tr>
					<td align="center" height="27"><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnDownload" runat="server" Width="60px" Text="Download" CausesValidation="False"></asp:button>
					</td>
				</tr>
				<TR>
					<TD align="center" height="27"></TD>
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
