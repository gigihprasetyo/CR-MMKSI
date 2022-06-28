<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmNewTrTraineeStatus.aspx.vb" Inherits="FrmNewTrTraineeStatus" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmNewTrTraineeStatus</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 0px; WIDTH: 800px; POSITION: absolute; TOP: 0px; HEIGHT: 100px"
				cellSpacing="0" cellPadding="0" width="801" border="0">
				<TR>
					<TD class="titlePage" style="WIDTH: 825px; HEIGHT: 6px">Training - Status 
						Pendaftaran</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px; HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 825px" height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px; HEIGHT: 50px" vAlign="top">
						Terima kasih, permohonan pendaftaran data siswa baru telah masuk ke sistem.<br>
						Selanjutnya proses review oleh Training Center MMKSI untuk menyetujui permohonan 
						Anda.
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px; HEIGHT: 10px" vAlign="top" align="center">
						<asp:Button id="btnOk" runat="server" Text="OK" Width="57px" CausesValidation="False"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
