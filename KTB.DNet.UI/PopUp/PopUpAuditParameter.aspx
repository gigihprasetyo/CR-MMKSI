<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpAuditParameter.aspx.vb" Inherits="PopUpAuditParameter" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpAuditParameter</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Pragma" content="no-cache">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		function getQueryVariable(variable)
		{
			var query = window.location.search.substring(1);
			var vars = query.split("&");
			for (var i=0;i<vars.length;i++)
			{	
				var pair = vars[i].split("=");
				if (pair[0] == variable)
				{
					return pair[1];
				}
			}
			return "nothing";
			//alert('Query Variable ' + variable + ' not found');
		}
		
		function GetSelectedAudit()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgAuditParameterSelection");
			var Audit ='';
			for (i = 1; i < table.rows.length; i++)
			{
						
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
									
						if(getQueryVariable("x") == "Territory")
						{	Audit = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText ;
						}
						else
						{	Audit = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText;
						}
						
						window.returnValue = Audit;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (getQueryVariable("x") == "Territory") {
					        Audit = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
					    }
					    else {
					        Audit = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
					    }

					    opener.dialogWin.returnFunc(Audit);
					    bcheck = true;
					}
					else
					{
						if(getQueryVariable("x") == "Territory")
						{	Audit = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].innerHTML,' ','') ;
						}
						else
						{	Audit = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].innerHTML,' ','') ;
						}						
						opener.dialogWin.returnFunc(Audit);
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
				alert("Silahkan pilih Audit");	
			  }
			
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 19px">SHOWROOM AUDIT - Daftar Kode Audit</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td width="100%">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dtgAuditParameterSelection" runat="server" CellSpacing="1" AllowSorting="True"
								Width="100%" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro"
								AutoGenerateColumns="False" ShowFooter="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
										</HeaderTemplate>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Kode Audit">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Period" HeaderText="Periode">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Period") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
				</tr>
				<tr>
					<td width="100%" align="center">
						<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedAudit();" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
