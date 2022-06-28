<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformReceiver.aspx.vb" Inherits="FrmSalesmanUniformReceiver" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
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
			showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=true','',600,600,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			var txtDealerNameSelection = document.getElementById("txtDealerName");
			var arrValue = selectedDealer.split(';');
			
			txtDealerCodeSelection.value = arrValue[0];
			txtDealerNameSelection.value = arrValue[1];
		}
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" >
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SERAGAM TENAGA PENJUAL - Penerima 
						Seragam</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 316px; HEIGHT: 10px" width="316">
									<asp:textbox onblur="omitSomeCharacter('txtSalesmanName','<>?*%$;')" id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										Width="376px" Runat="server" Height="32px" TextMode="MultiLine"></asp:textbox>
									<asp:label style="VISIBILITY:hidden" id="lblKodeDealer" runat="server" Width="16px"></asp:label>
								</TD>
								<TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Pesanan</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 316px; HEIGHT: 10px" width="316"><asp:label id="lblKodePesanan" runat="server" Width="128px"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Salesman Name</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 316px; HEIGHT: 10px" width="316"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSalesmanName" onblur="omitSomeCharacter('txtSalesmanName','<>?*%$;')"
										Runat="server"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 24%" width="10%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="20%"></TD>
								<!--<TD class="titleField" style="HEIGHT: 24%" width="10%">Lama Kerja</TD>
								<TD style="HEIGHT: 10px" width="1%">:</TD>
								<TD style="HEIGHT: 10px" width="20%">
									<asp:DropDownList ID="ddlLamaKerja" Runat="server" Width="132px"></asp:DropDownList>
								</TD>--></TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Posisi</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 316px; HEIGHT: 10px" width="316"><asp:dropdownlist id="ddlPosisi" Width="128px" Runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="WIDTH: 316px; HEIGHT: 20px" noWrap width="316"><asp:button id="btnSearch" Width="56px" Runat="server" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="10%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="WIDTH: 316px; HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD width="600" vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgSalesmanUniformReceiver" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
											AllowPaging="True" DataKeyField="ID">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAll('chkSelection',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkSelection" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Name">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn Visible="False" HeaderText="GenderCode">
													<ItemTemplate>
														<asp:Label id=lblGenderCode Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Gender")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Gender">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGender" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="JobPosition.Description" HeaderText="Posisi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblPosisi Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition.Description")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="HireDate" SortExpression="HireDate" HeaderText="Tgl Masuk" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Lama Kerja">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblLamaKerja Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LamaBekerjaBulan")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"><asp:button id="btnPilih" Width="80" Runat="server" Text="Pilih"></asp:button><INPUT id="btnCancel" style="WIDTH: 76px; HEIGHT: 21px" onclick="window.close()" type="button"
							value="Kembali" name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
