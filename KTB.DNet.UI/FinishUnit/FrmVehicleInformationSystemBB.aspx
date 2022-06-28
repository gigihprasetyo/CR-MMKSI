<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmVehicleInformationSystemBB.aspx.vb" Inherits="FrmVehicleInformationSystemBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UMUM - Informasi Kendaraan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
				
		function firstFocus()
		{
			document.all.txtChassisNumber.focus();
		}
		
			function enter(controlAfter)
		{
			
			var charPressed = event.keyCode;
				if (charPressed == 13)
			{
				controlAfter.focus();
				return false;
			}
				
		}
		function ShowPopUp()
			{
			}
			
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
			
			function ManageControlByLeasing()
			{
				var trEngineNumber = document.getElementById("trEngineNumber");
				
				if(!IsLeasing())
				{
					trEngineNumber.style.visibility='hidden';
					return false;	
				} 
				var tbl = document.getElementById("Table1");
				var i =0;
				//var dgPMStatus = document.getElementById("dgPMStatus");//trPMStatus
				var trPMStatus = document.getElementById("trPMStatus");
				//var dtgServiceData = document.getElementById("dtgServiceData");//id="trServiceData"
				var trServiceData = document.getElementById("trServiceData");
				var trFreeServiceTitle = document.getElementById("trFreeServiceTitle");
				var trMaintenancePeriod = document.getElementById("trMaintenancePeriod");

				//dgPMStatus.style.visibility = 'hidden';
				//dtgServiceData.style.visibility = 'hidden';
				trPMStatus.style.visibility='hidden';
				trServiceData.style.visibility='hidden';
				trFreeServiceTitle.style.visibility='hidden';
				trMaintenancePeriod.style.visibility='hidden';
				trEngineNumber.style.visibility='visible';
				
				for(i=5;i<tbl.rows.length-1;i++)				
				{
					tbl.rows[i].style.visibility='hidden';
				}
			}
			function IsLeasing()
			{
				var query=window.location.search.substring(1);
				var vars = query.split("&"); 
				
				for (var i=0;i<vars.length;i++) { 
					var pair = vars[i].split("="); 
					if (pair[0] == "IsLeasing") { 
						return true;
					} 
				} 
				return false;
			}
		</script>
	</HEAD>
	<body onload="firstFocus()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD colSpan="6">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">UMUM - Informasi Kendaraan (Special)</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="24%">Nomor Rangka</TD>
					<TD width="1%">:</TD>
					<TD width="75%" colSpan="4"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtChassisNumber" runat="server"></asp:textbox><asp:button id="btnSearch" runat="server" Text="Cari" Width="50px"></asp:button></TD>
				</TR>
				<TR id="trEngineNumber">
					<TD class="titleField" width="24%">Nomor Mesin</TD>
					<TD width="1%">:</TD>
					<TD colspan="2">
						<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNoEngine" runat="server"></asp:textbox>
						<asp:button id="btnSearchEngine" runat="server" Width="50px" Text="Cari"></asp:button></TD>
					<TD width="1%"></TD>
					<TD width="29%"></TD>
				</TR>
				<TR>
					<TD class="titleField" width="24%" style="HEIGHT: 18px">Model / Tipe / Warna</TD>
					<TD width="1%" style="HEIGHT: 18px">:</TD>
					<TD width="25%" style="HEIGHT: 18px"><asp:label id="lblMaterial" runat="server"></asp:label></TD>
					<TD class="titleField" width="20%" style="HEIGHT: 18px">No Serial</TD>
					<TD width="1%" style="HEIGHT: 18px">:</TD>
					<TD width="29%" style="HEIGHT: 18px"><asp:label id="lblNoSerial" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">No Rangka</TD>
					<TD>:</TD>
					<TD><asp:label id="lblNoChassis" runat="server"></asp:label></TD>
					<TD class="titleField">Tanggal Buka Faktur</TD>
					<TD>:</TD>
					<TD><asp:label id="lblFakturOpenDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">No Mesin</TD>
					<TD>:</TD>
					<TD><asp:label id="lblNoEngine" runat="server"></asp:label></TD>
					<TD class="titleField">Tanggal Faktur</TD>
					<TD>:</TD>
					<TD>
						<asp:label id="lblFakturDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">Dealer Alokasi</TD>
					<TD>:</TD>
					<TD><asp:label id="lblDealerSold" runat="server"></asp:label></TD>
					<TD class="titleField">Tanggal Cetak DO</TD>
					<TD>:</TD>
					<TD><asp:label id="lblDOPrintDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField">Dealer Pelaksana PDI</TD>
					<TD>:</TD>
					<TD><asp:label id="lblDealerPDI" runat="server"></asp:label></TD>
					<TD class="titleField">Tanggal Unit Keluar MKS</TD>
					<TD>:</TD>
					<TD><asp:label id="lblStockOutDate" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 18px"></TD>
					<TD style="HEIGHT: 18px"></TD>
					<TD style="HEIGHT: 18px"></TD>
					<TD class="titleField" style="HEIGHT: 18px">Tanggal PDI</TD>
					<TD style="HEIGHT: 18px">:</TD>
					<TD style="HEIGHT: 18px"><asp:label id="lblTglPDI" runat="server"></asp:label><asp:label id="lblPDIIndicator" runat="server"></asp:label>
						<asp:button id="BtnDeletePDI" runat="server" Text="Hapus PDI" Visible="False"></asp:button>
						<asp:LinkButton id="lbtnDeletePDI" Runat="server" CommandName="DeletePM" text="Hapus" CausesValidation="False"
							Visible="False">
							<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 10px" colSpan="6"></TD>
				</TR>
				<TR id="trFreeServiceTitle">
					<TD colSpan="6"><EM><STRONG><FONT size="2"> Free Service Data</FONT></STRONG></EM></TD>
				</TR>
				<TR id="trServiceData">
					<TD colSpan="6">
						<div id="div1" style="OVERFLOW: auto"><asp:datagrid id="dtgServiceData" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
								PageSize="50" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FSKind.KindCode" HeaderText="Kind">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblKind runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal FS">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblFS runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Proses">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglPro runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" Runat="server" CommandName="Delete" text="Hapus" CausesValidation="False">
												<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<tr id="trMaintenancePeriod">
					<TD colSpan="6"><EM><STRONG><FONT size="2"> Periodical Maintenance&nbsp;Data</FONT></STRONG></EM></TD>
					</TD>
				</tr>
				<tr id="trPMStatus">
					<TD colSpan="6">
						<asp:datagrid id="dgPMStatus" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							BorderColor="#E7E7FF" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD"
							BorderStyle="None" GridLines="Vertical" PageSize="100">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="StandKM" HeaderText="Kind">
									<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKindCode") %>' Tooltip='<%# DataBinder.Eval(Container, "DataItem.PMKindDesc") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StandKM" SortExpression="StandKM" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal PM">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tanggal Proses">
									<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Pelaksana">
									<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' >
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PMStatus" HeaderText="Status">
									<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblPopUpDetail" runat="server">
											<img src="../images/detail.gif" border="0" alt="Replacement Part"></asp:Label>
										<asp:LinkButton id="lbtnDelete" Runat="server" CommandName="DeletePM" text="Hapus" CausesValidation="False">
											<img border="0" src="../images/trash.gif" alt="Hapus" style="cursor:hand"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id=lblPMID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</tr>
			</TABLE>
		</form>
		<script language="javascript">
			ManageControlByLeasing();
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
