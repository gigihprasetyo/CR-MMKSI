<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmInsertEvent.aspx.vb" Inherits="FrmInsertEvent" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDepositA</title>
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
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
					
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam[0];
			}
			
			function ShowPPNoEvent()
			{
				showPopUp('../PopUp/PopUpEventNo.aspx','',500,760,NoEventSelection);
			}
					
			function NoEventSelection(selected)
			{
				var tempParam= selected.split(';');
				var txtNoEventSelection = document.getElementById("txtEventNo");
				var lblPeriodeSelection = document.getElementById("lblPeriode");
				var hdn = document.getElementById("hdn");
				txtNoEventSelection.value = tempParam[0];
				hdn.value = tempParam[1];
				lblPeriodeSelection.innerHTML = tempParam[1];
			}
			
			function ShowPPBabitAllocation()
			{
				var txtKodeDealer = document.getElementById("txtKodeDealer");
				if (txtKodeDealer.value == '')
				{
					alert('Kode Dealer harus diisi dahulu');
					return;
				}
				else
				{
					var myDate = new Date( );
					showPopUp('../PopUp/PopUpBabitAlocation.aspx?DealerCode='+ txtKodeDealer.value +'&time='+ myDate.getTime( ),'',500,760,BabitSelection);
				}
			}
					
			function BabitSelection(selected)
			{
				var tempParam= selected
				var txtSelection = document.getElementById("txtBabitAlocation");
				txtSelection.value = tempParam;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">EVENT&nbsp;- Info EVENT</TD>
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
								<TD class="titleField" style="WIDTH: 146px">No Pengajuan Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:label id="lblNo" runat="server">Auto Generated</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">No Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtEventNo" runat="server"></asp:textbox>
                                    <asp:label id="lblEventNo" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                    <asp:label id="lblNoEvent" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Periode</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:label id="lblPeriode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server"></asp:textbox>
                                    <asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
                                    <asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Jenis Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:dropdownlist id="ddlJenisEvent" runat="server"></asp:dropdownlist><asp:label id="lblEventType" runat="server"></asp:label></TD>
							</TR>
							<TR valign=top>
								<TD class="titleField" style="WIDTH: 146px">Jadwal Event</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD>
									<TABLE id="Table3" cellpadding=0 cellspacing=0 border=0>
										<TR>
											<TD><CC1:INTICALENDAR id="icDateFrom" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR><asp:label id="lblJadwalEventFrom" runat="server"></asp:label></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><CC1:INTICALENDAR id="icDateUntil" runat="server" TextBoxWidth="70"></CC1:INTICALENDAR><asp:label id="lblJadwalEventUntil" runat="server"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Jumlah Undangan</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtSum" runat="server" MaxLength="9"></asp:textbox><asp:label id="lblAudience" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Alokasi Biaya</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtBabitAlocation" onkeyup="pic(this,this.value,'9999999999','N')"
										runat="server"></asp:textbox><asp:label id="lblBabitAlocation" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Persetujuan Biaya</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtPersetujuanBiaya" onkeyup="pic(this,this.value,'9999999999','N')"
										runat="server"></asp:textbox><asp:label id="lblPersetujuanBiaya" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Area Koordinator/PIC</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD><asp:textbox id="txtArea1" runat="server"></asp:textbox><asp:label id="lblArea1" runat="server"></asp:label>&nbsp;Management 
									MMKSI / Observer :&nbsp;
									<asp:textbox id="txtArea2" runat="server"></asp:textbox><asp:label id="lblArea2" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px"></TD>
								<TD style="WIDTH: 2px"></TD>
								<TD><asp:button id="btnSave" runat="server" Text="Simpan" Width="56px"></asp:button><asp:button id="btnCancel" runat="server" Text="Kembali"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><input id="hdn" type="hidden" runat="server">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
