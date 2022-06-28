<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCustomerList.aspx.vb" Inherits="PopUpCustomerList"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpCustomerList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function GetSelectedCustomer()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgCustomerSelection");
			var Customer ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (radioBtn != null && radioBtn.checked)			
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{	
										
							Customer = replace(table.rows[i].cells[1].innerText,' ','');
							window.returnValue = Customer;
							bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    Customer = replace(table.rows[i].cells[1].innerText, ' ', '');
						    opener.dialogWin.returnFunc(Customer);
						    bcheck = true;
						}

						else
						{
							Customer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');			
							opener.dialogWin.returnFunc(Customer);
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
				alert("Silahkan Pilih Customer terlebih dahulu");	
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
								<td class="titlePage" colSpan="7">Daftar Pelanggan</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">Kode</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtKode" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
										onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="33%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px">Nama</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:textbox id="txtNama" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
										onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px"></TD>
								<TD class="titleField" style="HEIGHT: 20px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
								<TD style="HEIGHT: 20px"></TD>
								<TD style="HEIGHT: 20px"></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgCustomerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
											AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="White"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														&nbsp;
													</HeaderTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Code" HeaderText="Ref Kode Pelanggan">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTempCode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Ref No Pengajuan">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTempNumber" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="CustomerCode" HeaderText="Kode Pelanggan">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblTempCustomerCode" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Name1" SortExpression="Name1" HeaderText="Nama">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Kelurahan" SortExpression="Kelurahan" HeaderText="Kelurahan">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Kecamatan" SortExpression="Kecamatan" HeaderText="Kecamatan">
													<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Kota">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCity" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan="7" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedCustomer()" type="button"
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
