<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListAuditSchedule.aspx.vb" Inherits="FrmListAuditSchedule" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListAuditSchedule</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
			function ShowAuditParameter()
			{
				var txtDealerCode= document.getElementById("txtDealerCode");
				var txtUser= document.getElementById("txtUser");
				if (txtUser.value == "KTB" || txtUser.value == "MKS")
				{
					if (txtDealerCode.value != '')
					{
						showPopUp('../PopUp/PopUpAuditParameters.aspx?DealerCode='+ txtDealerCode.value,'',500,760,AuditSelection);
					}
					else
					{
						showPopUp('../PopUp/PopUpAuditParameters.aspx','',500,760,AuditSelection);
					}
				}
				else
				{ // case from dealer
					showPopUp('../PopUp/PopUpAuditParameters.aspx','',500,760,AuditSelection);
				}
				
				
			}
			function AuditSelection(selectedData)
			{				
				var txtAuditNo= document.getElementById("txtAuditCode");
				txtAuditNo.value = selectedData;
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,660,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{				
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;
			}
			function GetSelectedItem()
			{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgAuditScheduleDealer");
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					bcheck=true;
				}
			}
			if (bcheck)
			  {
				return true;
			  }
			else
			  {
				alert("Silahkan Pilih Data terlebih dahulu");	
				return false;
			  }
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px">SHOWROOM AUDIT-&nbsp;Daftar Jadwal
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" width="20%">Periode</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:dropdownlist id="ddlPeriode" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Kode Dealer</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%"><asp:textbox id="txtDealerCode" Runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:label id="lblKodeDealer" runat="server">Label</asp:label><INPUT id="txtUser" runat="server" type="hidden"></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Kode Audit</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtAuditCode" onblur="omitSomeCharacter('txtAuditCode','<>?*%$')"
										Runat="server"></asp:textbox><asp:label id="lblAuditSearch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowAuditParameter()"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%"></td>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" width="69%"><asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%"></td>
								<TD width="1%"></TD>
								<TD width="69%"></TD>
							</TR>
							<TR>
								<td vAlign="top" width="100%" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 340px"><asp:datagrid id="dtgAuditScheduleDealer" runat="server" Width="100%" CellSpacing="1" AllowSorting="True"
											CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="Gainsboro" PageSize="50" AllowPaging="True" AutoGenerateColumns="False">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#EFEFEF"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="1%" CssClass="titleTableRsd"></HeaderStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAll('chkItemChecked',document.forms[0].chkAllItems.checked)"
															type="checkbox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AuditSchedule.AuditParameter.Code" HeaderText="Kode Audit">
													<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditSchedule.AuditParameter.Code") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblItemNoAudit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="35%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AuditSchedule.AuditParameter.Period" HeaderText="Periode">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblItemPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditSchedule.AuditParameter.Period") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AuditSchedule.AuditParameter.JukLakFile" HeaderText="File Pelaksana">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblFilePelaksana" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditSchedule.AuditParameter.JuklakFile") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="AuditSchedule.AuditParameter.AssessmentItem" HeaderText="Item Penilaian">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblItemPenilaian" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditSchedule.AuditParameter.AssessmentItem") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" Runat="server" text="Ubah" Visible="True" CommandName="edit" CausesValidation="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDetail" Runat="server" text="Hasil" CommandName="hasil" CausesValidation="False">
															<img src="../images/detail.gif" border="0" alt="Hasil"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" Runat="server" text="Hapus" CommandName="delete" CausesValidation="False">
															<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');""></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="ID">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False" HeaderText="AuditParameterID">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblAuditParameterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AuditSchedule.AuditParameter.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
										</asp:datagrid></div>
								</td>
							</TR>
							<TR>
								<TD colSpan="3"><asp:button id="btnRilis" runat="server" Width="64px" Text="Rilis"></asp:button></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td align="center"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
