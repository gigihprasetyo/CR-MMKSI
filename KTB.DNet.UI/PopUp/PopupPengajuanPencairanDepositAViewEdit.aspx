<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopupPengajuanPencairanDepositAViewEdit.aspx.vb" Inherits="PopupPengajuanPencairanDepositAViewEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PopupPengajuanPencairanDepositAViewEdit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function CalculatePPn(txtAmount, txtPPn) {
            txtPPn.value = 0.1 * txtAmount.value;
            var lblTotal = document.getElementById("lblTotal");
            lblTotal.value = lblTotal.value + txtAmount.value;

        }

        function ShowPPDealerBankAccountSelection(DealerID) {
            showPopUp('../PopUp/PopUpDealerBankAccountSelectionOne.aspx?DealerID=' + DealerID, '', 500, 760, DealerBankAccountSelection);
        }

        function DealerBankAccountSelection(selectedAccount) {

            var txtDealerBankAccountSelection = document.getElementById("txtNomorRekening");
            txtDealerBankAccountSelection.value = selectedAccount;
        }
    </script>
    <base target="_self">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Sales - DepositA - Pengajuan Pencairan Deposit A (Detail)</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField">Kode Dealer</td>
                <td>:</td>
                <td>
                    <asp:Literal ID="ltrDealerCode" runat="server"></asp:Literal></td>
                <td class="titleField" style="height: 32px">Tanggal Pengajuan</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Label ID="lblPostingDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Nama Dealer</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Literal ID="ltrDealerName" runat="server"></asp:Literal></td>
                <td class="titleField">No. Surat Pengajuan</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNomerSuratPengajuan" runat="server" MaxLength="18" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Tipe Pengajuan</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:Label ID="lblTipePengajuan" runat="server"></asp:Label></td>
                <td class="titleField">
                    <asp:Label ID="lblNomorRekening" runat="server" Text="Nomor Rekening"></asp:Label></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNomorRekening" onblur="omitSomeCharacter('txtNomorRekening','<>?*%$')" runat="server"
                        Enabled="False" ReadOnly="True"></asp:TextBox><asp:LinkButton ID="lnkAccount" runat="server" Visible="False">
							<img src="../images/popup.gif" border="0" style="cursor:hand" alt="Klik popup">
                        </asp:LinkButton></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblAssignmentNo" runat="server" Text="Assignment Number"></asp:Label></td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblAssignmentNoValue" runat="server"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lblProduk1" runat="server" Text="Produk" Visible="false"></asp:Label></td>
                <td>
                    <asp:Label ID="lblProduk2" runat="server" Text=":" Visible="false"></asp:Label></td>
                <td> <asp:Label ID="lblProduk3" runat="server" Text="" Visible="false"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="lblDNSONumberCaption" runat="server" Text="DN Number"></asp:Label></td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblDNSONumberContent" runat="server"></asp:Label>

                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 265px">
            <asp:DataGrid ID="dgEntryPencairanDepositA" runat="server" BorderWidth="0px" BorderColor="Gainsboro"
                CellPadding="3" CellSpacing="1" AllowSorting="False" AutoGenerateColumns="False" ShowFooter="False" Width="100%" BackColor="#CDCDCD">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" Text="1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Total">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblHeaderAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtHeaderAmountEdit" runat="server" BackColor="White" Width="95px" MaxLength="18"
                                Enabled="False"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jumlah Pengajuan">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJumlahPencairan" Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}") %>' runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJumlahPencairanEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}" ) %>' BackColor="White" Width="95px" MaxLength="18">
                            </asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PPn">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPPn" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPPnEdit" runat="server" BackColor="White" Width="95px" MaxLength="18" Enabled="False"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Penjelasan">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPenjelasan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPenjelasanEntryEdit" Width="200px" runat="server" CssClass="textLeft" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
                        CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
                        EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
                        <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:EditCommandColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField" width="5%">Total</td>
                <td width="2%">:</td>
                <td align="right" width="20%">
                    <asp:Label ID="lblTotal" runat="server" Text="0" Font-Bold="True"></asp:Label></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                        name="btnCancel">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
