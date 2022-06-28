<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpFleetCustomer.aspx.vb" Inherits="PopUpFleetCustomer"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>PopUpFleetCustomer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function getSelectedFleetCust()
		{
			var table;
			var bcheck = false;
			var FleetCust = '';
			table = document.getElementById("dtgFleetCustHdr");
			for (i = 1; i < table.rows.length; i++)
			{
				var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (RadioButton != null && RadioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					  {
					    FleetCust = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText;
						window.returnValue = FleetCust;
						bcheck=true;
						break;
					}
					else if (navigator.appName == "Netscape") {
					    FleetCust = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText;
					    window.opener.dialogWin.returnFunc(FleetCust);
					    bcheck = true;
					    break;
					}
					else
					  {
					    FleetCust = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML;
					    window.opener.dialogWin.returnFunc(FleetCust);
					    bcheck = true;
						break;
					  }	
				}
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih Konsumen");	
			  }	
		}
		</script>

        <style type="text/css">
          .hiddencol
          {
            display: none;
          }
        </style>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <asp:HiddenField ID="hdnCustType" runat="server" />
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									FLEET KONSUMEN&nbsp;- Pencarian Konsumen</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%">Nama Konsumen</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtName" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 22px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="20%">Kode Fleet Konsumen</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="33%"><asp:textbox id="txtCode" runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR align="center">
								<TD style="HEIGHT: 13px" colspan="7" valign="middle"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
                            <tr>
                                <td colspan="7"><hr /></td>
                            </tr>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgFleetCustHdr" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
											CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowCustomPaging="True"
											AllowSorting="True" PageSize="1000" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												</asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="ID">
											    <HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                                <ItemStyle Width="1%" CssClass="hiddencol"></ItemStyle>
											    <ItemTemplate>
												    <asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
												    </asp:Label>
											    </ItemTemplate>
											    </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="FleetCode" HeaderText="Kode">
											    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
											    <ItemTemplate>
												    <asp:Label id="lblFleetCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetCode")%>'>
												    </asp:Label>
											    </ItemTemplate>
											    </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="FleetCustomerName" HeaderText="Nama">
											    <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
											    <ItemTemplate>
												    <asp:Label id="lblFleetCustomerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetCustomerName")%>'>
												    </asp:Label>
											    </ItemTemplate>
											    </asp:TemplateColumn>

												<%--<asp:BoundColumn DataField="ID" HeaderText="ID">
													<HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                                    <ItemStyle Width="1%" CssClass="hiddencol" />
												</asp:BoundColumn>--%>

												<%--<asp:BoundColumn DataField="FleetCode" SortExpression="FleetCode" HeaderText="Kode">
													<HeaderStyle Width="15%" CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>--%>

												<%--<asp:BoundColumn DataField="FleetCustomerName" SortExpression="FleetCustomerName" HeaderText="Nama">
													<HeaderStyle Width="20%" CssClass="titleTableSales" HorizontalAlign="Left"></HeaderStyle>
												</asp:BoundColumn>--%>

                                                <asp:TemplateColumn HeaderText="Domain Bisnis">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDomainBisnis" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Bisnis Sektor">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBisnisSektor" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align=center colspan=7><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedFleetCust()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
