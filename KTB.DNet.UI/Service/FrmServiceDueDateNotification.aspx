<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmServiceDueDateNotification.aspx.vb" Inherits=".FrmServiceDueDateNotification" %>

<%@ Import Namespace="KTB.DNet.Domain" %>

<!DOCTYPE html>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmServiceDueDateNotification</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = selectedDealer;
        }
    </script>

</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Umum - Due Date Reminder</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="2" width="70%" border="0">
                        <tr>
                            <td class="titleField">Kode Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="144px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Email</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Penerima</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNamaPenerima" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi Penerima</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlPosisiPenerima" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Jenis Notifikasi</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlEmailNotificationKind" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Upload Email</td>
                            <td>:</td>
                            <td>
                                <input onkeypress="return false;" id="dfPartShop" style="width: 320px; height: 20px" type="file"
                                    size="34" name="File1" runat="server">
                                <asp:Button ID="btnUpload" runat="server" Width="70px" Height="19px" Text="Upload"></asp:Button>
                                <asp:LinkButton ID="btnSample" runat="server">Download Template</asp:LinkButton>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Text="Cari" Width="60px"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" Text="Simpan" Width="60px"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="Batal" Width="60px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgSPPO" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
                            AllowPaging="True" AllowSorting="True" AllowCustomPaging="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="Gainsboro"
                            BorderWidth="0px">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server" Text='<%# CType(Container.DataItem, ServiceDueDateNotification).Dealer.DealerCode %>'>Label</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server" Text='<%# CType(Container.DataItem, ServiceDueDateNotification).Dealer.DealerName %>'>Label</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NameRecipient" HeaderText="Nama Penerima" SortExpression="NameRecipient">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="EmailDealer" HeaderText="Email Dealer" SortExpression="EmailDealer">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PositionRecipient" HeaderText="Posisi Penerima" SortExpression="PositionRecipient">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="EmailNotificationKind" HeaderText="Jenis Notifikasi">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailNotificationKind" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Note">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNote" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail">
											<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Anda yakin mau hapus?');">
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
</body>
</html>
