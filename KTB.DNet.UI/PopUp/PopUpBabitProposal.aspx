<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpBabitProposal.aspx.vb" Inherits="PopUpBabitProposal" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>

	<HEAD>
		<title>FrmBabitProposal</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>

		<base target="_self">
		<script language="javascript">
		
		function GetSelectedBabitProposal()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgBabitProposal");
			var BabitProposal ='';
			var vNoPengajuan ='';
			var vNoPersetujuan='';
			// mekanisme, disini bs melempar kolom value lebih dr satu
			// dengan multiselected, jadi dilakukan loop 1 persatu
			// loop untuk NoPengajuan 
			for (i = 1; i < table.rows.length; i++)
				{
					var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (CheckBox != null && CheckBox.checked)
					{
						var NoPengajuan = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
						//var NoPersetujuan = table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML;
						if (vNoPengajuan=='')
							{
								vNoPengajuan= NoPengajuan;
							}
						else
							{
								if (NoPengajuan != '')
								{
									vNoPengajuan= vNoPengajuan + ';' + NoPengajuan;
								}
							}
					}
				}
			// loop untuk NoPersetujuan
			for (i = 1; i < table.rows.length; i++)
				{
					var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (CheckBox != null && CheckBox.checked)
					{
						//var NoPengajuan = table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML;
					    var NoPersetujuan='';

					    if (navigator.appName == "Microsoft Internet Explorer") {
					        var NoPersetujuan = table.rows[i].cells[4].innerText;
					    } else if (navigator.appName == "Netscape")
					    {
					        var NoPersetujuan = table.rows[i].cells[4].innerText;
					    }
					    else
					    {
					        var NoPersetujuan = table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML;
					    }

						if (vNoPersetujuan=='')
							{
								vNoPersetujuan= NoPersetujuan;
							}
						else
							{
								if (NoPersetujuan != '')
								{
									vNoPersetujuan= vNoPersetujuan + ';' + NoPersetujuan;
								}
							}
					}
				}
			// mengabungkan kolom yg berbeda, dgn char "@"
			BabitProposal = vNoPengajuan + '@' + vNoPersetujuan;			//>> Comment by Hendra : No Persetujuan utk apa ? jika No persetujuan di aktifkan maka pada saat pencarian akan bermasalah
			//BabitProposal = vNoPengajuan;
			
			if(navigator.appName == "Microsoft Internet Explorer")
				{
					window.returnValue = BabitProposal;
					bcheck=true;
				}
				else
				{
			  		window.opener.dialogWin.returnFunc(BabitProposal);
					bcheck=true;
				}	
			
			/*if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan Pilih Babit Proposal terlebih dahulu");	
			  }*/
			  if (BabitProposal != "@")
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan Pilih Babit Proposal terlebih dahulu");	
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
								<td class="titlePage" colSpan="7">BABIT - Babit Proposal</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">No Pengajuan</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtNoPengajuan" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoPengajuan','<>?*%$;')" runat="server"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">No Persetujuan</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtNoPersetujuan" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoPersetujuan','<>?*%$;')" runat="server"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgBabitProposal" runat="server" Width="100%" AutoGenerateColumns="False" AllowCustomPaging="True"
											AllowPaging="True" PageSize="25" AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor=white></HeaderStyle>
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
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="NoPengajuan" HeaderText="No Pengajuan">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblNoPengajuan runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPengajuan") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="NoPersetujuan" HeaderText="No Persetujuan">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblNoPersetujuan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoPersetujuan") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align=center colspan=7><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedBabitProposal()"
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
