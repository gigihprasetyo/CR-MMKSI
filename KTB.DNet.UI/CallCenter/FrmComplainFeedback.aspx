<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="True" Codebehind="FrmComplainFeedback.aspx.vb" Inherits="FrmComplainFeedback" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Feedback</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript">
			function ShowDealerSelection()
			{
				showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer.split(';');
				var txtKodeDealer = document.getElementById("txtDealerCode");
				var lblDealerName = document.getElementById("lblDealerName");
				txtKodeDealer.value = tempParam[0];			
				lblDealerName.innerHTML = tempParam[1];	
			}
		
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
		</script>
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td class="titlePage">Feedback</td>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<TD class="titleField">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="60px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk Dealer" src="../images/popup.gif" border="0">
									</asp:label>
									<asp:requiredfieldvalidator id="valDealer" runat="server" ControlToValidate="txtDealerCode" ErrorMessage="Dealer harus dipilih">* Dealer harus dipilih</asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:Label ID="lblDealerName" Runat="server" EnableViewState="True"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD class="titleField">Status Feedback</TD>
								<TD width="1%">:</TD>
								<TD width="60%">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="264px"></asp:dropdownlist>
								</TD>
							</tr>
							<tr>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%">
									<asp:button id="btnShow" runat="server" width="70px" Text="Tampilkan"></asp:button>
								</td>
							</tr>
							<tr>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td width="60%"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
							<asp:datagrid id="dgFeedback" runat="server" Width="100%" PageSize="25" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False"
								CellSpacing="1" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ID" Visible="True">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer" Visible="False">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealer" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Survey">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSurveyDate" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TglSurvey") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Konsumen">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblConsumerName" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConsumerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keluhan">
										<HeaderStyle CssClass="titleTableCust"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblComplain" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Complain") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableCust"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Tanggapan"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
