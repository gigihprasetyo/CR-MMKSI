<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpBabitAlocation.aspx.vb" Inherits="PopUpBabitAlocation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>Babit Alocation</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <base target="_self">
        <script language="javascript">
		
		function GetSelected()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgBabitAllocation");
			var selected ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (selected == '')
						{
							selected = replace(table.rows[i].cells[3].innerText,' ','');
						}
					window.returnValue = selected;
					bcheck=true;
					} else if (navigator.appName == "Netscape") {
					    if (selected == '') {
					        selected = replace(table.rows[i].cells[3].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(selected);
					    bcheck = true;
					}
					else
					{
						if (selected == '')
						{
							selected = replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(selected);
					bcheck=true;
					}
				}
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih Alokasi Babit terlebih dahulu");	
			  }
		}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" colSpan="7">BABIT -&nbsp;Alokasi Babit</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD>
                        <TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <TD class="titleField">Kode Dealer</TD>
                                <TD>:</TD>
                                <TD><asp:textbox id="txtKodeDealer" runat="server" Enabled="False"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField">Periode</TD>
                                <TD>:</TD>
                                <TD><asp:dropdownlist id="ddlStartMonth" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlPeriod" runat="server"></asp:dropdownlist>s/d
                                    <asp:dropdownlist id="ddlEndMonth" runat="server"></asp:dropdownlist>
                                    <asp:dropdownlist id="ddlPeriodEnd" runat="server"></asp:dropdownlist></TD>
                            </TR>
                            <TR>
                                <TD class="titleField">No Perjanjian</TD>
                                <TD>:</TD>
                                <TD><asp:textbox id="txtNoPerjanjian" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                        onblur="omitSomeCharacter('txtNoPerjanjian','<>?*%$;')"></asp:textbox></TD>
                            </TR>
                            <TR>
                                <TD class="titleField"></TD>
                                <TD></TD>
                                <TD><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
                            </TR>
                            <TR>
                                <TD colSpan="5">
                                    <DIV id="div1" style="OVERFLOW: auto; HEIGHT: 280px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgBabitAllocation" runat="server" Width="100%" BorderColor="#E0E0E0" CellPadding="3"
                                            BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                            <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        &nbsp;
                                                    </HeaderTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="NoPerjanjian" HeaderText="No Perjanjian">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblNoPerjanjian" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPerjanjian") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="PC" HeaderText="PC">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblPC" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PC"),"#,##0") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="LCV" HeaderText="LCV">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblLCV" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.LCV"),"#,##0") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="CV" HeaderText="CV">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblCV" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CV"),"#,##0") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Dana Babit">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label id="lblDanaBabit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:datagrid></DIV>
                                </TD>
                            </TR>
                        </TABLE>
                    </TD>
                </TR>
                <TR>
                    <TD align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelected()" type="button"
                            value="Pilih" name="btnChoose" runat="server"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
                            name="btnCancel"></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
