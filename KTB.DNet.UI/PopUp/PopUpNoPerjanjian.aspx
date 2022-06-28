<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpNoPerjanjian.aspx.vb" Inherits="PopUpNoPerjanjian"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmNoPerjanjian</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelected()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgNoPerjanjian");
			var selected ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (selected == '')
						{
							selected = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText;
						}
					window.returnValue = selected;
					bcheck=true;
					}
					else
					{
						if (selected == '')
						{
							selected = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[2].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(selected);
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
				alert("Silahkan pilih No Perjanjian terlebih dahulu");	
			  }
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						Babit&nbsp;- No Perjanjian</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 97px">No Perjanjian</TD>
								<TD style="WIDTH: 4px">:</TD>
								<TD colSpan="3"><asp:TextBox id="txtNoPerjanjian" runat="server"></asp:TextBox><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD colSpan="5">
									<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgNoPerjanjian" runat="server" Width="100%" BorderColor="#E0E0E0" CellPadding="3"
											BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														&nbsp;
													</HeaderTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="NoPerjanjian" SortExpression="NoPerjanjian" HeaderText="No Perjanjian">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Periode">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPeriode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV>
								</TD>
							</TR>
						</TABLE>
						<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelected()" type="button"
							value="Pilih" name="btnChoose" runat="server"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
