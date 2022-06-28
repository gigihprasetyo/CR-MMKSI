<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFleetRequest.aspx.vb" Inherits="FrmFleetRequest" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFleetRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				var txtDealerName = document.getElementById("lblDealerVal");
				txtDealerSelection.value = tempParam[0];
				txtDealerName.innerHTML = tempParam[1];
			}
			function GetCurrentInputIndex(GridName)
				{
				var dtgDamageCode = document.getElementById(GridName);
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgDamageCode.rows.length; index++)
				{
					inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}				
				return -1;
			}
			
			function GetSelectedDamageCode(selectedCode)
			{
				var indek = GetCurrentInputIndex("dgKerusakan");
				var dtgDamageCode = document.getElementById("dgKerusakan");
				var tempParam = selectedCode.split(';');
				var KodeDamage = dtgDamageCode.rows[indek].getElementsByTagName("INPUT")[0];
				var DescDamage = dtgDamageCode.rows[indek].getElementsByTagName("SPAN")[1];

				KodeDamage.value = tempParam[0];				
				DescDamage.innerHTML = tempParam[1];				

			}
			
			function GetSelectedPartsCode(selectedCode)
			{
				var indek = GetCurrentInputIndex("dgParts");
				var dtgPartsCode = document.getElementById("dgParts");
				var tempParam = selectedCode.split(';');
				var KodeParts = dtgPartsCode.rows[indek].getElementsByTagName("INPUT")[0];
				var DescParts = dtgPartsCode.rows[indek].getElementsByTagName("SPAN")[1];

				KodeParts.value = tempParam[0];				
				DescParts.innerHTML = tempParam[1];				

			}

			function ShowPopUp()
			{
			}
			
			function BackButton()
			{
				//var ret = (parseInt(document.getElementById("hid_History").value) + 1)* (-1)
				//document.getElementById("btnBack").disabled=true
				//history.go(ret)
				document.location.href="../SparePart/FrmPQRList.aspx";
			}
			function focusSave()
			{
			document.getElementById("btnSimpan").focus();			
			}
			
			function setLastPos(lPosID)
			{
				var hiddenField = document.getElementById("hfLastPostId");
				hiddenField.value = lPosID;
			}
		</script>
	    <style type="text/css">
            .auto-style1 {
                font-weight: normal;
            }
        </style>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<INPUT id="hfLastPostId" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" size="1">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PENGAJUAN EXTENDED FREE SERVICE</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table cellSpacing="1" cellPadding="4" width="764" border="0">
				<tr vAlign="top">
					<td width="50%">
						<table cellSpacing="1" cellPadding="2" width="360">
							<TR>
								<TD class="titleField" width="40%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD class="auto-style1"><asp:label id="lblDealerCode" runat="server" Width="140px"></asp:label>
							    </TD>
							</TR>
							<tr>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD class="auto-style1"><asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server"></asp:label></TD>
							</tr>
							<TR>
								<TD class="titleField"><asp:label id="lblDealer" Runat="server">No Surat MFTBC</asp:label></TD>
								<TD>:</TD>
								<TD width="60%">
								    <asp:label id="lblFleetNumber" Runat="server"></asp:label>
									<asp:dropdownlist id="ddlFleetNumber" tabIndex="1" Runat="server"></asp:dropdownlist>
								</TD>
							</TR>
							<tr>
								<td class="titleField">No Extended Free Service</td>
								<td valign="top">:</td>
								<td valign="top"><asp:label id="lblReqNum" Runat="server">[AutoNumber]</asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label  Runat="server">Tanggal Pengajuan</asp:label></td>
								<td>:</td>
								<td><cc1:inticalendar id="icTglPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglPengajuan" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblTglPembuatan" Runat="server">Nama Konsumen</asp:label></td>
								<td>:</td>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNamaKonsumen" onblur="omitSomeCharacter('txtNoChasis','<>?*%$;')"
										Runat="server" Width="160px"></asp:textbox><asp:label id="lblNamaKonsumen" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblNoChasis" Runat="server">Status Konsumen</asp:label></td>
								<td>:</td>
								<td><asp:dropdownlist id="ddlStatusKonsumen" tabIndex="1" Runat="server"></asp:dropdownlist><asp:label id="lblStatusKonsumen" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField"><asp:label id="lblNoMesin" Runat="server">Profil Bisnis</asp:label></td>
								<td style="HEIGHT: 16px">:</td>
								<td style="HEIGHT: 16px"><asp:dropdownlist id="ddlProfilBisnis" tabIndex="1" Runat="server"></asp:dropdownlist><asp:label id="lblProfilBisnis" Runat="server"></asp:label></td>
							</tr>
							<TR>
								<TD class="titleField" valign="top">Kebutuhan Unit<br />
                                    <span class="auto-style1">(jenis,model dan jumlah)</span></TD>
								<TD valign="top">:</TD>
								<TD valign="top"><asp:textbox id="txtKebutuhanUnit" tabIndex="0" Runat="server" Width="209" TextMode="MultiLine" Height="60px"></asp:textbox><asp:label id="lblKebutuhanUnit" Runat="server"></asp:label></TD>
							</TR>
							</table>
						<br>
					</td>
					<td vAlign="top" width="50%">
                        <table cellSpacing="1" cellPadding="2" width="360" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 126px"><asp:label id="Label1" Runat="server">Mulai Pengadaan</asp:label></TD>
								<TD>:</TD>
								<TD width="34%">
									<cc1:inticalendar id="icTglMulaiPengadaan" runat="server" TextBoxWidth="70"></cc1:inticalendar>
								    <asp:label id="lblTglMulaiPengadaan" Runat="server"></asp:label>
								</TD>
							</TR>
							<tr>
								<td class="titleField" style="WIDTH: 126px">Selesai Pengadaan</td>
								<td>:</td>
								<td><cc1:inticalendar id="icTglSelesaiPengadaan" runat="server" TextBoxWidth="70"></cc1:inticalendar><asp:label id="lblTglSelesaiPengadaan" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px"><asp:label id="Label4" Runat="server">Status</asp:label></td>
								<td>:</td>
								<td><asp:label id="lblStatus" Runat="server" Font-Size="8pt" Width="196px">Baru</asp:label></td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 126px" valign="top"><asp:label id="Label6" Runat="server">Lampiran Berkas</asp:label></td>
								<td valign="top">:</td>
								<td><INPUT id="file1" type="file" size="25" runat="server" onkeypress="return false;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="linkAttachment" runat="server"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton id="linkDeleteAttachment" runat="server" CausesValidation="False" CommandName="Delete" Visible="False">
												<img src="../images/remove.gif" border="0" alt="Delete Attachment"  onclick="return confirm('Apakah anda akan menghapus Attachment ini ?')"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>

								</td>
							</tr>
							</table>
						<br>
					</td>
				</tr>
				<tr vAlign="top">
					<td style="WIDTH: 398px">&nbsp;</td>
					<td>&nbsp;</td>
				<tr>
					<td align="center" colSpan="2"><asp:button id="btnSimpan" tabIndex="0" Runat="server" Text="Simpan"></asp:button>&nbsp;&nbsp;<asp:Button ID="btnBatal2" runat="server" Text="Batal" />
&nbsp;&nbsp;
						<asp:button id="btnKembali" Runat="server" Text="Kembali"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnStatusChange" Runat="server" Visible="False"></asp:button>&nbsp;&nbsp;
						&nbsp;&nbsp;
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
		</form>
		</SPAN>
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
	</BODY>
</HTML>
