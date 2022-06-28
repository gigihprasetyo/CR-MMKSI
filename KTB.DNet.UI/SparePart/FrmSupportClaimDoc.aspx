<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSupportClaimDoc.aspx.vb" Inherits=".FrmSupportClaimDoc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPositionWSC</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CLAIM - Dokumen Pendukung Claim</td>
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
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="50%" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label2" runat="server">Nama Dokumen</asp:Label></td>
                            <td>
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td>
                                <asp:HiddenField ID="hdnID" runat="server" />
                                <asp:TextBox ID="txtDocumentName" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');" onblur="omitSomeCharacter('txtDocumentName','<>?*%$;')"
                                    runat="server" Width="250px" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="ValidCode" runat="server" ErrorMessage="*" ControlToValidate="txtDocumentName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label3" runat="server">Deskripsi</asp:Label></td>
                            <td>
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDescription" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"
                                    runat="server" Width="250px" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="ValidDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td width="24%" class="titleField">
                                <asp:Label ID="Label1" runat="server">Status</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="90px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Simpan"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 370px">
                        <asp:DataGrid ID="dgDocClaim" runat="server" Width="50%" AutoGenerateColumns="False" AllowPaging="True"
                            AllowSorting="True" AllowCustomPaging="True" PageSize="50" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DocumentName" SortExpression="DocumentName" HeaderText="Nama Dokumen">
                                    <HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi Dokumen">
                                    <HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" CommandName="View" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CausesValidation="False" Visible="false">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
