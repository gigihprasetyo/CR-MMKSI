<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmReportDownload.aspx.vb" Inherits="FrmReportDownload" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Tipe Kompetitor</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerCode = document.getElementById("txtDealerCode");
				txtDealerCode.value = tempParam;
			}
			function DownloadCcReport(fullPath)
			{
				//window.open("../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath, "","top=0,location=0,status=0, scrollbars=0,width=1px,height=1px");
				document.getElementById("fraDownload").src="../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath;
			}
			
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<iframe id="fraDownload" runat="server" width="0" height="0" style="DISPLAY:none">
			</iframe>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						Permintaan dan Download Report</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="80%"><asp:textbox onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" Width="230px" ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label>
									<asp:requiredfieldvalidator id="valDealer" runat="server" ControlToValidate="txtDealerCode" Display="Dynamic" ErrorMessage="* Dealer harus dipilih"></asp:requiredfieldvalidator>
								</TD>
							</TR>
							<!--
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<td width="60%"><asp:label id="lblDealerName" runat="server" EnableViewState="True"></asp:label></td>
							</TR>
							-->
							<TR>
								<TD class="titleField">Report</TD>
								<TD width="1%">:</TD>
								<TD width="80%"><asp:dropdownlist id="ddlServiceCategory" runat="server" Width="230px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 3px">Jenis Pelayanan</TD>
								<TD width="1%">:</TD>
								<TD width="80%">
									<asp:dropdownlist id="ddlServiceType" runat="server" Width="230px"></asp:dropdownlist></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Jenis Kendaraan</TD>
								<TD width="1%">:</TD>
								<td width="60%">
									<asp:dropdownlist id="ddlVehicle" runat="server" Width="230px"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Periode Bulan / Tahun</TD>
								<TD width="1%">:</TD>
								<TD width="80%" valign="middle">
									<table id="tblPeriod" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="90"><cc1:inticalendar id="icPeriodFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td width="20" align="center"><asp:Label ID="lblsd" Runat="server" Width="30px">s/d</asp:Label>
											<td width="90"><cc1:inticalendar id="icPeriodTo" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%"><asp:button id="btnShow" runat="server" width="70px" Text="Tampilkan"></asp:button><asp:button id="btnSave" runat="server" width="70px" Text="Simpan"></asp:button>&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px">
							<asp:datagrid id="dgReports" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode">
										<HeaderStyle Width="5%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer">
										<HeaderStyle Width="23%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblClassCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodFrom" HeaderText="Periode" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="15%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcReportMaster.RptDesc" HeaderText="Report">
										<HeaderStyle Width="20%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcReportMaster.RptDesc") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcCustomerCategory.Description" HeaderText="Jenis Pelayanan">
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDescription Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcCustomerCategory.Description") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CcVehicleCategory.Code" HeaderText="Jenis Kendaraan">
										<HeaderStyle Width="7%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label1" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CcVehicleCategory.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="File" Visible="False">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblFileName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PdfFileName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Tanggal Permintaan">
										<HeaderStyle Width="8%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRequestDate" Runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container, "DataItem.LastUpdateTime")) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ReportStatus" HeaderText="Status">
										<HeaderStyle Width="13%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="8%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDownload" runat="server" Text="Download" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<INPUT id="hdnValSave" type="hidden" value="-1" runat="server" NAME="hdnValSave">
		</form>
	</body>
</HTML>
