<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSAP.aspx.vb" Inherits="PopUpSAP" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpSAP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		function GetSelectedSAP()
			{
				var table;
				var bcheck =false;
				table = document.getElementById("dtgSAPList");
				var SAP ='';
				for (i = 1; i < table.rows.length; i++)
				{
			
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
							//if(getQueryVariable("x") == "Territory"){
								SAP = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText,' ','')+ ';' ;
							/*}
							else
							{	SAP = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText,' ','')+ ';' ;
							}*/
							
							window.returnValue = SAP;
							bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    SAP = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[3].innerText, ' ', '') + ';';
						    opener.dialogWin.returnFunc(SAP);
						    bcheck = true;
						}
						else
						{
							SAP = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
							opener.dialogWin.returnFunc(SAP);
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
					alert("Silahkan Pilih SAP PERIODE ");	
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="sapTable" width="100%">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 17px">Pilih NO SAP</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgSAPList" runat="server" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True"
								BorderWidth="0px" DataKeyField="ID" Width="100%" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" PageSize="25">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SAPNumber" HeaderText="SAP No">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id ="lblSAPNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SAPNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StartDate" HeaderText="Mulai">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id ="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EndDate" HeaderText="Akhir">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id ="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center"><INPUT id="btnChoose" style="WIDTH: 60px; HEIGHT: 21px" disabled onclick="GetSelectedSAP()"
							type="button" value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Tutup" name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
