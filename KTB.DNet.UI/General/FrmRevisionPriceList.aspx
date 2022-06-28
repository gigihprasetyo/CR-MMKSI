<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRevisionPriceList.aspx.vb" Inherits=".FrmRevisionPriceList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>FrmAplikasiHeader</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
	<script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">Revisi Faktur Kendaraan - Master Revisi Harga</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="2" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="2" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="formRevisionPrice" runat="server">
                        <table>                           
                            <tr>
                                <td class="titleField" width="10%">Category&nbsp;</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Tipe Revisi&nbsp;</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlRevisionType" runat="server" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="15%"> <asp:CheckBox ID="chkValidPeriod" runat="server" Text="Tgl Valid From"></asp:CheckBox></td>
                                <td width="1%">:</td>
                                <td>
                                   <table cellspacing="0" cellpadding="0" border="0">
									    <tr>
										    <td>
											    <cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										    <td>&nbsp;s/d&nbsp;</td>
										    <td>
											    <cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									    </tr>
								    </table>
                                </td>
                            </tr>                               
                            <tr>
                                <td class="titleField" width="20%">&nbsp;</td>
                                <td width="1%"></td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp;          
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">

                    <asp:Panel ID="formGrid" runat="server" Visible="true">
                        <asp:Button ID="btnTambah" Visible="false" runat="server" Text="Tambah" Width="60px" CausesValidation="False"></asp:Button>
                        <div id="div1" style="overflow: auto; height: 440px">
                            <asp:DataGrid ID="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                                PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                CellPadding="3" DataKeyField="ID">
                                <SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                <ItemStyle BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Kategori">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="RevisionType.Description" HeaderText="Tipe revisi">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRevision" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Amount" HeaderText="Amount">
                                        <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Valid From">
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblValid" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="">
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="center" Wrap="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDetail" runat="server" CommandName="View">
															    <img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
