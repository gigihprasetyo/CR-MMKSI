<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEventJV.aspx.vb" Inherits=".FrmEventJV" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmEventJV</title>
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
                    <asp:Label ID="lblTitle" runat="server" Text="EVENT - DAFTAR KUITANSI EVENT"></asp:Label></td>
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
                                        <td class="titleField" style="width: 200px; height: 10px">Dealer</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblDealer" runat="server" Width="500px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">No Dokumen</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblNoPengajuanJV" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trNoJV" runat="server">
                                        <td class="titleField" style="width: 200px; height: 10px">No JV</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblNoJV" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">No Referensi Kuitansi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblRefReceipt" runat="server" Width="128px"></asp:Label>
                                            <asp:TextBox ID="txtRefReceipt" runat="server" Width="128px" Maxlength="16" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">Teks Referensi</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblRefText" runat="server" Width="300px"></asp:Label>
                                            <asp:TextBox ID="txtRefText" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table id="Table12" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">Nomor Rekening</td>
                                        <td style="height: 10px" width="4px">
                                            <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                                        <td style="width: 375px; height: 10px">
                                            <asp:Label ID="lblNoRek" runat="server" Width="128px"></asp:Label>
                                            <%--<asp:DropDownList ID="ddlNoRek" runat="server" Width="200px"></asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr id="trProcessDate" runat="server">
                                        <td class="titleField" style="width: 200px; height: 10px">Tgl Proses</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblProcessDate" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">Tgl Cair</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblPencairan" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="width: 200px; height: 10px">Status</td>
                                        <td style="height: 3px" width="4px">
                                            <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                                        <td style="width: 150px; height: 3px">
                                            <asp:Label ID="lblStatus" runat="server" Width="128px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListJV" runat="server" Width="86%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="10" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="1%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Reg Babit">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Kuitansi">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoReceipt" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Acc">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoAcc" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amount Claim">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmountClaim" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amount PPn">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmountPPn" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amount PPh">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmountPPh" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Amount Kuitansi">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblReceiptTotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Teks">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblText" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Simpan"></asp:Button><span style="margin-right: 10px"></span>
                    <asp:Button ID="btnBack" runat="server" Text="Kembali"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
