<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpBabitAllocationProposal.aspx.vb" Inherits="PopUpBabitAllocationProposal" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>BABIT - Babit Proposal</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <base target="_self">
        <script language="javascript">
		function ViewForm()
		{
		}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
                <tr>
                    <td>
                        <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                            <tr>
                                <td style="FONT-WEIGHT: bold" colSpan="7">Dana Babit :
                                    <asp:label id="lblDanaBabit" Runat="server"></asp:label></td>
                            </tr>
                            <tr>
                                <td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                            </tr>
                            <tr>
                                <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                            </tr>
                            <TR>
                                <TD colSpan="7">
                                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 380px">
                                        <asp:datagrid id="dtgBabitProposal" runat="server" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
                                            PageSize="25">
                                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tanggal Persetujuan">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblTgl" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:label ID="lblTotal" Font-Bold="True">Total</asp:label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="False" HeaderText="No Pengajuan">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton id="lblNoPengajuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPengajuan")%>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn visible="true" HeaderText="No Persetujuan">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblNoPersetujuan" runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.NoPersetujuan") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Jumlah Persetujuan">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblJmlPengajuan" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.KTBApprovalAmount"),"#,##0") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:Label id="lblTotalPengajuan" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Sisa Babit">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblSisaBabit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:datagrid></div>
                                </TD>
                            </TR>
                            <TR>
                                <TD align="center">
                                    <INPUT id="btnPrint" style="WIDTH: 60px" onclick="window.print()" type="button" value="Cetak" name="btnPrint">
                                    <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel">
                                </TD>
                            </TR>
                        </TABLE>
                    </td>
                </tr>
            </TABLE>
        </form>
    </body>
</HTML>
