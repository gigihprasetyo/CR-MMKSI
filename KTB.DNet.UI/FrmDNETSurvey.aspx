<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDNETSurvey.aspx.vb" Inherits="FrmDNETSurvey"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>D-NET Survey</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="./WebResources/PreventNewWindow.js"></script>
		<LINK href="WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="2" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" vAlign="middle" colSpan="5">D-NET - Survey</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="5">
						<asp:Label id="lblErrors" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="middle" colSpan="5">1. Informasi Anda</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Dealer</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">[<asp:Label id="lblDealerCode" runat="server"></asp:Label>]&nbsp;-&nbsp;<asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">UserName</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:Label id="lblUserName" runat="server"></asp:Label>
						<asp:Label id="lblID" runat="server" Visible="False"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="middle" colSpan="5">2. Informasi Kontak</TD>
				</TR>
				<TR>
					<TD vAlign="middle"></TD>
					<TD class="titleField" vAlign="middle" colSpan="4">a. Kepala Cabang</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Nama</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtNamaKepalaCabang" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvNamaKepalaCabang" runat="server" ErrorMessage="Nama Kepala Cabang Harus Diisi"
							ControlToValidate="txtNamaKepalaCabang"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">E-Mail</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtEMailKepalaCabang" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvEMailKepalaCabang" runat="server" ErrorMessage="E-mail Kepala Cabang Harus Diisi"
							ControlToValidate="txtEMailKepalaCabang"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEMailKepalaCabang" runat="server" ErrorMessage="Alamat email Kepala Cabang tidak valid"
							ControlToValidate="txtEMailKepalaCabang" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="middle"></TD>
					<TD class="titleField" vAlign="middle" colSpan="4">b. Sales Manager</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Nama</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtNamaSalesManager" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvNamaSalesManager" runat="server" ErrorMessage="Nama Sales Manager Harus Diisi"
							ControlToValidate="txtNamaSalesManager"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">E-Mail</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtEMailSalesManager" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvEMailSalesManager" runat="server" ErrorMessage="E-mail Sales Manager Harus Diisi"
							ControlToValidate="txtEMailSalesManager"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEMailSalesManager" runat="server" ErrorMessage="Alamat email Sales Manager tidak valid"
							ControlToValidate="txtEMailSalesManager" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="middle"></TD>
					<TD class="titleField" vAlign="middle" colSpan="4">c. Service Manager</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Nama</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtNamaServiceManager" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvNamaServiceManager" runat="server" ErrorMessage="Nama Service Manager Harus Diisi"
							ControlToValidate="txtNamaServiceManager"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">E-Mail</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtEMailServiceManager" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvEMailServiceManager" runat="server" ErrorMessage="E-mail Service Manager Harus Diisi"
							ControlToValidate="txtEMailServiceManager"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEMailServiceManager" runat="server" ErrorMessage="Alamat email Service Manager tidak valid"
							ControlToValidate="txtEMailServiceManager" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="middle"></TD>
					<TD class="titleField" vAlign="middle" colSpan="4">d. Sparepart Manager</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Nama</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top"><asp:textbox id="txtNamaSparepartManager" runat="server" MaxLength="50" Columns="30"></asp:textbox><asp:requiredfieldvalidator id="rfvNamaSparepartManager" runat="server" ErrorMessage="Nama Sparepart Manager Harus Diisi"
							ControlToValidate="txtNamaSparepartManager"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">E-Mail</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:TextBox id="txtEMailSparepartManager" runat="server" MaxLength="50" Columns="30"></asp:TextBox>
						<asp:RequiredFieldValidator id="rfvEMailSparepartManager" runat="server" ErrorMessage="E-mail Sparepart Manager Harus Diisi"
							ControlToValidate="txtEMailSparepartManager"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="revEMailSparepartManager" runat="server" ErrorMessage="Alamat email Sparepart Manager tidak valid"
							ControlToValidate="txtEMailSparepartManager" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="middle" colSpan="5">3. Saran dan Masukan Terhadap 
						Help Desk</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Help desk officer menjawab dengan ramah</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:RadioButtonList id="optOfficer" runat="server" RepeatDirection="Horizontal" DataTextField="Text"
							DataValueField="Value"></asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Solusi yang diberikan membantu menyelesaikan 
						masalah</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:RadioButtonList id="optSolusi" runat="server" RepeatDirection="Horizontal" DataTextField="Text"
							DataValueField="Value"></asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Jawaban/solusi yang dikirimkan relatif cepat</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:RadioButtonList id="optJawaban" runat="server" RepeatDirection="Horizontal" DataTextField="Text"
							DataValueField="Value"></asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Saran lainnya</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:TextBox id="txtSaran" runat="server" Columns="50" TextMode="MultiLine" Rows="7"></asp:TextBox>
						<asp:requiredfieldvalidator id="rfvSaran" runat="server" ErrorMessage="Saran Harus Diisi" ControlToValidate="txtSaran"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="middle" colSpan="5">4. Fitur tambahan di D-NET<BR>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">
						Modul yang tersedia sudah memadai</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:RadioButtonList id="optModul" runat="server" RepeatDirection="Horizontal" DataTextField="Text" DataValueField="Value"></asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">
						Akses saat browsing D-NET cepat</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:RadioButtonList id="optAkses" runat="server" RepeatDirection="Horizontal" DataTextField="Text" DataValueField="Value"></asp:RadioButtonList></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Modul yang dirasa lambat saat diakses adalah</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:textbox id="txtModul" runat="server" MaxLength="255" Columns="50"></asp:textbox>
						<asp:requiredfieldvalidator id="rfvModul" runat="server" ErrorMessage="Modul Yang Dirasa Lambat Harus Diisi"
							ControlToValidate="txtModul"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top"></TD>
					<TD vAlign="top"></TD>
					<TD class="titleField" vAlign="top">Menurut Bapak/Ibu fitur atau modul apa yang 
						perlu ditambahkan di D-NET</TD>
					<TD vAlign="top" align="center">:</TD>
					<TD vAlign="top">
						<asp:TextBox id="txtFitur" runat="server" Columns="50" TextMode="MultiLine" Rows="7"></asp:TextBox>
						<asp:requiredfieldvalidator id="rfvFitur" runat="server" ErrorMessage="Fitur Harus Diisi" ControlToValidate="txtFitur"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="5">
						<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
