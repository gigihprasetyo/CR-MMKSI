<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUPItemNoSelection.aspx.vb" Inherits="PopUPItemNoSelection" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>PopUPItemNoSelection</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
		
		function GetSelectedItem()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgItemSelection");
			var Item ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Item == '')
						{
							Item = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Item = Item + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Item;
					bcheck=true;
					}
					else
					{
						if (Item == '')
						{
							Item = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Item = Item + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(Item);
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
				alert("Silahkan Pilih Item terlebih dahulu");	
			  }
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="tblItemNo" width="100%" cellpadding=2 cellspacing=1>
				<tr>
					<td class="titlePage">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 16px">Daftar  Item</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>					
					</td>
				</tr>
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 400px">
						<asp:datagrid id="dtgItemSelection" runat="server" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
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
								<asp:TemplateColumn HeaderText="Item No">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Descrption" HeaderText="Keterangan">
									<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></div>
					</td>
				</tr>
				<TR>
					<TD align=center><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedItem()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
