<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpEditNamePhone.aspx.vb" Inherits=".PopUpEditNamePhone" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpEdit</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
   

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess() {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = "Simpan berhasil.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Simpan berhasil.");
	        }

	        window.close();
	    }
	    
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
			<tr>
                <td class="titleTableParts3" colspan="2">Popup Edit</td>
            </tr>

    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" border="0">
			<tr>
                <td valign="top" align="left" width="50%">
                    <table id="Table_left" cellspacing="1" cellpadding="2" border="0">
                        <tr>
                            <td class="auto-style1" colspan="2">Name</td>
                            <td colspan="2">:
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Phone Number</td>
                            <td colspan="2">:
                                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField" width="24%"></td>
                            <td width="1%"></td>
                            <td width="75%">
                                <asp:Button ID="btnSave" Text="Simpan" runat="server" />
                            </td>
                        </tr>

                        </table>
                    </td>
                </tr>
        </table>
    </form>
</body>
</html>
