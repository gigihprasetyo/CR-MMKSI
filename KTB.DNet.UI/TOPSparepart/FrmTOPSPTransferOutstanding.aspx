<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPTransferOutstanding.aspx.vb" Inherits=".FrmTOPSPTransferOutstanding" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTOPSPTransferOutstanding</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript">
    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 214px;
        }
        .auto-style3 {
            width: 11px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">TOP SparePart – Report TOP COD</td>
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
                    <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label3" runat="server">Kode Bank</asp:Label></td>
                            <td class="auto-style3">
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td>&nbsp;
                                <asp:DropDownList ID="ddlBankCode" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">No Registrasi</td>
                            <td class="auto-style3">:</td>
                            <td>&nbsp;
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoReg" onblur="omitSomeCharacter('txtNoReg','<>?*%$;')"
                                    runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Nomor TR</td>
                            <td class="auto-style3">:</td>
                            <td>&nbsp;
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoTR" onblur="omitSomeCharacter('txtNoTR','<>?*%$;')"
                                    runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Tipe Transaksi</td>
                            <td class="auto-style3">:</td>
                            <td>&nbsp;
                                <asp:DropDownList ID="ddlTransaksi" runat="server">
                                        <asp:ListItem  Text="Silakan Pilih" Value="-1"></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="TOP" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="COD" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Keterangan</td>
                            <td class="auto-style3">:</td>
                            <td class="auto-style1">&nbsp;
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKeterangan" onblur="omitSomeCharacter('txtKeterangan','<>?*%$;')"
                                    runat="server" TextMode="MultiLine" Height="72px" Width="200px"></asp:TextBox></td>
                            
                            <td class="auto-style1">Total Amount Transfer</td>
                            <td class="auto-style3">:</td>
                            <td><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label5" runat="server">Tanggal Transfer</asp:Label></td>
                            <td class="auto-style3">
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td>
                                <table cellspacing="0" cellpadding="2" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTransferDateStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTransferDateEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="auto-style1"></td>
                            <td class="auto-style3"></td>
                            <td>&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="btnDownload" runat="server" Width="100px" Text="Download"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="height: 300px">
                        <asp:DataGrid ID="dgSPOutstanding" runat="server" Width="100%" AllowCustomPaging="True" CellSpacing="1"
                            CellPadding="3" AllowPaging="True" PageSize="30" AutoGenerateColumns="False" BorderWidth="0px" BorderColor="Gainsboro" BackColor="Gainsboro"
                            AllowSorting="True">
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle Font-Bold="True" Height="30px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Bank">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeBank" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Reff Bank">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReffBank" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="No Registrasi">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Tgl Transfer">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglTransfer" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Jml Transfer">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlTransfer" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Narrative/Keterangan">
                                    <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKet" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="No TR">
                                    <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoTR" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Transaksi">
                                    <HeaderStyle Width="7%" CssClass="titleTableParts" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransaksi" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
            </tr>
        </table>
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
