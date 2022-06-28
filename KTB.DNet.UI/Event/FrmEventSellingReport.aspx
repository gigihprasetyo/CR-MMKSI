<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventSellingReport.aspx.vb" Inherits="FrmEventSellingReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventSellingReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" type="text/javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
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

		    function ShowPPDealerGroupSelection()
		    {
			    showPopUp('../PopUp/PopUpGroupDealerSelection.aspx?x=Territory','',500,760,DealerGroupSelection);
		    }
		    function DealerGroupSelection(selectedDealer)
		    {
			    var txtGroupDealer= document.getElementById("txtGroupDealer");
			    txtGroupDealer.value = selectedDealer;				
		    }
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="titlePage" style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"><asp:label id="lblTitle" Runat="server" Text="Event - Daftar Penyelesaian">Event - Form/Daftar Laporan Penjualan</asp:label></div>
			<table cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<td class="titleField" style="HEIGHT: 18px" width="15%"><asp:radiobutton id="rbGroupDealer" runat="server" Text="Group Dealer" GroupName="Dealer"></asp:radiobutton></td>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD style="HEIGHT: 18px">
						<table cellSpacing="0" cellPadding="0">
							<tr>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtGroupDealer" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
										runat="server" Width="136px"></asp:textbox><asp:dropdownlist id="ddlGroupDealer" Runat="server" Visible="False"></asp:dropdownlist></td>
								<td><asp:label id="lblSearchGroupCode" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Dealer" src="../images/popup.gif"
											border="0"></asp:label></td>
								<td></td>
							</tr>
						</table>
					</TD>
					<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Area</td>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlSalesmanArea" Runat="server" Width="142px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:radiobutton id="rbDealer" runat="server" Text="Kode Dealer" GroupName="Dealer" Checked="True"></asp:radiobutton></TD>
					<TD style="WIDTH: 1px" width="1">:</TD>
					<TD style="HEIGHT: 18px">
						<table cellSpacing="0" cellPadding="0">
							<tr>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
										runat="server" Width="136px"></asp:textbox></td>
								<td><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Dealer" src="../images/popup.gif"
											border="0"></asp:label></td>
								<td></td>
							</tr>
						</table>
					</TD>
					<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Periode Kegiatan</td>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<td><asp:checkbox id="cbPeriode" Runat="server" AutoPostBack="True"></asp:checkbox></td>
								<TD><cc1:inticalendar id="calDari" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></TD>
								<TD>&nbsp;&nbsp;s.d&nbsp;&nbsp;</TD>
								<TD><cc1:inticalendar id="calSampai" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top">Jenis Kegiatan</TD>
					<TD style="WIDTH: 1px" vAlign="top" width="1">:</TD>
					<TD colSpan="5">
						<table cellSpacing="0" cellPadding="0">
							<tr>
								<td><asp:dropdownlist id="ddlJenisKegiatan" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
								<td>
									<TABLE id="tblCategoryModelType" cellPadding="0" border="0" runat="server">
										<TR>
											<TD class="titleField">Kategori</TD>
											<TD><asp:dropdownlist id="ddlCategory" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
											<TD class="titleField">Model</TD>
											<TD><asp:dropdownlist id="ddlModel" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
											<TD class="titleField">Tipe</TD>
											<TD><asp:dropdownlist id="ddlType" Runat="server"></asp:dropdownlist></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 18px">Nama Kegiatan</TD>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD style="HEIGHT: 18px">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:dropdownlist id="ddlNamaKegiatan" Runat="server"></asp:dropdownlist></td>
								<td class="titleField" style="WIDTH: 50px; HEIGHT: 18px; TEXT-ALIGN: right">Tahun</td>
								<TD style="WIDTH: 10px; HEIGHT: 18px; TEXT-ALIGN: center" width="1">:</TD>
								<td><asp:dropdownlist id="ddlYear" Runat="server"></asp:dropdownlist></td>
							</tr>
						</table>
					</TD>
					<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Tanggal Acara</td>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD style="HEIGHT: 18px">
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:checkbox id="cbEventDate" Runat="server" AutoPostBack="True"></asp:checkbox>
								</td>
								<td>
									<cc1:inticalendar id="calEventDate" runat="server" TextBoxWidth="60" Enabled="False"></cc1:inticalendar>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<td class="titleField"></td>
					<TD style="WIDTH: 1px" width="1"></TD>
					<TD colSpan="5"><asp:button id="btnCari" runat="server" Text="Cari" Width="64px" CausesValidation="False"></asp:button><asp:button id="btnSave" runat="server" Text="Simpan" Width="64px" Visible="False" CausesValidation="True"></asp:button><asp:button id="btnNew" runat="server" Text="Baru" Width="64px" CausesValidation="False"></asp:button><asp:button id="btnCancel" runat="server" Text="Cancel" Width="64px" Visible="False" CausesValidation="False"></asp:button></TD>
				</TR>
			</table>
			<br>
			<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="1988"><asp:datagrid id="dtgEvent" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
					AllowPaging="True" AllowCustomPaging="True" PageSize="25" AutoGenerateColumns="False">
					<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle ForeColor="White"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle CssClass="titleTablePromo" BackColor="White"></ItemStyle>
							<HeaderTemplate>
								<INPUT id="chkAllItems" onclick="CheckAll('chkSelect',document.all.chkAllItems.checked)"
									type="checkbox">
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblDealerCode"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Group/Kode Dealer">
							<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblDealerName"></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList Runat="server" ID="ddlDealerGroupFooter"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:DropDownList Runat="server" ID="ddlDealerGroupItem"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="VechileType.Category.Description" HeaderText="Kategori">
							<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=Label12 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Category.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList id="ddlCategoryFooter" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCarModel_SelectedIndexChanged"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlCategoryItem" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCarModel_SelectedIndexChanged"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Type Kendaraan">
							<HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList Runat="server" ID="ddlVehicleTypeFooter"></asp:DropDownList>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:DropDownList Runat="server" ID="ddlVehicleTypeItem"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False" SortExpression="Description" HeaderText="Deskripsi Kendaraan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:TextBox Runat="server" ID="txtCarDescriptionFooter" MaxLength="100"></asp:TextBox>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox Runat="server" ID="txtCarDescriptionItem" MaxLength="100" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Jumlah" HeaderText="Jumlah">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Jumlah", "{0:N0}") %>'>
								</asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:TextBox Runat="server" ID="txtJumlahFooter" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:TextBox Runat="server" ID="txtJumlahItem" onkeypress="return NumericOnlyWith(event,'');" Text='<%# DataBinder.Eval(Container, "DataItem.Jumlah") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id=lnbCardEdit runat="server" EnableViewState="False" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
									<img src="../images/edit.gif" border="0" alt="Ubah">
								</asp:LinkButton>
								<asp:LinkButton id=lnbCarDelete runat="server" EnableViewState="False" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus">
								</asp:LinkButton>
							</ItemTemplate>
							<FooterTemplate>
								<asp:LinkButton id="lnkAdd" runat="server" EnableViewState="False" CausesValidation="false" CommandArgument="0"
									CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah">
								</asp:LinkButton>
							</FooterTemplate>
							<EditItemTemplate>
								<asp:LinkButton id=lnbCarUpdate runat="server" EnableViewState="False" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Update">
									<img src="../images/simpan.gif" border="0" alt="Simpan">
								</asp:LinkButton>
								<asp:LinkButton id="lnbCarCancel" runat="server" EnableViewState="False" CausesValidation="false"
									CommandArgument="0" CommandName="Cancel">
									<img src="../images/batal.gif" border="0" alt="Batal">
								</asp:LinkButton>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></DIV>
			<br>
			<asp:button id="btnDownload" runat="server" Text="Download"></asp:button></form>
	</body>
</HTML>
