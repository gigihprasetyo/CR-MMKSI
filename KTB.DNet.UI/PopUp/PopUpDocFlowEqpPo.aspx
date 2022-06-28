<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDocFlowEqpPo.aspx.vb" Inherits="PopUpDocFlowEqpPo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpDocFlowEqpPo</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <asp:Label id="lblHeader" runat="server"
                Font-Bold="True"></asp:Label>
            <br>
            <asp:DataGrid id="dtgItem" runat="server"
                ShowHeader="False" ShowFooter="False" AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                BorderWidth="0">
                <Columns>
                    <asp:BoundColumn DataField="RequestNo" DataFormatString="Request Indent Part Equip # - {0}"
                        SortExpression="RequestNo"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EstimationNumber" DataFormatString="Estimation Indent Part Equip # - {0}"
                        SortExpression="EstimationNumber"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalItem" DataFormatString=" -> {0} unit" SortExpression="TotalItem"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalEstimationUnit" DataFormatString=" -> {0} unit" SortExpression="TotalEstimationUnit"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <br>
            <INPUT id="btnClose" onclick="window.close();" type="button" value="Tutup" name="btnlose">
        </form>
    </body>
</HTML>
