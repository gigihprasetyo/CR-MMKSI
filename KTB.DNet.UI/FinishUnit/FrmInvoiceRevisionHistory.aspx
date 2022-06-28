<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInvoiceRevisionHistory.aspx.vb" Inherits=".FrmInvoiceRevisionHistory" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>ListContract</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">REVISI FAKTUR&nbsp;-&nbsp;History  
						Revisi Faktur</td>
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
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJumRecord" runat="server"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 380px">
                                    <asp:DataGrid ID="dgInvoiceList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                        AllowCustomPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status Revisi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturStatusDesc" runat="server" >
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe Revisi">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisionType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RevisionFaktur.RevisionType.Description")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dealer">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Dealer.DealerCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe/Warna">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.VechileColor.MaterialNumber")%>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Revisi">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RevisionFaktur.CreatedTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Validasi">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ValidateTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Konfirmasi">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConfirmTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ConfirmTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Selesai">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrintedTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrintedTime")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor Faktur Kendaraan">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFakturNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FakturNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Konsumen">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Customer.Name1")%>' ID="Label7">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%--Revisi <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Revision")%>' ID="lblRemark">
                                                    </asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Detail">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                                <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
