<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpBabitDealerAllocationSelectionOne.aspx.vb" Inherits="PopUpBabitDealerAllocationSelectionOne" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDealerSelectionAlokasi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		
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
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerSelection");
			var Dealer ='';
			for (i = 1; i < table.rows.length; i++)
			{
						
			var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			if (radioBtn != null && radioBtn.checked)			
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						if(getQueryVariable("x") == "Territory")
						{	Dealer = replace(table.rows[i].cells[1].innerText,' ','') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText,' ','')+ ';' + replace(table.rows[i].cells[6].innerText,' ','');
						}
						else
						{
						    //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
						    Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
						}
						window.returnValue = Dealer;
						bcheck=true;
					}
					else if (navigator.appName == "Netscape") {
					    if (getQueryVariable("x") == "Territory") {
					        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
					    }
					    else {
					        //Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText;
					        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + table.rows[i].cells[2].innerText + ';' + replace(table.rows[i].cells[5].innerText, ' ', '') + ';' + replace(table.rows[i].cells[6].innerText, ' ', '');
					    }
					    window.opener.dialogWin.returnFunc(Dealer);
					    bcheck = true;
					}
					else
					{
						if(getQueryVariable("x") == "Territory")
						{	
							Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' +  table.rows[i].cells[6].innerHTML; 
						}
						else
						{	
						    //Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].innerHTML, ' ', '');
						    Dealer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + table.rows[i].cells[2].innerHTML + ';' + table.rows[i].cells[5].innerHTML + ';' + table.rows[i].cells[6].innerHTML;
						}
						window.opener.dialogWin.returnFunc(Dealer);
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
				alert("Silahkan pilih dealer");	
			  }
		}

		function replace(string,text,by) 
			{
				var strLength = string.length, txtLength = text.length;
				if ((strLength == 0) || (txtLength == 0)) return string;

				var i = string.indexOf(text);
				if ((!i) && (text != string.substring(0,txtLength))) return string;
				if (i == -1) return string;

				var newstr = string.substring(0,i) + by;

				if (i+txtLength < strLength)
				newstr += replace(string.substring(i+txtLength,strLength),text,by);

				return newstr;
			}	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">DEALER -&nbsp;Pencarian Dealer &nbsp;</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR valign="top">
								<TD class="titleField" width="20%">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtDealerName" 
										runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%">Term Cari 1</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 225px" width="225"><asp:textbox id="txtSearch1" 
										runat="server"></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:listbox id="lboxGroup" runat="server" Width="152px" Rows="3" SelectionMode="Multiple"></asp:listbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px">Term Cari 2</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="WIDTH: 225px; HEIGHT: 13px"><asp:textbox id="txtSearch2" 
										runat="server"></asp:textbox><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD  colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px"><asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False" 
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Kota" SortExpression="City.CityName">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm2" HeaderText="Term Cari 2">
													<HeaderStyle Width="12%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7">
								<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
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
