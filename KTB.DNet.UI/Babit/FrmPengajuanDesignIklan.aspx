<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanDesignIklan.aspx.vb" Inherits="FrmPengajuanDesignIklan" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>BABIT - Pengajuan Design Iklan</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
			function ShowDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtStockKodeDealer = document.getElementById("txtDealerCode");
				txtStockKodeDealer.value = tempParam[0];
			}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">BABIT - Pengajuan Design Iklan</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD align="left">
                        <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" width="20%">Kode Dealer</TD>
                                <TD width="1%">:</TD>
                                <td width="80%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtDealerCode"
                                        onblur="omitSomeCharacter('txtDealerCode','<>?*%^():|\@#$;+=`~{}');" Width="80px" Runat="server"></asp:textbox>&nbsp;
                                    <asp:label id="lblSearchDealer" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
                            </TR>
                            <TR>
                                <TD class="titleField">Nama Iklan MKS</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" width="262"><asp:dropdownlist id="ddlNamaIklanKTB" runat="server" Width="200px"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField">Nama Iklan Dealer</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtNamaIklanDealer"
                                        onblur="omitSomeCharacter('txtDealerCode','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField">Upload Dokumen</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" vAlign="middle" width="262"><INPUT id="UploadFile" onkeydown="return false;" style="WIDTH: 267px; HEIGHT: 20px" type="file"
                                        size="25" name="File1" runat="server">
                                </TD>
                            </TR>
                            <TR vAlign="top">
                                <TD class="titleField">Keterangan</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" vAlign="middle" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtDesc"
                                        onblur="omitSomeCharacter('txtDealerCode','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server" Rows="4" TextMode="MultiLine"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 88px" width="88"></TD>
                                <TD width="1%"></TD>
                                <td style="WIDTH: 262px" width="262">
                                    <asp:button id="btnSave" runat="server" width="70px" Text="Upload"></asp:button>
                                    <input runat="server" type="button" style="width:70px;" value="Kembali" id="btnBack" onclick="window.history.back();return false;"
                                        NAME="btnBack">
                                </td>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
