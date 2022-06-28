<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPUploadPayment.aspx.vb" Inherits=".FrmTOPSPUploadPayment" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recall Service</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script type="text/javascript" language="javascript">
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">TOP Spare Part - Upload Payment Status</td>
                </tr>
                <tr>
                    <td height="1" background="../images/bg_hor.gif">
                        <img border="0" src="../images/bg_hor.gif" height="1"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img border="0" src="../images/dot.gif" height="1"></td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tr>
                                <td class="titleField">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerCOde" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Nama Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="20%">Lokasi File</td>
                                <td width="1%">:</td>
                                <td width="79%" colspan="4">
                                    <input id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="60px"></asp:Button>
                                    <asp:Button ID="btnDonloadTmp" runat="server" Text="Download Template" Width="120px"></asp:Button>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="titleField">&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div style="overflow: auto; height: 330px" id="div1">
                            <asp:DataGrid ID="dgTopPartUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                                BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                                Visible="False" CellSpacing="1" AllowSorting="false">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="RegNumber" HeaderText="No Reg">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegNumber")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="KodeDealer" HeaderText="Dealer Code">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KodeDealer")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <%--<asp:TemplateColumn SortExpression="NoBilling" HeaderText="Billing Number">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoBilling")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>--%>
                                   
                                    <asp:TemplateColumn SortExpression="TransferAmount" HeaderText="Total Amount">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TransferAmount")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="TotalKliring" HeaderText="Kliring Amount">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalKliring")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="KliringDate" HeaderText="Kliring Date (ddmmyyyy)">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KliringDate", "{0:dd/MM/yyyy}")%>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="DocClearing" HeaderText="No TR">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocClearing")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                    

                                    <asp:TemplateColumn SortExpression="ActualDate" HeaderText="Actual Date (ddmmyyyy)">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActualDate", "{0:dd/MM/yyyy}")%>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ReffBank" HeaderText="Ref Bank">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReffBank")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" ReadOnly="True" HeaderText="Status">
                                        <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    </asp:BoundColumn>


                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnStore" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:Button>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>                
            </table>
        </div>
    </form>


    <script type="text/C#" language="javascript">
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

