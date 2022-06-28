<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpUnifDistribution.aspx.vb" Inherits="PopUpUnifDistribution" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpUnifDistribution</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedDistCode()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgUnifDistribution");
			var DistCode ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (DistCode == '')
						{
							DistCode = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							DistCode = DistCode + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = DistCode;
					bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (DistCode == '') {
					        DistCode = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        DistCode = DistCode + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(DistCode);
					    bcheck = true;
					}
					else
					{
						if (DistCode == '')
						{
							DistCode = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							DistCode = DistCode + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(DistCode);
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
				alert("Silahkan Pilih Kode Distribusi terlebih dahulu");	
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
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						SALESMAN UNIFORM - Kode&nbsp;Pesanan
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td class="titleField" width="25%">Kode Pesanan
					</td>
					<td width="1%">:</td>
					<td width="74%">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtOrderNo" runat="server" onblur="omitSomeCharacter('txtOrderNo','<>?*%$;')" MaxLength="15" Width="160px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="titleField" align="center" colspan="3"><asp:button id="btnSearch" runat="server" Text="Cari" Width="56px"></asp:button></td>
				</tr>
				<tr>
					<td colspan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
							<asp:datagrid id="dtgUnifDistribution" runat="server" Width="100%" AutoGenerateColumns="False"
								AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="120" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" ></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanUnifDistribution.SalesmanUnifDistributionCode" HeaderText="Kode Pesanan">
										<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDistributionCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUnifDistribution.SalesmanUnifDistributionCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Barang" Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUnifCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUniformCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td colspan="3" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedDistCode()" type="button"
							value="Pilih" name="btnChoose" runat="server"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
