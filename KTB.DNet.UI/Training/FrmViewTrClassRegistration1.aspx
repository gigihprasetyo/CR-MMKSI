<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmViewTrClassRegistration1.aspx.vb" Inherits="FrmViewTrClassRegistration1" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>List Pendaftaran</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function popUpClassInformation(kode)
			{		
				var url = '../PopUp/PopUpClassInformation.aspx?kode='+kode;
				showPopUp(url,'',320,440,null);
			}
			
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtDealerSearchCode");
				txtDealer.value = selectedCode
				txtDealer.focus();
			}			
					
			function ShowCategoryManySelection()
			{
				showPopUp('../PopUp/PopUpCourseMany.aspx','',500,760,CategorySelection);
			}
			
			function CategorySelection(selectedCategory)
			{			
				var txtKode = document.getElementById("txtKodeKategori");			
				txtKode.value = selectedCategory			
			}
			
			function ShowPPClassSelection()
			{
			    var txtKode = document.getElementById("txtKodeKategori");
			    //showPopUp('../PopUp/PopUpClassSelection.aspx', '', 500, 760, classSelection);
			    showPopUp('../PopUp/PopUpClassSelection.aspx?CourseCode=' + txtKode.value, '', 500, 760, classSelection);
			}
			
			function classSelection(selectedClass)
			{
				var tempParam= selectedClass.split(';');
				var txtClassCode = document.getElementById("txtClassCode");
				txtClassCode.value = tempParam[0];
			}
								
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
				EnableDelete(aspCheckBoxID)
			}
			
			function EnableDelete(aspCheckBoxID) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')
				if (!document.forms[0].btnUpdate)
				{
					return
				}
				document.forms[0].btnUpdate.disabled = true
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							if (elm.checked)
							{								
								if (document.forms[0].btnUpdate)
								{
									document.forms[0].btnUpdate.disabled = false;
									return;
								}
							}
						}
					}
				}
			}						
			
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblHeader" runat="server">TRAINING - List Pendaftaran</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="19%" style="HEIGHT: 18px">Kode Organisasi</TD>
								<TD width="1%" style="HEIGHT: 18px">:</TD>
								<TD width="216" style="WIDTH: 216px; HEIGHT: 18px"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
								<TD class="titleField" width="28" style="WIDTH: 28px; HEIGHT: 18px"></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Organisasi</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 216px"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 28px"></TD>
							</TR>
							<asp:panel id="pnlDealerSearch" Runat="server" Visible="False" Width="100%">
								<TR>
									<TD class="titleField" width="20%">Dealer</TD>
									<TD width="1%">:</TD>
									<TD style="WIDTH: 216px" width="216">
										<asp:textbox id="txtDealerSearchCode" runat="server" Width="150px"></asp:textbox>
										<asp:label id="lblPopUpDealer" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
									<TD class="titleField" style="WIDTH: 28px" width="28"></TD>
								</TR>
							</asp:panel>
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
								<TD class="titleField" width="20%">Kode Kelas</TD>
								<td width="1%">:</td>
								<TD width="216" style="WIDTH: 216px">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClassCode" runat="server" Width="100"
										MaxLength="20"></asp:textbox>
									<asp:label id="lblPopUpClass" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
								</TD>
								<TD class="titleField" width="28" style="WIDTH: 28px"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">No Registrasi</TD>
								<TD width="1%">:</TD>
								<TD width="216" style="WIDTH: 216px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNoReg" Runat="server" Width="150px"></asp:textbox></TD>
								<TD class="titleField" width="28" style="WIDTH: 28px"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%" style="HEIGHT: 26px">Nama Siswa</TD>
								<TD width="1%" style="HEIGHT: 26px">:</TD>
								<TD width="216" style="WIDTH: 216px; HEIGHT: 26px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtTraineeName" Runat="server" Width="256px"></asp:textbox></TD>
								<TD class="titleField" width="28" style="WIDTH: 28px; HEIGHT: 26px"></TD>
							</TR>
							<TR>
								<TD class="titleField">Periode Pendaftaran</TD>
								<td>:</td>
								<TD class="titleField" style="WIDTH: 216px">
									<asp:DropDownList id="ddlYear" runat="server"></asp:DropDownList>
								</TD>
								<td align="right" style="WIDTH: 28px"></td>
							</TR>
							<TR>
								<TD class="titleField">Periode Kelas &nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
								</TD>
								<TD>:</TD>
								<TD style="WIDTH: 216px">
									<table>
										<tr>
											<td>
												<asp:CheckBox id="cbDate" runat="server"></asp:CheckBox>
											</td>
											<td>
												<cc1:inticalendar id="icStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<td>
												<asp:Label id="Label3" runat="server">s.d</asp:Label>
											</td>
											<td>
												<cc1:inticalendar id="icEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<TD align="right" style="WIDTH: 28px">
												<asp:button id="btnSearch" runat="server" Font-Bold="True" Text=" Cari "></asp:button>
											</TD>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" border="0" id="Table3">
						</table>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px" DESIGNTIMEDRAGDROP="77"><asp:datagrid id="dtgClassRegistration" runat="server" Width="100%" PageSize="50" AllowPaging="True"
								Font-Size="Small" AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0"
								CellPadding="3" GridLines="Vertical" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="cbAll" onclick="CheckAll('cbItem',this.checked)" type="checkbox" runat="server">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RegistrationDate" HeaderText="Tgl Pendaftaran">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegistrationDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No Reg">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.cityname") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink id="hlClass" runat="server"></asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblStartDate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblEndDate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrTrainee.JobPosition" HeaderText="Posisi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.JobPosition") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.JobPosition") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrTrainee.ShirtSize" HeaderText="Size">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ShirtSize") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ShirtSize") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:DropDownList id="ddlStatus" runat="server"></asp:DropDownList>
						<asp:Button id="btnUpdate" runat="server" Text="Ubah Status" Enabled="False"></asp:Button><asp:button id="btnProsesCetak" runat="server" Text="Proses Cetak/Download" Enabled="False"
							CausesValidation="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
		<script>
	EnableDelete('cbItem');
		</script>
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
