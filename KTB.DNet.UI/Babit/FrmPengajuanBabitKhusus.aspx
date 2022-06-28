<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanBabitKhusus.aspx.vb" Inherits="FrmPengajuanBabitKhusus" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmPengajuanBabitKhusus</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">			
        
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var data = selectedDealer.split(";");
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = data[0];
			var lblDealerName = document.getElementById("lblDealerName");
			lblDealerName.innerHTML = data[1];
		}
        
			function DisplayJenisKegiatan()
			{
				var jenis  = document.getElementById('ddlJenisKegiatan');				
				var hdnPameran = document.getElementById('hdnPameran');
				var hdnIklan = document.getElementById('hdnIklan');
				var hdnEvent = document.getElementById('hdnEvent');
				var hdnPameranSubmit = document.getElementById('hdnPameranSubmit');
				var hdnIklanSubmit = document.getElementById('hdnIklanSubmit');
				var hdnEventSubmit = document.getElementById('hdnEventSubmit');
				var btnSave = document.getElementById('btnSave');
				var btnSubmit = document.getElementById('btnSubmit');
				var NoPengajuan = document.getElementById('lblNoPengajuan');
				
				if (jenis.value == '0') {
					if (hdnPameran.value != '') {
						NoPengajuan.innerHTML=hdnPameran.value;
						btnSave.disabled=true;
						btnSubmit.disabled=false;
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						btnSubmit.disabled=true;
					}
					if (hdnPameranSubmit.value != '') {
						btnSubmit.disabled=true;
					}
				}
				else if (jenis.value == '1') {
					if (hdnIklan.value != '') {
						NoPengajuan.innerHTML=hdnIklan.value;
						btnSave.disabled=true;
						btnSubmit.disabled=false;
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						btnSubmit.disabled=true;
					}
					if (hdnIklanSubmit.value != '') {
						btnSubmit.disabled=true;
					}
				}
				else if (jenis.value == '2') {
					if (hdnEvent.value != '') {
						NoPengajuan.innerHTML=hdnEvent.value;
						btnSave.disabled=true;
						btnSubmit.disabled=false;
					}
					else {
						NoPengajuan.innerHTML='Auto Generated';
						btnSave.disabled=false;
						btnSubmit.disabled=true;
					}
					if (hdnEventSubmit.value != '') {
						btnSubmit.disabled=true;
					}
				}
			}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <TR>
                    <TD class="titlePage">BABIT - Pengajuan BABIT Khusus</TD>
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
                                <TD class="titleField" style="WIDTH: 146px">No Babit Khusus</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD>
                                    <asp:TextBox id="txtNoPersetujuan" runat="server" MaxLength="30"></asp:TextBox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">No Surat Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox id="txtNoSuratDealer" runat="server"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Kode Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" Width="128px" ToolTip="Dealer Search 1"></asp:textbox>&nbsp;
                                    <asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;
                                    <asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label>&nbsp;
                                    <asp:button id="btnFindDealer" runat="server" Text="Find"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Nama Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblDealerName" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Kota</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblCity" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Propinsi</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblProvince" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Nomor Alokasi</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblNoPerjanjian" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Periode</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblPeriode" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Sisa Alokasi Babit</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:label id="lblSisaAlokasiBabit" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Jenis Kegiatan</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:dropdownlist id="ddlJenisKegiatan" runat="server" Width="90px" onchange="DisplayJenisKegiatan();"></asp:dropdownlist><asp:label id="lblJenisKegiatan" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Dana Babit Khusus</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtDana" onblur="omitSomeCharacter('txtDana','-')"
                                        onkeyup="pic(this,this.value,'9999999999','N')" runat="server" Width="90px" MaxLength="12"></asp:textbox><asp:label id="lblDana" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 146px">Upload File</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD><input id="fuBabitKhusus" type="file" runat="server">
                                    <asp:label id="lblUploadFile" runat="server"></asp:label></TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
            <asp:Button id="btnNew" runat="server" Text="Baru"></asp:Button>
            <asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>
            <asp:button id="btnSubmit" runat="server" Enabled="False" Text="Validasi"></asp:button>
            <INPUT id="btnBack" type="button" value="Kembali" runat="server" NAME="btnBack" onclick="window.history.back();">
            <input id="hdnPameran" type="hidden" name="hdnPameran" runat="server"> <input id="hdnEvent" type="hidden" name="hdnEvent" runat="server">
            <input id="hdnIklan" type="hidden" name="hdnIklan" runat="server"> <input id="hdnPameranSubmit" type="hidden" name="hdnPameranSubmit" runat="server">
            <input id="hdnEventSubmit" type="hidden" name="hdnEventSubmit" runat="server"> <input id="hdnIklanSubmit" type="hidden" name="hdnIklanSubmit" runat="server">
            <INPUT id="hdnValNew" type="hidden" value="-1" name="hdnValNew" runat="server"> <INPUT id="hdnValSubmit" type="hidden" value="-1" name="hdnValSubmit" runat="server">
            <asp:panel id="pnlScript" Runat="server">
                <SCRIPT type="text/javascript">
		            DisplayJenisKegiatan();
                </SCRIPT>
            </asp:panel>
        </form>
    </body>
</HTML>
