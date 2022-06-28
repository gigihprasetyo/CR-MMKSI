<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPQRListBB.aspx.vb" Inherits="FrmPQRListBB" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPQRList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">

		    function ShowPPDealerBranchSelection() {
		        var lblDealer = document.getElementById("lblKodeDealer");
		        var dealerCode = lblDealer.innerText.split("-")[0].replace(/\s/g, '').trim();
		        showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + dealerCode, '', 500, 760, DealerBranchSelection);
		    }

		    function DealerBranchSelection(selectedDealerBranch) {
		        var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
		        txtDealerBranchSelection.value = selectedDealerBranch;
		    }
            		    
			function ShowPPDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam[0];
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
			}
			function PopUpHistory()
			{
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PRODUCT QUALITY REPORT SPECIAL-&nbsp; Daftar PQR</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblDealerSearch" runat="server">Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server" MaxLength="10"></asp:textbox><asp:label id="lblKodeDealer" runat="server"></asp:label><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
								<TD class="titleField" width="19%"><asp:label id="lblStat" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></TD>
							</TR>
                            <tr>
                                <td class="titleField">Cabang</td>
                                <td></td>
                                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtKodeDealerBranch" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server" MaxLength="10"></asp:textbox><asp:label id="lblDealerBranch" runat="server"></asp:label><asp:label id="lblPopUpDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></td>
                                <td class="titleField">Model</td>
                                <td>:</td>
                                <td><asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></td>
                            </tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px"><asp:label id="lblPQRNoSearch" runat="server">No PQR</asp:label></TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPQRNo" onblur="omitSomeCharacter('txtPQRNo','<>?*%$;')"
										runat="server" MaxLength="25"></asp:textbox></TD>
                                <td><strong>Kode Posisi</strong></td>
                                <td>:</td>
                                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodePosisi" onblur="omitSomeCharacter('txtKodePosisi','<>?*%$;')"
										runat="server" MaxLength="50" Width="132px"></asp:textbox></td>
								<%--<TD class="titleField" style="HEIGHT: 18px">Model</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></TD>--%>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" height="20"><asp:label id="lblTglApplySearch" runat="server">Tgl Pembuatan</asp:label></TD>
								<TD>:</TD>
								<TD>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr vAlign="top">
											<td><asp:checkbox id="chkFilterTanggal" runat="server" Checked="True"></asp:checkbox></td>
											<td><cc1:inticalendar id="icTglApplyDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<td vAlign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
											<td><cc1:inticalendar id="icTglApplySampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField">Dibuat Oleh</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlCreator" runat="server"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<TD class="titleField"><asp:label id="Label1" runat="server">Jenis PQR</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlPqrType" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" style="HEIGHT: 18px"><asp:label id="lblProcessBy" runat="server">Di Proses Oleh</asp:label></TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="HEIGHT: 18px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtProcessBy" onblur="omitSomeCharacter('txtProcessBy','<>?*%$;')"
										runat="server" MaxLength="50"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="titleField">&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD style="HEIGHT: 18px"><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></TD>
								<TD class="titleField" style="HEIGHT: 18px" colspan="3"></TD>
							</tr>
							<tr>
								<td colspan="6" align="center">&nbsp;</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dgListPQR" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
					CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
					PageSize="50" AllowPaging="True" BorderStyle="None" DataKeyField="ID">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
							<HeaderTemplate>
								<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelection" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblNo" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="RowStatus" HeaderText="Status">
							<HeaderStyle ForeColor="White" CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblStatus" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Tooltip = '<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' ID="lblDealer" NAME="lblDealer">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Tooltip = '<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>' ID="lblDealerBranch" NAME="lblDealerBranch">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="PQRNo" SortExpression="PQRNo" HeaderText="Nomor PQR">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PQRType" SortExpression="PQRType" HeaderText="Jenis PQR">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DocumentDate" SortExpression="DocumentDate" HeaderText="Tgl Pembuatan"
							DataFormatString="{0:dd/MM/yyyy}">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="FinishDate" HeaderText="Tgl Selesai">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblTglSelesai"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ChassisMasterBB.ChassisNumber" HeaderText="No Rangka">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>' ID="lblNoChassis" NAME="lblNoChassis">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Model">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode") %>' ID="lblCategory" NAME="lblCategory">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject">
							<HeaderStyle width="15%" CssClass="titleTableService"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Kode Posisi">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblKodePosisi"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ConfirmBy" HeaderText="Diproses Oleh">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblProcessUser"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="CreatedBy" HeaderText="Dibuat Oleh">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblCreatedUser"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pesan">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="lnkbtnAdditionalInfoIcon" runat="server" Width="20px" CausesValidation="False" CommandName="AdditionalInfo" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
									<img id="img" runat="server" src="../images/edit.gif" border="0"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Buat WSC">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="lnkEditWSC" runat="server" Width="20px" CausesValidation="False" CommandName="BWSC" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
									<img alt="" id="imgEditWSC" runat="server" src="../images/dok.gif" border="0" /></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>

						<asp:TemplateColumn HeaderText="Edit">
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
								<asp:LinkButton id="lnkbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
									<img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
								<asp:LinkButton id="lnkbtnDelete" runat="server" Text="Hapus" Visible="false" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
									<img alt="Hapus" src="../images/trash.gif" border="0"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle CssClass="titleTableService"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblHistoryStatus" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="History Perubahan Status"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<br>
			<asp:button id="btnDownload" runat="server" Width="72px" Text="Download"></asp:button><asp:panel class="titleField" id="pnlChangeStatus" Visible="False" Runat="server" Height="1px">Mengubah Status : 
<asp:dropdownlist id="ddlStatus2" runat="server" Width="140" Visible="False"></asp:dropdownlist>
<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Visible="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp; 
</asp:panel></form>
	</body>
</HTML>
