<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBenefitTypeList.aspx.vb" Inherits="FrmBenefitTypeList" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function clearform() {
            document.getElementById('txtBenefitName').value = '';
            document.getElementById('cbLeasing').checked = false;
            document.getElementById('cbAssy').checked = false;
            document.getElementById('cbReceipt').checked = false;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - TIPE BENEFIT</td>
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
                <td >
                    <asp:Panel ID="formBenefitType" runat="server">
                        <table style="width:100%">
                            <tr>
                                <td class="titleField" width="20%">Tipe Benefit&nbsp;</td>
                                <td>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtBenefitName"
                                        runat="server" Width="242px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Event&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbEvent" runat="server" /></td>

                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Leasing&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbLeasing" runat="server" /></td>

                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Tahun Perakitan&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbAssy" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kuitansi&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbReceipt" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">WS Diskon&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbDiskon" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">&nbsp;</td>
                                <td>
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp;          
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
                                      <!--  <input type="button" value="Batal"  style="width:60px" onclick="clearform()" /> -->
                                    <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>

                        </table>

                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td >

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


                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tipe Benefit">
                                        <HeaderStyle Width="40%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBenefitName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Event">
                                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbEventGrid" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Leasing">
                                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbLeassingGrid" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tahun Perakitan">
                                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbAssyGrid" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Kuitansi">
                                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbReciptGrid" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Diskon">
                                        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDiskonGrid" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Detail"  title="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah"  title="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Hapus"  title="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"
																border="0"></asp:LinkButton>
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
