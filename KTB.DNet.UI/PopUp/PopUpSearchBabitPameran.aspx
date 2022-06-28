<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpSearchBabitPameran.aspx.vb" Inherits=".PopUpSearchBabitPameran" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pencarian Babit Pameran</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
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
		}
		
		function GetSelectedEventProposal()
		{
		    var table;
		    var bcheck = false;
		    table = document.getElementById("dtgEventProposalSelection");
		    var EventInfo = '';
		    for (i = 1; i < table.rows.length; i++) {
		        var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		        if (radioBtn != null && radioBtn.checked) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                EventInfo = table.rows[i].cells[1].innerText;

		                window.returnValue = EventInfo;
		                bcheck = true;
		            }
		            else if (navigator.appName == "Netscape") {
		                EventInfo = table.rows[i].cells[1].innerText;

		                opener.dialogWin.returnFunc(EventInfo);
		                bcheck = true;
		            }
		            else {
		                EventInfo = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;

		                opener.dialogWin.returnFunc(EventInfo);
		                bcheck = true;
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
	    		alert("Silahkan pilih Babit Pameran");	
			}
		}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">Pencarian Babit Pameran</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" alt="" src="../images/dot.gif" border="0"/></td>
							</tr>
							<TR valign="top">
								<TD class="titleField" width="20%">No. Reg Babit</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtEventRegNumber" runat="server" Width="172px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="10%"></TD>
								<TD class="titleField" width="20%">Nomor Surat</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 225px" width="225"><asp:TextBox ID="txtEventName" runat="server" Width="192px" /></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" width="20%">Periode Pameran</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="WIDTH: 215px; HEIGHT: 10px" width="215">                                    
                                    <table id="Table20" cellSpacing="0" cellPadding="0" border="0">
										<tr>
                                            <td><asp:checkbox id="chkTanggal" runat="server"></asp:checkbox></td>
											<td style="WIDTH: 101px"><cc1:inticalendar id="icEventDateFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEventDateTo" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px">&nbsp;</TD>
								<TD valign="bottom" style="WIDTH: 225px; HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD  colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
                                        <asp:datagrid id="dtgEventProposalSelection" runat="server" Width="100%" AutoGenerateColumns="False" 
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="#F7F7F7" BackColor="#4A3C8C" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="BabitRegNumber" HeaderText="Reg Number">
													<HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>     
														<asp:Label id=lblRegNumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BabitRegNumber")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="BabitDealerNumber" SortExpression="BabitDealerNumber" HeaderText="No. Surat">
													<HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Periode Awal" SortExpression="PeriodStart">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label id="lblPeriodStart" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodStart"), "dd/MM/yyyy")%>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Periode Akhir" SortExpression="PeriodEnd">
													<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label id="lblPeriodEnd" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.PeriodEnd"), "dd/MM/yyyy")%>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7">
								<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedEventProposal()" type="button" value="Pilih" name="btnChoose" runat="server">
                                    &nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
