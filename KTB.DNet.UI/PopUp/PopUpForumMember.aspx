<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpForumMember.aspx.vb" Inherits="PopUpForumMember" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMember</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
		function GetSelectedMemberMany()
		{	
			var table;
			var mSelected = false;
			table = document.getElementById("dtgAboutMember");
			if (table==null)
			{
			return
			}
			if (table==undefined)
			{
				return;
			}
			var Course ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Course == '')
						{
							//Course =replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
							Course =table.rows[i].cells[2].innerText + '-' + table.rows[i].cells[3].innerText;
						}
						else
						{
							//Course = Course + ';' + replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
							Course = Course + ';' + table.rows[i].cells[2].innerText + '-' + table.rows[i].cells[3].innerText ;
						}
					mSelected = true;
					}
					else if (navigator.appName == "Netscape") {
					    if (Course == '') {
					        //Course =replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
					        Course = table.rows[i].cells[2].innerText + '-' + table.rows[i].cells[3].innerText;
					    }
					    else {
					        //Course = Course + ';' + replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
					        Course = Course + ';' + table.rows[i].cells[2].innerText + '-' + table.rows[i].cells[3].innerText;
					    }
					    mSelected = true;
					}
					else
					{
						if (Course == '')
						{
							Course = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','')+'-'+replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Course = Course + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML,' ','')+'-'+replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					mSelected = true;
					}
				}
			}

			if(navigator.appName == "Microsoft Internet Explorer")
			{
					window.returnValue = Course;
			}
			else
			{
					opener.dialogWin.returnFunc(Course);
			}

			if(mSelected == false) {
				alert('Pilih anggota yang akan dijadikan member');
			}else {
				window.close();
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
					<td class="titlePage"><asp:label id="lblTitle" CssClass="titlePage" Runat="server"></asp:label></td>
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
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealer" onblur="omitSomeCharacter('txtDealer','<>?*%$')"
										runat="server" Width="248px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">User group</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$ ')" id="txtUserGroup" onblur="omitSomeCharacter('txtUserGroup','<>?*%$ ')"
										runat="server" Width="248px"></asp:textbox><asp:label id="lblSearchUserGroup" runat="server">
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
					<TD class="titleField" width="24%" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgAboutMember" runat="server" Width="100%" AllowPaging="True" PageSize="25"
								AllowCustomPaging="True" GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
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
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ID="Label2">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="User Id">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserName") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserName") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
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
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="40"><asp:panel id="pnlForum" Runat="server"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedMemberMany()" type="button"
								value="Pilih" name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
								name="btnCancel"> 
      </asp:panel><asp:panel id="pnlBuletinButton" Runat="server">
							<asp:Button id="btnSave" Runat="server" Text="Simpan"></asp:Button>
						</asp:panel></TD>
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
