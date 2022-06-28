<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrCertificateConfig.aspx.vb" Inherits=".FrmTrCertificateConfig" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.getElementById("IdDelete");
            if (confirm("Yakin data ini akan dihapus?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
        }

        function RestrictSpace() {
            if (event.keyCode == 32) {
                event.returnValue = false;
                return false;
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
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

            <tr id="trInput" runat="server">
                <td valign="top">
                    <table id="Table1" runat="server" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField">Nama </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtnamaTTD"  runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtnamaTTD"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Jabatan </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtJabatanTTD" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtJabatanTTD"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Keterangan </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDeskripsi" onkeypress="return HtmlCharUniv(event)" runat="server" Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trUpload" runat="server">
                            <td class="titleField">Upload TTD<asp:HiddenField ID="hdnFilePath" runat="server" />
                            </td>
                            <td>:</td>
                            <td>

                                <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                                    runat="server">
                                &nbsp;
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                            </td>

                        </tr>
                        <tr id="trDownload" runat="server" visible="false">
                            <td width="100" class="titleField">Download TTD</td>
                            <td style="height: 12px">:</td>
                            <td width="80%">
                                <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" CausesValidation="false"></asp:LinkButton>
                                &nbsp;
                                              <asp:LinkButton ID="lbnDelete" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DdlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td height="30px">
                                <asp:Button runat="server" Width="60px" ID="btnSimpan" CausesValidation="true" Text="Simpan" />&nbsp;
                                <asp:Button runat="server" Width="60px" ID="btnBatal" CausesValidation="false" Text="Batal" />&nbsp;
                                <asp:Button runat="server" Width="60px" ID="btnCari" CausesValidation="false" Text="Cari" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 360px">
                        <asp:DataGrid ID="dtgConfigCer" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
                            CellSpacing="1" ForeColor="GhostWhite" PageSize="25" AllowSorting="True" AllowPaging="True"
                            AllowCustomPaging="True" BorderColor="#CDCDCD"
                            BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal"
                            AutoGenerateColumns="False">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle CssClass="titleTableService" Width="3%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" NAME="lblNo" Text="1"></asp:Label>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="NamaTTD" HeaderText="Nama">
                                    <HeaderStyle CssClass="titleTableService" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="JabatanTTD" HeaderText="Jabatan">
                                    <HeaderStyle CssClass="titleTableService" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbljabatan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
                                    <HeaderStyle CssClass="titleTableService" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" NAME="lblStatus"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TTD">
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUnduh" runat="server" Width="20px" Text="Download" CausesValidation="False"
                                            CommandName="unduh">
															<img src="../images/unduh.png" border="0" alt="Download"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnTTDpath" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableService" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                            CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnAktif" runat="server" Text="" CausesValidation="False"  CommandName="aktif">
									                    <img src="../images/aktif.gif" border="0" alt="Aktifkan"></asp:LinkButton>
                                         <asp:LinkButton ID="lbtnInAktif" runat="server" Text="" CausesValidation="False"  CommandName="inaktif">
									                    <img src="../images/in-aktif.gif" border="0" alt="Non Aktifkan"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
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
