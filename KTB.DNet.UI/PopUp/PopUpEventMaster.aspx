<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventMaster.aspx.vb" Inherits="PopUpEventDocument" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerSelection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedEvent()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgEvent");
			var Event ='';
			for (i = 1; i < table.rows.length; i++)
			{
			
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						Event = table.rows[i].cells[1].innerText;
						window.returnValue = Event;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    Event = table.rows[i].cells[1].innerText;
					    opener.dialogWin.returnFunc(Event);
					    bcheck = true;
					}
					else
					{
						Event = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
						opener.dialogWin.returnFunc(Event);
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
				alert("Silahkan pilih event");	
			  }
			
			
		}

		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="WIDTH: 872px" colSpan="7">Event Master
								</td>
							</tr>
							<TR>
								<TD style="WIDTH: 872px" colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgEvent" runat="server" Width="728px" Height="96px" AutoGenerateColumns="False"
											AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="white"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														&nbsp;
													</HeaderTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="EventNo" HeaderText="EventNo">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblEventNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventNo") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Periode">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPeriod" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan=3 align=center><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedEvent()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
