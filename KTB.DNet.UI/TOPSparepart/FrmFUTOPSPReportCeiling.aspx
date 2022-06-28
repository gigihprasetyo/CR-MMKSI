<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFUTOPSPReportCeiling.aspx.vb" Inherits=".FrmFUTOPSPReportCeiling" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Ceiling Report</title>
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

        function Spanning() {
            var dtgMain = document.getElementById("dtgMain");
            var txtIsSpanned = document.getElementById("txtIsSpanned");
            var i = 0;
            if (txtIsSpanned.value != "1") return false;
            if (dtgMain.rows.length <= 1) return false;
            for (i = 1; i < dtgMain.rows.length; i++) {
                if ((i - 1) % 3 == 0) {
                    dtgMain.rows[i].cells[0].rowSpan = "3";
                    dtgMain.rows[i].cells[0].vAlign = "middle";
                    dtgMain.rows[i].cells[1].rowSpan = "3";
                    dtgMain.rows[i].cells[1].vAlign = "middle";

                    dtgMain.rows[i + 1].deleteCell(0);
                    dtgMain.rows[i + 1].deleteCell(0);
                    dtgMain.rows[i + 2].deleteCell(0);
                    dtgMain.rows[i + 2].deleteCell(0);

                    //dtgMain.rows[i + 2].deleteCell(0);
                    //dtgMain.rows[i + 2].deleteCell(0);
                }
            }
            
        }

    </script>
</head>
<body ms_positioning="GridLayout" style="margin: 5px;">
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 16px">&nbsp;
						<asp:Label ID="lblTitle" runat="server">TOP Sparepart - Report Ceiling Gabungan</asp:Label></td>
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
                                <td width="200px"></td>
                                <td width="150px"></td>
                                <td width="1px"></td>
                                <td width="1300px"></td>
                                <td width="1px"></td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Product Category</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <div style="display: none">
                                        <asp:TextBox runat="server" ID="txtID" Visible="false"></asp:TextBox>
                                    </div>
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" Width="160px"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Credit Account</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:TextBox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" ID="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" onclick="ShowPPAccountSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr style="display:none;">
                                <td><strong>Tipe Pembayaran</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="160px"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Tgl Report</strong></td>
                                <td><strong>:</strong></td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td>
                                                <cc1:IntiCalendar id="calEffectiveStart" runat="server"></cc1:IntiCalendar></td>
                                            <td width="1px"></td>
                                            <td>
                                                <cc1:IntiCalendar id="calEffectiveEnd" runat="server"></cc1:IntiCalendar></td>
                                        </tr>

                                    </table>

                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                    <asp:textbox style="display:none;" id="txtIsSpanned" runat="server" Width="16px" ReadOnly="True">1</asp:textbox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="6">

                                    <div id="divHidden" style="overflow: auto; width: 100%; height: 290px">
                                        <asp:DataGrid ID="dtgMain" border="1"
                                            runat="server" Width="100%" BackColor="#CDCDCD" BorderColor="#CDCDCD"
								AllowSorting="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False"
>
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
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                

                                                <asp:TemplateColumn HeaderText="Credit Account" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCreditAccount"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Jenis Report" Visible="true">
                                                    <HeaderStyle ForeColor="White" Width="200px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblReportKind"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                            
                                                <asp:TemplateColumn HeaderText="1" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day1" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="2" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day2" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="3" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day3" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="4" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day4" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="5" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day5" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="6" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day6" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="7" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day7" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="8" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day8" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="9" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day9" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="10" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day10" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="11" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day11" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="12" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day12" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="13" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day13" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="14" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day14" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="15" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day15" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="16" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day16" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="17" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day17" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="18" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day18" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="19" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day19" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="20" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day20" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="21" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day21" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="22" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day22" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="23" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day23" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="24" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day24" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="25" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day25" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="26" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day26" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="27" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day27" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="28" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day28" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="29" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day29" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="30" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day30" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="31" >
                                                    <HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="Day31" style="text-decoration:none;"></asp:LinkButton>
                                                     </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn visible="false">
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDetail" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Detail" style="text-decoration:none;"></asp:LinkButton>
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
     <script>
         Spanning();
         </script>
</body>
</html>
