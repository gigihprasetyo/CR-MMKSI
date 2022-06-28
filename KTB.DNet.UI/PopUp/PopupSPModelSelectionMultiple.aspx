<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupSPModelSelectionMultiple.aspx.vb" Inherits="PopupSPModelSelectionMultiple"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form Model Selection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function CheckAll(aspCheckBoxID, checkVal) {
		        re = new RegExp(':' + aspCheckBoxID + '$')
		        for (i = 0; i < document.forms[0].elements.length; i++) {
		            elm = document.forms[0].elements[i]
		            if (elm.type == 'checkbox') {
		                if (re.test(elm.name)) {
		                    elm.checked = checkVal
		                }
		            }
		        }
		}

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
		
		function GetSelectedModel()
		{
		    var table;
		    var bcheck = false;
		    table = document.getElementById("dtgDealerSelection");
		    var Dealer = '';
		    for (i = 1; i < table.rows.length; i++) {
		        var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		        if (CheckBox != null && CheckBox.checked) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                if (Dealer == '') {
		                    Dealer = replace(table.rows[i].cells[1].innerText, ' ', '');
		                }
		                else {
		                    Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                }
		                window.returnValue = Dealer;
		                bcheck = true;
		            }
		            else {
		                if (Dealer == '') {
		                    Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }
		                else {
		                    Dealer = Dealer + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }
		                bcheck = true;
		            }
		        }
		    }
		    if (bcheck) {
		        window.close();
		        if (navigator.appName != "Microsoft Internet Explorer")
		        { opener.dialogWin.returnFunc(Dealer); }
		    }
		    else {
		        alert("Silahkan Pilih Model terlebih dahulu");
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
								<td class="titlePage" style="WIDTH: 872px" colSpan="7">SparePart&nbsp;-&nbsp;Pencarian&nbsp;Model&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td style="WIDTH: 872px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 14px">Model</TD>
								<TD style="HEIGHT: 14px">:</TD>
								<TD style="HEIGHT: 14px">
									<asp:textbox id="txtModel" runat="server" Width="126px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 14px">
									<asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 14px"></TD>
								<TD style="HEIGHT: 14px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 14px"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 872px" colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgDealerSelection" runat="server" Height="96px" AutoGenerateColumns="False"
											Width="408px">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableGeneral" ></HeaderStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Spare Part Model">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblModelCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModelCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan="4" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedModel()" type="button"
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
