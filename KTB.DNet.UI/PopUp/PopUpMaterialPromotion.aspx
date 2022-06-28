<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpMaterialPromotion.aspx.vb" Inherits="PopUpMaterialPromotion" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpMaterialPromotion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedMaterialPromotion()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgMaterialPromotionSelection");
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
					else if (navigator.appName == "Netscape") {
					    if (Dealer == '') {
					        Dealer = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        Dealer = Dealer + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">Material Promosi &nbsp;-&nbsp;Pencarian Barang</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="10" src="../images/blank.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="30%">Kode Barang</TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtGoodNo" onblur="omitSomeCharacter('txtGoodNo','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Barang</TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dtgMaterialPromotionSelection" runat="server" Width="100%" AllowPaging="True"
											AllowCustomPaging="True" AutoGenerateColumns="False" AllowSorting="True" PageSize=25 BorderWidth="0" BackColor="#CDCDCD" CellPadding=3 CellSpacing=1>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<HeaderStyle ForeColor=white></HeaderStyle>
											<ItemStyle BackColor="#F1F6FB"></ItemStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="GoodNo" HeaderText="Kode Barang">
													<HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGoodNo" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Barang">
													<HeaderStyle Width="40%" CssClass="titleTablePromo"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblName" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" BackColor="#CCCCCC" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD colspan="3" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedMaterialPromotion()"
										type="button" value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
