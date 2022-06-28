<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmDaftarBenefitClaimDeducted.aspx.vb" Inherits=".FrmDaftarBenefitClaimDeducted" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.7.2.min.js"></script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.gif");
            $(this).closest("tr").next().remove();
        });


    </script>


    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 600, 600, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = selectedDealer;
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }
            else {
                txtDealerCodeSelection.onchange();
            }
        }

        function refreshGrid() {
            alert('Mohon lengkapi data kelas sebelumnya');
            var div = document.getElementById('<%=dvGrid.ClientID%>');
            var div_position = document.getElementById('<%= div_position.ClientID%>');
            var position = parseInt('<%=Request.Form("div_position") %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        }
    </script>

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">SALES CAMPAIGN - Daftar Reduksi Claim </td>
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
                <td valign="top">
                    <table id="tbl7" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="60%">
                                <table id="table5">
                                    <tr id="trDealer" runat="server" >
                                        <td class="titleField" width="200px" height="22">Kode Dealer</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtDealerCode" runat="server" Width="164px"></asp:TextBox>
                                            <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowPPDealerSelection();"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="200px" height="22">No Claim Reg DSF</td>
                                        <td style="height: 22px" width="20px">:</td>
                                        <td style="height: 22px" nowrap="nowrap">
                                            <asp:TextBox ID="txtRegNumber" runat="server" Width="164px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="titleField"></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Width="80px" Text="Cari" CausesValidation="False"></asp:Button>&nbsp;
                                            <asp:Button ID="btnDownload" runat="server" Width="80px" Text="Download" TabIndex="15"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="dvGrid" runat="server" style="overflow: auto; height: auto">
                        <input type="hidden" id="div_position" runat="server" />
                        <asp:DataGrid ID="dtgHeader" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" id="imgPlus" src="../images/plus.gif" />
                                        <asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                                            <asp:DataGrid ID="dtgDetail" Width="100%" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="No Klaim Reg Dealer" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblClaimRegNoDealer" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Pilihan Klaim" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Nilai Reduksi Klaim" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmountDeducted" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tgl Transaksi" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedTime" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="">
                                    <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No Claim Reg DSF" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimRegNoDsf" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Rangka" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNumber" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Claim Reg" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimRegNoDealer" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nilai Reduksi Awal" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeductedAmount" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Sisa" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemainAmount" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>

