<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSalesmanPartResign.aspx.vb" Inherits="PopUpSalesmanPartResign" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Part Employee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function getSelected()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgSalesmanHeader");
			var retVal ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (RadioButton != null && RadioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					  {
						retVal = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText+ ';' + table.rows[i].cells[5].innerText;
						window.returnValue = retVal;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    retVal = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText + ';' + table.rows[i].cells[5].innerText;
					    opener.dialogWin.returnFunc(retVal);
					    bcheck = true;
					}
					else
					  {
						retVal = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML + ';' + table.rows[i].cells[5].getElementsByTagName("span")[0].innerHTML;
						opener.dialogWin.returnFunc(retVal);
						bcheck=true;
						
					  }	
					break;
				}
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih Salesman");	
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
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">Part Employee Yang Sudah 
									Resign</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%">Kode Part Employee</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSalesmanCode" onblur="omitSomeCharacter('txtSalesmanCode','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 22px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="20%"><asp:button id="btnSearch" runat="server" Width="80px" Text=" Cari "></asp:button></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD style="HEIGHT: 22px" width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">Nama Part Employee</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgSalesmanHeader" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
											CellSpacing="1" ForeColor="GhostWhite" PageSize="25" AllowSorting="True" AllowCustomPaging="True" AllowPaging="True" BorderColor="#CDCDCD"
											BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanCode" HeaderText="Kode Employee">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblSalesmanCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Employee">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id ="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanAdditionalInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName"
													HeaderText="Kategori">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblJobPositionId_Main" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanAdditionalInfo.SalesmanCategoryLevel.PositionName" HeaderText="Posisi">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPosisi" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelected()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
		<!--<asp:TemplateColumn HeaderText="Kode Posisi">
													<HeaderStyle CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblJobPosition" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deskripsi Posisi">
													<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblJobPositionDesc" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblLevel2" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>-->
	</body>
</HTML>
