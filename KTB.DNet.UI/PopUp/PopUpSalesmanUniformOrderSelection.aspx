<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSalesmanUniformOrderSelection.aspx.vb" Inherits="PopUpSalesmanUniformOrderSelection" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmOrderNoSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelected()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgOrderNoSelection");
			var OrderNo ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (OrderNo == '')
						{
							OrderNo = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							OrderNo = OrderNo + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = OrderNo;
					bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (OrderNo == '') {
					        OrderNo = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        OrderNo = OrderNo + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(OrderNo);
					    bcheck = true;
					}
					else
					{	
						if (OrderNo == '')
						{
							OrderNo = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							OrderNo = OrderNo + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(OrderNo);
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
				alert("Silahkan Pilih No Order terlebih dahulu");	
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
								<td class="titlePage" colSpan="7">SALESMAN - Order No</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR valign="top">
								<TD class="titleField" width="20%" style="HEIGHT: 20px">Kode&nbsp;Dealer</TD>
								<TD width="1%" style="HEIGHT: 20px">:</TD>
								<TD width="25%" style="HEIGHT: 20px"><asp:textbox id="txtDealerCode" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" width="20%" style="HEIGHT: 20px"></TD>
								<TD width="1%" style="HEIGHT: 20px"></TD>
								<TD width="33%" style="HEIGHT: 20px"></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 13px">No Order</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNoOrder" onblur="omitSomeCharacter('txtNoOrder','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgOrderNoSelection" runat="server" Width="100%" AutoGenerateColumns="False"
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="White"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="OrderNumber" HeaderText="No Order">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id ="lblOrderNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderNumber") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="OrderDate" HeaderText="Tanggal Order">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id ="lblOrderDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.OrderDate"),"dd/MM/yyyy") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelected()" type="button"
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
