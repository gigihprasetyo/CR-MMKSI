<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmClaimJV.aspx.vb" Inherits=".FrmClaimJV" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmClaimJV</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="lblTitle" runat="server" Text="SALES CAMPAIGN - JOURNAL VOUCHER"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td valign="top">
                    <table>
                        <tr>
                            <td valign="top">
                                <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">Dealer</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblDealer" runat="server" Width="500px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">No Kuitansi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblReceiptNo" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trNoJV" runat="server">
                                        <td class="titleField" style="width: 140px; height: 10px">No Rekening</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblNoRek" runat="server" Width="500px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">Teks Referensi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblRefText" runat="server" Width="500px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">Status</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblStatus" runat="server" Width="300px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table id="Table12" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr id="trProcessDate" runat="server">
                                        <td class="titleField" style="width: 140px; height: 10px">Tgl Pembayaran</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <cc1:IntiCalendar ID="icProcessDate" runat="server" TextBoxWidth="70" Enabled="false"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">Amount Claim</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblTotalPencairan" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">PPh</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblPPh" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">PPn</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblVat" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 140px; height: 10px">DPP</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblDPP" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Simpan"></asp:Button><span style="margin-right: 10px"></span>
                    <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListJV" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000" AllowSorting="True">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Debit/ Credit Indicator">
                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDebitCreditIndicator" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlDebitCreditIndicatorGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDebitCreditIndicatorGrid_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDebitCreditIndicatorEditGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDebitCreditIndicatorGrid_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Account" SortExpression="TipeAccount">
                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTipeAccount" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlTipeAccountGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeAccountGrid_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTipeAccountEditGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeAccountGrid_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Business Area" SortExpression="BusinessArea">
                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBusinessArea" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblBusinessAreaGrid" runat="server"></asp:Label>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblBusinessAreaEditGrid" runat="server"></asp:Label>
                        </EditItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                        <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlVechileTypeCodeGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVechileTypeCodeGrid_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlVechileTypeCodeEditGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVechileTypeCodeGrid_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Month" SortExpression="Month">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMonth" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlMonthGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthGrid_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlMonthEditGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthGrid_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Cost Center" SortExpression="CostCenter">
                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenter" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlCostCenterGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCostCenterGrid_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCostCenterEditGrid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCostCenterGrid_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                        <HeaderStyle ForeColor="White" Width="100px" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" Width="80px" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblAmountGrid" Width="80px" runat="server"></asp:Label>
                            <asp:TextBox ID="txtAmountGrid" style="text-align: right" Visible="false" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')" 
                                runat="server" Width="90px" ></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAmountEditGrid" Width="80px" Visible="false" runat="server"></asp:Label>
                            <asp:TextBox ID="txtAmountEditGrid" style="text-align: right" Visible="false" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')" 
                                runat="server" Width="90px"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Accrual" SortExpression="MasterAccrued.ID">
                        <HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAccrual" Width="130px" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAccrualGrid" runat="server" class="ddlModelGrid"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAccrualEditGrid" runat="server" class="ddlModelGrid"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Keterangan" SortExpression="Remarks">
                        <HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle Wrap="True" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" Width="250px" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarksGrid" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarksEditGrid" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTableSales" Width="15%"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus"  OnClientClick="return confirm('Anda yakin mau hapus?');"
                                CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
									<img alt="Hapus"  src="../images/trash.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>

                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSaveEdit" TabIndex="40" runat="server" CausesValidation="True" CommandName="Save"
                                Text="Simpan">
							    <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="Cancel" Text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>

                        <FooterStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="Add" Text="Tambah" runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>

                </Columns>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
