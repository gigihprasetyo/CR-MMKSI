<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSPKHeader.aspx.vb" Inherits="PopUpSPKHeader" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp SPK</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		function trim(str)
		{
			if(!str || typeof str != 'string')
				return null;

			return str.replace(/^[\s]+/,'').replace(/[\s]+$/,'').replace(/[\s]{2,}/,' ');
		}		
		
		function GetSelectedSPK()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgSPK");
			var spknumber ='';
			for (i = 1; i < table.rows.length; i++){
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
							spknumber = trim(table.rows[i].cells[2].innerText)+";"+trim(table.rows[i].cells[10].innerText)+";"+trim(table.rows[i].cells[11].innerText)+";"+trim(table.rows[i].cells[12].innerText)+";"+trim(table.rows[i].cells[13].innerText);
							window.returnValue = spknumber;
							bcheck=true;
							break;
						}
						else if (navigator.appName == "Netscape") {
						    spknumber = trim(table.rows[i].cells[2].innerText) + ";" + trim(table.rows[i].cells[10].innerText) + ";" + trim(table.rows[i].cells[11].innerText) + ";" + trim(table.rows[i].cells[12].innerText) + ";" + trim(table.rows[i].cells[13].innerText);
						    opener.dialogWin.returnFunc(spknumber);
						    bcheck = true;
						    break;
						}
						else
						{	
							if (spknumber == '')
							{
								spknumber = replace(table.rows[i].cells[2].innerHTML,' ','') + ';' + replace(table.rows[i].cells[10].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[11].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[12].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[13].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							window.close();
							opener.dialogWin.returnFunc(spknumber);
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
				alert("Silahkan pilih SPK");	
			  }			
		}

		function ClosePopUp(){
			window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">SPK - Daftar SPK</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">No. Reg. SPK</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtSPKNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtSPKNumber','<>?*%$;')"
										runat="server" Width="230px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">No SPK Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtSPKDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtSPKDealer','<>?*%$;')"
										runat="server" Width="228px"></asp:textbox></TD>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<TD colSpan="7">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
							<asp:datagrid id="dtgSPK" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
								AllowCustomPaging="True" AllowPaging="True" PageSize="25">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
										<ItemTemplate>
											<input type="radio" id="x" name="y" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SPKNumber" SortExpression="SPKNumber" HeaderText="No. Reg. SPK">
										<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerSPKNumber" SortExpression="DealerSPKNumber" HeaderText="No. SPK Dealer">
										<HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tanggal Pengajuan SPK">
										<HeaderStyle width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Periode Pengajuan SPK">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Periode Pengajuan Faktur">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kategori">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "Category.CategoryCode") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Unit">
										<HeaderStyle width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SPKCustomer.Name1" HeaderText="Nama Customer">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPKCustomer.Name1") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Kode Salesman">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSalesmanCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanLevel.Description" HeaderText="Level Salesman">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanLevel.Description") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Posisi Salesman">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</TD>
				<tr>
				<TR>
					<TD align="center" colSpan="7">
						<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedSPK()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp; <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
