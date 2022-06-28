<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSelectingDealer.aspx.vb" Inherits="PopUpSelectingDealer" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpSelectingDealer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			var queryString;
			var ValsReturn='';
			table = document.getElementById("dtgDealerSelection");
			queryString = document.getElementById("lblQueryString");
			if (queryString.value == "true")
			{
				// 18 jul 2007 add by Deddy
				// mengembalikan multiple value
				for (i = 1; i < table.rows.length; i++)
				{
					var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (RadioButton != null && RadioButton.checked)
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{
							ValsReturn = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' 
							+ table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText + ';' 
							+ table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText + ';' 
							+ table.rows[i].cells[7].innerText + ';' + table.rows[i].cells[8].innerText + ';' 
							+ table.rows[i].cells[9].innerText + ';' + table.rows[i].cells[10].innerText + ';'
							+ table.rows[i].cells[11].innerText;
							window.returnValue = ValsReturn;
							bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    ValsReturn = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';'
							+ table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText + ';'
							+ table.rows[i].cells[5].innerText + ';' + table.rows[i].cells[6].innerText + ';'
							+ table.rows[i].cells[7].innerText + ';' + table.rows[i].cells[8].innerText + ';'
							+ table.rows[i].cells[9].innerText + ';' + table.rows[i].cells[10].innerText + ';'
							+ table.rows[i].cells[11].innerText;
						    opener.dialogWin.returnFunc(ValsReturn);
						    bcheck = true;
						}
						else
						{
							
			  				ValsReturn = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';'
							+ table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML + ';'
							+ table.rows[i].cells[5].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[6].getElementsByTagName("span")[0].innerHTML + ';'
							+ table.rows[i].cells[7].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[8].getElementsByTagName("span")[0].innerHTML + ';'
							+ table.rows[i].cells[9].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[10].getElementsByTagName("span")[0].innerHTML + ';'
							+ table.rows[i].cells[11].getElementsByTagName("span")[0].innerHTML;
							opener.dialogWin.returnFunc(ValsReturn);
							bcheck=true;
						}
						break;
					}
				}
			}
			else
			{	
				// untuk yg single value
				for (i = 1; i < table.rows.length; i++)
				{	
					var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (RadioButton != null && RadioButton.checked)
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{
						    ValsReturn = replace(table.rows[i].cells[1].innerText, ' ', '');
							window.returnValue = ValsReturn;
							bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    ValsReturn = replace(table.rows[i].cells[1].innerText, ' ', '');
						    opener.dialogWin.returnFunc(ValsReturn);
						    bcheck = true;
						}
						else
						{
						    //alert('x');
							ValsReturn = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
							//Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
							opener.dialogWin.returnFunc(ValsReturn);
							bcheck=true;
						}
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
				alert("Silahkan pilih dealer");	
			  }
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="6" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage" colSpan="7">DEALER -&nbsp;Pencarian Dealer</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtDealerName" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%">Term Cari 1</TD>
								<TD width="1%">:</TD>
								<TD width="33%"><asp:textbox id="txtSearch1" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtSearch1','<>?*%$;')"
										runat="server"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:dropdownlist id="ddlGroup" runat="server" Width="152px">
										<asp:ListItem Selected="True"></asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px">Term Cari 2</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtSearch2" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtSearch2','<>?*%$;')"
										runat="server"></asp:textbox><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
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
							<TR>
								<TD colSpan="7">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
										<asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
											AllowSorting="True" BorderColor="#CDCDCD" BackColor="#CDCDCD">
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														&nbsp;
													</HeaderTemplate>
													<ItemTemplate>
														&nbsp;
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
													<ItemTemplate>
														<asp:Label id ="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox2" runat="server" Text="" BackColor="Transparent"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox3" runat="server" Text=""></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SearchTerm1" HeaderText="Search 1">
													<ItemTemplate>
														<asp:Label id="lblSearchTerm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SearchTerm2" HeaderText="Search 2">
													<ItemTemplate>
														<asp:Label id="lblSearchTerm2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SearchTerm2") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Address" HeaderText="Alamat">
													<ItemTemplate>
														<asp:Label id="lblAddress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Address") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Phone" HeaderText="Telepon">
													<ItemTemplate>
														<asp:Label id="lblPhone" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Phone") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Fax" HeaderText="Fax">
													<ItemTemplate>
														<asp:Label id="lblFax" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Fax") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Email" HeaderText="Email">
													<ItemTemplate>
														<asp:Label id="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Area2" HeaderText="Area">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblArea" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid><INPUT type="hidden" id="lblQueryString" runat="server"></DIV>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7" height="40" valign="middle"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
