<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpAplikasiSPLSelection.aspx.vb" Inherits="PopUpAplikasiSPLSelection"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>PopUp Aplikasi SPL Selection</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function getSelectedSPL()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgSPL");
			for (i = 1; i < table.rows.length; i++)
			{
				var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (RadioButton != null && RadioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						var Course = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText;
						window.returnValue = Course;
						bcheck=true;
						break;
					}
					else if (navigator.appName == "Netscape") {
					    var Course = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText;
					    window.opener.dialogWin.returnFunc(Course);
					    bcheck = true;
					    break;
					}
					else
					  {
			  			var Course = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
					  	window.opener.dialogWin.returnFunc(Course);
					  	bcheck=true;
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
				alert("Silahkan pilih Nomor SPL");	
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="500px" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="700px" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									Pencarian Nomor Aplikasi</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="15%">Nomor SPL</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtSPLNumber" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="15%">Nama Customer</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtCustomerName" runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR align="center">
								<TD colspan="8">
									<hr /></TD>
							</TR>
							<TR align="center">
								<TD colspan="7">
									<asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button></TD>
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
						</TABLE>
					</td>
				</tr>
			</TABLE>

            <table width="100%">
                <tr>
                    <td>
                        <asp:datagrid id="dtgSPL" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
					        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="False">
					        <FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
					        <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
					        <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
					        <ItemStyle BackColor="White"></ItemStyle>
					        <HeaderStyle VerticalAlign="Top"></HeaderStyle>
						        <Columns>
							        <asp:TemplateColumn>
								        <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
							        </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                        <ItemStyle Width="1%" CssClass="hiddencol"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

							        <%--<asp:BoundColumn DataField="ID" HeaderText="ID">
								        <HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                        <ItemStyle Width="1%" CssClass="hiddencol" />
							        </asp:BoundColumn>--%>

                                    <asp:TemplateColumn HeaderText="Nomor SPL">
                                        <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSPLNumber" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

							        <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="DealerName">
								        <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
								        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
								        <ItemTemplate>
									        <asp:Label id="lblDealerName" runat="server"></asp:Label>
								        </ItemTemplate>
							        </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Nama Customer">
                                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
						        </Columns>
						    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
					    </asp:datagrid>
                    </td>
                </tr>
				<TR>
					<TD align=center><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedSPL()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
            </table>
		</form>
	</body>
</HTML>
