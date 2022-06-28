<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSPPORejectDescription.aspx.vb" Inherits="PopUpSPPORejectDescription" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpSPPORejectDescription</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script language="javascript">
        
        function send()
        {
			var txtKTBNote = document.getElementById("txtKTBNote");
			if (txtKTBNote.value == "")
			{
			    alert('Alasan penolakan belum diisi');
			    return;
			}

			if(navigator.appName == "Microsoft Internet Explorer")
			{ 
			    window.returnValue = txtKTBNote.value; 
			}
			else 
			{ 
			    opener.dialogWin.returnFunc(txtKTBNote.value); 
			}
            window.close();
        }
        
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <asp:Label ID="lblNote" Runat="server" CssClass="titlePage">Catatan MKS</asp:Label>
            <br />
            <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
                <tr>
                    <td vAlign="top">
                        <asp:Literal ID="ltrKTBNote" Runat="server" Text="Alasan Penolakan Indent Part" />
                     </td>
                     <td>
                        <asp:TextBox ID="txtKTBNote" Runat="server" TextMode="MultiLine" Rows="4" Columns="50" />
                     </td>
                </tr>
            </TABLE>
            <input type="button" value="Kirim" id="btnSend" onclick="javascript:send();" />
            <input type="button" value="Kembali" id="btnBack" onclick="javascript:window.close();" />
        </form>
    </body>
</HTML>
