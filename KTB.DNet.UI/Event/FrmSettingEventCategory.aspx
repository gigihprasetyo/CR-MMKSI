<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSettingEventCategory.aspx.vb" Inherits=".FrmSettingEventCategory" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSettingEventCategory</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpDealerCategory() {
            //var lblDealer = document.getElementById("lblDealerCode");
            //var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            //showPopUp('../PopUp/PopUpDealerBranchSelectionOne.aspx', '', 430, 800, TemporaryOutlet);
            showPopUp('../PopUp/PopUpDealerCategorySelectionEvent.aspx', '', 430, 800, DealerCategory);
        }


        function DealerCategory(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            var hdnTemporaryOutlet = document.getElementById("hdnDealerCode");
            var lblECategory = document.getElementById("lblECategory");
            hdnTemporaryOutlet.value = selectedRefNumber;

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnTemporaryOutlet.blur();
            }
            else {
                hdnTemporaryOutlet.onchange();
            }
        }
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="lblTitle" runat="server" Text="EVENT - SETTING EVENT CATEGORY"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" width="50%" border="0">
                        <tr>
                            <td class="titleField" style="height: 30px" width="4%">Dealer</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td style="width: 375px; height: 10px" width="375">
                                <asp:TextBox ID="txtDealerCode" runat="server" Width="128px" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnCategory" runat="server" />
                                <asp:HiddenField ID="hdnDealerCode" runat="server" />
                                <asp:LinkButton ID="lnkBtnPopUpDealer" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px; width: 30%">Event Kategori</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:Label ID="lblECategory" runat="server"  Text="Auto Generated dari PopUp Dealer"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px; width: 30%">Nama Kategori</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtCategoryName" runat="server" Width="128px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px">Status</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Tidak Aktif" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px;" colspan="3">Dokumen pendukung yang HARUS diupload</td>
                        </tr>
                        <tr>
                            <td colspan="3">

                                <asp:DataGrid ID="DataGrid1" runat="server" Width="20%" AutoGenerateColumns="False" ShowFooter="True" ShowHeader="false" GridLines="None">
                                    <ItemStyle ForeColor="Black" BackColor="White" Height="30px"></ItemStyle>
                                    <FooterStyle ForeColor="Black" BackColor="White" Height="30px"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileIklanG1" runat="server" Text="File Cache"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCatsG1" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" FooterStyle-Width="10px" ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px;" colspan="3">Dokumen yang HARUS diupload untuk pengajuan acara</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:DataGrid ID="dgFiles" runat="server" Width="30%" AutoGenerateColumns="False" ShowFooter="True" ShowHeader="false" GridLines="None">
                                    <ItemStyle ForeColor="Black" BackColor="White" Height="30px"></ItemStyle>
                                    <FooterStyle ForeColor="Black" BackColor="White" Height="30px"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn FooterStyle-Width="85%" ItemStyle-Width="85%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileIklanG2" runat="server" Text="File Cache"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCatsG2" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" FooterStyle-Width="10px" ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="height: 20px"></span>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="DataGrid2" runat="server" Width="80%" AutoGenerateColumns="False" ShowFooter="false">
                        <ItemStyle ForeColor="Black" BackColor="White" Height="30px" VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                        <FooterStyle ForeColor="Black" BackColor="White" Height="30px"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Kategori">
                                <HeaderStyle Width="60%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
