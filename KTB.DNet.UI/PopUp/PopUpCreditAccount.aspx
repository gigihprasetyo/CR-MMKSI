<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCreditAccount.aspx.vb" Inherits="PopUpCreditAccount"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerSelection");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Dealer;
					bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (Dealer == '') {
					        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					  
					    bcheck = true;
					}

					else
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Dealer = Dealer + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						bcheck=true;
					}
				}
			}
			if (bcheck)
			  {
					window.close();
					if(navigator.appName != "Microsoft Internet Explorer")
					{	opener.dialogWin.returnFunc(Dealer);	}
			  }
			else
			  {
				alert("Silahkan Pilih Dealer terlebih dahulu");	
			  }
		}

		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
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
								<td class="titlePage" colSpan="7">DEALER -&nbsp;Pencarian Credit Account</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR vAlign="top">
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Legal Status</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtLegalStatus" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="33%"></TD>
							</TR>
							<TR vAlign="top" style="DISPLAY: none">
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealer" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="33%"></TD>
							</TR>
							<TR vAlign="top" style="DISPLAY: none">
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Nama Dealer</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerName" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="white"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="1px" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="LegalStatus" HeaderText="Legal Status">
													<HeaderStyle Width="100%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblLegalStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LegalStatus") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer" Visible="False">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer" Visible="False">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota" Visible="False">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox2" runat="server" Text="" BackColor="Transparent"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup" Visible="False">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox id="TextBox3" runat="server" Text=""></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1" Visible="False">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm2" HeaderText="Term Cari 2" Visible="False">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
