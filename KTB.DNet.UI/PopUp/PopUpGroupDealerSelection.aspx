<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpGroupDealerSelection.aspx.vb" Inherits="PopUpGroupDealerSelection" smartNavigation="False"%>
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
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerGroupSelection");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Dealer = Dealer + ',' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Dealer;
					bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (Dealer == '') {
					        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        Dealer = Dealer + ',' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(Dealer);
					    bcheck = true;
					}
					else
					{
						if (Dealer == '')
						{
							Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Dealer = Dealer + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(Dealer);
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
				alert("Silahkan Pilih Dealer terlebih dahulu");	
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">DEALER -&nbsp;Pencarian Group Dealer</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">Kode Grup</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtGroupCode" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server" onblur="omitSomeCharacter('txtGroupCode','<>?*%$;')" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px">
									<asp:textbox id="txtGroup" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server" onblur="omitSomeCharacter('txtGroup','<>?*%$;')" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgDealerGroupSelection" runat="server" Width="100%" AutoGenerateColumns="False">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
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
												<asp:TemplateColumn HeaderText="Kode Grup" SortExpression="DealerGroupCode">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroupCode" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Grup" SortExpression="GroupName">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan=7 align=center><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
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
