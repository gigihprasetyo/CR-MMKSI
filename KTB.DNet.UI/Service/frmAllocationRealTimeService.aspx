<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAllocationRealTimeService.aspx.vb" Inherits=".frmAllocationRealTimeService" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmAllocationRealTimeService</title>
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
        function ShowPopUpUpload() {
            //alert(id + ' ' + mode);
            showPopUp('../PopUp/PopUpUploadAllocationRealTimeService.aspx?', '', 500, 760, AfterSave);
        }

        function AfterSave(msg) {
            alert(msg);
            var btn = document.getElementById("btnCari")
            btn.click();
        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionStallMaster.aspx', '', 500, 760, DealerSelection);
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

    </script>
</head>

<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" style="height: 17px">Stall and Service Booking&nbsp;- Allocation Realtime Service</td>
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
            <tr>
                <td class="titleField" style="width: 200px;">Kode Dealer</td>
                <td style="width: 5px;">:</td>
                <td>
                    <asp:TextBox ID="txtKodeDealer" OnTextChanged="txtKodeDealer_TextChanged" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                        runat="server" ReadOnly="true"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField">Nama Dealer</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtNamaDealer" runat="server" Width="160px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label1" runat="server">Alokasi Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtAlokasiStall" runat="server" Width="134px" onkeypress="return numericOnlyUniv(event)" ReadOnly="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">
                    <asp:Label ID="Label2" runat="server">Current Stall</asp:Label></td>
                <td>:</td>
                <td>
                    <%--<div style="display: none;">--%>
                    <asp:TextBox ID="txtCurrent" runat="server" Width="134px" ReadOnly="true" Text="Auto Generate" ></asp:TextBox>
                    <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Download Template</asp:LinkButton>
                    <%--</div>--%>
                </td>
            </tr>
            <tr>
                <td class="titleField">
                                        <asp:Button ID="btnDownload" runat="server" Width="120px" Visible="false" Text="Download Template" OnClick="btnDownload_Click"></asp:Button>

                </td>
                <td></td>
                <td>
                </td>
            </tr>
            
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="75px" Text="Simpan" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCari" runat="server" Width="75px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                    <asp:Button ID="btnBatal" runat="server" Width="75px" Text="Batal" OnClick="btnBatal_Click"></asp:Button>
                    <asp:Button ID="btnImport" runat="server" Width="75px" Text="Import" OnClick="btnImport_Click"></asp:Button>

                    <asp:HiddenField runat="server" ID="hdnValid" Value="0" />
                                        <input id="hdConfirm" type="hidden" value="-1" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="dtgStallMaster" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
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
                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Alokasi Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbAlokasiStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AlokasiStall")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Current Stall">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbCurrentStall" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentStall")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Detail">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
										<img src="../images/edit.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
<%--                <td colspan="3" align="left">
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False" OnClick="btnDownload_Click"></asp:Button></td>--%>
            </tr>
        </table>
        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
    </form>
</body>
</html>
