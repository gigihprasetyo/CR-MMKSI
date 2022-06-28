<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpDealerSelectionBenefit.aspx.vb" Inherits="PopUpDealerSelectionBenefit" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Dealar</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedDealer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgDealerSelection");
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
							Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Dealer;
					bcheck=true;
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
						bcheck=true;
					}
				}
			}
			if (bcheck)
			  {
					window.close();
					if(navigator.appName != "Microsoft Internet Explorer")
					{	opener.dialogWin.returnFunc(Dealer);	}
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">DEALER -&nbsp;Pencarian Dealer</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR valign="top">
								<TD class="titleField" width="20%" style="HEIGHT: 20px">Nama Dealer</TD>
								<TD width="1%" style="HEIGHT: 20px">:</TD>
								<TD width="25%" style="HEIGHT: 20px"><asp:textbox id="txtDealerName" runat="server" Width="152px" ></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"></TD>
								<TD class="titleField" width="20%" style="HEIGHT: 20px">Term Cari 1</TD>
								<TD width="1%" style="HEIGHT: 20px">:</TD>
								<TD width="33%" style="HEIGHT: 20px"><asp:textbox id="txtSearch1" runat="server" ></asp:textbox></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 13px">Grup</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:listbox id="lboxGroup" runat="server" Width="152px" SelectionMode="Multiple" Rows="3"></asp:listbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px">Term Cari 2</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtSearch2" runat="server" ></asp:textbox><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px">
                                        <asp:datagrid id="dtgDealerSelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2" 
											AllowSorting="True" AllowPaging="True" PageSize="25">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" ></HeaderStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="7%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="21%" ></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
													<HeaderStyle Width="10%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="DealerGroup.GroupName" HeaderText="Grup">
													<HeaderStyle Width="10%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SearchTerm1" SortExpression="SearchTerm1" HeaderText="Term Cari 1">
													<HeaderStyle Width="12%" ></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="SearchTerm2" SortExpression="SearchTerm2" HeaderText="Term Cari 2">
													<HeaderStyle Width="15%" ></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealer()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
