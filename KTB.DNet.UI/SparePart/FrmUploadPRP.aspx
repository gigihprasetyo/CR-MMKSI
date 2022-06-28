<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadPRP.aspx.vb" Inherits="KTB.DNet.UI.SparePart.FrmUploadPRP" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmUploadPRP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function inProgress()
			{
				TtxtYear = document.getElementById("txtYear");
				TinFileLocation = document.getElementById("inFileLocation");
				if (TtxtYear.value == "")
				{	
					return;
				}
				if (TinFileLocation.value == "")
				{	
					return;
				}
				Form1.btnUpload.disabled=true;
				Form1.btnUpload.value="Harap tunggu";
				return;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" encType="multipart/form-data" >
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="3">PARTSHOP REWARD PROGRAM&nbsp;- Upload PRP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 22px" width="20%">Periode</TD>
					<TD style="HEIGHT: 22px" width="1%">:</TD>
					<td style="HEIGHT: 22px" vAlign="top"><asp:dropdownlist id="ddlMonth" runat="server"></asp:dropdownlist><asp:textbox id="txtYear" runat="server" Width="56px" MaxLength="4" onKeyPress="return numericOnlyUniv(event)"></asp:textbox>
						<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ControlToValidate="ddlMonth" ErrorMessage="*"
							Display="Dynamic"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvMonth" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="txtYear"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="reTahun" runat="server" Display="Dynamic" ErrorMessage="Tahun harus xxxx, x=angka"
							ControlToValidate="txtYear" ValidationExpression="\d\d\d\d"></asp:regularexpressionvalidator></td>
				</TR>
				<TR>
					<TD class="titleField" width="20%" style="HEIGHT: 14px">Kategory</TD>
					<TD width="1%" style="HEIGHT: 14px">:</TD>
					<TD vAlign="top" style="HEIGHT: 14px"><asp:dropdownlist id="ddlCategory" runat="server" Width="272px"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory"
							Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField">Lokasi File</TD>
					<TD>:</TD>
					<TD vAlign="top"><INPUT id="inFileLocation" onkeydown="return false;" style="WIDTH: 340px" type="file" name="File1"
							runat="server">
						<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="inFileLocation"
							Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 37px" vAlign="top">Deskripsi</TD>
					<TD style="HEIGHT: 37px" vAlign="top">:</TD>
					<TD style="HEIGHT: 37px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDescription" runat="server" Width="100%"
							MaxLength="255" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px">Upload By</TD>
					<TD style="HEIGHT: 14px">:</TD>
					<TD style="HEIGHT: 14px"><asp:label id="lblUploadBy" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px"></TD>
					<TD style="HEIGHT: 14px"></TD>
					<TD style="HEIGHT: 14px"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px"></TD>
					<TD style="HEIGHT: 14px"></TD>
					<TD style="HEIGHT: 14px"><INPUT id="btnUpload" type="button" value="Simpan" name="btnUpload" runat="server" onclick="inProgress();"
							style="WIDTH: 60px; HEIGHT: 21px"></TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
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
