<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPTransferControl.aspx.vb" Inherits=".FrmTOPTransferControl" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Control</title>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script>


        function ShowPPAccountSelection() {
            showPopUp('../General/../PopUp/PopUpCreditAccountSelection2.aspx', '', 500, 760, AccountSelection2);
        }

        function AccountSelection2(selectedAccount) {
            var tempParam = selectedAccount;
            var txtAccountSelection = document.getElementById("txtCreditAccount");
            txtAccountSelection.value = selectedAccount;
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 200px;
        }
    </style>
</head>
<body ms_positioning="GridLayout" style="margin: 5px;">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TOP Spare Part - Transfer Transaction Control</asp:Label></td>
                </tr>
                <tr style="height: 1px;">
                    <td style="height: 1px; background-image: url('../images/bg_hor.gif'); background-size: auto;"></td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="8" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td class="auto-style1"></td>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr>
                                <div style="display: none">
                                    <asp:TextBox runat="server" ID="txtID" Visible="false" Text="0"></asp:TextBox>
                                </div>
                                <td><strong>Credit Account</strong></td>
                                <td><strong>:</strong></td>
                                <td class="auto-style1">
                                    <asp:TextBox onblur="omitSomeCharacter('txtCreditAccount','<>?*%$')" ID="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Tipe Pembayaran</strong></td>
                                <td><strong>:</strong></td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="160px"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Metode Pembayaran</strong></td>
                                <td><strong>:</strong></td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" Width="160px"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>
                                    <asp:CheckBox ID="chkvalidFrom" Checked="False" Visible="true" runat="server" />
                                    Berlaku Mulai</strong> &nbsp;  </td>
                                <td><strong>:</strong></td>
                                <td class="auto-style1">

                                    <cc1:IntiCalendar ID="calValidFrom" runat="server" CanPostBack="False"></cc1:IntiCalendar>




                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkValidTo" Checked="False" Visible="true" runat="server" />&nbsp;<strong>Berlaku&nbsp; Sampai</strong>  </td>
                                <td><strong>:</strong></td>
                                <td class="auto-style1">
                                    <cc1:IntiCalendar ID="calValidTo" runat="server" CanPostBack="False"></cc1:IntiCalendar>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td><strong>
                                    <asp:CheckBox ID="chkValidityDate" Checked="False" Visible="true" runat="server" />Tanggal</strong> <strong>Valid   </strong></td>
                                <td>:</td>
                                <td class="auto-style1">
                                    <cc1:IntiCalendar ID="calValidityDate" runat="server" CanPostBack="False"></cc1:IntiCalendar>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td class="auto-style1">


                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" />
                                    &nbsp;
                                    <asp:Button ID="btnBatal" runat="server" Text="Batal" />
                                    &nbsp;
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td class="auto-style1"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 290px">
                                        <asp:DataGrid ID="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
                                            CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                            AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                            DataKeyField="ID" ShowFooter="True">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>

                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle ForeColor="White" Width="40px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNo"></asp:Label>
                                                        <div style="display: none;">
                                                            <asp:Label runat="server" ID="lblID" Visible="false"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Deskripsi" Visible="false">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDeskripsi"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Credit Account" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Tipe Pembayaran" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Metode Pembayaran">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPaymentMethod"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Berlaku Mulai">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblValidFrom"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                                <asp:TemplateColumn HeaderText="Berlaku Sampai">
                                                    <HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblValidTo"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>



                                                <asp:TemplateColumn HeaderText="Tanggal Valid">
                                                    <HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblValidity"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>




                                                <asp:TemplateColumn HeaderText="Tgl Update">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUpdatedTime"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Diupdate Oleh">
                                                    <HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUpdatedBy"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>


                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Rubah"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6"></td>

                                <%--
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                --%>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
