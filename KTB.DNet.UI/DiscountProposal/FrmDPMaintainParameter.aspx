<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDPMaintainParameter.aspx.vb" Inherits=".FrmDPMaintainParameter" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Mantain Parameter</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>

    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">

    </script>
    <script language="javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                <td class="titlePage">DISCOUNT PROPOSAL - Maintain Parameter</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1" colspan="2">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10" colspan="2">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="titlefield" width="20%">Tipe</td>
                                <td style="padding-right:0px" width="1%">
                                    <asp:Label ID="Label19" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" Width="150px" ></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr valign="top">
                                <td class="titlefield" width="20%">Nama Parameter</td>
                                <td style="padding-right:0px" width="1%">
                                    <asp:Label ID="Label6" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtParameterName" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr valign="top">
                                <td class="titlefield" width="20%">Status</td>
                                <td style="padding-right:0px" width="1%">
                                    <asp:Label ID="Label1" runat="server">:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCari" runat="server" Text="Cari" Width="100px" />
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" Width="100px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" Width="100px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <br />
    <br />
    <asp:Panel ID="Panel2" runat="server">
        <asp:DataGrid ID="dtgParameter" runat="server" Width="60%" AllowPaging="True" AllowSorting="True"
                    PageSize="15" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="1px"
                    CellPadding="3" DataKeyField="ID">
                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="ID" Visible="false">
                            <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Tipe">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Nama Parameter">
                            <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblParamName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton id="lbtnNonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Activate">
									<img src="../images/in-aktif.gif" border="0" alt="Klik untuk Aktifkan data"></asp:LinkButton>
								<asp:LinkButton id="lbtnActive" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
									<img src="../images/aktif.gif" border="0" alt="Klik untuk non-Aktifkan data"></asp:LinkButton>                       
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
    </asp:Panel>    
    </form>
</body>
</html>
