<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpNoFaktur.aspx.vb" Inherits="PopUpNoFaktur" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpNoFaktur</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function GetSelectedPart()
		{
			var Hidden1 = document.getElementById("Hidden1");
			var table;
			table = document.getElementById("dtgSparePart");
			var find=false;
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioButton != null && radioButton.checked)
				{
				    var NoFaktur = '';
				    var TglFaktur = '';
				    var NoSo = '';
					if(navigator.appName == "Microsoft Internet Explorer")
					{
					    NoFaktur = replace(table.rows[i].cells[1].innerText,' ','');
					    TglFaktur = replace(table.rows[i].cells[2].innerText,' ','');
					    NoSo = replace(table.rows[i].cells[4].innerText, ' ', '');
					    window.returnValue = NoFaktur + ";" + TglFaktur + ";" + NoSo;
					}
					else if (navigator.appName == "Netscape") {
					    NoFaktur = replace(table.rows[i].cells[1].innerText, ' ', '');
					    TglFaktur = replace(table.rows[i].cells[2].innerText, ' ', '');
					    NoSo = replace(table.rows[i].cells[4].innerText, ' ', '');
					    opener.dialogWin.returnFunc(NoFaktur + ";" + TglFaktur + ";" + NoSo + ";");
                    }
					else {
					    NoFaktur = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
					    TglFaktur = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
					    NoSo = table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML;
					    opener.dialogWin.returnFunc(NoFaktur + ";" + TglFaktur + ";" + NoSo + ";");
					}
					
					find=true;
					break;
				}
			}
			if (find)
				window.close();
			else
				alert("Silahkan pilih no faktur");
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div>

			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SPAREPART - Daftar No Faktur</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>				
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td>
					
				<br>
				<cc1:compositefilter id="cfSparePart" runat="server" DataGridSouce="dtgSparePart"></cc1:compositefilter>
				<div id="div1" style="OVERFLOW: auto; HEIGHT: 440px">
				<asp:datagrid id="dtgSparePart" runat="server" AllowPaging="True" AllowCustomPaging="True" Width="100%"
					BorderWidth="0px" CellSpacing="1" CellPadding="3" BackColor="Gainsboro" BorderColor="Gainsboro"
					AutoGenerateColumns="False" AllowSorting="True" PageSize="20">
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
					<HeaderStyle ForeColor=white></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BillingNumber" HeaderText="No. Faktur">
							<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillingNumber") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="BillingDate" HeaderText="Tanggal Faktur">
							<HeaderStyle ForeColor="White" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPODate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BillingDate"),"dd/MM/yyyy") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SparePartPO.PONumber" HeaderText="No. PO">
							<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPO.PONumber")%>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SONumber" HeaderText="No. SO">
							<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblSONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber")%>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
				<br>
			</td>
			</tr><tr>
			<td align=center>
				<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedPart()" type="button" value="Pilih"
					name="btnChoose">&nbsp;<INPUT id="btnCancel" onclick="window.close()" type="button" value="Tutup" style="WIDTH: 60px"
					name="btnCancel">
			
			</td></tr>
		</table>					
			</div>
			<INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server">

		</form>
	</body>
</HTML>
