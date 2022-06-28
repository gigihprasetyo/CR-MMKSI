<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpSearchMember.aspx.vb" Inherits="PopUpSearchMember" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>PopUpSearchMember</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			alert(x);
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealer= document.getElementById("txtDealer");
			txtDealer.value = selectedDealer;				
		}
		
		function ShowPPUserGroup()
		{
			showPopUp('../PopUp/PopUpUserGroup.aspx?x=Territory','',500,760,GroupSelection);
		}
		function GroupSelection(selectedGroup)
		{
			var txtUserGroup= document.getElementById("txtUserGroup");
			txtUserGroup.value = selectedGroup;				
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
								<TD class="titleField" width="24%">User Group</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtUserGroup" runat="server" Width="248px"></asp:textbox><asp:label id="lblSearchUserGroup" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">User ID</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtUserID" runat="server" Width="248px"></asp:textbox></TD>
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
    <TD class=titleField width="24%" colSpan=3>
<asp:datagrid id=dtgAboutMember runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
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
									<asp:TemplateColumn HeaderText="User Group">
									<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
									
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="User Login">
									<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle></asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD></TR>
  <TR>
    <TD height=40 align=center>
<asp:Panel id=pnlHtmlControl Runat="server"><INPUT id=btnChoose style="WIDTH: 60px" onclick=GetSelectedMemberMany() type=button value=Pilih name=btnChoose>&nbsp;<INPUT id=btnCancel style="WIDTH: 60px" onclick=window.close() type=button value=Tutup name=btnCancel> 
      </asp:Panel></TD></TR>
				</asp:panel>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
