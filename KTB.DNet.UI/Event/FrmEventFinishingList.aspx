<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventFinishingList.aspx.vb" Inherits="FrmEventFinishingList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventFinishingList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
            function showDocumentation(eventproposalid)
            {
                showPopUp('../PopUp/PopUpEventProposalFile.aspx?id=' + eventproposalid,'',500,760,'');
            }
        
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
			function ShowSellingReport(id)
			{
				showPopUp('../PopUp/PopUpEventSellingReportView.aspx?id=' + id, '', 500, 700);
			}
			function ShowEventProposalViewer(id)
			{
				showPopUp('../PopUp/PopUpEventProposalView.aspx?id=' + id, '', 500, 700);
			}
			
			function ShowEventProposalHistory(id)
			{
				showPopUp('../PopUp/PopUpEventProposalHistoryView.aspx?id=' + id, '', 500, 700);
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="titlePage" style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"><asp:label id="lblTitle" Runat="server" Text="Event - Daftar Penyelesaian"></asp:label></div>
			<table cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<td class="titleField" style="HEIGHT: 18px" width="18%">Kode Dealer</td>
					<TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
					<TD style="HEIGHT: 18px">
						<table cellSpacing="0" cellPadding="0">
							<tr>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"
										runat="server" Width="136px"></asp:textbox></td>
								<td><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:label></td>
								<td></td>
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
								<TD><cc1:inticalendar id="calDari" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></TD>
								<TD>&nbsp;&nbsp;s.d&nbsp;&nbsp;</TD>
								<TD><cc1:inticalendar id="calSampai" runat="server" Enabled="False" TextBoxWidth="60"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top">Jenis Kegiatan</TD>
					<TD style="WIDTH: 1px" width="1" vAlign="top">:</TD>
					<TD colSpan="5">
						<asp:dropdownlist id="ddlJenisKegiatan" Runat="server" AutoPostBack="True"></asp:dropdownlist>
						<br>
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
						<asp:button id="btnCari" runat="server" Width="64px" CausesValidation="False" Text="Cari"></asp:button>
					</TD>
				</TR>
			</table>
			<br>
			<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="1988">
				<asp:datagrid id="dtgEvent" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
					PageSize="25" AutoGenerateColumns="False">
					<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
					<ItemStyle BackColor="White"></ItemStyle>
					<HeaderStyle ForeColor="White"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
						<asp:TemplateColumn ItemStyle-CssClass="titleTablePromo">
							<HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
							<HeaderTemplate>
								<INPUT id="chkAllItems" onclick="CheckAll('chkSelect',document.all.chkAllItems.checked)"
									type="checkbox">
							</HeaderTemplate>
							<ItemStyle BackColor="white"></ItemStyle>
							<ItemTemplate>
								<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No" ItemStyle-Wrap="True">
							<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="EventParameter.EventName" HeaderText="Nama Kegiatan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblEventName" Text='<%# DataBinder.Eval(Container, "DataItem.EventParameter.EventName") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ActivityPlace" HeaderText="Tempat Kegiatan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="lblActivityPlace" Text='<%# DataBinder.Eval(Container, "DataItem.ActivityPlace") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ActivitySchedule" HeaderText="Tanggal Acara">
							<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server" ID="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.ActivitySchedule", "{0:dd/MM/yyyy}") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id=Label8 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="TotalCost" HeaderText="Biaya Diajukan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:N0}") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="ApproveCost" HeaderText="Biaya Disetujui">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApproveCost", "{0:N0}") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Owner" HeaderText="Jumlah Undangan Owner">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Owner") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Driver" HeaderText="Jumlah Undangan Driver">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Driver") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OwnerAttendant" HeaderText="Jumlah Hadir Owner">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OwnerAttendant") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="DriverAttendant" HeaderText="Jumlah Hadir Driver">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DriverAttendant") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="InvitationNumber" HeaderText="Jumlah Undangan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InvitationNumber") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="AttendantNumber" HeaderText="Undangan Hadir">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttendantNumber") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="PercentageAttendent" HeaderText="Persentase">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PercentageAttendent") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Penilaian MMKSI">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:LinkButton Runat="server" id="lnkPenilaianKTB" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="DownloadPenilaianKTB" >
								</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Lap. Acara">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<asp:LinkButton Runat="server" id="lnkLaporanAcara" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="DownloadLaporanAcara" >
								</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Lap. Penjualan">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<img alt="Lihat Lap Penjualan" src="../images/detail.gif" border="0" style="cursor:pointer" onclick='<%# DataBinder.Eval(Container, "DataItem.EventParameter.ID", "ShowSellingReport({0});") %>' />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Dokumentasi" ItemStyle-HorizontalAlign="Center">
							<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemTemplate>
								<IMG alt="Lihat" src="../images/aktif.gif" border="0" style="cursor:pointer" onclick='<%# DataBinder.Eval(Container, "DataItem.ID", "showDocumentation({0});") %>'>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<A href='../Event/FrmEventFinishing.aspx?id=<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									<IMG alt="Edit" src="../images/edit.gif" border="0"></A> <A href='../Event/FrmEventRequestProposal.aspx?id=<%# DataBinder.Eval(Container, "DataItem.ID") %>&amp;ActivityType=<%# DataBinder.Eval(Container, "DataItem.ActivityType.ID") %>&amp;GroupCode=<%# DataBinder.Eval(Container, "DataItem.ActivityType.GroupCode") %>'>
									<IMG alt="Lihat" src="../images/detail.gif" border="0"></A>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</DIV>
			<br>
			<asp:Button id="btnDownload" runat="server" Text="Download"></asp:Button>
		</form>
	</body>
</HTML>
