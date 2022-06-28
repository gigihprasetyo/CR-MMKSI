<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmPreviewCustomer.aspx.vb" Inherits="FrmPreviewCustomer" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Preview Data Konsumen ASS</title>
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
			<iframe id="fraDownload" style="DISPLAY: none" src="" width="0" height="0" runat="server">
			</iframe>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Preview Data Konsumen ASS</td>
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
								<TD width="80%"><asp:label id="lblDealer" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Kategori Survei</TD>
								<TD width="1%">:</TD>
								<TD width="80%">
									<asp:Label ID="lblASS" runat="server">After Sales Service</asp:Label>
									<asp:dropdownlist id="ddlKategoriKonsumen" runat="server" Width="230px" Visible="False"></asp:dropdownlist>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Kendaraan</TD>
								<TD width="1%">:</TD>
								<TD width="80%"><asp:dropdownlist id="ddlJenisKendaraan" runat="server" Width="230px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Periode Survei</TD>
								<TD width="1%">:</TD>
								<TD vAlign="middle" width="80%">
									<table id="tblPeriod" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlMonth" runat="server" Width="80px"></asp:dropdownlist></td>
											<td><asp:dropdownlist id="ddlYear" runat="server" Width="50px"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<!--
							<TR>
								<TD class="titleField" style="HEIGHT: 3px">Kota</TD>
								<TD width="1%">:</TD>
								<TD width="80%"><asp:dropdownlist id="ddlKotaDealer" runat="server" Width="230px"></asp:dropdownlist></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Area Dealer</TD>
								<TD width="1%">:</TD>
								<td width="60%"><asp:dropdownlist id="ddlAreaDealer" runat="server" Width="230px"></asp:dropdownlist></td>
							</TR>
							<TR vAlign="top">
								<TD class="titleField">Group Dealer</TD>
								<TD width="1%">:</TD>
								<td width="60%"><asp:dropdownlist id="ddlGroupDealer" runat="server" Width="230px"></asp:dropdownlist></td>
							</TR>-->
							<TR vAlign="top">
								<TD class="titleField">Nama Konsumen</TD>
								<TD width="1%">:</TD>
								<td width="60%"><asp:textbox id="txtKonsumen" runat="server" Width="230px"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%"><asp:button id="btnShow" runat="server" Text="Tampilkan" width="70px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" class="titleField">
						<asp:Label ID="lblMessage" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgReports" runat="server" Width="100%" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False"
								BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" PageSize="25">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerCityName" SortExpression="DealerCityName" HeaderText="Kota Dealer">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Gelar" SortExpression="Gelar" HeaderText="Gelar">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ConsumerName" SortExpression="ConsumerName" HeaderText="Nama Konsumen">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HandphoneNo" SortExpression="HandphoneNo" HeaderText="No Handphone">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HomePhoneAreaCode" SortExpression="HomePhoneAreaCode" HeaderText="Kode Area Rumah">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HomePhoneNo" SortExpression="HomePhoneNo" HeaderText="Telp Rumah">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OfficePhoneAreaCode" SortExpression="OfficePhoneAreaCode" HeaderText="Kode Area Kantor">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OfficePhoneNo" SortExpression="OfficePhoneNo" HeaderText="Telp Kantor">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="OfficePhoneNoExt" SortExpression="OfficePhoneNoExt" HeaderText="Ekstensi">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VehicleCategory" SortExpression="VehicleCategory" HeaderText="Jenis Kendaraan">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VehicleType" SortExpression="VehicleType" HeaderText="Tipe Kendaraan">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NameSTNK" SortExpression="NameSTNK" HeaderText="Nama di STNK">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AddressSTNK" SortExpression="AddressSTNK" HeaderText="Alamat di STNK">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Kota">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ChassisNo" SortExpression="ChassisNo" HeaderText="No. Chassis">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TransactionDate" SortExpression="TransactionDate" DataFormatString="{0:dd/MM/yyyy}"
										HeaderText="Tgl. Service">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Odometer" SortExpression="Odometer" HeaderText="Odo Meter (KM)">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ServiceType" SortExpression="ServiceType" HeaderText="Jenis Perawatan / Perbaikan">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="StatusDescription" SortExpression="StatusDescription" HeaderText="Status"
										Visible="False">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
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
			<INPUT id="hdnValSave" type="hidden" value="-1" name="hdnValSave" runat="server">
		</form>
	</body>
</HTML>
