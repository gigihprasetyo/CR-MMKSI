<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEventInfo.aspx.vb" Inherits="PopUpEventInfo" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp Event Info</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
				
		function GetSelectedEventInfo()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dgEventInfo");
			var EventInfo ='';
			for (i = 1; i < table.rows.length; i++)
			{
			
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{						
						EventInfo = table.rows[i].cells[1].innerText;
						window.returnValue = EventInfo;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    EventInfo = table.rows[i].cells[1].innerText;
					    opener.dialogWin.returnFunc(EventInfo);
					    bcheck = true;
					}
					else
					{
						EventInfo = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;					

						opener.dialogWin.returnFunc(EventInfo);
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
				alert("Silahkan pilih EventInfo");	
			  }
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						EVENT - Info Event</td>
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
							<TBODY>
								<TR>
									<TD class="titleField" style="WIDTH: 149px; HEIGHT: 17px" width="149">No Pengajuan 
										Event</TD>
									<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
									<TD style="WIDTH: 215px; HEIGHT: 17px" width="215">
										<asp:textbox id="txtRequestEventNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtRequestEventNo','<>?*%$;')"
											runat="server" MaxLength="20"></asp:textbox></TD>
									<TD class="titleField" style="WIDTH: 95px; HEIGHT: 17px" width="95">Type Event</TD>
									<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
									<TD style="HEIGHT: 17px" width="29%">
										<asp:DropDownList id="ddlTypeEvent" runat="server" Width="120px"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 149px; HEIGHT: 10px" width="149">Lokasi</TD>
									<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
									<TD style="WIDTH: 215px; HEIGHT: 10px" width="215">
										<asp:textbox id="txtLokasi" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtLokasi','<>?*%$;')"
											runat="server" MaxLength="20"></asp:textbox></TD>
									<TD class="titleField" style="WIDTH: 95px; HEIGHT: 17px" width="95">
										Status</TD>
									<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
									<TD style="HEIGHT: 17px" width="29%">
										<asp:DropDownList id="ddlStatus" runat="server" Width="120px"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="titleField" style="WIDTH: 149px; HEIGHT: 10px" width="149">Tanggal 
										Pengajuan Event</TD>
									<TD style="HEIGHT: 10px" width="1%">:</TD>
									<TD style="WIDTH: 215px; HEIGHT: 10px" width="215">
										<CC1:INTICALENDAR id="icEventDateFrom" runat="server"></CC1:INTICALENDAR>&nbsp;s/d&nbsp;
										<CC1:INTICALENDAR id="icEventDateTo" runat="server"></CC1:INTICALENDAR></TD>
									<TD style="WIDTH: 95px"></TD>
									<TD></TD>
					</TD>
					<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
					<TD style="HEIGHT: 10px" width="1%"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 149px; HEIGHT: 10px" width="149"></TD>
					<TD style="HEIGHT: 10px" width="1%"></TD>
					<TD style="WIDTH: 215px; HEIGHT: 10px" width="215">
						<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
					<TD class="titleField" style="WIDTH: 95px; HEIGHT: 10px" width="95"></TD>
					<TD style="HEIGHT: 10px" width="1%"></TD>
					<TD style="HEIGHT: 10px" width="29%"></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="6">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 400px">
							<asp:datagrid id="dgEventInfo" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True"
								AllowCustomPaging="True" PageSize="25" AllowPaging="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="EventRequestNo" SortExpression="EventRequestNo" HeaderText="No Pengajuan Event">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Location" SortExpression="Location" HeaderText="Lokasi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="EventType.Description" HeaderText="Type Event">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblEventType" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Konfirmasi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox ID="chkConfirm" Runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Realisasi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox ID="chkReal" Runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</TD>
				</TR>
			</TABLE>
			</TD></TR>
			<TR>
				<TD align="center" colspan="7">
					<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedEventInfo()" type="button"
						value="Pilih" name="btnChoose" runat="server">&nbsp; <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
						name="btnCancel" runat="server"></TD>
			</TR>
			</TBODY></TABLE>
		</form>
	</body>
</HTML>
