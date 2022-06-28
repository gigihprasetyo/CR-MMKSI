<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmReportCSPerformance.aspx.vb" Inherits=".FrmReportCSPerformance" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Maintenance Tipe Kompetitor</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function ShowHistory(formula) {
            showPopUp('../PopUp/PopUpCcCalculationHistory.aspx?formula=' + formula, '', 500, 760, DealerSelection);
        }

        function ShowDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function ShowDealerSelectionGroup(groupId) {
            showPopUp('../PopUp/PopUpDealerSelection.aspx?Group=' + groupId, '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerCode = document.getElementById("txtDealerCode");
            txtDealerCode.value = tempParam;
        }
        function DownloadCcReport(fullPath) {
            //window.open("../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath, "","top=0,location=0,status=0, scrollbars=0,width=1px,height=1px");
            document.getElementById("fraDownload").src = "../PopUp/PopUpDownloadCcReport.aspx?file=" + fullPath;
        }

        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }



    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="FrmSalesmanLevel" method="post" runat="server">
        <iframe id="fraDownload" runat="server" width="0" height="0" style="display: none"></iframe>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CS Performance Report</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">Dealer</td>
                            <td width="1%">:</td>
                            <td width="80%">
                                <asp:TextBox onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    runat="server" Width="230px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:Label>
                                <asp:RequiredFieldValidator ID="valDealer" Enabled="false" runat="server" ControlToValidate="txtDealerCode" Display="Dynamic" ErrorMessage="* Dealer harus dipilih"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <!--
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<td width="60%"><asp:label id="lblDealerName" runat="server" EnableViewState="True"></asp:label></td>
							</TR>
							-->
                        <%--	<TR>
								<TD class="titleField">&nbsp;</TD>
								<TD width="1%">:</TD>
								<TD width="80%">&nbsp;</TD>
							</TR>
							
							<TR vAlign="top">
								<TD class="titleField">&nbsp;</TD>
								<TD width="1%">:</TD>
								<td width="60%">
									&nbsp;</td>
							</TR>--%>
                        <tr>
                            <td class="titleField">Periode</td>
                            <td width="1%">:</td>
                            <td width="80%" valign="middle">
                                <cc1:IntiCalendar ID="icPeriod" runat="server" CanPostBack="true"></cc1:IntiCalendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Formula</td>
                            <td width="1%">:</td>
                            <td width="80%" valign="middle">
                                <asp:DropDownList ID="ddlFormula" runat="server"></asp:DropDownList>
                                &nbsp;
                                <asp:LinkButton ID="lbtnPopupHistory" runat="server" Text="View History"></asp:LinkButton>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td width="60%">
                                <asp:Button ID="btnShow" runat="server" Width="70px" Text="Download"></asp:Button>&nbsp;<asp:Button ID="btnRecalculate" runat="server" Width="70px" Text="Recalculate"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 300px">
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <input id="hdnValSave" type="hidden" value="-1" runat="server" name="hdnValSave">
    </form>
</body>
</html>
