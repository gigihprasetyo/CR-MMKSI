<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpTimeWork.aspx.vb" Inherits=".PopUpTimeWork" %>

<%--<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>--%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmTimeSelection</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
	<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess()
	    {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = "Simpan berhasil.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Simpan berhasil.");
	        }

	        window.close();
	    }
			
	    $(function () {
	        $('.timepicker').clockpicker({
	            placement: 'bottom',
	            align: 'left',
	            autoclose: true
	        });
	    });
	</script>
</head>
<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
				<tr>
                    <td class="titleTableParts3" colspan="3">Popup Setting Calendar Kerja</td>
                </tr>
				<tr valign="top">
					<td class="titleField">Tanggal</td>
                    <td>:</td>
                    <td>
                        <asp:HiddenField ID="hdID" runat="server" />
                        <asp:Label ID="lblTgl" runat="server"></asp:Label>
                    </td>
				</tr>
				<tr valign="top">
					<td class="titleField">Waktu Operasional</td>
                    <td>:</td>
                    <td>
                        <table cellspacing="1" cellpadding="1">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtOptFrom" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                </td>
                                <td>&nbsp;s/d&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtOptTo" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Waktu Istirahat</td>
                    <td>:</td>
                    <td>
                        <table cellspacing="1" cellpadding="1">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtBreakFrom" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                </td>
                                <td>&nbsp;s/d&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtBreakTo" runat="server" TextMode="Time" CssClass="timepicker"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Stall</td>
                    <td>:</td> 
                    <td><asp:Label ID="lblStall" runat="server"></asp:Label></td>
				</tr>
                <tr valign="top">
					<td class="titleField">Servis</td>
                    <td>:</td>
                    <td><asp:DropDownList ID="ddlVisitType" runat="server"></asp:DropDownList></td>
				</tr>
                <tr valign="top">
					<td class="titleField">Libur</td>
                    <td>:</td> 
                    <td>
                        <asp:RadioButton ID="rbYes" runat="server" CssClass="setMargin" Text="Ya" GroupName="Libur"></asp:RadioButton>
                        <asp:RadioButton ID="rbNo" runat="server" CssClass="setMargin" Text="Tidak" GroupName="Libur"></asp:RadioButton>
                    </td>
				</tr>
                <tr valign="top">
					<td class="titleField">Catatan</td>
                    <td>:</td>
                    <td><asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="60" Width="250"></asp:TextBox></td>
				</tr>
				<tr>
                    <td colspan="2"></td>
					<td align="left">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Simpan" />
                        <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
