<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCreateTopic.aspx.vb" Inherits="FrmCreateTopic" smartNavigation="False"%>
<%@ Register TagPrefix="FCKeditorV2" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCreateTopic</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">FORUM&nbsp;- Topik Baru</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Forum</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="LblForum" runat="server">Label</asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Topik</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<td style="HEIGHT: 27px" width="75%"><asp:textbox id="txtTitle" runat="server" Width="448px" MaxLength="40" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtTitle','<>?*%$;')"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtTitle"></asp:RequiredFieldValidator></td>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Deskripsi</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
                                 
								<TD style="HEIGHT: 27px" width="75%"><FCKeditorV2:CKEditorControl id="txtDescription" BasePath="../UserControl/fckeditor/" runat="server" /></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px" width="24%">Attachment</TD>
								<TD style="HEIGHT: 16px" width="1%">:</TD>
								<TD style="HEIGHT: 16px" width="75%"><INPUT onkeypress="return false;" id="fileUpload" type="file" size="59" name="fileUpload"
										runat="server"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
								<TD style="HEIGHT: 27px" width="1%"></TD>
								<td style="HEIGHT: 27px" width="75%"><asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button><asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button><asp:textbox id="txtUserID" runat="server" Width="0px" ReadOnly="True" ForeColor="White" BorderStyle="None"></asp:textbox>
									<asp:Button id="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
