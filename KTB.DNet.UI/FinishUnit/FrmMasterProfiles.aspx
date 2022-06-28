<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterProfiles.aspx.vb" Inherits="FrmMasterProfiles" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmMasterProfile</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script>
		    function RebindModel(model) {
		        var btnBindModel = document.getElementById("btnBindModel");
		        if (model == "0") {
		            var ddlJenis = document.getElementById("DDLIST86_7");
		        }
		        else if (model == "1") {
		            var ddlJenis = document.getElementById("DDLIST84_6");
		        }
		        else if (model == "2") {
		            var ddlJenis = document.getElementById("DDLIST82_5");
		        }
		        var txtNewKindID = document.getElementById("txtNewKindID");
		        txtNewKindID.value = ddlJenis.options[ddlJenis.selectedIndex].value;
		        btnBindModel.click();
		    }
		    function ChoosenModel(model) {
		        if (model == "0") {
		            var ddlModel = document.getElementById("DDLIST87_7");
		        }
		        else if (model == "1") {
		            var ddlModel = document.getElementById("DDLIST85_6");
		        }
		        else if (model == "2") {
		            var ddlModel = document.getElementById("DDLIST83_5");
		        }
		        var txtNewModelID = document.getElementById("txtNewModelID");
		        txtNewModelID.value = ddlModel.options[ddlModel.selectedIndex].value;

		    }

		    function DisableLeasing() {
		        var btnDisableLeasing = document.getElementById("btnDisableLeasing");
		        btnDisableLeasing.click();
		    }

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="1"
				cellPadding="1" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 17px">
									<asp:Label id="lblHeader" runat="server">FAKTUR KENDARAAN - Customer Profile</asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePanel">
									<asp:Label id="lblCV" runat="server">Profile CV</asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD>
						<asp:Panel id="pnlMasterProfileCV" runat="server"></asp:Panel></TD>
				</TR>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePanel">
									<asp:Label id="lblPC" runat="server">Profile PC</asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD>
						<asp:Panel id="pnlMasterProfilePC" runat="server"></asp:Panel></TD>
				</TR>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePanel">
									<asp:Label id="lblLCV" runat="server">Profile LCV</asp:Label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD>
						<asp:Panel id="pnlMasterProfileLCV" runat="server"></asp:Panel></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button>
						<asp:Button id="BtnTutup" runat="server" Text="Tutup" CausesValidation="False"></asp:Button>
						<asp:button id="btnBindModel" runat="server" Text="Bind Model" CausesValidation="False" style="DISPLAY:none"></asp:button>
						<asp:TextBox id="txtNewKindID" runat="server" style="DISPLAY:none">0</asp:TextBox>
						<asp:TextBox id="txtNewModelID" runat="server" style="DISPLAY:none">0</asp:TextBox>
            <asp:button id="btnDisableLeasing" runat="server" Text="Disable Leasing" CausesValidation="False" style="DISPLAY:none"></asp:button>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
