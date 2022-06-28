<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpChassisMasterSelection2.aspx.vb" Inherits="PopUpChassisMasterSelection2"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpChassisMaster</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function GetSelectedChassis()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgChassisMaster");
			var ChassisMaster ='';
			for (i = 1; i < table.rows.length; i++){
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
							SelectedData = table.rows[i].cells[1].innerText;
							window.returnValue = SelectedData;
							bcheck=true;
							break;
						}
						else if (navigator.appName == "Netscape") {
						    SelectedData = table.rows[i].cells[1].innerText;
						    opener.dialogWin.returnFunc(SelectedData);
						    bcheck = true;
						    break;
						}
						else
						{							
							if (ChassisMaster == '')
							{
								ChassisMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							window.close();
							opener.dialogWin.returnFunc(ChassisMaster);
							bcheck=true;	
							break;
							}
					}
			}
			
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih No Rangka");	
			  }			
		}
		
		function GetSelectedChassisIndent()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgChassisMaster");
			var ChassisMaster ='';
			for (i = 1; i < table.rows.length; i++){
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
							SelectedData = table.rows[i].cells[1].innerText;
							window.returnValue = SelectedData;
							bcheck=true;
							break;
						}
						else if (navigator.appName == "Netscape") {
						    SelectedData = table.rows[i].cells[1].innerText;
						    opener.dialogWin.returnFunc(SelectedData);
						    bcheck = true;
						    break;
						}
						else
						{							
							if (ChassisMaster == '')
							{
								ChassisMaster = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + replace(table.rows[i].cells[1].getElementsByTagName("span")[1].innerHTML,' ','');
							}
							window.close();
							opener.dialogWin.returnFunc(ChassisMaster);
							bcheck=true;	
							break;
							}
					}
			}
			
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih No Rangka");	
			  }			
		}

		function ClosePopUp(){
			window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">List Data Chassis</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px">Nomor&nbsp;Rangka</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtNoRangka" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoRangka','<>?*%$;')"
										runat="server" Width="230px"></asp:textbox><INPUT id="hdnIndent" type="hidden" runat="server">
                                    <INPUT id="hdnPqrNo" type="hidden" runat="server">
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td><div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dtgChassisMaster" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
								AllowCustomPaging="True" AllowPaging="True" PageSize="25">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
										<ItemTemplate>
											<input type="radio" id="x" name="y" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNo" HeaderText="Nomor Rangka">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
                        <INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedChassis()" type="button" value="Pilih" name="btnChoose" runat="server">
                        &nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup" name="btnCancel">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
