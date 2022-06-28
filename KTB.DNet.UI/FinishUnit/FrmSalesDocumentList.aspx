<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesDocumentList.aspx.vb" Inherits="FrmSalesDocumentList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesDocumentList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		
		function ShowPPSalesDocListHistory()
		{
			showPopUp('../PopUp/PopUpSalesDocumentList.aspx','',400,500,null);
		}

		function ShowPPUserGroupSelection()
		{
			showPopUp('../PopUp/PopUpDitujukanKepadaUser.aspx?x=Territory','',500,760,UserGroupSelection);
		}
		function UserGroupSelection(selectedUserGroup)
		{
			var txtUserGroup= document.getElementById("txtKepada");
			txtUserGroup.value = selectedUserGroup;				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" align="left" colSpan="3">UMUM - Daftar Dokumen Sales</td>
							</tr>
							<tr>
								<td align="center" background="../images/bg_hor_sales.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 197px" colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Departemen</TD>
								<TD width="1%"><STRONG>&nbsp;</STRONG>:</TD>
								<TD align="left" width="80%"><asp:dropdownlist id="ddlDepartement" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 20%">Kode Dealer</TD>
								<TD width="1%">&nbsp;:</TD>
								<TD align="left"><asp:textbox id="txtDealer" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtDealer','<>?*%$')"></asp:textbox>&nbsp;
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:label id="lblDealerInfo" runat="server">Label</asp:label></TD>
							</TR>
							<asp:Panel ID="pnlSearchKepada" Runat="server">
								<TR>
									<TD class="titleField" width="20%">Ditujukan Kepada</TD>
									<TD width="1%">:</TD>
									<TD>
										<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKepada" onblur="omitSomeCharacter('txtKepada','<>?*%$')"
											runat="server" Width="194"></asp:textbox>
										<asp:label id="lblSearchUserGroup" onclick="ShowPPUserGroupSelection()" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								</TR>
							</asp:Panel>
							<TR>
								<TD class="titleField" width="20%">Tanggal Upload</TD>
								<TD width="1%">&nbsp;:</TD>
								<TD vAlign="middle" align="left">
									<table>
										<tr>
											<td><cc1:inticalendar id="icMinDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s.d
											</td>
											<td><cc1:inticalendar id="icMaxDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px" width="20%">Jenis Surat</TD>
								<TD width="1%">&nbsp;:</TD>
								<TD style="HEIGHT: 23px" align="left"><asp:dropdownlist id="ddlJenisSurat" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Status Download</TD>
								<TD width="1%">&nbsp;:</TD>
								<TD align="left"><asp:dropdownlist id="ddlStatusDownload" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%" height="20"></TD>
								<TD width="1%"></TD>
								<TD vAlign="middle" align="left" height="20"><asp:button id="btnSearch" runat="server" Width="50px" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgSalesDocumentList" runat="server" Width="100%" AutoGenerateColumns="False"
											AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="White"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:label id="lblRed" runat="server">
															<img src="../images/red.gif" style="cursor:hand" border="0" alt="Belum Pernah Di Download"></asp:label>
														<asp:label id="lblGreen" runat="server">
															<img src="../images/green.gif" style="cursor:hand" border="0" alt="Sudah Pernah Di Download"></asp:label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDealer" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNamaDealer" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="KindOfLetter.Description" HeaderText="Jenis Surat">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLetter" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.KindOfLetter.Description") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="NomorSurat" HeaderText="No Surat">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoSurat" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NomorSurat") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="UploadDate" HeaderText="Tanggal Upload">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblUploadDate" Runat="server" text='<%# Format(DataBinder.Eval(Container, "DataItem.UploadDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Perihal" HeaderText="Perihal">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPerihal" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Perihal") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Penerima" HeaderText="Ditujukan Kepada">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPenerima" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Penerima") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="LastDownloadBy" HeaderText="Terakhir Didownload Oleh">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLastDownloadBy" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LastDownloadBy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="LastDownloadDate" HeaderText="Terakhir Didownload Pada">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblLastDownloadDate text='<%# Format(DataBinder.Eval(Container, "DataItem.LastDownloadDate"),"dd/MM/yyyy") %>' Runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" CommandName="View">
															<img src="../images/popup.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnDpwnload" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img src="../images/simpan.gif" border="0" alt="Download Dokumen"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" BackColor="#CCCCCC" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
