<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmRegularOrderConfirmation.aspx.vb" Inherits="FrmRegularOrderConfirmation" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmRegularOrderConfirmation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PESANAN&nbsp;- Konfirmasi Pemesanan RO</TD>
							</TR>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblJudulDealer" runat="server">Kode Dealer</asp:label></TD>
					<TD><asp:label id="lblDealerTanda" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblDealerCode" runat="server"></asp:label><asp:textbox id="txtDealerCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" Width="144px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label2" runat="server">Tanggal  Konfirmasi</asp:label></TD>
					<TD><asp:label id="lblTanggalTanda" runat="server">:</asp:label></TD>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><cc1:inticalendar id="icConfirDate" runat="server" TextBoxWidth="70" ></cc1:inticalendar></TD>
								<td>
									<asp:label id="lblSampai" runat="server">s/d</asp:label>
									&nbsp;
								</td>
								<td><cc1:inticalendar id="icSampai" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblJudulReason" runat="server">Reason</asp:label></TD>
					<TD><asp:label id="lblReasonTanda" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtReason" runat="server" Width="248px" TextMode="MultiLine" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
							onblur="omitSomeCharacter('txtReason','<>?*%$;')"></asp:textbox><asp:requiredfieldvalidator id="reqReason" runat="server" ErrorMessage="*" ControlToValidate="txtReason"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD><asp:label id="lblButtonTanda" runat="server">:</asp:label></TD>
					<TD><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnCancel" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:button><asp:button id="btnSearch" runat="server" Text="Cari" Width="60px" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgRegulerOrderConfirmation" runat="server" Width="760px" AllowCustomPaging="True"
								AllowSorting="True" AllowPaging="True" PageSize="25" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None"
								BorderColor="#CCCCCC">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<ItemStyle ForeColor="#000066"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ConfirmationDate" SortExpression="ConfirmationDate" HeaderText="Tanggal Konfirmasi"
										DataFormatString="{0:d}">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Reason" SortExpression="Reason" HeaderText="Alasan">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
