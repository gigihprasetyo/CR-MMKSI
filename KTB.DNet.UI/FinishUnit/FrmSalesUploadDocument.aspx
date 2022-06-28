<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesUploadDocument.aspx.vb" Inherits="FrmSalesUploadDocument" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
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
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',600,600,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCodeMultiple");
			txtDealerCodeSelection.value = selectedDealer;
			if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}
		}
		function ShowPPUserGroupSelection()
		{
			showPopUp('../PopUp/PopUpDitujukanKepadaUser.aspx?x=Territory','',500,760,UserGroupSelection);
		}
		function UserGroupSelection(selectedUserGroup)
		{
			var txtUserGroup= document.getElementById("txtKepada");
			txtUserGroup.value = selectedUserGroup;				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="1" cellpadding="2">
				<TR>
					<td colspan="3">
						<table width="100%" border="0" cellpadding="0" cellspacing="0">
							<TR>
								<TD class="titlePage">UMUM - Upload Sales Document</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
						</table>
					</td>
				</TR>
				<tr>
					<td width="20%" class="titleField">No Surat</td>
					<td width="1%">:</td>
					<td><asp:label id="lblNoSurat" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Tanggal</td>
					<td width="1%">:</td>
					<td><asp:label id="lblTanggal" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Departemen</td>
					<td width="1%">:</td>
					<td><asp:dropdownlist id="ddlDepartemen" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Ditujukan Kepada</td>
					<td width="1%">:</td>
					<td><asp:textbox id="txtKepada" runat="server" Width="194" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
							onblur="omitSomeCharacter('txtKepada','<>?*%$')"></asp:textbox>
						<asp:label id="lblSearchUserGroup" runat="server" onclick="ShowPPUserGroupSelection()">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Perihal</td>
					<td width="1%">:</td>
					<td><asp:textbox id="txtPerihal" runat="server" Width="194px"></asp:textbox></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Jenis Surat</td>
					<td width="1%">:</td>
					<td><asp:dropdownlist id="ddlJenisSurat" runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">Di Upload Oleh</td>
					<td width="1%">:</td>
					<td><asp:label id="lblUploadBy" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="20%" class="titleField">File</td>
					<td width="1%">:</td>
					<td><input id="UploadFile" type="file" runat="server">
					</td>
				</tr>
				<tr>
					<td width="20%"></td>
					<td width="1%"></td>
					<td><asp:button id="btnUpload" runat="server" Width="60px" Text="Upload"></asp:button><asp:button id="btnCancel" runat="server" Width="59px" Text="Batal"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
