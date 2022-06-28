<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEntryStockMovement.aspx.vb" Inherits="PopUpEntryStockMovement"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Transfer Stock</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);	
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam= selectedDealer.split(';');
			var txtDealerSelection = document.getElementById("txtAllocateDealer");
			txtDealerSelection.value = tempParam[0];
			var lblAlamatAllocateDealer = document.getElementById("lblAlamatAllocateDealer");
			lblAlamatAllocateDealer.innerHTML = tempParam[1];
			var lblSearchTerm1AllocateDealer = document.getElementById("lblSearchTerm1AllocateDealer");
			lblSearchTerm1AllocateDealer.innerHTML = tempParam[2];
			lblSearchTerm1AllocateDealer.style.di = "none";
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td class="titlePage">Transfer Stok</td>
						</tr>
						<tr>
							<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
						</tr>
						<tr>
							<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
						</tr>
						</table>		
					
					</td></tr>
				<tr>
					<td width="100%">
					
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titlefield" width="20%" nowrap>Transfer No Rangka</td>
								<td width="1%">:</td>
								<td width="79%"><asp:label id="lblNoRangka" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titlefield">Dari Dealer</td>
								<td>:</td>
								<td><asp:Label ID="lblStockDealer" Runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td class="titlefield"></td>
								<td></td>
								<td><asp:Label ID="lblAlamatStockDealer" Runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td class="titlefield">Ke Dealer</td>
								<td>:</td>
								<td>
									<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" onblur="omitSomeCharacter('txtAllocateDealer','<>?*%^():|\@#$;+=`~{}');"
										ID="txtAllocateDealer" Runat="server"></asp:TextBox>
									<asp:label id="lblSearchStockDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
								</td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td><asp:Label ID="lblAlamatAllocateDealer" Runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td><asp:Label ID="lblSearchTerm1AllocateDealer" Runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td>
								<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button>&nbsp; <INPUT id="btnTutup" onclick="window.close();" type="button" value="Tutup">
								</td></tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
