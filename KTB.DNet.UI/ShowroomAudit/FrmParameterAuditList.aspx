<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmParameterAuditList.aspx.vb" Inherits="FrmParameterAuditList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmParameterAuditList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
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
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{				
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer;
			}	
			
			function ShowAuditParameter()
			{
				showPopUp('../PopUp/PopUpAuditParameters.aspx','',500,760,AuditSelection);
			}
			function AuditSelection(selectedData)
			{				
				var txtAuditNo= document.getElementById("txtAuditCode");
				txtAuditNo.value = selectedData;
			}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM AUDIT - Daftar Petunjuk 
						Pelaksanaan
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="10" src="../images/blank.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField" width="24%">Period</td>
					<td class="titleField" width="1%">:</td>
					<td class="titleField" width="75%">
						<asp:DropDownList ID="ddlYearPeriod" Runat="server"></asp:DropDownList>
					</td>
				</tr>
				<!-- refer bug 1094 
					<TR>
						<TD class="titleField" style="HEIGHT: 18px">Kode Dealer</TD>
						<TD class="titleField" style="HEIGHT: 18px">:</TD>
						<TD style="HEIGHT: 18px">
							<asp:textbox id="txtDealerCode" Runat="server"></asp:textbox>
							<asp:label id="lblSearchDealer" runat="server">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
							<asp:Label id="lblKodeDealer" runat="server">Label</asp:Label></TD>
					</TR>
				</asp:panel>
				refer bug 1094 -->
				<TR>
					<TD class="titleField" style="HEIGHT: 18px">Kode Audit</TD>
					<TD class="titleField" style="HEIGHT: 18px">:</TD>
					<TD style="HEIGHT: 18px">
						<asp:textbox id="txtAuditCode" Runat="server"></asp:textbox>
						<asp:label id="lblAuditSearch" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowAuditParameter()"></asp:label></TD>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td>
						<asp:Button id="btnCari" runat="server" Text="Cari" Width="56px"></asp:Button></td>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dtgParamAuditList" runat="server" BorderStyle="None" CellPadding="3" BorderWidth="1px"
								BorderColor="#CDCDCD" AutoGenerateColumns="False" BackColor="#E0E0E0" Width="100%" AllowCustomPaging="True" PageSize="25" AllowPaging="True"
								AllowSorting="True">
								<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNo runat="server" NAME="lblNo" text="<%# container.itemindex+1 %>">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Code" HeaderText="Kode Audit">
										<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblAuditCode" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Periode">
										<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPeriod" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.Period") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="JukLakFile" HeaderText="Petunjuk Pelaksana">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnJuklakFile" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.JukLakFile") %>' CommandName="DownloadJukLak">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="AssessmentItem" HeaderText="Item Penilaian">
										<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnItemScore" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.AssessmentItem") %>' CommandName="ItemScore">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblID" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<TR>
					<TD>
						<asp:Button id="btnRilis" runat="server" Width="64px" Text="Rilis"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
