<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFreePPh.aspx.vb" Inherits="FrmFreePPh"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFreePPh</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td style="HEIGHT: 18px" class="titlePage">UMUM&nbsp;&nbsp;- Pengajuan&nbsp;Bebas 
						PPh</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD style="HEIGHT: 17px" class="titleField" width="22%">Tipe Organisasi</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 287px; HEIGHT: 17px" width="287"><asp:dropdownlist id="ddlOrgType" runat="server" Width="140px"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 17px" class="titleField" width="20%"></TD>
								<TD style="HEIGHT: 17px" width="1%"></TD>
								<TD style="HEIGHT: 17px" width="20%"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px" class="titleField" width="22%"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD style="WIDTH: 800px; HEIGHT: 17px" width="800" colSpan="4"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" Width="136px"></asp:textbox><asp:label id="lblSearchDealer" onclick="ShowPPDealerSelection();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR vAlign="top">
								<TD style="HEIGHT: 17px" class="titleField"><asp:label id="Label5" runat="server" Width="88px">Bebas PPh22</asp:label></TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 1000px; HEIGHT: 17px" colSpan="4">
									<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<td><asp:checkbox id="chkPeriod" runat="server" Text="Selama Periode "></asp:checkbox></td>
											<TD><cc1:inticalendar id="icStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px" class="titleField">Nomor Surat Keterangan Bebas Pemungutan 
									PPh Pasal 22</TD>
								<TD style="HEIGHT: 17px">:</TD>
								<TD style="WIDTH: 287px; HEIGHT: 17px"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtNoSurat" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server" Width="152px" MaxLength="50"></asp:textbox><asp:label style="Z-INDEX: 0" id="lblIsi" runat="server" Visible="False" ForeColor="Red">*</asp:label></TD>
								<TD style="HEIGHT: 17px" class="titleField"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD class="titleField">Dibuat Oleh</TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 287px"><asp:label id="lblCreatedBy" runat="server" Width="224px"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD><STRONG>Disetujui Oleh</STRONG></TD>
								<TD>:</TD>
								<TD style="WIDTH: 287px"><asp:label id="lblApprovedBy" runat="server" Width="224px"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 287px"></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 287px"><asp:button id="btnBaru" runat="server" Width="60px" Text="Baru" CausesValidation="False"></asp:button><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button><asp:button id="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:button></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<DIV style="HEIGHT: 320px; OVERFLOW: auto" id="div1"><asp:datagrid id="dtgMain" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
								AllowCustomPaging="True" PageSize="50" AllowPaging="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
								<EditItemStyle VerticalAlign="Top"></EditItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" BackColor="Teal"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
											<asp:Label id="lblID" style="width:0px;visibility:hidden;" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PeriodStart" HeaderText="Periode">
										<HeaderStyle ForeColor="White" Width="18%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.PeriodStart"),"dd/MM/yy") & " - " & format(DataBinder.Eval(Container, "DataItem.PeriodEnd"),"dd/MM/yy")  %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LetterNumber" HeaderText="Nomor Surat">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoSurat" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LetterNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProposedBy" HeaderText="Dibuat Oleh">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProposedBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProposedBy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProposedDate" HeaderText="Tanggal Pengajuan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProposedDate" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.ProposedDate"),"dd/MM/yy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="KTBApprovalBy" HeaderText="Disetujui Oleh">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKTBApprovalBy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KTBApprovalBy") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="KTBApprovalDate" HeaderText="Tanggal Persetujuan">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblKTBApprovalDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KTBApprovalDate") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
											<asp:Label id="lblStatusOri" style="width:0px;visibility:hidden;" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnApprove" runat="server" CommandName="Approve">
												<img src="../images/aktif.gif" alt="Approve" border="0" style="cursor:hand"></asp:LinkButton>
											</asp:Label>
											<asp:LinkButton id="lbtnReject" runat="server" CommandName="Reject">
												<img src="../images/in-aktif.gif" alt="Reject" border="0" style="cursor:hand"></asp:LinkButton>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" alt="Edit" border="0" style="cursor:hand"></asp:LinkButton>
											</asp:Label>
											<asp:LinkButton id="lbtnView" runat="server" CommandName="View">
												<img src="../images/detail.gif" alt="Lihat" border="0" style="cursor:hand"></asp:LinkButton>
											</asp:Label>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" alt="Hapus" border="0" style="cursor:hand"></asp:LinkButton>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
