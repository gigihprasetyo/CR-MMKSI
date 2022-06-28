<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerMaintenance.aspx.vb" Inherits="FrmDealerMaintenance" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DEALER - Dealer Maintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelectionOne()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtLegalStatus");
			txtDealer.value = tempParam[0];				
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						ADMIN SISTEM&nbsp;-
						<asp:Label id="lblTitle" runat="server">Organisasi Baru</asp:Label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 24px" width="24%">Tipe Organisasi</TD>
								<TD style="HEIGHT: 24px" width="76%"><asp:dropdownlist id="ddlTitle" runat="server" Width="136px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px" width="24%">Kode Organisasi</TD>
								<TD style="HEIGHT: 24px" width="76%"><asp:textbox id="txtDealerCode" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDealerCode)"
										runat="server" Width="64px" MaxLength="6"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi kode dealer  (tidak boleh kosong)"
										ControlToValidate="txtDealerCode" Display="None"></asp:requiredfieldvalidator>
									<asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtDealerCode"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD><asp:textbox id="txtName" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtName)"
										runat="server" Width="264px" MaxLength="50"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi nama dealer  (tidak boleh kosong)"
										ControlToValidate="txtName" Display="None"></asp:requiredfieldvalidator>
									<asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Term Cari 1/2</TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtSearch1" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch1)"
										runat="server" Width="136px" MaxLength="20"></asp:textbox>/
									<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi Term Cari 1   (tidak boleh kosong)"
										ControlToValidate="txtSearch1" Display="None"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtSearch1"></asp:requiredfieldvalidator>&nbsp;
									<asp:textbox id="txtSearch2" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSearch2)"
										runat="server" Width="40px" MaxLength="4"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Silahkan isi Term Cari 2   (tidak boleh kosong)"
										ControlToValidate="txtSearch2" Display="None"></asp:requiredfieldvalidator>
									<asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtSearch2"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Area 1/2</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlArea1" runat="server" Width="144px"></asp:dropdownlist>/
									<asp:dropdownlist id="ddlArea2" runat="server" Width="136px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 14px">Grup</TD>
								<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlGroup" runat="server" Width="264px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD><STRONG>Legal Status</STRONG>&nbsp;</TD>
								<TD>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtLegalStatus" onblur="HtmlCharBlur(txtDealerCode)"
										runat="server" Width="96px" MaxLength="6"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Alamat</TD>
								<TD><asp:textbox id="txtAddress" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtAddress)"
										runat="server" Width="447px" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Silahkan isi alamat dealer (tidak boleh kosong)"
										ControlToValidate="txtAddress" Display="None"></asp:requiredfieldvalidator>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtAddress"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px">Provinsi</TD>
								<TD style="HEIGHT: 22px"><asp:dropdownlist id="ddlProvince" runat="server" Width="144px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Kota</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlCity" runat="server" Width="144px" Enabled="False"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kode 
									Pos&nbsp;
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPostCode)" id="txtPostCode"
										runat="server" Width="40px" MaxLength="5"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator12" runat="server" ErrorMessage="Silahkan isi Kode Pos dengan 5 digit angka"
										ControlToValidate="txtPostCode" Display="None" ValidationExpression="\d{5}"></asp:regularexpressionvalidator>&nbsp;<asp:regularexpressionvalidator id="RegularExpressionValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtPostCode"
										ValidationExpression="\d{5}"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Nomor SPA</TD>
								<TD style="HEIGHT: 18px"><asp:textbox id="txtSPANumber" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSPANumber)"
										runat="server" Width="312px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Tanggal SPA</TD>
								<TD style="HEIGHT: 18px"><asp:textbox id="txtSPADate" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtSPADate)"
										runat="server" Width="64px" MaxLength="8" ToolTip='Tanggal SPA diisi dengan format "ddMMyyyy", misal 20102005 berarti 20-10-2005'></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator13" runat="server" ErrorMessage='Silahkan isi tanggal SPA dengan format "ddMMyyyy",  misal "20102005 "'
										ControlToValidate="txtSPADate" Display="None" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:regularexpressionvalidator>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtSPADate"
										ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">No Penunjukan Dealer</TD>
								<TD style="HEIGHT: 18px">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNoPersetujuan" onblur="HtmlCharBlur(txtSPANumber)"
										runat="server" Width="312px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Tgl Penunjukan Dealer</TD>
								<TD style="HEIGHT: 18px">
									<cc1:inticalendar id="icTglPersetujuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px">Telpon</TD>
								<TD style="HEIGHT: 22px"><asp:textbox id="txtTelpArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtTelpArea)"
										runat="server" Width="32" MaxLength="4"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ErrorMessage="Silahkan isi Kode Area telpon  dengan [3,4] digit angka "
										ControlToValidate="txtTelpArea" Display="None" ValidationExpression="\d{3,4}"></asp:regularexpressionvalidator>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator16" runat="server" ErrorMessage="*" ControlToValidate="txtTelpArea"
										ValidationExpression="\d{3,4}"></asp:regularexpressionvalidator>-
									<asp:textbox id="txtTelpNo" onkeypress="return HtmlCharUniv(event)" runat="server" Width="96px"
										MaxLength="45"></asp:textbox>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" ErrorMessage="Silahkan isi No Telp  dengan [5,45] digit angka dan / (garis miring)"
										ControlToValidate="txtTelpNo" Display="None" ValidationExpression="^[0-9/]\d{5,45}"></asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtTelpNo"
										ValidationExpression="^[0-9/]\d{5,45}"></asp:regularexpressionvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Fax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtFaxArea" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxArea)"
										runat="server" Width="32px" MaxLength="4"></asp:textbox>-
									<asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ErrorMessage="Silahkan isi Kode Area Fax dengan [3,4] digit angka"
										ControlToValidate="txtFaxArea" Display="None" ValidationExpression="\d{3,4}"></asp:regularexpressionvalidator>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator18" runat="server" ErrorMessage="*" ControlToValidate="txtFaxArea"
										ValidationExpression="\d{3,4}"></asp:regularexpressionvalidator><asp:textbox id="txtFaxNo" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtFaxNo)"
										runat="server" Width="96px" MaxLength="14"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator7" runat="server" ErrorMessage="Silahkan isi No Fax dengan [5,14] digit angka"
										ControlToValidate="txtFaxNo" Display="None" ValidationExpression="\d{5,14}"></asp:regularexpressionvalidator>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator19" runat="server" ErrorMessage="*" ControlToValidate="txtFaxNo"
										ValidationExpression="\d{5,14}"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Email</TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtEmailAdd" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtEmailAdd)"
										runat="server" Width="248px" MaxLength="40"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator9" runat="server" ErrorMessage="Silahkan isi email dealer dengan format email"
										ControlToValidate="txtEmailAdd" Display="None" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator>&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator20" runat="server" ErrorMessage="*" ControlToValidate="txtEmailAdd"
										ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Situs web</TD>
								<TD style="HEIGHT: 26px"><asp:textbox id="txtWeb" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtWeb)"
										runat="server" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Bebas&nbsp;PPh22</TD>
								<TD style="HEIGHT: 26px">
									<table>
										<tr>
											<td>
												<asp:CheckBox id="chbxFreePPh" runat="server"></asp:CheckBox>
											</td>
											<td>Selama Periode</td>
											<td><asp:TextBox id="txtValidFrom" runat="server" MaxLength="6" onkeypress="return numericOnlyUniv(event)"
													Width="72px"></asp:TextBox></td>
											<td>s/d</td>
											<td><asp:TextBox id="txtValidTo" runat="server" MaxLength="6" onkeypress="return numericOnlyUniv(event)"
													Width="80px"></asp:TextBox></td>
											<td><asp:Label id="lblFormat" runat="server" ForeColor="Red">Format : MMyyyy</asp:Label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">Status</TD>
								<TD style="HEIGHT: 19px"><asp:dropdownlist id="ddlStatus" runat="server" Width="104px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD><STRONG>Area Bisnis</STRONG></TD>
								<TD><asp:checkbox id="cbSalesUnit" runat="server" AutoPostBack="True" Text="Sales Unit"></asp:checkbox><asp:checkbox id="cbService" runat="server" AutoPostBack="True" Text="Service"></asp:checkbox><asp:checkbox id="cbSpareParts" runat="server" AutoPostBack="True" Text="Spare Parts"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px"><STRONG>Perubahan Terakhir</STRONG>
								</TD>
								<TD style="HEIGHT: 12px"><asp:label id="LblLastChange" runat="server" Font-Size="Smaller"></asp:label></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
							DisplayMode="SingleParagraph"></asp:validationsummary></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="titleTableSales" align="center">SALES UNIT</td>
								<td class="titleTableService" align="center">SERVICE</td>
								<td class="titleTableParts" align="center">SPARE PARTS</td>
							</tr>
							<TR>
								<TD width="33%"><asp:panel id="pnlSales" runat="server" Width="100%" Enabled="False">
										<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD>Contact Person</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtContactPerson1" onblur="HtmlCharBlur(txtContactPerson1)"
														runat="server" Width="140px" MaxLength="30"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>HP</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtHP1" onblur="HtmlCharBlur(txtHP1)"
														runat="server" Width="140px" MaxLength="20"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="txtHP1"
														ErrorMessage="Silahkan Isi No HP Sales Unit dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator21" runat="server" ControlToValidate="txtHP1" ErrorMessage="*"
														ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 21px">
													<P>Email</P>
												</TD>
												<TD style="HEIGHT: 21px">
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtEmail1" onblur="HtmlCharBlur(txtEmail1)"
														runat="server" Width="140px" MaxLength="40"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator8" runat="server" Display="None" ControlToValidate="txtEmail1"
														ErrorMessage="Email Sales Unit diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator22" runat="server" ControlToValidate="txtEmail1" ErrorMessage="*"
														ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD>Perubahan Terakhir</TD>
												<TD>
													<asp:Label id="lblLastUpdate1" runat="server" Font-Size="Smaller"></asp:Label></TD>
											</TR>
										</TABLE>
									</asp:panel></TD>
								<TD width="33%"><asp:panel id="pnlService" runat="server" Width="100%" Enabled="False">
										<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD>Contact Person</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtContactPerson2" onblur="HtmlCharBlur(txtContactPerson2)"
														runat="server" Width="140px" MaxLength="30"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>HP</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtHP2" onblur="HtmlCharBlur(txtHP2)"
														runat="server" Width="140px" MaxLength="20"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" Display="None" ControlToValidate="txtHP2"
														ErrorMessage="Silahkan Isi No HP Servis dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator23" runat="server" ControlToValidate="txtHP2" ErrorMessage="*"
														ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD>
													<P>Email</P>
												</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtEmail2" onblur="HtmlCharBlur(txtEmail2)"
														runat="server" Width="140px" MaxLength="40"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator10" runat="server" Display="None" ControlToValidate="txtEmail2"
														ErrorMessage="Email Servis diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator25" runat="server" ControlToValidate="txtEmail2" ErrorMessage="*"
														ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD>Perubahan Terakhir</TD>
												<TD>
													<asp:Label id="lblLastUpdate2" runat="server" Font-Size="Smaller"></asp:Label></TD>
											</TR>
										</TABLE>
									</asp:panel></TD>
								<TD width="33%"><asp:panel id="pnlSpareparts" runat="server" Width="100%" Enabled="False" Height="104px" HorizontalAlign="Center">
										<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD>Contact Person</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtContactPerson3" onblur="HtmlCharBlur(txtContactPerson3)"
														runat="server" Width="140px" MaxLength="30"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>HP</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtHP3" onblur="HtmlCharBlur(txtHP3)"
														runat="server" Width="140px" MaxLength="20"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" Display="None" ControlToValidate="txtHP3"
														ErrorMessage="Silahkan Isi No HP Spare Part dengan [8,14] digit angka" ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator24" runat="server" ControlToValidate="txtHP3" ErrorMessage="*"
														ValidationExpression="\d{8,14}"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD>
													<P>Email</P>
												</TD>
												<TD>
													<asp:TextBox onkeypress="return HtmlCharUniv(event)" id="txtEmail3" onblur="HtmlCharBlur(txtEmail3)"
														runat="server" Width="140px" MaxLength="40"></asp:TextBox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator11" runat="server" Display="None" ControlToValidate="txtEmail3"
														ErrorMessage="Email Spare Part diisi dengan format email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator id="RegularExpressionValidator26" runat="server" ControlToValidate="txtEmail3" ErrorMessage="*"
														ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
											</TR>
											<TR>
												<TD>Perubahan Terakhir</TD>
												<TD>
													<asp:Label id="lblLastUpdate3" runat="server" Font-Size="Smaller"></asp:Label></TD>
											</TR>
										</TABLE>
									</asp:panel></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<asp:Button id="btnBack" runat="server" Text="Kembali"></asp:Button>
						<asp:button id="btnReset" runat="server" Text="Reset" CausesValidation="False"></asp:button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></td>
				</tr>
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
