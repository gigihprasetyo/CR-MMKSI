<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpTrainee.aspx.vb" Inherits="PopUpTrainee" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpUserInfo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
		<script language="javascript">
		function GetSelectedUser()
		{
			var table;
			var bcheck =false;
			table = document.getElementById('dtgUserInfo');
			var User ='';
			for (i = 1; i < table.rows.length; i++)
			{
			var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
			if (radioBtn != null && radioBtn.checked)			
				{	
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						User = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText;				
			
						window.returnValue = User;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    User = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText + ';' + table.rows[i].cells[4].innerText;

					    opener.dialogWin.returnFunc(User);
					    bcheck = true;
					}
					else
					{
						User = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[3].innerHTML,' ','')+ ';' 
								+ replace(table.rows[i].cells[4].innerHTML,' ','');
						
						opener.dialogWin.returnFunc(User);
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
				alert("Silahkan pilih User");	
			  }
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">USER INFO&nbsp;- Pilih User</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Nama</TD>
								<TD width="1%">:</TD>
								<TD width="75%">
									<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtUserName" onblur="omitSomeCharacter('txtUserName','<>?*%$;')"
										runat="server" Width="248px" MaxLength="20"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Email</TD>
								<TD width="1%">:</TD>
								<TD width="75%">
									<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEmail" onblur="omitSomeCharacter('txtEmail','<>?*%$;')"
										runat="server" Width="248px" MaxLength="50"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Text="Cari" Width="64px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px">
							<asp:datagrid id="dtgUserInfo" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"
									BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserName" HeaderText="User Id">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblUserID"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FirstName" HeaderText="Nama">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblNama"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email"></asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40" align="center">&nbsp;<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedUser()" type="button"
							value="Pilih" name="btnChoose" runat="server"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
