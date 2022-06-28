<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmGradeDealer.aspx.vb" Inherits=".frmGradeDealer" smartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimDealerETA</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function ShowPPDealerSelectionOne() {
		        showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }
		    function DealerSelection(selectedDealer) {
		        var txtDealerSelection = document.getElementById("txtDealerCode");
		        txtDealerSelection.value = selectedDealer;
		    }
		    function ShowDataHistory(sUrl) {
		        //alert(sUrl);return false;
		        showPopUp(sUrl, '', 500, 760);
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">Partshop - Grade Dealer </td>
							</tr>
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
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Kode</asp:label>Dealer</TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="128px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" noWrap>Grade Sparepart</TD>
					<td>:</td>
					<TD><asp:textbox   id="txtETA" onblur="omitSomeCharacter('txtETA','-')"
							   runat="server" Width="64px" MaxLength="2"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 360px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgClaimDealerETA" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Center></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="Dealer.ID" HeaderText="Dealer ID">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.ID" HeaderText="Kode Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#  (DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")) %>' ID=lblDealerCode >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' ID="lblDealerName" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Search Term 2" SortExpression="Dealer.SearchTerm2">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm2") %>' ID="lblSearchTerm2" >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SparePartGrade" SortExpression="SparePartGrade" HeaderText="SparePart Grade">
										<HeaderStyle Width="40%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False" visible="false"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:Label id="lblHistoryStatus" runat="server" visible="false">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Lihat Perubahan Data"></asp:Label>
															
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
		    if (window.parent == window) {
		        if (!navigator.appName == "Microsoft Internet Explorer") {
		            self.opener = null;
		            self.close();
		        }
		        else {
		            this.name = "origWin";
		            origWin = window.open(window.location, "origWin");
		            window.opener = top;
		            window.close();
		        }
		    }
		</script>
	</body>
</HTML>
