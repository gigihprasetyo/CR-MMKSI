<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitParameter.aspx.vb" Inherits="FrmInputBabitParameter" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Babit Parameter</title>
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
        <input id="hdnBabitParameterHeaderID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MASTER - SETTING PARAMETER</td>
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
                            <td class="titleField" style="width: 146px">Jenis Pengajuan</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisPengajuan" AutoPostBack="true" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tipe</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlBabitMasterEventType" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kategori Parameter</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlParameterCategory" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Parameter</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtParameterName" MaxLength="30" Width="20%" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Status Parameter</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Wajib Diisi</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlIsMandatory" AutoPostBack="true"  Width="100px" runat="server" />
                                <asp:CheckBox ID="chkIsMandatory" runat="server" style="display:none"></asp:CheckBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div></div>
        <div>
            <hr />
        </div>

        <div id="div1" style="width: 50%" runat="server">
            <div id="divDetailParam" style="width: 50%" runat="server">
            <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                <asp:Label ID="Label3" runat="server" Text="Detail Parameter" Font-Bold="True"></asp:Label>
            </div>
            <asp:DataGrid ID="dgParameterDetail" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Parameter Detail">
                        <HeaderStyle HorizontalAlign="Center" Width="300px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblParameterDetailName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ParameterDetailName")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFParameterDetailName" runat="server" TabIndex="2" Width="400px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEParameterDetailName" runat="server" TabIndex="2" Width="400px" Text='<%#DataBinder.Eval(Container, "DataItem.ParameterDetailName")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="40px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" TabIndex="7" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
                                        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        </div>
        <br />

        <asp:Button ID="btnBaru" runat="server" Text="Tambah" TabIndex="150" ></asp:Button>&nbsp;
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>&nbsp;
        <asp:Button ID="btnDelete" OnClientClick="return confirm('Anda yakin mau delete?');" runat="server" Width="60px" Text="Delete" TabIndex="165"></asp:Button>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari" TabIndex="160"></asp:Button>&nbsp;
        <asp:Button ID="btnDownloadExcel" runat="server" Width="100px" Text="Download Excel" TabIndex="166"></asp:Button>
        <br />
        <div>
            <hr />
        </div>


        <div style="width: 70%">
            <div runat="server" id="DivListParam" style="width: 70%">
                <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                    <asp:Label ID="lblJudulList" runat="server" Text="List Parameter" Font-Bold="True"></asp:Label>
                </div>
                <asp:DataGrid ID="dgListParam" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
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
                        <asp:TemplateColumn HeaderText="Tipe" SortExpression="BabitMasterEventType.ID">
                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBabitMasterEventType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kategori Parameter" SortExpression="ParameterCategory">
                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterCategory" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Parameter" SortExpression="ParameterName">
                            <HeaderStyle Width="50%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Wajib Diisi" SortExpression="IsMandatory">
                            <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblIsMandatory" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                            <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" style="display:none"></asp:Label>
                                <img  id="imgActif" runat="server" alt="Aktif" src="../images/aktif.gif" border="0">
                                <img  id="imgNonActif" runat="server" alt="Non-aktif" src="../images/in-aktif.gif" border="0">
                                <%--<asp:Image id="imgNonActif" border="0" runat="server" src="../images/aktif.gif" alt="Aktif" ></asp:Image>
                                <asp:Image id="imgActif" border="0" runat="server" src="../images/in-aktif.gif" alt="Non-aktif" ></asp:Image>--%>
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
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
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
