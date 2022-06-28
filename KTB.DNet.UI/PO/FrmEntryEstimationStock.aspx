<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryEstimationStock.aspx.vb" Inherits="FrmEntryEstimationStock"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmInputEstimationStock</title>
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Redemption Plan&nbsp;- Input Estimate Stock</td>
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
								<TD class="titleField" style="HEIGHT: 23px" width="24%">Period</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD style="WIDTH: 178px; HEIGHT: 23px" width="178"><table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlMonth" runat="server" Height="16px" Width="100px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlYear" runat="server" Width="56px"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
								<TD style="HEIGHT: 23px" width="55%">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td>Batas Maksimal Contract</td>
											<td>:</td>
											<td><cc1:inticalendar id="icContractDate" runat="server"></cc1:inticalendar></td>
											<td>
												<table cellpadding="0" cellspacing="0" border="0" width="100%">
													<tr>
														<td>
															<asp:textbox id="txtContractHour" Width="20" Runat="server" onkeypress="return numericOnlyUniv(event)"
																onblur="alphaNumericPlusSpaceBlur(txtContractHour)" MaxLength="2"></asp:textbox>
														</td>
														<td>:</td>
														<td>
															<asp:textbox id="txtContractMinute" Width="20" Runat="server" onkeypress="return numericOnlyUniv(event)"
																onblur="alphaNumericPlusSpaceBlur(txtContractMinute)" MaxLength="2"></asp:textbox>
														</td>
														<td>:</td>
														<td>
															<asp:textbox id="txtContractSecond" Width="20" Runat="server" onkeypress="return numericOnlyUniv(event)"
																onblur="alphaNumericPlusSpaceBlur(txtContractSecond)" MaxLength="2"></asp:textbox>
														</td>
													</tr>
												</table>
											</td>
											<td><asp:button id="btnSetContract" Width="50" Text="Set" Runat="server"></asp:button></td>
											<td></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 38px" width="24%"><asp:label id="lblCategory" runat="server"> Kategori</asp:label></TD>
								<TD style="HEIGHT: 38px" width="1%">:</TD>
								<td style="WIDTH: 178px; HEIGHT: 38px" width="178"><table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlCategory" runat="server" Width="80px" AutoPostBack="True" Height="24px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlSubCategory" runat="server" Width="96px" AutoPostBack="True" Height="24px"></asp:dropdownlist></td>
										</tr>
									</table>
								</td>
								<TD style="HEIGHT: 38px" width="55%"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblType" runat="server"> Tipe</asp:label></TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 178px">
									<table cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td>
												<asp:dropdownlist id="ddlType" runat="server" Width="80px" AutoPostBack="True"></asp:dropdownlist>
											</td>
											<td>
												<asp:dropdownlist id="ddlTipeWarna" runat="server" Width="96px"></asp:dropdownlist>
											</td>
										</tr>
									</table>
								</TD>
								<TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<TD colSpan="4" height="10"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><div id="div1" style="OVERFLOW: auto; HEIGHT: 370px">
							<asp:datagrid id="dtgMain" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="None"
								CellSpacing="1" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="10000000"
								ShowFooter="True">
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" CssClass="ms-formlabel DataGridFixedHeader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe/Warna" SortExpression="VechileType.VechileTypeCode">
										<HeaderStyle Width="22%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:textbox ID="txtID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' style="visibility:hidden;" >
											</asp:textbox>
											<asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Deskripsi" SortExpression="VechileType.Description">
										<HeaderStyle Width="22%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaterialDescription").replace(" ","_") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S1">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS1" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S2">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS2" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S3">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS3" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S4">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS4" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S5">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS5" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S6">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS6" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S7">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS7" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S8">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS8" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S9">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS9" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S10">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS10" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S11">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS11" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S12">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS12" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S13">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS13" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S14">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS14" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S15">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS15" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S16">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS16" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S17">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS17" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S18">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS18" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S19">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS19" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S20">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS20" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S21">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS21" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S22">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS22" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S23">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS23" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S24">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS24" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S25">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS25" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S26">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS26" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S27">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS27" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S28">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS28" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S29">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS29" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S30">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS30" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="S31">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtS31" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total">
										<HeaderStyle Width="25px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'')" MaxLength="3" ID="txtSubTotal" Runat="server"
												Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Temp" Visible="False">
										<HeaderStyle Width="100%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox ID="txtTemp" Runat="server" Text="" Width="25px"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="8" style="HEIGHT: 8px" align="center">
						<asp:button id="btnDownload" runat="server" Width="60px" Text="Download" Enabled="True"></asp:button>
						<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
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
