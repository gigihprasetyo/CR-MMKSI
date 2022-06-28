<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadAnnualDiscount.aspx.vb" Inherits="FrmUploadAnnualDiscount" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ANNUAL DISCOUNT - Upload PDF Annual Discount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = tempParam[0];			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="WIDTH: 731px; HEIGHT: 179px" cellSpacing="1" cellPadding="2"
				width="731" border="0">
				<TR>
					<td style="HEIGHT: 38px" colSpan="10">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">ANNUAL DISCOUNT - Upload&nbsp;File Annual Discount</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 116px" width="200">Tipe</TD>
					<TD style="WIDTH: 51px" width="51">:</TD>
					<TD style="WIDTH: 75px" width="75" colSpan="3">
						<asp:dropdownlist id="ddlTipe" runat="server" AutoPostBack="True" Width="144px">
							<asp:ListItem Value="Umum">Umum</asp:ListItem>
							<asp:ListItem Value="Per Dealer" Selected="True">Per Dealer</asp:ListItem>
							<asp:ListItem Value="Per Group">Per Group</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 116px" width="116">
						Pilih&nbsp;Tanggal&nbsp;&nbsp;</TD>
					<TD style="WIDTH: 51px" width="51">:</TD>
					<TD style="WIDTH: 75px" width="75" colSpan="3">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td><cc1:inticalendar id="intiFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<TD>&nbsp;s/d&nbsp;</TD>
								<TD><cc1:inticalendar id="intiTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<TD class="titleField" style="WIDTH: 77px" width="77">Dokumen :&nbsp;</TD>
					<td>:</td>
					<TD style="WIDTH: 63px" width="63"><asp:dropdownlist id="ddlProgramName" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
					<td>
						<asp:Button id="btnShowAllProgram" runat="server" Text="cari" Width="60px" ToolTip="Tampilkan Dokumen Yang Tersedia"></asp:Button></td>
				</tr>
				<tr>
					<TD class="titleField" style="WIDTH: 116px" width="116"><asp:label id="Label4" runat="server">Nama File</asp:label></TD>
					<td style="WIDTH: 51px" width="51">
						<P>:</P>
					</td>
					<TD style="WIDTH: 264px" width="264" colSpan="5">
						<asp:label id="lblFileName" runat="server"></asp:label></TD>
					<TD style="WIDTH: 144px" width="144"></TD>
					<TD style="WIDTH: 77px" width="77"><STRONG>
							<asp:Label id="lblGroup" runat="server" Visible="False">Group :</asp:Label></STRONG></TD>
					<TD width="75%">
						<asp:DropDownList id="ddlGroup" runat="server" Visible="False"></asp:DropDownList></TD>
				</tr>
				<TR>
					<TD class="titleField" style="WIDTH: 116px"><asp:label id="Label3" runat="server" Font-Bold="True">Lokasi File :</asp:label></TD>
					<td style="WIDTH: 51px">:</td>
					<TD colSpan="8">&nbsp;<INPUT onkeypress="return false" id="FileEnum" style="WIDTH: 384px; HEIGHT: 20px" type="file"
							size="44" name="File1" runat="server">
						<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>
					</TD>
				</TR>
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
