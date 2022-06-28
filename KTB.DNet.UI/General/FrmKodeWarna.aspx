<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmKodeWarna.aspx.vb" Inherits="FrmKodeWarna" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pilihan Kode Warna</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		
		function GetSelectedWarna()
		{
			var table;
			table = document.getElementById("dtgColor");
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
				
				if (radioButton != null && radioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						var Warna = table.rows[i].cells[2].innerText;
						var Deskripsi;
						if (Warna == 'ZZZZ')
						{
							var txtbox = table.rows[i].getElementsByTagName("INPUT")[1];
							Deskripsi = txtbox.value;
						}
						else
						{
							Deskripsi = table.rows[i].cells[3].innerText;
						}
						window.returnValue = Warna+';'+Deskripsi;
					}
					else
					{
						//var Warna = table.rows[i].cells[1].getElementsByTagName("Font")[0].innerHTML;
						var Warna = table.rows[i].cells[2].innerHTML;
						var Deskripsi;
						if (Warna == 'ZZZZ')
						{
							var txtbox = table.rows[i].getElementsByTagName("INPUT")[1];
							Deskripsi = txtbox.value;
						}
						else
						{
							//Deskripsi = table.rows[i].cells[2].getElementsByTagName("Font")[0].innerHTML;
							Deskripsi = table.rows[i].cells[3].innerHTML;
						}						
						opener.dialogWin.returnFunc(Warna+';'+Deskripsi);
					}
					window.close();					
				}
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				if (i == table.rows.length - 1)
				{
					alert('Silahkan Pilih Warna');
				}
				}
			}
			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table11" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<TBODY>
					<tr>
						<td>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td height="20">&nbsp;&nbsp;<b>PESANAN KENDARAAN&nbsp;- Kode Warna</b></td>
								</tr>
								<tr>
									<td background="../images/bg_hor_sales.gif" height="1"><IMG height="1" src="../images/bg_hor_sales.gif" border="0"></td>
								</tr>
								<tr>
									<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
								</tr>
								<TR>
									<TD>
										<div id="div1" style="OVERFLOW: auto; HEIGHT: 270px">
											<asp:datagrid id="dtgColor" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="1"
												BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3">
												<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
												<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
												<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
												<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
												<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
												<Columns>
													<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
														<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="No">
														<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													</asp:TemplateColumn>
													<asp:TemplateColumn>
														<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="ColorCode" HeaderText="Kode Warna">
														<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="ColorEngName" HeaderText="B. Inggris">
														<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="ColorIndName" HeaderText="B. Indonesia">
														<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
											</asp:datagrid>
										</div>
										<asp:Label id="LblKeterangan" runat="server" ForeColor="Red"></asp:Label>
									</TD>
								</TR>
								<TR>
									<TD align="center" valign="middle" height="40"><INPUT id="btnChoose" style="WIDTH: 55px" onclick="GetSelectedWarna()" type="button" value="Pilih"
											name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 55px" onclick="window.close()" type="button" value="Batal"
											name="btnCancel"></TD>
								</TR>
							</TABLE>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
