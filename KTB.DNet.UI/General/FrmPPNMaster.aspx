<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPPNMaster.aspx.vb" Inherits="FrmPPNMaster" SmartNavigation="False" %>

<%@ Register assembly="KTB.DNet.WebCC" namespace="KTB.DNet.WebCC" tagprefix="cc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmPPNMaster</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" language="javascript">
        function ShowPPDealerSelection() {
            //showPopUp('../General/../PopUp/PopUpDealerSelectionStallMaster.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var txtNamaDealerSelection = document.getElementById("txtNamaDealer");
            var hdnterm1 = document.getElementById("HiddenField1");
            var hdnterm2 = document.getElementById("HiddenField2");
            txtDealerSelection.value = temp[0];
            txtNamaDealerSelection.value = temp[1];
            hdnterm1.value = temp[0];
            hdnterm2.value = temp[1];

        }
        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }
        function CheckNumeric(e) {
            if (window.event) // IE
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 && e.keyCode != 44) {
                    event.returnValue = false;
                    return false;
                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8 && e.which != 44) {
                    e.preventDefault();
                    return false;
                }
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">MAINTENANCE&nbsp;- Master Pajak</td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="3" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <%--<img height="1" src="../images/dot.gif" border="0"></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table3" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr id="trMitsu1" runat="server">
                <td class="titleField">Tipe Pajak</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                   <asp:DropDownList ID="ddlTaxType" runat="server" Width="200" AutoPostBack="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField">Tanggal Mulai</td>
                <td>:</td>
                <td>
                    <cc1:IntiCalendar ID="icStartDate" runat="server" CanPostBack="False" Friday="True" Monday="True" Saturday="True" ScriptOnFocusOut="" Sunday="True" TargetForm="" TargetTemporaryFocus="" TargetTextBox="" Thursday="True" Tuesday="True" Value="2022-01-27" Wednesday="True"></cc1:IntiCalendar>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                    Persentase(%)</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtPercentage" onkeypress="CheckNumeric(event)" runat="server" Width="134px" ReadOnly="false"  style="text-align:right"></asp:TextBox>
                  
                </td>
            </tr>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="75px" Text="Simpan" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCari" runat="server" Width="75px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                    <asp:Button ID="btnBatal" runat="server" Width="75px" Text="Batal" OnClick="btnBatal_Click"></asp:Button>

                    <asp:TextBox ID="txtIDPPN" runat="server" Visible="False"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="dtgPPNMaster" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                        AllowPaging="True">
                        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle VerticalAlign="Top"></HeaderStyle>
                        <FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Tipe Pajak">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbTaxType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaxTypeID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tanggal Mulai">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Persentase(%)">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbPercentage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Percentage")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RowStatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Detail">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                   <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
								   <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
								   <asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False" OnClick="btnDownload_Click" Visible="false"></asp:Button></td>
            </tr>
        </table>
        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
    </form>
</body>
</html>
