<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmChassisMasterProfile.aspx.vb" Inherits="FrmChassisMasterProfile" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			var SomeChecked = false;
			function MakeValid()
			{
				SomeChecked = true;
			}
			
			function IsChecked() {
				if (IsAnyCheckedCheckBox('chkSelect')) {
					SomeChecked = true;
				}
				else {
					SomeChecked = false;
					alert("Anda belum memilih faktur");
				}
			}
						
		</script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1"  method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">KONSUMEN - Daftar Profile&nbsp;</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 11px" width="20%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD width="24%"><asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 11px" width="20%"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 11px" width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 11px"><asp:dropdownlist id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblChassisNo" runat="server">Nomor Rangka</asp:label></TD>
								<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD><asp:textbox id="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
										runat="server" Width="140px" MaxLength="20" size="22"></asp:textbox></TD>
								<TD class="titleField">Tanggal Pembuatan</TD>
								<TD><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD noWrap width="34%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartPembuatan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndPembuatan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Propinsi</TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlProvince" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="titleField">Kota</TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD noWrap><asp:dropdownlist id="ddlCity" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Berdasarkan</TD>
								<TD>:</TD>
								<TD>
									<asp:RadioButton id="rdoBasedOnDealer" runat="server" Text="Dealer" GroupName="basedon" Checked="True"></asp:RadioButton>
									<asp:RadioButton id="rdoBasedOnCustomer" runat="server" Text="Customer" GroupName="basedon"></asp:RadioButton></TD>
								<TD class="titleField"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgInvoiceList" runat="server" Width="100%" DataKeyField="ID" AllowCustomPaging="True"
											AllowPaging="True" PageSize="100" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" AllowSorting="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Check">
													<HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.all.chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Kategori">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblCat runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileColor.VechileType.VechileTypeCode" HeaderText="Tipe">
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.VechileType.VechileTypeCode") %>' ID="Label7">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileColor.MaterialDescription" HeaderText="Deskripsi">
													<HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialDescription") %>' ID="Label2">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Pelanggan">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKodePelanggan" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nama Pelanggan">
													<HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNamaPelanggan" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="FakturNumberText" SortExpression="EndCustomer.FakturNumber" ReadOnly="True"
													HeaderText="Nomor Faktur ">
													<HeaderStyle Width="6%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Profile Pelanggan">
													<HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblProfilePelanggan" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Detail">
													<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkView" runat="server" CommandName="lnkView">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil"></asp:LinkButton>
														<asp:LinkButton id="LinkEdit" runat="server" CommandName="lnkEdit">
															<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Edit"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
						<asp:button id="btnDnLoad" runat="server" Width="96px" Text="Download"></asp:button>&nbsp;
						<asp:Button id="btnSAP" runat="server" Text="Transfer to SAP"></asp:Button><asp:textbox id="txtDownload" runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">

			document.getElementById("txtDownload").style.visibility="hidden";
			
			if (document.getElementById("txtDownload").value != "")
			{
				var downloadURL = document.getElementById("txtDownload").value
				document.getElementById("txtDownload").value = ""
				document.location.href="../DownloadContainer.aspx?"+downloadURL	
				
						
			}
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
