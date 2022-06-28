<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpListInstruktur.aspx.vb" Inherits=".PopUpListInstruktur" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Popup Head MRTC</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <base target="_self">

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">List Instruktur MRTC</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
           
            <tr>
                <td class="titleField" colspan="3">
                    <div id="div1" style="overflow: auto; height: 330px">
                        <asp:DataGrid ID="dtgUserInfo" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
                            CellPadding="3" GridLines="None">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
                                BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID Trainee" SortExpression="ID">
                                      <HeaderStyle Width="50%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                 <asp:TemplateColumn HeaderText="Nama Trainee">
                                      <HeaderStyle Width="50%" CssClass="titleTableGeneral"></HeaderStyle>
                                     <ItemTemplate>
                                         <asp:Label ID="lblNama" runat="server"></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
           
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
