<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallInformation.aspx.vb" Inherits=".FrmRecallInformation" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Recall - Informasi Kendaraan</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function firstFocus() {
            var txtChassisNumber = document.getElementById('txtChassisNumber');
            txtChassisNumber.focus();
        }


    </script>
</head>
<body onload="firstFocus();" ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                <tr>
                    <td colspan="6">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="titlePage">Field Fix Campaign - Informasi Kendaraan</td>
                            </tr>
                            <tr>
                                <td background="../images/bg_hor.gif" height="1">
                                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
                            </tr>
                            <tr>
                                <td height="10">
                                    <img height="1" src="../images/dot.gif" border="0"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="titleField" width="24%">Nomor Rangka</td>
                    <td width="1%">:</td>
                    <td width="25%">
                        <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtChassisNumber" runat="server" MaxLength="20" Width="174px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvChasisNumber" runat="server" ErrorMessage="*" ControlToValidate="txtChassisNumber">*</asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="50px"></asp:Button></td>
                </tr>

                <tr>
                    <td class="titleField" width="24%">Model / Tipe / Warna</td>
                    <td width="1%">:</td>
                    <td width="25%">
                        <asp:Label ID="lblMaterial" runat="server"></asp:Label></td>
                    <td class="titleField" width="20%">&nbsp;</td>
                    <td width="1%">&nbsp;</td>
                    <td width="29%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="titleField">No Rangka</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblNoChassis" runat="server"></asp:Label></td>
                    <td class="titleField">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="titleField">
                        <asp:Label ID="lblNoEngineTitle" runat="server" Text="No Mesin"></asp:Label>

                    </td>

                    <td>
                        <asp:Label ID="lblNoEngineColon" runat="server" Text=":"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblNoEngine" runat="server"></asp:Label></td>
                    <td class="titleField">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="titleField">Dealer Alokasi</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblDealerSold" runat="server"></asp:Label></td>
                    <td class="titleField">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="titleField" style="vertical-align: text-top">Field Fix Reg No/ Service Bulletin No</td>
                    <td style="vertical-align: text-top">:</td>
                    <td style="vertical-align: text-top; padding-left: 0px; margin-left: 0px;">
                        <asp:Literal ID="ltrListRCM" runat="server"></asp:Literal>
                    <td class="titleField">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td style="height: 10px" colspan="6"></td>
                </tr>
                <tr id="trFreeServiceTitle">
                    <td colspan="6"><em><strong><font size="2">Service Data Campaign</font></strong></em></td>
                </tr>
                <tr id="trServiceData">
                    <td colspan="6">
                        <div id="div1" style="overflow: auto">
                            <asp:DataGrid ID="dtgServiceData" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                PageSize="50" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle BackColor="White"></ItemStyle>
                                <HeaderStyle ForeColor="White"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Dealer Pelaksana">
                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh" DataFormatString="{0:#,###}">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tanggal Pengerjaan">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tanggal Proses">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTglPro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Field Fix Reg No">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecallRegNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.RecallRegNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Deskripsi">
                                        <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.Description") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="false">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Text="Del" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                </Columns>
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>

            </table>
        </div>
    </form>
    <script language="javascript">

        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
