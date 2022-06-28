<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPPOEquipList.aspx.vb" Inherits="FrmSPPOEquipList"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSPPOEquipList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

        function ShowDocFlow(ponumber)
        {
            showPopUp('../PopUp/PopUpDocFlowEqpSPPOIndent.aspx?ponumber=' + ponumber,'',300,560,'');
        }
		
		function ShowPODetail(POID)
		{
			showPopUp('../PopUp/PopUpSPPODetail.aspx?poid=' + POID, '', 510, 700, null);
		}
		
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}

		function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;				
		}
		
		function PONOSelection(POSelection)
		{
			var txtNoPO= document.getElementById("txtNoPO");
			txtNoPO.value = POSelection;				
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
			<div class="titlePage">
				INDENT PART EQUIPMENT - Daftar SPPO Indent Part Equipment
			</div>
			<TABLE id="Table11" cellSpacing="1" cellPadding="2" border="0">
				<TR>
					<TD class="titleField" vAlign="top" style="WIDTH: 125px">Kode Dealer</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td vAlign="top" width="210" style="WIDTH: 210px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="ShowPPDealerSelection();"></asp:label></td>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 125px" vAlign="top">Tanggal PO</TD>
					<TD vAlign="top" width="1%">:</TD>
					<TD vAlign="top">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td>
									<cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar>
								</td>
								<td>
									&nbsp;s/d&nbsp;
								</td>
								<td>
									<cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" style="WIDTH: 125px">Nomor PO</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="210" style="WIDTH: 210px">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPONumber" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px" MaxLength="15"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" style="WIDTH: 125px">Nomor SO</TD>
					<TD vAlign="top" width="1%">:</TD>
					<TD style="WIDTH: 210px" width="210">
						<asp:TextBox id="txtSoNumber" runat="server" Width="160px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" style="WIDTH: 125px">Nomor Pengajuan</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="210" style="WIDTH: 210px">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="TxtRequestNo" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px" MaxLength="15"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" vAlign="top" style="WIDTH: 125px">Status Transfer</TD>
					<TD vAlign="top" width="1%">:</TD>
					<TD style="WIDTH: 210px" width="210">
						<asp:dropdownlist id="ddlTransferStatus" runat="server" Width="160px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField" style="WIDTH: 125px"></TD>
					<TD width="1%"></TD>
					<td>
						<asp:button id="btnSearch" runat="server" Text="Cari" width="60px"></asp:button>
					</td>
				</TR>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px">
				<asp:datagrid id="dgPO" runat="server" Width="100%" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False"
					BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True"
					BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="25">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
							<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
												document.forms[0].chkAllItems.checked)" />
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No.">
							<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="IndentTransferDesc" SortExpression="IndentTransfer" HeaderText="Status">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
							<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor PO">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblNoPO" Font-Underline="True" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>' runat="server" style="cursor:hand">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="PODate" SortExpression="PODate" HeaderText="Tanggal PO" DataFormatString="{0:dd MMM yyyy}">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblRequestNo" style="cursor:hand" Font-Underline="True" Text='<%# DataBinder.Eval(Container, "DataItem.RequestNo") %>' runat="server" >
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="SONumber" HeaderText="Nomor SO">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblSoNumber" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Jumlah Sisa PO (Rp.)">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblAmount" Text="" runat="server" style="cursor:hand;"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblDocFlow" runat="server" height="10px">
									<img style="cursor:hand" alt="Klik Disini untuk melihat document flow" src="../images/alur_flow.gif"
										border="0"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</div>
			<div>
				<asp:button id="Download" runat="server" Width="133px" Text="Download"></asp:button>
				<asp:button id="btnSubmit" runat="server" Width="133px" Text="Transfer ke SAP"></asp:button>
			</div>
		</form>
	</body>
</HTML>
