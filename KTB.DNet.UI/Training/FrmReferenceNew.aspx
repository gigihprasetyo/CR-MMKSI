<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmReferenceNew.aspx.vb" Inherits="FrmReferenceNew" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ReferenceHelper</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdntypeArea" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
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
                <td valign="top">
                    <br>
                    <div id="div1" style="overflow: auto; height: 440px">
                        <asp:DataGrid ID="dtgReferences" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" PageSize="25" ForeColor="GhostWhite"
                            CellSpacing="1" Font-Names="MS Reference Sans Serif" Width="100%">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="White" BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle HorizontalAlign="Left" ForeColor="White" VerticalAlign="Top" BackColor="#F7F7F7"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Code" HeaderText="Pengguna">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Deskripsi">
                                    <HeaderStyle Width="70%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine"
                                            Rows="2"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr id="trAction" runat="server">
                <td valign="top" align="center">
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="50px" Text="Batal"></asp:Button></td>
            </tr>
        </table>
    </form>
</body>
</html>
