<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformAssign.aspx.vb" Inherits="FrmSalesmanUniformAssign" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
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
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$')
			}
			
			function ShowPPUnifCodeSelection()
			{
				showPopUp('../PopUp/PopUpUnifDistribution.aspx','',450,400,DistributionCodeSelection);
			}
			function DistributionCodeSelection(selectedCode)
			{
				var TxtDistributionCode = document.getElementById("TxtDistributionCode");
				TxtDistributionCode.value = selectedCode;
			}
			
			
		</script>
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
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
			/*if(navigator.appName == "Microsoft Internet Explorer")
			{
				txtDealerCodeSelection.focus();
				txtDealerCodeSelection.blur();
			}
			else
			{
				txtDealerCodeSelection.onchange();
			}*/
		}
		
		function ShowPPSalesmanSelection()
		{
			var txtDealerCode=document.getElementById("txtDealerCode");
			if (txtDealerCode.value == "") 
			{
			  alert("Silakan pilih min 1 dealer!");
			  return;
			}
			var TxtDistributionCode=document.getElementById("TxtDistributionCode");
			if (TxtDistributionCode.value == '') 
			{
			  alert("Silakan pilih Kode Pesanan yg tersedia!");
			  return;
			}
			
			showPopUp('../Salesman/FrmSalesmanUniformReceiver.aspx?d=' + txtDealerCode.value + '&u=' + TxtDistributionCode.value ,'',600,600,SalesmanSelection);
			/*var ddlUniform=document.getElementById("ddlSalesmanUniform");
			if (ddlUniform.value == -1) 
			{
			  alert("Silakan pilih kode seragam yg tersedia!");
			  return;
			}*/
			
			//var myDate = new Date( );
			//showPopUp('../Salesman/FrmSalesmanUniformReceiver.aspx?d=' + txtDealerCode.value + '&u=' + ddlUniform.value + '&time='+myDate.getTime( ) ,'',600,600,SalesmanSelection);
		}
		
		function SalesmanSelection(QuerryString)
		{
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SERAGAM TENAGA PENJUAL - Pilih Tenaga 
						Penjual Penerima Seragam</td>
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
								<TD style="WIDTH: 375px; HEIGHT: 10px" width="375"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
										runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="10%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 3px" width="24%">Kode Pesanan</TD>
								<TD style="HEIGHT: 3px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 375px; HEIGHT: 3px" width="375">
									<asp:textbox onkeypress="TxtKeypress();" id="TxtDistributionCode" runat="server" Width="128px"
										ToolTip="Dealer Search 1"></asp:textbox>
									<asp:label id="lblPopupDistribution" runat="server" width="16px" onclick="ShowPPUnifCodeSelection()">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label><input id="btnSelectSalesman" style="WIDTH: 132px; HEIGHT: 21px" onclick="ShowPPSalesmanSelection();"
										type="button" value="Pilih Penerima Seragam" name="btnSelectSalesman" runat="server">
								<TD class="titleField" style="HEIGHT: 3px" width="10%"></TD>
								<TD style="HEIGHT: 3px" width="1%"></TD>
								<TD style="HEIGHT: 3px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px" width="24%">Kategori</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD style="WIDTH: 375px; HEIGHT: 23px" width="375"><asp:dropdownlist id="ddlJobPositionDesc" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 23px" width="10%"></TD>
								<TD style="HEIGHT: 23px" width="1%"></TD>
								<TD style="HEIGHT: 23px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px" width="24%">Periode</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD style="WIDTH: 375px; HEIGHT: 23px" width="375"><asp:dropdownlist id="ddlYear" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 23px" width="10%"></TD>
								<TD style="HEIGHT: 23px" width="1%"></TD>
								<TD style="HEIGHT: 23px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 25px" width="24%"></TD>
								<TD style="HEIGHT: 25px" width="1%"></TD>
								<TD style="WIDTH: 375px; HEIGHT: 25px" width="375"><asp:button id="btnSearch" Width="60px" Runat="server" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 25px" width="10%"></TD>
								<TD style="HEIGHT: 25px" width="1%"></TD>
								<TD style="HEIGHT: 25px" width="10%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="WIDTH: 375px; HEIGHT: 20px" noWrap width="375"></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="10%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="10%"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgSalesmanUniformAssigned" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAll('chkSelection',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkSelection" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label3" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanUniform.SalesmanUnifDistribution.SalesmanUnifDistributionCode"
													HeaderText="Kode Pesanan">
													<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label4" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanUniform.SalesmanUnifDistribution.SalesmanUnifDistributionCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Kode Tenaga Penjual">
													<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblSalesmanCode Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Tenaga Penjual">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblSalesmanName Runat="server" Text= '<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" SortExpression="SalesmanHeader.Gender" HeaderText="GenderCode">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblGenderCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Gender")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Gender">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblGender" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Posisi">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPosisi" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="Level">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" Runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Lama Kerja(bulan)">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblLamaKerja" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.LamaBekerjaBulan")  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID")  %>' CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
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
					<TD style="HEIGHT: 30px"><asp:button id="btnRilis" Width="80" Runat="server" Text="Rilis"></asp:button><asp:button id="btnSimpan" Width="80" Runat="server" Text="Setuju" Visible="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
