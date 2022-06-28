<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSparePartOrganization.aspx.vb" Inherits="FrmSparePartOrganization" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript">
			function ShowDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtKodeDealer = document.getElementById("txtDealerCode");
				var lblNama = document.getElementById("lblNama");
				var lblKota = document.getElementById("lblKota");
				txtKodeDealer.value = tempParam[0];			
				lblNama.innerHTML = tempParam[1];	
				lblKota.innerHTML = tempParam[2];	
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
			
			function ShowSalesmanSelection()
			{	
				var lblSalesmanCode = document.getElementById("lblShowSalesman");
				showPopUp('../PopUp/PopUpSalesmanPart.aspx?IsGroupDealer=1&IsSales=1','',470,600,SalesmanSelection);
			}
				
			function SalesmanSelection(SelectedSalesman)
			{
				var tempParam = SelectedSalesman.split(';');
				var txtSalesmanCode = document.getElementById("txtID");
				var txtNama = document.getElementById("txtNama");
				txtSalesmanCode.value = tempParam[0]
				txtNama.value = tempParam[1];
			}
			
		</script>
		<style type="text/css">
			.style3 { WIDTH: 150px }
			.styleHseparator { WIDTH: 40px }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage" style="HEIGHT: 8px">SPARE PART&nbsp;-&nbsp;Struktur 
						Organisasi</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="1" width="100%">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" Width="60px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label>
									<asp:requiredfieldvalidator id="valDealer" runat="server" ControlToValidate="txtDealerCode" ErrorMessage="Dealer harus dipilih">* Dealer harus dipilih</asp:requiredfieldvalidator>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<td><asp:label id="lblNama" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField">Kota</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<td><asp:label id="lblKota" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td>
									<asp:button id="btnCari" runat="server" width="60px" Text="Tampilkan"></asp:button>
								</td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td>&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<div id="div1" style="HEIGHT: 345px; OVERFLOW: auto">
							<asp:Literal runat="server" ID="ltStructure"></asp:Literal>
						</div>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
