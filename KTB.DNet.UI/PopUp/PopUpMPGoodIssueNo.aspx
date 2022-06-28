<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpMPGoodIssueNo.aspx.vb" Inherits="PopUpMPGoodIssueNo" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMPGoodIssueNo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
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
			
			function GetSelectedGINo()
			{
				var table;
				var bcheck =false;
				table = document.getElementById("dtgMPGINo");
				var GINo ='';
				for (i = 1; i < table.rows.length; i++)
				{
					var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (CheckBox != null && CheckBox.checked)
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{
							if (GINo == '')
							{
								GINo = replace(table.rows[i].cells[3].innerText,' ','');
							}
							else
							{
								GINo = GINo + ';' + replace(table.rows[i].cells[3].innerText,' ','');
							}
						window.returnValue = GINo;
						bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    if (GINo == '') {
						        GINo = replace(table.rows[i].cells[3].innerText, ' ', '');
						    }
						    else {
						        GINo = GINo + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
						    }
						    opener.dialogWin.returnFunc(GINo);
						    bcheck = true;
						}
						else
						{
							if (GINo == '')
							{
								GINo = replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							else
							{
								GINo = GINo + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
							}
						opener.dialogWin.returnFunc(GINo);
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
					alert("Silahkan pilih Nomor GI terlebih dahulu");	
				}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">Material Promotion - Good Issue No</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%" style="WIDTH: 118px">No. Good Issue</TD>
								<TD width="1%" style="WIDTH: 5px">:</TD>
								<TD width="75%"><asp:textbox id="txtGINo" runat="server" Width="228px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtGINo','<>?*%$;')"></asp:textbox></TD>
							<TR>
								<TD class="titleField" width="24%" style="WIDTH: 118px"></TD>
								<TD width="1" style="WIDTH: 5px"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgMPGINo" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
								BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True" AllowCustomPaging="True" AllowPaging="True" PageSize=25>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer Code">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblDealerCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Permintaan">
										<HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoPermintaan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RequestNo") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NoGI" HeaderText="No Good Issue">
										<HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblGINo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoGI") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="IsValidate" HeaderText="Validasi" Visible="False"></asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages" HorizontalAlign=Right></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center">
						<br>
						<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedGINo()" type="button" value="Pilih"
							name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
