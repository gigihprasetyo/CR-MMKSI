<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpUserGroup.aspx.vb" Inherits="PopUpUserGroup" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpUserGroup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		function GetSelectedUserGroup()
		{
			var table;
			table = document.getElementById("dtgUserGroup");
			var UserGroup ='';
			var bcheck =false;
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (UserGroup == '')
						{
							UserGroup = table.rows[i].cells[2].innerText;
						}
						else
						{
							UserGroup = UserGroup + ';' + table.rows[i].cells[2].innerText;
						}
					window.returnValue = UserGroup;
					bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (UserGroup == '') {
					        UserGroup = table.rows[i].cells[2].innerText;
					    }
					    else {
					        UserGroup = UserGroup + ';' + table.rows[i].cells[2].innerText;
					    }
					    opener.dialogWin.returnFunc(UserGroup);
					    bcheck = true;
					}
					else
					{
						if (UserGroup == '')
						{
							UserGroup = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
						}
						else
						{
							UserGroup = UserGroup + ';' + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
						}
					opener.dialogWin.returnFunc(UserGroup);
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
				alert("Silahkan Pilih User Group Terlebih Dahulu");	
			  }
		}
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Pop Up&nbsp;- Pilih User Group</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">User Group</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtUserGroup" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"
										runat="server" Width="248px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dtgUserGroup" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="true" SortExpression="Code" HeaderText="Kode">
										<HeaderStyle Width="0%"></HeaderStyle>
										<ItemStyle Width="0%" HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>' ID="Label1">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="false" SortExpression="ID" HeaderText="">
										<HeaderStyle Width="0%"></HeaderStyle>
										<ItemStyle Width="0%" HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<div style="display:none">
												<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' ID="Label2">
												</asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="User Group">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="40"><asp:panel id="PnlNonBuletin" Runat="server"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedUserGroup()" type="button"
								value="Pilih" name="btnChoose" runat="server"><INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
								name="btnCancel">
						</asp:panel></TD>
				</TR>
				<TR>
					<TD align="center"><asp:panel id="PnlBuletin" Runat="server">
							<asp:Button id="btnSaveBuletin" Text="Simpan" Runat="server"></asp:Button>
						</asp:panel>

					</TD>
				</TR>

                <TR>
					<TD align="center"><asp:panel id="PnlPresentation" Runat="server">
							<asp:Button id="btnSavePresentation" Text="Simpan" Runat="server"></asp:Button>
						</asp:panel>

					</TD>
				</TR>

				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
