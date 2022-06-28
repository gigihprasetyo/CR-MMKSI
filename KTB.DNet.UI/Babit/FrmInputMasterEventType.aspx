<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputMasterEventType.aspx.vb" Inherits="FrmInputMasterEventType" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPengajuanBabit</title>
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
        <input id="hdnBabitMasterEventTypeID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MASTER - INPUT JENIS KEGIATAN</td>
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
                            <td class="titleField" style="width: 146px">Kode Kegiatan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtTypeCode" MaxLength="10" Width="110px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Kategori Kegiatan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtTypeName" MaxLength="50" Width="20%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr id="trFormType" runat="server" style="display:none">
                            <td class="titleField" style="width: 146px">Tipe Form</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlFormType" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Status</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div></div>
        <div>
            <hr />
        </div>

        <asp:Button ID="btnBaru" runat="server" Text="Baru" TabIndex="150" Width="60px" ></asp:Button>
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" Width="60px" runat="server" Text="Simpan"></asp:Button>&nbsp;
        <asp:Button ID="btnDelete" OnClientClick="return confirm('Anda yakin mau delete?');" style="display:none" runat="server" Width="60px" Text="Delete" TabIndex="165"></asp:Button>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari" TabIndex="160"></asp:Button>
        <br />
        <div>
            <hr />
        </div>


        <div style="width: 70%">
            <div runat="server" id="DivList" style="width: 70%">
                <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                    <asp:Label ID="lblJudulList" runat="server" Font-Size="15px" Text="List Jenis Kegiatan" Font-Bold="True"></asp:Label>
                </div>
                <asp:DataGrid ID="dgBabitMasterEventTypeList" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                    PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                    CellPadding="3" DataKeyField="ID">
                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kode Kegiatan" SortExpression="TypeCode">
                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTypeCode" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Kategori Kegiatan" SortExpression="TypeName">
                            <HeaderStyle Width="50%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTypeName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tipe Form" SortExpression="FormType">
                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                            <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" style="display:none"></asp:Label>
                                <img  id="imgActif" runat="server" alt="Aktif" src="../images/aktif.gif" border="0">
                                <img  id="imgNonActif" runat="server" alt="Non-aktif" src="../images/in-aktif.gif" border="0">
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
                                <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');"  runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
								    <img alt="Delete"  src="../images/trash.gif" border="0"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
        </div>
        </div>
    </form>
</body>
</html>
