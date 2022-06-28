<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPendingOrder.aspx.vb" Inherits="FrmPendingOrder" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPendingOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}
			
			function Collapse(id, idImg)
			{
				var el = document.getElementById(id);				
				if (el.style.display=='block')
				{		
					document.getElementById(idImg).src = "../images/plus.gif"
					el.style.display='none'
				}
				else
				{
					document.getElementById(idImg).src = "../images/minus.gif"
					el.style.display='block'
				}
			}
			
			function ShowPODetail(POID)
			{
				showPopUp('../PopUp/PopUpSPPODetail.aspx?poid=' + POID, '', 510, 700, SparePartPO);
			}
			
			function SparePartPO()
			{
			}
			
			function ShowSODetail(SOID)
			{
				showPopUp('../SparePart/FrmPurchaseOrderEstimateDetail.aspx?isFromPO=Yes&POID=' + SOID, '', 510, 700, SODetail);
			}
			function SODetail() {}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">Pending Order</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox id="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"  ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
									<asp:Label id="lblDealer" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Tipe Order</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:dropdownlist id="ddlOrderType" runat="server"></asp:dropdownlist></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="WIDTH: 146px">Tipe Dokumen</TD>
								<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="cmbDocumentType" runat="server" Width="140px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Pending s/d</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><CC1:INTICALENDAR id="icDate" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px"></TD>
								<TD style="WIDTH: 2px"></TD>
								<TD class="titleField"><asp:dropdownlist id="ddlStatus" runat="server" Visible="False"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px"></TD>
								<TD style="WIDTH: 2px"></TD>
								<TD class="titleField"><asp:button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Literal ID="ltrTable" Runat="server" />
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
