<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSettingBabitPrice.aspx.vb" Inherits="FrmSettingBabitPrice" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSettingBabitPrice</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnIsExsistModel" type="hidden" value="-1" runat="server">
        <input id="hdnBabitMasterPriceID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MASTER - SETTING HARGA BABIT</td>
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
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kategori/ Model</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="90px" AutoPostBack="true"></asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:DropDownList ID="ddlSubCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Periode&nbsp;
                                <asp:CheckBox ID="chkConfirmPeriod" runat="server"></asp:CheckBox></td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Harga/ Unit</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtUnitPrice" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                    onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Status</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Babit Kategori Spesial</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlSpecialCategoryFlag" Width="100px" runat="server" /></td>
                        </tr>
                        <tr id="trBabitSpecial" style="display:none">
                            <td class="titleField" style="width: 146px">Babit Spesial</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlSpecialFlag" Width="100px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <hr />
        </div>
        <div id="divNavigationButton" runat="server">
            <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" Width="60px" runat="server" Text="Simpan"></asp:Button>&nbsp;
            <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px"></asp:Button>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button>
            <hr />
        </div>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" style="width: 146px">Upload Data</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                                    name="DataFile" runat="server">
                                &nbsp;&nbsp;<asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Download Template</asp:LinkButton><br />
                                Minimum Excel 2007 (*.xls / *.xlsx)
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td width="80%">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="70px"></asp:Button>&nbsp;
                                <asp:Button ID="btnSaveUpload" runat="server" Text="Simpan" Width="70px"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <hr />
        </div>

        <div runat="server" id="DivList" style="width: 80%">
            <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                <asp:Label ID="lblJudulList" runat="server" Font-Size="15px" Text="List Setting Harga Babit" Font-Bold="True"></asp:Label>
            </div>
            <asp:DataGrid ID="dgBabitMasterPriceList" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                CellPadding="3" DataKeyField="ID">
                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model" SortExpression="SubCategoryVehicle.ID">
                        <HeaderStyle Width="150px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSubCategoryVehicleName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubCategoryVehicle.Name")%>'>></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Harga/Unit" SortExpression="UnitPrice">
                        <HeaderStyle Width="140px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.UnitPrice"), "#,##0")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Awal" SortExpression="ValidFrom">
                        <HeaderStyle ForeColor="White" Width="140px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblValidFrom" NAME="lblValidFrom" runat="server" Text='<%#Format(DataBinder.Eval(Container, "DataItem.ValidFrom"), "dd/MM/yyyy")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Akhir" SortExpression="ValidTo">
                        <HeaderStyle ForeColor="White" Width="140px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblValidTo" NAME="lblValidFrom" runat="server" Text='<%#Format(DataBinder.Eval(Container, "DataItem.ValidTo"), "dd/MM/yyyy")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Babit Kategori Spesial" SortExpression="SpecialCategoryFlag">
                        <HeaderStyle Width="60px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSpecialCategoryFlag" runat="server" Enabled="false" ></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--<asp:TemplateColumn HeaderText="Babit Spesial" SortExpression="SpecialFlag">
                        <HeaderStyle Width="60px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSpecialFlag" runat="server" Enabled="false" ></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle Width="60px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Style="display: none"></asp:Label>
                            <img id="imgActif" runat="server" alt="Aktif" src="../images/aktif.gif" border="0">
                            <img id="imgNonActif" runat="server" alt="Tidak Aktif" src="../images/in-aktif.gif" border="0">
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status Upload">
                        <HeaderStyle Width="160px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusUpload" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								    <img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
								    <img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" runat="server" Width="20px" Text="Hapus"
                                CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
								    <img alt="Delete"  src="../images/trash.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
