<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_KewajibanList.aspx.vb" Inherits=".FrmDepB_KewajibanList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Kewajiban Deposit B</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function Toggle(commId, imageId) {

            var div = document.getElementById(commId);
            var GetImg = document.getElementById(imageId);
            if (document.all[commId].style.display == 'none') {
                document.all[commId].style.display = 'block';
                document.all[imageId].src = '../Images/minus.gif';
            }
            else {
                document.all[commId].style.display = 'none';
                document.all[imageId].src = '../Images/plus.gif';
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Deposit B - Daftar Kewajiban</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 100%"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField" style="width: 146px">Kode Dealer</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                        runat="server"></asp:TextBox>
                    <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                    </asp:Label>
                </td>
                <td class="titleField" style="width: 146px">No. SO</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtNoSO" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Tipe Kewajiban</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:DropDownList Style="z-index: 0" ID="ddlKewajiban" runat="server" Width="140px"></asp:DropDownList>
                </td>
                <td class="titleField" style="width: 146px">No. Reg.</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtNoReg" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Tahun</td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                </td>
                <td class="titleField" style="width: 146px">Tanggal Pembuatan</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkTglpembuatan" runat="server" /></td>
                            <td>
                                <cc1:inticalendar id="icCreateDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Status</td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:DropDownList Style="z-index: 0" ID="ddlStatus" runat="server" Width="140px"></asp:DropDownList>
                </td>
                <td class="titleField" style="width: 146px"></td>
                <td style="width: 2px"></td>
                <td class="titleField"></td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px"></td>
                <td style="width: 2px"></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="72px"></asp:Button>
                </td>
                <td class="titleField" style="width: 146px"></td>
                <td style="width: 2px"></td>
                <td class="titleField"></td>
            </tr>
        </table>
        <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 290px">
                    <asp:DataGrid ID="dgDaftarKewajiban" runat="server" BorderWidth="0px" CellSpacing="1"
                        BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="False"
                        AllowPaging="True" AllowCustomPaging="True" PageSize="10" AllowSorting="True">
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No.">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnDepositAInterestHID" runat="server" Visible="False"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="3%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <HeaderTemplate>
                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkItem', document.all.chkAllItems.checked)"
                                        type="checkbox">
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="PeriodYear" HeaderText="Tahun">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriodYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodYear")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TipeKewajiban" HeaderText="Tipe Kewajiban">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipeKewajiban" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="NoRegKewajiban" HeaderText="No. Reg">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoReg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoRegKewajiban")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="NoSalesorder" HeaderText="No. SO">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoSO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoSalesorder")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Qty">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total Harga">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalHarga" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PPN">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPpn" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton ID="lbnDetail" runat="server" CommandName="Detail">
												<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer ke SAP" Width="100px"></asp:Button>
                    <asp:Button ID="btnEstimasi" runat="server" Text="Buat Estimasi" Width="100px"></asp:Button>
                    <asp:Button ID="btnBatalEstimasi" runat="server" Text="Batal Estimasi" Width="100px"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
