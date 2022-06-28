<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmViewTrInhouse.aspx.vb" Inherits="FrmViewTrInhouse" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>List Inhouse Training Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtDealerSearchCode");
				txtDealer.value = selectedCode
				txtDealer.focus();
			}			
			
			function ShowPPClassSelectionMany()
			{
				var txtKode = document.getElementById("txtKodeKategori");
				showPopUp('../PopUp/PopUpClassSelectionMany.aspx?CourseCode=' + txtKode.value,'',500,760,classSelectionMany);
			}		
			function classSelectionMany(selectedClass)
			{
				var txtKode = document.getElementById("txtKodeKelas");
				txtKode.value = selectedClass;			
			}
				
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblHeader" runat="server">TRAINING - List Pendaftaran</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<asp:panel id="pnlDealerSearch" Runat="server" Visible="False" Width="100%">
								<TBODY>
									<TR>
										<TD class="titleField" width="20%"></TD>
										<TD width="1%"></TD>
										<TD style="WIDTH: 216px" width="216"></TD>
										<TD class="titleField" style="WIDTH: 28px" width="28"></TD>
									</TR>
							</asp:panel>
							<TR>
								<TD class="titleField" width="20%">Dealer Code</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 216px" width="216">
									<asp:textbox id="txtDealerSearchCode" runat="server" Width="152px" Visible="True" ></asp:textbox>
									<asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 28px" width="28"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Kode Kelas</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 216px" width="216">
									<asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKodeKelas" runat="server" Width="100"
										MaxLength="20"></asp:textbox>
									<asp:label id="lblPopUpClass" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
								<TD class="titleField" style="WIDTH: 28px" width="28"></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">No Report</TD>
								<td width="1%">:</td>
								<TD width="216" style="WIDTH: 216px">
									<asp:textbox id="txtCode" runat="server" Width="100" MaxLength="20"></asp:textbox>
								</TD>
								<TD class="titleField" width="28" style="WIDTH: 28px"></TD>
							</TR>
							<TR>
								<TD class="titleField">
									Periode 
									Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</TD>
								<TD>:</TD>
								<TD style="WIDTH: 216px">
									<table>
										<tr>
											<td>
												<asp:CheckBox id="cbDate" runat="server"></asp:CheckBox>
											</td>
											<td>
												<cc1:inticalendar id="icStart" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<td>
												<asp:Label id="Label3" runat="server">s.d</asp:Label>
											</td>
											<td>
												<cc1:inticalendar id="icEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar>
											</td>
											<TD align="right" style="WIDTH: 28px">
												<asp:button id="btnSearch" runat="server" Font-Bold="True" Text=" Cari "></asp:button>
											</TD>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD style="WIDTH: 216px"></TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" border="0" id="Table3">
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 321px" vAlign="top">
						<div id="div1" style="OVERFLOW: auto; WIDTH: 760px; HEIGHT: 338px">
							<asp:datagrid id="dtgMain" runat="server" Width="744px" CellSpacing="1" GridLines="Vertical" CellPadding="3"
								BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" Font-Size="Small" AllowPaging="True"
								PageSize="50">
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
											<asp:textbox id="txtID" runat="server" Width="0px" style="visibility:hidden;" Font-Size="Smaller" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:textbox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ReportDate" HeaderText="Tgl Report">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblReportDate runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReportDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="No Report">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
											<asp:TextBox id="txtOrganizationID" runat="server" Width="0px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.OrganizationID") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ApprovedBy1" HeaderText="Approval 1">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblApprovedBy1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedBy1") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ApprovedBy2" HeaderText="Approval 2">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblApprovedBy2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedBy2") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ApprovedBy3" HeaderText="Approval 3">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblApprovedBy3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ApprovedBy3") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnEdit" runat="server" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Rubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:DropDownList id="ddlStatus" runat="server" Visible="False"></asp:DropDownList>
						<asp:Button id="btnUpdate" runat="server" Text="Ubah Status" Enabled="False" Visible="False"></asp:Button><asp:button id="btnProsesCetak" runat="server" Text="Proses Cetak/Download" Enabled="False"
							CausesValidation="False" Visible="False"></asp:button></TD>
				</TR>
				</TBODY></TABLE>
		</form>
		<script>
	EnableDelete('cbItem');
		</script>
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
