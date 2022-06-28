<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmConfirmInfoEvent.aspx.vb" Inherits="FrmConfirmInfoEvent" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmConfirmEvent</title>
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
			function ShowPPEventInfoSelection()
			{
				showPopUp('../PopUp/PopUpEventInfo.aspx','',500,760,EventInfoSelection);
			}
					
			function EventInfoSelection(SelectedValue)
			{
				var tempParam= SelectedValue;
				var lblEventProposeNo = document.getElementById("txtEventProposeNo");
				lblEventProposeNo.value = tempParam;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">EVENT&nbsp;- Konfirmasi EVENT</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="20"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 174px; HEIGHT: 21px" vAlign="top">No Pengajuan 
									Event</TD>
								<TD style="WIDTH: 2px; HEIGHT: 21px">:</TD>
								<TD class="titleField" style="HEIGHT: 21px">
									<asp:TextBox id="txtEventProposeNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEventProposeNo','<>?*%$;')"
										Runat="server"></asp:TextBox>
									<asp:LinkButton id="lnkbtnPopUp" runat="server">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:LinkButton></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblDealer" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Jenis Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblEventType" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Jadwal Fix Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><table border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><CC1:INTICALENDAR id="icEventDateFrom" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><CC1:INTICALENDAR id="icEventDateTo" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Tempat Acara&nbsp;</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox id="txtEventLocation" Runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Jumlah Undangan</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtInvitationQty" Runat="server"
										MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">TotalBiaya</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtTotalCost" Runat="server"
										MaxLength="11"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Upload File Perkiraan 
									Biaya</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><input id="fuEstimatedCostFile" type="file" name="fuEstimatedCostFile" runat="server" style="WIDTH: 304px; HEIGHT: 20px"
										size="31">
									<asp:button id="btnUpload" Runat="server" Text="Upload" Visible="False"></asp:button></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="WIDTH: 174px" vAlign="top">Komentar Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:textbox id="txtDealerComment" Runat="server" TextMode="MultiLine" Width="356px" Height="102px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 174px" vAlign="top"></TD>
								<TD style="WIDTH: 2px"></TD>
								<TD class="titleField"><asp:button id="btnSimpan" runat="server" Text="Simpan" Width="72px"></asp:button><asp:button id="btnCancel" runat="server" Text="Kembali" Width="72px" Visible="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
