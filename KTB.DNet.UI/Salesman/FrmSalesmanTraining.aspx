<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanTraining.aspx.vb" Inherits="FrmSalesmanTraining" smartNavigation="False"%>
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

			function ShowPopUpTrainingMember()
			{
				var trainingCode = document.getElementById("ddlTrainingCode");
				var dealer = document.getElementById("txtDealerCode");
						
				if (trainingCode.value=="")
				{
					alert('Kode Training harus diisikan terlebih dahulu');
				}
				else
				{
					showPopUp('../Salesman/FrmSalesmanTrainingMember.aspx?strTrainingId='+ trainingCode.value +'&dealer=' + dealer.value,'',600,760,TrainingMember);
					//showPopUp('../Salesman/FrmSalesmanTrainingMember.aspx?strTrainingId='+ trainingCode.value +'&Time=' + myDate.getTime(),'',500,760,TrainingMember);

					// melakukan postback
					//document.Form1.submit();
					
				}
			}

			function TrainingMember(selectedMember)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				var arrValue = selectedMember
				__doPostBack('__Page','AddMember');
			}

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
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =selectedDealer;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">PELATIHAN TENAGA PENJUAL - Pilih Peserta 
						Pelatihan</td>
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
								<TD class="titleField" style="HEIGHT: 17px" width="24%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%"><asp:textbox onkeypress="TxtKeypress();" id="txtDealerCode" onblur="TxtBlur('txtDealerCode');"
										runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Kode Training</TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%"><asp:dropdownlist id="ddlTrainingCode" runat="server" Width="152px"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%">&nbsp;
									<asp:button id="btnPilih" runat="server" Width="120px" Text="Pilih Peserta Traning"></asp:button></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%">Level</TD>
								<TD style="HEIGHT: 10px" width="1%">
									<asp:label id="Label2" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%">
									<asp:dropdownlist id="ddlLevel" runat="server" Width="152px"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"></TD>
								<TD style="HEIGHT: 10px" width="1%"></TD>
								<TD style="HEIGHT: 10px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="24%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" noWrap width="25%"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD class="titleField" style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
								<TD style="HEIGHT: 11px"></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgSalesmanTraining" runat="server" Width="100%" AllowPaging="True" PageSize="25"
											AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblTrainingParticipantId" Runat="server" Visible="False"></asp:Label>
														<asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblSalesmanId" Runat="server" Visible="False"></asp:Label>
														<asp:Label ID="lblSalesmanCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Posisi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPosition" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Level">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLevel" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tanggal Mulai Bekerja">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblWorkPeriod" Text ='<%# format(DataBinder.Eval(Container, "DataItem.SalesmanHeader.HireDate"),"dd/MM/yyyy") %>' Runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Pengesahan">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														Pengesahan
														<br>
														<input id="chkAllValidation" type="checkbox" onclick="CheckAll('chkValidation',
														document.forms[0].chkAllValidation.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox ID="chkValidation" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Pembatalan">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														Pembatalan
														<br>
														<input id="chkAllCancel" type="checkbox" onclick="CheckAll('chkCancel',
														document.forms[0].chkAllCancel.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox ID="chkCancel" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View" Visible="False">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit"
															Visible="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
						<asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 8px"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
