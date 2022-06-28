<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSpOrderRestriction.aspx.vb" Inherits="FrmSpOrderRestriction"  smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSpOrderRestriction</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" type="text/javascript">
		function ShowPPDealerSelection()
		{			
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}		
		function ManageRadButton(obj)
		{
			if(obj == document.getElementById("rbtnDate"))
			{
				var radHr = document.getElementById("rbtnDay");
				var pnlTgl = document.getElementById("pnlTanggal");
				//var icStart = document.getElementById("icPeriodeStart");
				//var icEnd = document.getElementById("icPeriodeEnd");
				var ddDay = document.getElementById("ddlDays");
				
				obj.checked = true;				
				pnlTgl.style.display = "block";
				
				radHr.checked = false;
				ddDay.style.display = 'none';
			}
			else
			{
				var radTgl = document.getElementById("rbtnDate");
				var pnlTgl = document.getElementById("pnlTanggal");
				var ddDay = document.getElementById("ddlDays");
				
				obj.checked = true;
				ddDay.style.display = 'block';
				
				radTgl.checked = false;				
				pnlTgl.style.display = 'none';				
			}
		}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD colspan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PEMESANAN - Batas Waktu Pemesanan</TD>
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
					<TD class="titleField"><asp:label id="Label1" runat="server">Tipe Order</asp:label></TD>
					<TD>:</TD>
					<TD><asp:dropdownlist id="ddlOrderType" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label2" runat="server">Kode Dealer</asp:label></TD>
					<TD>:</TD>
					<TD><asp:textbox id="txtDealerCode" runat="server"  ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerCode" ErrorMessage="*"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="titleField" nowrap>
						<P><asp:radiobutton id="rbtnDate" runat="server" Text="Tanggal" Checked="True"></asp:radiobutton><asp:radiobutton id="rbtnDay" runat="server" Text="Hari"></asp:radiobutton></P>
					</TD>
					<TD>:</TD>
					<TD><asp:panel id="pnlTanggal" runat="server" Width="400px">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD>
										<cc1:inticalendar id="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
									<TD>s/d
									</TD>
									<TD>
										<cc1:inticalendar id="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:dropdownlist id="ddlDays" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR valign="top">
					<TD class="titleField"><asp:label id="Label3" runat="server">Jam</asp:label></TD>
					<TD>:</TD>
					<TD><asp:textbox id="txtHStart" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtHStart" ErrorMessage="*"></asp:requiredfieldvalidator>
						<asp:textbox id="txtMStart" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtMStart" ErrorMessage="*"></asp:requiredfieldvalidator>
						<asp:textbox id="txtSStart" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtSStart" ErrorMessage="*"></asp:requiredfieldvalidator>&nbsp;s/d
						<asp:textbox id="txtHEnd" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtHEnd" ErrorMessage="*"></asp:requiredfieldvalidator>
						<asp:textbox id="txtMEnd" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtMEnd" ErrorMessage="*"></asp:requiredfieldvalidator>
						<asp:textbox id="txtSEnd" runat="server" Width="32px" MaxLength="2"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtSEnd" ErrorMessage="*"></asp:requiredfieldvalidator><asp:rangevalidator id="RangeValidator1" runat="server" ControlToValidate="txtHStart" ErrorMessage="0<= Jam Awal <=23"
							Type="Integer" MinimumValue="0" MaximumValue="23"></asp:rangevalidator><asp:rangevalidator id="RangeValidator2" runat="server" ControlToValidate="txtHEnd" ErrorMessage="0<= Jam Akhir <=23"
							Type="Integer" MinimumValue="0" MaximumValue="23"></asp:rangevalidator><asp:rangevalidator id="RangeValidator3" runat="server" ControlToValidate="txtMStart" ErrorMessage="0<=Menit Awal <=59"
							Type="Integer" MinimumValue="0" MaximumValue="59"></asp:rangevalidator><asp:rangevalidator id="RangeValidator4" runat="server" ControlToValidate="txtMEnd" ErrorMessage="0<= Menit Akhir <=59"
							Type="Integer" MinimumValue="0" MaximumValue="59"></asp:rangevalidator><asp:rangevalidator id="RangeValidator5" runat="server" ControlToValidate="txtSStart" ErrorMessage="0<= Detik Awal <=59"
							Type="Integer" MinimumValue="0" MaximumValue="59"></asp:rangevalidator><asp:rangevalidator id="RangeValidator6" runat="server" ControlToValidate="txtSEnd" ErrorMessage="0<= Detik Akhir <=59"
							Type="Integer" MinimumValue="0" MaximumValue="59"></asp:rangevalidator></TD>
				</TR>
				<TR valign=top>
					<TD class="titleField">Catatan</TD>
					<TD>:</TD>
					<TD><asp:textbox id="txtNote" runat="server" Width="600px" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD class="titleField"><asp:checkbox id="chkActive" runat="server" Text="Aktif"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD><asp:button id="btnSave" Width="60px" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px"  Text="Batal"></asp:button><asp:button id="btnCari" runat="server" Width="60px"  Text="Cari" CausesValidation="False"></asp:button>
						<asp:button id="btnDeleteAll" runat="server" Text="Hapus semua" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 260px"><asp:datagrid id="dgOrderRestriction" runat="server" Width="98%" AutoGenerateColumns="False" BorderColor="#CCCCCC"
								BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" AllowPaging="True" AllowSorting="True" AllowCustomPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<ItemStyle ForeColor="#000066"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No" HeaderStyle-CssClass="titleTableParts"></asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="OrderType" HeaderText="Tipe Order" HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:Label id=lblTipeOrder runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderType") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderType") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderStyle-CssClass="titleTableParts" HeaderText="Kode Dealer"></asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="IsActive" HeaderText="Aktif" HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:Label id=lblAktive runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsActive") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsActive") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Harian" HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:Label id="lblHarian" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Mulai " HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:Label id="lblMulai" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sampai" HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:Label id="lblSampai" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-CssClass="titleTableParts">
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
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
