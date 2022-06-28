<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarPengajuanIklan.aspx.vb" Inherits="FrmDaftarPengajuanIklan" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>Maintenance Merek Kompetitor</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
		function ShowDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtStockKodeDealer = document.getElementById("txtDealerCode");
				txtStockKodeDealer.value = tempParam;			
			}
			function KTBNote(selectedCode)
			{
				//Don't Delete
			}		
			
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="FrmSalesmanLevel" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">BABIT - Daftar Pengajuan Iklan</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD align="left">
                        <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" style="WIDTH: 88px" width="88">Kode Dealer</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtDealerCode"
                                        onblur="omitSomeCharacter('txtDeskripsi','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 88px" width="88">Kode Iklan MKS</TD>
                                <TD width="1%">:</TD>
                                <td style="WIDTH: 262px" width="262"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtNamaIklanKTB"
                                        onblur="omitSomeCharacter('txtDeskripsi','<>?*%^():|\@#$;+=`~{}');" Width="200px" Runat="server"></asp:textbox>
                                    <asp:LinkButton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:LinkButton></td>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 88px" width="88">Status</TD>
                                <TD width="1%">:</TD>
                                <TD style="WIDTH: 262px" width="262"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 88px" width="88"></TD>
                                <TD width="1%"></TD>
                                <td style="WIDTH: 262px" width="262"><asp:button id="btnCari" runat="server" width="70px" Text="Cari"></asp:button>&nbsp;
                                    <asp:button id="btnBatal" runat="server" Text="Batal" width="70px"></asp:button></td>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD></TD>
                </TR>
                <TR>
                    <TD>
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgDaftarPengajuanIklan" runat="server" Width="100%" PageSize="10" CellPadding="3"
                                BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
                                AllowSorting="True">
                                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                <ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblNo" Runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NamaIklanKTB" HeaderText="Nama Iklan MMKSI">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lbtnDownloadKTB" runat="server" CommandName="DownloadKTB">
                                                <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                            <asp:linkbutton id="lbtnIklanKTB" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NamaIklanKTB") %>'>
                                            </asp:linkbutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NamaIklanDealer" HeaderText="Nama Iklan Dealer">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lbtnDownloadDealer" runat="server" CommandName="DownloadDealer">
                                                <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                            <asp:linkbutton id="lbtnDescription" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NamaIklanDealer") %>'>
                                            </asp:linkbutton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Contoh Iklan Dealer">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Image ID="imgIklanDealer" Height="50px" Width="50px" Runat="server"></asp:Image>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Catatan MMKSI">
                                        <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCatatanKTB" Runat="server">
                                                <img src="../images/popup.gif" border="0" alt="Catatan MMKSI">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id=lblKeterangan Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Persetujuan MMKSI">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" Visible="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' text="Ubah" Runat="server">
                                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:datagrid></div>
                    </TD>
                </TR>
                <TR>
                    <TD>&nbsp;
                        <asp:button id="btnRilis" runat="server" Width="50px" Text="Rilis"></asp:button></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
