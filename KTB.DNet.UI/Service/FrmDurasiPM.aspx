<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDurasiPM.aspx.vb" Inherits=".FrmDurasiPM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Durasi PM</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script language="javascript">

        function saveConfirmation(obj) {
            var btnSave = document.getElementById(obj.id);
            if (!confirm('Simpan Data durasi PM ?')) {
                btnSave.disabled = false;
                return false;
            }
            else {
                document.getElementById('btnSave').click();
                btnSave.disabled = true;
                return true;
            }
        }

    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PERIODICAL MAINTENANCE -&nbsp; Durasi PM</td>
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
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="lblDealer" runat="server">Jenis PM</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                            <td width="75%">
                                <asp:DropDownList ID="ddlPMKind" runat="server" Height="16px" Width="146px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kategori&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" Height="16px" Width="146px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="titleField">Durasi(hari)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                            <td>
                                <asp:TextBox ID="txtDuration" runat="server" Width="146px" onkeypress="return NumericOnlyWith(event,'');"
                                    ToolTip="Kode Cabang"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" CausesValidation="False" />
                                <asp:Button ID="btnSave" runat="server" Style="" onClientClick="return confirm('Simpan durasi?');" Text="Simpan" CausesValidation="False" />
                                <asp:Button ID="btnCancel" runat="server" Text="Batal" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dgPMDuration" runat="server" Width="100%" PageSize="50" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
                            AllowPaging="True" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis PM">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMKind" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Durasi">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDuration" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnPart" runat="server" Style="display: none">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Add or Remove Part">
                                        </asp:Label>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="False" CommandName="View" Text="Ubah" ToolTip="Detail">
												<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick="return confirm('Hapus data?')" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    </td>
            </tr>
            <tr>
                <td style="height: 11px" align="left">&nbsp;&nbsp;</td>
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
