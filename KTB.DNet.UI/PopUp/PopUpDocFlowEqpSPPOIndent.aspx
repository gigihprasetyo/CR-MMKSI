<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDocFlowEqpSPPOIndent.aspx.vb" Inherits="PopUpDocFlowEqpSPPOIndent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpDocFlowEqpSPPOIndent</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <asp:Label id="lblHeader" runat="server" Font-Bold="True"></asp:Label>
            <br>
            <asp:DataGrid id="dtgItem" runat="server" ShowHeader="False" ShowFooter="False" AutoGenerateColumns="False"
                CellPadding="2" CellSpacing="1" BorderWidth="0">
                <Columns>
                    <asp:BoundColumn DataField="RequestNo" DataFormatString="Request Indent Part Equip # - {0}"
                        SortExpression="RequestNo"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:Label Runat="server" ID="lblTotalItem"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <br>
            <INPUT id="btnClose" onclick="window.close();" type="button" value="Tutup" name="btnlose">
        </form>
    </body>
</HTML>
