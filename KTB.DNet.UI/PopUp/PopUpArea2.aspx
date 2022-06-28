<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpArea2.aspx.vb" Inherits="PopUpArea2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>PopUpArea2</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
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
		
		function GetSelectedArea()
		{
			var table;
			table = document.getElementById("dtgArea");
			var Area ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Area == '')
						{
							Area = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Area = Area + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Area;
					}
					else if (navigator.appName == "Netscape") {
					    if (Area == '') {
					        Area = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        Area = Area + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(Area);
					}

					else
					{
						if (Area == '')
						{
							Area = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Area = Area + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(Area);
					}
				}
			}
			
			window.close();
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Selection - Area 2</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><img src="../images/bg_hor.gif" height="1" border="0"></td>
				</tr>
				<tr>
					<td height="10"><img src="../images/dot.gif" height="1" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 390px"><asp:datagrid id="dtgArea" runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
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
									<asp:BoundColumn DataField="AreaCode" SortExpression="AreaCode" HeaderText="Kode Region">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Nama Region">
										<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedArea()" type="button" value="Pilih"
							name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
