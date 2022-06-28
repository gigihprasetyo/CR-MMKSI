<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventLaporanPenjualanExcel.aspx.vb" Inherits="FrmEventLaporanPenjualanExcel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmEventLaporanPenjualanExcel</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" valign="top">
                        <h4><%= GetNamaKegiatan  %></h4>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <h5><%= GetPeriode %></h5>
                    </td>
                </tr>
                <TR vAlign="top">
                    <TD class="titleField">
                        <asp:datagrid id="dtgExcel" runat="server" Width="100%" PageSize="2" AllowSorting="True" CellPadding="3" ShowFooter="True"
                            BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False">
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
										<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DealerName" HeaderText="Nama Dealer/Group Dealer">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid>
                    </TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
