<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImageViewer.aspx.vb" Inherits="ImageViewer" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>ImageViewer</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <base target="_self">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script language="javascript">
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <DIV style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; MARGIN: auto; OVERFLOW: auto; WIDTH: 100%; PADDING-TOP: 5px; TEXT-ALIGN: center; Text-Alignment: center">
                <asp:Image Runat="server" ID="img" Visible="False" Width="600px"></asp:Image>
                <br /><br />
                <input class="hideButtonOnPrint" type="button" value="Cetak" onclick="window.print();">
            </DIV>
            
        </form>
    </body>
</HTML>
