<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventProposalFile.aspx.vb" Inherits="PopUpEventProposalFile" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpEventProposalFile</title>
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
    <body>
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
                <TR>
                    <td colspan="3" class="titlePage" style="HEIGHT: 31px">
                        <asp:Label Runat="server" ID="lblTitle" Text="Event - Dokumentasi"></asp:Label>
                    </td>
                </TR>
                <TR>
                    <TD style="WIDTH: 171px">Kode Dealer</TD>
                    <TD style="WIDTH: 3px">:</TD>
                    <TD>
                        <asp:Label id="lblDealerCode" runat="server"></asp:Label></TD>
                </TR>
                <TR>
                    <TD style="WIDTH: 171px">NamaDealer</TD>
                    <TD style="WIDTH: 3px">:</TD>
                    <TD>
                        <asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
                </TR>
                <TR>
                    <TD style="WIDTH: 171px">Nama Kegiatan</TD>
                    <TD style="WIDTH: 3px">:</TD>
                    <TD>
                        <asp:Label id="lblEventName" runat="server"></asp:Label></TD>
                </TR>
                <TR>
                    <TD style="WIDTH: 171px">Tanggal Acara</TD>
                    <TD style="WIDTH: 3px">:</TD>
                    <TD>
                        <asp:Label id="lblEventDate" runat="server"></asp:Label></TD>
                </TR>
            </TABLE>
            <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
                <TR>
                    <TD style="WIDTH: 50%" valign="top">
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 250px">
                            <asp:DataGrid id="dtgEventFile" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="true"
                                Width="100%">
                                <SelectedItemStyle BackColor="silver" Font-Bold="True"></SelectedItemStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Nama File" SortExpression="FileName">
                                        <HeaderStyle Height="25px" CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <div style="padding:2px">
                                                <asp:LinkButton Runat="server" ID="lblFileName" text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="ViewImage">
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </TD>
                    <TD width="50%" valign="top">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="titleTablePromo" style="PADDING-RIGHT:5px;PADDING-LEFT:5px;PADDING-BOTTOM:5px;PADDING-TOP:5px">
                                    Gambar
                                </td>
                            </tr>
                            <tr>
                                <td vAlign="middle" align="center">
                                    <DIV style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; OVERFLOW: auto; PADDING-TOP: 5px">
                                        <asp:Label Runat="server" ID="lblImg" Font-Bold="True"></asp:Label></DIV>
                                    <DIV style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; OVERFLOW: auto; PADDING-TOP: 5px">
                                        <br>
                                        <asp:Image Runat="server" ID="img" Visible="False" Width="300px"></asp:Image></DIV>
                                </td>
                            </tr>
                        </table>
                    </TD>
                </TR>
            </TABLE>
            <br>
            <div style="TEXT-ALIGN:center">
                <input id="btnPrint" runat="server" type="button" value="Cetak" onclick="window.print();"> <input type="button" value="Tutup" onclick="window.close();">
            </div>
        </form>
    </body>
</HTML>
