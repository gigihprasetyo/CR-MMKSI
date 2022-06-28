<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPRPReport.aspx.vb" Inherits="FrmListPRPReport" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListPRPReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpSelectingDealer.aspx','',520,880,DealerSelection);
		}

		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
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
			EnableDelete(aspCheckBoxID)
		}
		
		function EnableDelete(aspCheckBoxID) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			document.forms[0].btnDelete.disabled = true
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						if (elm.checked)
						{
							document.forms[0].btnDelete.disabled = false
							return
						}
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 18px" colSpan="3">PARTSHOP REWARD 
						PROGRAM&nbsp;-&nbsp;Daftar Pengiriman Laporan PRP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<TR>
								<TD class="titleField" width="121" style="WIDTH: 121px">Kode Organisasi</TD>
								<TD width="11" style="WIDTH: 11px">:</TD>
								<TD><asp:label id="lblKodeOrganisasiValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px">Nama Organisasi</TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD><asp:label id="lblNamaOrganisasiValue" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px"></TD>
								<TD style="WIDTH: 11px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px">Kode Dealer</TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealerCode" runat="server" Width="128px"
										ToolTip="Dealer Search 1" MaxLength="10"></asp:textbox>
									<asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px">Status</TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD>
									<asp:DropDownList id="ddlStatus" runat="server">
										<asp:ListItem Value="1000">Silahkan Pilih</asp:ListItem>
										<asp:ListItem Value="0">Aktif</asp:ListItem>
										<asp:ListItem Value="-1">Tidak Aktif</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px">Created By</TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD>
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtCreatedBy" runat="server" MaxLength="10"
										ToolTip="Dibuat Oleh" Width="128px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 121px">Deskripsi</TD>
								<TD style="WIDTH: 11px">:</TD>
								<TD>
									<asp:TextBox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtDescription)" id="txtDescription"
										runat="server" Width="328px"></asp:TextBox>
									<asp:Button id="btnCari" runat="server" Width="64px" Text="Cari"></asp:Button></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgReportPRP" runat="server" AllowSorting="True" AllowPaging="True" PageSize="25"
							AutoGenerateColumns="False" Width="100%" AllowCustomPaging="True">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
									<HeaderStyle Width="0%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="0%" CssClass="titleTableParts"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<INPUT id="cbAll" onclick="CheckAll('cbItem',&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.forms[0].cbAll.checked)"
											type="checkbox">
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Kirim"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="CreatedBy" HeaderText="Kode Dealer">
									<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblOrganization" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Filename" SortExpression="Filename" HeaderText="Nama File">
									<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle HorizontalAlign="Center" Width="25%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
									<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStatus" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Created By">
									<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCreatedBy" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle HorizontalAlign="Center" Width="20%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnkDelete" runat="server" CommandName="Delete" Visible="False">
											<img src="../images/trash.gif" border="0" alt="Hapus">
										</asp:LinkButton>
										<asp:LinkButton id="lbtnActive" ToolTip="Aktifkan" CommandName="Active" CausesValidation="False"
											Runat="server" text="Aktif">
											<img border="0" src="../images/aktif.gif" alt="Aktifkan" style="cursor:hand"></asp:LinkButton>
										<asp:LinkButton id="lbtnInactive" ToolTip="Non Aktifkan" CommandName="Inactive" CausesValidation="False"
											Runat="server" text="Non-aktif">
											<img border="0" src="../images/in-aktif.gif" alt="Non-aktifkan" style="cursor:hand"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></div></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="6"><asp:button id="btnDelete" runat="server" Width="72px" Text="Hapus" Enabled="False"></asp:button>
						<asp:button id="btnDownload" runat="server" Width="72px" Text="Download"></asp:button></TD>
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
