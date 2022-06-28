<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListEventAgreement.aspx.vb" Inherits="ListEventAgreement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventAgreement</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
			function ShowEventProposalHistory(id)
			{
				showPopUp('../PopUp/PopUpEventProposalHistoryView.aspx?mode=Agree&id=' + id, '', 500, 700);
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 31px">
						EVENT&nbsp;-&nbsp;Proposal Persetujuan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="18%">Kode Dealer</td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
													runat="server" Width="312px"></asp:textbox></td>
											<td><asp:label id="lblSearchDealer" runat="server">
													<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
														border="0"></asp:label></td>
										</tr>
									</table>
								</TD>
								<td class="titleField" style="HEIGHT: 18px; TEXT-ALIGN: right">Area</td>
								<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlSalesmanArea" Width="142px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField">Periode Kegiatan</TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD width="82%" colSpan="5">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<td><asp:checkbox id="cbPeriode" Runat="server" AutoPostBack="True"></asp:checkbox></td>
											<TD><cc1:inticalendar id="calDari" runat="server" TextBoxWidth="60" Enabled="False"></cc1:inticalendar></TD>
											<TD>&nbsp;&nbsp;s.d&nbsp;&nbsp;</TD>
											<TD><cc1:inticalendar id="calSampai" runat="server" TextBoxWidth="60" Enabled="False"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Jenis Kegiatan</TD>
								<TD style="WIDTH: 1px" width="1">:</TD>
								<TD colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<TD><asp:dropdownlist id="ddlJenisKegiatan" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
											<td>
												<TABLE id="tblCategoryModelType" cellPadding="0" border="0" runat="server">
													<TR>
														<TD class="titleField">Kategori</TD>
														<TD><asp:dropdownlist id="ddlCategory" Width="100px" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD class="titleField">Model</TD>
														<TD><asp:dropdownlist id="ddlModel" Width="100px" Runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD class="titleField">Tipe</TD>
														<TD><asp:dropdownlist id="ddlType" Width="100px" Runat="server"></asp:dropdownlist></TD>
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
								<TD style="HEIGHT: 18px" colSpan="5">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:dropdownlist id="ddlNamaKegiatan" Runat="server"></asp:dropdownlist></td>
											<td class="titleField" style="WIDTH: 50px; HEIGHT: 18px; TEXT-ALIGN: right">Tahun</td>
											<TD style="WIDTH: 10px; HEIGHT: 18px; TEXT-ALIGN: center" width="1">:</TD>
											<td><asp:dropdownlist id="ddlYear" Runat="server"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<td class="titleField"></td>
								<TD style="WIDTH: 1px" width="1"></TD>
								<TD colSpan="5">
									<asp:button id="btnCari" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:button>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td class="titleField">
						<asp:Label ID="lblSubTitle" Runat="server"></asp:Label>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="1988">
							<asp:datagrid id="dtgEvent" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
								AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
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
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ActivitySchedule" HeaderText="Tanggal Kegiatan">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActivitySchedule", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ActivityPlace" HeaderText="Tempat Kegiatan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblActivityPlace" Text='<%# DataBinder.Eval(Container, "DataItem.ActivityPlace") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Ravine" HeaderText="Kelurahan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Ravine") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SubDistrict" HeaderText="Kecamatan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.SubDistrict") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Owner" HeaderText="Owner">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label4" Text='<%# DataBinder.Eval(Container, "DataItem.Owner") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Driver" HeaderText="Driver/ Co.Driver">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label5" Text='<%# DataBinder.Eval(Container, "DataItem.Driver") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="InvitationNumber" HeaderText="Jumlah Undangan">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InvitationNumber", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TotalCost" HeaderText="Biaya Diajukan">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label6 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ApproveCost" HeaderText="Biaya Disetujui">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblFavorCost Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApproveCost", "{0:#,##0.##}") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtFavorCost runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.ApproveCost", "{0:#,##0.##}") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SubsidiFile" HeaderText="Upload Surat Subsidi">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink id="lnkDownload" Runat="server">
												<%# DataBinder.Eval(Container, "DataItem.SubsidiFile") %>
											</asp:HyperLink>
										</ItemTemplate>
										<EditItemTemplate>
											<TABLE cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:HyperLink id="lnkEditDownLoad" Runat="server">
															<%# DataBinder.Eval(Container, "DataItem.SubsidiFile") %>
														</asp:HyperLink>
														<INPUT id="flUpload" type="file" runat="server" style="width: 80px;"></TD>
													<TD>
														<asp:LinkButton id="lnbRemoveFile" Runat="server" CommandName="DeleteFile" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img src="..\images\batal.gif" alt="Hapus file" border="0">
														</asp:LinkButton>
													</TD>
												</TR>
											</TABLE>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EventAgreementStatus" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnbEdit" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit">
											</asp:LinkButton>
											<A href='javascript:ShowEventProposalHistory("<%# DataBinder.Eval(Container, "DataItem.ID") %>");'>
												<IMG alt="Lihat Histori" src="../images/alur_flow.gif" border="0"></A>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lnbUpdate" runat="server" CausesValidation="false" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/simpan.gif" border="0" alt="Simpan">
											</asp:LinkButton>
											<asp:LinkButton id="lnbCancel" runat="server" CausesValidation="false" CommandName="Cancel">
												<img src="../images/batal.gif" border="0" alt="Batal">
											</asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</td>
				</tr>
				<tr>
					<td>
						<asp:button id="btnValidate" runat="server" Width="64px" Text="Validasi" CausesValidation="False"></asp:button>
						<asp:button id="btnExcelDwl" runat="server" Width="64px" Text="Download" CausesValidation="False"></asp:button>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
