<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListDealerActivityBabit.aspx.vb" Inherits="FrmListDealerActivityBabit" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmListDealerActivityBabit</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',540,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;			
			}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 19px">BABIT - Daftar Rencana Aktivitas</td>
                </tr>
                <tr>
                    <td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD align="left">
                        <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField" width="24%">Kode Dealer</TD>
                                <TD width="1%">:</TD>
                                <td width="75%"><asp:textbox id="txtDealerCode" runat="server" Width="80px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"></asp:textbox>
                                    <asp:label id="lblDealers" runat="server">
                                        <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:label></td>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 27px" width="24%">Aktivitas BABIT</TD>
                                <TD style="HEIGHT: 27px" width="1%">:</TD>
                                <td style="HEIGHT: 27px" width="75%">
                                    <asp:TextBox id="txtBabitActivity" runat="server" Width="288px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtBabitActivity','<>?*%$')"></asp:TextBox></td>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 16px" width="24%">Periode</TD>
                                <TD style="HEIGHT: 16px" width="1%">:</TD>
                                <TD style="HEIGHT: 16px" width="75%">
                                    <asp:DropDownList id="ddlStartPeriod" runat="server"></asp:DropDownList>
                                    <asp:dropdownlist id="ddlTahun" Runat="server"></asp:dropdownlist>s/d
                                    <asp:DropDownList id="ddlEndPeriod" runat="server"></asp:DropDownList>
                                    <asp:dropdownlist id="ddlTahunTo" Runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
                                <TD style="HEIGHT: 27px" width="1%"></TD>
                                <td style="HEIGHT: 27px" width="75%">
                                    <asp:Button id="btnCari" runat="server" Text="Cari" Width="64px"></asp:Button></td>
                            </TR>
                        </TABLE>
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dtgActivityPlanning" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                                AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#FFFFFF" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="NO">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblNo" Runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Periode">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblStartPeriode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartPeriodMonth") %>'>
                                            </asp:Label>
                                            -
                                            <asp:Label id="lblEndPeriode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndPeriodMonth") %>'>
                                            </asp:Label>
                                            &nbsp;
                                            <asp:Label id="lblPeriodYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PeriodYear") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ActivityPlanning" HeaderText="Nama File Rencana Aktivitas">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lbtnActivityPlan" runat="server" CommandArgument = '<%# DataBinder.Eval(Container, "DataItem.ActivityPlanning") %>' CommandName="Download">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Simpan">
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label id="lblCreatedTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreatedTime"),"dd/MM/yyyy") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
                                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                            <asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CommandName="Delete" CausesValidation="False">
                                                <img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label id=lblID Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:datagrid></div>
                    </TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
