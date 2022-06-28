<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTOPFacility.aspx.vb" Inherits="FrmTOPFacility" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTOPFacility</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<base target="_self">
		<script language="javascript" type="text/javascript">
		
		function ValidateRadioButton(obj)
		{
			var rbtnDate = document.getElementById("rbtnDate");
			var rbtnDay = document.getElementById("rbtnDay");
			var rbtnNone = document.getElementById("rbtnNone");
			rbtnDate.checked = false;
			rbtnDay.checked = false;
			rbtnNone.checked = false;
			obj.checked = true;			
		}
				
		function GetSelectedTOP()
		{
			var rbtnDate = document.getElementById("rbtnDate");
			var rbtnDay = document.getElementById("rbtnDay");
			var rbtnNone = document.getElementById("rbtnNone");
			var icMaxDate = document.getElementsByTagName(icMaxDate);
			//var icMaxDate = document.Form1.icMaxDate.value;
			var txtMaxDay = document.getElementById("txtMaxDay");
			var selectedTOP = ''
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				if(rbtnDate.checked)
				{
						alert(icMaxDate);
						selectedTOP = '0,' + icMaxDate.value + ',0';
						
				}
				else if(rbtnDay.checked)
				{
						selectedTOP = '1,' + icMaxDate.value + ',' + txtMaxDay.value;
						window.returnValue = selectedTOP;
				}
				else
				{
						selectedTOP = '-1,,';
						window.returnValue=selectedTOP;
				}
				
				window.returnValue = selectedTOP;
				
			}
			else
			{
			    if (rbtnDate.checked) {
			       
			        selectedTOP = '0,' + icMaxDate.value + ',0';

			    }
			    else if (rbtnDay.checked) {
			        selectedTOP = '1,' + icMaxDate.value + ',' + txtMaxDay.value;
			     
			    }
			    else {
			        selectedTOP = '-1,,';
			       
			    }
			    alert(selectedTOP);
			    opener.dialogWin.returnFunc(selectedTOP);
			     
			}
			window.close()
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%">
						<asp:label id="Label6" runat="server">Fasilitas TOP</asp:label></TD>
					<TD style="HEIGHT: 20px" width="1%">
						<asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="75%" nowrap>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD>
									<asp:radiobutton id="rbtnDate" runat="server" Text="s/d Tanggal" GroupName="TOP"></asp:radiobutton></TD>
								<TD>
									<cc1:inticalendar id="icMaxDate" runat="server" TextBoxWidth="70" FormatDate="dd-MMM-yyyy"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
					<TD style="HEIGHT: 20px" width="1%">
						<asp:label id="Label15" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 20px" width="75%">
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD>
									<asp:radiobutton id="rbtnDay" runat="server" Text="Hari" GroupName="TOP"></asp:radiobutton></TD>
								<TD>
									<asp:textbox id="txtMaxDay" runat="server" MaxLength="3" Width="100" onkeypress="return NumericOnlyWith(event,'');">1</asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 25px" width="24%"></TD>
					<TD style="HEIGHT: 25px" width="1%">
						<asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 25px" width="75%">
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD>
									<asp:radiobutton id="rbtnNone" runat="server" Text="Tidak Ada" GroupName="TOP" Checked="True"></asp:radiobutton></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center" style="HEIGHT: 20px" colspan="3">
						<asp:Button id="btnPilih" runat="server" Text="Pilih" Width="60px"></asp:Button>&nbsp;
						<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
