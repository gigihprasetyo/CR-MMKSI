<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpGroupMember.aspx.vb" Inherits="PopUpGroupMember" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpGroupMember</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function GetSelectedMemberMany()
		{	
			var table;
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
							Course =replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
						}
						else
						{
							Course = Course + ";" + replace(table.rows[i].cells[2].innerText,' ','') + '-' + replace(table.rows[i].cells[3].innerText,' ','') ;
						}
					window.returnValue = Course;
					}
					else if (navigator.appName == "Netscape") {
					    if (Course == '') {
					        Course = replace(table.rows[i].cells[2].innerText, ' ', '') + '-' + replace(table.rows[i].cells[3].innerText, ' ', '');
					    }
					    else {
					        Course = Course + ";" + replace(table.rows[i].cells[2].innerText, ' ', '') + '-' + replace(table.rows[i].cells[3].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(Course);
                    }
					else
					{
						if (Course == '')
						{
							Course = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Course = Course + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(Course);
					}
				}
			}
			window.close();
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
		function ClosePopUp(){
			window.close();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">BULLETIN&nbsp;- Pilih Bulletin Member</td>
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
								<TD width="75%"><asp:textbox id="txtDealer" runat="server" Width="248px"></asp:textbox><asp:label id="lblSearchDealer" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Posisi</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtPosisi" runat="server" Width="248px"></asp:textbox>
									<!--<asp:label id="lblSearchUserGroup" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label> --></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali"></asp:button></td>
							</TR>
						</TABLE>
						Silahkan pilih Buletin Member
					</TD>
				</TR>
				<asp:panel id="panelNew" Runat="server">
					<TR>
						<TD class="titleField" width="24%" colSpan="3">
							<asp:datagrid id="dtgAboutMember" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="false"
								AllowCustomPaging="false" BackColor="#CDCDCD" CellPadding="3" GridLines="None">
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
									<asp:TemplateColumn HeaderText="Kode Dealer">
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
									<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" HeaderText="Posisi">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD height="40" align=center>
							<asp:Panel id="pnlHtmlControl" Runat="server"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedMemberMany()" type="button"
									value="Pilih" name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
									name="btnCancel"> 
      </asp:Panel></TD>
					</TR>
				</asp:panel>
				<TR>
					<TD></TD>
				</TR>
				<asp:panel id="panelAdd" Runat="server">
					<TR>
						<TD class="titleField" width="24%" colSpan="3">
							<asp:datagrid id="dtgAddMember" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="False"
								AllowCustomPaging="False" BackColor="#CDCDCD" CellPadding="3" GridLines="None" DataKeyField="ID">
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
											<input id="chkBulletinMembers" type="checkbox" onclick="CheckAll('chkBuletinMemberChecked',
														document.forms[0].chkBulletinMembers.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkBuletinMemberChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNoAdd"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' ID="lblKodeAdd">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="User Id">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" id="lblUserName" Text='<%# DataBinder.Eval(Container, "DataItem.UserName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Posisi">
									<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" id="lblJobPosition" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD height="40" align=center>
							<asp:Panel id="Panel1" Runat="server">
<asp:Button id="btnSimpan" runat="server" Width="60" Text="Simpan"></asp:Button>&nbsp; 
<asp:Button id="btnBatal" runat="server" Width="60" Text="Batal"></asp:Button></asp:Panel></TD>
					</TR>
				</asp:panel></TABLE>
		</form>
		<DIV></DIV>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
