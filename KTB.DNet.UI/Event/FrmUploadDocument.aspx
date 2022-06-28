<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadDocument.aspx.vb" Inherits="FrmUploadDocument" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Maintenance Merek Kompetitor</title>
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
				showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtStockKodeDealer = document.getElementById("txtDealerCode");
				txtStockKodeDealer.value = tempParam[0];
			}
			function ShowEventSelection()
			{
				showPopUp('../General/../PopUp/PopUpEventMaster.aspx','',500,760,EventSelection);
			}
			function EventSelection(selectedEvent)
			{
				var txtEventNumber = document.getElementById("txtEventNumber");
				txtEventNumber.value = selectedEvent;			
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">EVENT - Upload Dokumen</td>
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
								<TD class="titleField" style="WIDTH: 88px" width="88">No Event</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtEventNumber"
										onblur="omitSomeCharacter('txtEventNumber','<>?*%^():|\@#$;+=`~{}');" runat="server" Width="200px"></asp:textbox>&nbsp;
									<asp:label id="lblSearchEvent" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtDealerCode"
										onblur="omitSomeCharacter('txtDealerCode','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server"></asp:textbox>&nbsp;
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
								</td>
							</TR>
							<TR>
								<TD class="titleField"  width="20%">Upload dokumen</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 262px" vAlign="middle" width="262"><INPUT id="UploadFile" onkeydown="return false;" style="WIDTH: 267px; HEIGHT: 20px" type="file"
										size="25" name="File1" runat="server">
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 88px" width="88"></TD>
								<TD width="1%"></TD>
								<td style="WIDTH: 262px" width="262"><asp:button id="btnSave" runat="server" width="70px" Text="Simpan"></asp:button>&nbsp;</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
