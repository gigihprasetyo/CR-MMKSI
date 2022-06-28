<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPExtendedMaster.aspx.vb" Inherits=".FrmMSPExtendedMaster" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MSP Master</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmMSPMaster" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MSP Extended - Master Data</td>
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
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="14%">Kategori</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <asp:DropDownList runat="server" ID="ddlCategory" Width="161px" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlVechileModel" Width="161px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                            <td class="titleField" width="14%">
                                <asp:CheckBox ID="chkStartDate" runat="server"></asp:CheckBox>Mulai Berlaku</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="StartDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="StartDateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Tipe MSP Extended</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <asp:DropDownList runat="server" ID="ddlMSPType" Width="161px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                            <td class="titleField" width="14%">
                                <asp:CheckBox ID="chkEndDate" runat="server"></asp:CheckBox>Berlaku Sampai Dengan</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="EndDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="EndDateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">Durasi</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <asp:TextBox ID="txtDuration" runat="server" Width="200px" onblur="numericOnlyBlur(txtDuration)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                            </td>
                            <td class="titleField" width="14%">Status</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <asp:DropDownList runat="server" ID="ddlStatus" Width="161px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%">KM</td>
                            <td width="1%">:</td>
                            <td width="35%">
                                <asp:TextBox ID="txtKm" runat="server" Width="200px" onblur="numericOnlyBlur(txtKm)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" width="14%"></td>
                            <td width="1%"></td>
                            <td style="width: 262px" width="35%">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="70px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgMSPExMaster" runat="server" Width="100%" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" PageSize="25">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Model Kendaraan">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileModel" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VechileTypeCode">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileTypeCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe MSP Extended">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSPExType" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl. Mulai Berlaku">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Berlaku Sampai Tanggal">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Duration" HeaderText="Durasi(Thn)">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="KM">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblKm"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Amount" HeaderText="Harga">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PPN">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPPN" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Harga (Incl. PPN)">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblHargaInclPPN" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="20px"></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

