<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpForumMemberOne.aspx.vb" Inherits="PopUpForumMemberOne" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMember</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<base target="_self">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function GetSelectedUser()
		{
			var table;
			table = document.getElementById("dtgAboutMember");
			var find=false;
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioButton != null && radioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
						{
							Cust = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText ;
							window.returnValue = Cust;
					}
					else if (navigator.appName == "Netscape") {
					    Cust = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText;
					    opener.dialogWin.returnFunc(Cust);
                    }
					else 
						{ 
							Cust = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[3].innerHTML,' ','') ;
							opener.dialogWin.returnFunc(Cust);
						}
					find=true;
					break;
				}
			}
			if (find)
				window.close();
			else
				alert("Silahkan pilih Customer");
		}
		
		/*function GetSelectedUser()
		{
			var table;
			var bcheck =false;
			table = document.getElementById('dtgAboutMember');
			var Cust ='';
			for (i = 1; i < table.rows.length; i++)
			{
			
			var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						Cust = table.rows[i].cells[2].innerText + ';' + table.rows[i].cells[3].innerText ;				
						
						window.returnValue = Cust;
						bcheck=true;
					}
					else
					{
						Cust = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + replace(table.rows[i].cells[3].innerHTML,' ','') ;
						
						opener.dialogWin.returnFunc(Cust);
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
				alert("Silahkan pilih Customer");	
			  }
		}
		*/
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealer= document.getElementById("txtDealer");
			txtDealer.value = selectedDealer;				
		}
		
		
		function ShowPPUserGroupSelection()
		{
			showPopUp('../PopUp/PopUpUserGroup.aspx?x=Territory','',500,760,UserGroupSelection);
		}
		function UserGroupSelection(selectedUserGroup)
		{
			var txtUserGroup= document.getElementById("txtUserGroup");
			txtUserGroup.value = selectedUserGroup;				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Forum - UserName</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtDealer" runat="server" Width="248px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtDealer','<>?*%$')"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">User group</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtUserGroup" runat="server" Width="248px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										onblur="omitSomeCharacter('txtUserGroup','<>?*%$')"></asp:textbox><asp:label id="lblSearchUserGroup" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<asp:panel id="pnlBuletin" Runat="server">
									<TD class="titleField" width="24%">User Name</TD>
									<TD width="1%">:</TD>
									<TD width="75%">
										<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtUserName" onblur="omitSomeCharacter('txtUserName','<>?*%$;')"
											runat="server" Width="248px"></asp:textbox></TD>
								</asp:panel>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="24%" colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgAboutMember" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
							AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="None"
							PageSize="25" AllowPaging="True" AllowCustomPaging="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<INPUT type="radio" name="radio">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label Runat="server" ID="lblNo"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ID="Label2">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="User Id">
									<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="JobPosition.Description" HeaderText="Posisi">
									<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description") %>' ID="lblJob">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="DealerID">
									<ItemTemplate>
										<asp:Label runat="server" ID="DealerID" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div></TD>
				</TR>
				<TR>
					<TD height="40" align="center">
						<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedUser()" type="button" value="Pilih"
							name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
		<DIV></DIV>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
