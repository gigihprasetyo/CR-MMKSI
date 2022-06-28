<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmModelSelection.aspx.vb" Inherits="FrmModelSelection" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pilihan Kode Tipe</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function GetSelectedModel()
		{
			var table;
			table = document.getElementById("dtgVechileType");
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
				if (radioButton != null && radioButton.checked)
				{
					//var Tipe = table.rows[i].cells[1].innerText;
					//var Deskripsi = table.rows[i].cells[2].innerText;

					//window.returnValue = Tipe;
					
					if(navigator.appName == "Microsoft Internet Explorer")
					{
					var Tipe = table.rows[i].cells[2].innerText;
					//var Deskripsi = table.rows[i].cells[2].innerText;
					window.returnValue = Tipe; 
					}
					else 
					{
					var Tipe = table.rows[i].cells[2].innerHTML;
					//var Tipe = table.rows[i].cells[1].getElementsByTagName("Font")[0].innerHTML;
					//var Deskripsi = table.rows[i].cells[2].getElementsByTagName("Font")[0].innerHTML; 
					opener.dialogWin.returnFunc(Tipe);
					}
					window.close();
				}
				if (navigator.appName == "Microsoft Internet Explorer")
				{
				if (i == table.rows.length - 1) 
				{
					alert('Silahkan Pilih Model');
				}
				}
			}
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table11" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td height="20">&nbsp;&nbsp;<b>PESANAN KENDARAAN&nbsp;- Kode Tipe</b></td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" height="1"><IMG height="1" src="../images/bg_hor_sales.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
										<asp:DataGrid id="dtgVechileType" runat="server" Width="100%" BorderColor="#CDCDCD" BorderWidth="0px"
											BackColor="#CDCDCD" CellPadding="2" GridLines="None" CellSpacing="1" ForeColor="Black" AutoGenerateColumns="False">
											<SelectedItemStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
											<FooterStyle BackColor="Tan"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="VechileTypeCode" HeaderText="Tipe">
													<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" HeaderText="Deskripsi">
													<HeaderStyle Width="60%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="PaleGoldenrod"></PagerStyle>
										</asp:DataGrid>
									</div>
								</TD>
							</TR>
							<TR>
								<TD height="40" valign="middle" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedModel()" type="button" value="Pilih"
										name="btnChoose">&nbsp;<INPUT style="WIDTH: 60px" id="btnCancel" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
