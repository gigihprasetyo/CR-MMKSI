<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListParameterEvent.aspx.vb" Inherits="ListParameterEvent" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListParameterEvent</title>
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
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
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
		function ShowEventViewer(id)
		{
			showPopUp('../PopUp/PopUpEventParameterView.aspx?id=' + id);
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 31px">EVENT&nbsp;-&nbsp;List Parameter Event</td>
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
											<td><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Kode Dealer harus diisi"
													ControlToValidate="txtDealerCode" Display="Dynamic">*</asp:requiredfieldvalidator></td>
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
							<TR id="trStatus" runat="server">
								<TD class="titleField" style="HEIGHT: 19px">Status Event</TD>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField" colSpan="5">
									<asp:dropdownlist id="ddlStatusEvent" Runat="server"></asp:dropdownlist></TD>
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
					<td style="DISPLAY: none">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr id="trSearchJenisKegiatan" runat="server">
								<td class="titleField" style="WIDTH: 100px; HEIGHT: 19px">Jenis Kegiatan</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField"><asp:label id="lblSearchJenisKegiatan" Runat="server"></asp:label></TD>
							</tr>
							<tr id="trSearchNamaKegiatan" runat="server">
								<td class="titleField" style="WIDTH: 100px; HEIGHT: 19px">Nama Kegiatan</td>
								<TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
								<TD class="titleField"><asp:label id="lblSearchNamaKegiatan" Runat="server"></asp:label></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 240px" DESIGNTIMEDRAGDROP="1988">
							<asp:datagrid id="dtgEvent" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
								AllowCustomPaging="True" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
										<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
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
									<asp:TemplateColumn SortExpression="ActivityType.ActivityName" HeaderText="Jenis Kegiatan">
										<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.ActivityType.ActivityName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="EventName" SortExpression="EventName" HeaderText="Nama Kegiatan">
										<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="EventStatus" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblEventStatus" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.EventStatus") = CType(KTB.DNet.Domain.EnumEventParameter.StatusEventParameter.Baru, Short), KTB.DNet.Domain.EnumEventParameter.StatusEventParameter.Baru.ToString, KTB.DNet.Domain.EnumEventParameter.StatusEventParameter.Kirim.ToString) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblDealerCode" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle ForeColor="White" Width="27%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblDealerName" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FileNameMaterial" HeaderText="File Material">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton ID="lnbFileMaterial" Runat="server" CommandName="FileDownload" CausesValidation="False" CommandArgument='<%# String.Format("{0}\{1}", DataBinder.Eval(Container, "DataItem.DirTarget"), DataBinder.Eval(Container, "DataItem.FileNameMaterial")) %>'>
												<%# DataBinder.Eval(Container, "DataItem.FileNameMaterial") %>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FileNameJuklak" HeaderText="Juklak">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton ID="lnbFileJuklak" Runat="server" CommandName="FileDownload" CausesValidation="False" CommandArgument='<%# String.Format("{0}\{1}", DataBinder.Eval(Container, "DataItem.DirTarget"), DataBinder.Eval(Container, "DataItem.FileNameJuklak")) %>'>
												<%# DataBinder.Eval(Container, "DataItem.FileNameJuklak") %>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<A href='javascript:ShowEventViewer("<%# DataBinder.Eval(Container, "DataItem.ID") %>");'>
												<IMG alt="Lihat" src="../images/detail.gif" border="0"></A>
											<asp:LinkButton id=lbtnEdit runat="server" Width="20px" CausesValidation="False" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id=lbtnDelete runat="server" Width="20px" CausesValidation="False" Text="Hapus" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</DIV>
					</td>
				</tr>
				<tr>
					<td style="TEXT-ALIGN: center">
						<asp:button id="btnSend" runat="server" Width="64px" Text="Kirim" CausesValidation="False"></asp:button>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
