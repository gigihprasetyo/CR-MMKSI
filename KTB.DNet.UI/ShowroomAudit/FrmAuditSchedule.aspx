<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAuditSchedule.aspx.vb" Inherits="FrmAuditSchedule" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAuditSchedule</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowAuditParameter()
		{
			showPopUp('../PopUp/PopUpAuditParameter.aspx?isInput=1','',520,780,AuditSelection);
		}
		function AuditSelection(selectedCode)
		{
			var tempParam = selectedCode.split(';');
			var txtEventID = document.getElementById("txtAuditNo");
			var lblPeriod = document.getElementById("lblPeriod");
				
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			txtEventID.innerText = tempParam[0];
			lblPeriod.innerText = tempParam[1];
			}
			else
			{
			txtEventID.value = tempParam[0];
			lblPeriod.value = tempParam[1];
			}		
		}
		
		function ShowPPDealerSelection()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',520,800,DealerSelection);
		}
		function DealerSelection(selectedDealer)
			{				
				var tempParam = selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = tempParam[0];
				/*
				document.all.hdnSelectedDealer.value = tempParam;
				document.getElementById(document.all.hdnFooterDealerID.value).innerText = tempParam[0];
				document.getElementById(document.all.hdnFooterDealerName.value).innerText = tempParam[1];
				*/
			}
		function ShowPopUpDealerOne()
		{
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,OneDealerSelection);
		}
		function OneDealerSelection(selectedCode)
		{
			/*
			var tempParam = selectedCode.split(';');
			var indek = GetCurrentInputIndex();
			var dgAuditor = document.getElementById("dtgAuditor");
			var txtDealer = dgAuditor.rows[indek].getElementsByTagName("INPUT")[0];
			var dealerName = dgAuditor.rows[indek].cells[2].childNodes[0]; //.getElementsByTagName("SPAN")[1];
			//var dddd =	dgAuditor.rows[indek].cells[1].outerHTML;
			//alert(dddd);
			if(navigator.appName == "Microsoft Internet Explorer")
			{
			txtDealer.innerText = tempParam[0];
			dealerName.innerText= tempParam[1];
			}
			else
			{
			txtDealer.value = tempParam[0];
			dealerName.value = tempParam[1];
			}
			*/		
		}
		function GetCurrentInputIndex()
			{
				var dgAuditor = document.getElementById("dtgAuditor");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgAuditor.rows.length; index++)
				{
					inputs = dgAuditor.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}
				
				return -1;
			}
			function checkSelectedAuditorType(ddl){
				if(ddl.options[ddl.selectedIndex].text == "DEALER"){
					var txt = ddl.parentNode.nextSibling.childNodes[0];
					txt.disabled = true;
					txt.value = document.getElementById("txtDealerCode").value;
				}
				else{
					var txt = ddl.parentNode.nextSibling.childNodes[0];
					txt.disabled = false;
					txt.value = '';	
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input id="hdnFooterDealerID" type="hidden" runat="server"> <input id="hdnFooterDealerName" type="hidden" runat="server">
			<input id="hdnSelectedDealer" type="hidden" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM AUDIT-&nbsp;Input Jadwal
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
								<td class="titleField" width="20%">Kode Audit</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAuditNo" onblur="omitSomeCharacter('txtAuditNo','<>?*%$;')"
										Runat="server" CssClass="textRight"></asp:textbox><asp:label id="lblAuditSearch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:button id="btnCari" runat="server" Text="Cari" Width="56px"></asp:button></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Periode</td>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:label id="lblPeriod" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Kode Dealer</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
										Runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%"></td>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" width="69%"></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%"></td>
								<TD width="1%"></TD>
								<TD width="69%"></TD>
							</TR>
							<TR>
								<td vAlign="top" width="100%" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 320px"><asp:datagrid id="dtgAuditor" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="Gainsboro"
											BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" ShowFooter="True" AllowSorting="True" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" VerticalAlign="Top" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#EFEFEF"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblFooterDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:Label id="lblEditDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
													<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left" Height="20px" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:Label id="lblFDealerName" runat="server"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:Label id="lblEDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
														</asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="StartDate" HeaderText="Mulai">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StartDate") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<cc1:inticalendar id="icFStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
													</FooterTemplate>
													<EditItemTemplate>
														<cc1:inticalendar id="icEStartDate" runat="server" TextBoxWidth="70" value='<%# DataBinder.Eval(Container, "DataItem.StartDate") %>'>
														</cc1:inticalendar>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="EndDate" HeaderText="Akhir">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndDate") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<cc1:inticalendar id="icFEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
													</FooterTemplate>
													<EditItemTemplate>
														<cc1:inticalendar id="icEEndDate" runat="server" TextBoxWidth="70" value='<%# DataBinder.Eval(Container, "DataItem.EndDate") %>'>
														</cc1:inticalendar>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Tipe">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditorTypeDesc") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left" VerticalAlign="Middle"></FooterStyle>
													<FooterTemplate>
														<asp:DropDownList id="ddlFAuditorType" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList id="ddlEAuditorType" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nama">
													<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblAuditorName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Auditor") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtFAuditorName" Runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEAuditorName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Auditor") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" Runat="server" text="Ubah" CommandName="edit" CausesValidation="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" Runat="server" text="Hapus" CommandName="delete" CausesValidation="False">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center" Width="20%"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" tabIndex="40" Runat="server" text="Tambah" CommandName="add">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="49" Runat="server" text="Simpan" CommandName="save">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" text="Batal" CommandName="cancel">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="btnSimpan" runat="server" Text="Simpan" Width="64px" Enabled="False"></asp:button><asp:button id="btnCancel" runat="server" Text="Batal"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali" Visible="False"></asp:button>
						<asp:button id="btnRilis" runat="server" Width="48px" Text="Rilis" Enabled="False"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
