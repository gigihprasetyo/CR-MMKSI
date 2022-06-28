<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryRedemptionPlan.aspx.vb" Inherits="FrmEntryRedemptionPlan"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEntryRedemptionPlan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<!-- Start Header & Footer Freeze -->
		<style type="text/css">.DataGridFixedHeader { POSITION: relative; ; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white }
		</style>
		<script language="javascript" type="text/javascript"> 
			function getScrollBottom(p_oElem){
				return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
			}
		</script>
		<style type="text/css">.DataGridFixedFooter { ; BOTTOM: expression(getScrollBottom(this.offsetParent)); POSITION: relative; BACKGROUND-COLOR: white }
		</style>
		<!-- End Header & Footer Freeze -->
		<script>
			//function ShowDetail(RedemptionHeaderID,DealerCode)
			function ShowDetail(RowIndex,DayIndex,DealerCode)
			{	
				//showPopUp('../PopUp/PopUpAlertSound.aspx');
				//showPopUp('../PopUp/PopUpRedemptionDetail.aspx?RedemptionHeaderID='+RedemptionHeaderID+'&DealerCode='+DealerCode,'',500,600,RDStatus);
				var txtRowIndex = document.getElementById("txtRowIndex");
				txtRowIndex.value = RowIndex;
				showPopUp('../PopUp/PopUpRedemptionDetail.aspx?RowIndex='+RowIndex+'&DayIndex='+DayIndex+'&DealerCode='+DealerCode,'',500,600,RDStatus);				
			}
			function RDStatus(a)
			{	
				var btnRefreshGrid = document.getElementById("btnRefreshGrid");
				btnRefreshGrid.click();				
				//window.location="FrmEntryRedemptionPlan.aspx?AutoRefresh=1";
			}
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
	<body bottomMargin="3" leftMargin="3" topMargin="3" rightMargin="3" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div style="display:none;">
				<asp:Button Runat="server" ID="btnRefreshGrid" text="RefreshGrid"></asp:Button>
				<asp:TextBox Runat="server" ID="txtRowIndex" Text="0"></asp:TextBox>
			</div>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Redemption Plan&nbsp;-
						<asp:label id="lblTitle" runat="server">Input</asp:label>Redemption Plan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 32px" width="24%">Period</TD>
								<TD style="HEIGHT: 32px" width="1%">:</TD>
								<TD style="WIDTH: 187px; HEIGHT: 32px" width="187"><table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlMonth" runat="server" Width="80px" Height="16px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlYear" runat="server" Width="56px"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
								<TD style="HEIGHT: 32px" width="55%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px" width="24%"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<td style="WIDTH: 187px; HEIGHT: 23px" width="187"><asp:dropdownlist id="ddlCategory" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></td>
								<TD style="HEIGHT: 23px" width="55%"><asp:dropdownlist id="ddlSubCategory" runat="server" Width="120px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblType" runat="server"> Tipe</asp:label></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 187px"><asp:dropdownlist id="ddlType" runat="server" Width="140px" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD>
									<asp:dropdownlist id="ddlTipeWarna" runat="server" Width="120px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblDealerCodeColon" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 187px">
									<table cellpadding="0" cellspacing="0" border="0" width="100%">
										<tr>
											<td>
												<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
													runat="server"></asp:textbox>
											</td>
											<td>
												<asp:label id="lblSearchDealer" runat="server">
													<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" OnClick="ShowPPDealerSelection();"></asp:label>
											</td>
										</tr>
									</table>
								</TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD colSpan="4" height="10">&nbsp;* Batas maksimal penginputan redemption plan 
									adalah sampai dengan
									<asp:label id="lblMaxContractDate" runat="server" Width="224px"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0"> <!-- 1300 -->
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 320px">
							<asp:datagrid id="dtgMain" runat="server" Width="1000px" ShowFooter="True" AutoGenerateColumns="False"
								BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
								GridLines="None" CellSpacing="1" AllowSorting="True" PageSize="10000">
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" CssClass="ms-formlabel DataGridFixedHeader"
									BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle Width="50px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Tipe/Warna">
										<HeaderStyle Width="50px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:textbox ID="txtID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' style="visibility:hidden;" Width="1px" >
											</asp:textbox>
											<asp:Label ID="lblCode" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Deskripsi">
										<HeaderStyle Width="120px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblDescription" runat="server" Text=''></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="O/C Unit">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblOC" Runat="server" Text="0" Width="25px" style="text-align:center;"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S1">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH1" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS1" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image1" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S2">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH2" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS2" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image2" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S3">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH3" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS3" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image3" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S4">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH4" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS4" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image4" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S5">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH5" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS5" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image5" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S6">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH6" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS6" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image6" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S7">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH7" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS7" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image7" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S8">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH8" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS8" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image8" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S9">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH9" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS9" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image9" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S10">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH10" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS10" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image10" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S11">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH11" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS11" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image11" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S12">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH12" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS12" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image12" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S13">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH13" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS13" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image13" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S14">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH14" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS14" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image14" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S15">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH15" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS15" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image15" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S16">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH16" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS16" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image16" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S17">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH17" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS17" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image17" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S18">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH18" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS18" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image18" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S19">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH19" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS19" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image19" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S20">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH20" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS20" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image20" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S21">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH21" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS21" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image21" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S22">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH22" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS22" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image22" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S23">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH23" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS23" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image23" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S24">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH24" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS24" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image24" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S25">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH25" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS25" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image25" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S26">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH26" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS26" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image26" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S27">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH27" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS27" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image27" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S28">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH28" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS28" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image28" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S29">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH29" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS29" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image29" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S30">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH30" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS30" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image30" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S31">
										<HeaderStyle CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<HeaderTemplate>
											<asp:Label ID="lblH31" Runat="server"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lblS31" Runat="server" Text="" Width="25px" style="text-align:center;">
												<asp:Image ID="Image31" Runat="server" ImageUrl="../Images/Checked.gif" Width="9" Height="9"></asp:Image>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSubTotal" Runat="server" Text="" Width="25px" style="text-align:center;"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px" align="left" height="8"><asp:button id="btnDownload" runat="server" Width="60px" Text="Download"></asp:button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnDistribute" Text="Auto Distribute" Runat="server"></asp:button></TD>
				</TR>
			</table>
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
		<asp:Label Runat="server" ID="lblBreak" style="VISIBILITY:hidden" text="<br />"></asp:Label>
	</body>
</HTML>
